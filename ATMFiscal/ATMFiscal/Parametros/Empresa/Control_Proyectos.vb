Imports Telerik.WinControls
Public Class Control_Proyectos
    Dim activo As Boolean
    Dim nuevo As Boolean
    Private Sub Control_Proyectos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        Equipos()
        Cargar_listas()
        Limpiar()
        Ventanas(False, False, True)
        activo = False
    End Sub
    Private Sub Equipos()
        Dim consulta As String = "SELECT Control_Equipos_Clientes.Id_Control_Equipos_Clientes, Equipos.Nombre, Empresa.Reg_fed_causantes, "
        consulta &= "         Empresa.Razon_Social, Control_Equipos_Clientes.Fecha_Asignacion"
        consulta &= " FROM     Control_Equipos_Clientes INNER JOIN"
        consulta &= " Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN"
        consulta &= " Empresa ON Control_Equipos_Clientes.Id_Empresa = Empresa.Id_Empresa"
        Dim sql As String = "select * from (" & consulta & ") as tabla"
        Dim sql2 As String = "(" & consulta & ") as tabla"
        Dim sql4 As String = "(" & consulta & " and datepart(year,Usuarios.Fecha_Asignacion) = " & Me.TablaControlEquipos.ComboAño.Text & " and datepart(month,Usuarios.Fecha_Asignacion) =" & Me.TablaControlEquipos.ComboMes.Text & " ) as tabla"
        Dim sql5 As String = "(" & consulta & " and datepart(year,Usuarios.Fecha_Asignacion) = " & Me.TablaControlEquipos.ComboAño.Text & "  ) as tabla"
        Dim sql3 As String = "(" & consulta & "  ) as tabla"
        If Me.TablaControlEquipos.ComboAño.Text = "*" Then
            Me.TablaControlEquipos.SqlSelect = sql3
        ElseIf Me.TablaControlEquipos.ComboAño.Text <> "*" And Me.TablaControlEquipos.ComboMes.Text <> "*" Then
            Me.TablaControlEquipos.SqlSelect = sql3 & " where datepart(year, Fecha_Asignacion) = " & Me.TablaControlEquipos.fecha_año & " and datepart(month, Fecha_Asignacion) = " & Me.TablaControlEquipos.fecha_mes & " "
        ElseIf Me.TablaControlEquipos.ComboAño.Text <> "*" And Me.TablaControlEquipos.ComboMes.Text = "*" Then
            Me.TablaControlEquipos.SqlSelect = sql3 & " where datepart(year, Fecha_Asignacion) = " & Me.TablaControlEquipos.fecha_año & "  "
        End If
        Me.TablaControlEquipos.Ordenar(True, "Id_Control_Equipos_Clientes")
        If Me.TablaControlEquipos.ComboAño.Text <> "*" And Me.TablaControlEquipos.ComboMes.Text = "*" Then
            Me.TablaControlEquipos.Tablaconsulta(sql2, sql5)
            Me.TablaControlEquipos.Cargar("P_Master", sql, , "Fecha_Asignacion", " where datepart(year, Fecha_Asignacion) = " & Me.TablaControlEquipos.ComboAño.Text & "")
        Else
            Me.TablaControlEquipos.Tablaconsulta(sql2, sql4)
            Me.TablaControlEquipos.Cargar("P_Master", sql, , "Fecha_Asignacion", " where datepart(year, Fecha_Asignacion) = " & Me.TablaControlEquipos.ComboAño.Text & " and datepart(month, Fecha_Asignacion) = " & Me.TablaControlEquipos.ComboMes.Text & "")
        End If
        Me.TablaControlEquipos.Ordenar("Id_Control_Equipos_Clientes")
        Me.TablaControlEquipos.Tabla.Columns(0).Visible = False
    End Sub
    Private Sub Cargar_listas()
        Me.lstClientes.Cargar("select Id_Empresa, Razon_Social from Empresa ")
        Me.lstEquipo.Cargar(" select Id_Equipo, Nombre from Equipos ")
    End Sub
    Private Sub Limpiar()
        Me.lstClientes.SelectText = ""
        Me.lstEquipo.SelectText = ""
        Me.Label1.Text = ""
    End Sub
    Private Sub Nuevo_control()
        Dim sql As String = "INSERT INTO dbo.Control_Equipos_Clientes"
        sql &= " 	("
        sql &= "         Id_Equipo,"
        sql &= "         Id_Empresa,"
        sql &= "         Fecha_Asignacion"
        sql &= " 	)"
        sql &= "         VALUES"
        sql &= " 	("
        sql &= " 	" & Me.lstEquipo.SelectItem & "," '@id_equipo
        sql &= " " & Me.lstClientes.SelectItem & "	," '@id_cliente
        sql &= " 	" & Eventos.Sql_hoy() & "" '@fecha_asignacion
        sql &= " 	)"
        sql &= " "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("C_Equipos_N", sql)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
    End Sub
    Private Sub Edita_equipo()
        Dim sql As String = "UPDATE dbo.Control_Equipos_Clientes"
        sql &= " SET  Id_Equipo = " & Me.lstEquipo.SelectItem & " ," '@id_equipo
        sql &= " 	Id_Empresa = " & Me.lstClientes.SelectItem & " ," '@id_cliente
        sql &= " 	Fecha_Asignacion = " & Eventos.Sql_hoy() & "  where id_control_equipos_clientes = " & Me.Label1.Text & " " '@fecha_asignacion
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("C_Equipos_M", sql)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
    End Sub
    Private Sub Eliminar_equipo()
        If MessageBox.Show("Realmente deseas eliminar el registro?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
            Dim sql As String = "Delete from Control_Equipos_Clientes where id_control_equipos_clientes = " & Me.Label1.Text & ""
            If Eventos.Comando_sql(sql) > 0 Then
                Eventos.Insertar_usuariol("C_Equipos_D", sql)
            Else

                RadMessageBox.SetThemeName("MaterialBlueGrey")
                Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo eliminar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Me.Text = Ms.ToString()
                Exit Sub
            End If
        Else
            Exit Sub
        End If
    End Sub

    Private Sub TablaControlEquipos_cerrar() Handles TablaControlEquipos.Cerrar
        Me.Close()
    End Sub

    Private Sub TablaControlEquipos_eliminar() Handles TablaControlEquipos.Eliminar
        If Me.Label1.Text <> "" Then
            Eliminar_equipo()
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se ha seleccionado ningun registro...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
        Me.TablaControlEquipos.CmdActualizar.PerformClick()
    End Sub

    Private Sub TablaControlEquipos_guardar() Handles TablaControlEquipos.Guardar
        If nuevo = True Then
            Nuevo_control()
        Else
            If Me.Label1.Text <> "" Then
                Edita_equipo()
            Else
                Exit Sub
            End If
        End If
        Me.TablaControlEquipos.CmdActualizar.PerformClick()
    End Sub

    Private Sub TablaControlEquipos_nuevo() Handles TablaControlEquipos.Nuevo
        activo = True
        Limpiar()
        activo = False
        Ventanas(True, False, False)
        nuevo = True
    End Sub

    Private Sub TablaControlEquipos_refrescar() Handles TablaControlEquipos.Refrescar
        activo = True
        Equipos()
        Cargar_listas()
        Limpiar()
        Ventanas(False, False, True)
        activo = False
    End Sub

    Private Sub TablaControlEquipos_registro() Handles TablaControlEquipos.Registro
        nuevo = False
        Carga_registro(Me.TablaControlEquipos.Registro_columa(0))
        Me.Label1.Text = Me.TablaControlEquipos.Registro_columa(0)
        Ventanas(False, True, False)
    End Sub
    Private Sub Carga_registro(ByVal registro As Integer)
        Dim ds As DataSet = Eventos.Obtener_DS(" Select * from Control_Equipos_Clientes where Id_Control_Equipos_Clientes = " & registro & "")
        If ds.Tables(0).Rows.Count > 0 Then
            Me.lstClientes.SelectItem = ds.Tables(0).Rows(0)("Id_Empresa")
            Me.Label1.Text = ds.Tables(0).Rows(0)("Id_Equipo")
            Me.lstEquipo.SelectItem = ds.Tables(0).Rows(0)("Id_Equipo")
        Else
            activo = True
            Limpiar()
            activo = False
            Exit Sub
        End If
    End Sub
    Private Sub Ventanas(ByVal nuevo As Boolean, ByVal edit As Boolean, ByVal load As Boolean)
        If nuevo = True Then
            Me.TablaControlEquipos.CmdEliminar.Enabled = False
            Me.TablaControlEquipos.CmdGuardar.Enabled = True
            Me.TablaControlEquipos.CmdNuevo.Enabled = False
        ElseIf edit = True Then
            Me.TablaControlEquipos.CmdEliminar.Enabled = True
            Me.TablaControlEquipos.CmdGuardar.Enabled = True
            Me.TablaControlEquipos.CmdNuevo.Enabled = True
        ElseIf load = True Then
            Me.TablaControlEquipos.CmdEliminar.Enabled = False
            Me.TablaControlEquipos.CmdGuardar.Enabled = False
            Me.TablaControlEquipos.CmdNuevo.Enabled = True
        End If
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs)
    '    Eventos.Abrir_Manual("Asignación de Proyectos")
    'End Sub
End Class
