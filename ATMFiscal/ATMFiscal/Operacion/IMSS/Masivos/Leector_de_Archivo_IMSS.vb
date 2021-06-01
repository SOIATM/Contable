Imports System
Imports System.Data.OleDb
Imports Telerik.WinControls
Public Class Leector_de_Archivo_IMSS

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiarN_Click(sender As Object, e As EventArgs) Handles CmdLimpiarN.Click
        If Me.TablaAltas.Rows.Count > 0 Then
            Me.TablaAltas.Columns.Clear()
        End If
    End Sub

    Private Sub CmdBuscarN_Click(sender As Object, e As EventArgs) Handles CmdBuscarN.Click
        Try

            Dim dt As DataTable = Leector()
            Me.TablaAltas.DataSource = dt

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try
    End Sub
    Public Function Leector() As DataTable
        Dim dt As DataTable = New DataTable
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        With OpenFD
            .Title = "Seleccionar archivo de Texto"
            .Filter = "Archivos de Texto (*.txt)|*.txt"

            .Multiselect = False
            '.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Return Nothing
            End If
        End With
        Dim Line As String
        Try
            Dim Reader As System.IO.StreamReader = New System.IO.StreamReader(archivo)
            Dim Lineas As New ArrayList()
            dt.Columns.Add("Registro_Patronal")
            dt.Columns.Add("Digito_Verificador")
            dt.Columns.Add("Numero_Seguridad_Social")
            dt.Columns.Add("Digito_Verificador_NSS")
            dt.Columns.Add("Ap_Paterno")
            dt.Columns.Add("Ap_Materno")
            dt.Columns.Add("Nombre")
            dt.Columns.Add("Salario_Base")
            dt.Columns.Add("Tipo_Trabajador")
            dt.Columns.Add("Tipo_Salario")
            dt.Columns.Add("Semana_Jornada_Reduciada")
            dt.Columns.Add("Fecha_Movimiento")
            dt.Columns.Add("Unidad_Medica_Familiar")
            dt.Columns.Add("Tipo_Movimiento")
            dt.Columns.Add("Guia")
            dt.Columns.Add("Clave_Trabajador")
            dt.Columns.Add("CURP")
            dt.Columns.Add("Identificador_Formato")
            dt.Columns.Add("Causa_Baja")

            Do
                Line = Reader.ReadLine()
                If Not Line Is Nothing Then
                    Lineas.Add(Line)
                End If
            Loop Until Line Is Nothing
            Reader.Close()


            For Each Fila In Lineas
                dt.Rows.Add(Fila.ToString.Substring(0, 10), Fila.ToString.Substring(10, 1),
                            Fila.ToString.Substring(11, 10), Fila.ToString.Substring(21, 1),
                            Fila.ToString.Substring(22, 27), Fila.ToString.Substring(49, 27),
                            Fila.ToString.Substring(76, 27), Fila.ToString.Substring(103, 6),
                            Fila.ToString.Substring(115, 1), Fila.ToString.Substring(116, 1),
                            Fila.ToString.Substring(117, 1), Fila.ToString.Substring(118, 8),
                            Fila.ToString.Substring(126, 3), Fila.ToString.Substring(131, 2),
                            Fila.ToString.Substring(133, 5), Fila.ToString.Substring(138, 10),
                            Fila.ToString.Substring(149, 18), Fila.ToString.Substring(167, 1),
                            Fila.ToString.Substring(148, 1))
            Next

            Return dt
        Catch ex As Exception

        End Try



    End Function
    Private Sub DataTable_To_Text(ByVal table As DataTable, ByVal path As String, ByVal header As Boolean, ByVal delimiter As Char)
        If table.Columns.Count < 0 OrElse table.Rows.Count < 0 Then
            Exit Sub
        End If

        Using sw As IO.StreamWriter = New IO.StreamWriter(path)
            If header Then
                For i As Integer = 0 To table.Columns.Count - 2
                    sw.Write(table.Columns(i).ColumnName & delimiter)
                Next
                sw.Write(table.Columns(table.Columns.Count - 1).ColumnName & Environment.NewLine)
            End If

            For row As Integer = 0 To table.Rows.Count - 2
                For col As Integer = 0 To table.Columns.Count - 2
                    sw.Write(table.Rows(row).Item(col).ToString & delimiter)
                Next
                sw.Write(table.Rows(row).Item(table.Columns.Count - 1).ToString & Environment.NewLine)
            Next

            For col As Integer = 0 To table.Columns.Count - 2
                sw.Write(table.Rows(table.Rows.Count - 1).Item(col).ToString & delimiter)
            Next
            sw.Write(table.Rows(table.Rows.Count - 1).Item(table.Columns.Count - 1).ToString)
        End Using

    End Sub

    Private Function Text_To_DataTable(ByVal path As String, ByVal delimitter As Char, ByVal header As Boolean) As DataTable
        Dim source As String = String.Empty
        Dim dt As DataTable = New DataTable

        If IO.File.Exists(path) Then
            source = IO.File.ReadAllText(path)
        Else
            Throw New IO.FileNotFoundException("Could not find the file at " & path, path)
        End If

        Dim rows() As String = source.Split({Environment.NewLine}, StringSplitOptions.None)

        For i As Integer = 0 To rows(0).Split(delimitter).Length - 1
            Dim column As String = rows(0).Split(delimitter)(i)
            dt.Columns.Add(If(header, column, "column" & i + 1))
        Next

        For i As Integer = If(header, 1, 0) To rows.Length - 1
            Dim dr As DataRow = dt.NewRow

            For x As Integer = 0 To rows(i).Split(delimitter).Length - 1
                If x <= dt.Columns.Count - 1 Then
                    dr(x) = rows(i).Split(delimitter)(x)
                Else
                    Throw New Exception("The number of columns on row " & i + If(header, 0, 1) & " is greater than the amount of columns in the " & If(header, "header.", "first row."))
                End If
            Next

            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim DT As New DataTable
        Dim result As DialogResult = DialogoImpresoras.ShowDialog()
        Dim nom_impresora As String = DialogoImpresoras.PrinterSettings.PrinterName

        If Trim(Me.TablaAltas.Item(13, 0).Value) = "08" Then
            With DT ' Reingresos
                .Columns.Add("Num_Seg_Socila")
                .Columns.Add("Dv")
                .Columns.Add("CURP")
                .Columns.Add("Nombre_Asegurado")
                .Columns.Add("Salario_Base")
                .Columns.Add("Tipo_Trabaja")
                .Columns.Add("Tipo_Salario")
                .Columns.Add("Jornada_Reducida")
                .Columns.Add("Fecha_Mov")
                .Columns.Add("UMF")
                .Columns.Add("Matriula")
            End With
            For Each dr As DataGridViewRow In Me.TablaAltas.Rows
                DT.Rows.Add(dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(16).Value, Trim(dr.Cells(6).Value) & " " & Trim(dr.Cells(4).Value) & " " & Trim(dr.Cells(5).Value),
                            dr.Cells(7).Value, dr.Cells(8).Value, dr.Cells(9).Value, dr.Cells(10).Value, dr.Cells(11).Value, dr.Cells(12).Value, dr.Cells(15).Value)
            Next
            Dim Cr As New AltasIMSS
            Cr.SetDataSource(DT)
            Dim Numero_Patron As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim RptIMSS As New CrystalDecisions.Shared.ParameterValues()
            Numero_Patron.Value = "REG.PAT-DV"
            RptIMSS.Add(Numero_Patron)
            Cr.DataDefinition.ParameterFields("Nombre_Patron").ApplyCurrentValues(RptIMSS)

            Dim Guia As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim G As New CrystalDecisions.Shared.ParameterValues()
            Guia.Value = Me.TablaAltas.Item(14, 0).Value.ToString.Substring(0, 2) & "-" & Me.TablaAltas.Item(14, 0).Value.ToString.Substring(2, 3)
            G.Add(Guia)
            Cr.DataDefinition.ParameterFields("Guia").ApplyCurrentValues(G)

            If nom_impresora Like "*PDF*" Then
                Dim rfcNombre As String = Eventos.ObtenerValorDB("Clientes", "RFC", " ID_CLIENTE= " & Me.lstCliente.SelectItem & "", 1)
                Dim DOptions As Global.CrystalDecisions.Shared.DiskFileDestinationOptions
                Dim EOptions As Global.CrystalDecisions.Shared.ExportOptions
                DOptions = CrystalDecisions.Shared.ExportOptions.CreateDiskFileDestinationOptions
                DOptions.DiskFileName = "C:\Program Files\Contable\SetupProyectoContable\Altas\" & rfcNombre & "\" & rfcNombre & "-Reingresos.PDF"
                EOptions = New CrystalDecisions.Shared.ExportOptions
                EOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                EOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                EOptions.ExportFormatOptions = CrystalDecisions.Shared.ExportOptions.CreatePdfRtfWordFormatOptions
                EOptions.ExportDestinationOptions = DOptions
                Cr.Export(EOptions)
                RadMessageBox.Show("Archivo Generado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Else
                Cr.PrintOptions.PrinterName = nom_impresora
                Cr.PrintToPrinter(1, False, 1, 1)
            End If
        ElseIf Trim(Me.TablaAltas.Item(13, 0).Value) = "02" Then ' bajas

            With DT
                .Columns.Add("Num_Seg_Socila")
                .Columns.Add("Dv")
                .Columns.Add("Nombre_Asegurado")
                .Columns.Add("Causa_Baja")
                .Columns.Add("Fecha_Mov")
                .Columns.Add("Matriula")
            End With
            For Each dr As DataGridViewRow In Me.TablaAltas.Rows
                DT.Rows.Add(dr.Cells(2).Value, dr.Cells(3).Value, Trim(dr.Cells(6).Value) & " " & Trim(dr.Cells(4).Value) & " " & Trim(dr.Cells(5).Value),
                            dr.Cells(18).Value, dr.Cells(11).Value, dr.Cells(15).Value)
            Next
            Dim Cr As New BajasIMSSS
            Cr.SetDataSource(DT)
            Dim Numero_Patron As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim RptIMSS As New CrystalDecisions.Shared.ParameterValues()
            Numero_Patron.Value = "REG.PAT-DV"
            RptIMSS.Add(Numero_Patron)
            Cr.DataDefinition.ParameterFields("Numero_Patron").ApplyCurrentValues(RptIMSS)

            Dim Guia As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim G As New CrystalDecisions.Shared.ParameterValues()
            Guia.Value = Me.TablaAltas.Item(14, 0).Value.ToString.Substring(0, 2) & "-" & Me.TablaAltas.Item(14, 0).Value.ToString.Substring(2, 3)
            G.Add(Guia)
            Cr.DataDefinition.ParameterFields("Guia").ApplyCurrentValues(G)

            If nom_impresora Like "*PDF*" Then
                Dim rfcNombre As String = Eventos.ObtenerValorDB("Clientes", "RFC", " ID_CLIENTE= " & Me.lstCliente.SelectItem & "", 1)
                Dim DOptions As Global.CrystalDecisions.Shared.DiskFileDestinationOptions
                Dim EOptions As Global.CrystalDecisions.Shared.ExportOptions
                DOptions = CrystalDecisions.Shared.ExportOptions.CreateDiskFileDestinationOptions
                DOptions.DiskFileName = "C:\Program Files\Contable\SetupProyectoContable\Altas\" & rfcNombre & "\" & rfcNombre & "-Bajas.PDF"
                EOptions = New CrystalDecisions.Shared.ExportOptions
                EOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                EOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                EOptions.ExportFormatOptions = CrystalDecisions.Shared.ExportOptions.CreatePdfRtfWordFormatOptions
                EOptions.ExportDestinationOptions = DOptions
                Cr.Export(EOptions)
                RadMessageBox.Show("Archivo Generado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Else
                Cr.PrintOptions.PrinterName = nom_impresora
                Cr.PrintToPrinter(1, False, 1, 1)
            End If

        ElseIf Me.TablaAltas.Item(13, 0).Value = "07" Then
            With DT ' Modificacion Salarios
                .Columns.Add("Num_Seg_Socila")
                .Columns.Add("Dv")
                .Columns.Add("CURP")
                .Columns.Add("Nombre_Asegurado")
                .Columns.Add("Salario_Base")
                .Columns.Add("Tipo_Trabaja")
                .Columns.Add("Tipo_Salario")
                .Columns.Add("Jornada_Reducida")
                .Columns.Add("Fecha_Mov")
                .Columns.Add("UMF")
                .Columns.Add("Matriula")
            End With
            For Each dr As DataGridViewRow In Me.TablaAltas.Rows
                DT.Rows.Add(dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(16).Value, Trim(dr.Cells(6).Value) & " " & Trim(dr.Cells(4).Value) & " " & Trim(dr.Cells(5).Value),
                            dr.Cells(7).Value, dr.Cells(8).Value, dr.Cells(9).Value, dr.Cells(10).Value, dr.Cells(11).Value, dr.Cells(12).Value, dr.Cells(15).Value)
            Next

            Dim Cr As New SalariosIMSS
            Cr.SetDataSource(DT)
            Dim Numero_Patron As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim RptIMSS As New CrystalDecisions.Shared.ParameterValues()
            Numero_Patron.Value = "REG.PAT-DV"
            RptIMSS.Add(Numero_Patron)
            Cr.DataDefinition.ParameterFields("Nombre_Patron").ApplyCurrentValues(RptIMSS)

            Dim Guia As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim G As New CrystalDecisions.Shared.ParameterValues()
            Guia.Value = Me.TablaAltas.Item(14, 0).Value.ToString.Substring(0, 2) & "-" & Me.TablaAltas.Item(14, 0).Value.ToString.Substring(2, 3)
            G.Add(Guia)
            Cr.DataDefinition.ParameterFields("Guia").ApplyCurrentValues(G)






            If nom_impresora Like "*PDF*" Then
                Dim rfcNombre As String = Eventos.ObtenerValorDB("Clientes", "RFC", " ID_CLIENTE= " & Me.lstCliente.SelectItem & "", 1)
                Dim DOptions As Global.CrystalDecisions.Shared.DiskFileDestinationOptions
                Dim EOptions As Global.CrystalDecisions.Shared.ExportOptions
                DOptions = CrystalDecisions.Shared.ExportOptions.CreateDiskFileDestinationOptions
                DOptions.DiskFileName = "C:\Program Files\Contable\SetupProyectoContable\Altas\" & rfcNombre & "\" & rfcNombre & "-ModifSalarios.PDF"
                EOptions = New CrystalDecisions.Shared.ExportOptions
                EOptions.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
                EOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
                EOptions.ExportFormatOptions = CrystalDecisions.Shared.ExportOptions.CreatePdfRtfWordFormatOptions
                EOptions.ExportDestinationOptions = DOptions
                Cr.Export(EOptions)
                RadMessageBox.Show("Archivo Generado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Else
                Cr.PrintOptions.PrinterName = nom_impresora
                Cr.PrintToPrinter(1, False, 1, 1)
            End If


        End If

    End Sub

    Private Sub Leector_de_Archivo_IMSS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_clientes()
        Eventos.DiseñoTabla(Me.TablaAltas)
    End Sub
    Private Sub Cargar_clientes()
        Me.lstCliente.Cargar(" SELECT Id_Empresa, Razon_social FROM Empresa ")
        Me.lstCliente.SelectItem = 1
    End Sub

    'Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
    '    'Eventos.Abrir_Manual("Clientes IDSE")
    'End Sub
End Class