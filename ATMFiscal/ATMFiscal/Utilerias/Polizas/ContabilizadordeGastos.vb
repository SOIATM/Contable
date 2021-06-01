Imports Telerik.WinControls
Public Class ContabilizadordeGastos
    Private Sub ContabilizadordeGastos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.DiseñoTabla(Me.Tabla)
        Dim i As Integer
        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2010 Then
                Me.comboAño.Items.Add(Str(i))
            End If
        Next

        Me.comboAño.Text = Str(DateTime.Now.Year)
        Dim Mes = Now.Date.Month.ToString

        If Len(Mes) < 2 Then
            Mes = "0" & Mes
        End If
        Me.ComboMes.Text = Mes

        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2014 Then
                Me.AnioFin.Items.Add(Str(i))
            End If
        Next

        Me.AnioFin.Text = Str(DateTime.Now.Year)
        Mes = Now.Date.Month.ToString

        If Len(Mes) < 2 Then
            Mes = "0" & Mes
        End If
        Me.MesFin.Text = Mes
    End Sub

    Private Sub CmdCerrar_Click(sender As Object, e As EventArgs) Handles CmdCerrar.Click
		Me.Close()
	End Sub

	Private Sub CmdExportaExcel_Click(sender As Object, e As EventArgs) Handles CmdExportaExcel.Click
		RadMessageBox.SetThemeName("MaterialBlueGrey")
		If Tabla.RowCount > 0 Then
			RadMessageBox.Show("Este Proceso puede tardar dependiendo de la información a exportar, presione Aceptar y espere a que el proceso termine...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
			Dim frm As New DialogExpExcel
			frm.Show()
			frm.Barra.Minimum = 0
			frm.Barra.Maximum = Me.Tabla.RowCount - 1
			Dim m_Excel As Microsoft.Office.Interop.Excel.Application
			Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook
			Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet
			m_Excel = New Microsoft.Office.Interop.Excel.Application
			m_Excel.Visible = False
			objLibroExcel = m_Excel.Workbooks.Add()
			objHojaExcel = objLibroExcel.Worksheets(1)
			objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
			objHojaExcel.Activate()
			Dim i As Integer, j As Integer
			For i = 0 To Tabla.Columns.Count - 1
				objHojaExcel.Cells(1, i + 1) = Me.Tabla.Columns.Item(i).HeaderCell.Value
			Next
			For i = 0 To Tabla.RowCount - 1
				frm.Barra.Value1 = i
				For j = 0 To Tabla.Columns.Count - 1
					objHojaExcel.Cells(i + 2, j + 1) = Me.Tabla.Item(j, i).Value
				Next
			Next
			frm.Close()
			RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
			m_Excel.Visible = True
		Else
			RadMessageBox.Show("No hay registros a exportar....", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
		End If
	End Sub

	Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
		Dim Sql As String = "SELECT CASE WHEN XMLPolizas.Tipo  IN( 1,2) THEN 'Combustible' "
		Sql &= " WHEN XMLPolizas.Tipo  IN(3) THEN 'Peaje'"
		Sql &= " WHEN XMLPolizas.Tipo  IN(4) THEN 'Alimentos'"
		Sql &= " WHEN XMLPolizas.Tipo  IN(5) THEN 'Pasaje'"
		Sql &= " WHEN XMLPolizas.Tipo  IN(6) THEN 'Otros' END AS Serie,  XMLPolizas.UUDI ,  XMLPolizas.Emisor, XMLPolizas.Id_orden, personal.ID_matricula, personal.Nombres + ' ' + personal.Ap_paterno + ' ' + Personal.Ap_materno AS Nombre,personal.Reg_fed_causantes AS RFC "
		Sql &= " FROM XMLPolizas INNER JOIN Orden_traslados ON Orden_traslados.ID_orden = XMLPolizas.Id_orden "
		Sql &= " INNER JOIN Personal ON personal.ID_matricula = Orden_traslados.ID_matricula  WHERE Orden_traslados.Id_orden IN (SELECT Orden_traslados.ID_orden  FROM Orden_traslados"
		Sql &= " WHERE ((  Orden_traslados.Anio_ot =" & Me.comboAño.Text.Trim() & " and Orden_traslados.Mes_ot =" & Me.ComboMes.Text.Trim() & ") "
		Sql &= " Or (Orden_traslados.Anio_ot = " & Me.AnioFin.Text.Trim() & " And Orden_traslados.Mes_ot <= " & Me.MesFin.Text.Trim() & "))"
		Sql &= " AND Orden_traslados.Traslado_cancelado =0 AND Facturar <>127)"
		Dim ds As DataSet = Eventos.Obtener_DSTusa(Sql)
		If ds.Tables(0).Rows.Count > 0 Then
			Me.Tabla.RowCount = ds.Tables(0).Rows.Count
			For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
				Me.Tabla.Item(Serie.Index, i).Value = ds.Tables(0).Rows(i)("Serie")
				Me.Tabla.Item(UUID.Index, i).Value = ds.Tables(0).Rows(i)("UUDI")
				Me.Tabla.Item(Orden.Index, i).Value = ds.Tables(0).Rows(i)("Id_orden")
				Me.Tabla.Item(Emisor.Index, i).Value = ds.Tables(0).Rows(i)("Emisor")
				Me.Tabla.Item(Mat.Index, i).Value = ds.Tables(0).Rows(i)("ID_matricula")
				Me.Tabla.Item(Nombre.Index, i).Value = ds.Tables(0).Rows(i)("Nombre")
				Me.Tabla.Item(RFC.Index, i).Value = ds.Tables(0).Rows(i)("RFC")
			Next
		End If
	End Sub

    Private Sub Cmdguardar_Click(sender As Object, e As EventArgs) Handles Cmdguardar.Click

    End Sub
End Class