Imports Telerik.WinControls
Public Class Control_Depto_trabajo
    Dim nuevo As Boolean
    Dim activo As Boolean
    Private Sub Control_Equipos_trabajo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        cargar_info()
        cargar_listas()
        limpiar()
        ventanas(False, False, True)
        activo = False
    End Sub
    Private Sub cargar_listas()
        Me.lstEquipo.Cargar("select Id_Equipo,Nombre from Equipos ")
    End Sub
    Private Sub cargar_info()
        Dim consulta As String = "SELECT Equipos.Id_Equipo, Equipos.Nombre, Equipos.Fecha_Captura "
        consulta &= " FROM     Equipos "
        Dim sql As String = "select * from (" & consulta & ") as tabla"
        Dim sql2 As String = "(" & consulta & ") as tabla"
        Dim sql4 As String = "(" & consulta & " and datepart(year,Clientes.Fecha_captura) = " & Me.TablaEquipos.ComboAño.Text & " and datepart(month,Clientes.Fecha_captura) =" & Me.TablaEquipos.ComboMes.Text & " ) as tabla"
        Dim sql5 As String = "(" & consulta & " and datepart(year,Clientes.Fecha_captura) = " & Me.TablaEquipos.ComboAño.Text & "  ) as tabla"
        Dim sql3 As String = "(" & consulta & "  ) as tabla"
        If Me.TablaEquipos.ComboAño.Text = "*" Then
            Me.TablaEquipos.SqlSelect = sql3
        ElseIf Me.TablaEquipos.ComboAño.Text <> "*" And Me.TablaEquipos.ComboMes.Text <> "*" Then
            Me.TablaEquipos.SqlSelect = sql3 & " where datepart(year, Fecha_captura) = " & TablaEquipos.fecha_año & " and datepart(month, Fecha_captura) = " & Me.TablaEquipos.fecha_mes & " "
        ElseIf Me.TablaEquipos.ComboAño.Text <> "*" And Me.TablaEquipos.ComboMes.Text = "*" Then
            Me.TablaEquipos.SqlSelect = sql3 & " where datepart(year, Fecha_captura) = " & TablaEquipos.fecha_año & "  "
        End If
        Me.TablaEquipos.Ordenar(True, "Id_Equipo")
        If Me.TablaEquipos.ComboAño.Text <> "*" And Me.TablaEquipos.ComboMes.Text = "*" Then
            Me.TablaEquipos.Tablaconsulta(sql2, sql5)
            Me.TablaEquipos.Cargar("P_Master", sql, , "Fecha_captura", " where datepart(year, Fecha_captura) = " & Me.TablaEquipos.ComboAño.Text & "")
        Else
            Me.TablaEquipos.Tablaconsulta(sql2, sql4)
            Me.TablaEquipos.Cargar("P_Master", sql, , "Fecha_captura", " where datepart(year, Fecha_captura) = " & Me.TablaEquipos.ComboAño.Text & " and datepart(month, Fecha_captura) = " & Me.TablaEquipos.ComboMes.Text & "")
        End If
        Me.TablaEquipos.Ordenar("Id_Equipo")
        Me.TablaEquipos.Tabla.Columns(0).Visible = False
    End Sub
    Private Sub Limpiar()
        Me.lstEquipo.SelectText = ""
    End Sub

    Private Sub TablaEquipos_cerrar() Handles TablaEquipos.Cerrar
        Me.Close()
    End Sub

    Private Sub TablaEquipos_eliminar() Handles TablaEquipos.Eliminar
        eliminar()
        Me.TablaEquipos.CmdActualizar.PerformClick()
    End Sub
    Private Sub Guarda_nuevo()
        Dim sql As String = ""
        sql &= " INSERT INTO dbo.Equipos"
        sql &= " 	("
        sql &= "         Equipos.Nombre,"
        sql &= "         Fecha_Captura"
        sql &= " 	)"
        sql &= "         VALUES "
        sql &= " 	("
        sql &= " 	'" & Me.lstEquipo.SelectText & "'," '@nombre
        sql &= " 	" & Eventos.Sql_hoy() & "" '@f_captura
        sql &= " 	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Equipos_N", sql)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
    End Sub
    Private Sub Guarda_edicion(ByVal id_equipo As Integer)
        Dim sql As String = ""
        sql &= "         Update dbo.Equipos "
        sql &= " SET "
        sql &= " 	Equipos.Nombre = '" & Me.lstEquipo.SelectText & "' , Fecha_Captura = " & Eventos.Sql_hoy() & " where Id_Equipo = " & id_equipo & "" '@movto
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Equipos_E", sql)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
    End Sub
    Private Sub Eliminar()
        If MessageBox.Show("Realmente deseas eliminar el registro?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
            Dim sql As String = "Delete from Equipos where Id_Equipo = " & Me.lstEquipo.SelectItem & ""
            If Eventos.Comando_sql(sql) > 0 Then
                Eventos.Insertar_usuariol("Equipos_D", sql)
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


    Private Sub TablaEquipos_guardar() Handles TablaEquipos.Guardar
        If nuevo = True Then
            guarda_nuevo()
        Else
            If Me.lstEquipo.SelectText <> "" Then
                guarda_edicion(Me.lstEquipo.SelectItem)
            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se ha seleccionado un Equipo...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Me.Text = Ms.ToString()

                Exit Sub
            End If
        End If
        Me.TablaEquipos.CmdActualizar.PerformClick()
    End Sub


    Private Sub TablaEquipos_nuevo() Handles TablaEquipos.Nuevo
        activo = True
        limpiar()
        activo = False
        nuevo = True
        ventanas(True, False, False)
    End Sub

    Private Sub TablaEquipos_refrescar() Handles TablaEquipos.Refrescar
        activo = True
        cargar_info()
        cargar_listas()
        limpiar()
        ventanas(False, False, True)
        activo = False
    End Sub

    Private Sub TablaEquipos_registro() Handles TablaEquipos.Registro
        nuevo = False
        ventanas(False, True, False)
        cargar_datos(Me.TablaEquipos.Registro_columa(0))
    End Sub
    Private Sub cargar_datos(ByVal Equipo As Integer)
        Dim sql As String = " select * from Equipos where id_equipo  =" & Equipo & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.lstEquipo.SelectItem = ds.Tables(0).Rows(0)("Id_equipo")
        Else
            activo = True
            limpiar()
            activo = False
            Exit Sub
        End If
    End Sub
    Private Sub ventanas(ByVal nuevo As Boolean, ByVal edit As Boolean, ByVal load As Boolean)
        If nuevo = True Then
            Me.TablaEquipos.CmdEliminar.Enabled = False
            Me.TablaEquipos.CmdGuardar.Enabled = True
            Me.TablaEquipos.CmdNuevo.Enabled = False
        ElseIf edit = True Then
            Me.TablaEquipos.CmdEliminar.Enabled = True
            Me.TablaEquipos.CmdGuardar.Enabled = True
            Me.TablaEquipos.CmdNuevo.Enabled = True
        ElseIf load = True Then
            Me.TablaEquipos.CmdEliminar.Enabled = False
            Me.TablaEquipos.CmdGuardar.Enabled = False
            Me.TablaEquipos.CmdNuevo.Enabled = True
        End If
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Control de Departamentos")
    'End Sub


End Class
