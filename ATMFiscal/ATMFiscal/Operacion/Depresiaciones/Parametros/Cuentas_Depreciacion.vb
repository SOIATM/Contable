Imports Telerik.WinControls
Public Class Cuentas_Depreciacion
	Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
		Me.Close()
	End Sub

	Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.TablaImportar.Rows.Count > 0 Then
			Me.TablaImportar.Rows.Clear()
		End If
		Dim Cuentas As DataSet = Eventos.Obtener_DS(" SELECT Convert(varchar,Clave_Activos.Id_Clave ,103)  + '-'+ 	Clave_Activos.Descripcion AS Tipo FROM Clave_Activos   order by Tipo ")
		If Cuentas.Tables(0).Rows.Count > 0 Then
			If Me.Tipo.Items.Count = 0 Then
				For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
					Me.Tipo.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Tipo")))
				Next
			End If
		End If


		Dim consulta As String = " SELECT CtasDep.Id_Cta_Dep,   CtasDep.Cta_Madre ,Convert(VARCHAR,Clave_Activos.Id_Clave,103) + '-' + Clave_Activos.Descripcion AS Tipo      FROM CtasDep  INNER JOIN Clave_Activos ON Clave_Activos.Id_Clave =CtasDep.Id_Clave INNER JOIN Empresa ON Empresa.Id_Empresa =CtasDep.Id_Empresa WHERE Empresa.Id_Empresa  = " & Me.lstCliente.SelectItem & "  "
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
				Try
					If Trim(ds.Tables(0).Rows(j)("Tipo")) <> "" Then
						Fila.Cells(Tipo.Index).Value = Me.Tipo.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Tipo")), Me.Tipo))
					End If
				Catch ex As Exception

				End Try
				Me.TablaImportar.Item(ID.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Cta_Dep")) = True, "", ds.Tables(0).Rows(j)("Id_Cta_Dep"))
				Me.TablaImportar.Item(CtaActivo.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cta_Madre")) = True, "", ds.Tables(0).Rows(j)("Cta_Madre"))
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
			If Me.TablaImportar.Item(ID.Index, i).Value <> Nothing Then

				Edita_Registro(Me.TablaImportar.Item(ID.Index, i).Value, Me.TablaImportar.Item(CtaActivo.Index, i).Value, Tip)
			Else
				Inserta_Registro(Me.TablaImportar.Item(CtaActivo.Index, i).Value, Tip, Me.lstCliente.SelectItem)
			End If
		Next
		Me.CmdBuscarFact.PerformClick()
	End Sub
	Private Sub Edita_Registro(ByVal Id As Integer, ByVal Cta_Madre As String, ByVal Id_Clave As Integer)
		Dim sql As String = "UPDATE dbo.CtasDep
                            SET  
	                        Cta_Madre = '" & Cta_Madre & "',Id_Clave = " & Id_Clave & "  where Id_Cta_Dep = " & Id & "  "
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("CtasDepE", sql)
		End If
	End Sub
	Private Sub Inserta_Registro(ByVal Cta_Madre As String, ByVal Id_Clave As Integer, ByVal Id_Empresa As String)
		Dim sql As String = "INSERT INTO dbo.CtasDep
	                        (
	                        Cta_Madre, Id_Clave,Id_Empresa 
	                        )
                            VALUES 
                            	(
                            	 
                            	'" & Cta_Madre & "', 
                                " & Id_Clave & " ,
                                " & Id_Empresa & " 
                            	)"
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("CtasDepI", sql)
		End If
	End Sub
	Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.TablaImportar.Rows.Count > 0 Then
			For Each Fila As DataGridViewRow In TablaImportar.Rows
				If Fila.Cells(CtaActivo.Index).Selected = True Then
					If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
						If Me.TablaImportar.Item(ID.Index, Fila.Index).Value <> Nothing Then
							If Eventos.Comando_sql("Delete from dbo.CtasDep where Id_Cta_Dep=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value) > 0 Then
								RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
								Eventos.Insertar_usuariol("CtasDepD", "Delete from dbo.CtasDep where Id_Cta_Dep=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value & "")
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
End Class