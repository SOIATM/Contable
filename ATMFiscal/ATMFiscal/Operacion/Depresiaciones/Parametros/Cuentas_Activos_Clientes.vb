Imports Telerik.WinControls
Public Class Cuentas_Activos_Clientes
	Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
		Me.Close()
	End Sub
	Private Sub Buscar_Listas()
		Dim sql As String = "  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias  
                            FROM Catalogo_de_Cuentas WHERE   Clasificacion ='AFI' AND Id_Empresa = " & Me.lstCliente.SelectItem & " AND (RFc <>'NULL' AND RFC <> '' AND RFC IS NOT NULL ) "
		Dim RP As DataSet = Eventos.Obtener_DS(sql)
		If RP.Tables(0).Rows.Count > 0 Then


			If Me.CtaActivo.Items.Count = 0 Then
				For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
					Me.CtaActivo.Items.Add(RP.Tables(0).Rows(i)("Alias"))
				Next
			Else

			End If


		End If
		'Cambiar codigo para activos
		RP.Clear()
		sql = "  SELECT Min(convert(BIGINT,CtaMadre)) AS Minima,max(convert(BIGINT,CtaMadre)) + 100000000 AS Maxima FROM (SELECT DISTINCT    replace( CtaMadre,'-','') AS CtaMadre FROM Tipo_Activos   WHERE CtaMadre IS NOT NULL AND CtaMadre <> '' ) AS Tabla"
		RP = Eventos.Obtener_DS(sql)
		If RP.Tables(0).Rows.Count > 0 Then

			sql = "SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias  FROM Catalogo_de_Cuentas WHERE   Clasificacion ='AFI' AND Id_Empresa = " & Me.lstCliente.SelectItem & "  AND Cuenta  >='" & RP.Tables(0).Rows(0)("Minima") & "' AND Cuenta  <'" & RP.Tables(0).Rows(0)("Maxima") & "' "
			RP.Clear()
			RP = Eventos.Obtener_DS(sql)
			If RP.Tables(0).Rows.Count > 0 Then
				If Me.CtaDepAct.Items.Count = 0 Then
					For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
						Me.CtaDepAct.Items.Add(RP.Tables(0).Rows(i)("Alias"))
					Next
				Else

				End If
			End If
		End If



		Dim sql2 As String = "  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias  
                            FROM Catalogo_de_Cuentas WHERE     Id_Empresa = " & Me.lstCliente.SelectItem & "     "
		Dim RP2 As DataSet = Eventos.Obtener_DS(sql2)
		If RP2.Tables(0).Rows.Count > 0 Then


			If Me.ResDep1.Items.Count = 0 Then
				For i As Integer = 0 To RP2.Tables(0).Rows.Count - 1
					Me.ResDep1.Items.Add(RP2.Tables(0).Rows(i)("Alias"))
					Me.ResDep2.Items.Add(RP2.Tables(0).Rows(i)("Alias"))
					Me.ResDep3.Items.Add(RP2.Tables(0).Rows(i)("Alias"))
					Me.ResDep4.Items.Add(RP2.Tables(0).Rows(i)("Alias"))
				Next
			Else

			End If
		End If


	End Sub
	Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.TablaImportar.Rows.Count > 0 Then
			Me.TablaImportar.Rows.Clear()
		End If

		Dim Cuentas As DataSet = Eventos.Obtener_DS(" SELECT Convert(varchar,Tipo_Activos.Id_Tipo ,103)  + '-'+ 	Tipo_Activos.Descripcion AS Tipo FROM Tipo_Activos   order by Tipo ")
		If Cuentas.Tables(0).Rows.Count > 0 Then
			If Me.Tipo.Items.Count = 0 Then
				For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
					Me.Tipo.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Tipo")))
				Next
			End If
		End If

		Cuentas.Clear()
		Cuentas = Eventos.Obtener_DS(" SELECT  Convert(varchar,Dep_Anual_Activos.Id_Dep_Activos,103) + '-' + convert(VARCHAR,Dep_Anual_Activos.Monto,103)   AS Dep FROM Dep_Anual_Activos order by Dep    ")
		If Cuentas.Tables(0).Rows.Count > 0 Then
			If Me.Dep.Items.Count = 0 Then
				For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
					Me.Dep.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Dep")))
				Next
			End If
		End If


		Buscar_Listas()
		Dim consulta As String = "SELECT 	Id_Ctas_Activo,	Convert(VARCHAR,Tipo_Activos.Id_Tipo,103) + '-' + Tipo_Activos.Descripcion AS Tipo,Convert(VARCHAR,Dep_Anual_Activos.Id_Dep_Activos,103)  + '-' + convert(VARCHAR,Dep_Anual_Activos.Monto,103) AS Dep ,	
                                Empresa.Id_Empresa,Catalogo_de_Cuentas.Id_cat_Cuentas ,Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias,Ctas_Activo_Cliente.Id_Res_dep1,
                                Ctas_Activo_Cliente.Id_Res_dep2,Ctas_Activo_Cliente.Id_Res_dep3,Ctas_Activo_Cliente.Id_Res_dep4,Ctas_Activo_Cliente.Id_Cta_depAct
                                FROM     Ctas_Activo_Cliente INNER JOIN
                  Tipo_Activos ON Tipo_Activos.Id_Tipo = Ctas_Activo_Cliente.Id_Tipo INNER JOIN
                  Empresa ON Empresa.Id_Empresa = Ctas_Activo_Cliente.Id_Empresa INNER JOIN
                  Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Ctas_Activo_Cliente.Id_cat_Cuentas AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa LEFT OUTER JOIN
                  Dep_Anual_Activos ON Dep_Anual_Activos.Id_Dep_Activos = Ctas_Activo_Cliente.Id_Dep_Activos WHERE Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ""
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
				Me.TablaImportar.Item(ID.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Ctas_Activo")) = True, "", ds.Tables(0).Rows(j)("Id_Ctas_Activo"))
				Try
					If Trim(ds.Tables(0).Rows(j)("Tipo")) <> "" Then
						Fila.Cells(Tipo.Index).Value = Me.Tipo.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Tipo")), Me.Tipo))
					End If
				Catch ex As Exception

				End Try
				Try
					If Trim(ds.Tables(0).Rows(j)("Dep")) <> "" Then
						Fila.Cells(Dep.Index).Value = Me.Dep.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Dep")), Me.Dep))
					End If
				Catch ex As Exception

				End Try
				Me.TablaImportar.Item(IDCta.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_cat_Cuentas")) = True, "", ds.Tables(0).Rows(j)("Id_cat_Cuentas"))

				Try
					If Trim(ds.Tables(0).Rows(j)("Alias")) <> "" Then
						Fila.Cells(CtaActivo.Index).Value = Me.CtaActivo.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Alias")), Me.CtaActivo))
					End If
				Catch ex As Exception

				End Try


				Try
					If Trim(ds.Tables(0).Rows(j)("Id_Res_dep1")) <> "" Then
						Fila.Cells(ResDep1.Index).Value = Me.ResDep1.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Id_Res_dep1")), Me.ResDep1))
					End If
				Catch ex As Exception

				End Try
				Try
					If Trim(ds.Tables(0).Rows(j)("Id_Res_dep2")) <> "" Then
						Fila.Cells(ResDep2.Index).Value = Me.ResDep2.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Id_Res_dep2")), Me.ResDep2))
					End If
				Catch ex As Exception

				End Try
				Try
					If Trim(ds.Tables(0).Rows(j)("Id_Res_dep3")) <> "" Then
						Fila.Cells(ResDep3.Index).Value = Me.ResDep3.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Id_Res_dep3")), Me.ResDep3))
					End If
				Catch ex As Exception

				End Try
				Try
					If Trim(ds.Tables(0).Rows(j)("Id_Res_dep4")) <> "" Then
						Fila.Cells(ResDep4.Index).Value = Me.ResDep4.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Id_Res_dep4")), Me.ResDep4))
					End If
				Catch ex As Exception

				End Try
				Try
					If Trim(ds.Tables(0).Rows(j)("Id_Cta_depAct")) <> "" Then
						Fila.Cells(CtaDepAct.Index).Value = Me.CtaDepAct.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Id_Cta_depAct")), Me.CtaDepAct))
					End If
				Catch ex As Exception

				End Try
				frm.Barra.Value = j
			Next
			frm.Close()
			RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
			Me.Cursor = Cursors.Arrow

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
    Private Sub TablaVentas_EditingControlShowing(ByVal sender As Object, ByVal e As DataGridViewEditingControlShowingEventArgs) Handles TablaImportar.EditingControlShowing

        Dim ctrl As Control = DirectCast(e.Control, Control)

        If (TypeOf ctrl Is DataGridViewComboBoxEditingControl) Then

            Dim cb As ComboBox = DirectCast(ctrl, ComboBox)

            cb.DropDownStyle = ComboBoxStyle.DropDown

        End If

    End Sub

    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
		Me.TablaImportar.Rows.Add()
	End Sub

	Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
		Dim Tip, De As Integer
		For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
			Dim cadena As String = Me.TablaImportar.Item(Tipo.Index, i).Value
			Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
			Dim Cla As String = cadena.Substring(0, posil - 1)

			Tip = Cla
			Cla = ""
			If Me.TablaImportar.Item(Dep.Index, i).Value <> Nothing Then
				cadena = Me.TablaImportar.Item(Dep.Index, i).Value
				posil = InStr(1, cadena, "-", CompareMethod.Binary)
				Cla = cadena.Substring(0, posil - 1)
				De = Cla
			Else
				De = 0
			End If
			If Me.TablaImportar.Item(ID.Index, i).Value <> Nothing Then

				Edita_Registro(Me.TablaImportar.Item(ID.Index, i).Value, Tip, De, Me.TablaImportar.Item(IDCta.Index, i).Value,
							   Me.TablaImportar.Item(ResDep1.Index, i).Value, Me.TablaImportar.Item(ResDep2.Index, i).Value, Me.TablaImportar.Item(ResDep3.Index, i).Value, Me.TablaImportar.Item(ResDep4.Index, i).Value, Me.TablaImportar.Item(CtaDepAct.Index, i).Value)
			Else
				Inserta_Registro(Tip, De, Me.TablaImportar.Item(IDCta.Index, i).Value, Me.TablaImportar.Item(ResDep1.Index, i).Value,
								 Me.TablaImportar.Item(ResDep2.Index, i).Value, Me.TablaImportar.Item(ResDep3.Index, i).Value, Me.TablaImportar.Item(ResDep4.Index, i).Value, Me.TablaImportar.Item(CtaDepAct.Index, i).Value)
			End If
		Next
		Me.CmdBuscarFact.PerformClick()
	End Sub
	Private Sub Edita_Registro(ByVal Id As Integer, ByVal Id_Tipo As Integer, ByVal Id_Dep_Activos As Integer, ByVal Id_cat_Cuentas As Integer,
							   ByVal Id_Res_dep1 As String, ByVal Id_Res_dep2 As String, ByVal Id_Res_dep3 As String, ByVal Id_Res_dep4 As String, ByVal Id_Cta_depAct As String)
		Dim sql As String = "UPDATE dbo.Ctas_Activo_Cliente
                            SET  
	                        Id_Tipo = '" & Id_Tipo & "',Id_Dep_Activos = " & Id_Dep_Activos & ",Id_cat_Cuentas = " & Id_cat_Cuentas & " 
                            ,Id_Res_dep1 = '" & Id_Res_dep1 & "',Id_Res_dep2 = '" & Id_Res_dep2 & "',Id_Res_dep3 = '" & Id_Res_dep3 & "',Id_Res_dep4 = '" & Id_Res_dep4 & "' ,Id_Cta_depAct = '" & Id_Cta_depAct & "'
                             where Id_Ctas_Activo = " & Id & "  "
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("Ctas_Activo_ClienteE", sql)
		End If
	End Sub
	Private Sub Inserta_Registro(ByVal Id_Tipo As Integer, ByVal Id_Dep_Activos As Integer, ByVal Id_cat_Cuentas As Integer,
							   ByVal Id_Res_dep1 As String, ByVal Id_Res_dep2 As String, ByVal Id_Res_dep3 As String, ByVal Id_Res_dep4 As String, ByVal Id_Cta_depAct As String)
		Dim sql As String = "INSERT INTO dbo.Ctas_Activo_Cliente
	                        (
	                        Id_Tipo, Id_Dep_Activos,Id_cat_Cuentas,Id_Empresa,Id_Res_dep1,Id_Res_dep2,Id_Res_dep3,Id_Res_dep4,Id_Cta_depAct
	                        )
                            VALUES 
                            	(
                            	 
                            	" & Id_Tipo & ", 
                                " & Id_Dep_Activos & " ,
                                " & Id_cat_Cuentas & "," & Me.lstCliente.SelectItem & ",'" & Id_Res_dep1 & "','" & Id_Res_dep2 & "','" & Id_Res_dep3 & "','" & Id_Res_dep4 & "','" & Id_Cta_depAct & "'
                            	)"
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("Ctas_Activo_ClienteI", sql)
		End If
	End Sub
	Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.TablaImportar.Rows.Count > 0 Then
			For Each Fila As DataGridViewRow In TablaImportar.Rows
				If Fila.Cells(CtaActivo.Index).Selected = True Then
					If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
						If Me.TablaImportar.Item(ID.Index, Fila.Index).Value <> Nothing Then
							If Eventos.Comando_sql("Delete from dbo.Ctas_Activo_Cliente where Id_Ctas_Activo=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value) > 0 Then
								RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
								Eventos.Insertar_usuariol("Ctas_Activo_ClienteD", "Delete from dbo.Ctas_Activo_Cliente where Id_Ctas_Activo=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value & "")
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

	Private Sub TipoActivos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Eventos.DiseñoTabla(Me.TablaImportar)
		Me.lstCliente.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                                FROM     Empresa INNER JOIN
                                Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                                Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                                Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                                Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                                WHERE  (Usuarios.Usuario LIKE '%" & Inicio.LblUsuario.Text & "%')")
		Me.lstCliente.SelectItem = Inicio.Clt
		Me.CmdBuscarFact.PerformClick()

	End Sub

	Private Sub TablaImportar_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellEndEdit
		If Me.TablaImportar.Item(CtaActivo.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
			Me.TablaImportar.Item(IDCta.Index, Me.TablaImportar.CurrentRow.Index).Value = Buscar_cuenta(Replace(Me.TablaImportar.Item(CtaActivo.Index, Me.TablaImportar.CurrentRow.Index).Value.ToString.Substring(0, 19), "-", ""))
		Else
			Me.TablaImportar.Item(IDCta.Index, Me.TablaImportar.CurrentRow.Index).Value = ""

		End If
	End Sub
	Private Function Buscar_cuenta(ByVal Cta As String)
		Dim Nombre As String = ""
		Nombre = "SELECT Id_cat_Cuentas FROM Catalogo_de_Cuentas WHERE Id_Empresa = " & Me.lstCliente.SelectItem & " AND Cuenta =" & Cta & ""
		Dim ds As DataSet = Eventos.Obtener_DS(Nombre)
		If ds.Tables(0).Rows.Count > 0 Then
			Nombre = ds.Tables(0).Rows(0)(0)
		Else
			Nombre = ""
		End If
		Return Nombre
	End Function

    Private Sub TablaImportar_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaImportar.DataError

    End Sub
End Class