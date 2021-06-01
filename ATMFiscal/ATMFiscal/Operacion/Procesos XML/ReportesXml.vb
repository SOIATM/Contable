Imports System.Linq
Imports System
Imports System.Collections.Generic
Imports System.Reflection
Imports Microsoft.Office.Interop
Imports System.ComponentModel
Imports Telerik.WinControls
Public Class ReportesXml
    Dim anio = Str(DateTime.Now.Year)
    Dim m = Now.Date.Month.ToString
    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Dim DsF As DataSet
    Dim Pgo As New List(Of Pagos)
    Dim NsC As New List(Of Notas)
    Dim Factus As New List(Of Facturas)
    Dim DetallePue As New List(Of TotalesDetallePUE)
    Dim DetalleNotas As New List(Of TotalesDetalleNota)
    Dim DetallePuePAGO As New List(Of TotalesDetallePagos)
    Dim DetallePuePPD As New List(Of TotalesDetallePPD)
    Dim DetalleUsos As New List(Of TotalesUsos)
    Dim DetalleUsosPPD As New List(Of TotalesUsosPPD)
    Dim DetalleUsosP As New List(Of TotalesUsosPagos)
    Dim DetalleUsosNotas As New List(Of TotalesNotas)
    Dim DetalleM As New List(Of TotalesMe)
    Dim DetalleMNotas As New List(Of TotalesMeNotas)
    Dim DetalleMPPD As New List(Of TotalesMePPD)
    Dim DetalleMP As New List(Of TotalesMePagos)
    Dim Negrita_verde As New DataGridViewCellStyle
    Dim Negrita_morado As New DataGridViewCellStyle
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub
    Private Sub Consulatar()
        Dim SQL As String = ""
        Dim Ds As DataSet

        If Me.Tabla.RowCount > 0 Then
            Me.Tabla.Rows.Clear()
            If Pgo.Count > 0 Then
                Pgo.Clear()
            End If
            If Factus.Count > 0 Then
                Factus.Clear()
            End If
        End If

        Dim Consulta As String
        If Me.RadE.Checked = True Then
            Consulta = "Xml_Sat.RFC_Receptor ,Xml_Sat.Nombre_Receptor"
            Me.Tabla.Columns(RFC_Receptor.Index).HeaderText = "RFC Receptor"
            Me.Tabla.Columns(Nombre_Receptor.Index).HeaderText = "Nombre Receptor"
        Else
            Consulta = "Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor"
            Me.Tabla.Columns(RFC_Receptor.Index).HeaderText = "RFC Emisor"
            Me.Tabla.Columns(Nombre_Receptor.Index).HeaderText = "Nombre Emisor"
        End If
#Region "Facturar PPD"
        SQL = " SELECT DISTINCT  * FROM ("
        SQL &= " SELECT  Xml_Sat.Estado_SAT, Xml_Sat.Tipo ,Xml_Sat.Fecha_Emision ,Xml_Sat.Fecha_Timbrado ,Xml_Sat.Serie ,Xml_Sat.Folio,  "
        SQL &= " Xml_Sat.UUID,Xml_Sat.Conceptos  ," & Consulta & " ,Xml_Sat.UsoCFDI ,Xml_Sat.SubTotal ,"
        SQL &= " Xml_Sat.Descuento , Xml_Sat.Total_IEPS ,Xml_Sat.IVA_16 ,Xml_Sat.IVA_8  ,Xml_Sat.Retenido_IVA ,"
        SQL &= " Xml_Sat.Retenido_ISR ,Xml_Sat.ISH ,Xml_Sat.Total, Xml_Sat.FormaDePago ,Xml_Sat.Metodo_de_Pago "
        SQL &= " FROM Xml_Sat LEFT OUTER JOIN PagosXML ON PagosXML.UUID = Xml_Sat.UUID  "
        SQL &= " WHERE ( Fecha_Timbrado >= " & Eventos.Sql_hoy(Me.DtInicio.Value) & " and Fecha_Timbrado <= " & Eventos.Sql_hoy(Dtfin.Value) & "  )   "
        SQL &= " And Xml_Sat.Id_Empresa = " & Me.lstCliente.SelectItem & " And Xml_Sat.Metodo_de_Pago Like '%PPD%' AND  Xml_Sat.Tipo ='Factura' and  Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " AND Xml_Sat.Estado_SAT <> 'Cancelado' "
        SQL &= " UNION "
        SQL &= " SELECT  Xml_Sat.Estado_SAT, Xml_Sat.Tipo ,Xml_Sat.Fecha_Emision ,Xml_Sat.Fecha_Timbrado ,"
        SQL &= " Xml_Sat.Serie ,Xml_Sat.Folio,  Xml_Sat.UUID,Xml_Sat.Conceptos  ," & Consulta & ",Xml_Sat.UsoCFDI ,Xml_Sat.SubTotal ,Xml_Sat.Descuento , "
        SQL &= " Xml_Sat.Total_IEPS ,Xml_Sat.IVA_16 ,Xml_Sat.IVA_8  ,Xml_Sat.Retenido_IVA ,"
        SQL &= " Xml_Sat.Retenido_ISR ,Xml_Sat.ISH ,Xml_Sat.Total, Xml_Sat.FormaDePago ,Xml_Sat.Metodo_de_Pago  "
        SQL &= " FROM Xml_Sat "
        SQL &= " RIGHT OUTER  JOIN PagosXML    ON PagosXML.UUID = Xml_Sat.UUID "
        SQL &= " WHERE   Xml_Sat.Id_Empresa =" & Me.lstCliente.SelectItem & " AND Xml_Sat.Metodo_de_Pago  LIKE '%PPD%' AND  Xml_Sat.Tipo ='Factura'  and  Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & "  AND Xml_Sat.Estado_SAT <> 'Cancelado' "
        SQL &= "  ) AS Tabla "
        Ds = Eventos.Obtener_DS(SQL)
        If Ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = Ds.Tables(0).Rows.Count
            Me.LblFacturasPPD.Text = "Total de Facturas encontradas: " & Ds.Tables(0).Rows.Count
            DsF = Ds
        End If


