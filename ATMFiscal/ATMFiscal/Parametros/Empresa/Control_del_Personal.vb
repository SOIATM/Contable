Imports Telerik.WinControls
Public Class Control_del_Personal
    Dim nuevo As Boolean
    Dim activo As Boolean
    Private Sub Control_del_Personal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        Cargar_info()
        Cargar_listas()
        limpiar()
        ventanas(False, False, True)
        activo = False
    End Sub
    Private Sub Cargar_listas()
        Me.lstMatricula.Cargar(" select DISTINCT Id_matricula,Id_matricula FROM Personal")
        Me.LstCp.Cargar(" select DISTINCT Codigo_postal,Codigo_postal FROM Personal")
        Me.LstEmpresa.Cargar(" SELECT id_Empresa, Razon_social FROM Empresa ")
        Me.lstNombre.Cargar(" SELECT Id_matricula , Nombres + ' '+ Ap_paterno + ' ' + Ap_Materno FROM Personal ")
        Me.LstPuesto.Cargar(" SELECT DISTINCT Puesto,Puesto FROM Personal")
        Me.lstSexo.Cargar(" SELECT 1, 'M' UNION SELECT 0, 'F' ")
    End Sub
    Private Sub Cargar_info()
        Dim consulta As String = "SELECT Personal.ID_matricula, Personal.Nombres, Personal.Ap_paterno, Personal.Ap_materno, Personal.Fecha_nacimto, "
        consulta &= " Personal.Reg_fed_causantes, Personal.Direccion, Personal.Codigo_postal, Personal.Puesto, Personal.Telefono,"
        consulta &= " Personal.No_IMSS, Personal.No_CURP, Personal.Fecha_ingreso, Personal.Fecha_alta_IMSS, Personal.Fecha_de_baja, "
        consulta &= " Personal.Sexo, Personal.Estado_civil, Personal.Fecha_baja_IMSS, Personal.Sueldo_promedio, "
        consulta &= " Personal.Fecha_captura, Personal.Movto,Empresa.Razon_social,Personal.Folio_cred_elector "
        consulta &= " FROM     Personal INNER JOIN Empresa ON Personal.ID_empresa = Empresa.ID_empresa"
        Dim sql As String = "select * from (" & consulta & ") as tabla"
        Dim sql2 As String = "(" & consulta & ") as tabla"
        Dim sql4 As String = "(" & consulta & " and datepart(year,Clientes.Fecha_captura) = " & Me.TablaPersonal.ComboAño.Text & " and datepart(month,Clientes.Fecha_captura) =" & Me.TablaPersonal.ComboMes.Text & " ) as tabla"
        Dim sql5 As String = "(" & consulta & " and datepart(year,Clientes.Fecha_captura) = " & Me.TablaPersonal.ComboAño.Text & "  ) as tabla"
        Dim sql3 As String = "(" & consulta & "  ) as tabla"
        If Me.TablaPersonal.ComboAño.Text = "*" Then
            Me.TablaPersonal.SqlSelect = sql3
        ElseIf Me.TablaPersonal.ComboAño.Text <> "*" And Me.TablaPersonal.ComboMes.Text <> "*" Then
            Me.TablaPersonal.SqlSelect = sql3 & " where datepart(year, Fecha_captura) = " & TablaPersonal.fecha_año & " and datepart(month, Fecha_captura) = " & Me.TablaPersonal.fecha_mes & " "
        ElseIf Me.TablaPersonal.ComboAño.Text <> "*" And Me.TablaPersonal.ComboMes.Text = "*" Then
            Me.TablaPersonal.SqlSelect = sql3 & " where datepart(year, Fecha_captura) = " & TablaPersonal.fecha_año & "  "
        End If
        Me.TablaPersonal.Ordenar(True, "ID_matricula")
        If Me.TablaPersonal.ComboAño.Text <> "*" And Me.TablaPersonal.ComboMes.Text = "*" Then
            Me.TablaPersonal.Tablaconsulta(sql2, sql5)
            Me.TablaPersonal.Cargar("P_Master", sql, , "Fecha_captura", " where datepart(year, Fecha_captura) = " & Me.TablaPersonal.ComboAño.Text & "")
        Else
            Me.TablaPersonal.Tablaconsulta(sql2, sql4)
            Me.TablaPersonal.Cargar("P_Master", sql, , "Fecha_captura", " where datepart(year, Fecha_captura) = " & Me.TablaPersonal.ComboAño.Text & " and datepart(month, Fecha_captura) = " & Me.TablaPersonal.ComboMes.Text & "")
        End If
        Me.TablaPersonal.Ordenar("ID_matricula")
        Me.TablaPersonal.Tabla.Columns(0).Visible = False
    End Sub
    Private Sub limpiar()
        Me.TxtApMaterno.Text = ""
        Me.TxtApPaterno.Text = ""
        Me.TxtCurp.Text = ""
        Me.txtDireccion.Text = ""
        Me.TxtIfe.Text = ""
        Me.TxtImss.Text = ""
        Me.TxtNombre.Text = ""
        Me.TxtSueldo.Text = ""
        Me.TxtTelefono.Text = ""
        Me.TxtRfc.Text = ""
        Me.lstMatricula.SelectText = ""
        Me.LstCp.SelectText = ""
        Me.LstEmpresa.SelectText = ""
        Me.lstNombre.SelectText = ""
        Me.LstPuesto.SelectText = ""
        Me.lstSexo.SelectText = ""
        Me.dtFechaBaja.Clear()
        Me.DTFechaNacimiento.Clear()
        Me.DtAltaIMSS.Clear()
        Me.DtFechaBajaImss.Clear()
        Me.DtIngreso.Clear()
        Me.lstEdoCivil.SelectText = ""
    End Sub
    Private Sub ventanas(ByVal nuevo As Boolean, ByVal edit As Boolean, ByVal load As Boolean)
        If nuevo = True Then
            Me.TablaPersonal.CmdEliminar.Enabled = False
            Me.TablaPersonal.CmdGuardar.Enabled = True
            Me.TablaPersonal.CmdNuevo.Enabled = False
        ElseIf edit = True Then
            Me.TablaPersonal.CmdEliminar.Enabled = True
            Me.TablaPersonal.CmdGuardar.Enabled = True
            Me.TablaPersonal.CmdNuevo.Enabled = True
        ElseIf load = True Then
            Me.TablaPersonal.CmdEliminar.Enabled = False
            Me.TablaPersonal.CmdGuardar.Enabled = False
            Me.TablaPersonal.CmdNuevo.Enabled = True
        End If
    End Sub

    Private Sub TablaPersonal_cerrar() Handles TablaPersonal.Cerrar
        Me.Close()
    End Sub

    Private Sub TablaPersonal_eliminar() Handles TablaPersonal.Eliminar
        Elimina_persona()
        Me.TablaPersonal.CmdActualizar.PerformClick()
    End Sub
    Private Sub Elimina_persona()
        If Me.lstMatricula.SelectItem <> "" Then
            If RadMessageBox.Show("Realemnte deseas eliminar el registro con matricula: " & Me.lstMatricula.SelectItem & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Eventos.Comando_sql("Delete from personal where id_matricula=" & Me.lstMatricula.SelectItem) = 1 Then
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    Dim Ms As DialogResult = RadMessageBox.Show(Me, "Registro eliminado corectamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                    Me.Text = Ms.ToString()

                    limpiar()
                Else
                    RadMessageBox.Show("El dato no fue eliminado, verifique la información proporcionada.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                End If
            End If
        End If
    End Sub

    Private Sub TablaPersonal_guardar() Handles TablaPersonal.Guardar
        If nuevo = True Then
            guardar()
        Else
            editar(Me.lstMatricula.SelectItem)
        End If
        Me.TablaPersonal.CmdActualizar.PerformClick()
    End Sub
    Private Sub guardar()
        Dim sql As String = "INSERT INTO dbo.Personal"
        sql &= " 	(                           "
        sql &= " 	ID_matricula,      "
        sql &= "     Ap_paterno,        "
        sql &= "     Ap_materno,        "
        sql &= "     Nombres,           "
        sql &= "     Fecha_nacimto,     "
        sql &= "     Reg_fed_causantes, "
        sql &= "     Direccion,         "
        sql &= "     Codigo_postal,     "
        sql &= "     Puesto,            "
        sql &= "     Telefono,          "
        sql &= "     No_IMSS,           "
        sql &= "     No_CURP,           "
        sql &= "     Fecha_ingreso,     "
        sql &= "     Fecha_alta_IMSS,   "

        sql &= "     Sexo,              "
        sql &= "     Estado_civil,      "

        sql &= "     ID_empresa,        "
        sql &= "     Sueldo_promedio,   "
        sql &= "     Fecha_captura,     "
        sql &= "     Movto,             "
        sql &= "     Folio_cred_elector "
        sql &= " 	)                  "
        sql &= " VALUES                 "
        sql &= " 	( "
        sql &= " " & Me.lstMatricula.SelectText & ",	" '@id_matricula,     
        sql &= " '" & Me.TxtApPaterno.Text & "',	" '@ap_paterno,       
        sql &= " '" & Me.TxtApMaterno.Text & "',	" '@ap_materno,       
        sql &= " 	'" & Me.TxtNombre.Text & "'," '@nombres,          
        sql &= " " & Eventos.Sql_hoy(Me.DTFechaNacimiento.Text) & ",	" '@fecha_nacimto,    
        sql &= " 	'" & Me.TxtRfc.Text & "'," '@reg_fed_causantes,
        sql &= " 	'" & Me.txtDireccion.Text & "'," '@direccion,        
        sql &= " '" & Me.LstCp.SelectText & "'	," '@codigo_postal,    
        sql &= " '" & Me.LstPuesto.SelectText & "'	," '@puesto,           
        sql &= " '" & Me.TxtTelefono.Text & "'	," '@telefono,         
        sql &= " '" & Me.TxtImss.Text & "'	," '@no_imss,          
        sql &= " 	'" & Me.TxtCurp.Text & "'," '@no_curp,          
        sql &= " " & Eventos.Sql_hoy(Me.DtIngreso.Text) & "	," '@fecha_ingreso,    
        sql &= " " & Eventos.Sql_hoy(Me.DtAltaIMSS.Text) & ",	" '@fecha_alta_imss,  

        sql &= " 	'" & Me.lstSexo.SelectText & "'," '@sexo,             
        sql &= " 	'" & Me.lstEdoCivil.SelectText & "'," '@estado_civil,     

        sql &= " 	" & Me.LstEmpresa.SelectItem & "," '@id_empresa,       
        sql &= " 	" & Me.TxtSueldo.Text & "," '@sueldo_promedio,  
        sql &= " " & Eventos.Sql_hoy() & ",	" '@fecha_captura,    
        sql &= " 	'A'," '@movto,            
        sql &= " '" & Me.TxtIfe.Text & "'	" '@folio_cred_elector
        sql &= " 	) "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Personal_I", sql)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
    End Sub
    Private Sub editar(ByVal mat As Integer)
        If Me.lstMatricula.SelectText <> "" Then
            Dim sql As String = ""
            sql &= "             Update dbo.Personal"
            sql &= " SET "
            sql &= " 	Ap_paterno ='" & Me.TxtApPaterno.Text & "' ," '@ap_paterno
            sql &= " 	Ap_materno = '" & Me.TxtApMaterno.Text & "'," '@ap_materno
            sql &= " 	Nombres = '" & Me.TxtNombre.Text & "', " '@nombres
            sql &= " 	Fecha_nacimto = " & Eventos.Sql_hoy(Me.DTFechaNacimiento.Text) & "," '@fecha_nacimto
            sql &= " 	Reg_fed_causantes = '" & Me.TxtRfc.Text & "', " '@reg_fed_causantes
            sql &= " 	Direccion = '" & Me.txtDireccion.Text & "'," '@direccion
            sql &= " 	Codigo_postal = '" & Me.LstCp.SelectText & "'," '@codigo_postal
            sql &= " 	Puesto = '" & Me.LstPuesto.SelectText & "' ," '@puesto
            sql &= " 	Telefono = '" & Me.TxtTelefono.Text & "'," '@telefono
            sql &= " 	No_IMSS = '" & Me.TxtImss.Text & "'	," '@no_imss
            sql &= " 	No_CURP = '" & Me.TxtCurp.Text & "'," '@no_curp
            sql &= " 	Fecha_ingreso = " & Eventos.Sql_hoy(Me.DtIngreso.Text) & "," '@fecha_ingreso
            sql &= " 	Fecha_alta_IMSS = " & Eventos.Sql_hoy(Me.DtAltaIMSS.Text) & "," '@fecha_alta_imss
            sql &= " 	Fecha_de_baja = " & Eventos.Sql_hoy(Me.dtFechaBaja.Text) & "," '@fecha_de_baja
            sql &= " 	Sexo = '" & Me.lstSexo.SelectText & "'," '@sexo
            sql &= " 	Estado_civil ='" & Me.lstEdoCivil.SelectText & "' ," '@estado_civil
            sql &= " 	Fecha_baja_IMSS =  " & Eventos.Sql_hoy(Me.DtFechaBajaImss.Text) & " ," '@fecha_baja_imss
            sql &= " 	ID_empresa = " & Me.LstEmpresa.SelectItem & "," '@id_empresa
            sql &= " 	Sueldo_promedio =	" & Me.TxtSueldo.Text & " ," '@sueldo_promedio
            sql &= " 	Fecha_captura = " & Eventos.Sql_hoy() & "," '@fecha_captura
            sql &= " 	Movto = 'M'," '@movto
            sql &= " 	Folio_cred_elector = '" & Me.TxtIfe.Text & "' WHERE Id_Matricula = " & mat & "" '@folio_cred_elector
            If Eventos.Comando_sql(sql) > 0 Then
                Eventos.Insertar_usuariol("Personal_E", sql)
            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Me.Text = Ms.ToString()
                Exit Sub
            End If
        Else

        End If
    End Sub
    Private Sub TablaPersonal_nuevo() Handles TablaPersonal.Nuevo
        nuevo = True
        activo = True
        limpiar()
        ventanas(True, False, False)
        activo = False
    End Sub
    Private Sub TablaPersonal_refrescar() Handles TablaPersonal.Refrescar
        activo = True
        Cargar_info()
        Cargar_listas()
        limpiar()
        ventanas(False, False, True)
        activo = False
    End Sub
    Private Sub TablaPersonal_registro() Handles TablaPersonal.Registro
        carga_datos_Persona(Me.TablaPersonal.Registro_columa(0))
    End Sub
    Private Sub carga_datos_Persona(ByVal id_matricula As Integer)
        nuevo = False
        ventanas(False, True, False)
        Dim consulta As String = "SELECT Personal.ID_matricula, Personal.Ap_paterno, Personal.Ap_materno,  Personal.Nombres,Personal.Fecha_nacimto, "
        consulta &= " Personal.Reg_fed_causantes, Personal.Direccion, Personal.Codigo_postal, Personal.Puesto, Personal.Telefono,"
        consulta &= " Personal.No_IMSS, Personal.No_CURP, Personal.Fecha_ingreso, Personal.Fecha_alta_IMSS, Personal.Fecha_de_baja, "
        consulta &= " Personal.Sexo, Personal.Estado_civil, Personal.Fecha_baja_IMSS, Personal.Sueldo_promedio, "
        consulta &= " Personal.Fecha_captura, Personal.Movto,Empresa.Razon_social, Personal.Folio_cred_elector "
        consulta &= " FROM     Personal INNER JOIN Empresa ON Personal.ID_empresa = Empresa.ID_empresa  where personal.id_matricula= " & Val(id_matricula) & " "
        Dim ds As DataSet = Obtener_DS(consulta)
        If ds.Tables(0).Rows.Count > 0 Then
            activo = True
            Me.lstMatricula.SelectText = ds.Tables(0).Rows(0)("ID_matricula")
            Me.LstEmpresa.SelectText = Eventos.Ds_valor(ds, 0, 21)
            Me.TxtApPaterno.Text = ds.Tables(0).Rows(0)("Ap_Paterno")
            Me.TxtApMaterno.Text = ds.Tables(0).Rows(0)("Ap_Materno")
            Me.TxtNombre.Text = ds.Tables(0).Rows(0)("Nombres")
            Me.DTFechaNacimiento.Text = Eventos.Ds_valor(ds, 0, 4)
            Me.TxtRfc.Text = ds.Tables(0).Rows(0)("Reg_fed_causantes")
            Me.txtDireccion.Text = ds.Tables(0).Rows(0)("Direccion")
            Me.LstCp.SelectText = ds.Tables(0).Rows(0)("Codigo_postal")
            Me.LstPuesto.SelectText = ds.Tables(0).Rows(0)("Puesto")
            Me.TxtTelefono.Text = ds.Tables(0).Rows(0)("Telefono")
            Me.TxtImss.Text = ds.Tables(0).Rows(0)("No_IMSS")
            Me.TxtCurp.Text = ds.Tables(0).Rows(0)("No_CURP")
            Me.DtIngreso.Text = Eventos.Ds_valor(ds, 0, 12)
            Me.DtAltaIMSS.Text = Eventos.Ds_valor(ds, 0, 13)
            Me.dtFechaBaja.Text = Eventos.Ds_valor(ds, 0, 14)
            Me.lstSexo.SelectText = ds.Tables(0).Rows(0)("Sexo")
            Me.lstEdoCivil.SelectText = ds.Tables(0).Rows(0)("Estado_civil")
            Me.DtFechaBajaImss.Text = Eventos.Ds_valor(ds, 0, 17)
            Me.TxtSueldo.Text = ds.Tables(0).Rows(0)("Sueldo_promedio")
            Me.TxtIfe.Text = ds.Tables(0).Rows(0)("Folio_cred_elector")
            Me.lstNombre.SelectText = ds.Tables(0).Rows(0)("Nombres") & " " & ds.Tables(0).Rows(0)("Ap_Paterno") & " " & ds.Tables(0).Rows(0)("Ap_Materno")
            activo = False
        Else
            activo = True
            limpiar()
            activo = False
            Exit Sub
        End If
    End Sub

    Private Sub lstMatricula_cambio_item(value As String, texto As String) Handles lstMatricula.Cambio_item
        If activo = False Then
            carga_datos_Persona(value)
        End If
    End Sub

    Private Sub lstNombre_cambio_item(value As String, texto As String) Handles lstNombre.Cambio_item
        If activo = False Then
            carga_datos_Persona(value)
        End If
    End Sub
End Class
