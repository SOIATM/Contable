Imports Telerik.WinControls
Public Class Cuentas_Diot

    Private Sub Cuentas_Diot_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Cargar_Listas()
        Buscar_Listas()
        Me.CmdBuscarFact.PerformClick()

    End Sub
    Private Sub Cargar_Listas()
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
        Eventos.DiseñoTablaEnca(Me.TablaImportar)
    End Sub
    Private Sub Buscar_Listas()
        Me.TablaImportar.RowCount = 1
        Dim sql As String = "  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias  
                            FROM Catalogo_de_Cuentas WHERE Nivel1 > 0 and Nivel4 = '0000' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        Dim RP As DataSet = Eventos.Obtener_DS(sql)
        If RP.Tables(0).Rows.Count > 0 Then


            If Me.Alias1.Items.Count = 0 Then
                'Me.Alias1.DataSource = RP.Tables(0)
                'Me.Alias1.DisplayMember = RP.Tables(0).Columns(0).Caption.ToString
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.Alias1.Items.Add(RP.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If
        RP.Clear()
        RP = Eventos.Obtener_DS("SELECT 'Nivel1' AS Nivel UNION SELECT 'Nivel2' AS Nivel UNION SELECT  'Nivel3' AS Nivel UNION SELECT 'Nivel4' AS Nivel")
        If RP.Tables(0).Rows.Count > 0 Then
            If Me.Nivel1.Items.Count = 0 Then
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.Nivel1.Items.Add(RP.Tables(0).Rows(i)("Nivel"))
                Next
            Else

            End If
        End If
        RP.Clear()
        RP = Eventos.Obtener_DS("SELECT 'Cargo' AS Tipo UNION SELECT 'Abono' AS Tipo")
        If RP.Tables(0).Rows.Count > 0 Then
            If Me.TipoC.Items.Count = 0 Then
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.TipoC.Items.Add(RP.Tables(0).Rows(i)("Tipo"))
                Next
            Else

            End If
        End If
        RP.Clear()
        RP = Eventos.Obtener_DS("SELECT 'N' AS Valor UNION SELECT 'P' AS Valor")
        If RP.Tables(0).Rows.Count > 0 Then
            If Me.Valor1.Items.Count = 0 Then
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.Valor1.Items.Add(RP.Tables(0).Rows(i)("Valor"))
                Next
            Else

            End If
        End If
        RP.Clear()
        RP = Eventos.Obtener_DS("SELECT Descripcion FROM Tasas ")
        If RP.Tables(0).Rows.Count > 0 Then
            If Me.Tasa1.Items.Count = 0 Then
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.Tasa1.Items.Add(RP.Tables(0).Rows(i)("Descripcion"))
                Next
            Else

            End If
        End If
    End Sub

    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
		Me.Close()
	End Sub

	Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Rows.Clear()
        End If
        Buscar_Listas()
        Dim consulta As String = " SELECT Id_Cta_Diot, Cuenta, Nivel, Tipo, Valor, Tasa FROM dbo.Cuentas_Diot where Id_Empresa= " & Me.lstCliente.SelectItem & ""
		Dim ds As DataSet = Obtener_DS(consulta)
		If ds.Tables(0).Rows.Count > 0 Then
			Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count
            Dim frm As New BarraProcesovb
            frm.Show()
			frm.Barra.Minimum = 0
			frm.Barra.Maximum = ds.Tables(0).Rows.Count - 1
            Me.Cursor = Cursors.AppStarting

            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

				Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(j)
                Me.TablaImportar.Item(IDr.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Cta_Diot")) = True, "", ds.Tables(0).Rows(j)("Id_Cta_Diot"))

                Try
                    If Trim(ds.Tables(0).Rows(j)("Cuenta")) <> "" Then
                        Fila.Cells(Alias1.Index).Value = Me.Alias1.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Cuenta")), Me.Alias1))
                    End If
                Catch ex As Exception

                End Try


                Try
					If Trim(ds.Tables(0).Rows(j)("Nivel")) <> "" Then
                        Fila.Cells(Nivel1.Index).Value = Me.Nivel1.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Nivel")), Me.Nivel1))
                    End If
				Catch ex As Exception

				End Try

				Try
					If Trim(ds.Tables(0).Rows(j)("Tipo")) <> "" Then
                        Fila.Cells(Tipoc.Index).Value = Me.Tipoc.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Tipo")), Me.Tipoc))
                    End If
				Catch ex As Exception

				End Try
				Try
					If Trim(ds.Tables(0).Rows(j)("Valor")) <> "" Then
                        Fila.Cells(Valor1.Index).Value = Me.Valor1.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Valor")), Me.Valor1))
                    End If
				Catch ex As Exception

				End Try
				Try
					If Trim(ds.Tables(0).Rows(j)("Tasa")) <> "" Then
                        Fila.Cells(Tasa1.Index).Value = Me.Tasa1.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Tasa")), Me.Tasa1))
                    End If
				Catch ex As Exception

				End Try
				frm.Barra.Value = j
			Next
			frm.Close()
			RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
			Me.Cursor = Cursors.Arrow
		Else

			RadMessageBox.Show("No hay Cuentas  para Calculo de Diot ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
		End If

	End Sub
    Private Function Obtener_Index(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer = -1
        For i As Integer = 0 To Col.Items.Count - 1
            If valor = Trim(Col.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice
    End Function
    Private Function Obtener_index2(ByVal valor As String)

        Dim Indice As Integer = -1
        'Dim foundIndex = Me.Alias1.DataSource.Find("Temp", valor)


        For i As Integer = 0 To Me.Alias1.Items.Count - 1
            If valor = Trim(Me.Alias1.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function

    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        Me.TablaImportar.Rows.Add()
    End Sub

    Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.TablaImportar.Rows.Count > 0 Then
			For Each Fila As DataGridViewRow In TablaImportar.Rows
                If Fila.Cells(Alias1.Index).Selected = True Then
                    If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If Me.TablaImportar.Item(IDr.Index, Fila.Index).Value <> Nothing Then
                            If Eventos.Comando_sql("Delete from dbo.Cuentas_Diot where Id_Cta_Diot=" & Me.TablaImportar.Item(IDr.Index, Fila.Index).Value) > 0 Then
                                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                            Else
                                RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                            End If
                        End If

                    End If
                    Me.CmdBuscarFact.PerformClick()
                End If
            Next
		End If
	End Sub
	Private Function Buscar_cuenta(ByVal Cta As String)
		Dim Nombre As String = ""
        Nombre = "SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias FROM Catalogo_de_Cuentas WHERE   Cuenta =" & Cta & " and Id_Empresa = " & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(Nombre)
		If ds.Tables(0).Rows.Count > 0 Then
			Nombre = ds.Tables(0).Rows(0)(0)
		Else
			Nombre = ""
		End If
		Return Nombre
	End Function

	Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
		For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1

            Dim Ctas As String() = Me.TablaImportar.Item(Alias1.Index, i).Value.Split(New Char() {"/"c})
            Dim c As String = Trim(Ctas(0).ToString).Replace("-", "")
            If Me.TablaImportar.Item(IDr.Index, i).Value <> Nothing Then

                Edita_Registro(Me.TablaImportar.Item(IDr.Index, i).Value, Me.TablaImportar.Item(Alias1.Index, i).Value, Me.TablaImportar.Item(Nivel1.Index, i).Value, Me.TablaImportar.Item(Tipoc.Index, i).Value, Me.TablaImportar.Item(Valor1.Index, i).Value, Me.TablaImportar.Item(Tasa1.Index, i).Value)
            Else
                Inserta_Registro(Me.TablaImportar.Item(Alias1.Index, i).Value, Me.TablaImportar.Item(Nivel1.Index, i).Value, Me.TablaImportar.Item(Tipoc.Index, i).Value, Me.TablaImportar.Item(Valor1.Index, i).Value, Me.TablaImportar.Item(Tasa1.Index, i).Value)
            End If
		Next
		Me.CmdBuscarFact.PerformClick()
	End Sub
	Private Sub Edita_Registro(ByVal Id_Cta_Diot As Integer, ByVal Cuenta As String, ByVal Nivel As String, ByVal Tipo As String, ByVal Valor As String, ByVal Tasa As String)
		Dim sql As String = "UPDATE dbo.Cuentas_Diot SET   	Cuenta = '" & Cuenta & "',	Nivel = '" & Nivel & "',	Tipo = '" & Tipo & "',	
                                Valor = '" & Valor & "',	Tasa = '" & Tasa & "'  where Id_Cta_Diot = " & Id_Cta_Diot & "  "
		If Eventos.Comando_sql(sql) > 0 Then

        End If
	End Sub
	Private Sub Inserta_Registro(ByVal Cuenta As String, ByVal Nivel As String, ByVal Tipo As String, ByVal Valor As String, ByVal Tasa As String)
		Dim sql As String = "INSERT INTO dbo.Cuentas_Diot 	(	Cuenta,	Nivel,	Tipo,	Valor,	Tasa,ID_Empresa	) VALUES 	(	'" & Cuenta & "',	'" & Nivel & "',	'" & Tipo & "','" & Valor & "',	'" & Tasa & "'," & Me.lstCliente.SelectItem & "	)"
		If Eventos.Comando_sql(sql) > 0 Then

        End If
	End Sub

	Private Sub CmdPlant_Click(sender As Object, e As EventArgs) Handles CmdPlant.Click

        Dim ds As DataSet = Eventos.Obtener_DS(" SELECT Id_Cta_Diot, Cuenta, Nivel, Tipo, Valor, Tasa FROM dbo.Cuentas_Diot where Id_Empresa= " & Me.lstCliente.SelectItem & "")
        If ds.Tables(0).Rows.Count <= 0 Then



            Dim sql As String = "INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '1810-0000-0000-0000 / GASTOS DE INSTALACIÓN', 'Nivel3', 'Cargo', 'P', '16%',  " & Me.lstCliente.SelectItem & ")
                                

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '9010-0000-0000-0000 / COMPRAS POR PAGAR (CO)', 'Nivel4', 'Cargo', 'P', '16%', " & Me.lstCliente.SelectItem & ")
                                

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '1090-0000-0000-0000 / PAGOS ANTICIPADOS', 'Nivel3', 'Cargo', 'P', '16%', " & Me.lstCliente.SelectItem & ")
                                

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '6010-0020-0000-0000 / Tasa 16%', 'Nivel4', 'Cargo', 'P', '16%', " & Me.lstCliente.SelectItem & ")
                                

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '6020-0020-0000-0000 / Tasa 16%', 'Nivel4', 'Cargo', 'P', '16%', " & Me.lstCliente.SelectItem & ")
                                

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '6030-0020-0000-0000 / Tasa 16%', 'Nivel4', 'Cargo', 'P', '16%', " & Me.lstCliente.SelectItem & ")
                                

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '6040-0020-0000-0000 / Tasa 16%', 'Nivel4', 'Cargo', 'P', '16%', " & Me.lstCliente.SelectItem & ")
                                

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '7010-0010-0000-0000 / Comisiones bancarias', 'Nivel3', 'Cargo', 'P', '16%', " & Me.lstCliente.SelectItem & ")
                              

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '9020-0020-0000-0000 / Tasa 16%', 'Nivel4', 'Abono', 'P', '16%', " & Me.lstCliente.SelectItem & ")
                              

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '1560-0001-0000-0000 / Equipo de cómputo', 'Nivel3', 'Cargo', 'P', '16%', " & Me.lstCliente.SelectItem & ")
                              

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '1530-0000-0000-0000 / MAQUINARIA Y EQUIPO', 'Nivel3', 'Cargo', 'P', '16%', " & Me.lstCliente.SelectItem & ")
                               

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES ( '5020-0001-0003-0000 / Tasa 16%', 'Nivel4', 'Cargo', 'P', '16%', " & Me.lstCliente.SelectItem & ")
                            

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '9021-0010-0020-0000 / Tasa 16%', 'Nivel4', 'Cargo', 'P', '16%', " & Me.lstCliente.SelectItem & ")
                           

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '5030-0001-0003-0000 / Tasa 16%', 'Nivel4', 'Abono', 'N', '16%', " & Me.lstCliente.SelectItem & ")
                              

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '5020-0001-0002-0000 / Tasa 0%', 'Nivel4', 'Cargo', 'P', 'Cero', " & Me.lstCliente.SelectItem & ")
                              

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '5030-0001-0002-0000 / Tasa 0%', 'Nivel4', 'Abono', 'N', 'Cero', " & Me.lstCliente.SelectItem & ")
                              

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '5020-0001-0001-0000 / Tasa Exenta', 'Nivel4', 'Cargo', 'P', 'Exenta', " & Me.lstCliente.SelectItem & ")
                            

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '5030-0001-0001-0000 / Tasa Exenta', 'Nivel4', 'Abono', 'N', 'Exenta', " & Me.lstCliente.SelectItem & ")
                         

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '6010-0010-0029-0000 / Impuesto estatal sobre nóminas', 'Nivel4', 'Cargo', 'P', 'Exenta', " & Me.lstCliente.SelectItem & ")
                              

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '6010-0010-0032-0000 / Servicios administrativos', 'Nivel4', 'Cargo', 'P', 'Exenta', " & Me.lstCliente.SelectItem & ")
                              

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '6010-0010-0033-0000 / Servicios administrativos PR', 'Nivel4', 'Cargo', 'P', 'Exenta', " & Me.lstCliente.SelectItem & ")
                              

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '6010-0010-0034-0000 / Honor persona física resid nac', 'Nivel4', 'Cargo', 'P', 'Exenta', " & Me.lstCliente.SelectItem & ")
                              

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '6010-0010-0035-0000 / Honor persona físic res nac PR', 'Nivel4', 'Cargo', 'P', 'Exenta', " & Me.lstCliente.SelectItem & ")
                              

                                INSERT INTO dbo.Cuentas_Diot (  Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '6010-0010-0036-0000 / Honor persona física resid ext', 'Nivel4', 'Cargo', 'P', 'Exenta', " & Me.lstCliente.SelectItem & ")
                              

                                INSERT INTO dbo.Cuentas_Diot (   Cuenta, Nivel, Tipo, Valor, Tasa, Id_Empresa)
                                VALUES (  '6010-0010-0037-0000 / Honor persona físic res ext PR', 'Nivel4', 'Cargo', 'P', 'Exenta', " & Me.lstCliente.SelectItem & ")
                              
                                "
            If Eventos.Comando_sql(sql) > 0 Then

            End If
        End If
    End Sub


End Class