#End Region
    End Sub

    Private Sub Calcular1(ByVal ds As DataSet)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Try
            Factus.Clear()
        Catch ex As Exception

        End Try
        Try
            NsC.Clear()
        Catch ex As Exception

        End Try
        Try
            Pgo.Clear()
        Catch ex As Exception

        End Try
        Dim Pa As DataSet
        Dim NC As DataSet
        RadMessageBox.Show("Este Proceso puede tardar dependiendo de la información, presione Aceptar y espere a que el proceso termine...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Barra.Minimum = 0
        frm.Text = "Calculando Facturas por cobrar por favor espere..."
        frm.Barra.Maximum = Me.Tabla.RowCount - 1
        For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim Fila As DataGridViewRow = Me.Tabla.Rows(j)
            Me.Tabla.Item(Estado_SAT.Index, j).Value = ds.Tables(0).Rows(j)("Estado_SAT")
            Me.Tabla.Item(Tipo.Index, j).Value = ds.Tables(0).Rows(j)("Tipo")
            Me.Tabla.Item(Fecha_Emision.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Emision")
            Me.Tabla.Item(Fecha_Timbrado.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Timbrado")
            Me.Tabla.Item(Serie.Index, j).Value = ds.Tables(0).Rows(j)("Serie")
            Me.Tabla.Item(UUID.Index, j).Value = ds.Tables(0).Rows(j)("UUID")
            Me.Tabla.Item(Folio.Index, j).Value = ds.Tables(0).Rows(j)("Folio")
            Me.Tabla.Item(Conceptos.Index, j).Value = ds.Tables(0).Rows(j)("Conceptos")
            Me.Tabla.Item(RFC_Receptor.Index, j).Value = ds.Tables(0).Rows(j)(8)
            Me.Tabla.Item(Nombre_Receptor.Index, j).Value = ds.Tables(0).Rows(j)(9)
            Me.Tabla.Item(UsoCFDI.Index, j).Value = Trim(ds.Tables(0).Rows(j)("UsoCFDI"))
            Me.Tabla.Item(SubTotal.Index, j).Value = ds.Tables(0).Rows(j)("SubTotal")
            Me.Tabla.Item(Descuento.Index, j).Value = ds.Tables(0).Rows(j)("Descuento")
            Me.Tabla.Item(Total_IEPS.Index, j).Value = ds.Tables(0).Rows(j)("Total_IEPS")
            Me.Tabla.Item(IVA_16.Index, j).Value = ds.Tables(0).Rows(j)("IVA_16")
            Me.Tabla.Item(IVA_8.Index, j).Value = ds.Tables(0).Rows(j)("IVA_8")
            Me.Tabla.Item(Retenido_IVA.Index, j).Value = ds.Tables(0).Rows(j)("Retenido_IVA")
            Me.Tabla.Item(Retenido_ISR.Index, j).Value = Trim(ds.Tables(0).Rows(j)("Retenido_ISR"))
            Me.Tabla.Item(ISH.Index, j).Value = ds.Tables(0).Rows(j)("ISH")
            Me.Tabla.Item(Total.Index, j).Value = ds.Tables(0).Rows(j)("Total")
            Me.Tabla.Item(FormaDePago.Index, j).Value = ds.Tables(0).Rows(j)("FormaDePago")
            Me.Tabla.Item(Metodo_de_Pago.Index, j).Value = ds.Tables(0).Rows(j)("Metodo_de_Pago")

            Dim Consulta As String
            If Me.RadE.Checked = True Then
                Consulta = "Xml_Sat.RFC_Emisor ,Xml_Sat.Nombre_Emisor"
                'Me.Tabla.Item()
            Else
                Consulta = "Xml_Sat.RFC_Receptor ,Xml_Sat.Nombre_Receptor"
            End If
            NC = Eventos.Obtener_DS(" Select Xml_Sat.Estado_SAT, Xml_Sat.Tipo ,Xml_Sat.Fecha_Emision ,Xml_Sat.Fecha_Timbrado , 
	Xml_Sat.Serie ,Xml_Sat.Folio,  	  Xml_Sat.UUID,Xml_Sat.UUID_Relacion ,Xml_Sat.Conceptos  ," & Consulta & ",Xml_Sat.UsoCFDI ,Xml_Sat.SubTotal ,
	  Xml_Sat.Descuento ,  Xml_Sat.Total_IEPS ,Xml_Sat.IVA_16 ,Xml_Sat.IVA_8  ,Xml_Sat.Retenido_IVA , Xml_Sat.Retenido_ISR ,Xml_Sat.ISH ,
	  Xml_Sat.Total, Xml_Sat.FormaDePago ,Xml_Sat.Metodo_de_Pago     FROM Xml_Sat WHERE   Xml_Sat.Id_Empresa =" & Me.lstCliente.SelectItem & " AND  Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " AND  Xml_Sat.Tipo ='NotaCredito' AND Xml_Sat.Estado_SAT <> 'Cancelado'
	  AND Xml_Sat.UUID_Relacion IN ( '" & Me.Tabla.Item(UUID.Index, j).Value & "') ORDER BY Xml_Sat.Fecha_Timbrado")
            If NC.Tables(0).Rows.Count > 0 Then

                For s As Integer = 0 To NC.Tables(0).Rows.Count - 1

                    NsC.Add(New Notas() With {.Estado_SAT = Trim(NC.Tables(0).Rows(s)("Estado_SAT")).ToString,
                            .Tipo = Trim(NC.Tables(0).Rows(s)("Tipo")).ToString, .Fecha_Emision = Trim(NC.Tables(0).Rows(s)("Fecha_Emision")).ToString,
                            .Fecha_Timbrado = Trim(NC.Tables(0).Rows(s)("Fecha_Timbrado")).ToString, .Serie = Trim(NC.Tables(0).Rows(s)("Serie")).ToString,
                            .UUID = Trim(NC.Tables(0).Rows(s)("UUID")).ToString, .UUIDR = Trim(NC.Tables(0).Rows(s)("UUID_Relacion")).ToString,
                            .Folio = Trim(NC.Tables(0).Rows(s)("Folio")).ToString,
                            .Conceptos = Trim(NC.Tables(0).Rows(s)("Conceptos")).ToString,
                            .RFC_Receptor = Trim(NC.Tables(0).Rows(s)(9)).ToString,
                            .Nombre_Receptor = Trim(NC.Tables(0).Rows(s)(10)).ToString,
                            .UsoCFDI = Trim(NC.Tables(0).Rows(s)("UsoCFDI")).ToString,
                            .SubTotal = Format(NC.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"),
                            .Descuento = Format(NC.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                            .Total_IEPS = Format(NC.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"),
                            .IVA_16 = Format(NC.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                            .IVA_8 = Format(NC.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"),
                            .Retenido_IVA = Format(NC.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                            .Retenido_ISR = Format(NC.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"),
                            .ISH = Format(NC.Tables(0).Rows(s)("ISH"), "$ #,##0.00"), .Total = Format(NC.Tables(0).Rows(s)("Total"), "$ #,##0.00"),
                            .FormaDePago = Trim(NC.Tables(0).Rows(s)("FormaDePago")).ToString, .Metodo_de_Pago = Trim(NC.Tables(0).Rows(s)("Metodo_de_Pago")).ToString
                              })
                Next
            End If



            Pa = Eventos.Obtener_DS("SELECT Xml_Complemento.UUID  ,Xml_Complemento.FechaPago ,Xml_Complemento.FormaDePago ,Xml_Complemento.Monto,Xml_Complemento.IdDocumento ,Xml_Complemento.NumParcialidad ,Xml_Complemento.ImpSaldoAnt ,Xml_Complemento.ImpPagado , Xml_Complemento.ImpSaldoInsoluto       FROM Xml_Complemento WHERE Xml_Complemento.IdDocumento   = '" & Me.Tabla.Item(UUID.Index, j).Value & "' ORDER BY Xml_Complemento.NumParcialidad ")
            If Pa.Tables(0).Rows.Count > 0 Then
                Dim Porcen As Decimal = 0
                For s As Integer = 0 To Pa.Tables(0).Rows.Count - 1
                    Porcen = (Pa.Tables(0).Rows(s)("ImpPagado") / Me.Tabla.Item(Total.Index, j).Value)
                    Pgo.Add(New Pagos() With {.UUID = Trim(Pa.Tables(0).Rows(s)("UUID")).ToString,
                            .Fecha = Trim(Pa.Tables(0).Rows(s)("FechaPago")).ToString, .Forma = Trim(Pa.Tables(0).Rows(s)("FormaDePago")).ToString,
                            .Monto = Format(Pa.Tables(0).Rows(s)("ImpPagado"), "$ #,##0.00"), .UUIDR = Trim(Pa.Tables(0).Rows(s)("IdDocumento")).ToString,
                            .NumPar = Trim(Pa.Tables(0).Rows(s)("NumParcialidad")).ToString, .ImporteAnterior = Format(Pa.Tables(0).Rows(s)("ImpSaldoAnt"), "$ #,##0.00"),
                            .ImportePagado = Format(Pa.Tables(0).Rows(s)("ImpPagado"), "$ #,##0.00"), .ImporteSaldoInsoluto = Format(Pa.Tables(0).Rows(s)("ImpSaldoInsoluto"), "$ #,##0.00"), .SubTotal = Format(Me.Tabla.Item(SubTotal.Index, j).Value * Porcen, "$ #,##0.00"),
                            .Descuento = Format(Me.Tabla.Item(Descuento.Index, j).Value * Porcen, "$ #,##0.00"), .Total_IEPS = Format(Me.Tabla.Item(Total_IEPS.Index, j).Value * Porcen, "$ #,##0.00"),
                            .IVA16 = Format(Me.Tabla.Item(IVA_16.Index, j).Value * Porcen, "$ #,##0.00"), .IVA8 = Format(Me.Tabla.Item(IVA_8.Index, j).Value * Porcen, "$ #,##0.00"),
                            .Retenido_IVA = Format(Me.Tabla.Item(Retenido_IVA.Index, j).Value * Porcen, "$ #,##0.00"), .Retenido_ISR = Format(Me.Tabla.Item(Retenido_ISR.Index, j).Value * Porcen, "$ #,##0.00"),
                            .ISH = Format(Me.Tabla.Item(ISH.Index, j).Value * Porcen, "$ #,##0.00")})
                    '#,##0.00;[Rojo]-#,##0.00
                Next
            End If
            Me.Tabla.Item(TotalNotas.Index, j).Value = SaldosNsC(ds.Tables(0).Rows(j)("UUID"))
            Me.Tabla.Item(TotalCobros.Index, j).Value = Saldos(ds.Tables(0).Rows(j)("UUID"))
            Me.Tabla.Item(Diferencia.Index, j).Value = Convert.ToDecimal(Me.Tabla.Item(Total.Index, j).Value) - Convert.ToDecimal(Me.Tabla.Item(TotalNotas.Index, j).Value) - Convert.ToDecimal(Me.Tabla.Item(TotalCobros.Index, j).Value)
            If Me.Tabla.Item(Diferencia.Index, j).Value < 0 Then
                Me.Tabla.Item(Diferencia.Index, j).Style.ForeColor = Color.Red
            ElseIf Me.Tabla.Item(Diferencia.Index, j).Value > 0 Then
                Me.Tabla.Item(Diferencia.Index, j).Style.ForeColor = Color.DarkBlue
            End If
            Factus.Add(New Facturas() With {.Estado_SAT = Me.Tabla.Item(Estado_SAT.Index, j).Value,
                            .Tipo = Me.Tabla.Item(Tipo.Index, j).Value, .Fecha_Emision = Me.Tabla.Item(Fecha_Emision.Index, j).Value,
                            .Fecha_Timbrado = Me.Tabla.Item(Fecha_Timbrado.Index, j).Value, .Serie = Me.Tabla.Item(Serie.Index, j).Value,
                            .UUID = Me.Tabla.Item(UUID.Index, j).Value, .Folio = Me.Tabla.Item(Folio.Index, j).Value,
                            .Conceptos = Me.Tabla.Item(Conceptos.Index, j).Value, .RFC_Receptor = Me.Tabla.Item(RFC_Receptor.Index, j).Value, .Nombre_Receptor = Me.Tabla.Item(Nombre_Receptor.Index, j).Value,
                            .UsoCFDI = Me.Tabla.Item(UsoCFDI.Index, j).Value, .SubTotal = Format(Me.Tabla.Item(SubTotal.Index, j).Value, "$ #,##0.00"),
                            .Descuento = Format(Me.Tabla.Item(Descuento.Index, j).Value, "$ #,##0.00"), .Total_IEPS = Format(Me.Tabla.Item(Total_IEPS.Index, j).Value, "$ #,##0.00"),
                            .IVA_16 = Format(Me.Tabla.Item(IVA_16.Index, j).Value, "$ #,##0.00"), .IVA_8 = Format(Me.Tabla.Item(IVA_8.Index, j).Value, "$ #,##0.00"),
                            .Retenido_IVA = Format(Me.Tabla.Item(Retenido_IVA.Index, j).Value, "$ #,##0.00"), .Retenido_ISR = Format(Me.Tabla.Item(Retenido_ISR.Index, j).Value, "$ #,##0.00"),
                            .ISH = Format(Me.Tabla.Item(ISH.Index, j).Value, "$ #,##0.00"), .Total = Format(Me.Tabla.Item(Total.Index, j).Value, "$ #,##0.00"),
                            .FormaDePago = Me.Tabla.Item(FormaDePago.Index, j).Value, .Metodo_de_Pago = Me.Tabla.Item(Metodo_de_Pago.Index, j).Value,
                            .TotalCobros = Format(Me.Tabla.Item(TotalCobros.Index, j).Value, "$ #,##0.00"), .TotalNotas = Format(Me.Tabla.Item(TotalNotas.Index, j).Value, "$ #,##0.00"), .Diferencia = Format(Me.Tabla.Item(Diferencia.Index, j).Value, "$ #,##0.00")})

            frm.Barra.Value = j
        Next
        frm.Close()
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
    End Sub
    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        Me.Tabla.Enabled = False
        Consulatar()
        SegundoPlano.RunWorkerAsync(Me.Tabla)
        Control.CheckForIllegalCrossThreadCalls = False
        Me.Tabla.Enabled = True
    End Sub
    Private Sub Calcula2()
        If Me.Tabla2.RowCount > 0 Then
            Me.Tabla2.Rows.Clear()
        End If
        Me.Tabla2.RowCount = 13
    End Sub

    Private Sub Calcular2()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Try
            DetallePue.Clear()
            DetalleNotas.Clear()
            DetallePuePAGO.Clear()
            DetallePuePPD.Clear()

        Catch ex As Exception

        End Try
        Dim tot As New Totales
        Dim ds As DataSet
        Dim consulta As String = ""
        Dim SQL As String
        For i As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
#Region "PAGOS PUE"

            Me.Tabla2.Item(Mes.Index, i - 1).Value = Eventos.MesEnletra(IIf(Len(i.ToString) = 1, "0" & i.ToString(), i.ToString()))
            consulta = "   Select    Sum(Xml_Sat.SubTotal) As SubTotal ,Sum(Xml_Sat.Descuento ) As Descuento ,Sum(Xml_Sat.Total_IEPS) As Total_IEPS ,"
            consulta &= "Sum(Xml_Sat.IVA_16) As IVA_16 ,Sum(Xml_Sat.IVA_8) As IVA_8 ,"
            consulta &= "Sum(Xml_Sat.Retenido_IVA) As Retenido_IVA ,Sum(Xml_Sat.Retenido_ISR) As Retenido_ISR ,"
            consulta &= "Sum(Xml_Sat.ISH ) As ISH ,Sum(Xml_Sat.Total ) As Total"
            consulta &= " FROM Xml_Sat WHERE Tipo='Factura' and (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & i & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
            consulta &= "  AND Xml_Sat.Metodo_de_Pago  LIKE '%PUE%'  and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "


            ds = Eventos.Obtener_DS(consulta)
            If ds.Tables(0).Rows.Count > 0 Then
                For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Me.Tabla2.Item(PUEGravado.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("SubTotal")), 0, ds.Tables(0).Rows(j)("SubTotal"))
                    tot.PUE_Subtotal += Convert.ToDecimal(Me.Tabla2.Item(PUEGravado.Index, i - 1).Value)

                    Me.Tabla2.Item(Exento.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Descuento")), 0, ds.Tables(0).Rows(j)("Descuento"))
                    tot.PUE_Descuento += Convert.ToDecimal(Me.Tabla2.Item(Exento.Index, i - 1).Value)

                    Me.Tabla2.Item(IEPS1.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Total_IEPS")), 0, ds.Tables(0).Rows(j)("Total_IEPS"))
                    tot.PUE_Ieps += Convert.ToDecimal(Me.Tabla2.Item(IEPS1.Index, i - 1).Value)

                    Me.Tabla2.Item(IVA81.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("IVA_8")), 0, ds.Tables(0).Rows(j)("IVA_8"))
                    tot.PUE_Iva8 += Convert.ToDecimal(Me.Tabla2.Item(IVA81.Index, i - 1).Value)

                    Me.Tabla2.Item(IVA.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("IVA_16")), 0, ds.Tables(0).Rows(j)("IVA_16"))
                    tot.PUE_Iva16 += Convert.ToDecimal(Me.Tabla2.Item(IVA.Index, i - 1).Value)

                    Me.Tabla2.Item(RI1.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Retenido_IVA")), 0, ds.Tables(0).Rows(j)("Retenido_IVA"))
                    tot.PUE_Riva += Convert.ToDecimal(Me.Tabla2.Item(RI1.Index, i - 1).Value)

                    Me.Tabla2.Item(RISR1.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Retenido_ISR")), 0, ds.Tables(0).Rows(j)("Retenido_ISR"))
                    tot.PUE_Risr += Convert.ToDecimal(Me.Tabla2.Item(RISR1.Index, i - 1).Value)

                    Me.Tabla2.Item(ISH1.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("ISH")), 0, ds.Tables(0).Rows(j)("ISH"))
                    tot.PUE_Ish += Convert.ToDecimal(Me.Tabla2.Item(ISH1.Index, i - 1).Value)

                    Me.Tabla2.Item(Totala.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Total")), 0, ds.Tables(0).Rows(j)("Total"))
                    tot.PUE_Tp += Convert.ToDecimal(Me.Tabla2.Item(Totala.Index, i - 1).Value)
                Next
                consulta = "   Select  Xml_Sat.UUID, Xml_Sat.UUID_Relacion, Xml_Sat.Nombre_Emisor as E, Xml_Sat.SubTotal  , Xml_Sat.Descuento   , Xml_Sat.Total_IEPS ,"
                consulta &= " Xml_Sat.IVA_16  , Xml_Sat.IVA_8  ,"
                consulta &= " Xml_Sat.Retenido_IVA,  Xml_Sat.Retenido_ISR  ,"
                consulta &= " Xml_Sat.ISH,  Xml_Sat.Total  "
                consulta &= " FROM Xml_Sat WHERE Tipo='Factura' and (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & i & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                consulta &= "  AND Xml_Sat.Metodo_de_Pago  LIKE '%PUE%'  and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                Dim pa = Eventos.Obtener_DS(consulta)
                If pa.Tables(0).Rows.Count > 0 Then
                    For s As Integer = 0 To pa.Tables(0).Rows.Count - 1

                        DetallePue.Add(New TotalesDetallePUE() With {.Mes = Me.Tabla2.Item(Mes.Index, i - 1).Value, .UUIDpue = pa.Tables(0).Rows(s)("UUID"), .Emisor = pa.Tables(0).Rows(s)("E"),
                                .PUE_Subtotal = Format(pa.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"), .PUE_Descuento = Format(pa.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                                .PUE_Ieps = Format(pa.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"), .PUE_Iva16 = Format(pa.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                                .PUE_Iva8 = Format(pa.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"), .PUE_Riva = Format(pa.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                                .PUE_Risr = Format(pa.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"), .PUE_Ish = Format(pa.Tables(0).Rows(s)("ISH"), "$ #,##0.00"),
                                .PUE_Tp = Format(pa.Tables(0).Rows(s)("Total"), "$ #,##0.00"), .Tipo = "PUE", .UUIDRel = pa.Tables(0).Rows(s)("UUID_Relacion")})
                    Next
                    DetallePue.Add(New TotalesDetallePUE() With {.Mes = Me.Tabla2.Item(Mes.Index, i - 1).Value, .UUIDpue = "TOTAL DEL MES", .Emisor = "",
                                .PUE_Subtotal = Format(Me.Tabla2.Item(PUEGravado.Index, i - 1).Value, "$ #,##0.00"), .PUE_Descuento = Format(Me.Tabla2.Item(Exento.Index, i - 1).Value, "$ #,##0.00"),
                                .PUE_Ieps = Format(Me.Tabla2.Item(IEPS1.Index, i - 1).Value, "$ #,##0.00"), .PUE_Iva16 = Format(Me.Tabla2.Item(IVA.Index, i - 1).Value, "$ #,##0.00"),
                                .PUE_Iva8 = Format(Me.Tabla2.Item(IVA81.Index, i - 1).Value, "$ #,##0.00"), .PUE_Riva = Format(Me.Tabla2.Item(RI1.Index, i - 1).Value, "$ #,##0.00"),
                                .PUE_Risr = Format(Me.Tabla2.Item(RISR1.Index, i - 1).Value, "$ #,##0.00"), .PUE_Ish = Format(Me.Tabla2.Item(ISH1.Index, i - 1).Value, "$ #,##0.00"),
                                .PUE_Tp = Format(Me.Tabla2.Item(Totala.Index, i - 1).Value, "$ #,##0.00"), .Tipo = "PUE", .UUIDRel = ""})
                End If

            End If
#End Region

#Region "Notas PUE"

            Me.Tabla2.Item(Mes.Index, i - 1).Value = Eventos.MesEnletra(IIf(Len(i.ToString) = 1, "0" & i.ToString(), i.ToString()))
            consulta = "   Select    Sum(Xml_Sat.SubTotal) As SubTotal ,Sum(Xml_Sat.Descuento ) As Descuento ,Sum(Xml_Sat.Total_IEPS) As Total_IEPS ,"
            consulta &= "Sum(Xml_Sat.IVA_16) As IVA_16 ,Sum(Xml_Sat.IVA_8) As IVA_8 ,"
            consulta &= "Sum(Xml_Sat.Retenido_IVA) As Retenido_IVA ,Sum(Xml_Sat.Retenido_ISR) As Retenido_ISR ,"
            consulta &= "Sum(Xml_Sat.ISH ) As ISH ,Sum(Xml_Sat.Total ) As Total"
            consulta &= " FROM Xml_Sat WHERE Tipo='NotaCredito' and (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & i & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
            consulta &= "  AND Xml_Sat.Metodo_de_Pago  LIKE '%PUE%'  and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "


            ds = Eventos.Obtener_DS(consulta)
            If ds.Tables(0).Rows.Count > 0 Then
                For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Me.Tabla2.Item(NotaSub.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("SubTotal")), 0, ds.Tables(0).Rows(j)("SubTotal"))
                    tot.N_Subtotal += Convert.ToDecimal(Me.Tabla2.Item(NotaSub.Index, i - 1).Value)

                    Me.Tabla2.Item(DesNota.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Descuento")), 0, ds.Tables(0).Rows(j)("Descuento"))
                    tot.N_Descuento += Convert.ToDecimal(Me.Tabla2.Item(DesNota.Index, i - 1).Value)

                    Me.Tabla2.Item(NotaIEPS.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Total_IEPS")), 0, ds.Tables(0).Rows(j)("Total_IEPS"))
                    tot.N_Ieps += Convert.ToDecimal(Me.Tabla2.Item(NotaIEPS.Index, i - 1).Value)

                    Me.Tabla2.Item(NotaIVA8.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("IVA_8")), 0, ds.Tables(0).Rows(j)("IVA_8"))
                    tot.N_Iva8 += Convert.ToDecimal(Me.Tabla2.Item(NotaIVA8.Index, i - 1).Value)

                    Me.Tabla2.Item(NotaIVA16.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("IVA_16")), 0, ds.Tables(0).Rows(j)("IVA_16"))
                    tot.N_Iva16 += Convert.ToDecimal(Me.Tabla2.Item(NotaIVA16.Index, i - 1).Value)

                    Me.Tabla2.Item(NOTARIVA.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Retenido_IVA")), 0, ds.Tables(0).Rows(j)("Retenido_IVA"))
                    tot.N_Riva += Convert.ToDecimal(Me.Tabla2.Item(NOTARIVA.Index, i - 1).Value)

                    Me.Tabla2.Item(NotaRISR.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Retenido_ISR")), 0, ds.Tables(0).Rows(j)("Retenido_ISR"))
                    tot.N_Risr += Convert.ToDecimal(Me.Tabla2.Item(NotaRISR.Index, i - 1).Value)

                    Me.Tabla2.Item(NotaISH.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("ISH")), 0, ds.Tables(0).Rows(j)("ISH"))
                    tot.N_Ish += Convert.ToDecimal(Me.Tabla2.Item(NotaISH.Index, i - 1).Value)

                    Me.Tabla2.Item(NotaTotal.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Total")), 0, ds.Tables(0).Rows(j)("Total"))
                    tot.N_Tp += Convert.ToDecimal(Me.Tabla2.Item(NotaTotal.Index, i - 1).Value)
                Next
                consulta = "   Select  Xml_Sat.UUID,Xml_Sat.UUID_Relacion, Xml_Sat.Nombre_Emisor as E, Xml_Sat.SubTotal  , Xml_Sat.Descuento   , Xml_Sat.Total_IEPS ,"
                consulta &= " Xml_Sat.IVA_16  , Xml_Sat.IVA_8  ,"
                consulta &= " Xml_Sat.Retenido_IVA,  Xml_Sat.Retenido_ISR  ,"
                consulta &= " Xml_Sat.ISH,  Xml_Sat.Total  "
                consulta &= " FROM Xml_Sat WHERE Tipo='NotaCredito' and (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & i & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                consulta &= "  AND Xml_Sat.Metodo_de_Pago  LIKE '%PUE%'  and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                Dim pa = Eventos.Obtener_DS(consulta)
                If pa.Tables(0).Rows.Count > 0 Then
                    For s As Integer = 0 To pa.Tables(0).Rows.Count - 1

                        DetalleNotas.Add(New TotalesDetalleNota() With {.Mes = Me.Tabla2.Item(Mes.Index, i - 1).Value, .UUIDpue = pa.Tables(0).Rows(s)("UUID"), .Emisor = pa.Tables(0).Rows(s)("E"),
                                .PUE_Subtotal = Format(pa.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"), .PUE_Descuento = Format(pa.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                                .PUE_Ieps = Format(pa.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"), .PUE_Iva16 = Format(pa.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                                .PUE_Iva8 = Format(pa.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"), .PUE_Riva = Format(pa.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                                .PUE_Risr = Format(pa.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"), .PUE_Ish = Format(pa.Tables(0).Rows(s)("ISH"), "$ #,##0.00"),
                                .PUE_Tp = Format(pa.Tables(0).Rows(s)("Total"), "$ #,##0.00"), .Tipo = "Nota", .UUIDRel = pa.Tables(0).Rows(s)("UUID_Relacion")})
                    Next
                    DetalleNotas.Add(New TotalesDetalleNota() With {.Mes = Me.Tabla2.Item(Mes.Index, i - 1).Value, .UUIDpue = "TOTAL DEL MES", .Emisor = "",
                                .PUE_Subtotal = Format(Me.Tabla2.Item(NotaSub.Index, i - 1).Value, "$ #,##0.00"), .PUE_Descuento = Format(Me.Tabla2.Item(DesNota.Index, i - 1).Value, "$ #,##0.00"),
                                .PUE_Ieps = Format(Me.Tabla2.Item(NotaIEPS.Index, i - 1).Value, "$ #,##0.00"), .PUE_Iva16 = Format(Me.Tabla2.Item(NotaIVA16.Index, i - 1).Value, "$ #,##0.00"),
                                .PUE_Iva8 = Format(Me.Tabla2.Item(NotaIVA8.Index, i - 1).Value, "$ #,##0.00"), .PUE_Riva = Format(Me.Tabla2.Item(NOTARIVA.Index, i - 1).Value, "$ #,##0.00"),
                                .PUE_Risr = Format(Me.Tabla2.Item(NotaRISR.Index, i - 1).Value, "$ #,##0.00"), .PUE_Ish = Format(Me.Tabla2.Item(NotaISH.Index, i - 1).Value, "$ #,##0.00"),
                                .PUE_Tp = Format(Me.Tabla2.Item(NotaTotal.Index, i - 1).Value, "$ #,##0.00"), .Tipo = "Nota", .UUIDRel = ""})
                End If

            End If
#End Region


#Region "Complementos de PAGO "
            ds.Clear()


            consulta = "SELECT    Sum(A.Monto ) AS Monto , Sum((A.Monto / B.Total) * B.SubTotal) AS SubTotal ,Sum((A.Monto / B.Total) * B.Descuento ) AS Descuento,Sum( (A.Monto / B.Total) * B.Total_IEPS ) AS [Total IEPS] ,"
            consulta &= " Sum((A.Monto / B.Total) * B.IVA_16) AS [IVA 16%] ,Sum((A.Monto / B.Total) * B.IVA_8 ) AS [ IVA 8%] , Sum((A.Monto / B.Total) * B.Retenido_IVA) AS [Retenido IVA] ,"
            consulta &= " Sum((A.Monto / B.Total) * B.Retenido_ISR) AS [Retenido ISR] ,Sum((A.Monto / B.Total) * B.ISH )AS ISH ,Sum( (A.Monto / B.Total) * B.Total)  AS Total"
            consulta &= " FROM (SELECT Xml_Complemento.IdDocumento AS UUID, Xml_Complemento.MONTO FROM Xml_Complemento "
            consulta &= " WHERE  (datepart(year, Xml_Complemento.FechaPago ) = " & Me.DtInicio.Value.Year & " AND datepart(month,Xml_Complemento.FechaPago) = " & i & " )  AND Id_Empresa =" & Me.lstCliente.SelectItem & " ) AS A "
            consulta &= "  LEFT OUTER JOIN Xml_Sat AS B ON B.uuid = A.UUID WHERE B.total IS NOT NULL AND B.Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & "  AND b.Id_Empresa =" & Me.lstCliente.SelectItem & " "

            ds = Eventos.Obtener_DS(consulta)
            If ds.Tables(0).Rows.Count > 0 Then
                For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Me.Tabla2.Item(PagosGravado.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("SubTotal")), 0, ds.Tables(0).Rows(j)("SubTotal"))
                    tot.P_Subtotal += Convert.ToDecimal(Me.Tabla2.Item(PagosGravado.Index, i - 1).Value)

                    Me.Tabla2.Item(Exentob.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Descuento")), 0, ds.Tables(0).Rows(j)("Descuento"))
                    tot.P_Descuento += Convert.ToDecimal(Me.Tabla2.Item(Exentob.Index, i - 1).Value)

                    Me.Tabla2.Item(IEPS2.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Total IEPS")), 0, ds.Tables(0).Rows(j)("Total IEPS"))
                    tot.P_Ieps += Convert.ToDecimal(Me.Tabla2.Item(IEPS2.Index, i - 1).Value)

                    Me.Tabla2.Item(IVA82.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)(" IVA 8%")), 0, ds.Tables(0).Rows(j)(" IVA 8%"))
                    tot.P_Iva8 += Convert.ToDecimal(Me.Tabla2.Item(IVA82.Index, i - 1).Value)

                    Me.Tabla2.Item(IVAb.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("IVA 16%")), 0, ds.Tables(0).Rows(j)("IVA 16%"))
                    tot.P_Iva16 += Convert.ToDecimal(Me.Tabla2.Item(IVAb.Index, i - 1).Value)

                    Me.Tabla2.Item(RI2.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Retenido IVA")), 0, ds.Tables(0).Rows(j)("Retenido IVA"))
                    tot.P_Riva += Convert.ToDecimal(Me.Tabla2.Item(RI2.Index, i - 1).Value)

                    Me.Tabla2.Item(RISR2.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Retenido ISR")), 0, ds.Tables(0).Rows(j)("Retenido ISR"))
                    tot.P_Risr += Convert.ToDecimal(Me.Tabla2.Item(RISR2.Index, i - 1).Value)

                    Me.Tabla2.Item(ISH2.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("ISH")), 0, ds.Tables(0).Rows(j)("ISH"))
                    tot.P_Ish += Convert.ToDecimal(Me.Tabla2.Item(ISH2.Index, i - 1).Value)

                    Me.Tabla2.Item(Totalb.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Total")), 0, ds.Tables(0).Rows(j)("Total"))
                    tot.P_Tp += Convert.ToDecimal(Me.Tabla2.Item(Totalb.Index, i - 1).Value)
                Next
                consulta = "SELECT     A.UUID,A.UUID_Pago , B.Nombre_Emisor as E, (A.Monto / B.Total) * B.SubTotal AS SubTotal  ,"
                consulta &= "(A.Monto / B.Total) * B.Descuento As Descuento , (A.Monto / B.Total) * B.Total_IEPS As Total_IEPS,"
                consulta &= " (A.Monto / B.Total) *  B.IVA_16 AS IVA_16 , (A.Monto / B.Total) * B.IVA_8 AS IVA_8 , "
                consulta &= " (A.Monto / B.Total) * B.Retenido_IVA As Retenido_IVA ,(A.Monto / B.Total) *  B.Retenido_ISR As Retenido_ISR"
                consulta &= "  , (A.Monto / B.Total) * B.ISH As ISH , (A.Monto / B.Total) * B.Total As Total  "
                consulta &= " FROM (SELECT Xml_Complemento.IdDocumento AS UUID,Xml_Complemento.UUID AS UUID_Pago, Xml_Complemento.MONTO FROM Xml_Complemento "
                consulta &= " WHERE  (datepart(year, Xml_Complemento.FechaPago ) = " & Me.DtInicio.Value.Year & " AND datepart(month,Xml_Complemento.FechaPago) = " & i & " )  AND Id_Empresa =" & Me.lstCliente.SelectItem & " ) AS A "
                consulta &= "  LEFT OUTER JOIN Xml_Sat AS B ON B.uuid = A.UUID WHERE B.total IS NOT NULL AND B.Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " AND b.Id_Empresa =" & Me.lstCliente.SelectItem & " "
                Dim pa = Eventos.Obtener_DS(consulta)
                If pa.Tables(0).Rows.Count > 0 Then
                    For s As Integer = 0 To pa.Tables(0).Rows.Count - 1

                        DetallePuePAGO.Add(New TotalesDetallePagos() With {.Mes = Me.Tabla2.Item(Mes.Index, i - 1).Value, .UUIDpue = pa.Tables(0).Rows(s)("UUID"), .Emisor = pa.Tables(0).Rows(s)("E"),
                                .PUE_Subtotal = Format(pa.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"), .PUE_Descuento = Format(pa.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                                .PUE_Ieps = Format(pa.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"), .PUE_Iva16 = Format(pa.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                                .PUE_Iva8 = Format(pa.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"), .PUE_Riva = Format(pa.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                                .PUE_Risr = Format(pa.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"), .PUE_Ish = Format(pa.Tables(0).Rows(s)("ISH"), "$ #,##0.00"),
                                .PUE_Tp = Format(pa.Tables(0).Rows(s)("Total"), "$ #,##0.00"), .Tipo = "PAGO", .UUIDRel = pa.Tables(0).Rows(s)("UUID_Pago")})
                    Next
                    DetallePuePAGO.Add(New TotalesDetallePagos() With {.Mes = Me.Tabla2.Item(Mes.Index, i - 1).Value, .UUIDpue = "TOTAL DEL MES", .Emisor = "",
                             .PUE_Subtotal = Format(Me.Tabla2.Item(PagosGravado.Index, i - 1).Value, "$ #,##0.00"), .PUE_Descuento = Format(Me.Tabla2.Item(Exentob.Index, i - 1).Value, "$ #,##0.00"),
                             .PUE_Ieps = Format(Me.Tabla2.Item(IEPS2.Index, i - 1).Value, "$ #,##0.00"), .PUE_Iva16 = Format(Me.Tabla2.Item(IVAb.Index, i - 1).Value, "$ #,##0.00"),
                             .PUE_Iva8 = Format(Me.Tabla2.Item(IVA82.Index, i - 1).Value, "$ #,##0.00"), .PUE_Riva = Format(Me.Tabla2.Item(RI2.Index, i - 1).Value, "$ #,##0.00"),
                             .PUE_Risr = Format(Me.Tabla2.Item(RISR2.Index, i - 1).Value, "$ #,##0.00"), .PUE_Ish = Format(Me.Tabla2.Item(ISH2.Index, i - 1).Value, "$ #,##0.00"),
                             .PUE_Tp = Format(Me.Tabla2.Item(Totalb.Index, i - 1).Value, "$ #,##0.00"), .Tipo = "PAGO", .UUIDRel = ""})
                End If

            End If




#End Region
#Region "PPD Facturas"


            ds.Clear()


            consulta = "   Select    Sum(Xml_Sat.SubTotal) As SubTotal ,Sum(Xml_Sat.Descuento ) As Descuento ,Sum(Xml_Sat.Total_IEPS) As Total_IEPS ,"
            consulta &= "Sum(Xml_Sat.IVA_16) As IVA_16 ,Sum(Xml_Sat.IVA_8) As IVA_8 ,"
            consulta &= "Sum(Xml_Sat.Retenido_IVA) As Retenido_IVA ,Sum(Xml_Sat.Retenido_ISR) As Retenido_ISR ,"
            consulta &= "Sum(Xml_Sat.ISH ) As ISH ,Sum(Xml_Sat.Total ) As Total"
            consulta &= " FROM Xml_Sat WHERE (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & i & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
            consulta &= "  AND Xml_Sat.Metodo_de_Pago  LIKE '%PPD%'  and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "

            ds = Eventos.Obtener_DS(consulta)
            If ds.Tables(0).Rows.Count > 0 Then
                For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Me.Tabla2.Item(TotalPPD.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("SubTotal")), 0, ds.Tables(0).Rows(j)("SubTotal"))
                    tot.PPD_Subtotal += Convert.ToDecimal(Me.Tabla2.Item(TotalPPD.Index, i - 1).Value)

                    Me.Tabla2.Item(Exentod.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Descuento")), 0, ds.Tables(0).Rows(j)("Descuento"))
                    tot.PPD_Descuento += Convert.ToDecimal(Me.Tabla2.Item(Exentod.Index, i - 1).Value)

                    Me.Tabla2.Item(IEPS4.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Total_IEPS")), 0, ds.Tables(0).Rows(j)("Total_IEPS"))
                    tot.PPD_Ieps += Convert.ToDecimal(Me.Tabla2.Item(IEPS4.Index, i - 1).Value)

                    Me.Tabla2.Item(IVA84.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("IVA_8")), 0, ds.Tables(0).Rows(j)("IVA_8"))
                    tot.PPD_Iva8 += Convert.ToDecimal(Me.Tabla2.Item(IVA84.Index, i - 1).Value)

                    Me.Tabla2.Item(IVAd.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("IVA_16")), 0, ds.Tables(0).Rows(j)("IVA_16"))
                    tot.PPD_Iva16 += Convert.ToDecimal(Me.Tabla2.Item(IVAd.Index, i - 1).Value)

                    Me.Tabla2.Item(RI4.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Retenido_IVA")), 0, ds.Tables(0).Rows(j)("Retenido_IVA"))
                    tot.PPD_Riva += Convert.ToDecimal(Me.Tabla2.Item(RI4.Index, i - 1).Value)

                    Me.Tabla2.Item(RISR4.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Retenido_ISR")), 0, ds.Tables(0).Rows(j)("Retenido_ISR"))
                    tot.PPD_Risr += Convert.ToDecimal(Me.Tabla2.Item(RISR4.Index, i - 1).Value)

                    Me.Tabla2.Item(ISH4.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("ISH")), 0, ds.Tables(0).Rows(j)("ISH"))
                    tot.PPD_Ish += Convert.ToDecimal(Me.Tabla2.Item(ISH4.Index, i - 1).Value)

                    Me.Tabla2.Item(Totald.Index, i - 1).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Total")), 0, ds.Tables(0).Rows(j)("Total"))
                    tot.PPD_Tp += Convert.ToDecimal(Me.Tabla2.Item(Totald.Index, i - 1).Value)
                Next
                consulta = "   Select  Xml_Sat.UUID, Xml_Sat.UUID_Relacion, Xml_Sat.Nombre_Emisor as E, Xml_Sat.SubTotal  , Xml_Sat.Descuento   , Xml_Sat.Total_IEPS ,"
                consulta &= " Xml_Sat.IVA_16  , Xml_Sat.IVA_8  ,"
                consulta &= " Xml_Sat.Retenido_IVA,  Xml_Sat.Retenido_ISR  ,"
                consulta &= " Xml_Sat.ISH,  Xml_Sat.Total  "
                consulta &= " FROM Xml_Sat WHERE (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & i & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                consulta &= "  AND Xml_Sat.Metodo_de_Pago  LIKE '%PPD%'  and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                Dim pa = Eventos.Obtener_DS(consulta)
                If pa.Tables(0).Rows.Count > 0 Then
                    For s As Integer = 0 To pa.Tables(0).Rows.Count - 1

                        DetallePuePPD.Add(New TotalesDetallePPD() With {.Mes = Me.Tabla2.Item(Mes.Index, i - 1).Value, .UUIDpue = pa.Tables(0).Rows(s)("UUID"), .Emisor = pa.Tables(0).Rows(s)("E"),
                                .PUE_Subtotal = Format(pa.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"), .PUE_Descuento = Format(pa.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                                .PUE_Ieps = Format(pa.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"), .PUE_Iva16 = Format(pa.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                                .PUE_Iva8 = Format(pa.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"), .PUE_Riva = Format(pa.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                                .PUE_Risr = Format(pa.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"), .PUE_Ish = Format(pa.Tables(0).Rows(s)("ISH"), "$ #,##0.00"),
                                .PUE_Tp = Format(pa.Tables(0).Rows(s)("Total"), "$ #,##0.00"), .Tipo = "PPD", .UUIDRel = pa.Tables(0).Rows(s)("UUID_Relacion")})
                    Next
                    DetallePuePPD.Add(New TotalesDetallePPD() With {.Mes = Me.Tabla2.Item(Mes.Index, i - 1).Value, .UUIDpue = "TOTAL DEL MES", .Emisor = "",
                             .PUE_Subtotal = Format(Me.Tabla2.Item(TotalPPD.Index, i - 1).Value, "$ #,##0.00"), .PUE_Descuento = Format(Me.Tabla2.Item(Exentod.Index, i - 1).Value, "$ #,##0.00"),
                             .PUE_Ieps = Format(Me.Tabla2.Item(IEPS4.Index, i - 1).Value, "$ #,##0.00"), .PUE_Iva16 = Format(Me.Tabla2.Item(IVAd.Index, i - 1).Value, "$ #,##0.00"),
                             .PUE_Iva8 = Format(Me.Tabla2.Item(IVA84.Index, i - 1).Value, "$ #,##0.00"), .PUE_Riva = Format(Me.Tabla2.Item(RI4.Index, i - 1).Value, "$ #,##0.00"),
                             .PUE_Risr = Format(Me.Tabla2.Item(RISR4.Index, i - 1).Value, "$ #,##0.00"), .PUE_Ish = Format(Me.Tabla2.Item(ISH4.Index, i - 1).Value, "$ #,##0.00"),
                             .PUE_Tp = Format(Me.Tabla2.Item(Totald.Index, i - 1).Value, "$ #,##0.00"), .Tipo = "PPD", .UUIDRel = ""})
                End If

            End If



#End Region

            Me.Tabla2.Item(TCG.Index, i - 1).Value = Me.Tabla2.Item(PUEGravado.Index, i - 1).Value + Me.Tabla2.Item(PagosGravado.Index, i - 1).Value
            tot.Ps_Subtotal += Convert.ToDecimal(Me.Tabla2.Item(TCG.Index, i - 1).Value)
            Me.Tabla2.Item(Exentoc.Index, i - 1).Value = Me.Tabla2.Item(Exento.Index, i - 1).Value + Me.Tabla2.Item(Exentob.Index, i - 1).Value
            tot.Ps_Descuento += Convert.ToDecimal(Me.Tabla2.Item(Exentoc.Index, i - 1).Value)
            Me.Tabla2.Item(IEPS3.Index, i - 1).Value = Me.Tabla2.Item(IEPS1.Index, i - 1).Value + Me.Tabla2.Item(IEPS2.Index, i - 1).Value
            tot.Ps_Ieps += Convert.ToDecimal(Me.Tabla2.Item(IEPS3.Index, i - 1).Value)
            Me.Tabla2.Item(IVA83.Index, i - 1).Value = Me.Tabla2.Item(IVA81.Index, i - 1).Value + Me.Tabla2.Item(IVA82.Index, i - 1).Value
            tot.Ps_Iva8 += Convert.ToDecimal(Me.Tabla2.Item(IVA83.Index, i - 1).Value)
            Me.Tabla2.Item(IVAc.Index, i - 1).Value = Me.Tabla2.Item(IVA.Index, i - 1).Value + Me.Tabla2.Item(IVAb.Index, i - 1).Value
            tot.Ps_Iva16 += Convert.ToDecimal(Me.Tabla2.Item(IVAc.Index, i - 1).Value)
            Me.Tabla2.Item(RI3.Index, i - 1).Value = Me.Tabla2.Item(RI1.Index, i - 1).Value + Me.Tabla2.Item(RI2.Index, i - 1).Value
            tot.Ps_Riva += Convert.ToDecimal(Me.Tabla2.Item(RI3.Index, i - 1).Value)
            Me.Tabla2.Item(RISR3.Index, i - 1).Value = Me.Tabla2.Item(RISR1.Index, i - 1).Value + Me.Tabla2.Item(RISR2.Index, i - 1).Value
            tot.Ps_Risr += Convert.ToDecimal(Me.Tabla2.Item(RISR3.Index, i - 1).Value)
            Me.Tabla2.Item(ISH3.Index, i - 1).Value = Me.Tabla2.Item(ISH1.Index, i - 1).Value + Me.Tabla2.Item(ISH2.Index, i - 1).Value
            tot.Ps_Ish += Convert.ToDecimal(Me.Tabla2.Item(ISH3.Index, i - 1).Value)
            Me.Tabla2.Item(Totalc.Index, i - 1).Value = Me.Tabla2.Item(Totala.Index, i - 1).Value + Me.Tabla2.Item(Totalb.Index, i - 1).Value
            tot.Ps_Tp += Convert.ToDecimal(Me.Tabla2.Item(Totalc.Index, i - 1).Value)

        Next
        Negrita_morado.Font = New Font(Me.Tabla2.Font, FontStyle.Bold)
        Negrita_morado.BackColor = Color.Plum
        Dim Fila As DataGridViewRow = Me.Tabla2.Rows(12)
        Me.Tabla2.Item(PUEGravado.Index, 12).Value = tot.PUE_Subtotal
        Me.Tabla2.Item(Exento.Index, 12).Value = tot.PUE_Descuento
        Me.Tabla2.Item(IVA81.Index, 12).Value = tot.PUE_Iva8
        Me.Tabla2.Item(IVA.Index, 12).Value = tot.PUE_Iva16
        Me.Tabla2.Item(RI1.Index, 12).Value = tot.PUE_Riva
        Me.Tabla2.Item(RISR1.Index, 12).Value = tot.PUE_Risr
        Me.Tabla2.Item(ISH1.Index, 12).Value = tot.PUE_Ish
        Me.Tabla2.Item(Totala.Index, 12).Value = tot.PUE_Tp

        '***** Pagos
        Me.Tabla2.Item(PagosGravado.Index, 12).Value = tot.P_Subtotal
        Me.Tabla2.Item(Exentob.Index, 12).Value = tot.P_Descuento
        Me.Tabla2.Item(IEPS2.Index, 12).Value = tot.P_Ieps
        Me.Tabla2.Item(IVA82.Index, 12).Value = tot.P_Iva8
        Me.Tabla2.Item(IVAb.Index, 12).Value = tot.P_Iva16
        Me.Tabla2.Item(RI2.Index, 12).Value = tot.P_Riva
        Me.Tabla2.Item(RISR2.Index, 12).Value = tot.P_Risr
        Me.Tabla2.Item(ISH2.Index, 12).Value = tot.P_Ish
        Me.Tabla2.Item(Totalb.Index, 12).Value = tot.P_Tp



        ' **** Sumatorias
        Me.Tabla2.Item(TCG.Index, 12).Value = tot.Ps_Subtotal
        Me.Tabla2.Item(Exentoc.Index, 12).Value = tot.Ps_Descuento
        Me.Tabla2.Item(IEPS3.Index, 12).Value = tot.Ps_Ieps
        Me.Tabla2.Item(IVA83.Index, 12).Value = tot.Ps_Iva8
        Me.Tabla2.Item(IVAc.Index, 12).Value = tot.Ps_Iva16
        Me.Tabla2.Item(RI3.Index, 12).Value = tot.Ps_Riva
        Me.Tabla2.Item(RISR3.Index, 12).Value = tot.Ps_Risr
        Me.Tabla2.Item(ISH3.Index, 12).Value = tot.Ps_Ish
        Me.Tabla2.Item(Totalc.Index, 12).Value = tot.Ps_Tp

        '****** PPD
        Me.Tabla2.Item(TotalPPD.Index, 12).Value = tot.PPD_Subtotal
        Me.Tabla2.Item(Exentod.Index, 12).Value = tot.PPD_Descuento
        Me.Tabla2.Item(IEPS4.Index, 12).Value = tot.PPD_Ieps
        Me.Tabla2.Item(IVA84.Index, 12).Value = tot.PPD_Iva8
        Me.Tabla2.Item(IVAd.Index, 12).Value = tot.PPD_Iva16
        Me.Tabla2.Item(RI4.Index, 12).Value = tot.PPD_Riva
        Me.Tabla2.Item(RISR4.Index, 12).Value = tot.PPD_Risr
        Me.Tabla2.Item(ISH4.Index, 12).Value = tot.PPD_Ish
        Me.Tabla2.Item(Totald.Index, 12).Value = tot.PPD_Tp

        '****** Nota
        Me.Tabla2.Item(NotaSub.Index, 12).Value = tot.N_Subtotal
        Me.Tabla2.Item(DesNota.Index, 12).Value = tot.N_Descuento
        Me.Tabla2.Item(NotaIEPS.Index, 12).Value = tot.N_Ieps
        Me.Tabla2.Item(NotaIVA8.Index, 12).Value = tot.N_Iva8
        Me.Tabla2.Item(NotaIVA16.Index, 12).Value = tot.N_Iva16
        Me.Tabla2.Item(NOTARIVA.Index, 12).Value = tot.N_Riva
        Me.Tabla2.Item(NotaRISR.Index, 12).Value = tot.N_Risr
        Me.Tabla2.Item(NotaISH.Index, 12).Value = tot.N_Ish
        Me.Tabla2.Item(NotaTotal.Index, 12).Value = tot.N_Tp



        Me.Tabla2.Item(Mes.Index, 12).Value = "Totales"
        Fila.DefaultCellStyle = Negrita_morado
        Me.Tabla2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub
    Private Sub Calcula3()
        If Me.Tabla3.RowCount > 0 Then
            Me.Tabla3.Rows.Clear()
        End If
        Dim Usos = Eventos.Obtener_DS("SELECT DISTINCT  Uso_CFDI.Clave AS  C,Uso_CFDI.Descripcion AS D  FROM Uso_CFDI INNER JOIN Xml_Sat ON Xml_Sat.UsoCFDI = Uso_CFDI.Clave WHERE Xml_Sat.Id_Empresa = " & Me.lstCliente.SelectItem & " ORDER BY c")
        If Usos.Tables(0).Rows.Count > 0 Then
            Me.Tabla3.RowCount = Usos.Tables(0).Rows.Count * 5 + 25
            Try
                If DsF Is Nothing Then
                Else
                    DsF.Clear()
                End If

            Catch ex As Exception

            End Try
            DsF = Usos
        End If
    End Sub
    Private Sub Calcular3(ByVal Usos As DataSet)
        If DetalleUsos.Count > 0 Then
            DetalleUsos.Clear()
        End If
        Dim ds As DataSet
        Dim consulta As String = ""
        Dim Fila As Integer = 0
        Dim Colum As Integer = 2
        Dim Tipo As String = ""
        Dim Primer As Integer = 0
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Barra.Minimum = 0
        frm.Text = "Calculando Resumen de Usos por favor espere..."
        frm.Barra.Maximum = Me.Tabla3.RowCount - 1
        For T As Integer = 1 To 5 ' las cuatro tablas


            If T = 1 Then
                Tipo = "PUE"
                For U As Integer = 0 To Usos.Tables(0).Rows.Count - 1 ' todos los USOS
                    Me.Tabla3.Item(0, Fila).Value = Usos.Tables(0).Rows(U)("C").ToString().Trim()
                    Me.Tabla3.Item(1, Fila).Value = Usos.Tables(0).Rows(U)("D").ToString().Trim()
                    For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text) 'TODOS LOS MESES
                        consulta = "   Select    Sum(Xml_Sat.SubTotal) As SubTotal ,Sum(Xml_Sat.Descuento ) As Descuento ,Sum(Xml_Sat.Total_IEPS) As Total_IEPS ,"
                        consulta &= "Sum(Xml_Sat.IVA_16) As IVA_16 ,Sum(Xml_Sat.IVA_8) As IVA_8 ,"
                        consulta &= "Sum(Xml_Sat.Retenido_IVA) As Retenido_IVA ,Sum(Xml_Sat.Retenido_ISR) As Retenido_ISR ,"
                        consulta &= "Sum(Xml_Sat.ISH ) As ISH ,Sum(Xml_Sat.Total ) As Total"
                        consulta &= " FROM Xml_Sat WHERE Tipo='Factura' and (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & M & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                        consulta &= "  AND Xml_Sat.UsoCFDI  LIKE '%" & Usos.Tables(0).Rows(U)(0).ToString().Trim() & "%' AND Xml_Sat.Metodo_de_Pago  LIKE '%" & Tipo & "%' and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                        ds = Eventos.Obtener_DS(consulta)
                        If ds.Tables(0).Rows.Count > 0 Then
                            For D As Integer = 0 To ds.Tables(0).Rows.Count - 1

                                Me.Tabla3.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("SubTotal")), 0, ds.Tables(0).Rows(D)("SubTotal"))
                                'tot.PPD_Subtotal += Convert.ToDecimal(Me.Tabla3.Item(TotalPPD.Index, U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Descuento")), 0, ds.Tables(0).Rows(D)("Descuento"))
                                'tot.PPD_Descuento += Convert.ToDecimal(Me.Tabla3.Item(Exentod.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total_IEPS")), 0, ds.Tables(0).Rows(D)("Total_IEPS"))
                                'tot.PPD_Ieps += Convert.ToDecimal(Me.Tabla3.Item(IEPS4.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA_8")), 0, ds.Tables(0).Rows(D)("IVA_8"))
                                'tot.PPD_Iva16 += Convert.ToDecimal(Me.Tabla3.Item(IVAd.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA_16")), 0, ds.Tables(0).Rows(D)("IVA_16"))
                                'tot.PPD_Iva8 += Convert.ToDecimal(Me.Tabla3.Item(IVA84.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido_IVA")), 0, ds.Tables(0).Rows(D)("Retenido_IVA"))
                                'tot.PPD_Riva += Convert.ToDecimal(Me.Tabla3.Item(RI4.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido_ISR")), 0, ds.Tables(0).Rows(D)("Retenido_ISR"))
                                'tot.PPD_Risr += Convert.ToDecimal(Me.Tabla3.Item(RISR4.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("ISH")), 0, ds.Tables(0).Rows(D)("ISH"))
                                'tot.PPD_Ish += Convert.ToDecimal(Me.Tabla3.Item(ISH4.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total")), 0, ds.Tables(0).Rows(D)("Total"))
                                'tot.PPD_Tp += Convert.ToDecimal(Me.Tabla3.Item(Totald.Index,U).Value)

                                Try



                                    consulta = "   Select  Xml_Sat.UUID,Xml_Sat.UUID_Relacion, Xml_Sat.Nombre_Emisor as E, Xml_Sat.SubTotal  , Xml_Sat.Descuento   , Xml_Sat.Total_IEPS ,"
                                    consulta &= " Xml_Sat.IVA_16  , Xml_Sat.IVA_8  ,"
                                    consulta &= " Xml_Sat.Retenido_IVA,  Xml_Sat.Retenido_ISR  ,"
                                    consulta &= " Xml_Sat.ISH,  Xml_Sat.Total  "
                                    consulta &= " FROM Xml_Sat WHERE Tipo='Factura' and (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & M & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                                    consulta &= "  AND  Xml_Sat.Metodo_de_Pago  LIKE '%" & Tipo & "%' AND Xml_Sat.UsoCFDI  LIKE '%" & Usos.Tables(0).Rows(U)(0).ToString().Trim() & "%' and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                                    Dim pa = Eventos.Obtener_DS(consulta)
                                    If pa.Tables(0).Rows.Count > 0 Then
                                        For s As Integer = 0 To pa.Tables(0).Rows.Count - 1
                                            DetalleUsos.Add(New TotalesUsos() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .USO = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = pa.Tables(0).Rows(s)("UUID"), .Emisor = pa.Tables(0).Rows(s)("E"),
                                                    .PUE_Subtotal = Format(pa.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"), .PUE_Descuento = Format(pa.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                                                    .PUE_Ieps = Format(pa.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"), .PUE_Iva16 = Format(pa.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                                                    .PUE_Iva8 = Format(pa.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"), .PUE_Riva = Format(pa.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                                                    .PUE_Risr = Format(pa.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"), .PUE_Ish = Format(pa.Tables(0).Rows(s)("ISH"), "$ #,##0.00"),
                                                    .PUE_Tp = Format(pa.Tables(0).Rows(s)("Total"), "$ #,##0.00"), .Tipo = "PUE", .UUIDR = pa.Tables(0).Rows(s)("UUID_Relacion")})
                                        Next
                                        DetalleUsos.Add(New TotalesUsos() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .USO = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = "Total del Mes: ", .Emisor = "",
                                                 .PUE_Subtotal = Format(Me.Tabla3.Item(Colum - 8, U).Value, "$ #,##0.00"), .PUE_Descuento = Format(Me.Tabla3.Item(Colum - 7, U).Value, "$ #,##0.00"),
                                                 .PUE_Ieps = Format(Me.Tabla3.Item(Colum - 6, U).Value, "$ #,##0.00"), .PUE_Iva16 = Format(Me.Tabla3.Item(Colum - 4, U).Value, "$ #,##0.00"),
                                                 .PUE_Iva8 = Format(Me.Tabla3.Item(Colum - 5, U).Value, "$ #,##0.00"), .PUE_Riva = Format(Me.Tabla3.Item(Colum - 3, U).Value, "$ #,##0.00"),
                                                 .PUE_Risr = Format(Me.Tabla3.Item(Colum - 2, U).Value, "$ #,##0.00"), .PUE_Ish = Format(Me.Tabla3.Item(Colum - 1, U).Value, "$ #,##0.00"),
                                                 .PUE_Tp = Format(Me.Tabla3.Item(Colum, U).Value, "$ #,##0.00"), .Tipo = "PUE", .UUIDR = ""})
                                    End If
                                Catch ex As Exception

                                End Try
                                Colum += 2
                            Next

                        End If
                    Next
                    Colum = 2
                    Fila += 1
                    frm.Barra.Value = Fila
                Next
                Dim Su As Decimal = 0
                Dim Cont As Integer = 0


                For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                    For p As Integer = 1 To 9
                        Me.Tabla3.Item(p + 1 + Cont, Fila + 2).Value = Me.Tabla2.Item(p, M - 1).Value
                    Next
                    Cont += 10
                Next

                For j As Integer = 2 To Me.Tabla3.ColumnCount - 1
                    For i As Integer = Primer To Fila - 1
                        Su += Me.Tabla3.Item(j, i).Value
                    Next
                    Me.Tabla3.Item(j, Fila + 1).Value = Su
                    Me.Tabla3.Item(j, Fila + 3).Value = Su - Me.Tabla3.Item(j, Fila + 2).Value
                    Su = 0
                Next

            ElseIf T = 2 Then
                Tipo = "Pago"
                For U As Integer = 0 To Usos.Tables(0).Rows.Count - 1 ' todos los USOS
                    Me.Tabla3.Item(0, Fila).Value = Usos.Tables(0).Rows(U)("C").ToString().Trim()
                    Me.Tabla3.Item(1, Fila).Value = Usos.Tables(0).Rows(U)("D").ToString().Trim()
                    For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                        consulta = "SELECT    Sum(A.Monto ) AS Monto , Sum((A.Monto / B.Total) * B.SubTotal) AS SubTotal ,Sum((A.Monto / B.Total) * B.Descuento ) AS Descuento,Sum( (A.Monto / B.Total) * B.Total_IEPS ) AS [Total IEPS] ,"
                        consulta &= " Sum((A.Monto / B.Total) * B.IVA_16) AS [IVA 16%] ,Sum((A.Monto / B.Total) * B.IVA_8 ) AS [ IVA 8%] , Sum((A.Monto / B.Total) * B.Retenido_IVA) AS [Retenido IVA] ,"
                        consulta &= " Sum((A.Monto / B.Total) * B.Retenido_ISR) AS [Retenido ISR] ,Sum((A.Monto / B.Total) * B.ISH )AS ISH ,Sum( (A.Monto / B.Total) * B.Total)  AS Total"
                        consulta &= " FROM (SELECT Xml_Complemento.IdDocumento AS UUID, Xml_Complemento.MONTO, Xml_Complemento.UsoCFDI  FROM Xml_Complemento "
                        consulta &= " WHERE  (datepart(year, Xml_Complemento.FechaPago ) = " & Me.DtInicio.Value.Year & " AND datepart(month,Xml_Complemento.FechaPago) = " & M & " )  AND Id_Empresa =" & Me.lstCliente.SelectItem & "  And Xml_Complemento.UsoCFDI = '" & Usos.Tables(0).Rows(U)("C").ToString().Trim() & "' ) AS A "
                        consulta &= "  LEFT OUTER JOIN Xml_Sat AS B ON B.uuid = A.UUID WHERE B.total IS NOT NULL AND B.Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & "  AND b.Id_Empresa =" & Me.lstCliente.SelectItem & " "
                        ds = Eventos.Obtener_DS(consulta)
                        If ds.Tables(0).Rows.Count > 0 Then
                            For D As Integer = 0 To ds.Tables(0).Rows.Count - 1


                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("SubTotal")), 0, ds.Tables(0).Rows(D)("SubTotal"))
                                'tot.PPD_Subtotal += Convert.ToDecimal(Me.Tabla3.Item(TotalPPD.Index, U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Descuento")), 0, ds.Tables(0).Rows(D)("Descuento"))
                                'tot.PPD_Descuento += Convert.ToDecimal(Me.Tabla3.Item(Exentod.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total IEPS")), 0, ds.Tables(0).Rows(D)("Total IEPS"))
                                'tot.PPD_Ieps += Convert.ToDecimal(Me.Tabla3.Item(IEPS4.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)(" IVA 8%")), 0, ds.Tables(0).Rows(D)(" IVA 8%"))
                                'tot.PPD_Iva16 += Convert.ToDecimal(Me.Tabla3.Item(IVAd.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA 16%")), 0, ds.Tables(0).Rows(D)("IVA 16%"))
                                'tot.PPD_Iva8 += Convert.ToDecimal(Me.Tabla3.Item(IVA84.Index,U).Value)
                                Colum += 1

                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido IVA")), 0, ds.Tables(0).Rows(D)("Retenido IVA"))
                                'tot.PPD_Riva += Convert.ToDecimal(Me.Tabla3.Item(RI4.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido ISR")), 0, ds.Tables(0).Rows(D)("Retenido ISR"))
                                'tot.PPD_Risr += Convert.ToDecimal(Me.Tabla3.Item(RISR4.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("ISH")), 0, ds.Tables(0).Rows(D)("ISH"))
                                'tot.PPD_Ish += Convert.ToDecimal(Me.Tabla3.Item(ISH4.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total")), 0, ds.Tables(0).Rows(D)("Total"))
                                'tot.PPD_Tp += Convert.ToDecimal(Me.Tabla3.Item(Totald.Index,U).Value)



                                consulta = "SELECT     A.UUID,A.UUID_Pago , B.Nombre_Emisor as E, (A.Monto / B.Total) * B.SubTotal AS SubTotal  ,"
                                consulta &= "(A.Monto / B.Total) * B.Descuento As Descuento , (A.Monto / B.Total) * B.Total_IEPS As Total_IEPS,"
                                consulta &= " (A.Monto / B.Total) *  B.IVA_16 AS IVA_16 , (A.Monto / B.Total) * B.IVA_8 AS IVA_8 , "
                                consulta &= " (A.Monto / B.Total) * B.Retenido_IVA As Retenido_IVA ,(A.Monto / B.Total) *  B.Retenido_ISR As Retenido_ISR"
                                consulta &= "  , (A.Monto / B.Total) * B.ISH As ISH , (A.Monto / B.Total) * B.Total As Total  "
                                consulta &= " FROM (Select Xml_Complemento.IdDocumento As UUID,Xml_Complemento.UUID AS UUID_Pago, Xml_Complemento.MONTO FROM Xml_Complemento "
                                consulta &= " WHERE  (datepart(year, Xml_Complemento.FechaPago ) = " & Me.DtInicio.Value.Year & " And datepart(month,Xml_Complemento.FechaPago) = " & M & " )  And Id_Empresa =" & Me.lstCliente.SelectItem & " And Xml_Complemento.UsoCFDI = '" & Usos.Tables(0).Rows(U)("C").ToString().Trim() & "' ) AS A "
                                consulta &= "  LEFT OUTER JOIN Xml_Sat AS B ON B.uuid = A.UUID WHERE B.total IS NOT NULL AND B.Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " AND b.Id_Empresa =" & Me.lstCliente.SelectItem & " "
                                Dim pa = Eventos.Obtener_DS(consulta)
                                If pa.Tables(0).Rows.Count > 0 Then
                                    For s As Integer = 0 To pa.Tables(0).Rows.Count - 1
                                        DetalleUsosP.Add(New TotalesUsosPagos() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .USO = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = pa.Tables(0).Rows(s)("UUID"), .Emisor = pa.Tables(0).Rows(s)("E"),
                                                    .PUE_Subtotal = Format(pa.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"), .PUE_Descuento = Format(pa.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                                                    .PUE_Ieps = Format(pa.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"), .PUE_Iva16 = Format(pa.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                                                    .PUE_Iva8 = Format(pa.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"), .PUE_Riva = Format(pa.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                                                    .PUE_Risr = Format(pa.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"), .PUE_Ish = Format(pa.Tables(0).Rows(s)("ISH"), "$ #,##0.00"),
                                                    .PUE_Tp = Format(pa.Tables(0).Rows(s)("Total"), "$ #,##0.00"), .Tipo = "Pago", .UUIDR = pa.Tables(0).Rows(s)("UUID_Pago")})
                                    Next
                                    DetalleUsosP.Add(New TotalesUsosPagos() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .USO = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = "Total del Mes: ", .Emisor = "",
                                                 .PUE_Subtotal = Format(Me.Tabla3.Item(Colum - 8, U + Fila).Value, "$ #,##0.00"), .PUE_Descuento = Format(Me.Tabla3.Item(Colum - 7, U + Fila).Value, "$ #,##0.00"),
                                                 .PUE_Ieps = Format(Me.Tabla3.Item(Colum - 6, U + Fila).Value, "$ #,##0.00"), .PUE_Iva16 = Format(Me.Tabla3.Item(Colum - 4, U + Fila).Value, "$ #,##0.00"),
                                                 .PUE_Iva8 = Format(Me.Tabla3.Item(Colum - 5, U + Fila).Value, "$ #,##0.00"), .PUE_Riva = Format(Me.Tabla3.Item(Colum - 3, U + Fila).Value, "$ #,##0.00"),
                                                 .PUE_Risr = Format(Me.Tabla3.Item(Colum - 2, U + Fila).Value, "$ #,##0.00"), .PUE_Ish = Format(Me.Tabla3.Item(Colum - 1, U + Fila).Value, "$ #,##0.00"),
                                                 .PUE_Tp = Format(Me.Tabla3.Item(Colum, U + Fila).Value, "$ #,##0.00"), .Tipo = "Pago", .UUIDR = ""})
                                End If
                                Colum += 2

                            Next

                        End If
                    Next
                    Colum = 2
                    Fila += 1
                    frm.Barra.Value = Fila
                Next
                Dim Su As Decimal = 0
                Dim Cont As Integer = 0


                For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                    For p As Integer = 1 To 9
                        Me.Tabla3.Item(p + 1 + Cont, Fila + 2).Value = Me.Tabla2.Item(p + 10, M - 1).Value
                    Next
                    Cont += 10
                Next
                For j As Integer = 2 To Me.Tabla3.ColumnCount - 1
                    For i As Integer = Primer To Fila - 1
                        Su += Me.Tabla3.Item(j, i).Value
                    Next
                    Me.Tabla3.Item(j, Fila + 1).Value = Su
                    Me.Tabla3.Item(j, Fila + 3).Value = Su - Me.Tabla3.Item(j, Fila + 2).Value
                    Su = 0
                Next

            ElseIf T = 3 Then
                Tipo = "Suma"
                For U As Integer = 0 To Usos.Tables(0).Rows.Count - 1 ' todos los USOS
                    Me.Tabla3.Item(0, Fila).Value = Usos.Tables(0).Rows(U)("C").ToString().Trim()
                    Me.Tabla3.Item(1, Fila).Value = Usos.Tables(0).Rows(U)("D").ToString().Trim()

                    Colum = 2
                    Fila += 1
                    frm.Barra.Value = Fila
                Next

                Dim Su As Decimal = 0
                For j As Integer = 2 To Me.Tabla3.ColumnCount - 1
                    For i As Integer = Primer To Fila - 1
                        Su += Me.Tabla3.Item(j, i).Value
                    Next
                    Me.Tabla3.Item(j, Fila + 1).Value = Su
                    Me.Tabla3.Item(j, Fila + 3).Value = Su - Me.Tabla3.Item(j, Fila + 2).Value
                    Su = 0
                Next

            ElseIf T = 4 Then
                Tipo = "PPD"
                For U As Integer = 0 To Usos.Tables(0).Rows.Count - 1 ' todos los USOS
                    Me.Tabla3.Item(0, Fila).Value = Usos.Tables(0).Rows(U)("C").ToString().Trim()
                    Me.Tabla3.Item(1, Fila).Value = Usos.Tables(0).Rows(U)("D").ToString().Trim()
                    For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                        consulta = "   Select    Sum(Xml_Sat.SubTotal) As SubTotal ,Sum(Xml_Sat.Descuento ) As Descuento ,Sum(Xml_Sat.Total_IEPS) As Total_IEPS ,"
                        consulta &= "Sum(Xml_Sat.IVA_16) As IVA_16 ,Sum(Xml_Sat.IVA_8) As IVA_8 ,"
                        consulta &= "Sum(Xml_Sat.Retenido_IVA) As Retenido_IVA ,Sum(Xml_Sat.Retenido_ISR) As Retenido_ISR ,"
                        consulta &= "Sum(Xml_Sat.ISH ) As ISH ,Sum(Xml_Sat.Total ) As Total"
                        consulta &= " FROM Xml_Sat WHERE (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & M & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                        consulta &= "  AND Xml_Sat.UsoCFDI  LIKE '%" & Usos.Tables(0).Rows(U)(0).ToString().Trim() & "%' AND Xml_Sat.Metodo_de_Pago  LIKE '%" & Tipo & "%' and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                        ds = Eventos.Obtener_DS(consulta)
                        If ds.Tables(0).Rows.Count > 0 Then
                            For D As Integer = 0 To ds.Tables(0).Rows.Count - 1


                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("SubTotal")), 0, ds.Tables(0).Rows(D)("SubTotal"))
                                'tot.PPD_Subtotal += Convert.ToDecimal(Me.Tabla3.Item(TotalPPD.Index, Fila).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Descuento")), 0, ds.Tables(0).Rows(D)("Descuento"))
                                'tot.PPD_Descuento += Convert.ToDecimal(Me.Tabla3.Item(Exentod.Index,Fila).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total_IEPS")), 0, ds.Tables(0).Rows(D)("Total_IEPS"))
                                'tot.PPD_Ieps += Convert.ToDecimal(Me.Tabla3.Item(IEPS4.Index,Fila).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA_8")), 0, ds.Tables(0).Rows(D)("IVA_8"))
                                'tot.PPD_Iva16 += Convert.ToDecimal(Me.Tabla3.Item(IVAd.Index,Fila).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA_16")), 0, ds.Tables(0).Rows(D)("IVA_16"))
                                'tot.PPD_Iva8 += Convert.ToDecimal(Me.Tabla3.Item(IVA84.Index,Fila).Value)
                                Colum += 1

                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido_IVA")), 0, ds.Tables(0).Rows(D)("Retenido_IVA"))
                                'tot.PPD_Riva += Convert.ToDecimal(Me.Tabla3.Item(RI4.Index,Fila).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido_ISR")), 0, ds.Tables(0).Rows(D)("Retenido_ISR"))
                                'tot.PPD_Risr += Convert.ToDecimal(Me.Tabla3.Item(RISR4.Index,Fila).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("ISH")), 0, ds.Tables(0).Rows(D)("ISH"))
                                'tot.PPD_Ish += Convert.ToDecimal(Me.Tabla3.Item(ISH4.Index,Fila).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total")), 0, ds.Tables(0).Rows(D)("Total"))
                                'tot.PPD_Tp += Convert.ToDecimal(Me.Tabla3.Item(Totald.Index,Fila).Value)


                                consulta = "   Select  Xml_Sat.UUID, Xml_Sat.Nombre_Emisor as E, Xml_Sat.SubTotal  , Xml_Sat.Descuento   , Xml_Sat.Total_IEPS ,"
                                consulta &= " Xml_Sat.IVA_16  , Xml_Sat.IVA_8  ,"
                                consulta &= " Xml_Sat.Retenido_IVA,  Xml_Sat.Retenido_ISR  ,"
                                consulta &= " Xml_Sat.ISH,  Xml_Sat.Total  "
                                consulta &= " FROM Xml_Sat WHERE (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & M & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                                consulta &= "  AND  Xml_Sat.Metodo_de_Pago  LIKE '%" & Tipo & "%' AND Xml_Sat.UsoCFDI  LIKE '%" & Usos.Tables(0).Rows(U)(0).ToString().Trim() & "%' and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                                Dim pa = Eventos.Obtener_DS(consulta)
                                If pa.Tables(0).Rows.Count > 0 Then
                                    For s As Integer = 0 To pa.Tables(0).Rows.Count - 1
                                        DetalleUsosPPD.Add(New TotalesUsosPPD() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .USO = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = pa.Tables(0).Rows(s)("UUID"), .Emisor = pa.Tables(0).Rows(s)("E"),
                                                .PUE_Subtotal = Format(pa.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"), .PUE_Descuento = Format(pa.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                                                .PUE_Ieps = Format(pa.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"), .PUE_Iva16 = Format(pa.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                                                .PUE_Iva8 = Format(pa.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"), .PUE_Riva = Format(pa.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                                                .PUE_Risr = Format(pa.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"), .PUE_Ish = Format(pa.Tables(0).Rows(s)("ISH"), "$ #,##0.00"),
                                                .PUE_Tp = Format(pa.Tables(0).Rows(s)("Total"), "$ #,##0.00"), .Tipo = "PPD"})
                                    Next
                                    DetalleUsosPPD.Add(New TotalesUsosPPD() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .USO = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = "Total del Mes: ", .Emisor = "",
                                             .PUE_Subtotal = Format(Me.Tabla3.Item(Colum - 8, U).Value, "$ #,##0.00"), .PUE_Descuento = Format(Me.Tabla3.Item(Colum - 7, U).Value, "$ #,##0.00"),
                                             .PUE_Ieps = Format(Me.Tabla3.Item(Colum - 6, U).Value, "$ #,##0.00"), .PUE_Iva16 = Format(Me.Tabla3.Item(Colum - 4, U).Value, "$ #,##0.00"),
                                             .PUE_Iva8 = Format(Me.Tabla3.Item(Colum - 5, U).Value, "$ #,##0.00"), .PUE_Riva = Format(Me.Tabla3.Item(Colum - 3, U).Value, "$ #,##0.00"),
                                             .PUE_Risr = Format(Me.Tabla3.Item(Colum - 2, U).Value, "$ #,##0.00"), .PUE_Ish = Format(Me.Tabla3.Item(Colum - 1, U).Value, "$ #,##0.00"),
                                             .PUE_Tp = Format(Me.Tabla3.Item(Colum, U).Value, "$ #,##0.00"), .Tipo = "PPD"})
                                End If
                                Colum += 2
                            Next

                        End If
                    Next
                    Colum = 2
                    Fila += 1
                    frm.Barra.Value = Fila
                Next

                Dim Su As Decimal = 0
                Dim Cont As Integer = 0


                For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                    For p As Integer = 1 To 9
                        Me.Tabla3.Item(p + 1 + Cont, Fila + 2).Value = Me.Tabla2.Item(p + 30, M - 1).Value
                    Next
                    Cont += 10
                Next
                For j As Integer = 2 To Me.Tabla3.ColumnCount - 1
                    For i As Integer = Primer To Fila - 1
                        Su += Me.Tabla3.Item(j, i).Value
                    Next
                    Me.Tabla3.Item(j, Fila + 1).Value = Su
                    Me.Tabla3.Item(j, Fila + 3).Value = Su - Me.Tabla3.Item(j, Fila + 2).Value
                    Su = 0
                Next
            ElseIf T = 5 Then
                Tipo = "Nota"
                For U As Integer = 0 To Usos.Tables(0).Rows.Count - 1 ' todos los USOS
                    Me.Tabla3.Item(0, Fila).Value = Usos.Tables(0).Rows(U)("C").ToString().Trim()
                    Me.Tabla3.Item(1, Fila).Value = Usos.Tables(0).Rows(U)("D").ToString().Trim()
                    For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text) 'TODOS LOS MESES
                        consulta = "   Select    Sum(Xml_Sat.SubTotal) As SubTotal ,Sum(Xml_Sat.Descuento ) As Descuento ,Sum(Xml_Sat.Total_IEPS) As Total_IEPS ,"
                        consulta &= "Sum(Xml_Sat.IVA_16) As IVA_16 ,Sum(Xml_Sat.IVA_8) As IVA_8 ,"
                        consulta &= "Sum(Xml_Sat.Retenido_IVA) As Retenido_IVA ,Sum(Xml_Sat.Retenido_ISR) As Retenido_ISR ,"
                        consulta &= "Sum(Xml_Sat.ISH ) As ISH ,Sum(Xml_Sat.Total ) As Total"
                        consulta &= " FROM Xml_Sat WHERE Tipo='NotaCredito' and (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & M & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                        consulta &= "  AND Xml_Sat.UsoCFDI  LIKE '%" & Usos.Tables(0).Rows(U)(0).ToString().Trim() & "%' AND     Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                        ds = Eventos.Obtener_DS(consulta)
                        If ds.Tables(0).Rows.Count > 0 Then
                            For D As Integer = 0 To ds.Tables(0).Rows.Count - 1

                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("SubTotal")), 0, ds.Tables(0).Rows(D)("SubTotal"))
                                'tot.PPD_Subtotal += Convert.ToDecimal(Me.Tabla3.Item(TotalPPD.Index, U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Descuento")), 0, ds.Tables(0).Rows(D)("Descuento"))
                                'tot.PPD_Descuento += Convert.ToDecimal(Me.Tabla3.Item(Exentod.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total_IEPS")), 0, ds.Tables(0).Rows(D)("Total_IEPS"))
                                'tot.PPD_Ieps += Convert.ToDecimal(Me.Tabla3.Item(IEPS4.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA_8")), 0, ds.Tables(0).Rows(D)("IVA_8"))
                                'tot.PPD_Iva16 += Convert.ToDecimal(Me.Tabla3.Item(IVAd.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA_16")), 0, ds.Tables(0).Rows(D)("IVA_16"))
                                'tot.PPD_Iva8 += Convert.ToDecimal(Me.Tabla3.Item(IVA84.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido_IVA")), 0, ds.Tables(0).Rows(D)("Retenido_IVA"))
                                'tot.PPD_Riva += Convert.ToDecimal(Me.Tabla3.Item(RI4.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido_ISR")), 0, ds.Tables(0).Rows(D)("Retenido_ISR"))
                                'tot.PPD_Risr += Convert.ToDecimal(Me.Tabla3.Item(RISR4.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("ISH")), 0, ds.Tables(0).Rows(D)("ISH"))
                                'tot.PPD_Ish += Convert.ToDecimal(Me.Tabla3.Item(ISH4.Index,U).Value)
                                Colum += 1
                                Me.Tabla3.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total")), 0, ds.Tables(0).Rows(D)("Total"))
                                'tot.PPD_Tp += Convert.ToDecimal(Me.Tabla3.Item(Totald.Index,U).Value)

                                Try



                                    consulta = "   Select  Xml_Sat.UUID,Xml_Sat.UUID_Relacion, Xml_Sat.Nombre_Emisor as E, Xml_Sat.SubTotal  , Xml_Sat.Descuento   , Xml_Sat.Total_IEPS ,"
                                    consulta &= " Xml_Sat.IVA_16  , Xml_Sat.IVA_8  ,"
                                    consulta &= " Xml_Sat.Retenido_IVA,  Xml_Sat.Retenido_ISR  ,"
                                    consulta &= " Xml_Sat.ISH,  Xml_Sat.Total  "
                                    consulta &= " FROM Xml_Sat WHERE Tipo='NotaCredito' and (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & M & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                                    consulta &= "  AND   Xml_Sat.UsoCFDI  LIKE '%" & Usos.Tables(0).Rows(U)(0).ToString().Trim() & "%' and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                                    Dim pa = Eventos.Obtener_DS(consulta)
                                    If pa.Tables(0).Rows.Count > 0 Then
                                        For s As Integer = 0 To pa.Tables(0).Rows.Count - 1
                                            DetalleUsosNotas.Add(New TotalesNotas() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .USO = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = pa.Tables(0).Rows(s)("UUID"), .Emisor = pa.Tables(0).Rows(s)("E"),
                                                    .PUE_Subtotal = Format(pa.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"), .PUE_Descuento = Format(pa.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                                                    .PUE_Ieps = Format(pa.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"), .PUE_Iva16 = Format(pa.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                                                    .PUE_Iva8 = Format(pa.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"), .PUE_Riva = Format(pa.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                                                    .PUE_Risr = Format(pa.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"), .PUE_Ish = Format(pa.Tables(0).Rows(s)("ISH"), "$ #,##0.00"),
                                                    .PUE_Tp = Format(pa.Tables(0).Rows(s)("Total"), "$ #,##0.00"), .Tipo = "Nota", .UUIDR = pa.Tables(0).Rows(s)("UUID_Relacion")})
                                        Next
                                        DetalleUsosNotas.Add(New TotalesNotas() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .USO = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = "Total del Mes: ", .Emisor = "",
                                                 .PUE_Subtotal = Format(Me.Tabla3.Item(Colum - 8, U).Value, "$ #,##0.00"), .PUE_Descuento = Format(Me.Tabla3.Item(Colum - 7, U).Value, "$ #,##0.00"),
                                                 .PUE_Ieps = Format(Me.Tabla3.Item(Colum - 6, U).Value, "$ #,##0.00"), .PUE_Iva16 = Format(Me.Tabla3.Item(Colum - 4, U).Value, "$ #,##0.00"),
                                                 .PUE_Iva8 = Format(Me.Tabla3.Item(Colum - 5, U).Value, "$ #,##0.00"), .PUE_Riva = Format(Me.Tabla3.Item(Colum - 3, U).Value, "$ #,##0.00"),
                                                 .PUE_Risr = Format(Me.Tabla3.Item(Colum - 2, U).Value, "$ #,##0.00"), .PUE_Ish = Format(Me.Tabla3.Item(Colum - 1, U).Value, "$ #,##0.00"),
                                                 .PUE_Tp = Format(Me.Tabla3.Item(Colum, U).Value, "$ #,##0.00"), .Tipo = "Nota", .UUIDR = ""})
                                    End If
                                Catch ex As Exception

                                End Try
                                Colum += 2
                            Next

                        End If
                    Next
                    Colum = 2
                    Fila += 1
                    frm.Barra.Value = Fila
                Next

                Dim Su As Decimal = 0
                Dim Cont As Integer = 0


                For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                    For p As Integer = 1 To 9
                        Me.Tabla3.Item(p + 1 + Cont, Fila + 2).Value = Me.Tabla2.Item(p + 40, M - 1).Value
                    Next
                    Cont += 10
                Next
                For j As Integer = 2 To Me.Tabla3.ColumnCount - 1
                    For i As Integer = Primer To Fila - 1
                        Su += Me.Tabla3.Item(j, i).Value
                    Next
                    Me.Tabla3.Item(j, Fila + 1).Value = Su
                    Me.Tabla3.Item(j, Fila + 3).Value = Su - Me.Tabla3.Item(j, Fila + 2).Value
                    Su = 0
                Next
            End If

            Me.Tabla3.Item(0, Fila).Value = ""
            Me.Tabla3.Item(1, Fila).Value = " "
            Fila += 1
            Me.Tabla3.Item(0, Fila).Value = ""
            Me.Tabla3.Item(1, Fila).Value = "Tolal " & Tipo
            Fila += 1
            Me.Tabla3.Item(0, Fila).Value = ""
            Me.Tabla3.Item(1, Fila).Value = "Tolal Resumen " & Tipo
            Fila += 1
            Me.Tabla3.Item(0, Fila).Value = ""
            Me.Tabla3.Item(1, Fila).Value = "Diferencia " & Tipo
            Fila += 1
            Me.Tabla3.Item(0, Fila).Value = ""
            Me.Tabla3.Item(1, Fila).Value = " "
            Fila += 1
            Primer = Fila
            Try
                frm.Barra.Value = Fila
            Catch ex As Exception

            End Try

        Next
        frm.Close()
    End Sub

    Private Sub Calcula4()
        If Me.Tabla4.RowCount > 0 Then
            Me.Tabla4.Rows.Clear()
        End If
        Dim Usos = Eventos.Obtener_DS("SELECT DISTINCT  Metodos_de_Pago.Clave AS  C,Metodos_de_Pago.Descripcion AS D  FROM Metodos_de_Pago INNER JOIN Xml_Sat ON Xml_Sat.FormaDePago = Metodos_de_Pago.Clave WHERE Xml_Sat.Id_Empresa = " & Me.lstCliente.SelectItem & "  ORDER BY c")
        If Usos.Tables(0).Rows.Count > 0 Then
            Me.Tabla4.RowCount = Usos.Tables(0).Rows.Count * 5 + 25
            Try
                DsF.Clear()
            Catch ex As Exception

            End Try
            DsF = Usos
        End If
    End Sub

    Private Sub Calcular4(ByVal Usos As DataSet)

        Dim ds As DataSet
        Dim consulta As String = ""
        Dim Primer As Integer = 0
        Dim Fila As Integer = 0
        Dim Colum As Integer = 2
        Dim Tipo As String = ""

        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Barra.Minimum = 0
        frm.Text = "Calculando Resumen de Formas de Pago por favor espere..."
        frm.Barra.Maximum = Me.Tabla4.RowCount - 1
        For T As Integer = 1 To 5 ' las cuatro tablas


            If T = 1 Then
                Tipo = "PUE"
                For U As Integer = 0 To Usos.Tables(0).Rows.Count - 1 ' todos los USOS
                    Me.Tabla4.Item(0, Fila).Value = Usos.Tables(0).Rows(U)("C").ToString().Trim()
                    Me.Tabla4.Item(1, Fila).Value = Usos.Tables(0).Rows(U)("D").ToString().Trim()
                    For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                        consulta = "   Select    Sum(Xml_Sat.SubTotal) As SubTotal ,Sum(Xml_Sat.Descuento ) As Descuento ,Sum(Xml_Sat.Total_IEPS) As Total_IEPS ,"
                        consulta &= "Sum(Xml_Sat.IVA_16) As IVA_16 ,Sum(Xml_Sat.IVA_8) As IVA_8 ,"
                        consulta &= "Sum(Xml_Sat.Retenido_IVA) As Retenido_IVA ,Sum(Xml_Sat.Retenido_ISR) As Retenido_ISR ,"
                        consulta &= "Sum(Xml_Sat.ISH ) As ISH ,Sum(Xml_Sat.Total ) As Total"
                        consulta &= " FROM Xml_Sat WHERE Tipo='Factura' and (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & M & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                        consulta &= "  AND Xml_Sat.FormaDePago  LIKE '%" & Usos.Tables(0).Rows(U)(0).ToString().Trim() & "%' AND Xml_Sat.Metodo_de_Pago  LIKE '%" & Tipo & "%' and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                        ds = Eventos.Obtener_DS(consulta)
                        If ds.Tables(0).Rows.Count > 0 Then
                            For D As Integer = 0 To ds.Tables(0).Rows.Count - 1

                                Me.Tabla4.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("SubTotal")), 0, ds.Tables(0).Rows(D)("SubTotal"))
                                'tot.PPD_Subtotal += Convert.ToDecimal(Me.Tabla4.Item(TotalPPD.Index, U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Descuento")), 0, ds.Tables(0).Rows(D)("Descuento"))
                                'tot.PPD_Descuento += Convert.ToDecimal(Me.Tabla4.Item(Exentod.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total_IEPS")), 0, ds.Tables(0).Rows(D)("Total_IEPS"))
                                'tot.PPD_Ieps += Convert.ToDecimal(Me.Tabla4.Item(IEPS4.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA_8")), 0, ds.Tables(0).Rows(D)("IVA_8"))
                                'tot.PPD_Iva16 += Convert.ToDecimal(Me.Tabla4.Item(IVAd.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA_16")), 0, ds.Tables(0).Rows(D)("IVA_16"))
                                'tot.PPD_Iva8 += Convert.ToDecimal(Me.Tabla4.Item(IVA84.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido_IVA")), 0, ds.Tables(0).Rows(D)("Retenido_IVA"))
                                'tot.PPD_Riva += Convert.ToDecimal(Me.Tabla4.Item(RI4.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido_ISR")), 0, ds.Tables(0).Rows(D)("Retenido_ISR"))
                                'tot.PPD_Risr += Convert.ToDecimal(Me.Tabla4.Item(RISR4.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("ISH")), 0, ds.Tables(0).Rows(D)("ISH"))
                                'tot.PPD_Ish += Convert.ToDecimal(Me.Tabla4.Item(ISH4.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, U).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total")), 0, ds.Tables(0).Rows(D)("Total"))
                                'tot.PPD_Tp += Convert.ToDecimal(Me.Tabla4.Item(Totald.Index,U).Value)




                                Try



                                    consulta = "   Select  Xml_Sat.UUID,Xml_Sat.UUID_Relacion, Xml_Sat.Nombre_Emisor as E, Xml_Sat.SubTotal  , Xml_Sat.Descuento   , Xml_Sat.Total_IEPS ,"
                                    consulta &= " Xml_Sat.IVA_16  , Xml_Sat.IVA_8  ,"
                                    consulta &= " Xml_Sat.Retenido_IVA,  Xml_Sat.Retenido_ISR  ,"
                                    consulta &= " Xml_Sat.ISH,  Xml_Sat.Total  "
                                    consulta &= " FROM Xml_Sat WHERE Tipo='Factura' and (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & M & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                                    consulta &= "  AND  Xml_Sat.Metodo_de_Pago  LIKE '%" & Tipo & "%' AND Xml_Sat.FormaDePago  LIKE '%" & Usos.Tables(0).Rows(U)(0).ToString().Trim() & "%' and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                                    Dim pa = Eventos.Obtener_DS(consulta)
                                    If pa.Tables(0).Rows.Count > 0 Then
                                        For s As Integer = 0 To pa.Tables(0).Rows.Count - 1
                                            DetalleM.Add(New TotalesMe() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .Forma = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = pa.Tables(0).Rows(s)("UUID"), .Emisor = pa.Tables(0).Rows(s)("E"),
                                                    .PUE_Subtotal = Format(pa.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"), .PUE_Descuento = Format(pa.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                                                    .PUE_Ieps = Format(pa.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"), .PUE_Iva16 = Format(pa.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                                                    .PUE_Iva8 = Format(pa.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"), .PUE_Riva = Format(pa.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                                                    .PUE_Risr = Format(pa.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"), .PUE_Ish = Format(pa.Tables(0).Rows(s)("ISH"), "$ #,##0.00"),
                                                    .PUE_Tp = Format(pa.Tables(0).Rows(s)("Total"), "$ #,##0.00"), .Tipo = "PUE", .UUIDR = pa.Tables(0).Rows(s)("UUID_Relacion")})
                                        Next
                                        DetalleM.Add(New TotalesMe() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .Forma = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = "", .Emisor = "",
                                                 .PUE_Subtotal = Format(Me.Tabla4.Item(Colum - 8, U).Value, "$ #,##0.00"), .PUE_Descuento = Format(Me.Tabla4.Item(Colum - 7, U).Value, "$ #,##0.00"),
                                                 .PUE_Ieps = Format(Me.Tabla4.Item(Colum - 6, U).Value, "$ #,##0.00"), .PUE_Iva16 = Format(Me.Tabla4.Item(Colum - 4, U).Value, "$ #,##0.00"),
                                                 .PUE_Iva8 = Format(Me.Tabla4.Item(Colum - 5, U).Value, "$ #,##0.00"), .PUE_Riva = Format(Me.Tabla4.Item(Colum - 3, U).Value, "$ #,##0.00"),
                                                 .PUE_Risr = Format(Me.Tabla4.Item(Colum - 2, U).Value, "$ #,##0.00"), .PUE_Ish = Format(Me.Tabla4.Item(Colum - 1, U).Value, "$ #,##0.00"),
                                                 .PUE_Tp = Format(Me.Tabla4.Item(Colum, U).Value, "$ #,##0.00"), .Tipo = "PUE", .UUIDR = ""})
                                    End If
                                Catch ex As Exception

                                End Try
                                Colum += 2
                            Next

                        End If
                    Next
                    Colum = 2
                    Fila += 1
                    frm.Barra.Value = Fila
                Next
                Dim Su As Decimal = 0
                Dim Cont As Integer = 0


                For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                    For p As Integer = 1 To 9
                        Me.Tabla4.Item(p + 1 + Cont, Fila + 2).Value = Me.Tabla2.Item(p, M - 1).Value
                    Next
                    Cont += 10
                Next

                For j As Integer = 2 To Me.Tabla4.ColumnCount - 1
                    For i As Integer = Primer To Fila - 1
                        Su += Me.Tabla4.Item(j, i).Value
                    Next
                    Me.Tabla4.Item(j, Fila + 1).Value = Su
                    Me.Tabla4.Item(j, Fila + 3).Value = Su - Me.Tabla4.Item(j, Fila + 2).Value
                    Su = 0
                Next
            ElseIf T = 2 Then
                Tipo = "Pago"
                For U As Integer = 0 To Usos.Tables(0).Rows.Count - 1 ' todos los USOS
                    Me.Tabla4.Item(0, Fila).Value = Usos.Tables(0).Rows(U)("C").ToString().Trim()
                    Me.Tabla4.Item(1, Fila).Value = Usos.Tables(0).Rows(U)("D").ToString().Trim()
                    For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                        consulta = "SELECT    Sum(A.Monto ) AS Monto , Sum((A.Monto / B.Total) * B.SubTotal) AS SubTotal ,Sum((A.Monto / B.Total) * B.Descuento ) AS Descuento,Sum( (A.Monto / B.Total) * B.Total_IEPS ) AS [Total IEPS] ,"
                        consulta &= " Sum((A.Monto / B.Total) * B.IVA_16) AS [IVA 16%] ,Sum((A.Monto / B.Total) * B.IVA_8 ) AS [ IVA 8%] , Sum((A.Monto / B.Total) * B.Retenido_IVA) AS [Retenido IVA] ,"
                        consulta &= " Sum((A.Monto / B.Total) * B.Retenido_ISR) AS [Retenido ISR] ,Sum((A.Monto / B.Total) * B.ISH )AS ISH ,Sum( (A.Monto / B.Total) * B.Total)  AS Total"
                        consulta &= " FROM (SELECT Xml_Complemento.IdDocumento AS UUID, Xml_Complemento.MONTO, Xml_Complemento.FormaDePago  FROM Xml_Complemento "
                        consulta &= " WHERE  (datepart(year, Xml_Complemento.FechaPago ) = " & Me.DtInicio.Value.Year & " AND datepart(month,Xml_Complemento.FechaPago) = " & M & " )  AND Id_Empresa =" & Me.lstCliente.SelectItem & "  And Xml_Complemento.FormaDePago = '" & Usos.Tables(0).Rows(U)("C").ToString().Trim() & "' ) AS A "
                        consulta &= "  LEFT OUTER JOIN Xml_Sat AS B ON B.uuid = A.UUID WHERE B.total IS NOT NULL AND B.Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & "  AND b.Id_Empresa =" & Me.lstCliente.SelectItem & " "
                        ds = Eventos.Obtener_DS(consulta)
                        If ds.Tables(0).Rows.Count > 0 Then
                            For D As Integer = 0 To ds.Tables(0).Rows.Count - 1


                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("SubTotal")), 0, ds.Tables(0).Rows(D)("SubTotal"))
                                'tot.PPD_Subtotal += Convert.ToDecimal(Me.Tabla4.Item(TotalPPD.Index, U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Descuento")), 0, ds.Tables(0).Rows(D)("Descuento"))
                                'tot.PPD_Descuento += Convert.ToDecimal(Me.Tabla4.Item(Exentod.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total IEPS")), 0, ds.Tables(0).Rows(D)("Total IEPS"))
                                'tot.PPD_Ieps += Convert.ToDecimal(Me.Tabla4.Item(IEPS4.Index,U).Value)
                                Colum += 1

                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)(" IVA 8%")), 0, ds.Tables(0).Rows(D)(" IVA 8%"))
                                'tot.PPD_Iva16 += Convert.ToDecimal(Me.Tabla4.Item(IVAd.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA 16%")), 0, ds.Tables(0).Rows(D)("IVA 16%"))
                                'tot.PPD_Iva8 += Convert.ToDecimal(Me.Tabla4.Item(IVA84.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido IVA")), 0, ds.Tables(0).Rows(D)("Retenido IVA"))
                                'tot.PPD_Riva += Convert.ToDecimal(Me.Tabla4.Item(RI4.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido ISR")), 0, ds.Tables(0).Rows(D)("Retenido ISR"))
                                'tot.PPD_Risr += Convert.ToDecimal(Me.Tabla4.Item(RISR4.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("ISH")), 0, ds.Tables(0).Rows(D)("ISH"))
                                'tot.PPD_Ish += Convert.ToDecimal(Me.Tabla4.Item(ISH4.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total")), 0, ds.Tables(0).Rows(D)("Total"))
                                'tot.PPD_Tp += Convert.ToDecimal(Me.Tabla4.Item(Totald.Index,U).Value)
                                Try


                                    consulta = "SELECT     A.UUID,A.UUID_Pago , B.Nombre_Emisor as E, (A.Monto / B.Total) * B.SubTotal AS SubTotal  ,"
                                    consulta &= "(A.Monto / B.Total) * B.Descuento As Descuento , (A.Monto / B.Total) * B.Total_IEPS As Total_IEPS,"
                                    consulta &= " (A.Monto / B.Total) *  B.IVA_16 AS IVA_16 , (A.Monto / B.Total) * B.IVA_8 AS IVA_8 , "
                                    consulta &= " (A.Monto / B.Total) * B.Retenido_IVA As Retenido_IVA ,(A.Monto / B.Total) *  B.Retenido_ISR As Retenido_ISR"
                                    consulta &= "  , (A.Monto / B.Total) * B.ISH As ISH , (A.Monto / B.Total) * B.Total As Total  "
                                    consulta &= " FROM (SELECT Xml_Complemento.IdDocumento AS UUID,Xml_Complemento.UUID AS UUID_Pago, Xml_Complemento.MONTO FROM Xml_Complemento "
                                    consulta &= " WHERE  (datepart(year, Xml_Complemento.FechaPago ) = " & Me.DtInicio.Value.Year & " AND datepart(month,Xml_Complemento.FechaPago) = " & M & " )  AND Id_Empresa =" & Me.lstCliente.SelectItem & " And Xml_Complemento.FormaDePago = '" & Usos.Tables(0).Rows(U)("C").ToString().Trim() & "' ) AS A "
                                    consulta &= "  LEFT OUTER JOIN Xml_Sat AS B ON B.uuid = A.UUID WHERE B.total IS NOT NULL AND B.Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " AND b.Id_Empresa =" & Me.lstCliente.SelectItem & " "
                                    Dim pa = Eventos.Obtener_DS(consulta)
                                    If pa.Tables(0).Rows.Count > 0 Then
                                        For s As Integer = 0 To pa.Tables(0).Rows.Count - 1
                                            DetalleMP.Add(New TotalesMePagos() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .Forma = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = pa.Tables(0).Rows(s)("UUID"), .Emisor = pa.Tables(0).Rows(s)("E"),
                                                    .PUE_Subtotal = Format(pa.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"), .PUE_Descuento = Format(pa.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                                                    .PUE_Ieps = Format(pa.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"), .PUE_Iva16 = Format(pa.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                                                    .PUE_Iva8 = Format(pa.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"), .PUE_Riva = Format(pa.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                                                    .PUE_Risr = Format(pa.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"), .PUE_Ish = Format(pa.Tables(0).Rows(s)("ISH"), "$ #,##0.00"),
                                                    .PUE_Tp = Format(pa.Tables(0).Rows(s)("Total"), "$ #,##0.00"), .Tipo = "Pago", .UUIDR = pa.Tables(0).Rows(s)("UUID_Pago")})
                                        Next
                                        DetalleMP.Add(New TotalesMePagos() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .Forma = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = "", .Emisor = "",
                                                 .PUE_Subtotal = Format(Me.Tabla4.Item(Colum - 8, U).Value, "$ #,##0.00"), .PUE_Descuento = Format(Me.Tabla4.Item(Colum - 7, U).Value, "$ #,##0.00"),
                                                 .PUE_Ieps = Format(Me.Tabla4.Item(Colum - 6, U).Value, "$ #,##0.00"), .PUE_Iva16 = Format(Me.Tabla4.Item(Colum - 4, U).Value, "$ #,##0.00"),
                                                 .PUE_Iva8 = Format(Me.Tabla4.Item(Colum - 5, U).Value, "$ #,##0.00"), .PUE_Riva = Format(Me.Tabla4.Item(Colum - 3, U).Value, "$ #,##0.00"),
                                                 .PUE_Risr = Format(Me.Tabla4.Item(Colum - 2, U).Value, "$ #,##0.00"), .PUE_Ish = Format(Me.Tabla4.Item(Colum - 1, U).Value, "$ #,##0.00"),
                                                 .PUE_Tp = Format(Me.Tabla4.Item(Colum, U).Value, "$ #,##0.00"), .Tipo = "Pago", .UUIDR = ""})
                                    End If
                                Catch ex As Exception

                                End Try
                                Colum += 2

                            Next

                        End If
                    Next
                    Colum = 2
                    Fila += 1
                    frm.Barra.Value = Fila
                Next
                Dim Su As Decimal = 0
                Dim Cont As Integer = 0
                For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                    For p As Integer = 1 To 9
                        Me.Tabla4.Item(p + 1 + Cont, Fila + 2).Value = Me.Tabla2.Item(p + 10, M - 1).Value
                    Next
                    Cont += 10
                Next
                For j As Integer = 2 To Me.Tabla4.ColumnCount - 1
                    For i As Integer = Primer To Fila - 1
                        Su += Me.Tabla4.Item(j, i).Value
                    Next
                    Me.Tabla4.Item(j, Fila + 1).Value = Su
                    Me.Tabla4.Item(j, Fila + 3).Value = Su - Me.Tabla4.Item(j, Fila + 2).Value
                    Su = 0
                Next
            ElseIf T = 3 Then
                Tipo = "Suma"
                For U As Integer = 0 To Usos.Tables(0).Rows.Count - 1 ' todos los USOS
                    Me.Tabla4.Item(0, Fila).Value = Usos.Tables(0).Rows(U)("C").ToString().Trim()
                    Me.Tabla4.Item(1, Fila).Value = Usos.Tables(0).Rows(U)("D").ToString().Trim()

                    Colum = 2
                    Fila += 1
                    frm.Barra.Value = Fila
                Next
            ElseIf T = 4 Then
                Tipo = "PPD"
                For U As Integer = 0 To Usos.Tables(0).Rows.Count - 1 ' todos los USOS
                    Me.Tabla4.Item(0, Fila).Value = Usos.Tables(0).Rows(U)("C").ToString().Trim()
                    Me.Tabla4.Item(1, Fila).Value = Usos.Tables(0).Rows(U)("D").ToString().Trim()
                    For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                        consulta = "   Select    Sum(Xml_Sat.SubTotal) As SubTotal ,Sum(Xml_Sat.Descuento ) As Descuento ,Sum(Xml_Sat.Total_IEPS) As Total_IEPS ,"
                        consulta &= "Sum(Xml_Sat.IVA_16) As IVA_16 ,Sum(Xml_Sat.IVA_8) As IVA_8 ,"
                        consulta &= "Sum(Xml_Sat.Retenido_IVA) As Retenido_IVA ,Sum(Xml_Sat.Retenido_ISR) As Retenido_ISR ,"
                        consulta &= "Sum(Xml_Sat.ISH ) As ISH ,Sum(Xml_Sat.Total ) As Total"
                        consulta &= " FROM Xml_Sat WHERE (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & M & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                        consulta &= "  AND Xml_Sat.FormaDePago  LIKE '%" & Usos.Tables(0).Rows(U)(0).ToString().Trim() & "%' AND Xml_Sat.Metodo_de_Pago  LIKE '%" & Tipo & "%' and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                        ds = Eventos.Obtener_DS(consulta)
                        If ds.Tables(0).Rows.Count > 0 Then
                            For D As Integer = 0 To ds.Tables(0).Rows.Count - 1


                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("SubTotal")), 0, ds.Tables(0).Rows(D)("SubTotal"))
                                'tot.PPD_Subtotal += Convert.ToDecimal(Me.Tabla4.Item(TotalPPD.Index, Fila).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Descuento")), 0, ds.Tables(0).Rows(D)("Descuento"))
                                'tot.PPD_Descuento += Convert.ToDecimal(Me.Tabla4.Item(Exentod.Index,Fila).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total_IEPS")), 0, ds.Tables(0).Rows(D)("Total_IEPS"))
                                'tot.PPD_Ieps += Convert.ToDecimal(Me.Tabla4.Item(IEPS4.Index,Fila).Value)
                                Colum += 1

                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA_8")), 0, ds.Tables(0).Rows(D)("IVA_8"))
                                'tot.PPD_Iva16 += Convert.ToDecimal(Me.Tabla4.Item(IVAd.Index,Fila).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA_16")), 0, ds.Tables(0).Rows(D)("IVA_16"))
                                'tot.PPD_Iva8 += Convert.ToDecimal(Me.Tabla4.Item(IVA84.Index,Fila).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido_IVA")), 0, ds.Tables(0).Rows(D)("Retenido_IVA"))
                                'tot.PPD_Riva += Convert.ToDecimal(Me.Tabla4.Item(RI4.Index,Fila).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido_ISR")), 0, ds.Tables(0).Rows(D)("Retenido_ISR"))
                                'tot.PPD_Risr += Convert.ToDecimal(Me.Tabla4.Item(RISR4.Index,Fila).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("ISH")), 0, ds.Tables(0).Rows(D)("ISH"))
                                'tot.PPD_Ish += Convert.ToDecimal(Me.Tabla4.Item(ISH4.Index,Fila).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total")), 0, ds.Tables(0).Rows(D)("Total"))
                                'tot.PPD_Tp += Convert.ToDecimal(Me.Tabla4.Item(Totald.Index,Fila).Value)
                                consulta = "   Select  Xml_Sat.UUID,Xml_Sat.UUID_Relacion, Xml_Sat.Nombre_Emisor as E, Xml_Sat.SubTotal  , Xml_Sat.Descuento   , Xml_Sat.Total_IEPS ,"
                                consulta &= " Xml_Sat.IVA_16  , Xml_Sat.IVA_8  ,"
                                consulta &= " Xml_Sat.Retenido_IVA,  Xml_Sat.Retenido_ISR  ,"
                                consulta &= " Xml_Sat.ISH,  Xml_Sat.Total  "
                                consulta &= " FROM Xml_Sat WHERE (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & M & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                                consulta &= "  AND  Xml_Sat.Metodo_de_Pago  LIKE '%" & Tipo & "%' AND Xml_Sat.FormaDePago  LIKE '%" & Usos.Tables(0).Rows(U)(0).ToString().Trim() & "%' and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                                Dim pa = Eventos.Obtener_DS(consulta)
                                If pa.Tables(0).Rows.Count > 0 Then
                                    For s As Integer = 0 To pa.Tables(0).Rows.Count - 1
                                        DetalleMPPD.Add(New TotalesMePPD() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .Forma = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = pa.Tables(0).Rows(s)("UUID"), .Emisor = pa.Tables(0).Rows(s)("E"),
                                                .PUE_Subtotal = Format(pa.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"), .PUE_Descuento = Format(pa.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                                                .PUE_Ieps = Format(pa.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"), .PUE_Iva16 = Format(pa.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                                                .PUE_Iva8 = Format(pa.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"), .PUE_Riva = Format(pa.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                                                .PUE_Risr = Format(pa.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"), .PUE_Ish = Format(pa.Tables(0).Rows(s)("ISH"), "$ #,##0.00"),
                                                .PUE_Tp = Format(pa.Tables(0).Rows(s)("Total"), "$ #,##0.00"), .Tipo = "PPD", .UUIDR = pa.Tables(0).Rows(s)("UUID_Relacion")})
                                    Next
                                    DetalleMPPD.Add(New TotalesMePPD() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .Forma = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = "", .Emisor = "",
                                             .PUE_Subtotal = Format(Me.Tabla4.Item(Colum - 8, U).Value, "$ #,##0.00"), .PUE_Descuento = Format(Me.Tabla4.Item(Colum - 7, U).Value, "$ #,##0.00"),
                                             .PUE_Ieps = Format(Me.Tabla4.Item(Colum - 6, U).Value, "$ #,##0.00"), .PUE_Iva16 = Format(Me.Tabla4.Item(Colum - 4, U).Value, "$ #,##0.00"),
                                             .PUE_Iva8 = Format(Me.Tabla4.Item(Colum - 5, U).Value, "$ #,##0.00"), .PUE_Riva = Format(Me.Tabla4.Item(Colum - 3, U).Value, "$ #,##0.00"),
                                             .PUE_Risr = Format(Me.Tabla4.Item(Colum - 2, U).Value, "$ #,##0.00"), .PUE_Ish = Format(Me.Tabla4.Item(Colum - 1, U).Value, "$ #,##0.00"),
                                             .PUE_Tp = Format(Me.Tabla4.Item(Colum, U).Value, "$ #,##0.00"), .Tipo = "PPD", .UUIDR = ""})
                                End If
                                Colum += 2


                            Next

                        End If
                    Next
                    Colum = 2
                    Fila += 1
                    frm.Barra.Value = Fila
                Next
                Dim Su As Decimal = 0
                Dim Cont As Integer = 0
                For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                    For p As Integer = 1 To 9
                        Me.Tabla4.Item(p + 1 + Cont, Fila + 2).Value = Me.Tabla2.Item(p + 30, M - 1).Value
                    Next
                    Cont += 10
                Next
                For j As Integer = 2 To Me.Tabla4.ColumnCount - 1
                    For i As Integer = Primer To Fila - 1
                        Su += Me.Tabla4.Item(j, i).Value
                    Next
                    Me.Tabla4.Item(j, Fila + 1).Value = Su
                    Me.Tabla4.Item(j, Fila + 3).Value = Su - Me.Tabla4.Item(j, Fila + 2).Value
                    Su = 0
                Next
            ElseIf T = 5 Then
                Tipo = "Nota"
                For U As Integer = 0 To Usos.Tables(0).Rows.Count - 1 ' todos los USOS
                    Me.Tabla4.Item(0, Fila).Value = Usos.Tables(0).Rows(U)("C").ToString().Trim()
                    Me.Tabla4.Item(1, Fila).Value = Usos.Tables(0).Rows(U)("D").ToString().Trim()
                    For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                        consulta = "   Select    Sum(Xml_Sat.SubTotal) As SubTotal ,Sum(Xml_Sat.Descuento ) As Descuento ,Sum(Xml_Sat.Total_IEPS) As Total_IEPS ,"
                        consulta &= "Sum(Xml_Sat.IVA_16) As IVA_16 ,Sum(Xml_Sat.IVA_8) As IVA_8 ,"
                        consulta &= "Sum(Xml_Sat.Retenido_IVA) As Retenido_IVA ,Sum(Xml_Sat.Retenido_ISR) As Retenido_ISR ,"
                        consulta &= "Sum(Xml_Sat.ISH ) As ISH ,Sum(Xml_Sat.Total ) As Total"
                        consulta &= " FROM Xml_Sat WHERE Tipo='NotaCredito' and (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & M & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                        consulta &= "  AND Xml_Sat.FormaDePago  LIKE '%" & Usos.Tables(0).Rows(U)(0).ToString().Trim() & "%'  and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                        ds = Eventos.Obtener_DS(consulta)
                        If ds.Tables(0).Rows.Count > 0 Then
                            For D As Integer = 0 To ds.Tables(0).Rows.Count - 1

                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("SubTotal")), 0, ds.Tables(0).Rows(D)("SubTotal"))
                                'tot.PPD_Subtotal += Convert.ToDecimal(Me.Tabla4.Item(TotalPPD.Index, U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Descuento")), 0, ds.Tables(0).Rows(D)("Descuento"))
                                'tot.PPD_Descuento += Convert.ToDecimal(Me.Tabla4.Item(Exentod.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total_IEPS")), 0, ds.Tables(0).Rows(D)("Total_IEPS"))
                                'tot.PPD_Ieps += Convert.ToDecimal(Me.Tabla4.Item(IEPS4.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA_8")), 0, ds.Tables(0).Rows(D)("IVA_8"))
                                'tot.PPD_Iva16 += Convert.ToDecimal(Me.Tabla4.Item(IVAd.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("IVA_16")), 0, ds.Tables(0).Rows(D)("IVA_16"))
                                'tot.PPD_Iva8 += Convert.ToDecimal(Me.Tabla4.Item(IVA84.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido_IVA")), 0, ds.Tables(0).Rows(D)("Retenido_IVA"))
                                'tot.PPD_Riva += Convert.ToDecimal(Me.Tabla4.Item(RI4.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Retenido_ISR")), 0, ds.Tables(0).Rows(D)("Retenido_ISR"))
                                'tot.PPD_Risr += Convert.ToDecimal(Me.Tabla4.Item(RISR4.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("ISH")), 0, ds.Tables(0).Rows(D)("ISH"))
                                'tot.PPD_Ish += Convert.ToDecimal(Me.Tabla4.Item(ISH4.Index,U).Value)
                                Colum += 1
                                Me.Tabla4.Item(Colum, Fila).Value = IIf(IsDBNull(ds.Tables(0).Rows(D)("Total")), 0, ds.Tables(0).Rows(D)("Total"))
                                'tot.PPD_Tp += Convert.ToDecimal(Me.Tabla4.Item(Totald.Index,U).Value)

                                Try

                                    consulta = "   Select  Xml_Sat.UUID,Xml_Sat.UUID_Relacion, Xml_Sat.Nombre_Emisor as E, Xml_Sat.SubTotal  , Xml_Sat.Descuento   , Xml_Sat.Total_IEPS ,"
                                    consulta &= " Xml_Sat.IVA_16  , Xml_Sat.IVA_8  ,"
                                    consulta &= " Xml_Sat.Retenido_IVA,  Xml_Sat.Retenido_ISR  ,"
                                    consulta &= " Xml_Sat.ISH,  Xml_Sat.Total  "
                                    consulta &= " FROM Xml_Sat WHERE Tipo='NotaCredito' and (datepart(year, Fecha_Timbrado ) = " & Me.DtInicio.Value.Year & " AND datepart(month,fecha_timbrado) = " & M & " )    AND Id_Empresa =" & Me.lstCliente.SelectItem & " "
                                    consulta &= "   AND Xml_Sat.FormaDePago  LIKE '%" & Usos.Tables(0).Rows(U)(0).ToString().Trim() & "%' and   Emitidas = " & Eventos.Bool2(Me.RadE.Checked) & " "
                                    Dim pa = Eventos.Obtener_DS(consulta)
                                    If pa.Tables(0).Rows.Count > 0 Then
                                        For s As Integer = 0 To pa.Tables(0).Rows.Count - 1
                                            DetalleMNotas.Add(New TotalesMeNotas() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .Forma = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = pa.Tables(0).Rows(s)("UUID"), .Emisor = pa.Tables(0).Rows(s)("E"),
                                                    .PUE_Subtotal = Format(pa.Tables(0).Rows(s)("SubTotal"), "$ #,##0.00"), .PUE_Descuento = Format(pa.Tables(0).Rows(s)("Descuento"), "$ #,##0.00"),
                                                    .PUE_Ieps = Format(pa.Tables(0).Rows(s)("Total_IEPS"), "$ #,##0.00"), .PUE_Iva16 = Format(pa.Tables(0).Rows(s)("IVA_16"), "$ #,##0.00"),
                                                    .PUE_Iva8 = Format(pa.Tables(0).Rows(s)("IVA_8"), "$ #,##0.00"), .PUE_Riva = Format(pa.Tables(0).Rows(s)("Retenido_IVA"), "$ #,##0.00"),
                                                    .PUE_Risr = Format(pa.Tables(0).Rows(s)("Retenido_ISR"), "$ #,##0.00"), .PUE_Ish = Format(pa.Tables(0).Rows(s)("ISH"), "$ #,##0.00"),
                                                    .PUE_Tp = Format(pa.Tables(0).Rows(s)("Total"), "$ #,##0.00"), .Tipo = "Nota", .UUIDR = pa.Tables(0).Rows(s)("UUID_Relacion")})
                                        Next
                                        DetalleMNotas.Add(New TotalesMeNotas() With {.Mes = Eventos.MesEnletra(IIf(Len(M.ToString()) = 1, "0" & M.ToString(), M.ToString())), .Forma = Usos.Tables(0).Rows(U)(0).ToString().Trim(), .UUIDpue = "", .Emisor = "",
                                                 .PUE_Subtotal = Format(Me.Tabla4.Item(Colum - 8, U).Value, "$ #,##0.00"), .PUE_Descuento = Format(Me.Tabla4.Item(Colum - 7, U).Value, "$ #,##0.00"),
                                                 .PUE_Ieps = Format(Me.Tabla4.Item(Colum - 6, U).Value, "$ #,##0.00"), .PUE_Iva16 = Format(Me.Tabla4.Item(Colum - 4, U).Value, "$ #,##0.00"),
                                                 .PUE_Iva8 = Format(Me.Tabla4.Item(Colum - 5, U).Value, "$ #,##0.00"), .PUE_Riva = Format(Me.Tabla4.Item(Colum - 3, U).Value, "$ #,##0.00"),
                                                 .PUE_Risr = Format(Me.Tabla4.Item(Colum - 2, U).Value, "$ #,##0.00"), .PUE_Ish = Format(Me.Tabla4.Item(Colum - 1, U).Value, "$ #,##0.00"),
                                                 .PUE_Tp = Format(Me.Tabla4.Item(Colum, U).Value, "$ #,##0.00"), .Tipo = "Nota", .UUIDR = ""})
                                    End If
                                Catch ex As Exception

                                End Try
                                Colum += 2
                            Next

                        End If
                    Next
                    Colum = 2
                    Fila += 1
                    frm.Barra.Value = Fila
                Next
                Dim Su As Decimal = 0
                Dim Cont As Integer = 0
                For M As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
                    For p As Integer = 1 To 9
                        Me.Tabla4.Item(p + 1 + Cont, Fila + 2).Value = Me.Tabla2.Item(p + 40, M - 1).Value
                    Next
                    Cont += 10
                Next
                For j As Integer = 2 To Me.Tabla4.ColumnCount - 1
                    For i As Integer = Primer To Fila - 1
                        Su += Me.Tabla4.Item(j, i).Value
                    Next
                    Me.Tabla4.Item(j, Fila + 1).Value = Su
                    Me.Tabla4.Item(j, Fila + 3).Value = Su - Me.Tabla4.Item(j, Fila + 2).Value
                    Su = 0
                Next
            End If
            Me.Tabla4.Item(0, Fila).Value = ""
            Me.Tabla4.Item(1, Fila).Value = " "
            Fila += 1
            Me.Tabla4.Item(0, Fila).Value = ""
            Me.Tabla4.Item(1, Fila).Value = "Tolal " & Tipo
            Fila += 1
            Me.Tabla4.Item(0, Fila).Value = ""
            Me.Tabla4.Item(1, Fila).Value = "Tolal Resumen " & Tipo
            Fila += 1
            Me.Tabla4.Item(0, Fila).Value = ""
            Me.Tabla4.Item(1, Fila).Value = "Diferencia " & Tipo
            Fila += 1
            Me.Tabla4.Item(0, Fila).Value = ""
            Me.Tabla4.Item(1, Fila).Value = " "
            Fila += 1
            Primer = Fila
            Try
                frm.Barra.Value = Fila
            Catch ex As Exception

            End Try

        Next
        frm.Close()
    End Sub
    Public Class Totales

        Public Property PUE_Subtotal As Decimal
        Public Property PUE_Descuento As Decimal
        Public Property PUE_Ieps As Decimal
        Public Property PUE_Iva8 As Decimal
        Public Property PUE_Iva16 As Decimal
        Public Property PUE_Riva As Decimal
        Public Property PUE_Risr As Decimal
        Public Property PUE_Ish As Decimal
        Public Property PUE_Tp As Decimal

        Public Property P_Subtotal As Decimal
        Public Property P_Descuento As Decimal
        Public Property P_Ieps As Decimal
        Public Property P_Iva8 As Decimal
        Public Property P_Iva16 As Decimal
        Public Property P_Riva As Decimal
        Public Property P_Risr As Decimal
        Public Property P_Ish As Decimal
        Public Property P_Tp As Decimal

        Public Property Ps_Subtotal As Decimal
        Public Property Ps_Descuento As Decimal
        Public Property Ps_Ieps As Decimal
        Public Property Ps_Iva8 As Decimal
        Public Property Ps_Iva16 As Decimal
        Public Property Ps_Riva As Decimal
        Public Property Ps_Risr As Decimal
        Public Property Ps_Ish As Decimal
        Public Property Ps_Tp As Decimal

        Public Property PPD_Subtotal As Decimal
        Public Property PPD_Descuento As Decimal
        Public Property PPD_Ieps As Decimal
        Public Property PPD_Iva8 As Decimal
        Public Property PPD_Iva16 As Decimal
        Public Property PPD_Riva As Decimal
        Public Property PPD_Risr As Decimal
        Public Property PPD_Ish As Decimal
        Public Property PPD_Tp As Decimal

        Public Property N_Subtotal As Decimal
        Public Property N_Descuento As Decimal
        Public Property N_Ieps As Decimal
        Public Property N_Iva8 As Decimal
        Public Property N_Iva16 As Decimal
        Public Property N_Riva As Decimal
        Public Property N_Risr As Decimal
        Public Property N_Ish As Decimal
        Public Property N_Tp As Decimal

    End Class

    Public Class TotalesUsos
        Public Property USO As String
        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property UUIDR As String
        Public Property Emisor As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String

        Public Property Tipo As String



    End Class
    Public Class TotalesNotas
        Public Property USO As String
        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property UUIDR As String
        Public Property Emisor As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String

        Public Property Tipo As String



    End Class
    Public Class TotalesUsosPPD
        Public Property USO As String
        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property UUIDR As String
        Public Property Emisor As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String

        Public Property Tipo As String



    End Class

    Public Class TotalesUsosPagos
        Public Property USO As String
        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property UUIDR As String
        Public Property Emisor As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String
        Public Property Tipo As String

    End Class

    Public Class TotalesDetallePUE

        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property UUIDRel As String
        Public Property Emisor As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String

        Public Property Tipo As String



    End Class

    Public Class TotalesDetalleNota

        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property UUIDRel As String
        Public Property Emisor As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String

        Public Property Tipo As String



    End Class
    Public Class TotalesDetallePagos

        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property UUIDRel As String
        Public Property Emisor As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String

        Public Property Tipo As String



    End Class
    Public Class TotalesDetallePPD

        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property UUIDRel As String
        Public Property Emisor As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String

        Public Property Tipo As String



    End Class
    Public Class TotalesUSOPUE

        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String

        Public Property Tipo As String



    End Class
    Public Class Pagos
        Public Property UUID As String
        Public Property Fecha As String
        Public Property Forma As String
        Public Property Monto As Decimal
        Public Property UUIDR As String
        Public Property NumPar As String
        Public Property ImporteAnterior As String
        Public Property ImportePagado As String
        Public Property ImporteSaldoInsoluto As String
        Public Property SubTotal As String
        Public Property Descuento As String
        Public Property Total_IEPS As String
        Public Property IVA16 As String
        Public Property IVA8 As String
        Public Property Retenido_IVA As String
        Public Property Retenido_ISR As String
        Public Property ISH As String


    End Class
    Public Class Notas
        Public Property Estado_SAT As String
        Public Property Tipo As String
        Public Property Fecha_Emision As String
        Public Property Fecha_Timbrado As String
        Public Property FormaDePago As String
        Public Property Metodo_de_Pago As String
        Public Property Serie As String
        Public Property UUID As String
        Public Property UUIDR As String
        Public Property Folio As String
        Public Property Conceptos As String
        Public Property RFC_Receptor As String
        Public Property Nombre_Receptor As String
        Public Property UsoCFDI As String
        Public Property SubTotal As String
        Public Property Descuento As String
        Public Property Total_IEPS As String
        Public Property IVA_16 As String
        Public Property IVA_8 As String
        Public Property Retenido_IVA As String
        Public Property Retenido_ISR As String
        Public Property ISH As String
        Public Property Total As String
    End Class
    Public Class Facturas

        Public Property Estado_SAT As String
        Public Property Tipo As String
        Public Property Fecha_Emision As String
        Public Property Fecha_Timbrado As String
        Public Property FormaDePago As String
        Public Property Metodo_de_Pago As String
        Public Property Serie As String
        Public Property UUID As String
        Public Property Folio As String
        Public Property Conceptos As String
        Public Property RFC_Receptor As String
        Public Property Nombre_Receptor As String
        Public Property UsoCFDI As String
        Public Property SubTotal As String
        Public Property Descuento As String
        Public Property Total_IEPS As String
        Public Property IVA_16 As String
        Public Property IVA_8 As String
        Public Property Retenido_IVA As String
        Public Property Retenido_ISR As String
        Public Property ISH As String
        Public Property Total As String
        Public Property TotalCobros As String
        Public Property TotalNotas As String
        Public Property Diferencia As String

    End Class
    Public Class TotalesMe
        Public Property Forma As String
        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property UUIDR As String
        Public Property Emisor As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String

        Public Property Tipo As String



    End Class
    Public Class TotalesMeNotas
        Public Property Forma As String
        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property UUIDR As String
        Public Property Emisor As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String

        Public Property Tipo As String



    End Class
    Public Class TotalesMePPD
        Public Property Forma As String
        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property UUIDR As String
        Public Property Emisor As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String

        Public Property Tipo As String



    End Class
    Public Class TotalesMePagos
        Public Property Forma As String
        Public Property Mes As String
        Public Property UUIDpue As String
        Public Property UUIDR As String
        Public Property Emisor As String
        Public Property PUE_Subtotal As String
        Public Property PUE_Descuento As String
        Public Property PUE_Ieps As String
        Public Property PUE_Iva8 As String
        Public Property PUE_Iva16 As String
        Public Property PUE_Riva As String
        Public Property PUE_Risr As String
        Public Property PUE_Ish As String
        Public Property PUE_Tp As String
        Public Property Tipo As String

    End Class
    Private Sub ReportesXml_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Permiso(Me.Tag.ToString) Then
            For i = DateTime.Now.Year To DateTime.Now.Year - 5 Step -1
                If i >= 2008 Then
                    Me.LstAnio.Items.Add(Str(i))
                End If
            Next
            Me.LstAnio.Text = anio

            If Len(m) < 2 Then
                m = "0" & m
            End If
            Me.ComboMes.Text = m
            If Len(m) < 2 Then
                m = "0" & m
            End If
            Me.ComboMes2.Text = m
            Me.lstCliente.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social FROM     Empresa  ")
            Me.lstCliente.SelectItem = 1
        Else
            Me.CmdBuscarF.Enabled = False
            Me.CmdExcel.Enabled = False
            Me.CmdExportar.Enabled = False
            Me.CmdExportarf.Enabled = False
            Me.CmdGuardar.Enabled = False
            Me.CmdImportar.Enabled = False
            Me.CmdUsos.Enabled = False
            Me.CmdResumen.Enabled = False
            RadMessageBox.Show("No tienes permiso para consultar la información...", "Proyecto Contable", MessageBoxButtons.OK, RadMessageIcon.Error)
        End If

    End Sub
    Private Sub CmdExportar_Click(sender As Object, e As EventArgs) Handles CmdExportar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim Dato
        Dim item As Integer = Me.Tabla.Columns.Count
        RadMessageBox.Show("Este Proceso puede tardar dependiendo de la información a exportar, presione Aceptar y espere a que el proceso termine...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Exportando reporte de Facturas por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.Tabla.RowCount - 1
        Dim Excel As Excel.Application = Eventos.NuevoExcelM("ReporteFacturas", False)
        For j As Integer = 1 To Me.Tabla.ColumnCount - 1
            Eventos.EscribeExcel(Excel, 1, j, Me.Tabla.Columns(j).HeaderText)
        Next
        For i As Integer = 0 To Me.Tabla.RowCount - 1
            For j As Integer = 1 To Me.Tabla.Columns.Count - 1
                Try
                    If Me.Tabla.Item(j, i).Value Is Nothing Then
                        Dato = ""
                    Else
                        If Me.Tabla.Item(j, i).Value.ToString <> "" Then
                            Dato = Me.Tabla.Item(j, i).Value.ToString
                        End If
                    End If
                    Eventos.EscribeExcel(Excel, i + 2, j, Valor(Dato))
                Catch ex As Exception

                End Try
            Next
            For j As Integer = 0 To NsC.Count - 1

                If NsC(j).UUIDR = Me.Tabla.Item(UUID.Index, i).Value.ToString Then
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Tipo")
                    Eventos.EscribeExcel(Excel, i + 2, item, "Nota")
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "UUID")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).UUID)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Fecha de Emision")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).Fecha_Emision)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Fecha de Timbrado")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).Fecha_Timbrado)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Serie")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).Serie)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Folio")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).Folio)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Conceptos")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).Conceptos)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "RFC Receptor")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).RFC_Receptor)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Nombre Receptor")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).Nombre_Receptor)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Subtotal")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).SubTotal)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Descuento")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).Descuento)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Total IEPS")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).Total_IEPS)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "IVA 16%")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).IVA_16)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "IVA 8%")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).IVA_8)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Retenido IVA")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).Retenido_IVA)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Retenido ISR")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).Retenido_ISR)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "ISH")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).ISH)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Total")
                    Eventos.EscribeExcel(Excel, i + 2, item, NsC(j).Total)
                    item += 1

                End If
            Next
            For j As Integer = 0 To Pgo.Count - 1
                If Pgo(j).UUIDR = Me.Tabla.Item(UUID.Index, i).Value.ToString Then
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Tipo")
                    Eventos.EscribeExcel(Excel, i + 2, item, "Pago")
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "UUID")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).UUID)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Fecha")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).Fecha)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Forma de Pago")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).Forma)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Monto")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).Monto)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "UUID Relacionado")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).UUIDR)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Numero de Parcialidad")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).NumPar)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Importe Anteriro")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).ImporteAnterior)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Importe Pagado")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).ImportePagado)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Importe Saldo Insoluto")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).ImporteSaldoInsoluto)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Subtotal")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).SubTotal)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Descuento")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).Descuento)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Total IEPS")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).Total_IEPS)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "IVA 16%")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).IVA16)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "IVA 8%")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).IVA8)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Retenido IVA")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).Retenido_IVA)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "Retenido ISR")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).Retenido_ISR)
                    item += 1
                    Eventos.EscribeExcel(Excel, 1, item, "ISH")
                    Eventos.EscribeExcel(Excel, i + 2, item, Pgo(j).ISH)
                    item += 1

                End If
            Next
            item = Me.Tabla.Columns.Count
            frm.Barra.Value = i
        Next
        frm.Close()
        ' Pagos
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Excel.Visible = True
        Excel = Nothing

    End Sub
    Private Function Saldos(ByVal UUID) As Decimal
        For j As Integer = 0 To Pgo.Count - 1
            If Pgo(j).UUIDR = UUID Then
                Saldos += Pgo(j).ImportePagado
            End If
        Next
        Return Saldos
    End Function
    Private Function SaldosNsC(ByVal UUID) As Decimal
        For j As Integer = 0 To NsC.Count - 1
            If NsC(j).UUIDR = UUID Then
                SaldosNsC += NsC(j).Total
            End If
        Next
        Return SaldosNsC
    End Function
    Private Sub CmdResumen_Click(sender As Object, e As EventArgs) Handles CmdResumen.Click

        Me.Tabla2.Enabled = False
        Calcula2()
        S2.RunWorkerAsync(Me.Tabla2)
        Control.CheckForIllegalCrossThreadCalls = False
        Me.Tabla2.Enabled = True

    End Sub
    Private Sub CmdUsos_Click(sender As Object, e As EventArgs) Handles CmdUsos.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Me.Tabla3.Enabled = False
        If Me.Tabla2.RowCount > 0 Then
            Calcula3()
            S3.RunWorkerAsync(Me.Tabla3)
            Control.CheckForIllegalCrossThreadCalls = False
            Me.Tabla3.Enabled = True
        Else
            RadMessageBox.Show("Debes Calcular primero el Resumen PUE / Pago / PPD.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub
    Private Sub CmdBuscarF_Click(sender As Object, e As EventArgs) Handles CmdBuscarF.Click
        Me.Tabla4.Enabled = False
        If Me.Tabla2.RowCount > 0 Then
            Calcula4()
            S4.RunWorkerAsync(Me.Tabla4)
            Control.CheckForIllegalCrossThreadCalls = False
            Me.Tabla4.Enabled = True
        Else
            RadMessageBox.Show("Debes Calcular primero el Resumen PUE / Pago / PPD.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub
    Private Sub SegundoPlano_DoWork(sender As Object, e As DoWorkEventArgs) Handles SegundoPlano.DoWork
        My.Forms.Inicio.txtServerDB.Text = serV
        Try
            Calcular1(DsF)
            DsF.Clear()
            TotalesFacturas()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub TotalesFacturas()
        Me.Tabla.Rows.Add("", "", "", "", "", "", "", "", "", "", "Totales:", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "", "")
        For i As Integer = 0 To Me.Tabla.Rows.Count - 2
            For j As Integer = 11 To 22
                Me.Tabla.Item(j, Me.Tabla.Rows.Count - 1).Value += Me.Tabla.Item(j, i).Value
            Next
        Next
    End Sub
    Private Sub S2_DoWork(sender As Object, e As DoWorkEventArgs) Handles S2.DoWork
        My.Forms.Inicio.txtServerDB.Text = serV
        Calcular2()
    End Sub
    Private Sub CmdExcel_Click(sender As Object, e As EventArgs) Handles CmdExcel.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Exportando reporte de Facturas PUE, PPD Y Pagos por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = 3
        Dim Excel As Excel.Application = Eventos.NuevoExcel("ResumenPUEPPD", False)
#Region "Pasar Datos PUE"

        Dim Data As New DataTable()
        For Each Pr As PropertyInfo In DetallePue(0).GetType().GetProperties
            Data.Columns.Add(New DataColumn(Pr.Name, Pr.PropertyType))
        Next


        For i As Integer = 0 To DetallePue.Count - 1
            Dim NFila As DataRow = Data.NewRow()
            For Each Pr As PropertyInfo In DetallePue(i).GetType().GetProperties
                NFila(Pr.Name) = Pr.GetValue(DetallePue(i), Nothing)
            Next
            Data.Rows.Add(NFila)
        Next
        Me.Puente.DataSource = Data
        Try
            Me.Puente.SelectAll()
            Dim dataObj As DataObject = Me.Puente.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "A2", "Resumen PUE-PPD-Pagos")
            frm.Barra.Value += 1
        Catch ex As Exception

        End Try
        Data.Clear()

        For i As Integer = 0 To DetallePuePAGO.Count - 1
            Dim NFila As DataRow = Data.NewRow()
            For Each Pr As PropertyInfo In DetallePuePAGO(i).GetType().GetProperties
                NFila(Pr.Name) = Pr.GetValue(DetallePuePAGO(i), Nothing)
            Next
            Data.Rows.Add(NFila)
        Next
        Me.Puente.DataSource = Data
        Try
            Me.Puente.SelectAll()
            Dim dataObj As DataObject = Me.Puente.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "P2", "Resumen PUE-PPD-Pagos")
            frm.Barra.Value += 1
        Catch ex As Exception

        End Try
        Data.Clear()

        For i As Integer = 0 To DetallePuePPD.Count - 1
            Dim NFila As DataRow = Data.NewRow()
            For Each Pr As PropertyInfo In DetallePuePPD(i).GetType().GetProperties
                NFila(Pr.Name) = Pr.GetValue(DetallePuePPD(i), Nothing)
            Next
            Data.Rows.Add(NFila)
        Next
        Me.Puente.DataSource = Data
        Try
            Me.Puente.SelectAll()
            Dim dataObj As DataObject = Me.Puente.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "AE2", "Resumen PUE-PPD-Pagos")
            frm.Barra.Value += 1
        Catch ex As Exception

        End Try
        Data.Clear()

        For i As Integer = 0 To DetalleNotas.Count - 1
            Dim NFila As DataRow = Data.NewRow()
            For Each Pr As PropertyInfo In DetalleNotas(i).GetType().GetProperties
                NFila(Pr.Name) = Pr.GetValue(DetalleNotas(i), Nothing)
            Next
            Data.Rows.Add(NFila)
        Next
        Me.Puente.DataSource = Data
        Try
            Me.Puente.SelectAll()
            Dim dataObj As DataObject = Me.Puente.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "AT2", "Resumen PUE-PPD-Pagos")
            frm.Barra.Value += 1
        Catch ex As Exception

        End Try
        frm.Close()
