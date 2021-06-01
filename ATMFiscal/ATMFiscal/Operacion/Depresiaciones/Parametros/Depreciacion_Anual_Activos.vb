Imports Telerik.WinControls
Public Class Depreciacion_Anual_Activos
	Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
		Me.Close()
	End Sub


	Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.TablaImportar.Rows.Count > 0 Then
			Me.TablaImportar.Rows.Clear()
		End If


		Dim consulta As String = "SELECT 	Id_Dep_Activos,Dia,Mes,Anio,Monto FROM dbo.Dep_Anual_Activos "
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
				Me.TablaImportar.Item(ID.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Dep_Activos")) = True, "", ds.Tables(0).Rows(j)("Id_Dep_Activos"))
				Me.TablaImportar.Item(Dia.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Dia")) = True, "", ds.Tables(0).Rows(j)("Dia"))
				Me.TablaImportar.Item(Mes.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Mes")) = True, "", ds.Tables(0).Rows(j)("Mes"))
				Me.TablaImportar.Item(Anio.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Anio")) = True, "", ds.Tables(0).Rows(j)("Anio"))
				Me.TablaImportar.Item(Monto.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Monto")) = True, "", ds.Tables(0).Rows(j)("Monto"))
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
		For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1

			If Me.TablaImportar.Item(ID.Index, i).Value <> Nothing Then

				Edita_Registro(Me.TablaImportar.Item(ID.Index, i).Value, IIf(Len(Me.TablaImportar.Item(Dia.Index, i).Value.trim()) = 1, "" & Me.TablaImportar.Item(Dia.Index, i).Value.trim(), Me.TablaImportar.Item(Dia.Index, i).Value.trim()),
								IIf(Len(Me.TablaImportar.Item(Mes.Index, i).Value.trim()) = 1, "" & Me.TablaImportar.Item(Mes.Index, i).Value.trim(), Me.TablaImportar.Item(Mes.Index, i).Value.trim()),
								IIf(Len(Me.TablaImportar.Item(Anio.Index, i).Value.trim()) = 1, "" & Me.TablaImportar.Item(Anio.Index, i).Value.trim(), Me.TablaImportar.Item(Anio.Index, i).Value.trim()), Me.TablaImportar.Item(Monto.Index, i).Value)
			Else
				Inserta_Registro(IIf(Len(Me.TablaImportar.Item(Dia.Index, i).Value.trim()) = 1, "" & Me.TablaImportar.Item(Dia.Index, i).Value.trim(), Me.TablaImportar.Item(Dia.Index, i).Value.trim()),
								IIf(Len(Me.TablaImportar.Item(Mes.Index, i).Value.trim()) = 1, "" & Me.TablaImportar.Item(Mes.Index, i).Value.trim(), Me.TablaImportar.Item(Mes.Index, i).Value.trim()),
								IIf(Len(Me.TablaImportar.Item(Anio.Index, i).Value.trim()) = 1, "" & Me.TablaImportar.Item(Anio.Index, i).Value.trim(), Me.TablaImportar.Item(Anio.Index, i).Value.trim()), Me.TablaImportar.Item(Monto.Index, i).Value)
			End If
		Next
		Me.CmdBuscarFact.PerformClick()
	End Sub
	Private Sub Edita_Registro(ByVal Id As Integer, ByVal Dia As String, ByVal Mes As String, ByVal Anio As String, ByVal Monto As Decimal)
		Dim sql As String = "UPDATE dbo.Dep_Anual_Activos
                            SET  Dia = '" & Dia & "',Mes = '" & Mes & "',Anio = '" & Anio & "',Monto = " & Monto & "

	                        where Id_Tipo = " & Id & "  "
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("Dep_Anual_ActivosE", sql)
		End If
	End Sub
	Private Sub Inserta_Registro(ByVal Dia As String, ByVal Mes As String, ByVal Anio As String, ByVal Monto As Decimal)
		Dim sql As String = "INSERT INTO dbo.Dep_Anual_Activos
	                        (
	                        Dia,Mes,Anio,Monto ) VALUES ('" & Trim(Dia) & "', '" & Mes & "' ,'" & Anio & "'," & Monto & ")"
		If Eventos.Comando_sql(sql) > 0 Then
			Eventos.Insertar_usuariol("Dep_Anual_ActivosI", sql)
		End If
	End Sub
	Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Me.TablaImportar.Rows.Count > 0 Then
			For Each Fila As DataGridViewRow In TablaImportar.Rows
				If Fila.Cells(Dia.Index).Selected = True Then
					If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
						If Me.TablaImportar.Item(ID.Index, Fila.Index).Value <> Nothing Then
							If Eventos.Comando_sql("Delete from dbo.Dep_Anual_Activos where Id_Dep_Activos=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value) > 0 Then
								RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
								Eventos.Insertar_usuariol("Dep_Anual_ActivosD", "Delete from dbo.Dep_Anual_Activos where Id_Dep_Activos=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value & "")
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

	Private Sub Depreciacion_Anual_Activos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.DiseñoTabla(Me.TablaImportar)
        Me.CmdBuscarFact.PerformClick()
    End Sub
End Class
