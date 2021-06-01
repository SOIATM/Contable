Imports Telerik.WinControls
Public Class Designar_Usuarios_Equipos
    Dim activo As Boolean
    Dim nuevo As Boolean
    Private Sub Designar_Usuarios_Equipos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        equipos()
        cargar_listas()
        Limpiar()
        ventanas(False, False, True)
        activo = False
    End Sub
    Private Sub equipos()
        Dim consulta As String = "SELECT Usuarios_Equipos.Id_Usuarios_Equipos, Usuarios.Usuario, Equipos.Nombre, Usuarios_Equipos.Fecha_Asignacion"
        consulta &= " FROM     Usuarios INNER JOIN"
        consulta &= " Usuarios_Equipos ON Usuarios.ID_Usuario = Usuarios_Equipos.ID_Usuario INNER JOIN"
        consulta &= " Equipos ON Usuarios_Equipos.Id_Equipo = Equipos.Id_Equipo"
        Dim sql As String = "select * from (" & consulta & ") as tabla"
        Dim sql2 As String = "(" & consulta & ") as tabla"
        Dim sql4 As String = "(" & consulta & " and datepart(year,Usuarios.Fecha_Asignacion) = " & Me.TablaEquipos_Usuarios.ComboAño.Text & " and datepart(month,Usuarios.Fecha_Asignacion) =" & Me.TablaEquipos_Usuarios.ComboMes.Text & " ) as tabla"
        Dim sql5 As String = "(" & consulta & " and datepart(year,Usuarios.Fecha_Asignacion) = " & Me.TablaEquipos_Usuarios.ComboAño.Text & "  ) as tabla"
        Dim sql3 As String = "(" & consulta & "  ) as tabla"
        If Me.TablaEquipos_Usuarios.ComboAño.Text = "*" Then
            Me.TablaEquipos_Usuarios.SqlSelect = sql3
        ElseIf Me.TablaEquipos_Usuarios.ComboAño.Text <> "*" And Me.TablaEquipos_Usuarios.ComboMes.Text <> "*" Then
            Me.TablaEquipos_Usuarios.SqlSelect = sql3 & " where datepart(year, Fecha_Asignacion) = " & Me.TablaEquipos_Usuarios.fecha_año & " and datepart(month, Fecha_Asignacion) = " & Me.TablaEquipos_Usuarios.fecha_mes & " "
        ElseIf Me.TablaEquipos_Usuarios.ComboAño.Text <> "*" And Me.TablaEquipos_Usuarios.ComboMes.Text = "*" Then
            Me.TablaEquipos_Usuarios.SqlSelect = sql3 & " where datepart(year, Fecha_Asignacion) = " & Me.TablaEquipos_Usuarios.fecha_año & "  "
        End If
        Me.TablaEquipos_Usuarios.Ordenar(True, "Id_Usuarios_Equipos")
        If Me.TablaEquipos_Usuarios.ComboAño.Text <> "*" And Me.TablaEquipos_Usuarios.ComboMes.Text = "*" Then
            Me.TablaEquipos_Usuarios.Tablaconsulta(sql2, sql5)
            Me.TablaEquipos_Usuarios.Cargar("P_Master", sql, , "Fecha_Asignacion", " where datepart(year, Fecha_Asignacion) = " & Me.TablaEquipos_Usuarios.ComboAño.Text & "")
        Else
            Me.TablaEquipos_Usuarios.Tablaconsulta(sql2, sql4)
            Me.TablaEquipos_Usuarios.Cargar("P_Master", sql, , "Fecha_Asignacion", " where datepart(year, Fecha_Asignacion) = " & Me.TablaEquipos_Usuarios.ComboAño.Text & " and datepart(month, Fecha_Asignacion) = " & Me.TablaEquipos_Usuarios.ComboMes.Text & "")
        End If
        Me.TablaEquipos_Usuarios.Ordenar("Id_Usuarios_Equipos")
        Me.TablaEquipos_Usuarios.Tabla.Columns(0).Visible = False
    End Sub
    Private Sub cargar_listas()
        Me.lstUsuario.Cargar("select ID_Usuario, Usuario from Usuarios ")
        Me.lstEquipos.Cargar(" select Id_Equipo, Nombre from Equipos ")
    End Sub
    Private Sub Limpiar()
        Me.lstUsuario.SelectText = ""
        Me.lstEquipos.SelectText = ""
        Me.Label3.Text = ""
    End Sub
    Private Sub Carga_registro(ByVal registro As Integer)
        Dim sql As String = "SELECT Equipos.Nombre, Usuarios.Usuario
                                FROM     Equipos INNER JOIN
                                 Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                                 Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario where Id_Usuarios_Equipos = " & registro & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.lstUsuario.SelectText = ds.Tables(0).Rows(0)("Usuario")
            Me.lstEquipos.SelectText = ds.Tables(0).Rows(0)("Nombre")
        Else
            activo = True
            Limpiar()
            activo = False
            Exit Sub
        End If
    End Sub
    Private Sub Nuevo_control()
        Dim sql As String = "INSERT INTO dbo.Usuarios_Equipos"
        sql &= " 	("
        sql &= "         Id_Equipo,"
        sql &= "         Id_Usuario,"
        sql &= "         Fecha_Asignacion"
        sql &= " 	)"
        sql &= "         VALUES"
        sql &= " 	("
        sql &= " 	" & Me.lstEquipos.SelectItem & "," '@id_equipo
        sql &= " " & Me.lstUsuario.SelectItem & "	," '@id_cliente
        sql &= " 	" & Eventos.Sql_hoy() & "" '@fecha_asignacion
        sql &= " 	)"
        sql &= " "
        If Eventos.Comando_sql(sql) > 0 Then

        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
    End Sub
    Private Sub Edita_equipo()
        Dim sql As String = "UPDATE dbo.Usuarios_Equipos "
        sql &= " SET  Id_Equipo = " & Me.lstEquipos.SelectItem & " ," '@id_equipo
        sql &= " 	Id_Usuario = " & Me.lstUsuario.SelectItem & " ," '@id_cliente
        sql &= " 	Fecha_Asignacion = " & Eventos.Sql_hoy() & "  where Id_Usuarios_Equipos = " & Me.Label3.Text & " " '@fecha_asignacion
        If Eventos.Comando_sql(sql) > 0 Then

        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
    End Sub
    Private Sub Eliminar_equipo()
        If MessageBox.Show("Realmente deseas eliminar el registro?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
            Dim sql As String = "Delete from Usuarios_Equipos where Id_Usuarios_Equipos = " & Me.Label3.Text & ""
            If Eventos.Comando_sql(sql) > 0 Then
                Eventos.Insertar_usuariol("Usuarios_Equipos_D", sql)
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

    Private Sub TablaEquipos_Usuarios_cerrar() Handles TablaEquipos_Usuarios.Cerrar
        Me.Close()
    End Sub

    Private Sub TablaEquipos_Usuarios_eliminar() Handles TablaEquipos_Usuarios.Eliminar
        If Me.Label3.Text <> "" Then
            Eliminar_equipo()
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se ha seleccionado ningun registro...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
        Me.TablaEquipos_Usuarios.CmdActualizar.PerformClick()
    End Sub

    Private Sub TablaEquipos_Usuarios_guardar() Handles TablaEquipos_Usuarios.Guardar
        If nuevo = True Then
            Nuevo_control()
        Else
            If Me.Label3.Text <> "" Then
                Edita_equipo()
            Else
                Exit Sub
            End If
        End If
        Me.TablaEquipos_Usuarios.CmdActualizar.PerformClick()
    End Sub

    Private Sub TablaEquipos_Usuarios_nuevo() Handles TablaEquipos_Usuarios.Nuevo
        activo = True
        Limpiar()
        activo = False
        nuevo = True
        ventanas(True, False, False)
    End Sub

    Private Sub TablaEquipos_Usuarios_refrescar() Handles TablaEquipos_Usuarios.Refrescar
        activo = True
        equipos()
        cargar_listas()
        Limpiar()
        ventanas(False, False, True)
        activo = False
    End Sub

    Private Sub TablaEquipos_Usuarios_registro() Handles TablaEquipos_Usuarios.Registro
        nuevo = False
        Carga_registro(Me.TablaEquipos_Usuarios.Registro_columa(0))
        Me.Label3.Text = Me.TablaEquipos_Usuarios.Registro_columa(0)
        ventanas(False, True, False)
    End Sub
    Private Sub ventanas(ByVal nuevo As Boolean, ByVal edit As Boolean, ByVal load As Boolean)
        If nuevo = True Then
            Me.TablaEquipos_Usuarios.CmdEliminar.Enabled = False
            Me.TablaEquipos_Usuarios.CmdGuardar.Enabled = True
            Me.TablaEquipos_Usuarios.CmdNuevo.Enabled = False
        ElseIf edit = True Then
            Me.TablaEquipos_Usuarios.CmdEliminar.Enabled = True
            Me.TablaEquipos_Usuarios.CmdGuardar.Enabled = True
            Me.TablaEquipos_Usuarios.CmdNuevo.Enabled = True
        ElseIf load = True Then
            Me.TablaEquipos_Usuarios.CmdEliminar.Enabled = False
            Me.TablaEquipos_Usuarios.CmdGuardar.Enabled = False
            Me.TablaEquipos_Usuarios.CmdNuevo.Enabled = True
        End If
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Asignación de Departamentos")
    'End Sub
End Class