#End Region

        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Excel.Visible = True
        Excel = Nothing
    End Sub

    Private Sub S3_DoWork(sender As Object, e As DoWorkEventArgs) Handles S3.DoWork
        My.Forms.Inicio.txtServerDB.Text = serV
        Try
            Calcular3(DsF)
            DsF.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub S4_DoWork(sender As Object, e As DoWorkEventArgs) Handles S4.DoWork
        My.Forms.Inicio.txtServerDB.Text = serV
        Try
            Calcular4(DsF)
            DsF.Clear()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmdExportarU_Click(sender As Object, e As EventArgs) Handles CmdExportarU.Click

        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Exportando reporte de Usos PUE, PPD Y Pagos por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = 3
        Dim Excel As Excel.Application = Eventos.NuevoExcel("ResumenUsos", False)
#Region "Pasar Datos PUE"

        Dim Data As New DataTable()
        For Each Pr As PropertyInfo In DetalleUsos(0).GetType().GetProperties
            Data.Columns.Add(New DataColumn(Pr.Name, Pr.PropertyType))
        Next


        For i As Integer = 0 To DetalleUsos.Count - 1
            Dim NFila As DataRow = Data.NewRow()
            For Each Pr As PropertyInfo In DetalleUsos(i).GetType().GetProperties
                NFila(Pr.Name) = Pr.GetValue(DetalleUsos(i), Nothing)
            Next
            Data.Rows.Add(NFila)
        Next
        Me.Puente.DataSource = Data
        Try
            Me.Puente.SelectAll()
            Dim dataObj As DataObject = Me.Puente.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "A2", "Resumen Usos PUE-PPD-Pagos")
            frm.Barra.Value += 1
        Catch ex As Exception

        End Try

        Data.Clear()

        For i As Integer = 0 To DetalleUsosP.Count - 1
            Dim NFila As DataRow = Data.NewRow()
            For Each Pr As PropertyInfo In DetalleUsosP(i).GetType().GetProperties
                NFila(Pr.Name) = Pr.GetValue(DetalleUsosP(i), Nothing)
            Next
            Data.Rows.Add(NFila)
        Next
        Me.Puente.DataSource = Data
        Try
            Me.Puente.SelectAll()
            Dim dataObj As DataObject = Me.Puente.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "Q2", "Resumen PUE-PPD-Pagos")
            frm.Barra.Value += 1
        Catch ex As Exception

        End Try
        Data.Clear()

        For i As Integer = 0 To DetalleUsosPPD.Count - 1
            Dim NFila As DataRow = Data.NewRow()
            For Each Pr As PropertyInfo In DetalleUsosPPD(i).GetType().GetProperties
                NFila(Pr.Name) = Pr.GetValue(DetalleUsosPPD(i), Nothing)
            Next
            Data.Rows.Add(NFila)
        Next
        Me.Puente.DataSource = Data
        Try
            Me.Puente.SelectAll()
            Dim dataObj As DataObject = Me.Puente.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "AG2", "Resumen PUE-PPD-Pagos")
            frm.Barra.Value += 1
        Catch ex As Exception

        End Try
        Data.Clear()

        For i As Integer = 0 To DetalleUsosNotas.Count - 1
            Dim NFila As DataRow = Data.NewRow()
            For Each Pr As PropertyInfo In DetalleUsosNotas(i).GetType().GetProperties
                NFila(Pr.Name) = Pr.GetValue(DetalleUsosNotas(i), Nothing)
            Next
            Data.Rows.Add(NFila)
        Next
        Me.Puente.DataSource = Data
        Try
            Me.Puente.SelectAll()
            Dim dataObj As DataObject = Me.Puente.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "AW2", "Resumen PUE-PPD-Pagos")
            frm.Barra.Value += 1
        Catch ex As Exception

        End Try
        frm.Close()
