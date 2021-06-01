Imports Telerik.WinControls
Public Class Control_de_Usuarios
    Dim Activo As Boolean
    Dim Nuevo As Boolean

    Private Sub Control_de_Usuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Activo = True
        Cargar_Info()
        Cargarlistas()
        Limpiar()
        ventanas(False, False, True)
        Me.TablaUsuarios.Tabla.Columns(3).Visible = False
        Activo = False
    End Sub
    Private Sub Cargar_Info()
        Dim consulta As String = "SELECT Usuarios.ID_Usuario, Usuarios.Id_Matricula, Usuarios.Usuario, "
        consulta &= " Usuarios.Contraseña, Empresa.Razon_social, Usuarios.Area, Usuarios.Telefono,"
        consulta &= " Usuarios.Extrencion, Usuarios.Correo, Usuarios.P_Master,Usuarios.P_Auditor,Usuarios.P_Importar, Usuarios.P_Acceso, Usuarios.P_Unplus, Usuarios.P_Reportes, Usuarios.P_Impuestos, Usuarios.P_Anual, Usuarios.P_Depreciaciones, Usuarios.P_Parametros, Usuarios.F_captura, Usuarios.Movto "
        consulta &= " FROM     Usuarios INNER JOIN Empresa ON Usuarios.ID_Empresa = Empresa.ID_empresa"
        Dim sql As String = "select * from (" & consulta & ") as tabla"
        Dim sql2 As String = "(" & consulta & ") as tabla"
        Dim sql4 As String = "(" & consulta & " and datepart(year,Usuarios.F_captura) = " & Me.TablaUsuarios.ComboAño.Text & " and datepart(month,Usuarios.F_captura) =" & Me.TablaUsuarios.ComboMes.Text & " ) as tabla"
        Dim sql5 As String = "(" & consulta & " and datepart(year,Usuarios.F_captura) = " & Me.TablaUsuarios.ComboAño.Text & "  ) as tabla"
        Dim sql3 As String = "(" & consulta & "  ) as tabla"
        If Me.TablaUsuarios.ComboAño.Text = "*" Then
            Me.TablaUsuarios.SqlSelect = sql3
        ElseIf Me.TablaUsuarios.ComboAño.Text <> "*" And Me.TablaUsuarios.ComboMes.Text <> "*" Then
            Me.TablaUsuarios.SqlSelect = sql3 & " where datepart(year, F_captura) = " & Me.TablaUsuarios.fecha_año & " and datepart(month, F_captura) = " & Me.TablaUsuarios.fecha_mes & " "
        ElseIf Me.TablaUsuarios.ComboAño.Text <> "*" And Me.TablaUsuarios.ComboMes.Text = "*" Then
            Me.TablaUsuarios.SqlSelect = sql3 & " where datepart(year, F_captura) = " & Me.TablaUsuarios.fecha_año & "  "
        End If
        Me.TablaUsuarios.Ordenar(True, "ID_usuario")
        If Me.TablaUsuarios.ComboAño.Text <> "*" And Me.TablaUsuarios.ComboMes.Text = "*" Then
            Me.TablaUsuarios.Tablaconsulta(sql2, sql5)
            Me.TablaUsuarios.Cargar("P_Unplus", sql, , "F_captura", " where datepart(year, F_captura) = " & Me.TablaUsuarios.ComboAño.Text & "")
        Else
            Me.TablaUsuarios.Tablaconsulta(sql2, sql4)
            Me.TablaUsuarios.Cargar("P_Unplus", sql, , "F_captura", " where datepart(year, F_captura) = " & Me.TablaUsuarios.ComboAño.Text & " and datepart(month, F_captura) = " & Me.TablaUsuarios.ComboMes.Text & "")
        End If
        Me.TablaUsuarios.Ordenar("ID_usuario")
        Try
            Me.TablaUsuarios.Tabla.Columns(0).Visible = False
            Me.TablaUsuarios.Tabla.Columns(1).Visible = False
        Catch ex As Exception

        End Try


    End Sub
    Private Sub Limpiar()
        Me.LstAcceso.SelectText = ""
        Me.lstArea.SelectText = ""
        Me.lstTotal.SelectText = ""
        Me.LstReportes.SelectText = ""
        Me.LstCorreo.SelectText = ""
        Me.lstEmpresa.SelectText = ""
        Me.LstAuditoria.SelectText = ""
        Me.LstImportar.SelectText = ""
        Me.lstExtencion.SelectText = ""
        Me.lstMaster.SelectText = ""
        Me.lstMatricula.SelectText = ""
        Me.LstTelefono.SelectText = ""
        Me.LstAnual.SelectText = ""
        Me.LstDep.SelectText = ""
        Me.LstImps.SelectText = ""
        Me.LstParametros.SelectText = ""
        Me.lstUsuarios.SelectText = ""

    End Sub
    Private Sub Cargarlistas()
        Me.LstAcceso.Cargar("SELECT 'M', 'Modificación' UNION SELECT 'N', 'Nada'")
        Me.lstTotal.Cargar("SELECT 'M', 'Modificación' UNION SELECT 'N', 'Nada'")
        Me.LstReportes.Cargar("SELECT 'M', 'Modificación' UNION SELECT 'N', 'Nada'")
        Me.LstAuditoria.Cargar("SELECT 'M', 'Modificación' UNION SELECT 'N', 'Nada'")
        Me.LstImportar.Cargar("SELECT 'M', 'Modificación' UNION SELECT 'N', 'Nada'")
        Me.lstArea.Cargar("SELECT Id_usuario , area from Usuarios")
        Me.LstCorreo.Cargar("SELECT Id_usuario , correo from Usuarios")
        Me.lstEmpresa.Cargar("SELECT ID_Empresa,Razon_social from Empresa ")
        Me.lstExtencion.Cargar("SELECT Id_usuario , Extrencion from Usuarios")
        Me.lstMaster.Cargar("SELECT 'M', 'Modificación' UNION SELECT 'N', 'Nada'")
        Me.lstMatricula.Cargar(" SELECT ID_Matricula ,  Personal.Nombres + ' ' + Personal.Ap_paterno + ' ' + Personal.Ap_materno from personal")
        Me.LstTelefono.Cargar("SELECT DISTINCT telefono , telefono from Usuarios")
        Me.lstUsuarios.Cargar("SELECT id_matricula , Usuario from Usuarios where id_empresa = 1")
        Me.LstDep.Cargar("SELECT 'M', 'Modificación' UNION SELECT 'N', 'Nada'")
        Me.LstImps.Cargar("SELECT 'M', 'Modificación' UNION SELECT 'N', 'Nada'")
        Me.LstParametros.Cargar("SELECT 'M', 'Modificación' UNION SELECT 'N', 'Nada'")
        Me.LstAnual.Cargar("SELECT 'M', 'Modificación' UNION SELECT 'N', 'Nada'")
    End Sub
    Private Sub TablaUsuarios_cerrar() Handles TablaUsuarios.Cerrar
        Me.Close()
    End Sub
    Private Sub TablaUsuarios_eliminar() Handles TablaUsuarios.Eliminar
        eliminar()
        Me.TablaUsuarios.CmdActualizar.PerformClick()
    End Sub
    Private Sub TablaUsuarios_guardar() Handles TablaUsuarios.Guardar
        If Nuevo = True Then
            guarda_nuevo()
        Else
            guarda_edicion(Me.lstUsuarios.SelectItem)
        End If
        Me.TablaUsuarios.CmdActualizar.PerformClick()
    End Sub
    Private Sub TablaUsuarios_nuevo() Handles TablaUsuarios.Nuevo
        Nuevo = True
        Activo = True
        Limpiar()
        ventanas(True, False, False)
        Activo = False
    End Sub
    Private Sub TablaUsuarios_refrescar() Handles TablaUsuarios.Refrescar
        Activo = True
        Cargar_Info()
        Cargarlistas()
        Limpiar()
        ventanas(False, False, True)
        Activo = False
    End Sub
    Private Sub TablaUsuarios_registro() Handles TablaUsuarios.Registro
        Cargar_datos(Me.TablaUsuarios.Registro_columa(1))
    End Sub
    Private Sub guarda_nuevo()
        Dim sql As String = ""
        sql &= " INSERT INTO dbo.Usuarios"
        sql &= " 	("

        sql &= "         Id_Matricula,"
        sql &= "         Usuario,"
        sql &= "         ID_Empresa,"
        sql &= "         Area,"
        sql &= "         Telefono,"
        sql &= "         Extrencion,"
        sql &= "         Correo,"
        sql &= "         P_Master,"
        sql &= "         P_Acceso,"
        sql &= "         P_Unplus,"
        sql &= "         P_Reportes,"
        sql &= "         F_captura,"
        sql &= "         Movto,"
        sql &= "         P_Impuestos,"
        sql &= "         P_Anual,"
        sql &= "         P_Depreciaciones,"
        sql &= "         P_Auditor,"
        sql &= "         P_Importar,"
        sql &= "         P_Parametros"
        sql &= " 	)"
        sql &= "         VALUES "
        sql &= " 	("
        sql &= " 	" & Me.lstMatricula.SelectItem & "," '@id_matricula
        sql &= " 	'" & Me.lstUsuarios.SelectText & "'," '@usuario
        sql &= " 	" & Me.lstEmpresa.SelectItem & "," '@id_empresa
        sql &= " 	'" & Me.lstArea.SelectText & "'," '@area
        sql &= " 	'" & Me.LstTelefono.SelectText & "'," '@telefono
        sql &= " 	'" & Me.lstExtencion.SelectText & "'," '@extrencion
        sql &= " 	'" & Me.LstCorreo.SelectText & "'," '@correo
        sql &= " 	'" & Me.lstMaster.SelectItem & "'," '@p_master
        sql &= " 	'" & Me.LstAcceso.SelectItem & "'," '@p_acceso
        sql &= " 	'" & Me.lstTotal.SelectItem & "'," '@p_master
        sql &= " 	'" & Me.LstReportes.SelectItem & "'," '@p_acceso
        sql &= " 	" & Eventos.Sql_hoy() & "," '@f_captura
        sql &= " 	'A'," '@movto
        sql &= " 	'" & Me.LstImps.SelectItem & "'," '@correo
        sql &= " 	'" & Me.LstAnual.SelectItem & "'," '@p_master
        sql &= " 	'" & Me.LstDep.SelectItem & "'," '@p_acceso
        sql &= " 	'" & Me.LstAuditoria.SelectItem & "'," '@p_master
        sql &= " 	'" & Me.LstImportar.SelectItem & "'," '@p_acceso
        sql &= " 	'" & Me.LstParametros.SelectItem & "'" '@p_master
        sql &= " 	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Usuarios_N", sql)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
    End Sub
    Private Sub guarda_edicion(ByVal id_usuario As Integer)
        Dim sql As String = ""
        sql &= "         Update dbo.Usuarios "
        sql &= " SET "

        sql &= " 	Usuario = '" & Me.lstUsuarios.SelectText & "'," '@usuario
        sql &= " 	ID_Empresa = " & Me.lstEmpresa.SelectItem & "," '@id_empresa
        sql &= " 	Area = '" & Me.lstArea.SelectText & "'," '@area
        sql &= " 	Telefono = '" & Me.LstTelefono.SelectText & "'," '@telefono
        sql &= " 	Extrencion = '" & Me.lstExtencion.SelectText & "'," '@extrencion
        sql &= " 	Correo = '" & Me.LstCorreo.SelectText & "'," '@correo
        sql &= " 	P_Master = '" & Me.lstMaster.SelectItem & "'," '@p_master
        sql &= " 	P_Acceso = '" & Me.LstAcceso.SelectItem & "' ," '@p_acceso
        sql &= " 	P_Unplus = '" & Me.lstTotal.SelectItem & "'," '@p_master
        sql &= " 	P_Reportes = '" & Me.LstReportes.SelectItem & "' ," '@p_acceso
        sql &= " 	P_Impuestos = '" & Me.LstImps.SelectItem & "'," '@p_master
        sql &= " 	P_Anual = '" & Me.LstAnual.SelectItem & "' ," '@p_acceso
        sql &= " 	P_Depreciaciones = '" & Me.LstDep.SelectItem & "'," '@p_master
        sql &= " 	P_Parametros = '" & Me.LstParametros.SelectItem & "' ," '@p_acceso
        sql &= " 	P_Auditor = '" & Me.LstAuditoria.SelectItem & "'," '@p_master
        sql &= " 	P_Importar = '" & Me.LstImportar.SelectItem & "' ," '@p_acceso
        sql &= " 	F_captura = " & Eventos.Sql_hoy() & "," '@f_captura
        sql &= " 	Movto = 'E' where id_matricula =" & id_usuario & "" '@movto
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Usuarios_E", sql)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
    End Sub
    Private Sub eliminar()
        If RadMessageBox.Show("Realmente deseas eliminar Al usuario: " & Me.lstUsuarios.SelectText & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim sql As String = "Delete from usuarios where id_matricula = " & Me.lstUsuarios.SelectItem & ""
            If Eventos.Comando_sql(sql) > 0 Then
                Eventos.Insertar_usuariol("Usuarios_D", sql)
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
    Private Sub ventanas(ByVal nuevo As Boolean, ByVal edit As Boolean, ByVal load As Boolean)
        If nuevo = True Then
            Me.TablaUsuarios.CmdEliminar.Enabled = False
            Me.TablaUsuarios.CmdGuardar.Enabled = True
            Me.TablaUsuarios.CmdNuevo.Enabled = False
        ElseIf edit = True Then
            Me.TablaUsuarios.CmdEliminar.Enabled = True
            Me.TablaUsuarios.CmdGuardar.Enabled = True
            Me.TablaUsuarios.CmdNuevo.Enabled = True
        ElseIf load = True Then
            Me.TablaUsuarios.CmdEliminar.Enabled = False
            Me.TablaUsuarios.CmdGuardar.Enabled = False
            Me.TablaUsuarios.CmdNuevo.Enabled = True
        End If
    End Sub
    Private Sub Cargar_datos(ByVal id_Usuario As Integer)
        Nuevo = False
        ventanas(False, True, False)
        Dim consulta As String = "SELECT Usuarios.ID_Usuario, Usuarios.Id_Matricula, Usuarios.Usuario, "
        consulta &= " Usuarios.Contraseña, Empresa.Razon_social, Usuarios.Area, Usuarios.Telefono,"
        consulta &= " Usuarios.Extrencion, Usuarios.Correo,Usuarios.P_Master,Usuarios.P_Auditor,Usuarios.P_Importar, Usuarios.P_Acceso, Usuarios.P_Unplus, "
        consulta &= " Usuarios.P_Reportes, Usuarios.F_captura, Usuarios.Movto, Usuarios.P_Impuestos, 
        Usuarios.P_Anual, Usuarios.P_Depreciaciones, Usuarios.P_Parametros, "
        consulta &= "  Personal.Nombres + ' ' + Personal.Ap_paterno + ' ' + Personal.Ap_materno as Nombre,Empresa.Id_Empresa FROM     Usuarios INNER JOIN Empresa ON Usuarios.ID_Empresa = Empresa.ID_empresa INNER JOIN personal ON Personal.Id_Matricula = Usuarios.Id_Matricula where Usuarios.id_matricula= " & Val(id_Usuario) & " "
        Dim ds As DataSet = Obtener_DS(consulta)
        If ds.Tables(0).Rows.Count > 0 Then
            'Me.lstUsuarios.SelectText = Trim(ds.Tables(0).Rows(0)("Usuario"))
            Me.lstUsuarios.SelectItem = ds.Tables(0).Rows(0)("Id_Matricula")
            Activo = True
            Me.lstMatricula.SelectText = Trim(ds.Tables(0).Rows(0)("Nombre"))
            Me.lstEmpresa.SelectItem = ds.Tables(0).Rows(0)("Id_Empresa")

            Me.lstArea.SelectText = ds.Tables(0).Rows(0)("Area")
            Me.LstTelefono.SelectText = ds.Tables(0).Rows(0)("Telefono")
            Me.lstExtencion.SelectText = ds.Tables(0).Rows(0)("Extrencion")
            Me.LstCorreo.SelectText = ds.Tables(0).Rows(0)("Correo")
            Me.lstMaster.SelectText = IIf(Trim(ds.Tables(0).Rows(0)("P_Master")) = "M", "Modificación", "Nada")
            Me.LstAcceso.SelectText = IIf(Trim(ds.Tables(0).Rows(0)("P_Acceso")) = "M", "Modificación", "Nada")
            Me.lstTotal.SelectText = IIf(Trim(ds.Tables(0).Rows(0)("P_Unplus")) = "M", "Modificación", "Nada")
            Me.LstImps.SelectText = IIf(Trim(ds.Tables(0).Rows(0)("P_Impuestos")) = "M", "Modificación", "Nada")
            Me.LstAnual.SelectText = IIf(Trim(ds.Tables(0).Rows(0)("P_Anual")) = "M", "Modificación", "Nada")
            Me.LstDep.SelectText = IIf(Trim(ds.Tables(0).Rows(0)("P_Depreciaciones")) = "M", "Modificación", "Nada")
            Me.LstParametros.SelectText = IIf(Trim(ds.Tables(0).Rows(0)("P_Parametros")) = "M", "Modificación", "Nada")
            Me.LstReportes.SelectText = IIf(Trim(ds.Tables(0).Rows(0)("P_Reportes")) = "M", "Modificación", "Nada")
            Me.LstAuditoria.SelectText = IIf(Trim(ds.Tables(0).Rows(0)("P_Auditor")) = "M", "Modificación", "Nada")
            Me.LstImportar.SelectText = IIf(Trim(ds.Tables(0).Rows(0)("P_Importar")) = "M", "Modificación", "Nada")
            Activo = False
        Else
            Activo = True
            Limpiar()
            Activo = False
            Exit Sub
        End If
    End Sub

    Private Sub lstMatricula_cambio_item(value As String, texto As String) Handles lstMatricula.Cambio_item
        If Activo = False Then
            Dim sql As String = "Select id_matricula from Usuarios where id_matricula= " & value & ""
            Dim ds As DataSet = Eventos.Obtener_DS(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Cargar_datos(ds.Tables(0).Rows(0)("id_matricula"))
            Else
                Exit Sub
            End If
        End If
    End Sub

    Private Sub lstUsuarios_cambio_item(value As String, texto As String) Handles lstUsuarios.Cambio_item

    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Control de Usuarios")
    'End Sub


End Class