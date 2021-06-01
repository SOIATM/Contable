Imports System
Imports System.IO
Imports CrystalDecisions.Shared
Imports ThoughtWorks.QRCode
Imports ThoughtWorks.QRCode.Codec
Imports System.Xml
Imports Telerik.WinControls
Public Class Codificar_XML
    Dim Activo As Boolean
    Private ColorFondoQR As Integer = Color.FromArgb(255, 255, 255, 255).ToArgb ' Blanco
    Private colorQR As Integer = Color.FromArgb(255, 0, 0, 0).ToArgb 'Negro
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub
    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        If Me.TCPrincipal.SelectedIndex = 0 Then
            If Me.Tabla1.Rows.Count > 0 Then
                Me.Tabla1.Rows.Clear()
                Me.lblRegistros.Text = "Total de Registros: "
            End If
        Else
            If Me.Tabla4.Rows.Count > 0 Then
                Me.Tabla4.Rows.Clear()
                Me.lblRegistros.Text = "Total de Registros: "
            End If
        End If
    End Sub
    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaFaturasPagos.SelectedIndex = 0 Then
            Try
                If Me.Tabla1.Rows.Count > 0 Then
                    Me.Tabla1.Rows.Clear()
                End If
            Catch ex As Exception

            End Try
            'Facturas
            Dim contador As Integer = 0
            Dim archivo As String = ""
            archivo = Eventos.Boveda(My.Forms.Inicio.LblUsuario.Text)
            Dim ruta As String
            Dim tipo As String = ""

            If Me.ComboMesIn.Text <> "" And Me.ComboMesFin.Text <> "" Then
                If Me.ComboMesIn.Text <= Me.ComboMesFin.Text Then ' seleccion correcta del periodo
                    Dim Tam As Integer = Convert.ToInt32(Me.ComboMesFin.Text) - Convert.ToInt32(Me.ComboMesIn.Text)
                    Dim meces(Tam) As String
                    For i As Integer = Convert.ToInt32(Me.ComboMesIn.Text) - 1 To Convert.ToInt32(Me.ComboMesIn.Text) + Tam - 1

                        meces(contador) = Me.ComboMesFin.Items(i).ToString()
                        contador = contador + 1
                    Next

                    For Each Va In meces


                        If Me.RadEmitidas.Checked = True Then
                            tipo = "Emitidas"
                        Else
                            tipo = "Recibidas"
                        End If

                        Dim cadena As String = "\" & Trim(CalculaFRC(Me.lstCliente.SelectItem)) & "\" & tipo & "\" & Trim(Me.comboAño.Text) & "\" & Va
                        ruta = archivo & cadena
                        If archivo = "" Then
                            RadMessageBox.SetThemeName("MaterialBlueGrey")
                            RadMessageBox.Show("No se ha asignado una Boveda...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                            Exit Sub
                        Else
                            Try
                                Dim Fila As Integer = 0

                                'Dim cant() As String = Directory.GetFiles(archivo, "*.xml", False)
                                'Dim cuanto As Integer = cant.Length

                                Me.Barra.Maximum = Convert.ToInt32(Directory.GetFiles(ruta, "*.xml", False).Count - 1)
                                Me.Barra.Minimum = 0
                                Me.Barra.Value2 = 0
                                Me.Barra.Value1 = 0
                                For Each Item As String In Directory.GetFiles(ruta, "*.xml", False)
                                    Dim nombre As String = My.Computer.FileSystem.GetName(Item)

                                    Try

                                        Dim xDoc = New XmlDocument()
                                        xDoc.Load(Item)

                                        If xDoc.GetElementsByTagName("cfdi:Comprobante") IsNot Nothing Then



                                            Dim Comprobante As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Comprobante")


                                            If CType(Comprobante(0), XmlElement).GetAttribute("TipoDeComprobante") = "I" Then
                                                Me.Tabla1.Rows.Add()
                                                Dim ImpImpuesto16 As Decimal = 0
                                                Dim ImpImpuesto8 As Decimal = 0
                                                Dim ImpIeps6 As Decimal = 0
                                                Dim ImpIeps3 As Decimal = 0
                                                Dim ImpIeps7 As Decimal = 0
                                                Dim ImpIeps8 As Decimal = 0
                                                Dim ImpIeps9 As Decimal = 0
                                                Dim ImpIeps26 As Decimal = 0
                                                Dim ImpIeps30 As Decimal = 0
                                                Dim ImpIeps53 As Decimal = 0
                                                Dim ImpIeps160 As Decimal = 0
                                                Select Case Trim(CType(Comprobante(0), XmlElement).GetAttribute("TipoDeComprobante"))
                                                    Case "I"
                                                        'E es Nota , N es para tipo Nomina
                                                        Me.Tabla1.Item(Tip.Index, Fila).Value = "Factura"
                                                    Case "P"
                                                        Me.Tabla1.Item(Tip.Index, Fila).Value = "Complemento"
                                                    Case "E"
                                                        Me.Tabla1.Item(Tip.Index, Fila).Value = "NotaCredito"
                                                    Case "N"
                                                        Me.Tabla1.Item(Tip.Index, Fila).Value = "Nomina"
                                                End Select
                                                Me.Tabla1.Item(Versio.Index, Fila).Value = CType(Comprobante(0), XmlElement).GetAttribute("Version")
                                                If Item.Contains("@2") Then
                                                    Me.Tabla1.Item(EstadoSAT.Index, Fila).Value = "Cancelado"
                                                Else
                                                    Me.Tabla1.Item(EstadoSAT.Index, Fila).Value = "Vigente"
                                                End If
                                                Me.Tabla1.Item(FechaEmision.Index, Fila).Value = Convert.ToString(CType(Comprobante(0), XmlElement).GetAttribute("Fecha")).Substring(0, 10) '.ToString("dd/MM/yyyy")
                                                Me.Tabla1.Item(Serie.Index, Fila).Value = CType(Comprobante(0), XmlElement).GetAttribute("Serie")
                                                Me.Tabla1.Item(Folio.Index, Fila).Value = CType(Comprobante(0), XmlElement).GetAttribute("Folio")
                                                Me.Tabla1.Item(LugarDeExpedicion.Index, Fila).Value = CType(Comprobante(0), XmlElement).GetAttribute("LugarExpedicion")
                                                Me.Tabla1.Item(SubTotal.Index, Fila).Value = CType(Comprobante(0), XmlElement).GetAttribute("SubTotal")
                                                Me.Tabla1.Item(SubTotal.Index, Fila).Value *= 1
                                                Try
                                                    Me.Tabla1.Item(Descuento.Index, Fila).Value = CType(Comprobante(0), XmlElement).GetAttribute("Descuento")
                                                    Me.Tabla1.Item(Descuento.Index, Fila).Value *= 1
                                                Catch ex As Exception
                                                    Me.Tabla1.Item(Descuento.Index, Fila).Value = 0
                                                End Try

                                                Me.Tabla1.Item(TotalIEPS.Index, Fila).Value = 0
                                                Me.Tabla1.Item(Total.Index, Fila).Value = CType(Comprobante(0), XmlElement).GetAttribute("Total")
                                                Me.Tabla1.Item(Total.Index, Fila).Value *= 1


                                                Me.Tabla1.Item(Moneda.Index, Fila).Value = CType(Comprobante(0), XmlElement).GetAttribute("Moneda")
                                                Me.Tabla1.Item(TipoDeCambio.Index, Fila).Value = 0
                                                Me.Tabla1.Item(FormaDePago.Index, Fila).Value = CType(Comprobante(0), XmlElement).GetAttribute("FormaPago")
                                                Me.Tabla1.Item(MetododePago.Index, Fila).Value = CType(Comprobante(0), XmlElement).GetAttribute("MetodoPago")
                                                Me.Tabla1.Item(NumCtaPago.Index, Fila).Value = ""
                                                Me.Tabla1.Item(CondiciondePago.Index, Fila).Value = CType(Comprobante(0), XmlElement).GetAttribute("CondicionesDePago")

                                                If xDoc.GetElementsByTagName("cfdi:Emisor") IsNot Nothing Then
                                                    Dim Emisor As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Emisor")
                                                    Me.Tabla1.Item(RFCEmisor.Index, Fila).Value = CType(Emisor(0), XmlElement).GetAttribute("Rfc")
                                                    Me.Tabla1.Item(NombreEmisor.Index, Fila).Value = CType(Emisor(0), XmlElement).GetAttribute("Nombre")
                                                End If
                                                If xDoc.GetElementsByTagName("cfdi:Receptor") IsNot Nothing Then
                                                    Dim Receptor As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Receptor")
                                                    Me.Tabla1.Item(RFCReceptor.Index, Fila).Value = CType(Receptor(0), XmlElement).GetAttribute("Rfc")
                                                    Me.Tabla1.Item(NombreReceptor.Index, Fila).Value = CType(Receptor(0), XmlElement).GetAttribute("Nombre")
                                                    Me.Tabla1.Item(UsoCFDI.Index, Fila).Value = CType(Receptor(0), XmlElement).GetAttribute("UsoCFDI")

                                                End If
                                                Dim ImpImpuesto0 As Decimal = 0
                                                Dim ImpImpuestoE As Decimal = 0

                                                If xDoc.GetElementsByTagName("cfdi:Conceptos") IsNot Nothing Then


                                                    Dim Nodos As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Traslados")
                                                    For N As Integer = 0 To Nodos.Count - 1
                                                        Dim Valores As XmlNode = Nodos(N).LastChild
                                                        Try
                                                            If Valores.Attributes("TipoFactor").Value = "Exento" Then
                                                                ImpImpuestoE += Valores.Attributes("Base").Value
                                                            ElseIf Valores.Attributes("TipoFactor").Value = "Tasa" And Valores.Attributes("TasaOCuota").Value = "0.000000" Then
                                                                ImpImpuesto0 += Valores.Attributes("Base").Value
                                                            End If
                                                        Catch ex As Exception

                                                        End Try
                                                    Next


                                                    Dim Concepto As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Concepto")




                                                    For w As Integer = 0 To Concepto.Count - 1

                                                        Try
                                                            Me.Tabla1.Item(Conceptos.Index, Fila).Value = Me.Tabla1.Item(Conceptos.Index, Fila).Value & CType(Concepto(w), XmlElement).GetAttribute("Descripcion") & " * "
                                                            Try
                                                                If Convert.ToString(CType(Concepto(w), XmlElement).GetAttribute("ClaveProdServ")).Substring(0, 6) = "151015" Then
                                                                    Me.Tabla1.Item(Combustible.Index, Fila).Value = "SI"
                                                                Else
                                                                    Me.Tabla1.Item(Combustible.Index, Fila).Value = "NO"
                                                                End If
                                                            Catch ex As Exception
                                                                Me.Tabla1.Item(Combustible.Index, Fila).Value = "NO"
                                                            End Try



                                                        Catch ex As Exception

                                                        End Try
                                                    Next

                                                End If
                                                If xDoc.GetElementsByTagName("cfdi:Conceptos") IsNot Nothing Then
                                                    Dim Conceptos As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Concepto")




                                                    For Each N In Conceptos

                                                        Dim Traslados As XmlNodeList = N.GetElementsByTagName("cfdi:Traslado")

                                                        For Each T In Traslados
                                                            Try
                                                                If Convert.ToString(T.Attributes("Impuesto").Value) = "002" Then
                                                                    If T.Attributes("TasaOCuota").Value = 0.08 Then
                                                                        ImpImpuesto8 += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                                    ElseIf T.Attributes("TasaOCuota").Value = 0.16 Then
                                                                        ImpImpuesto16 += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                                    End If
                                                                End If
                                                            Catch ex As Exception

                                                            End Try
                                                            Try
                                                                Me.Tabla1.Item(TotalTrasladados.Index, Fila).Value += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                            Catch ex As Exception
                                                                Me.Tabla1.Item(TotalTrasladados.Index, Fila).Value += 0
                                                            End Try

                                                            Try
                                                                If Convert.ToString(T.Attributes("Impuesto").VALUE) = "003" Then
                                                                    Select Case Convert.ToDecimal(T.Attributes("TasaOCuota").Value)
                                                                        Case 0.06
                                                                            ImpIeps6 += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                                        Case 0.03
                                                                            ImpIeps3 += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                                        Case 0.07
                                                                            ImpIeps7 += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                                        Case 0.08
                                                                            ImpIeps8 += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                                        Case 0.09
                                                                            ImpIeps9 += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                                        Case 0.26
                                                                            ImpIeps26 += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                                        Case 0.3
                                                                            ImpIeps30 += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                                        Case 0.53
                                                                            ImpIeps53 += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                                        Case 1.6
                                                                            ImpIeps160 += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                                        Case Else
                                                                            Me.Tabla1.Item(otieps.Index, Fila).Value += Convert.ToDecimal(T.Attributes("Importe").Value)
                                                                    End Select
                                                                End If
                                                            Catch ex As Exception

                                                            End Try
                                                        Next



                                                    Next

                                                    Me.Tabla1.Item(IVA8.Index, Fila).Value = ImpImpuesto8
                                                    Me.Tabla1.Item(IVA16.Index, Fila).Value = ImpImpuesto16
                                                    Me.Tabla1.Item(IEPS3.Index, Fila).Value = ImpIeps3
                                                    Me.Tabla1.Item(IEPS6.Index, Fila).Value = ImpIeps6
                                                    Me.Tabla1.Item(IEPS7.Index, Fila).Value = ImpIeps7
                                                    Me.Tabla1.Item(IEPS8.Index, Fila).Value = ImpIeps8
                                                    Me.Tabla1.Item(IEPS9.Index, Fila).Value = ImpIeps9
                                                    Me.Tabla1.Item(IEPS26.Index, Fila).Value = ImpIeps26
                                                    Me.Tabla1.Item(IEPS30.Index, Fila).Value = ImpIeps30
                                                    Me.Tabla1.Item(IEPS53.Index, Fila).Value = ImpIeps53
                                                    Me.Tabla1.Item(IEPS160.Index, Fila).Value = ImpIeps160
                                                    Me.Tabla1.Item(IEPS304.Index, Fila).Value = 0
                                                    Me.Tabla1.Item(ISH.Index, Fila).Value = 0
                                                End If



                                                If xDoc.GetElementsByTagName("cfdi:Impuestos") IsNot Nothing Then
                                                    Dim Impuestos As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Impuestos")



                                                    Try
                                                        Me.Tabla1.Item(TotalTrasladados.Index, Fila).Value = IIf(CType(Impuestos(0), XmlElement).GetAttribute("TotalImpuestosTrasladados") IsNot Nothing, CType(Impuestos(0), XmlElement).GetAttribute("TotalImpuestosTrasladados"), 0)
                                                        Me.Tabla1.Item(TotalRetenidos.Index, Fila).Value = IIf(CType(Impuestos(0), XmlElement).GetAttribute("TotalImpuestosRetenidos") IsNot Nothing, CType(Impuestos(0), XmlElement).GetAttribute("TotalImpuestosRetenidos"), 0)
                                                        If Me.Tabla1.Item(TotalRetenidos.Index, Fila).Value > 0 Then
                                                            Dim importe = 0
                                                        End If
                                                    Catch ex As Exception

                                                    End Try
                                                    Me.Tabla1.Item(TotalLocalTrasladado.Index, Fila).Value = 0

                                                    Me.Tabla1.Item(TotalLocalRetenido.Index, Fila).Value = 0
                                                    Me.Tabla1.Item(Complemento.Index, Fila).Value = 0
                                                    Dim t
                                                    Me.Tabla1.Item(TotalTrasladados.Index, Fila).Value = 0
                                                    Me.Tabla1.Item(TotalRetenidos.Index, Fila).Value = 0
                                                    Me.Tabla1.Item(RetenidoIVA.Index, Fila).Value = 0
                                                    Me.Tabla1.Item(RetenidoISR.Index, Fila).Value = 0
                                                    Try

                                                        Dim Nodos As XmlNodeList = Impuestos(0).ChildNodes


                                                        Try
                                                            Dim Retenciones As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Retenciones")
                                                            Nodos = Retenciones(Retenciones.Count - 1).ChildNodes

                                                            For N As Integer = 0 To Nodos.Count - 1
                                                                If Convert.ToString(Nodos(N).Attributes("Impuesto").Value) = "001" Then
                                                                    Me.Tabla1.Item(RetenidoISR.Index, Fila).Value += Convert.ToDecimal(Nodos(N).Attributes("Importe").Value)
                                                                ElseIf Convert.ToString(Nodos(N).Attributes("Impuesto").Value) = "002" Then
                                                                    Me.Tabla1.Item(RetenidoIVA.Index, Fila).Value += Convert.ToDecimal(Nodos(N).Attributes("Importe").Value)
                                                                End If
                                                                Me.Tabla1.Item(TotalRetenidos.Index, Fila).Value += Convert.ToDecimal(Nodos(N).Attributes("Importe").Value)
                                                            Next
                                                        Catch ex As Exception

                                                        End Try


                                                    Catch ex As Exception
                                                        ImpImpuesto8 += 0
                                                        ImpImpuesto16 += 0
                                                    End Try



                                                End If

                                                Try

                                                    If xDoc.GetElementsByTagName("implocal:TrasladosLocales") IsNot Nothing Then
                                                        Dim Timbre As XmlNodeList = xDoc.GetElementsByTagName("implocal:TrasladosLocales")
                                                        For ia As Integer = 0 To Timbre.Count - 1
                                                            Me.Tabla1.Item(ISH.Index, Fila).Value += CType(Timbre(ia), XmlElement).GetAttribute("Importe")
                                                        Next
                                                    End If
                                                Catch ex As Exception

                                                End Try
                                                Me.Tabla1.Item(TotalOriginal.Index, Fila).Value = 0
                                                Me.Tabla1.Item(ArchivoXML.Index, Fila).Value = ""
                                                Me.Tabla1.Item(DireccionEmisor.Index, Fila).Value = ""
                                                Me.Tabla1.Item(LocalidadEmisor.Index, Fila).Value = ""
                                                Me.Tabla1.Item(DireccionReceptor.Index, Fila).Value = ""
                                                Me.Tabla1.Item(LocalidadReceptor.Index, Fila).Value = ""
                                                Me.Tabla1.Item(ru.Index, Fila).Value = ruta
                                                Me.Tabla1.Item(nom.Index, Fila).Value = nombre
                                                If xDoc.GetElementsByTagName("tfd:TimbreFiscalDigital") IsNot Nothing Then
                                                    Dim Timbre As XmlNodeList = xDoc.GetElementsByTagName("tfd:TimbreFiscalDigital")
                                                    Me.Tabla1.Item(FechaTimbrado.Index, Fila).Value = Convert.ToString(CType(Timbre(0), XmlElement).GetAttribute("FechaTimbrado")).Substring(0, 10)
                                                    Me.Tabla1.Item(UUID.Index, Fila).Value = CType(Timbre(0), XmlElement).GetAttribute("UUID")
                                                End If

                                                Fila += 1
                                            End If
                                        End If
                                    Catch ex As Exception

                                    End Try
                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow

                                        Me.Barra.Value1 = 0
                                    Else
                                        Me.Barra.Value1 += 1

                                    End If
                                Next
                            Catch ex As Exception
                                RadMessageBox.Show("No existe la Ruta " & ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                            End Try
                        End If

                        ruta = ""
                    Next

                    If Me.Tabla1.Rows.Count > 0 Then
                        Me.lblRegistros.Text = "Total de Registros: " & Me.Tabla1.Rows.Count
                    End If
                    Dim fr As New BarraProcesovb
                    fr.Barra.Minimum = 0
                    fr.Barra.Value = 0
                    fr.Text = "Buscando errores en XML"
                    fr.Barra.Maximum = Me.Tabla1.Rows.Count
                    For i As Integer = 0 To Me.Tabla1.Rows.Count - 1
                        Dim Fila As New DataGridViewRow
                        If Me.Tabla1.Item(UsoCFDI.Index, i).Value Is Nothing Then
                            Fila = Me.Tabla1.Rows(i)
                            Fila.DefaultCellStyle.BackColor = Color.Red
                        Else
                            If ValidarXMl(Me.Tabla1.Item(UsoCFDI.Index, i).Value.ToString().Trim(), Me.Tabla1.Item(Tip.Index, i).Value.ToString().Trim(), i, Me.Tabla1.Item(RFCReceptor.Index, i).Value.ToString().Trim(), Me.Tabla1.Item(Moneda.Index, i).Value.ToString().Trim(), Me.Tabla1.Item(MetododePago.Index, i).Value.ToString().Trim(), Me.Tabla1.Item(FormaDePago.Index, i).Value.ToString().Trim(), Me.Tabla1.Item(Total.Index, i).Value.ToString().Trim()) = "NO" Then
                                Fila = Me.Tabla1.Rows(i)
                                Fila.DefaultCellStyle.BackColor = Color.Red
                            End If
                        End If
                        If fr.Barra.Value = fr.Barra.Maximum Then
                            Me.Barra.Minimum = 0
                            Me.Cursor = Cursors.Arrow
                            MessageBox.Show("Busqueda completada...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            fr.Barra.Value = 0
                        Else
                            fr.Barra.Increment(1)
                        End If
                    Next
                Else
                    RadMessageBox.Show("El periodo esta mal seleccionado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                End If
            End If
        ElseIf Me.TablaFaturasPagos.SelectedIndex = 1 Then
            ' pagos
            'Facturas
            Dim contador As Integer = 0
            Dim archivo As String = ""
            archivo = Eventos.Boveda(My.Forms.Inicio.LblUsuario.Text)
            Dim ruta As String
            Dim tipo As String = ""
            If Me.ComboMesIn.Text <> "" And Me.ComboMesFin.Text <> "" Then
                If Me.ComboMesIn.Text <= Me.ComboMesFin.Text Then ' seleccion correcta del periodo
                    Dim Tam As Integer = Convert.ToInt32(Me.ComboMesFin.Text) - Convert.ToInt32(Me.ComboMesIn.Text)
                    Dim meces(Tam) As String
                    For i As Integer = Convert.ToInt32(Me.ComboMesIn.Text) - 1 To Convert.ToInt32(Me.ComboMesIn.Text) + Tam - 1

                        meces(contador) = Me.ComboMesFin.Items(i).ToString()
                        contador = contador + 1
                    Next
                    Me.Barra.Maximum = Convert.ToInt32(meces.Length - 1)
                    Me.Barra.Minimum = 0
                    Me.Barra.Value1 = 0
                    For Each Va As String In meces


                        If Me.RadEmitidas.Checked = True Then
                            tipo = "Emitidas"
                        Else
                            tipo = "Recibidas"
                        End If
                        Dim cadena As String = "\" & Trim(CalculaFRC(Me.lstCliente.SelectItem)) & "\" & tipo & "\" & Trim(Me.comboAño.Text) & "\" & Va
                        ruta = archivo & cadena
                        If archivo = "" Then
                            RadMessageBox.Show("No se ha asignado una Boveda...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                            Exit Sub
                        Else
                            Try


                                'Dim cant() As String = Directory.GetFiles(archivo, "*.xml", False)
                                'Dim cuanto As Integer = cant.Length
                                For Each item As String In Directory.GetFiles(ruta, "*.xml", False)

                                    Dim nombre As String = My.Computer.FileSystem.GetName(item)

                                    Dim sa As DataSet = Eventos.CargarXMLaDataSet(item)
                                    If sa.Tables(0).Rows(0)("TipoDeComprobante") = "P" Then



                                        If sa.Tables(0).Rows.Count > 0 Then
                                            Dim tiene As Boolean = False
                                            For i As Integer = 0 To Me.Tabla4.RowCount - 1
                                                If Me.Tabla4.Item(TipoPF.Index, i).Value = "" Then
                                                    tiene = True
                                                End If
                                            Next
                                            If tiene = False Then
                                                Me.Tabla4.Rows.Add()
                                            End If
                                            For i As Integer = 0 To Me.Tabla4.RowCount - 1


                                                If Me.Tabla4.Item(TipoPF.Index, i).Value = "" Then
                                                    'Me.Tabla4.Item(VerificadoAsoc.Index, i).Value = ""
                                                    'Me.Tabla4.Item(EstadoSAT.Index, i).Value = ""
                                                    'Me.Tabla4.Item(Versio.Index, i).Value = sa.Tables(0).Rows(0)("Version")
                                                    Select Case Trim(sa.Tables(0).Rows(0)("TipoDeComprobante"))
                                                        Case "I"
                                                            Me.Tabla4.Item(TipoPF.Index, i).Value = "Factura"
                                                        Case "P"
                                                            Me.Tabla4.Item(TipoPF.Index, i).Value = "Complemento"
                                                    End Select
                                                    Me.Tabla4.Item(UUIDpf.Index, i).Value = sa.Tables("TimbreFiscalDigital").Rows(0)("UUID")
                                                    Me.Tabla4.Item(Fecha_Emisionpf.Index, i).Value = sa.Tables(0).Rows(0)("Fecha")
                                                    Try
                                                        Me.Tabla4.Item(FolioPF.Index, i).Value = sa.Tables(0).Rows(0)("Folio")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(FolioPF.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(Seriepf.Index, i).Value = sa.Tables(0).Rows(0)("Serie")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(Seriepf.Index, i).Value = ""
                                                    End Try

                                                    Me.Tabla4.Item(Sub_Totalpf.Index, i).Value = sa.Tables(0).Rows(0)("SubTotal")
                                                    Me.Tabla4.Item(Monedapf.Index, i).Value = sa.Tables(0).Rows(0)("Moneda")
                                                    Me.Tabla4.Item(Totalpf.Index, i).Value = sa.Tables(0).Rows(0)("Total")
                                                    Me.Tabla4.Item(Lugar_Expedicionpf.Index, i).Value = sa.Tables(0).Rows(0)("LugarExpedicion")
                                                    Me.Tabla4.Item(RFC_Emisorpf.Index, i).Value = sa.Tables(1).Rows(0)("rfc")
                                                    Try
                                                        Me.Tabla4.Item(Nombre_Emisorpf.Index, i).Value = sa.Tables(1).Rows(0)("Nombre")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(Nombre_Emisorpf.Index, i).Value = ""
                                                    End Try
                                                    Me.Tabla4.Item(Regimen_Fiscalpf.Index, i).Value = sa.Tables(1).Rows(0)("RegimenFiscal")

                                                    Try
                                                        Me.Tabla4.Item(RFC_Receptorpf.Index, i).Value = sa.Tables(2).Rows(0)("rfc")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(RFC_Receptorpf.Index, i).Value = ""
                                                    End Try

                                                    Try
                                                        Me.Tabla4.Item(Nombre_Receptorpf.Index, i).Value = sa.Tables(2).Rows(0)("Nombre")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(Nombre_Receptorpf.Index, i).Value = ""
                                                    End Try
                                                    Me.Tabla4.Item(UsoCFDI.Index, i).Value = sa.Tables(2).Rows(0)("UsoCFDI")
                                                    Me.Tabla4.Item(Clave_Productopf.Index, i).Value = sa.Tables("Concepto").Rows(0)("ClaveProdServ")
                                                    Me.Tabla4.Item(Cantidadpf.Index, i).Value = sa.Tables("Concepto").Rows(0)("Cantidad")
                                                    Me.Tabla4.Item(Unidadpf.Index, i).Value = sa.Tables("Concepto").Rows(0)("ClaveUnidad")
                                                    Me.Tabla4.Item(Descripcionpf.Index, i).Value = sa.Tables("Concepto").Rows(0)("Descripcion")
                                                    Me.Tabla4.Item(Valor_Unitariopf.Index, i).Value = sa.Tables("Concepto").Rows(0)("ValorUnitario")
                                                    Me.Tabla4.Item(Importepf.Index, i).Value = sa.Tables("Concepto").Rows(0)("Importe")
                                                    Me.Tabla4.Item(FechaDePagopf.Index, i).Value = sa.Tables("Pago").Rows(0)("FechaPago")
                                                    Me.Tabla4.Item(FormaDePagopf.Index, i).Value = sa.Tables("Pago").Rows(0)("FormaDePagoP")
                                                    Me.Tabla4.Item(MonedaPpf.Index, i).Value = sa.Tables("Pago").Rows(0)("MonedaP")
                                                    Try
                                                        Me.Tabla4.Item(TipoCambioPpf.Index, i).Value = sa.Tables("Pago").Rows(0)("TipoCambioP")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(TipoCambioPpf.Index, i).Value = 0
                                                    End Try

                                                    Try
                                                        Me.Tabla4.Item(NumOperacionpf.Index, i).Value = sa.Tables("Pago").Rows(0)("NumOperacion")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(NumOperacionpf.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(RfcEmisorCtaOrdpf.Index, i).Value = sa.Tables("Pago").Rows(0)("RfcEmisorCtaOrd")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(RfcEmisorCtaOrdpf.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(NomBancoOrdExtpf.Index, i).Value = sa.Tables("Pago").Rows(0)("NomBancoOrdExt")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(NomBancoOrdExtpf.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(RfcEmisorCtaBenpf.Index, i).Value = sa.Tables("Pago").Rows(0)("RfcEmisorCtaBen")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(RfcEmisorCtaBenpf.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(RfcEmisorCtaBenpf.Index, i).Value = sa.Tables("Pago").Rows(0)("RfcEmisorCtaBen")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(RfcEmisorCtaBenpf.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(CtaBeneficiariopf.Index, i).Value = sa.Tables("Pago").Rows(0)("CtaBeneficiario")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(CtaBeneficiariopf.Index, i).Value = ""
                                                    End Try



                                                    Try
                                                        Me.Tabla4.Item(TipoCadPagopf.Index, i).Value = sa.Tables("Pago").Rows(0)("TipoCadPago")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(TipoCadPagopf.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(CertPagopf.Index, i).Value = sa.Tables("Pago").Rows(0)("CertPago")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(CertPagopf.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(CadPagopf.Index, i).Value = sa.Tables("Pago").Rows(0)("CadPago")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(CadPagopf.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(SelloPagopf.Index, i).Value = sa.Tables("Pago").Rows(0)("SelloPago")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(SelloPagopf.Index, i).Value = ""
                                                    End Try

                                                    Try
                                                        Me.Tabla4.Item(IdDocumentopf.Index, i).Value = sa.Tables("Pago").Rows(0)("IdDocumento")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(IdDocumentopf.Index, i).Value = ""
                                                    End Try

                                                    Try
                                                        Me.Tabla4.Item(SerieDRpf.Index, i).Value = sa.Tables("Pago").Rows(0)("SerieDR")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(SerieDRpf.Index, i).Value = ""
                                                    End Try

                                                    Try
                                                        Me.Tabla4.Item(FolioDRpf.Index, i).Value = sa.Tables("Pago").Rows(0)("FolioDR")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(FolioDRpf.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(MonedaDRpf.Index, i).Value = sa.Tables("Pago").Rows(0)("MonedaDR")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(MonedaDRpf.Index, i).Value = ""
                                                    End Try

                                                    Try
                                                        Me.Tabla4.Item(Montopf.Index, i).Value = sa.Tables("Pago").Rows(0)("Monto")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(Montopf.Index, i).Value = ""
                                                    End Try


                                                    Try
                                                        Me.Tabla4.Item(TipoCambioDRpf.Index, i).Value = sa.Tables("Pago").Rows(0)("TipoCambioDR")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(TipoCambioDRpf.Index, i).Value = ""
                                                    End Try



                                                    Try
                                                        Me.Tabla4.Item(MetodoDePagoDRpf.Index, i).Value = sa.Tables("Pago").Rows(0)("MetodoDePagoDR")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(MetodoDePagoDRpf.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(NumParcialidadpf.Index, i).Value = sa.Tables("Pago").Rows(0)("NumParcialidad")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(NumParcialidadpf.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(ImpSaldoAntpf.Index, i).Value = sa.Tables("Pago").Rows(0)("ImpSaldoAnt")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(ImpSaldoAntpf.Index, i).Value = ""
                                                    End Try

                                                    Try
                                                        Me.Tabla4.Item(ImpPagadopf.Index, i).Value = sa.Tables("Pago").Rows(0)("ImpPagado")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(ImpPagadopf.Index, i).Value = ""
                                                    End Try


                                                    Try
                                                        Me.Tabla4.Item(ImpPagadopf.Index, i).Value = sa.Tables("Pago").Rows(0)("ImpPagado")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(ImpPagadopf.Index, i).Value = ""
                                                    End Try
                                                    Try
                                                        Me.Tabla4.Item(ImpSaldoInsolutopf.Index, i).Value = sa.Tables("Pago").Rows(0)("ImpSaldoInsoluto")
                                                    Catch ex As Exception
                                                        Me.Tabla4.Item(ImpSaldoInsolutopf.Index, i).Value = ""
                                                    End Try

                                                    Me.Tabla4.Item(Rutapf.Index, i).Value = ruta
                                                    Me.Tabla4.Item(Archivopf.Index, i).Value = nombre
                                                End If


                                            Next
                                        End If
                                    End If
                                Next
                            Catch ex As Exception
                                RadMessageBox.Show("No existe la Ruta" & ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                                'Me.CmdLimpiar.PerformClick()
                            End Try

                        End If
                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                            Me.Barra.Minimum = 0
                            Me.Cursor = Cursors.Arrow
                            RadMessageBox.Show("Movimientos Cargados ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

                            Me.Barra.Value1 = 0
                        Else
                            Me.Barra.Value1 += 1
                        End If
                        ruta = ""
                    Next
                    If Me.Tabla4.Rows.Count > 0 Then
                        Me.lblRegistros.Text = "Total de Registros: " & Me.Tabla4.Rows.Count
                    End If

                    Dim fr As New BarraProcesovb
                    fr.Barra.Minimum = 0
                    fr.Barra.Value = 0
                    fr.Text = "Buscando errores en XML"
                    fr.Barra.Maximum = Me.Tabla1.Rows.Count
                    For i As Integer = 0 To Me.Tabla1.Rows.Count - 1
                        Dim Fila As New DataGridViewRow
                        If Me.Tabla1.Item(UsoCFDI.Index, i).Value Is Nothing Then
                            Fila = Me.Tabla1.Rows(i)
                            Fila.DefaultCellStyle.BackColor = Color.Red
                        Else
                            If ValidarXMl(Me.Tabla1.Item(UsoCFDI.Index, i).Value.ToString().Trim(), Me.Tabla1.Item(Tip.Index, i).Value.ToString().Trim(), i, Me.Tabla1.Item(RFCReceptor.Index, i).Value.ToString().Trim(), Me.Tabla1.Item(Moneda.Index, i).Value.ToString().Trim(), Me.Tabla1.Item(MetododePago.Index, i).Value.ToString().Trim(), Me.Tabla1.Item(FormaDePago.Index, i).Value.ToString().Trim(), Me.Tabla1.Item(Total.Index, i).Value.ToString().Trim()) = "NO" Then
                                Fila = Me.Tabla1.Rows(i)
                                Fila.DefaultCellStyle.BackColor = Color.Red
                            End If
                        End If
                        If fr.Barra.Value = fr.Barra.Maximum Then
                            Me.Barra.Minimum = 0
                            Me.Cursor = Cursors.Arrow
                            MessageBox.Show("Busqueda completada...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            fr.Barra.Value = 0
                        Else
                            fr.Barra.Increment(1)
                        End If
                    Next
                Else
                    RadMessageBox.Show("El periodo esta mal seleccionado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                End If
            End If
        End If
    End Sub
    Private Sub CmdAsignaBoveda_Click(sender As Object, e As EventArgs) Handles CmdAsignaBoveda.Click
        Dim archivo As String = InputBox("Teclea la ruta de la Boveda:", Eventos.titulo_app, My.Computer.FileSystem.SpecialDirectories.MyDocuments)

        If archivo <> "" Then
            Dim sql As String = "UPDATE dbo.Usuarios SET Boveda = '" & archivo & "' WHERE (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')"
            If Eventos.Comando_sql(sql) > 0 Then

            End If
        Else
            Exit Sub
        End If
    End Sub
    Private Sub Codificar_XML_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Activo = True
        Cargar_Listas()
        Diseño()

        Activo = False
    End Sub

    Private Sub Diseño()
        Eventos.DiseñoTablaEnca(Me.Tabla1)
        Eventos.DiseñoTablaEnca(Me.Tabla2)
        Eventos.DiseñoTablaEnca(Me.Tabla3)
        Eventos.DiseñoTablaEnca(Me.Tabla4)
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

        Me.LstrfcC2.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.LstrfcC2.SelectItem = My.Forms.Inicio.Clt


        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2010 Then
                Me.comboAño.Items.Add(Str(i))
            End If
        Next
        Me.comboAño.Text = Str(DateTime.Now.Year)
        Dim mes = Now.Date.Month.ToString

        If Len(mes) < 2 Then
            mes = "0" & mes
        End If
        Me.ComboMesIn.Text = mes
        Me.ComboMesFin.Text = mes


        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2010 Then
                Me.LstAnio2.Items.Add(Str(i))
            End If
        Next
        Me.LstAnio2.Text = Str(DateTime.Now.Year)

        If Len(mes) < 2 Then
            mes = "0" & mes
        End If
        Me.ComboMesIni2.Text = mes
        Me.CombomESfIN2.Text = mes
    End Sub
    Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TCPrincipal.SelectedIndex = 0 Then
            If Me.Tabla1.Rows.Count > 0 Then
                If Trim(IIf(IsDBNull(Me.Tabla1.Item(ru.Index, Me.Tabla1.CurrentRow.Index).Value), "", Me.Tabla1.Item(ru.Index, Me.Tabla1.CurrentRow.Index).Value.ToString)) <> "" Then
                    Eventos.Abrir_Capeta_con_archivo(Trim(Me.Tabla1.Item(ru.Index, Me.Tabla1.CurrentRow.Index).Value.ToString), Trim(Me.Tabla1.Item(nom.Index, Me.Tabla1.CurrentRow.Index).Value.ToString))
                Else
                    RadMessageBox.Show("Debes seleccionar un archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                End If
            Else
                RadMessageBox.Show("No se ha consultado preiamente informacion...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                Exit Sub
            End If
        ElseIf Me.TCPrincipal.SelectedIndex = 1 Then ' falta codificar
            If Me.Tabla4.Rows.Count > 0 Then

                If Trim(Me.Tabla4.Item(Rutapf.Index, Me.Tabla4.CurrentRow.Index).Value.ToString) <> "" Then
                    Eventos.Abrir_Capeta_con_archivo(Trim(Me.Tabla4.Item(Rutapf.Index, Me.Tabla4.CurrentRow.Index).Value.ToString), Trim(Me.Tabla4.Item(Archivopf.Index, Me.Tabla4.CurrentRow.Index).Value.ToString))
                Else
                    RadMessageBox.Show("Debes seleccionar un archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                End If
            Else
                RadMessageBox.Show("No se ha consultado preiamente informacion...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                Exit Sub
            End If
        End If




    End Sub
    Private Sub TCPrincipal_TabIndexChanged(sender As Object, e As EventArgs) Handles TCPrincipal.TabIndexChanged
        If Activo = False Then
            If Me.TCPrincipal.SelectedIndex = 0 Then

            ElseIf Me.TCPrincipal.SelectedIndex = 1 Then

            End If

        End If
    End Sub
    Private Sub CmdLimpiar2_Click(sender As Object, e As EventArgs) Handles CmdLimpiar2.Click
        If Me.TablaConceptos.SelectedIndex = 0 Then
            If Me.Tabla2.Rows.Count > 0 Then
                Me.Tabla2.Rows.Clear()
                Me.LblTotal2.Text = "Total de Registros: "
            End If
        ElseIf Me.TablaConceptos.SelectedIndex = 1 Then
            If Me.Tabla3.Rows.Count > 0 Then
                Me.Tabla3.Rows.Clear()
                Me.LblTotal2.Text = "Total de Registros: "
            End If
        End If

    End Sub
    Private Sub CmdSalir2_Click(sender As Object, e As EventArgs) Handles CmdSalir2.Click
        Me.Close()
    End Sub
    Private Sub CmdProcesar2_Click(sender As Object, e As EventArgs) Handles CmdProcesar2.Click
        If Me.TablaConceptos.SelectedIndex = 0 Then

            'Facturas
            Dim contador As Integer = 0
            Dim archivo As String = ""
            Dim indice As Integer = 0
            archivo = Eventos.Boveda(My.Forms.Inicio.LblUsuario.Text)
            Dim ruta As String
            Dim tipo As String = ""
            If Me.ComboMesIni2.Text <> "" And Me.CombomESfIN2.Text <> "" Then
                If Me.ComboMesIni2.Text <= Me.CombomESfIN2.Text Then ' seleccion correcta del periodo
                    Dim Tam As Integer = Convert.ToInt32(Me.CombomESfIN2.Text) - Convert.ToInt32(Me.ComboMesIni2.Text)
                    Dim meces(Tam) As Object
                    For i As Integer = Convert.ToInt32(Me.ComboMesIni2.Text) - 1 To Convert.ToInt32(Me.ComboMesIni2.Text) + Tam - 1

                        meces(contador) = Me.CombomESfIN2.Items(i)
                        contador = contador + 1
                    Next
                    Me.Barra2.Maximum = meces.Length - 1
                    Me.Barra2.Minimum = 0
                    Me.Barra2.Value1 = 0
                    For Each Va As String In meces.ToString()


                        If Me.RadEmitidas2.Checked = True Then
                            tipo = "Emitidas"
                        Else
                            tipo = "Recibidas"
                        End If
                        Dim cadena As String = "\" & Trim(CalculaFRC(Me.LstrfcC2.SelectItem)) & "\" & tipo & "\" & Trim(Me.LstAnio2.Text) & "\" & Va
                        ruta = archivo & cadena
                        If archivo = "" Then
                            RadMessageBox.Show("No se ha asignado una Boveda...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                            Exit Sub
                        Else
                            Try

                                'Dim cant() As String = Directory.GetFiles(archivo, "*.xml", False)
                                'Dim cuanto As Integer = cant.Length
                                For Each item As String In Directory.GetFiles(ruta, "*.xml", False)
                                    Dim nombre As String = My.Computer.FileSystem.GetName(item)

                                    Dim sa As DataSet = Eventos.CargarXMLaDataSet(item)

                                    If sa.Tables(0).Rows(0)("TipoDeComprobante") = "I" Then


                                        If sa.Tables("Concepto").Rows.Count > 0 Then

                                            Me.Tabla2.RowCount = Me.Tabla2.RowCount + sa.Tables("Concepto").Rows.Count
                                            For J As Integer = 0 To sa.Tables("Concepto").Rows.Count - 1
                                                If Me.Tabla2.Item(RFC_Emisor.Index, indice).Value = "" Then
                                                    Try
                                                        Me.Tabla2.Item(Estado_SAT.Index, indice).Value = ""
                                                        Try
                                                            Me.Tabla2.Item(UUID2.Index, indice).Value = sa.Tables("TimbreFiscalDigital").Rows(0)("UUID")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(UUID2.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(Serie2.Index, indice).Value = sa.Tables(0).Rows(0)("Serie")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(Serie2.Index, indice).Value = ""
                                                        End Try

                                                        Me.Tabla2.Item(Folio2.Index, indice).Value = ""
                                                        Try
                                                            Me.Tabla2.Item(Fecha_Emision.Index, indice).Value = sa.Tables(0).Rows(0)("Fecha")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(Fecha_Emision.Index, indice).Value = ""
                                                        End Try

                                                        Try
                                                            Me.Tabla2.Item(Nombre_Emisor.Index, indice).Value = sa.Tables("Emisor").Rows(0)("Nombre")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(Nombre_Emisor.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(RFC_Emisor.Index, indice).Value = sa.Tables("Emisor").Rows(0)("rfc")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(RFC_Emisor.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(Nombre_Receptor.Index, indice).Value = sa.Tables("Receptor").Rows(0)("Nombre")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(Nombre_Receptor.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(RFC_Receptor.Index, indice).Value = sa.Tables("Receptor").Rows(0)("rfc")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(RFC_Receptor.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(Clave_prod.Index, indice).Value = sa.Tables("Concepto").Rows(J)("ClaveProdServ")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(Clave_prod.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(No_Id.Index, indice).Value = sa.Tables("Concepto").Rows(J)("NoIdentificacion")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(No_Id.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(Descripcion.Index, indice).Value = sa.Tables("Concepto").Rows(J)("Descripcion")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(Descripcion.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(Unidad.Index, indice).Value = sa.Tables("Concepto").Rows(J)("Unidad")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(Unidad.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(ClaveUnidad.Index, indice).Value = sa.Tables("Concepto").Rows(J)("ClaveUnidad")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(ClaveUnidad.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(Cantidad.Index, indice).Value = sa.Tables("Concepto").Rows(J)("Cantidad")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(Cantidad.Index, indice).Value = 0
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(PrecioUnitario.Index, indice).Value = sa.Tables("Concepto").Rows(J)("ValorUnitario")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(PrecioUnitario.Index, indice).Value = 0
                                                        End Try

                                                        Try
                                                            Me.Tabla2.Item(Importe2.Index, indice).Value = sa.Tables("Concepto").Rows(J)("Importe")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(Importe2.Index, indice).Value = 0
                                                        End Try
                                                        Me.Tabla2.Item(Descuento2.Index, indice).Value = 0
                                                        Me.Tabla2.Item(TipoCambio.Index, indice).Value = 0
                                                        Try
                                                            Me.Tabla2.Item(Mone.Index, indice).Value = sa.Tables(0).Rows(0)("Moneda")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(Mone.Index, indice).Value = "MXN"
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(Version2.Index, indice).Value = sa.Tables(0).Rows(0)("Version")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(Version2.Index, indice).Value = "3.3"
                                                        End Try
                                                        Try
                                                            Me.Tabla2.Item(IVA.Index, indice).Value = sa.Tables("Traslado").Rows(J)("Importe")
                                                        Catch ex As Exception
                                                            Me.Tabla2.Item(IVA.Index, indice).Value = 0
                                                        End Try

                                                        Me.Tabla2.Item(Rtu.Index, indice).Value = ruta
                                                        Me.Tabla2.Item(Arc.Index, indice).Value = nombre
                                                        indice = indice + 1
                                                    Catch ex As Exception

                                                    End Try
                                                End If
                                            Next

                                        End If
                                    End If ' Termina la verificacion de El tipo de comprobante
                                Next
                            Catch ex As Exception
                                RadMessageBox.Show("No existe la Ruta" & ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                                'Me.CmdLimpiar.PerformClick()
                            End Try

                        End If
                        If Me.Barra2.Value1 = Me.Barra2.Maximum Then
                            Me.Barra2.Minimum = 0
                            Me.Cursor = Cursors.Arrow
                            RadMessageBox.Show("Movimientos Cargados ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

                            Me.Barra2.Value1 = 0
                        Else
                            Me.Barra2.Value1 += 1
                        End If
                        ruta = ""
                    Next
                    If Me.Tabla2.Rows.Count > 0 Then
                        Me.LblTotal2.Text = "Total de Registros: " & Me.Tabla2.Rows.Count
                    End If
                Else
                    RadMessageBox.Show("El periodo esta mal seleccionado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                End If
            End If
        ElseIf Me.TablaConceptos.SelectedIndex = 1 Then 'Falta codificar
            'Codigo de pagos
            Dim contador As Integer = 0
            Dim archivo As String = ""
            Dim indice As Integer = 0
            archivo = Eventos.Boveda(My.Forms.Inicio.LblUsuario.Text)
            Dim ruta As String
            Dim tipo As String = ""
            If Me.ComboMesIni2.Text <> "" And Me.CombomESfIN2.Text <> "" Then
                If Me.ComboMesIni2.Text <= Me.CombomESfIN2.Text Then ' seleccion correcta del periodo
                    Dim Tam As Integer = Convert.ToInt32(Me.CombomESfIN2.Text) - Convert.ToInt32(Me.ComboMesIni2.Text)
                    Dim meces(Tam) As Object
                    For i As Integer = Convert.ToInt32(Me.ComboMesIni2.Text) - 1 To Convert.ToInt32(Me.ComboMesIni2.Text) + Tam - 1

                        meces(contador) = Me.CombomESfIN2.Items(i)
                        contador = contador + 1
                    Next
                    Me.Barra2.Maximum = meces.Length - 1
                    Me.Barra2.Minimum = 0
                    Me.Barra2.Value1 = 0
                    For Each Va As String In meces


                        If Me.RadEmitidas2.Checked = True Then
                            tipo = "Emitidas"
                        Else
                            tipo = "Recibidas"
                        End If
                        Dim cadena As String = "\" & Trim(CalculaFRC(Me.LstrfcC2.SelectItem)) & "\" & tipo & "\" & Trim(Me.LstAnio2.Text) & "\" & Va
                        ruta = archivo & cadena
                        If archivo = "" Then
                            RadMessageBox.Show("No se ha asignado una Boveda...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                            Exit Sub
                        Else
                            Try

                                For Each item As String In Directory.GetFiles(ruta, "*.xml", False)
                                    Dim nombre As String = My.Computer.FileSystem.GetName(item)

                                    Dim sa As DataSet = Eventos.CargarXMLaDataSet(item)

                                    If sa.Tables(0).Rows(0)("TipoDeComprobante") = "P" Then


                                        If sa.Tables("DoctoRelacionado").Rows.Count > 0 Then

                                            Me.Tabla3.RowCount = Me.Tabla3.RowCount + sa.Tables("DoctoRelacionado").Rows.Count
                                            For J As Integer = 0 To sa.Tables("DoctoRelacionado").Rows.Count - 1
                                                If Me.Tabla3.Item(RFC_Emisor.Index, indice).Value = "" Then
                                                    Try

                                                        Select Case Trim(sa.Tables(0).Rows(0)("TipoDeComprobante"))
                                                            Case "I"
                                                                Me.Tabla3.Item(TipoP.Index, indice).Value = "Factura"
                                                            Case "P"
                                                                Me.Tabla3.Item(TipoP.Index, indice).Value = "Complemento"
                                                        End Select

                                                        Me.Tabla3.Item(UUIDP.Index, indice).Value = sa.Tables("TimbreFiscalDigital").Rows(0)("UUID")

                                                        Me.Tabla3.Item(Fecha_EmisionP.Index, indice).Value = sa.Tables(0).Rows(0)("Fecha")
                                                        Try
                                                            Me.Tabla3.Item(FolioP.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("Folio")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(FolioP.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla3.Item(SerieP.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("Serie")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(SerieP.Index, indice).Value = ""
                                                        End Try
                                                        Me.Tabla3.Item(SubTotalP.Index, indice).Value = sa.Tables(0).Rows(0)("SubTotal")
                                                        Me.Tabla3.Item(MonedaP.Index, indice).Value = sa.Tables(0).Rows(0)("Moneda")
                                                        Me.Tabla3.Item(TotalP.Index, indice).Value = sa.Tables(0).Rows(0)("Total")
                                                        Me.Tabla3.Item(LugarP.Index, indice).Value = sa.Tables(0).Rows(0)("LugarExpedicion")
                                                        Me.Tabla3.Item(RFC_EmisorP.Index, indice).Value = sa.Tables("Emisor").Rows(0)("rfc")
                                                        Try
                                                            Me.Tabla3.Item(Nombre_EmisorP.Index, indice).Value = sa.Tables("Emisor").Rows(0)("Nombre")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(Nombre_EmisorP.Index, indice).Value = ""
                                                        End Try
                                                        Me.Tabla3.Item(RegimenFiscalP.Index, indice).Value = sa.Tables("Emisor").Rows(0)("RegimenFiscal")
                                                        Try
                                                            Me.Tabla3.Item(RFC_ReceptorP.Index, indice).Value = sa.Tables("Receptor").Rows(0)("rfc")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(RFC_ReceptorP.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla3.Item(Nombre_ReceptorP.Index, indice).Value = sa.Tables("Receptor").Rows(0)("Nombre")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(Nombre_ReceptorP.Index, indice).Value = ""
                                                        End Try
                                                        Me.Tabla3.Item(UsoCFDI.Index, indice).Value = sa.Tables("Receptor").Rows(0)("UsoCFDI")
                                                        Me.Tabla3.Item(Clave_ProductoP.Index, indice).Value = sa.Tables("Concepto").Rows(0)("ClaveProdServ")

                                                        Me.Tabla3.Item(CantidadP.Index, indice).Value = sa.Tables("Concepto").Rows(0)("Cantidad")
                                                        Me.Tabla3.Item(UnidadP.Index, indice).Value = sa.Tables("Concepto").Rows(0)("ClaveUnidad")
                                                        Me.Tabla3.Item(DescripcionP.Index, indice).Value = sa.Tables("Concepto").Rows(0)("Descripcion")
                                                        Me.Tabla3.Item(PrecioUnitarioP.Index, indice).Value = sa.Tables("Concepto").Rows(0)("ValorUnitario")
                                                        Me.Tabla3.Item(Importep.Index, indice).Value = sa.Tables("Concepto").Rows(0)("Importe")
                                                        Me.Tabla3.Item(FechaDePagoP.Index, indice).Value = sa.Tables("Pago").Rows(0)("FechaPago")
                                                        Me.Tabla3.Item(FormaDePagoP.Index, indice).Value = sa.Tables("Pago").Rows(0)("FormaDePagoP")
                                                        Me.Tabla3.Item(MonedaP.Index, indice).Value = sa.Tables("Pago").Rows(0)("MonedaP")

                                                        Try
                                                            Me.Tabla3.Item(TipoCambioP.Index, indice).Value = sa.Tables("Pago").Rows(0)("TipoCambioP")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(TipoCambioP.Index, indice).Value = 0
                                                        End Try

                                                        Try
                                                            Me.Tabla3.Item(NumOperacionP.Index, indice).Value = sa.Tables("Pago").Rows(0)("NumOperacion")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(NumOperacionP.Index, indice).Value = ""
                                                        End Try

                                                        Try
                                                            Me.Tabla3.Item(RfcEmisorCtaOrdP.Index, indice).Value = sa.Tables("Pago").Rows(0)("RfcEmisorCtaOrd")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(RfcEmisorCtaOrdP.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla3.Item(NomBancoOrdExtP.Index, indice).Value = sa.Tables("Pago").Rows(0)("NomBancoOrdExt")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(NomBancoOrdExtP.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla3.Item(RfcEmisorCtaBen.Index, indice).Value = sa.Tables("Pago").Rows(0)("RfcEmisorCtaBen")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(RfcEmisorCtaBen.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla3.Item(CtaBeneficiario.Index, indice).Value = sa.Tables("Pago").Rows(0)("CtaBeneficiario")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(CtaBeneficiario.Index, indice).Value = ""
                                                        End Try



                                                        Try
                                                            Me.Tabla3.Item(TipoCadPago.Index, indice).Value = sa.Tables("Pago").Rows(0)("TipoCadPago")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(TipoCadPago.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla3.Item(CertPago.Index, indice).Value = sa.Tables("Pago").Rows(0)("CertPago")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(CertPago.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla3.Item(CadPago.Index, indice).Value = sa.Tables("Pago").Rows(0)("CadPago")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(CadPago.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla3.Item(SelloPago.Index, indice).Value = sa.Tables("Pago").Rows(0)("SelloPago")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(SelloPago.Index, indice).Value = ""
                                                        End Try

                                                        Try
                                                            Me.Tabla3.Item(IdDocumento.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("IdDocumento")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(IdDocumento.Index, indice).Value = ""
                                                        End Try

                                                        Try
                                                            Me.Tabla3.Item(SerieDR.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("Serie")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(SerieDR.Index, indice).Value = ""
                                                        End Try

                                                        Try
                                                            Me.Tabla3.Item(FolioDR.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("Folio")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(FolioDR.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla3.Item(MonedaDR.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("MonedaDR")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(MonedaDR.Index, indice).Value = ""
                                                        End Try

                                                        Try
                                                            Me.Tabla3.Item(TipoCambioDR.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("TipoCambioDR")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(TipoCambioDR.Index, indice).Value = ""
                                                        End Try



                                                        Try
                                                            Me.Tabla3.Item(MetodoDePagoDR.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("MetodoDePagoDR")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(MetodoDePagoDR.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla3.Item(NumParcialidad.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("NumParcialidad")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(NumParcialidad.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla3.Item(ImpSaldoAnt.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("ImpSaldoAnt")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(ImpSaldoAnt.Index, indice).Value = ""
                                                        End Try

                                                        Try
                                                            Me.Tabla3.Item(ImpPagado.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("ImpPagado")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(ImpPagado.Index, indice).Value = ""
                                                        End Try


                                                        Try
                                                            Me.Tabla3.Item(ImpSaldoAnt.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("ImpSaldoAnt")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(ImpSaldoAnt.Index, indice).Value = ""
                                                        End Try
                                                        Try
                                                            Me.Tabla3.Item(ImpSaldoInsoluto.Index, indice).Value = sa.Tables("DoctoRelacionado").Rows(J)("ImpSaldoInsoluto")
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(ImpSaldoInsoluto.Index, indice).Value = ""
                                                        End Try
                                                        Me.Tabla3.Item(Rutap.Index, indice).Value = ruta
                                                        Me.Tabla3.Item(Archivop.Index, indice).Value = nombre


                                                        Try

                                                            Dim Objeto(3) As Decimal
                                                            Dim i As Integer = 0
                                                            For Each Elemento In Calcula_Importes(Trim(Me.Tabla3.Item(IdDocumento.Index, indice).Value), archivo & "\" & Trim(CalculaFRC(Me.LstrfcC2.SelectItem)) & "\" & tipo, Convert.ToDecimal(sa.Tables("DoctoRelacionado").Rows(J)("ImpPagado")))
                                                                Objeto(i) = Elemento
                                                                i += 1
                                                            Next
                                                            Dim Graba As Decimal = Objeto(0), Exent As Decimal = Objeto(1), Impuesto As Decimal = Objeto(2)

                                                            Me.Tabla3.Item(Grabado.Index, indice).Value = Graba
                                                            Me.Tabla3.Item(Excento.Index, indice).Value = Exent
                                                            Me.Tabla3.Item(IvaP.Index, indice).Value = Impuesto
                                                        Catch ex As Exception
                                                            Me.Tabla3.Item(Grabado.Index, indice).Value = 0
                                                            Me.Tabla3.Item(Excento.Index, indice).Value = 0
                                                            Me.Tabla3.Item(IvaP.Index, indice).Value = 0
                                                        End Try

                                                        indice = indice + 1
                                                    Catch ex As Exception

                                                    End Try
                                                End If
                                            Next

                                        End If
                                    End If ' Termina la verificacion de El tipo de comprobante
                                Next
                            Catch ex As Exception
                                RadMessageBox.Show("No existe la Ruta" & ex.Message, Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                                Me.CmdLimpiar.PerformClick()
                            End Try

                        End If
                        If Me.Barra2.Value1 = Me.Barra2.Maximum Then
                            Me.Barra2.Minimum = 0
                            Me.Cursor = Cursors.Arrow
                            RadMessageBox.Show("Movimientos Cargados ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

                            Me.Barra2.Value1 = 0
                        Else
                            Me.Barra2.Value1 += 1
                        End If
                        ruta = ""
                    Next
                    If Me.Tabla3.Rows.Count > 0 Then
                        Me.LblTotal2.Text = "Total de Registros: " & Me.Tabla3.Rows.Count
                    End If
                Else
                    RadMessageBox.Show("El periodo esta mal seleccionado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                End If
            End If
        End If
    End Sub
    Private Function Calcula_Importes(ByVal archivo As String, ByVal Ruta As String, ByVal Importe As Decimal) As Decimal()
        Dim Objeto(4) As Decimal
        Dim Objeto2(3) As Decimal
        Dim i As Integer = 0
        For Each item In ImpFaCURA(archivo, Ruta)
            Objeto(i) = item
            i += 1
        Next

        Dim Porcen As Decimal = 0
        Dim Grabado As Decimal = 0, Exento As Decimal = 0, IVA As Decimal = 0
        Try

            Porcen = Math.Round(Importe, 2) / Math.Round(Objeto(3), 2) 'se debe sumar las retenciones
            Grabado = Math.Round((Objeto(2) / 0.16), 2)
            'importe exento
            Exento = Math.Round(Objeto(1) - Grabado, 2)
            'Iva_real
            IVA = Math.Round((Grabado * 0.16), 2)
            'calcula el % Pago Acumulado
            If Exento < 1 Then
                Grabado = Grabado - Exento
                Exento = 0
            End If
            If Math.Round(Porcen, 0) = 1 Then
                Objeto2(0) = Grabado
                Objeto2(1) = Exento
                Objeto2(2) = IVA
            Else
                Objeto2(0) = Grabado * Porcen
                Objeto2(1) = Exento * Porcen
                Objeto2(2) = IVA * Porcen
            End If
        Catch ex As Exception
            Objeto2(0) = 0
            Objeto2(1) = 0
            Objeto2(2) = 0
        End Try
        Return Objeto2
    End Function
    Private Function ImpFaCURA(ByVal File As String, ByVal ruta As String) As Decimal()
        Dim Importe(4) As Decimal
        Dim Nombre As String = ""
        Try

            For Each name As String In Directory.GetFiles(ruta, "*" & File & "*", SearchOption.AllDirectories)

                Nombre = name
                Exit For
            Next

        Catch oe As Exception
            MsgBox(oe.Message, MsgBoxStyle.Critical)
        End Try
        If Nombre <> "" Then


            Dim sa As DataSet = Eventos.CargarXMLaDataSet(Nombre)
            Try
                If sa.Tables(0).Rows(0)("TipoDeComprobante") = "I" Then
                    Importe(0) = sa.Tables(0).Rows(0)("Total")
                    Importe(1) = sa.Tables(0).Rows(0)("SubTotal")
                    Importe(2) = sa.Tables("Traslado").Rows(0)("Importe")
                    Importe(3) = sa.Tables(0).Rows(0)("Total")
                Else
                    Importe(0) = 0
                    Importe(1) = 0
                    Importe(2) = 0
                    Importe(3) = 0
                End If

            Catch ex As Exception
                Importe(0) = 0
                Importe(1) = 0
                Importe(2) = 0
                Importe(3) = 0
            End Try
        Else
            Importe(0) = 0
            Importe(1) = 0
            Importe(2) = 0
            Importe(3) = 0
        End If
        Return Importe
    End Function
    Private Sub lstCliente_cambio_item(value As String, texto As String) Handles lstCliente.Cambio_item
        Me.LstrfcC2.SelectText = texto
    End Sub
    Private Sub CmdBusacar2_Click(sender As Object, e As EventArgs) Handles CmdBusacar2.Click

        If Me.TablaConceptos.SelectedIndex = 0 Then
            If Trim(Me.Tabla2.Item(ru.Index, Me.Tabla2.CurrentRow.Index).Value.ToString) <> "" Then
                Eventos.Abrir_Capeta_con_archivo(Trim(Me.Tabla2.Item(Rtu.Index, Me.Tabla2.CurrentRow.Index).Value.ToString), Trim(Me.Tabla2.Item(Arc.Index, Me.Tabla2.CurrentRow.Index).Value.ToString))
            Else
                RadMessageBox.Show("Debes seleccionar un archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        ElseIf Me.TablaConceptos.SelectedIndex = 1 Then
            If Trim(Me.Tabla3.Item(Rutap.Index, Me.Tabla3.CurrentRow.Index).Value.ToString) <> "" Then
                Eventos.Abrir_Capeta_con_archivo(Trim(Me.Tabla3.Item(Rutap.Index, Me.Tabla3.CurrentRow.Index).Value.ToString), Trim(Me.Tabla3.Item(Archivop.Index, Me.Tabla3.CurrentRow.Index).Value.ToString))
            Else
                RadMessageBox.Show("Debes seleccionar un archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        End If

    End Sub
    Private Sub CmdExportar_Click(sender As Object, e As EventArgs) Handles CmdExportar.Click
        If Me.TablaConceptos.SelectedIndex = 0 Then

            If Me.Tabla2.RowCount > 0 Then
                RadMessageBox.Show("Este Proceso puede tardar dependiendo de la información a exportar, presione Aceptar y espere a que el proceso termine...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.Tabla2.RowCount - 1
                Dim m_Excel As Microsoft.Office.Interop.Excel.Application

                Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook

                Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet

                m_Excel = New Microsoft.Office.Interop.Excel.Application
                m_Excel.Visible = False

                objLibroExcel = m_Excel.Workbooks.Add()
                objHojaExcel = objLibroExcel.Worksheets(1)
                objLibroExcel.ActiveSheet.Name = "Conceptos"
                objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible

                objHojaExcel.Activate()
                Dim i As Integer, j As Integer
                For i = 0 To Tabla2.Columns.Count - 3
                    objHojaExcel.Cells(1, i + 1) = Me.Tabla2.Columns.Item(i).HeaderCell.Value
                Next
                For i = 0 To Tabla2.RowCount - 1
                    frm.Barra.value = i
                    For j = 0 To Tabla2.Columns.Count - 3
                        objHojaExcel.Cells(i + 2, j + 1) = Me.Tabla2.Item(j, i).Value
                    Next
                Next
                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                m_Excel.Visible = True
            Else
                RadMessageBox.Show("No hay registros a exportar....", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
        ElseIf Me.TablaConceptos.SelectedIndex = 1 Then

            If Me.Tabla3.RowCount > 0 Then
                RadMessageBox.Show("Este Proceso puede tardar dependiendo de la información a exportar, presione Aceptar y espere a que el proceso termine...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.Tabla3.RowCount - 1
                Dim m_Excel As Microsoft.Office.Interop.Excel.Application

                Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook

                Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet

                m_Excel = New Microsoft.Office.Interop.Excel.Application
                m_Excel.Visible = False

                objLibroExcel = m_Excel.Workbooks.Add()
                objHojaExcel = objLibroExcel.Worksheets(1)
                objLibroExcel.ActiveSheet.Name = "Conceptos"
                objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible

                objHojaExcel.Activate()
                Dim i As Integer, j As Integer
                For i = 0 To Tabla3.Columns.Count - 3
                    objHojaExcel.Cells(1, i + 1) = Me.Tabla3.Columns.Item(i).HeaderCell.Value
                Next
                For i = 0 To Tabla3.RowCount - 1
                    frm.Barra.value = i
                    For j = 0 To Tabla3.Columns.Count - 3
                        objHojaExcel.Cells(i + 2, j + 1) = Me.Tabla3.Item(j, i).Value
                    Next
                Next
                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                m_Excel.Visible = True
            Else
                RadMessageBox.Show("No hay registros a exportar....", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
        End If
    End Sub
    Private Sub CmdExportar2_Click(sender As Object, e As EventArgs) Handles CmdExportar2.Click
        If Me.TCPrincipal.SelectedIndex = 0 Then
            If Me.Tabla1.RowCount > 0 Then
                RadMessageBox.Show("Este Proceso puede tardar dependiendo de la información a exportar, presione Aceptar y espere a que el proceso termine...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.Tabla1.RowCount - 1
                Dim m_Excel As Microsoft.Office.Interop.Excel.Application

                Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook

                Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet

                m_Excel = New Microsoft.Office.Interop.Excel.Application
                m_Excel.Visible = False

                objLibroExcel = m_Excel.Workbooks.Add()
                objHojaExcel = objLibroExcel.Worksheets(1)
                objLibroExcel.ActiveSheet.Name = "XML"
                objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible

                objHojaExcel.Activate()
                Dim i As Integer, j As Integer
                For i = 0 To Tabla1.Columns.Count - 3
                    objHojaExcel.Cells(1, i + 1) = Me.Tabla1.Columns.Item(i).HeaderCell.Value
                Next
                For i = 0 To Tabla1.RowCount - 1
                    frm.Barra.value = i
                    For j = 0 To Tabla1.Columns.Count - 3
                        objHojaExcel.Cells(i + 2, j + 1) = Me.Tabla1.Item(j, i).Value
                    Next
                Next
                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                m_Excel.Visible = True
            Else
                RadMessageBox.Show("No hay registros a exportar....", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
        ElseIf Me.TCPrincipal.SelectedIndex = 1 Then
            If Me.Tabla4.RowCount > 0 Then
                RadMessageBox.Show("Este Proceso puede tardar dependiendo de la información a exportar, presione Aceptar y espere a que el proceso termine...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.Tabla4.RowCount - 1
                Dim m_Excel As Microsoft.Office.Interop.Excel.Application

                Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook

                Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet

                m_Excel = New Microsoft.Office.Interop.Excel.Application
                m_Excel.Visible = False

                objLibroExcel = m_Excel.Workbooks.Add()
                objHojaExcel = objLibroExcel.Worksheets(1)
                objLibroExcel.ActiveSheet.Name = "XML"
                objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible

                objHojaExcel.Activate()
                Dim i As Integer, j As Integer
                For i = 0 To Tabla4.Columns.Count - 3
                    objHojaExcel.Cells(1, i + 1) = Me.Tabla4.Columns.Item(i).HeaderCell.Value
                Next
                For i = 0 To Tabla4.RowCount - 1
                    frm.Barra.value = i
                    For j = 0 To Tabla4.Columns.Count - 3
                        objHojaExcel.Cells(i + 2, j + 1) = Me.Tabla4.Item(j, i).Value
                    Next
                Next
                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                m_Excel.Visible = True
            Else
                RadMessageBox.Show("No hay registros a exportar....", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
        End If
    End Sub
    Private Function CalculaFRC(ByVal Cliente As Integer)
        Dim rfc As String = ""
        Dim sql As String = "select Reg_fed_causantes from Empresa where Id_Empresa  =" & Cliente & ""

        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            rfc = ds.Tables(0).Rows(0)("Reg_fed_causantes")
        Else
            rfc = ""
        End If
        Return rfc
    End Function
    Private Sub CmdFactura_Click(sender As Object, e As EventArgs) Handles CmdFactura.Click
        'crear pdf de factura
        If Me.Tabla1.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In Tabla1.Rows
                If Fila.Cells(UUID.Index).Selected = True Then
                    If Trim(Me.Tabla1.Item(ru.Index, Me.Tabla1.CurrentRow.Index).Value.ToString) <> "" Then
                        GeneraFactura(Trim(Fila.Cells(ru.Index).Value.ToString) & "\" & Trim(Fila.Cells(nom.Index).Value.ToString), Trim(Fila.Cells(ru.Index).Value.ToString))
                    Else
                        RadMessageBox.Show("Debes seleccionar un archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                    End If
                End If
            Next
        End If
    End Sub
    Private Function CalculaUsoCFDI(Clave)
        Dim des As String = ""
        Dim sql As String = "select Descripcion From Uso_CFDI where clave = '" & Clave & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            des = ds.Tables(0).Rows(0)(0)
        Else
            des = ""
        End If
        Return des
    End Function
    Private Function CalculaForma(Clave)
        Dim des As String = ""
        Dim sql As String = "select Descripcion From Metodos_de_Pago where clave = '" & Clave & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            des = ds.Tables(0).Rows(0)(0)
        Else
            des = ""
        End If
        Return des
    End Function
    Private Function CalculaMetodo(Clave)
        Dim des As String = ""
        Dim sql As String = "select Descripcion From Forma_de_Pago where clave = '" & Clave & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            des = ds.Tables(0).Rows(0)(0)
        Else
            des = ""
        End If
        Return des
    End Function
    Private Sub GeneraFactura(ByVal item As String, ByVal Ubicacion As String)

        Dim DT As New DataTable
        Dim Detalle As New DataTable
        With DT
            .Columns.Add("Nombre")
            .Columns.Add("RFC")
            .Columns.Add("Regimen_Fiscal")
            .Columns.Add("Lugar_Expedicion")
            .Columns.Add("Tipo_Relacion")
            .Columns.Add("CFDI_Relacionado")
            .Columns.Add("Documento")
            .Columns.Add("Fecha_De_Emision")
            .Columns.Add("Fecha_Certificacion")
            .Columns.Add("Tipo_de_comprobante")
            .Columns.Add("Nombre_Receptor")
            .Columns.Add("RFC_Receptor")
            .Columns.Add("Residencia_Fiscal_Receptor")
            .Columns.Add("NumRegIdTrib")
            .Columns.Add("UsoCFDI")
            .Columns.Add("No_Certificado_SAT")
            .Columns.Add("Importe_en_letra")
            .Columns.Add("Forma_de_pago")
            .Columns.Add("Metodo_de_pago")
            .Columns.Add("Condicion_de_pago")
            .Columns.Add("Moneda")
            .Columns.Add("Version_del_comprobante")
            .Columns.Add("Tipo_de_cambio")
            .Columns.Add("Clave_Confirmacion")
            .Columns.Add("Subtotal")
            .Columns.Add("Descuento")
            .Columns.Add("IEPS")
            .Columns.Add("IVA")
            .Columns.Add("Retenciones_ISR")
            .Columns.Add("Retenciones_IVA")
            .Columns.Add("Total")
            .Columns.Add("Sello_Digital_CFDi")
            .Columns.Add("Sello_Digital_del_SAT")
            .Columns.Add("Cadena")
        End With

        With Detalle
            .Columns.Add("ClvProdServ")
            .Columns.Add("NoIdent")
            .Columns.Add("Cantidad")
            .Columns.Add(" Clv_Unidad")
            .Columns.Add("Unidad")
            .Columns.Add("Descripcion")
            .Columns.Add("Valor_Unitario")
            .Columns.Add("Descuento")
            .Columns.Add("Importe")
            .Columns.Add("No_Certificado_SAT")

        End With

        Dim sa As DataSet = Eventos.CargarXMLaDataSet(item)
        Dim CADENA As String = ""
        If sa.Tables(0).Rows.Count > 0 Then ' Verifico si tiene datos el XML


            Dim retisr As Decimal = 0
            Dim retIVA As Decimal = 0
            Try
                For J As Integer = 0 To sa.Tables("Retencion").Rows.Count - 1
                    If sa.Tables("Retencion").Rows.Item(J)(1) = "002" Then 'IVA
                        retIVA = sa.Tables("Retencion").Rows.Item(J)(4)
                    ElseIf sa.Tables(7).Rows.Item(J)(1) = "003" Then 'IEPS

                    ElseIf sa.Tables(7).Rows.Item(J)(1) = "001" Then 'ISR
                        retisr = sa.Tables(8).Rows.Item(J)(4)
                    End If
                Next

            Catch ex As Exception

            End Try
            CADENA = "||1.1|" & sa.Tables("TimbreFiscalDigital").Rows(0)("UUID") & "|" & sa.Tables("TimbreFiscalDigital").Rows(0)("FechaTimbrado") & "|" & sa.Tables("TimbreFiscalDigital").Rows(0)("RfcProvCertif") & "|" & sa.Tables("TimbreFiscalDigital").Rows(0)("SelloCFD") & sa.Tables("TimbreFiscalDigital").Rows(0)("NoCertificadoSAT")
            Dim Tip As String = ""
            Dim Relacion As String = ""
            Dim recidencia As String = ""
            Dim NumRegIdTrib As String = ""
            Dim condicion As String = ""
            Dim Cambio As Decimal = 0
            Dim clav As String = ""
            Dim Fol As String = ""
            Dim descuento As Decimal = 0
            Try
                descuento = sa.Tables(0).Rows(0)("Descuento")
            Catch ex As Exception
                descuento = 0
            End Try
            Try
                clav = sa.Tables(0).Rows(0)("Clave_Confirmacion")
            Catch ex As Exception
                clav = ""
            End Try
            Try
                Cambio = sa.Tables(0).Rows(0)("TipoCambio")
            Catch ex As Exception
                Cambio = 0
            End Try
            Try
                condicion = sa.Tables(0).Rows(0)("CondicionesDePago")
            Catch ex As Exception
                condicion = ""
            End Try
            Try
                NumRegIdTrib = sa.Tables("Receptor").Rows(0)("NumRegIdTrib")
            Catch ex As Exception
                NumRegIdTrib = ""
            End Try
            Try
                Relacion = sa.Tables(0).Rows(0)("UUIDRel")
            Catch ex As Exception
                Relacion = ""
            End Try
            Try
                Tip = IIf(IsNothing(sa.Tables(0).Rows(0)("TipoRelacion")) = True, "", sa.Tables(0).Rows(0)("TipoRelacion"))
            Catch ex As Exception
                Tip = ""
            End Try
            Try
                recidencia = sa.Tables("Receptor").Rows(0)("RecidensiaFiscal")
            Catch ex As Exception
                recidencia = ""
            End Try
            Try
                Fol = sa.Tables(0).Rows(0)("Folio")
            Catch ex As Exception
                Fol = ""
            End Try
            'For Each dr As DataGridViewRow In Me.Tabla1.Rows
            DT.Rows.Add(sa.Tables("Emisor").Rows(0)("Nombre"),
                        sa.Tables("Emisor").Rows(0)("rfc"),
                            sa.Tables("Emisor").Rows(0)("RegimenFiscal"),
                            sa.Tables(0).Rows(0)("LugarExpedicion"),
                            Tip,
                            Relacion,
                            Fol,
                            sa.Tables(0).Rows(0)("Fecha"),
                            sa.Tables("TimbreFiscalDigital").Rows(0)("FechaTimbrado"),
                            IIf(Trim(sa.Tables(0).Rows(0)("TipoDeComprobante")) = "I", "Ingreso", "Pago"),
                            sa.Tables("Receptor").Rows(0)("Nombre"),
                            sa.Tables("Receptor").Rows(0)("rfc"),
                            recidencia,
                            NumRegIdTrib,
                            sa.Tables("Receptor").Rows(0)("UsoCFDI") & " - " & CalculaUsoCFDI(sa.Tables("Receptor").Rows(0)("UsoCFDI")),
                            sa.Tables("TimbreFiscalDigital").Rows(0)("NoCertificadoSAT"),
                            sa.Tables(0).Rows(0)("Total"),
                            sa.Tables(0).Rows(0)("FormaPago") & " - " & CalculaForma(sa.Tables(0).Rows(0)("FormaPago")),
                            sa.Tables(0).Rows(0)("MetodoPago") & " - " & CalculaMetodo(sa.Tables(0).Rows(0)("MetodoPago")),
                            condicion,
                            sa.Tables(0).Rows(0)("Moneda"),
                            sa.Tables(0).Rows(0)("Version"),
                            Cambio,
                            clav,
                            sa.Tables(0).Rows(0)("SubTotal"),
                            descuento,
                            0,
                            sa.Tables("Traslado").Rows(0)("Importe"),
                            retisr,
                            retIVA,
                            sa.Tables(0).Rows(0)("Total"),
                            sa.Tables("TimbreFiscalDigital").Rows(0)("SelloCFD"),
                            sa.Tables("TimbreFiscalDigital").Rows(0)("SelloSAT"), CADENA)
            'Next




            Dim descu As Decimal = 0
            Dim Noid As String = ""
            Dim uni As String = ""
            For J As Integer = 0 To sa.Tables("Concepto").Rows.Count - 1
                Try
                    descu = sa.Tables("Concepto").Rows(0)("Descuento")
                Catch ex As Exception
                    descu = 0
                End Try
                Try
                    Noid = sa.Tables("Concepto").Rows(J)("NoIdentificacion")
                Catch ex As Exception
                    Noid = ""
                End Try
                Try
                    uni = sa.Tables("Concepto").Rows(J)("Unidad")
                Catch ex As Exception
                    uni = ""
                End Try
                Try
                    Detalle.Rows.Add(sa.Tables("Concepto").Rows(J)("ClaveProdServ"), Noid,
                                sa.Tables("Concepto").Rows(J)("Cantidad"), sa.Tables("Concepto").Rows(J)("ClaveUnidad"), uni,
                                sa.Tables("Concepto").Rows(J)("Descripcion"), sa.Tables("Concepto").Rows(J)("ValorUnitario"), descu,
                                sa.Tables("Traslado").Rows(J)("Importe"), sa.Tables("TimbreFiscalDigital").Rows(0)("NoCertificadoSAT"))
                Catch ex As Exception

                End Try

            Next

            CrearQR(Leerxml(item), sa.Tables("TimbreFiscalDigital").Rows(0)("UUID"))
            Dim Cr As New FacturaPDF
            Cr.Database.Tables("Factura").SetDataSource(DT)
            Cr.Database.Tables("DetalleFactura").SetDataSource(Detalle)
            ' Cr.SetDataSource(DT) ' lo muetsro en reporte
            'Dim Reporte As New ReporteFactura
            'Reporte.CrvFactura.ReportSource = Cr

            Dim Cri As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim Valor As New CrystalDecisions.Shared.ParameterValues()
            Cri.Value = sa.Tables(0).Rows(0)("NoCertificado")
            Valor.Add(Cri)
            Cr.DataDefinition.ParameterFields("Cer").ApplyCurrentValues(Valor)


            Dim Img As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim Ruta As New CrystalDecisions.Shared.ParameterValues()
            Img.Value = "C:\Program Files\Contable\SetupProyectoContable\Plantillas\Qrs\" & sa.Tables("TimbreFiscalDigital").Rows(0)("UUID") & ".jpg"
            Ruta.Add(Img)
            Cr.DataDefinition.ParameterFields("Imagen").ApplyCurrentValues(Ruta)
            Try
                Dim diskOpts As New DiskFileDestinationOptions()
                Cr.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile
                Cr.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat

                'Este es la ruta donde se guardara tu archivo.

                If File.Exists(Ubicacion & sa.Tables("TimbreFiscalDigital").Rows(0)("UUID") & ".pdf") Then
                    File.Delete(Ubicacion & sa.Tables("TimbreFiscalDigital").Rows(0)("UUID"))
                End If
                diskOpts.DiskFileName = Ubicacion & "\" & sa.Tables("TimbreFiscalDigital").Rows(0)("UUID") & ".pdf"
                Cr.ExportOptions.DestinationOptions = diskOpts
                Cr.Export()
            Catch ex As Exception
                Throw ex
            End Try
            'Reporte.ShowDialog()
        End If
    End Sub
    Private Sub CrearQR(ByVal Texto As String, ByVal nombre As String)
        Dim generar As QRCodeEncoder = New QRCodeEncoder
        generar.QRCodeEncodeMode = Codec.QRCodeEncoder.ENCODE_MODE.BYTE ' se declara el tipo de codificacion en este caso es Codigo QR
        generar.QRCodeScale = Int32.Parse(2) ' Se declara el tamaño de la escala del codigo

        generar.QRCodeErrorCorrect = Codec.QRCodeEncoder.ERROR_CORRECTION.Q ' Se declara el tipo de correccion a 30% H 25% Q 15% M 7% L 
        generar.QRCodeVersion = 0 ' La version sero Calcuala en automatico el tamaño

        generar.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(ColorFondoQR)
        generar.QRCodeForegroundColor = System.Drawing.Color.FromArgb(colorQR)
        Dim img As Image = generar.Encode(Texto)
        'Dim img As Image = DirectCast(Me.QR.Image.Clone, Image)
        img.Save("C:\Program Files\Contable\SetupProyectoContable\Plantillas\Qrs\" & nombre & ".jpg")
        img.Dispose()

    End Sub
    Private Function Leerxml(ByVal NombreDelFichero As String)
        Dim cadena As String
        Dim varXmlFile As XmlDocument = New XmlDocument()
        Dim varXmlNsMngr As XmlNamespaceManager = New XmlNamespaceManager(varXmlFile.NameTable)
        varXmlFile.Load(NombreDelFichero)
        varXmlNsMngr.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
        varXmlNsMngr.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital")
        Dim varUUID As String
        Dim varTotal As Decimal
        varTotal = varXmlFile.SelectSingleNode("/cfdi:Comprobante/@Total", varXmlNsMngr).InnerText
        varUUID = varXmlFile.SelectSingleNode("/cfdi:Comprobante/cfdi:Complemento/tfd:TimbreFiscalDigital/@UUID", varXmlNsMngr).InnerText
        Dim importeTotal As String = Format(Int(varTotal), "0000000000.000000")
        cadena = importeTotal & "&id¿" & varUUID
        Return cadena
    End Function

    Private Sub CmdInsertar_Click(sender As Object, e As EventArgs) Handles CmdInsertar.Click
        Dim Xml As New List(Of Evaluador_XML)
        Dim Contador As Integer = 0
        Dim G, EX, IV, T, Ta, Des As Decimal
        For i As Integer = 0 To Me.Tabla1.Rows.Count - 1
            G = 0
            EX = 0
            IV = 0
            T = 0
            Ta = 0
            Des = 0


            Dim Fila As New DataGridViewRow
            Fila = Me.Tabla1.Rows(i)
            Des = IIf(IsDBNull(Me.Tabla1.Item(Descuento.Index, i).Value), 0, Me.Tabla1.Item(Descuento.Index, i).Value)
            If Me.Tabla1.Item(IVA16.Index, i).Value > 0 And Me.Tabla1.Item(IVA8.Index, i).Value = 0 Then
                G = Me.Tabla1.Item(IVA16.Index, i).Value / 0.16 + Me.Tabla1.Item(ISH.Index, i).Value
                IV = Me.Tabla1.Item(IVA16.Index, i).Value
                Ta = 0.16
            ElseIf Me.Tabla1.Item(IVA8.Index, i).Value > 0 And Me.Tabla1.Item(IVA16.Index, i).Value = 0 Then
                G = Me.Tabla1.Item(IVA8.Index, i).Value / 0.08 + Me.Tabla1.Item(ISH.Index, i).Value
                IV = Me.Tabla1.Item(IVA8.Index, i).Value
                Ta = 0.08
            End If
            EX = Me.Tabla1.Item(SubTotal.Index, i).Value - G - Des
            T = Me.Tabla1.Item(Total.Index, i).Value

            If EX < 1 Then
                EX = 0
                G += EX
            End If
            'If Fila.DefaultCellStyle.BackColor = Color.Red Then
            Xml.Add(New Evaluador_XML() With {.Importe_Grabado = G, .Importe_Exento = EX, .IVA = IV, .Orden = "", .Estatus = Me.Tabla1.Item(Er.Index, i).Value, .Total = T, .Tasa = Ta, .UUID = Me.Tabla1.Item(UUID.Index, i).Value, .Fecha = Me.Tabla1.Item(FechaTimbrado.Index, i).Value, .Emisor = Me.Tabla1.Item(RFCEmisor.Index, i).Value, .Nombre = Me.Tabla1.Item(NombreEmisor.Index, i).Value, .Metodo = Me.Tabla1.Item(FormaDePago.Index, i).Value})
            'End If

        Next

        For Each Archi In Xml
            If XmlExiste(Archi.UUID) = "SI" Then
                Dim sql As String = "INSERT INTO dbo.XmlAuditados"
                Dim F As DateTime = Archi.Fecha
                sql &= "("
                sql &= "	UUID,"
                sql &= "	Orden,"
                sql &= "	ImpGrabado,"
                sql &= "	ImpExcento,"
                sql &= "	ImpCero,"
                sql &= "	Tasa,"
                sql &= "	ImpIva,Errores,Total,Fecha,Emisor,Nombre, Id_Empresa,Metodo"
                sql &= "	)"
                sql &= "VALUES "
                sql &= "	("
                sql &= "	'" & UCase(Archi.UUID) & "',"
                sql &= "	'" & Archi.Orden & "',"
                sql &= "	" & Archi.Importe_Grabado & ","
                sql &= "	" & Archi.Importe_Exento & ","
                sql &= "	0,"
                sql &= "	" & Archi.Tasa & ","
                sql &= "	" & Archi.Iva & ", '" & Archi.Estatus & "', " & Archi.Total & ", " & Eventos.Sql_hoy(F.ToString("dd/MM/yyyy")) & ",'" & Archi.Emisor.Replace("'", "") & "','" & Archi.Nombre.Replace("'", "") & "', " & Me.lstCliente.SelectItem & ",'" & Archi.Metodo & "'"
                sql &= "	)"
                If Eventos.Comando_sql(sql) > 0 Then
                    Contador += 1
                End If
            End If
        Next
        RadMessageBox.Show("Se Insertaron " & Contador & " XML.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
    End Sub
    Private Function XmlExiste(ByVal UUID As String) As String
        If Eventos.ObtenerValorDB("XmlAuditados", "UUID", " UUID = '" & UUID & "' ", True) <> " " Then
            XmlExiste = "NO"
        Else
            XmlExiste = "SI"
        End If

        Return XmlExiste
    End Function
    Private Function ValidarXMl(ByVal Uso As String, ByVal TipoC As String, ByVal Fila As Integer, ByVal Rfc As String, ByVal Moneda As String, ByVal Metodo As String, ByVal Pago As String, ByVal Importe As Decimal) As String
        ValidarXMl = "SI"
        Select Case Uso
            Case "G03 - Gastos General"
                ValidarXMl = "SI"
            Case "G03"
                ValidarXMl = "SI"
            Case Else
                ValidarXMl = "NO"
                Me.Tabla1.Item(Er.Index, Fila).Value = Me.Tabla1.Item(Er.Index, Fila).Value & " No cuenta con el uso /  "
        End Select
        If TipoC <> "Factura" Then
            ValidarXMl = "NO"
            Me.Tabla1.Item(Er.Index, Fila).Value = Me.Tabla1.Item(Er.Index, Fila).Value & " No corresponde a una Factura / "
        End If
        If Rfc <> "AMA9611062U0" Then
            ValidarXMl = "NO"
            Me.Tabla1.Item(Er.Index, Fila).Value = Me.Tabla1.Item(Er.Index, Fila).Value & " No corresponde el RFC / "
        End If
        If Moneda <> "MXN" Then
            ValidarXMl = "NO"
            Me.Tabla1.Item(Er.Index, Fila).Value = Me.Tabla1.Item(Er.Index, Fila).Value & " No contiene el tipo de cambio correcto / "
        End If
        If Metodo.Substring(0, 3) <> "PUE" Then
            ValidarXMl = "NO"
            Me.Tabla1.Item(Er.Index, Fila).Value = Me.Tabla1.Item(Er.Index, Fila).Value & " El metodo de pago debe ser PUE / "
        End If
        Select Case Pago.Substring(0, 2)
            Case "03", "04", "05", "06", "28", "29"
                ValidarXMl = "SI"
            Case "01"
                If Me.Tabla1.Item(Conceptos.Index, Fila).Value Like "*DIESEL*" Or Me.Tabla1.Item(Conceptos.Index, Fila).Value Like "*MAGNA*" Or Me.Tabla1.Item(Conceptos.Index, Fila).Value Like "*PREMIUM*" Then
                    ValidarXMl = "NO"
                    Me.Tabla1.Item(Er.Index, Fila).Value = Me.Tabla1.Item(Er.Index, Fila).Value & " El pago no puede ser en efectivo y la Factura es de Combustible /  "
                Else
                    If Importe >= 2000 Then
                        ValidarXMl = "NO"
                        Me.Tabla1.Item(Er.Index, Fila).Value = Me.Tabla1.Item(Er.Index, Fila).Value & " El pago no puede ser en efectivo es Mayor a $2,000 /  "
                    Else
                        ValidarXMl = "SI"
                    End If
                End If

            Case Else
                ValidarXMl = "NO"
                Me.Tabla1.Item(Er.Index, Fila).Value = Me.Tabla1.Item(Er.Index, Fila).Value & " El Metodo de pago no es Correcto /  "
        End Select
        If Me.Tabla1.Item(FechaTimbrado.Index, Fila).Value < Today.AddDays(-45) Then
            ValidarXMl = "NO"
            Me.Tabla1.Item(Er.Index, Fila).Value = Me.Tabla1.Item(Er.Index, Fila).Value & " El CFDI sobrepasa la fecha valida de Uso /  "
        End If
        Return ValidarXMl
    End Function

    Private Sub CmdAct_Click(sender As Object, e As EventArgs) Handles CmdAct.Click
        Dim Xml As New List(Of Evaluador_XML)
        Dim Contador As Integer = 0
        Dim G, EX, IV, T, Ta, Des As Decimal
        For i As Integer = 0 To Me.Tabla1.Rows.Count - 1
            G = 0
            EX = 0
            IV = 0
            T = 0
            Ta = 0
            Des = 0

            Dim Fila As New DataGridViewRow
            Fila = Me.Tabla1.Rows(i)
            Des = IIf(IsDBNull(Me.Tabla1.Item(Descuento.Index, i).Value), 0, Me.Tabla1.Item(Descuento.Index, i).Value)
            If Me.Tabla1.Item(IVA16.Index, i).Value > 0 And Me.Tabla1.Item(IVA8.Index, i).Value = 0 Then
                G = (Me.Tabla1.Item(IVA16.Index, i).Value / 0.16) + Me.Tabla1.Item(otieps.Index, i).Value + Me.Tabla1.Item(ISH.Index, i).Value

                IV = Me.Tabla1.Item(IVA16.Index, i).Value
                Ta = 0.16
            ElseIf Me.Tabla1.Item(IVA8.Index, i).Value > 0 And Me.Tabla1.Item(IVA16.Index, i).Value = 0 Then
                G = (Me.Tabla1.Item(IVA8.Index, i).Value / 0.08) + Me.Tabla1.Item(otieps.Index, i).Value + Me.Tabla1.Item(ISH.Index, i).Value
                IV = Me.Tabla1.Item(IVA8.Index, i).Value
                Ta = 0.08
            Else
                Ta = 0
            End If
            EX = Me.Tabla1.Item(SubTotal.Index, i).Value - G - Des
            T = Me.Tabla1.Item(Total.Index, i).Value
            If EX < 1 Then
                EX = 0
                G += EX
            End If
            Xml.Add(New Evaluador_XML() With {.Importe_Grabado = G, .Importe_Exento = EX, .Iva = IV, .Orden = "",
                    .Estatus = Me.Tabla1.Item(Er.Index, i).Value, .Total = T, .Tasa = Ta,
                    .UUID = UCase(Me.Tabla1.Item(UUID.Index, i).Value), .Fecha = Me.Tabla1.Item(FechaTimbrado.Index, i).Value,
                    .Emisor = Me.Tabla1.Item(RFCEmisor.Index, i).Value, .Nombre = Me.Tabla1.Item(NombreEmisor.Index, i).Value,
                    .Metodo = Me.Tabla1.Item(FormaDePago.Index, i).Value})

        Next
        Dim sql As String = ""
        For Each Archi In Xml

            If Contador = 999 Then
                If Eventos.Comando_sql(sql) = 0 Then
                    Contador += 1
                End If
                sql = " UPDATE dbo.XmlAuditados SET Tasa = " & Ta & " , ImpGrabado =" & Archi.Importe_Grabado & ", ImpExcento = " & Archi.Importe_Exento & " ,ImpIva = " & Archi.Iva & ",Total= " & Archi.Total & ", Errores = '" & Archi.Estatus & "' WHERE UUID = '" & UCase(Archi.UUID) & "' "
                Contador = 1
            Else
                sql += " UPDATE dbo.XmlAuditados SET Tasa = " & Ta & " , ImpGrabado =" & Archi.Importe_Grabado & ", ImpExcento = " & Archi.Importe_Exento & " ,ImpIva = " & Archi.Iva & ",Total= " & Archi.Total & ", Errores = '" & Archi.Estatus & "' WHERE UUID = '" & UCase(Archi.UUID) & "' " & vbCrLf
                Contador += 1
            End If


        Next
        If Contador > 0 Then
            If Eventos.Comando_sql(sql) = 0 Then
                Contador += 1
            End If
        End If

    End Sub


End Class