#End Region

        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Excel.Visible = True
        Excel = Nothing
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim SQL As String = "Delete from PagosXML where Id_Empresa =" & Me.lstCliente.SelectItem & ""
        Eventos.Comando_sql(SQL)
        For i As Integer = 0 To Me.Tabla.RowCount - 1
            If Me.Tabla.Item(Diferencia.Index, i).Value <> 0 Then
                SQL = " INSERT INTO dbo.PagosXML 	(Id_Empresa,  UUID,Diferencia	) VALUES (" & Me.lstCliente.SelectItem & ",'" & Me.Tabla.Item(UUID.Index, i).Value & "'," & Me.Tabla.Item(Diferencia.Index, i).Value & " )"
                Try
                    Eventos.Comando_sql(SQL)
                Catch ex As Exception
                End Try
            End If
        Next
        RadMessageBox.Show("Proceso de Guardado Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
    End Sub


    Private Sub PintarPPDS()

    End Sub

    Private Sub CmdExportarf_Click(sender As Object, e As EventArgs) Handles CmdExportarf.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Exportando reporte de Formas PUE, PPD Y Pagos por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = 3
        Dim Excel As Excel.Application = Eventos.NuevoExcel("ResumenFormas", False)


        Dim Data As New DataTable()
        For Each Pr As PropertyInfo In DetalleM(0).GetType().GetProperties
            Data.Columns.Add(New DataColumn(Pr.Name, Pr.PropertyType))
        Next


        For i As Integer = 0 To DetalleM.Count - 1
            Dim NFila As DataRow = Data.NewRow()
            For Each Pr As PropertyInfo In DetalleM(i).GetType().GetProperties
                NFila(Pr.Name) = Pr.GetValue(DetalleM(i), Nothing)
            Next
            Data.Rows.Add(NFila)
        Next
        Me.Puente.DataSource = Data
        Try
            Me.Puente.SelectAll()
            Dim dataObj As DataObject = Me.Puente.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "A2", "Resumen")
            frm.Barra.Value += 1
        Catch ex As Exception

        End Try
        Data.Clear()

        For i As Integer = 0 To DetalleMP.Count - 1
            Dim NFila As DataRow = Data.NewRow()
            For Each Pr As PropertyInfo In DetalleMP(i).GetType().GetProperties
                NFila(Pr.Name) = Pr.GetValue(DetalleMP(i), Nothing)
            Next
            Data.Rows.Add(NFila)
        Next
        Me.Puente.DataSource = Data
        Try
            Me.Puente.SelectAll()
            Dim dataObj As DataObject = Me.Puente.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "P2", "Resumen")
            frm.Barra.Value += 1
        Catch ex As Exception

        End Try
        Data.Clear()

        For i As Integer = 0 To DetalleMPPD.Count - 1
            Dim NFila As DataRow = Data.NewRow()
            For Each Pr As PropertyInfo In DetalleMPPD(i).GetType().GetProperties
                NFila(Pr.Name) = Pr.GetValue(DetalleMPPD(i), Nothing)
            Next
            Data.Rows.Add(NFila)
        Next
        Me.Puente.DataSource = Data
        Try
            Me.Puente.SelectAll()
            Dim dataObj As DataObject = Me.Puente.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "AE2", "Resumen")
            frm.Barra.Value += 1
        Catch ex As Exception

        End Try
        Data.Clear()

        For i As Integer = 0 To DetalleMNotas.Count - 1
            Dim NFila As DataRow = Data.NewRow()
            For Each Pr As PropertyInfo In DetalleMNotas(i).GetType().GetProperties
                NFila(Pr.Name) = Pr.GetValue(DetalleMNotas(i), Nothing)
            Next
            Data.Rows.Add(NFila)
        Next
        Me.Puente.DataSource = Data
        Try
            Me.Puente.SelectAll()
            Dim dataObj As DataObject = Me.Puente.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "AT2", "Resumen")
            frm.Barra.Value += 1
        Catch ex As Exception

        End Try
        frm.Close()


        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Excel.Visible = True
        Excel = Nothing
    End Sub

    Private Sub RadE_CheckedChanged(sender As Object, e As EventArgs) Handles RadE.CheckedChanged
        Me.Tabla.Rows.Clear()
        Me.Tabla2.Rows.Clear()
        Me.Tabla3.Rows.Clear()
        Me.Tabla4.Rows.Clear()
    End Sub

    Private Sub RadR_CheckedChanged(sender As Object, e As EventArgs) Handles RadR.CheckedChanged
        Me.Tabla.Rows.Clear()
        Me.Tabla2.Rows.Clear()
        Tabla3.Rows.Clear()
        Tabla4.Rows.Clear()
    End Sub
End Class