Imports Telerik.WinControls
Public Class Control_de_Empresas
    Dim activo As Boolean
    Dim nuevo As Boolean
    Private Sub Control_de_Empresas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        Cargar_Info()
        Cargar_listas()
        Limpiar()
        Ventanas(False, False, True)
        activo = False
    End Sub
    Private Sub Cargar_Info()
        Dim consulta As String = "SELECT Empresa.ID_empresa, Empresa.Razon_social, Empresa.Direccion, Empresa.Colonia, "
        consulta &= "         Empresa.Reg_fed_causantes, Empresa.Codigo_postal, Empresa.Giro, Empresa.Telefono1,"
        consulta &= "         Empresa.Telefono2, Empresa.Telefono3, Empresa.Fax, Empresa.Correo_electronico,"
        consulta &= "         Empresa.Fecha_captura, Empresa.Movto"
        consulta &= " FROM     Empresa"
        Dim sql As String = "select * from (" & consulta & ") as tabla"
        Dim sql2 As String = "(" & consulta & ") as tabla"
        Dim sql4 As String = "(" & consulta & " and datepart(year,Empresa.Fecha_captura) = " & Me.TablaEmpresas.ComboAño.Text & " and datepart(month,Empresa.Fecha_captura) =" & Me.TablaEmpresas.ComboMes.Text & " ) as tabla"
        Dim sql5 As String = "(" & consulta & " and datepart(year,Empresa.Fecha_captura) = " & Me.TablaEmpresas.ComboAño.Text & "  ) as tabla"
        Dim sql3 As String = "(" & consulta & "  ) as tabla"
        If Me.TablaEmpresas.ComboAño.Text = "*" Then
            Me.TablaEmpresas.SqlSelect = sql3
        ElseIf Me.TablaEmpresas.ComboAño.Text <> "*" And Me.TablaEmpresas.ComboMes.Text <> "*" Then
            Me.TablaEmpresas.SqlSelect = sql3 & " where datepart(year, Fecha_captura) = " & TablaEmpresas.fecha_año & " and datepart(month, Fecha_captura) = " & TablaEmpresas.fecha_mes & " "
        ElseIf Me.TablaEmpresas.ComboAño.Text <> "*" And Me.TablaEmpresas.ComboMes.Text = "*" Then
            Me.TablaEmpresas.SqlSelect = sql3 & " where datepart(year, Fecha_captura) = " & TablaEmpresas.fecha_año & "  "
        End If
        Me.TablaEmpresas.Ordenar(True, "ID_empresa")
        If Me.TablaEmpresas.ComboAño.Text <> "*" And Me.TablaEmpresas.ComboMes.Text = "*" Then
            Me.TablaEmpresas.Tablaconsulta(sql2, sql5)
            Me.TablaEmpresas.Cargar("P_Master", sql, , "Fecha_captura", " where datepart(year, Fecha_captura) = " & TablaEmpresas.ComboAño.Text & "")
        Else
            Me.TablaEmpresas.Tablaconsulta(sql2, sql4)
            Me.TablaEmpresas.Cargar("P_Master", sql, , "Fecha_captura", " where datepart(year, Fecha_captura) = " & TablaEmpresas.ComboAño.Text & " and datepart(month, Fecha_captura) = " & TablaEmpresas.ComboMes.Text & "")
        End If
        Me.TablaEmpresas.Ordenar("ID_empresa")
    End Sub
    Private Sub Limpiar()
        Me.LstCodigoPostal.SelectText = ""
        Me.LstCodigoPostal.SelectText = ""
        Me.lstEmpresa.SelectText = ""
        Me.LstGiro.SelectText = ""
        Me.lstRazonSocial.SelectText = ""
        Me.lstRfc.SelectText = ""
        Me.txtCorreo.Text = ""
        Me.TxtDireccion.Text = ""
        Me.txtTelefono.Text = ""
        Me.txtTelefono2.Text = ""
        Me.TxtTelefono3.Text = ""

    End Sub
    Private Sub Cargar_listas()
        Me.lstEmpresa.Cargar("select Id_empresa ,razon_social from Empresa")
        Me.LstGiro.Cargar("select giro,giro from Empresa ")
        Me.lstRazonSocial.Cargar("Select id_empresa,Razon_social from Empresa")
        Me.lstRfc.Cargar("Select id_empresa,Reg_fed_causantes from Empresa")
    End Sub
    Private Sub Ventanas(ByVal nuevo As Boolean, ByVal edit As Boolean, ByVal load As Boolean)
        If nuevo = True Then
            Me.TablaEmpresas.CmdEliminar.Enabled = False
            Me.TablaEmpresas.CmdGuardar.Enabled = True
            Me.TablaEmpresas.CmdNuevo.Enabled = False
        ElseIf edit = True Then
            Me.TablaEmpresas.CmdEliminar.Enabled = True
            Me.TablaEmpresas.CmdGuardar.Enabled = True
            Me.TablaEmpresas.CmdNuevo.Enabled = True
        ElseIf load = True Then
            Me.TablaEmpresas.CmdEliminar.Enabled = False
            Me.TablaEmpresas.CmdGuardar.Enabled = False
            Me.TablaEmpresas.CmdNuevo.Enabled = True
        End If
    End Sub

    Private Sub TablaEmpresas_cerrar() Handles TablaEmpresas.Cerrar
        Me.Close()
    End Sub

    Private Sub TablaEmpresas_eliminar() Handles TablaEmpresas.Eliminar
        eliminar()
        Me.TablaEmpresas.CmdActualizar.PerformClick()
    End Sub
    Private Sub Eliminar()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.lstEmpresa.SelectItem <> "" Then
            If RadMessageBox.Show("Realemnte deseas eliminar el registro de la Empresa: " & Me.lstEmpresa.SelectText & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Eventos.Comando_sql("Delete from Empresa where Id_empresa=" & Me.lstEmpresa.SelectItem) = 1 Then

                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    Dim Ms As DialogResult = RadMessageBox.Show(Me, "Registro eliminado corectamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                    Me.Text = Ms.ToString()

                    Limpiar()
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    Dim Ms As DialogResult = RadMessageBox.Show(Me, "El dato no fue eliminado, verifique la información proporcionada.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                    Me.Text = Ms.ToString()

                End If
            End If
        End If
    End Sub

    Private Sub TablaEmpresas_guardar() Handles TablaEmpresas.Guardar
        If nuevo = True Then
            guargar_nuevo()
        Else
            editar(Me.lstEmpresa.SelectItem)
        End If
        Me.TablaEmpresas.CmdActualizar.PerformClick()
    End Sub
    Private Sub Guargar_nuevo()
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Empresa"
        sql &= "     Razon_social,       "
        sql &= "     Reg_fed_causantes, "
        sql &= "     Direccion,          "
        ' sql &= "     Colonia,           "
        sql &= "     Codigo_postal,      "
        sql &= "     Giro,              "
        sql &= "     Telefono1,          "
        sql &= "     Telefono2,         "
        sql &= "     Telefono3,          "
        'sql &= "     Fax,               "
        sql &= "     Correo_electronico, "
        sql &= "     Fecha_captura,     "
        sql &= "     Movto               "
        sql &= " 	)                   "
        sql &= " VALUES                  "
        sql &= " 	(                   "

        sql &= " '" & Me.lstRazonSocial.SelectText & "'	," '@razon_social,      
        sql &= " 	'" & Me.lstRfc.SelectText & "'," '@reg_fed_causantes, 
        sql &= " 	'" & Me.TxtDireccion.Text & "'," '@direccion,         
        '  sql &= " 	'" & Me.TxtColonia.Text & "'," '@colonia,           
        sql &= " 	'" & Me.LstCodigoPostal.SelectText & "'," '@codigo_postal,     
        sql &= " '" & Me.LstGiro.SelectText & "'	," '@giro,              
        sql &= " 	'" & Me.txtTelefono.Text & "'," '@telefono1,         
        sql &= " 	'" & Me.txtTelefono2.Text & "'," '@telefono2,         
        sql &= " 	'" & Me.TxtTelefono3.Text & "'," '@telefono3,         
        '  sql &= " 	'" & Me.txtFax.Text & "'," '@fax,               
        sql &= " 	'" & Me.txtCorreo.Text & "'," '@correo_electronico,
        sql &= " 	" & Eventos.Sql_hoy() & "," '@fecha_captura,     
        sql &= " 'A'	" '@movto              
        sql &= " 	)"
        If Eventos.Comando_sql(sql) > 0 Then

        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
    End Sub
    Private Sub Editar(ByVal id_empresa As Integer)
        Dim sql As String = "UPDATE dbo.Empresa  "
        sql &= " SET Razon_social = '" & Me.lstRazonSocial.SelectText & "', "
        sql &= " Reg_fed_causantes = '" & Me.lstRfc.SelectText & "',"
        sql &= " Direccion = '" & Me.TxtDireccion.Text & "', "
        sql &= " Codigo_postal ='" & Me.LstCodigoPostal.SelectText & "',"
        sql &= " Giro = '" & Me.LstGiro.SelectText & "', "
        sql &= " Telefono1 = '" & Me.txtTelefono.Text & "', "
        sql &= " Telefono2 = '" & Me.txtTelefono2.Text & "', "
        sql &= " Telefono3 = '" & Me.TxtTelefono3.Text & "',"
        sql &= " Correo_electronico = '" & Me.txtCorreo.Text & "', "
        sql &= " Fecha_captura = " & Eventos.Sql_hoy() & ",            "
        sql &= " Movto = 'M' Where id_empresa= " & Me.lstEmpresa.SelectItem & ""
        If Eventos.Comando_sql(sql) > 0 Then

        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()
            Exit Sub
        End If

    End Sub

    Private Sub TablaEmpresas_nuevo() Handles TablaEmpresas.Nuevo
        nuevo = True
        activo = True
        limpiar()
        ventanas(True, False, False)
        activo = False
    End Sub

    Private Sub TablaEmpresas_refrescar() Handles TablaEmpresas.Refrescar
        activo = True
        cargar_Info()
        cargar_listas()
        limpiar()
        ventanas(False, False, True)
        activo = False
    End Sub


    Private Sub Cargar_datos(ByVal id_empresa As Integer)
        Dim consulta As String = "SELECT Empresa.ID_empresa, Empresa.Razon_social, Empresa.Direccion, Empresa.Colonia, "
        consulta &= "         Empresa.Reg_fed_causantes, Empresa.Codigo_postal, Empresa.Giro, Empresa.Telefono1,"
        consulta &= "         Empresa.Telefono1, Empresa.Telefono3, Empresa.Fax, Empresa.Correo_electronico,"
        consulta &= "         Empresa.Fecha_captura, Empresa.Movto"
        consulta &= " FROM     Empresa where id_empresa = " & id_empresa & ""
        Dim ds As DataSet = Obtener_DS(consulta)
        If ds.Tables(0).Rows.Count > 0 Then
            activo = True
            Me.lstEmpresa.SelectItem = id_empresa
            Me.lstEmpresa.SelectText = ds.Tables(0).Rows(0)("Razon_social")
            Me.lstRazonSocial.SelectText = ds.Tables(0).Rows(0)("Razon_social")
            Me.lstRfc.SelectText = ds.Tables(0).Rows(0)("Reg_fed_causantes")
            Me.TxtDireccion.Text = ds.Tables(0).Rows(0)("Direccion")
            Me.LstCodigoPostal.SelectText = ds.Tables(0).Rows(0)("Codigo_postal")
            Me.LstGiro.SelectText = ds.Tables(0).Rows(0)("Giro")
            Me.txtTelefono.Text = ds.Tables(0).Rows(0)("Telefono1")
            Me.txtTelefono2.Text = ds.Tables(0).Rows(0)("Telefono1")
            Me.TxtTelefono3.Text = ds.Tables(0).Rows(0)("Telefono3")
            Me.txtCorreo.Text = ds.Tables(0).Rows(0)("Correo_electronico")
            activo = False
        Else
            activo = True
            Limpiar()
            activo = False
            Exit Sub
        End If
    End Sub

    Private Sub TablaEmpresas_registro() Handles TablaEmpresas.Registro
        cargar_datos(Me.TablaEmpresas.Registro_columa(0))
        ventanas(False, True, False)
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    Eventos.Abrir_Manual("Control del Empresas")
    'End Sub
    Private Sub lstEmpresa_cambio_item(value As String, texto As String) Handles lstEmpresa.Cambio_item
        If activo = False Then
            cargar_datos(value)
            ventanas(False, True, False)
        End If
    End Sub
End Class
