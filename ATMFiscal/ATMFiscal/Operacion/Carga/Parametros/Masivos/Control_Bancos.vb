
Imports Telerik.WinControls
Public Class Control_Bancos
    Dim activo As Boolean
    Dim nuevo As Boolean
    Private Sub Control_Bancos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        Me.Tabla_detalleBancos.SqlSelect = " SELECT * FROM Bancos "
        Me.Tabla_detalleBancos.Cargar()
        Me.Tabla_detalleBancos.Tabla.Columns(0).Visible = False
        Cargar_Info()
        Cargar_listas()
        Limpiar()

        Ventanas(False, False, True)
        activo = False
    End Sub
    Private Sub Cargar_Info()
        Dim consulta As String = "SELECT Bancos_Clientes.Id_Bancos_Clientes, Bancos_Clientes.Id_Banco, Bancos_Clientes.Id_cat_Cuentas, Bancos_Clientes.Id_Empresa, Empresa.Razon_Social, Bancos_Clientes.Alias, Bancos.Nombre, Bancos.Clave, Bancos_Clientes.No_Cuenta, 
                  Catalogo_de_Cuentas.Cuenta, Catalogo_de_Cuentas.Descripcion, Bancos_Clientes.Fecha_inicio, Bancos_Clientes.Fecha_fin
                  FROM     Bancos INNER JOIN Bancos_Clientes ON Bancos.Id_Banco = Bancos_Clientes.Id_Banco INNER JOIN
                  Catalogo_de_Cuentas ON Bancos_Clientes.Id_cat_Cuentas = Catalogo_de_Cuentas.Id_cat_Cuentas INNER JOIN
                  Empresa ON Empresa.Id_Empresa = Bancos_Clientes.Id_Empresa AND Catalogo_de_Cuentas.Id_Empresa = Empresa.Id_Empresa WHERE Empresa.Id_Empresa =" & Inicio.Clt & ""
        Dim sql As String = "select * from (" & consulta & ") as tabla"
        Dim sql2 As String = "(" & consulta & ") as tabla"
        Dim sql4 As String = "(" & consulta & " and datepart(year,Bancos_Clientes.Fecha_inicio) = " & Me.TablaBancos.ComboAño.Text & " and datepart(month,Bancos_Clientes.Fecha_inicio) =" & Me.TablaBancos.ComboMes.Text & " ) as tabla"
        Dim sql5 As String = "(" & consulta & " and datepart(year,Bancos_Clientes.Fecha_inicio) = " & Me.TablaBancos.ComboAño.Text & "  ) as tabla"
        Dim sql3 As String = "(" & consulta & "  ) as tabla"
        If Me.TablaBancos.ComboAño.Text = "*" Then
            Me.TablaBancos.SqlSelect = sql3
        ElseIf Me.TablaBancos.ComboAño.Text <> "*" And Me.TablaBancos.ComboMes.Text <> "*" Then
            Me.TablaBancos.SqlSelect = sql3 & " where datepart(year, Fecha_inicio) = " & TablaBancos.fecha_año & " and datepart(month, Fecha_inicio) = " & TablaBancos.fecha_mes & " "
        ElseIf Me.TablaBancos.ComboAño.Text <> "*" And Me.TablaBancos.ComboMes.Text = "*" Then
            Me.TablaBancos.SqlSelect = sql3 & " where datepart(year, Fecha_inicio) = " & TablaBancos.fecha_año & "  "
        End If
        Me.TablaBancos.Ordenar(True, "Id_Bancos_Clientes")
        If Me.TablaBancos.ComboAño.Text <> "*" And Me.TablaBancos.ComboMes.Text = "*" Then
            Me.TablaBancos.Tablaconsulta(sql2, sql5)
            Me.TablaBancos.Cargar("P_Master", sql, , "Fecha_inicio", " where datepart(year, Fecha_inicio) = " & TablaBancos.ComboAño.Text & "")
        Else
            Me.TablaBancos.Tablaconsulta(sql2, sql4)
            Me.TablaBancos.Cargar("P_Master", sql, , "Fecha_inicio", " where datepart(year, Fecha_inicio) = " & TablaBancos.ComboAño.Text & " and datepart(month, Fecha_inicio) = " & TablaBancos.ComboMes.Text & "")
        End If
        Me.TablaBancos.Ordenar("Id_Bancos_Clientes")
        Try
            Me.TablaBancos.Tabla.Columns(0).Visible = False
            Me.TablaBancos.Tabla.Columns(1).Visible = False
            Me.TablaBancos.Tabla.Columns(2).Visible = False
            Me.TablaBancos.Tabla.Columns(3).Visible = False
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Limpiar()
        Me.lstBancos.SelectText = ""
        Me.LstCuentas.SelectText = ""
        Me.TxtAlias.Text = ""
        Me.TxtCuenta.Text = ""
        Me.DTFechaF.Clear()
        Me.DTFechaI.Clear()
        Me.lstBancos.Text = ""
    End Sub
    Private Sub Cargar_listas()
        Me.lstBancos.Cargar("SELECT Bancos.Id_Banco, Bancos.Nombre FROM     Bancos  ")
        Me.lstCliente.Cargar("SELECT Id_Empresa,Razon_Social FROM Empresa ")
        Me.lstCliente.Combo.SelectedValue = My.Forms.Inicio.Clt
        Me.LstCuentas.Cargar("SELECT Catalogo_de_Cuentas.Id_cat_Cuentas,rtrim(Catalogo_de_Cuentas.Descripcion) + '-' + convert(nvarchar,Catalogo_de_Cuentas.cuenta,103) as Cuenta FROM     Catalogo_de_Cuentas where Id_Empresa =" & Inicio.Clt & " and ( nivel1 ='1020' or Nivel1='2050' or Nivel1='2020'  or Nivel1='1030')and Nivel2 > 0 and Nivel3 > 0 ")

    End Sub
    Private Sub Ventanas(ByVal nuevo As Boolean, ByVal edit As Boolean, ByVal load As Boolean)
        If nuevo = True Then
            Me.TablaBancos.CmdEliminar.Enabled = False
            Me.TablaBancos.CmdGuardar.Enabled = True
            Me.TablaBancos.CmdNuevo.Enabled = False
        ElseIf edit = True Then
            Me.TablaBancos.CmdEliminar.Enabled = True
            Me.TablaBancos.CmdGuardar.Enabled = True
            Me.TablaBancos.CmdNuevo.Enabled = True
        ElseIf load = True Then
            Me.TablaBancos.CmdEliminar.Enabled = False
            Me.TablaBancos.CmdGuardar.Enabled = False
            Me.TablaBancos.CmdNuevo.Enabled = True
        End If
    End Sub
    Private Sub Cargar_datos(ByVal id_Banco_Cliente As Integer)
        Dim consulta As String = "SELECT Bancos_Clientes.Id_Bancos_Clientes, Bancos_Clientes.Id_Banco, Bancos_Clientes.Id_cat_Cuentas, 
                  Bancos_Clientes.Id_Empresa, Empresa.Razon_Social, Bancos_Clientes.Alias, Bancos.Nombre, Bancos.Clave, Bancos_Clientes.No_Cuenta, 
                  Catalogo_de_Cuentas.Cuenta, Catalogo_de_Cuentas.Descripcion, Bancos_Clientes.Fecha_inicio, Bancos_Clientes.Fecha_fin
                  FROM     Bancos INNER JOIN Bancos_Clientes ON Bancos.Id_Banco = Bancos_Clientes.Id_Banco INNER JOIN
                  Catalogo_de_Cuentas ON Bancos_Clientes.Id_cat_Cuentas = Catalogo_de_Cuentas.Id_cat_Cuentas INNER JOIN
                  Empresa ON Empresa.Id_Empresa = Bancos_Clientes.Id_Empresa AND Catalogo_de_Cuentas.Id_Empresa = 
                  Empresa.Id_Empresa where Id_Bancos_Clientes = " & id_Banco_Cliente & ""
        Dim ds As DataSet = Obtener_DS(consulta)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.lstBancos.SelectText = ds.Tables(0).Rows(0)("Nombre")
            Me.lstCliente.SelectText = ds.Tables(0).Rows(0)("Razon_Social")
            Me.DTFechaF.Text = Eventos.Ds_valor(ds, 0, 12)
            Me.DTFechaI.Text = Eventos.Ds_valor(ds, 0, 11)
            Me.TxtAlias.Text = ds.Tables(0).Rows(0)("Alias")
            Me.TxtCuenta.Text = ds.Tables(0).Rows(0)("No_Cuenta")
            Me.LstCuentas.SelectItem = ds.Tables(0).Rows(0)("Id_cat_Cuentas")
            Me.LstCuentas.SelectText = ds.Tables(0).Rows(0)("Cuenta")
            Dim Leyenda As String = ds.Tables(0).Rows(0)("Alias")

            If ds.Tables(0).Rows(0)("Alias") Like "*Transf*" Then
                Me.RadTransf.Checked = True
            ElseIf ds.Tables(0).Rows(0)("Alias") Like "*Tarj*" Then
                Me.RadTarjeta.Checked = True
            ElseIf ds.Tables(0).Rows(0)("Alias") Like "*Cheq*" Then
                Me.RadCheque.Checked = True
            End If
        Else
            activo = True
            Limpiar()
            activo = False
            Exit Sub
        End If
    End Sub

    Private Sub TablaBancos_cerrar() Handles TablaBancos.Cerrar
        Me.Close()
    End Sub

    Private Sub TablaBancos_eliminar() Handles TablaBancos.Eliminar
        Eliminar()
        Me.TablaBancos.CmdActualizar.PerformClick()
    End Sub
    Private Sub Eliminar()
        If Me.lstBancos.Text <> "" Then

            If RadMessageBox.Show("Realmente deseas eliminar el registro : " & Me.lstBancos.SelectText & " del cliente " & Me.lstCliente.SelectText & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If Eventos.Comando_sql("Delete from Bancos_Clientes where Id_Bancos_Clientes=" & Me.lstBancos.Text) = 1 Then
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
    Private Sub TablaBancos_guardar() Handles TablaBancos.Guardar
        If nuevo = True Then
            Guargar_nuevo()
            Me.TablaBancos.CmdActualizar.PerformClick()
        Else
            Editar(Me.lstBancos.Text)
            Me.TablaBancos.CmdActualizar.PerformClick()
        End If
    End Sub
    Private Sub Guargar_nuevo()
        Dim sql As String = "INSERT INTO dbo.Bancos_Clientes"
        sql &= "("
        sql &= " 		Id_Banco, 	Id_Empresa,	Fecha_inicio,	Fecha_fin,	Id_cat_Cuentas,	Alias,	No_Cuenta"
        sql &= " 	)"
        sql &= " VALUES "
        sql &= "("
        sql &= " " & Me.lstBancos.SelectItem & "	," '@id_banco
        sql &= " 	" & Me.lstCliente.SelectItem & "," '@Id_Empresa
        If Me.DTFechaI.Text = "  /  /" Then
            sql &= " 	" & Eventos.Sql_hoy() & "," '@fecha_inicio
        Else
            sql &= " 	" & Eventos.Sql_hoy(Me.DTFechaI.Text) & "," '@fecha_inicio
        End If

        If Me.DTFechaF.Text = "  /  /" Then
            sql &= " 	" & Eventos.Sql_hoy("01/01/2100") & "," '@fecha_fin
        Else
            sql &= " 	" & Eventos.Sql_hoy(Me.DTFechaF.Text) & "," '@fecha_fin
        End If

        sql &= " 	" & Me.LstCuentas.SelectItem & "," '@cuenta catalogo
        sql &= " 	'" & Me.TxtAlias.Text & "'," '@alias
        sql &= " 	" & Me.TxtCuenta.Text & " " '@cuenta banco
        sql &= " 	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("BancosC_I", sql)
            Dim Tip As String = Eventos.ObtenerValorDB("Tipos_Poliza_Sat", "max(clave)+1", " Id_Empresa = " & Me.lstCliente.SelectItem & "", True).ToString.PadLeft(3, "0")

            Eventos.InsertarTipo_poliza(1, Me.TxtAlias.Text, InputBox("Teclea el Tipo de Poliza a 3 digitos Ejemplo: " & Tip & "  ", Eventos.titulo_app, Tip), Me.lstCliente.SelectItem)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
    End Sub
    Private Sub Editar(ByVal id_empresa As Integer)
        Dim sql As String = "UPDATE dbo.Bancos_Clientes"
        sql &= " SET  "
        sql &= " 	Id_Banco =  " & Me.lstBancos.SelectItem & " ," '@id_banco
        sql &= " 	Id_Empresa = " & Me.lstCliente.SelectItem & " ," '@Id_Empresa
        sql &= " 	Id_cat_Cuentas = " & Me.LstCuentas.SelectItem & " ," '@cuenta catalogo
        sql &= " 	No_Cuenta = " & Me.TxtCuenta.Text & " ," '@cuenta bancaria
        sql &= " 	Alias = '" & Me.TxtAlias.Text & "' ," '@Alias 


        If Me.DTFechaF.Text = "  /  /" Then
            sql &= " 	Fecha_inicio = " & Eventos.Sql_hoy() & "," ' @fecha_inicio
        Else
            sql &= " 	Fecha_inicio = " & Eventos.Sql_hoy(Me.DTFechaI.Text) & "," ' @fecha_inicio
        End If

        If Me.DTFechaF.Text = "  /  /" Then
            sql &= " 	Fecha_fin = " & Eventos.Sql_hoy() & " " ' @fecha_fin
        Else
            sql &= " 	Fecha_fin = " & Eventos.Sql_hoy(Me.DTFechaF.Text) & " " ' @fecha_fin
        End If



        sql &= "  Where Id_Bancos_Clientes= " & Me.lstBancos.Text & ""
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("BancosC_U", sql)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo guardar el registro, verifique la información ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If

    End Sub

    Private Sub TablaBancos_nuevo() Handles TablaBancos.Nuevo
        nuevo = True
        activo = True
        Limpiar()
        Ventanas(True, False, False)
        activo = False
    End Sub

    Private Sub TablaBancos_refrescar() Handles TablaBancos.Refrescar
        activo = True
        Cargar_Info()
        Cargar_listas()
        Limpiar()
        Ventanas(False, False, True)
        activo = False
    End Sub

    Private Sub TablaBancos_registro() Handles TablaBancos.Registro
        nuevo = False
        Ventanas(False, True, False)
        Me.lstBancos.Text = Me.TablaBancos.Registro_columa(0)
        Cargar_datos(Me.TablaBancos.Registro_columa(0))
    End Sub

    Private Sub Tabla_detalleBancos_cmd_Editar(clave As String) Handles Tabla_detalleBancos.Cmd_Editar

        Dim descripcion As String = InputBox("Teclea el Nombre del Banco:", Eventos.titulo_app, Me.Tabla_detalleBancos.Registro_columna(1))
        If descripcion = "" Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "Debe colocar un nombre valido", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

            Exit Sub
        End If
        Dim sql As String = "UPDATE dbo.Bancos"
        sql &= " SET Nombre = '" & descripcion & "'" '@nombre
        sql &= " where Id_Banco= " & clave
        If Eventos.Comando_sql(sql) = 1 Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "Datos Guardados correctamente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Text = Ms.ToString()

            Eventos.Insertar_usuariol("Bancos", sql)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "Error al actualizar los datos, revise la información proporcionada...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Me.Text = Ms.ToString()

        End If
    End Sub

    Private Sub Tabla_detalleBancos_cmd_eliminar(clave As String) Handles Tabla_detalleBancos.Cmd_eliminar
        If MessageBox.Show("Realmente deseas eliminar el Banco: " & Me.Tabla_detalleBancos.Registro_columna(1) & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Eventos.Comando_sql("Delete from dbo.Bancos where Id_Banco=" & clave) > 0 Then
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                Dim Ms As DialogResult = RadMessageBox.Show(Me, "Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Text = Ms.ToString()

                Eventos.Insertar_usuariol("Bancos_D", "Delete from dbo.Bancos where Id_Banco= " & clave & "")
            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                Me.Text = Ms.ToString()

            End If
        End If
    End Sub



    Private Sub lstCliente_cambio_item(value As String, texto As String) Handles lstCliente.Cambio_item
        If activo = False Then
            Me.LstCuentas.Cargar("SELECT Catalogo_de_Cuentas.Id_cat_Cuentas,rtrim(Catalogo_de_Cuentas.Descripcion) + '-' + convert(nvarchar,Catalogo_de_Cuentas.cuenta,103) as Cuenta FROM     Catalogo_de_Cuentas where Id_Empresa =" & value & " and ( nivel1 ='1020' or Nivel1='2050' or Nivel1='2020'  or Nivel1='1030')and Nivel2 > 0 and Nivel3 > 0 ")
        End If
    End Sub

    Private Sub Tabla_detalleBancos_Cerrar() Handles Tabla_detalleBancos.Cerrar
        Me.Close()
    End Sub


    Private Sub RadCheque_CheckedChanged(sender As Object, e As EventArgs) Handles RadCheque.CheckedChanged
        If activo = False Then
            Dim largo As Integer = Len(Me.TxtCuenta.Text)
            Try
                If Me.RadCheque.Checked = True Then
                    Me.TxtAlias.Text = "Cheq " & Me.lstBancos.SelectText & " " & Trim(Me.TxtCuenta.Text.Substring(largo - 3, 3))
                ElseIf Me.RadTarjeta.Checked = True Then
                    Me.TxtAlias.Text = "Transf " & Me.lstBancos.SelectText & " Tarj" & " " & Trim(Me.TxtCuenta.Text.Substring(largo - 3, 3))
                ElseIf Me.RadTransf.Checked = True Then
                    Me.TxtAlias.Text = "Transf " & Me.lstBancos.SelectText & " " & Trim(Me.TxtCuenta.Text.Substring(largo - 3, 3))
                End If
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub RadTarjeta_CheckedChanged(sender As Object, e As EventArgs) Handles RadTarjeta.CheckedChanged
        If activo = False Then
            Dim largo As Integer = Len(Me.TxtCuenta.Text)

            Try


                If Me.RadCheque.Checked = True Then
                    Me.TxtAlias.Text = "Cheq " & Me.lstBancos.SelectText & " " & Trim(Me.TxtCuenta.Text.Substring(largo - 3, 3))
                ElseIf Me.RadTarjeta.Checked = True Then
                    Me.TxtAlias.Text = "Transf " & Me.lstBancos.SelectText & " Tarj" & " " & Trim(Me.TxtCuenta.Text.Substring(largo - 3, 3))
                ElseIf Me.RadTransf.Checked = True Then
                    Me.TxtAlias.Text = "Transf " & Me.lstBancos.SelectText & " " & Trim(Me.TxtCuenta.Text.Substring(largo - 3, 3))
                End If
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub RadTransf_CheckedChanged(sender As Object, e As EventArgs) Handles RadTransf.CheckedChanged
        If activo = False Then
            Dim largo As Integer = Len(Me.TxtCuenta.Text)
            Try
                If Me.RadCheque.Checked = True Then
                    Me.TxtAlias.Text = "Cheq " & Me.lstBancos.SelectText & " " & Trim(Me.TxtCuenta.Text.Substring(largo - 3, 3))
                ElseIf Me.RadTarjeta.Checked = True Then
                    Me.TxtAlias.Text = "Transf " & Me.lstBancos.SelectText & " Tarj" & " " & Trim(Me.TxtCuenta.Text.Substring(largo - 3, 3))
                ElseIf Me.RadTransf.Checked = True Then
                    Me.TxtAlias.Text = "Transf " & Me.lstBancos.SelectText & " " & Trim(Me.TxtCuenta.Text.Substring(largo - 3, 3))
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub TxtCuenta_TextChanged(sender As Object, e As EventArgs) Handles TxtCuenta.TextChanged
        If activo = False Then
            Dim largo As Integer = Len(Me.TxtCuenta.Text)

            Try


                If Me.RadCheque.Checked = True Then
                    Me.TxtAlias.Text = "Cheq " & Me.lstBancos.SelectText & " " & Trim(Me.TxtCuenta.Text.Substring(largo - 3, 3))
                ElseIf Me.RadTarjeta.Checked = True Then
                    Me.TxtAlias.Text = "Transf " & Me.lstBancos.SelectText & " Tarj" & " " & Trim(Me.TxtCuenta.Text.Substring(largo - 3, 3))
                ElseIf Me.RadTransf.Checked = True Then
                    Me.TxtAlias.Text = "Transf " & Me.lstBancos.SelectText & " " & Trim(Me.TxtCuenta.Text.Substring(largo - 3, 3))
                End If
            Catch ex As Exception

            End Try

        End If
    End Sub

    Private Sub Tabla_detalleBancos_Cmd_Nuevo() Handles Tabla_detalleBancos.Cmd_Nuevo
        Eventos.Abrir_form(MasivosBancos)
    End Sub


End Class
