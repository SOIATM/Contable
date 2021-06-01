Imports Telerik.WinControls
Public Class Parcialidades
    Dim activo As Boolean
    Dim Compl As DataSet
    Public serV As String = My.Forms.Inicio.txtServerDB.Text

    '************************************************ Complementos de pago *********************************************************************************

    Private Sub CmdSalirComple_Click(sender As Object, e As EventArgs) Handles CmdSalirComple.Click
        Me.Close()
    End Sub
    Private Sub CmdLimpiaComple_Click(sender As Object, e As EventArgs) Handles CmdLimpiaComple.Click
        If Me.TablaCF.Rows.Count > 0 Then
            LimpiaC()
            Me.LstComple.SelectText = ""
        End If
    End Sub
    Private Sub LimpiaC()
        If Me.TablaCF.RowCount > 0 Then
            Me.TablaCF.Rows.Clear()
        End If
    End Sub
    Private Sub CmdBuscarComple_Click(sender As Object, e As EventArgs) Handles CmdBuscarComple.Click

        activo = True
        LimpiaC()
        If Me.lstCliente.SelectText <> "" Then
            If Buscar_Parametros(Me.lstCliente.SelectItem) = True Then
                Try
                    Compl.Clear()
                Catch ex As Exception
                End Try

                Buscar_Complementos(Me.lstCliente.SelectItem, " and Fecha_Emision >= " & Eventos.Sql_hoy(Me.DtFiComple.Value) & " and Fecha_Emision <= " & Eventos.Sql_hoy(Me.Dtfin.Value) & "")
                SP1.RunWorkerAsync(Me.TablaCF)
                Control.CheckForIllegalCrossThreadCalls = False
                Me.TablaCF.Enabled = True
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
        activo = False
    End Sub
    Private Function Buscar_Parametros(ByVal Cliente As Integer)
        Dim hacer As Boolean
        Dim sql As String = "SELECT  TasaC , TaseE ,  Tasa16,  IvaErogaciones,Contab_Elect  FROM dbo.Parametros_Clientes WHERE Id_Empresa = " & Cliente & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.lbl0.Text = IIf(IsDBNull(ds.Tables(0).Rows(0)("TasaC")) = True, "", ds.Tables(0).Rows(0)("TasaC"))
            Me.lble.Text = IIf(IsDBNull(ds.Tables(0).Rows(0)("TaseE")) = True, "", ds.Tables(0).Rows(0)("TaseE"))
            Me.lbl16.Text = IIf(IsDBNull(ds.Tables(0).Rows(0)("Tasa16")) = True, "", ds.Tables(0).Rows(0)("Tasa16"))
            Me.lblce.Text = IIf(IsDBNull(ds.Tables(0).Rows(0)("Contab_Elect")) = True, "", ds.Tables(0).Rows(0)("Contab_Elect"))
            hacer = True
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay registros de parametros para la Empresa " & Me.lstCliente.SelectText & " ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            hacer = False
        End If
        Return hacer
    End Function
    Private Function Cuenta_cargos(ByVal rfc As String, ByVal letra As String)
        Dim cuenta As String
        Dim sql As String = " select Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, Rtrim(Descripcion) as DES from catalogo_de_cuentas where  clave= '" & Trim(letra) & "'  and nivel3 >0  and rfc= '" & rfc & "' and Id_Empresa = " & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim cadena As String = ds.Tables(0).Rows(0)("Cuenta")
            Dim posi As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
            Dim cuantos As Integer = Len(cadena) - Len(cadena.Substring(0, posi))
            Dim consecutivo As String = cadena.Substring(posi, cuantos)

            sql = " select Rtrim(Descripcion) as Cuenta from catalogo_de_cuentas where  Nivel1= '" & consecutivo.ToString.Substring(0, 4) & "' and nivel2= '" & consecutivo.ToString.Substring(4, 4) & "'  and nivel3 ='" & consecutivo.ToString.Substring(8, 4) & "'  and nivel4= '0000'  and Id_Empresa = " & Me.lstCliente.SelectItem & ""

            Dim ds2 As DataSet = Eventos.Obtener_DS(sql)

            If ds2.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0)("DES")) = Trim(ds2.Tables(0).Rows(0)("Cuenta")) Then
                    cuenta = ds.Tables(0).Rows(0)("Cuenta")
                Else
                    cuenta = ds2.Tables(0).Rows(0)("Cuenta") & " " & ds.Tables(0).Rows(0)("Cuenta")
                End If
            Else
                cuenta = ds.Tables(0).Rows(0)("Cuenta")
            End If
        Else
            cuenta = ""
        End If
        Return cuenta
    End Function
    Private Sub TablaC_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaCF.CellEndEdit
        ComplemantosL(Me.TablaCF.CurrentRow.Index)
    End Sub

    Private Sub Buscar_Complementos(ByVal Id_Empresa As Integer, ByVal periodo As String)
        Dim sql As String = " SELECT  Id_Registro_Xml,	Verificado_Asoc,	Estado_SAT,	Version,	Tipo,	Fecha_Emision,"
        sql &= " 	Fecha_Timbrado,	EstadoPago,	FechaPago,	Serie,	Folio,	UUID,	UUID_Relacion,	RFC_Emisor,"
        sql &= " 	Nombre_Emisor,	LugarDeExpedicion,	RFC_Receptor,	Nombre_Receptor,	ResidenciaFiscal,	NumRegIdTrib,"
        sql &= " 	UsoCFDI,Retenido_IVA + Retenido_ISR  + Total -IVA_16 AS SubTotal,	Descuento,	Total_IEPS,	IVA_16,	Retenido_IVA,	Retenido_ISR,	ISH,	Total,"
        sql &= " 	TotalOriginal,	Total_Trasladados,	Total_Retenidos,	Total_LocalTrasladado,	Total_LocalRetenido,	Complemento,"
        sql &= " 	Moneda,	Tipo_De_Cambio,	Metodos_de_Pago.descripcion   ,	Metodo_de_Pago ,	NumCtaPago,	Condicion_de_Pago,	Conceptos,"
        sql &= " 	Combustible,	IEPS_3,	IEPS_6,	IEPS_7,	IEPS_8,	IEPS_9,	IEPS_26,	IEPS_30,	IEPS_53,	IEPS_160,"
        sql &= " 	Archivo_XML,	Direccion_Emisor,	Localidad_Emisor,	Direccion_Receptor,	Localidad_Receptor,	Autorizada,	Consecutivo_Carga,"
        sql &= " 	Id_Empresa,	Carga_Procesada,	Id_Poliza,Letra_Sat,	Nombre_cuenta,	Xml_Sat.Clave,	Anio_Contable,	Mes_Contable,	Imp_Efectivo,	Cuenta_Efectivo,"
        sql &= "    Imp_Transf,	Banco_Origen,	Cuenta_Origen,	Banco_Destino,Cuenta_Destino,	Fecha_Transaccion,	Imp_Cheque,"
        sql &= "    Nom_Banco_Ch,	Cuenta_Origen_Ch,	No_Cheque,	Fecha_Ch, Cuenta_Cheques ,	Cuenta_Bancos,	Provision_Acreedor,	Provision_Proveedor,"
        sql &= "    Diferencia,	Tipo_Poliza,	Imp_Grabado,	Imp_Exento,	IVA_real,	Prc_Pago_Acumulado,	Total_Real,	Utilidad_Cambiaria,"
        sql &= "    Perdida_Cambiaria,	Imp_Devolucion,	Cuenta_Devolucion 	,Numpol,	RIRS,	RIVA,Ajuste , Anticipos, Cta_Ajuste, Cta_Anticipos,Imp_Provision ,Cta_Orden,"
        sql &= "    Imp_Transf1,        Banco_Origen1,	 Cuenta_Origen1,	 Banco_Destino1, Cuenta_Destino1 	,	 Fecha_Transaccion1,	 Cuenta_Bancos1 "
        sql &= "    FROM dbo.Xml_Sat inner join Metodos_de_Pago on Metodos_de_Pago.clave = Xml_Sat.FormaDePago   where  (Xml_Sat.Emitidas = " & Eventos.Bool2(False) & ") AND (Xml_Sat.Id_Empresa = " & Id_Empresa & ") " & periodo & " AND Xml_Sat.Imp_Provision >0 AND Xml_Sat.Id_Poliza IS NOT NULL AND (Tiene_Comple = 0 OR Tiene_Comple IS NULL )"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaCF.RowCount = 1
            Dim contab As DataSet = Eventos.Obtener_DS(" Select Rtrim(Clave) as Clave from ClaveEgresos where ClaveEgresos.Id_Empresa = " & Me.lstCliente.SelectItem & " and (CargoG =1 or CargoE = 1 ) and Negativo=0 ")
            If contab.Tables(0).Rows.Count > 0 Then
                Me.LetraContabilidadComplemento.DataSource = contab.Tables(0)
                Me.LetraContabilidadComplemento.DisplayMember = contab.Tables(0).Columns(0).Caption.ToString
            End If

            Dim Tipo As DataSet = Eventos.Obtener_DS(" Select convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
            If Tipo.Tables(0).Rows.Count > 0 Then
                Me.TipPolComplemento.DataSource = Tipo.Tables(0)
                Me.TipPolComplemento.DisplayMember = Tipo.Tables(0).Columns(0).Caption.ToString
            End If

            Dim Ctaor As DataSet = Eventos.Obtener_DS(" Select 'SI' as Clave  UNION sELECT 'NO' as Clave")
            If Ctaor.Tables(0).Rows.Count > 0 Then
                Me.CtaOrdenC.DataSource = Ctaor.Tables(0)
                Me.CtaOrdenC.DisplayMember = Ctaor.Tables(0).Columns(0).Caption.ToString
            End If
            'Cargar Bancos
            Cargar_bancosComple("Transf")
            Cargar_bancosComple("Cheq")
            Me.TablaCF.RowCount = ds.Tables(0).Rows.Count
            Compl = ds
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay registros para procesar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
    Private Sub Complemento(ByVal Ds As DataSet)
        Try
            If Ds.Tables(0).Rows.Count > 0 Then

            End If
        Catch ex As Exception
            Exit Sub
        End Try
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Calculando Pagos por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Ds.Tables(0).Rows.Count
        For j As Integer = 0 To Ds.Tables(0).Rows.Count - 1
            Try
                Me.TablaCF.Item(ApliComplemento.Index, j).Value = False
                Me.TablaCF.Item(Id_Complemento.Index, j).Value = Ds.Tables(0).Rows(j)("Id_Registro_Xml")
                Me.TablaCF.Item(Tip.Index, j).Value = ""
                Me.TablaCF.Item(FechaEComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("Fecha_Emision").ToString()
                Me.TablaCF.Item(UUIDComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("UUID")
                Me.TablaCF.Item(UUIDRComplemento.Index, j).Value = IIf(IsDBNull(Ds.Tables(0).Rows(j)("UUID_Relacion")) = True, "", Ds.Tables(0).Rows(j)("UUID_Relacion"))
                Me.TablaCF.Item(UCFDIComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("UsoCFDI")
                Me.TablaCF.Item(RFCComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("RFC_Emisor")
                Me.TablaCF.Item(NEmComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("Nombre_Emisor")
                Me.TablaCF.Item(ConceptoCom.Index, j).Value = Ds.Tables(0).Rows(j)("Descripcion")
                Me.TablaCF.Item(MPc.Index, j).Value = Ds.Tables(0).Rows(j)("Metodo_de_Pago")
                Me.TablaCF.Item(NomCtaComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("Nombre_cuenta")
                Me.TablaCF.Item(SubTotComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("SubTotal")
                Me.TablaCF.Item(IvaComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("IVA_16")
                Me.TablaCF.Item(TotComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("Total")
                Me.TablaCF.Item(LetraSatComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("Letra_Sat")
                Dim year As String = Ds.Tables(0).Rows(j)("Fecha_Emision").ToString.Substring(6, 4)
                Dim month As String = Ds.Tables(0).Rows(j)("Fecha_Emision").ToString.Substring(3, 2)
                Me.TablaCF.Item(AnioComplemento.Index, j).Value = IIf(IsDBNull(Ds.Tables(0).Rows(j)("Anio_Contable")) = True, year, Ds.Tables(0).Rows(j)("Anio_Contable"))
                Me.TablaCF.Item(MesComplemento.Index, j).Value = IIf(IsDBNull(Ds.Tables(0).Rows(j)("Mes_Contable")) = True, month, Ds.Tables(0).Rows(j)("Mes_Contable"))
                Me.TablaCF.Item(MoneComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("Moneda")
                Try
                    Me.TablaCF.Item(RefcLAVE.Index, j).Value = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, j).Value.ToString().Trim(), "Clave").ToString().Trim()
                    ComplemantosL(j)
                Catch ex As Exception

                End Try
                Dim PorcenPro As Decimal = 0 ' Calcula valores de la factura
                Try
                    PorcenPro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, j).Value, "Total_Real")
                Catch ex As Exception
                    PorcenPro = 0
                End Try







            Catch ex As Exception

            End Try
            frm.Barra.Value = j
        Next
        frm.Close()
    End Sub

    Private Function Busacar_parcialidades(ByVal UUID As String)
        Dim Importe As Decimal = 0
        Dim sql As String = " SELECT sum(Parcialidades.Imp_Transf + Parcialidades.Imp_Cheque +  Parcialidades.Imp_Devolucion + Parcialidades.Imp_Efectivo ) AS Importe  FROM Parcialidades WHERE Id_Empresa = " & Me.lstCliente.SelectItem & " AND  UUID_Relacion  ='" & UUID & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Importe = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))

        End If

        Return Importe
    End Function
    Private Function ImpFaCURA(ByVal uuid As String, ByVal CAMPO As String)
        My.Forms.Inicio.txtServerDB.Text = serV
        Dim iMP
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT " & CAMPO & " FROM dbo.Xml_Sat WHERE UUID = '" & uuid & "' and Id_Empresa = " & Me.lstCliente.SelectItem & "")
        If ds.Tables(0).Rows.Count > 0 Then
            iMP = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
        Else
            iMP = 0
        End If
        Return iMP
    End Function

    Private Function Calcula_Moneda(ByVal fecha As String)
        Dim importe As Decimal = 0
        Dim ds As DataSet = Eventos.Obtener_DS(" Select Costo_Pesos FROM Tipos_de_Cambio WHERE Fecha_Aplicacion = " & Eventos.Sql_hoy(fecha) & "")
        If ds.Tables(0).Rows.Count > 0 Then
            importe = ds.Tables(0).Rows(0)("Costo_Pesos")
        Else
            importe = 0
        End If
        Return importe
    End Function
    Private Sub Color_ColumnasC()
        Dim contador As Integer = 0
        For Each Column As DataGridViewColumn In TablaCF.Columns
            If Column.Index = AnioComplemento.Index Or Column.Index = MesComplemento.Index Or Column.Index = BancoOrigenTComplemento.Index Or Column.Index = BancoDtComplemento.Index Or Column.Index = FTComplemento.Index Or Column.Index = BancoCHComplemento.Index Or Column.Index = NoChComplemento.Index Or Column.Index = FCHComplemento.Index Or Column.Index = ProvAComplemento.Index Or Column.Index = ProvPComplemento.Index Or Column.Index = UCComplemento.Index Or Column.Index = PCComplemento.Index Or Column.Index = ImpDevComplemento.Index Or Column.Index = CtaDevComplemento.Index Then
                Column.DefaultCellStyle.BackColor = Color.RoyalBlue
            ElseIf Column.Index = AjusComplemento.Index Then
                Column.DefaultCellStyle.BackColor = Color.GreenYellow
            ElseIf Column.Index = AntiComplemento.Index Then
                Column.DefaultCellStyle.BackColor = Color.LawnGreen
            ElseIf Column.Index = ImpProviComplemento.Index Then
                Column.DefaultCellStyle.BackColor = Color.MediumPurple
            ElseIf Column.Index = TipPolComplemento.Index Or Column.Index = NumPolComplemento.Index Then
                Column.DefaultCellStyle.BackColor = Color.Orange
            ElseIf Column.Index = NomCtaComplemento.Index Or Column.Index = CtaBancosComplemento.Index Or Column.Index = CtaChequeC.Index Then
                Column.DefaultCellStyle.BackColor = Color.Orange
            ElseIf Column.Index = ImpEfComplemento.Index Or Column.Index = ImpTComplemento.Index Or Column.Index = ImpChComplemento.Index Then
                Column.DefaultCellStyle.BackColor = Color.OliveDrab
            End If
        Next
    End Sub
    Private Sub CmdProcesaComple_Click(sender As Object, e As EventArgs) Handles CmdProcesaComple.Click


        If Me.TablaCF.Rows.Count > 0 Then
            If Me.lstCliente.SelectText <> "" Then
                If Verifica_catalogo_cliente(Me.lstCliente.SelectItem) = True Then
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    If RadMessageBox.Show("La Empresa " & Me.lstCliente.SelectText & " es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Me.BarraComple.Maximum = Me.TablaCF.RowCount - 1
                        Me.BarraComple.Minimum = 0
                        Me.BarraComple.Value1 = 0

                        For p As Integer = 0 To Me.TablaCF.RowCount - 1
                            If Me.TablaCF.Item(ApliComplemento.Index, p).Value = True Then ' se paso todos los filtros de creacion
                                Codificar_polizasComplementos(p)
                            End If
                            If Me.BarraComple.Value1 = Me.BarraComple.Maximum Then
                                Me.BarraComple.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                Me.BarraComple.Value1 = 0
                            Else
                                Me.BarraComple.Value1 += 1
                            End If

                        Next
                    End If
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    RadMessageBox.Show("No existe Catalogo de cuentas para: " & Me.lstCliente.SelectText & "", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                End If
                Me.CmdBuscarComple.PerformClick()
            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If



        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If


    End Sub
    Private Function Verifica_catalogo_cliente(ByVal cliente As Integer) ' se verifica el Catalogo de cuentas

        Dim hacer As Boolean
        Dim sql As String = " Select * From catalogo_de_cuentas where Id_Empresa = " & cliente & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = True
        Else
            hacer = False
        End If
        Return hacer
    End Function
    Private Sub Guardar_Complementos(ByVal Poliza As String, ByVal i As Integer)


        Guardar_Cmpl(IIf(IsDBNull(Me.TablaCF.Item(UUIDComplemento.Index, i).Value), "", Me.TablaCF.Item(UUIDComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(LetraSatComplemento.Index, i).Value), "", Me.TablaCF.Item(LetraSatComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(NomCtaComplemento.Index, i).Value), "", Me.TablaCF.Item(NomCtaComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(LetraContabilidadComplemento.Index, i).Value), "", Me.TablaCF.Item(LetraContabilidadComplemento.Index, i).Value),
                           Me.TablaCF.Item(AnioComplemento.Index, i).Value, Me.TablaCF.Item(MesComplemento.Index, i).Value, Me.TablaCF.Item(ImpEfComplemento.Index, i).Value, IIf(IsDBNull(Me.TablaCF.Item(CtaEfComplemento.Index, i).Value), "", Me.TablaCF.Item(CtaEfComplemento.Index, i).Value),
                           Me.TablaCF.Item(ImpTComplemento.Index, i).Value, IIf(IsDBNull(Me.TablaCF.Item(BancoOrigenTComplemento.Index, i).Value), "", Me.TablaCF.Item(BancoOrigenTComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(CtaOTComplemento.Index, i).Value), "", Me.TablaCF.Item(CtaOTComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(BancoDtComplemento.Index, i).Value), "", Me.TablaCF.Item(BancoDtComplemento.Index, i).Value),
                           IIf(IsDBNull(Me.TablaCF.Item(FTComplemento.Index, i).Value), "", Me.TablaCF.Item(FTComplemento.Index, i).Value), Me.TablaCF.Item(ImpChComplemento.Index, i).Value, IIf(IsDBNull(Me.TablaCF.Item(BancoCHComplemento.Index, i).Value), "", Me.TablaCF.Item(BancoCHComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(CtaOchComplemento.Index, i).Value), "", Me.TablaCF.Item(CtaOchComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(NoChComplemento.Index, i).Value), "", Me.TablaCF.Item(NoChComplemento.Index, i).Value),
                          IIf(IsDBNull(Me.TablaCF.Item(FCHComplemento.Index, i).Value), "", Me.TablaCF.Item(FCHComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(CtaBancosComplemento.Index, i).Value), "", Me.TablaCF.Item(CtaBancosComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(ProvAComplemento.Index, i).Value), 0, Me.TablaCF.Item(ProvAComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(ProvPComplemento.Index, i).Value), 0, Me.TablaCF.Item(ProvPComplemento.Index, i).Value),
                           IIf(IsDBNull(Me.TablaCF.Item(DifComplemento.Index, i).Value), 0, Me.TablaCF.Item(DifComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(TipPolComplemento.Index, i).Value), "", Me.TablaCF.Item(TipPolComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(ImpGComplemento.Index, i).Value), 0, Me.TablaCF.Item(ImpGComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(ImpEComplemento.Index, i).Value), 0, Me.TablaCF.Item(ImpEComplemento.Index, i).Value),
                          IIf(IsDBNull(Me.TablaCF.Item(IvaRComplemento.Index, i).Value), 0, Me.TablaCF.Item(IvaRComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(PPAComplemento.Index, i).Value), 0, Me.TablaCF.Item(PPAComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(TRComplemento.Index, i).Value), 0, Me.TablaCF.Item(TRComplemento.Index, i).Value), Me.TablaCF.Item(UCComplemento.Index, i).Value, IIf(IsDBNull(Me.TablaCF.Item(PCComplemento.Index, i).Value), 0, Me.TablaCF.Item(PCComplemento.Index, i).Value),
                           Me.TablaCF.Item(ImpDevComplemento.Index, i).Value, IIf(IsDBNull(Me.TablaCF.Item(CtaDevComplemento.Index, i).Value), "", Me.TablaCF.Item(CtaDevComplemento.Index, i).Value), Me.TablaCF.Item(Id_Complemento.Index, i).Value, IIf(IsDBNull(Me.TablaCF.Item(NumPolComplemento.Index, i).Value), "", Me.TablaCF.Item(NumPolComplemento.Index, i).Value), Me.TablaCF.Item(RISRComplemento.Index, i).Value, Me.TablaCF.Item(RIVAComplemento.Index, i).Value, IIf(IsDBNull(Me.TablaCF.Item(CtaDTComplemento.Index, i).Value), "", Me.TablaCF.Item(CtaDTComplemento.Index, i).Value),
                           IIf(IsDBNull(Me.TablaCF.Item(AjusComplemento.Index, i).Value), 0, Me.TablaCF.Item(AjusComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(CtaAjusComplemento.Index, i).Value), "", Me.TablaCF.Item(CtaAjusComplemento.Index, i).Value),
                           IIf(IsDBNull(Me.TablaCF.Item(AntiComplemento.Index, i).Value), 0, Me.TablaCF.Item(AntiComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(CtaAntiComplemento.Index, i).Value), "", Me.TablaCF.Item(CtaAntiComplemento.Index, i).Value),
                           IIf(IsDBNull(Me.TablaCF.Item(ImpProviComplemento.Index, i).Value), 0, Me.TablaCF.Item(ImpProviComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(CtaOrdenC.Index, i).Value), "", Me.TablaCF.Item(CtaOrdenC.Index, i).Value), IIf(IsDBNull(Me.TablaCF.Item(CtaChequeC.Index, i).Value), "", Me.TablaCF.Item(CtaChequeC.Index, i).Value), Poliza)


    End Sub

    Private Sub Guardar_Cmpl(ByVal Folio As String, ByVal Letra_Sat As String, ByVal Nombre_cuenta As String, ByVal Clave As String,
                            ByVal Anio_Contable As String, ByVal Mes_Contable As String, ByVal Imp_Efectivo As Decimal, ByVal Cuenta_Efectivo As String,
                            ByVal Imp_Transf As Decimal, ByVal Banco_Origen As String, ByVal Cuenta_Origen As String, ByVal Banco_Destino As String,
                            ByVal Fecha_Transaccion As String, ByVal Imp_Cheque As Decimal, ByVal Nom_Banco_Ch As String, ByVal Cuenta_Origen_Ch As String, ByVal No_Cheque As String,
                            ByVal Fecha_Ch As String, ByVal Cuenta_Bancos As String, ByVal Provision_Acreedor As Decimal, ByVal Provision_Proveedor As Decimal,
                            ByVal Diferencia As Decimal, ByVal Tipo_Poliza As String, ByVal Imp_Grabado As Decimal, ByVal Imp_Exento As Decimal,
                            ByVal IVA_real As Decimal, ByVal Prc_Pago_Acumulado As Decimal, ByVal Total_Real As Decimal, ByVal Utilidad_Cambiaria As Decimal, ByVal Perdida_Cambiaria As Decimal,
                            ByVal Imp_Devolucion As Decimal, ByVal Cuenta_Devolucion As String, ByVal Id_Registro_Xml As Integer, ByVal numpol As String, ByVal risr As Decimal, ByVal riva As Decimal,
                            ByVal ctadestino As String, ByVal ajus As Decimal, ByVal ctaajus As String, ByVal anti As Decimal, ByVal ctaanti As String, ByVal ImpProvis As Decimal, ByVal CtaOrdenC As String, ByVal Cuenta_Cheques As String, ByVal Poliza As String)
        Dim sql As String = " INSERT INTO dbo.Parcialidades
	(
	UUID_Relacion,
	Id_Empresa,Id_Poliza,
	Emitidas,
	Nombre_cuenta,
	Clave,
	Anio_Contable,
	Mes_Contable,
	Imp_Efectivo,
	Cuenta_Efectivo,
	Imp_Transf,
	Banco_Origen,
	Cuenta_Origen,
	Banco_Destino,
	Fecha_Transaccion,
	Imp_Cheque,
	Nom_Banco_Ch,
	Cuenta_Origen_Ch,
	Cuenta_Cheques,
	No_Cheque,
	Fecha_Ch,
	Cuenta_Bancos,
	Provision_Acreedor,
	Provision_Proveedor,
	Diferencia,
	Tipo_Poliza,
	Imp_Grabado,
	Imp_Exento,
	IVA_real,
	Prc_Pago_Acumulado,
	Total_Real,
	Utilidad_Cambiaria,
	Perdida_Cambiaria,
	Imp_Devolucion,
	Cuenta_Devolucion,
	Numpol,
	RIRS,
	RIVA,
 	Ajuste,
	Anticipos,
	Cta_Ajuste,
	Cta_Anticipos,
	Imp_Provision
	)
VALUES 
	(
	'" & Folio & "',
	" & Me.lstCliente.SelectItem & ",'" & Poliza & "',
	0,
	'" & Nombre_cuenta & "' ,
	'" & Clave & "'  ,
	'" & Anio_Contable & "'  ,
	'" & Mes_Contable & "' ,
	" & Imp_Efectivo & ",
	'" & Cuenta_Efectivo & "'  ,
	" & Imp_Transf & " ,
	'" & Banco_Origen & "'  ,
	'" & Cuenta_Origen & "'  ,
	'" & Banco_Destino & "'  ,
	'" & Fecha_Transaccion & "' ,
	 " & Imp_Cheque & "   ,
	 '" & Nom_Banco_Ch & "'  ,
	 '" & Cuenta_Origen_Ch & "'   ,
	  '" & Cuenta_Cheques & "',
	 '" & No_Cheque & "'  ,
       '" & Fecha_Ch & "'  ,
	'" & Cuenta_Bancos & "',
	" & Provision_Acreedor & ",
	" & Provision_Proveedor & " ,
	" & Diferencia & ",
	'" & Tipo_Poliza & "',
	" & Imp_Grabado & ",
	" & Imp_Exento & ",
	" & IVA_real & " ,
	" & Prc_Pago_Acumulado & ",
	" & Total_Real & " ,
	" & Utilidad_Cambiaria & ",
	" & Perdida_Cambiaria & ",
	" & Imp_Devolucion & ",
	'" & Cuenta_Devolucion & "' ,
	" & numpol & " ,
	" & risr & " ,
	" & riva & "  ,
	" & ajus & ",
	" & anti & ",
	'" & ctaajus & "',
	'" & ctaanti & "',
	" & ImpProvis & "
	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("GuardaXMLC", sql)
        End If
    End Sub
    Private Function Calcula_polizaC(ByVal i As Integer)
        Dim mess As String = IIf(Len(Me.TablaCF.Item(MesComplemento.Index, i).Value) = 1, "0" & Me.TablaCF.Item(MesComplemento.Index, i).Value, Me.TablaCF.Item(MesComplemento.Index, i).Value)
        Dim poliza As String = Eventos.Num_polizaS(Me.lstCliente.SelectItem, Checa_tipo(Me.TablaCF.Item(TipPolComplemento.Index, i).Value, Me.lstCliente.SelectItem), Me.TablaCF.Item(AnioComplemento.Index, i).Value, mess, Busca_tipificar(Me.TablaCF.Item(TipPolComplemento.Index, i).Value))

        Return poliza
    End Function
    Private Function Busca_tipificar(ByVal tipos As String)
        Dim tipo As String = ""
        Dim sql As String = " SELECT Id_Tipo_Pol_Sat FROM Tipos_Poliza_Sat WHERE Id_Empresa= " & Me.lstCliente.SelectItem & " AND clave = '" & tipos.Substring(0, 3) & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            tipo = ds.Tables(0).Rows(0)(0)
        Else
            tipo = "N/A"
        End If
        Return tipo
    End Function
    Private Function Checa_tipo(ByVal tipo As String, ByVal cliente As Integer)
        Dim clave As String = ""
        Dim sql As String = "SELECT Id_Tipo_Pol_Sat FROM Tipos_Poliza_Sat WHERE Id_Empresa= " & cliente & " AND clave = '" & tipo.Substring(0, 3) & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            clave = ds.Tables(0).Rows(0)("Id_Tipo_Pol_Sat")
        Else
            clave = 0
        End If
        Return clave
    End Function
    Private Sub Codificar_polizasComplementos(ByVal posicion As Integer)



        Dim poliza_Sistema As String = ""
        '  If Me.TablaImportar.Item(Psistema.Index, posicion).Value <> "" Then ' quitar linea despues del desbloqueo
        poliza_Sistema = Calcula_polizaC(posicion)
        '  End If

        'Calcular consecutivo poliza
        Dim posi As Integer = InStr(1, poliza_Sistema, "-", CompareMethod.Binary)
        Dim cuantos As Integer = Len(poliza_Sistema) - Len(poliza_Sistema.Substring(0, posi))
        Dim consecutivo As Integer = Val(poliza_Sistema.Substring(posi, cuantos))
        'Crear poliza
        'Checar dia de la Poliza poner if
        Dim dia As String = ""
        Dim Chk As String = ""
        If Me.TablaCF.Item(ImpTComplemento.Index, posicion).Value > 0 Then
            dia = Me.TablaCF.Item(FTComplemento.Index, posicion).Value.ToString.Substring(0, 2)
            Chk = ""
        ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, posicion).Value > 0 Then
            dia = Me.TablaCF.Item(FechaEComplemento.Index, posicion).Value.ToString.Substring(0, 2)
            Chk = ""
        ElseIf Me.TablaCF.Item(ImpChComplemento.Index, posicion).Value > 0 Then
            dia = Me.TablaCF.Item(FCHComplemento.Index, posicion).Value.ToString.Substring(0, 2)
            Chk = "Ch/ " & Me.TablaCF.Item(NoChComplemento.Index, posicion).Value.ToString() & " F/ "
        Else
            dia = Me.TablaCF.Item(FechaEComplemento.Index, posicion).Value.ToString.Substring(0, 2)
        End If
        Dim leyenda As String = ""
        If Chk = "" Then
            leyenda = "Pago S/Fact" & " " & Trim(Me.TablaCF.Item(UUIDComplemento.Index, posicion).Value)
        Else
            leyenda = Chk
        End If

        ' CREAR iF para calcular el dia
        If Creapoliza(poliza_Sistema, Me.TablaCF.Item(AnioComplemento.Index, posicion).Value, Me.TablaCF.Item(MesComplemento.Index, posicion).Value, dia,
                   consecutivo, Checa_tipo(Me.TablaCF.Item(TipPolComplemento.Index, posicion).Value, Me.lstCliente.SelectItem),
                   Me.TablaCF.Item(FechaEComplemento.Index, posicion).Value, leyenda, "Carga", Me.TablaCF.Item(NumPolComplemento.Index, posicion).Value, Me.TablaCF.Item(Id_Complemento.Index, posicion).Value, True) = True Then

            If Me.TablaCF.Item(ImpEfComplemento.Index, posicion).Value > 0 Then
                ' Insertar registro contabiidad electronica efectivo
                Inserta_Comprobante_Fiscal_Efectivo(poliza_Sistema, Me.TablaCF.Item(AnioComplemento.Index, posicion).Value, Me.TablaCF.Item(MesComplemento.Index, posicion).Value,
                                Me.TablaCF.Item(RFCComplemento.Index, posicion).Value, Me.TablaCF.Item(TipPolComplemento.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaCF.Item(FechaEComplemento.Index, posicion).Value,
                              "", "", "", "", Me.TablaCF.Item(ImpEfComplemento.Index, posicion).Value)
            End If
            If Trim(UCase(Me.lblce.Text)) = "TRUE" Then ' SE INSERTA CONTABILIDAD ELECTRONICA

                If Me.TablaCF.Item(ImpTComplemento.Index, posicion).Value > 0 Then
                    ' Insertar registro contabiidad electronica Transferencia
                    Dim cadena As String = Me.TablaCF.Item(BancoOrigenTComplemento.Index, posicion).Value
                    Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim BO As String = cadena.Substring(0, posil - 1)

                    cadena = Me.TablaCF.Item(BancoDtComplemento.Index, posicion).Value
                    posil = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim Bd As String = cadena.Substring(0, posil - 1)

                    Inserta_Comprobante_Fiscal_Transf(poliza_Sistema, Me.TablaCF.Item(AnioComplemento.Index, posicion).Value, Me.TablaCF.Item(MesComplemento.Index, posicion).Value,
                                    Me.TablaCF.Item(RFCComplemento.Index, posicion).Value, Me.TablaCF.Item(TipPolComplemento.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaCF.Item(FTComplemento.Index, posicion).Value,
                                  "", BO, Me.TablaCF.Item(CtaOTComplemento.Index, posicion).Value, Me.TablaCF.Item(UUIDComplemento.Index, posicion).Value, Me.TablaCF.Item(ImpTComplemento.Index, posicion).Value, Bd, Me.TablaCF.Item(CtaDTComplemento.Index, posicion).Value)
                End If

                If Me.TablaCF.Item(ImpChComplemento.Index, posicion).Value > 0 Then
                    ' Insertar registro contabiidad electronica Cheque
                    Dim cadena As String = Me.TablaCF.Item(BancoCHComplemento.Index, posicion).Value
                    Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim BO As String = cadena.Substring(0, posil - 1)

                    Inserta_Comprobante_Fiscal_Cheque(poliza_Sistema, Me.TablaCF.Item(AnioComplemento.Index, posicion).Value, Me.TablaCF.Item(MesComplemento.Index, posicion).Value,
                                    Me.TablaCF.Item(RFCComplemento.Index, posicion).Value, Me.TablaCF.Item(TipPolComplemento.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaCF.Item(FCHComplemento.Index, posicion).Value,
                                  Me.TablaCF.Item(NoChComplemento.Index, posicion).Value, BO, Me.TablaCF.Item(CtaOchComplemento.Index, posicion).Value, Me.TablaCF.Item(UUIDComplemento.Index, posicion).Value, Me.TablaCF.Item(ImpChComplemento.Index, posicion).Value)
                End If
            End If
            Guardar_Complementos(poliza_Sistema, posicion)
            If Me.TablaCF.Item(DifComplemento.Index, posicion).Value = 0 Then
                Actualiza_Factura(Me.TablaCF.Item(Id_Complemento.Index, posicion).Value)
            End If
            'Verificara el sistema las polizas automatizadas
            CrearDetalleComplementoNuevo(posicion, poliza_Sistema)
        End If
    End Sub
    Private Sub Actualiza_Factura(ByVal id As String)
        Dim SQL As String = "UPDATE dbo.Xml_Sat SET Tiene_Comple = 1 WHERE Id_Registro_Xml = " & id & " "
        If Eventos.Comando_sql(SQL) > 0 Then
            Eventos.Insertar_usuariol("COMPLE", SQL)
        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Cheque(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
	(
    anio,    mes,    tipo,      RFC_Ce,
    No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
    Id_Poliza,    Tipo_CE	) VALUES	("

        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,     
        sql &= " '" & tipo & "'," '@tipo    

        sql &= " '" & Rfc_Emisor & "'," '@rfc_ce,                
        sql &= " '" & No_cheque & "'," '@no_cheque,  
        sql &= " '" & no_banco & "'," '@no_banco,  
        sql &= " '" & cuenta_origen & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Importe & "	," '@importe,                    
        sql &= " '" & id_poliza & "', " '@id_poliza,  
        sql &= " 'H' " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarCeH", sql)
        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Efectivo(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
	(     anio,    mes,    tipo,       RFC_Ce,
    No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
    Id_Poliza,    Tipo_CE	) VALUES	("

        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,     
        sql &= " '" & tipo & "'," '@tipo    

        sql &= " '" & Rfc_Emisor & "'," '@rfc_ce,                
        sql &= " '" & No_cheque & "'," '@no_cheque,  
        sql &= " '" & no_banco & "'," '@no_banco,  
        sql &= " '" & cuenta_origen & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Importe & "	," '@importe,                    
        sql &= " '" & id_poliza & "', " '@id_poliza,  
        sql &= " 'P' " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarCeE", sql)
        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Transf(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal, ByVal bancoD As String, ByVal cuentaD As String)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
	(
    anio,    mes,    tipo,       RFC_Ce,
    No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
    Id_Poliza,    Tipo_CE,Banco_Destino,Cuenta_Destino	) VALUES	("

        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,     
        sql &= " '" & tipo & "'," '@tipo    

        sql &= " '" & Rfc_Emisor & "'," '@rfc_ce,                
        sql &= " '" & No_cheque & "'," '@no_cheque,  
        sql &= " '" & no_banco & "'," '@no_banco,  
        sql &= " '" & cuenta_origen & "'," '@cuenta_origen,  
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_mov,    
        sql &= " " & Importe & "	," '@importe,                    
        sql &= " '" & id_poliza & "', " '@id_poliza,  
        sql &= " 'T','" & Trim(bancoD) & "', '" & Trim(cuentaD.Replace("/", "")) & "' " '@tipo_ce, 
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarCeT", sql)
        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal fecha As String,
                           ByVal Folio_Fiscal As String, ByVal Referencia As String, ByVal Importe As Decimal)
        Dim sql As String = "INSERT INTO dbo.Facturas"
        sql &= " 	(                   "
        sql &= " 	ID_anio,                    "
        sql &= " 	ID_mes,                     "
        sql &= " 	Id_Poliza,                  "
        sql &= " 	RFC_Emisor,                 "
        sql &= " 	Folio_Fiscal,               "
        sql &= " 	Referencia,                 "
        sql &= " 	Importe,                "
        sql &= " 	Fecha_Comprobante,          "
        sql &= " 	Detalle_Comp_Electronico,Id_Empresa"
        sql &= "    )                         "
        sql &= " VALUES "
        sql &= "(                             "
        sql &= " '" & anio & "',	" '@id_anio,                   
        sql &= " '" & mes & "'," '@id_mes,                    
        sql &= " '" & id_poliza & "'," '@id_poliza,                 
        sql &= " '" & Rfc_Emisor & "'," '@rfc_emisor,                
        sql &= " '" & Folio_Fiscal & "'," '@folio_fiscal,              
        sql &= " '" & Referencia & "'," '@referencia,                
        sql &= " " & Importe & "	," '@importe,                   
        sql &= " " & Eventos.Sql_hoy(fecha) & "," '@fecha_comprobante,         
        sql &= " 'C'," & Me.lstCliente.SelectItem & "" '@detalle_comp_electronico   
        sql &= " )"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarFacturas", sql)

        End If
    End Sub
    Private Function Creapoliza(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String, ByVal dia As String,
                         ByVal consecutivo As Integer, ByVal tipo As Integer, ByVal fecha As String,
                         ByVal concepto As String, ByVal movimiento As String, ByVal num_pol As Integer, ByVal registro As Integer, Optional ByVal comple As Boolean = False)
        Dim hacer As Boolean
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Polizas"
        sql &= "("
        sql &= " 	Id_Poliza,      "
        sql &= "     ID_anio,        "
        sql &= "     ID_mes,        "
        sql &= "     ID_dia,        "
        sql &= "     consecutivo,    "
        sql &= "     Num_Pol,    "
        sql &= "     Id_Tipo_Pol_Sat,"
        sql &= "     Fecha,          "
        sql &= "     Concepto,      "
        sql &= "     Id_Empresa,     "
        sql &= "     No_Mov,        "
        sql &= "     Fecha_captura,  "
        sql &= "     Movto,         "
        sql &= "     Usuario,Aplicar_Poliza         "
        sql &= " 	)               "
        sql &= " VALUES              "
        sql &= " 	(               "
        sql &= " 	'" & id_poliza & "'," '@id_poliza,         
        sql &= " 	" & anio & "," '@id_anio,           
        sql &= " 	'" & mes & "'," '@id_mes,     
        sql &= " 	'" & dia & "'," '@id_dia,     
        sql &= " 	" & consecutivo & "," '@consecutivo,   
        sql &= " 	" & num_pol & "," '@num_pol,  
        sql &= " 	" & tipo & "," '@id_tipo_poliza, 
        sql &= " 	" & Eventos.Sql_hoy(fecha) & "," '@fecha,             
        sql &= " 	'" & concepto & "'," '@concepto,          
        sql &= " 	" & Me.lstCliente.SelectItem & "," '@Id_Empresa,        
        sql &= " 	'" & movimiento & "'," '@no_mov,            
        sql &= " 	" & Eventos.Sql_hoy("" & dia & "/" & mes & "/" & anio & "") & "," '@fecha_captura,     
        sql &= " 	'A'," '@movto,             
        sql &= "  '" & Eventos.Usuario(Inicio.LblUsuario.Text) & "', 1" '@usuario            
        sql &= " 	) "

        If Eventos.Comando_sql(sql) > 0 Then
            hacer = True
            Eventos.Insertar_usuariol("InsertarPolizCp", sql)

        Else
            hacer = False
        End If
        Return hacer
    End Function
    Private Sub Crea_detalle_poliza(ByVal id_poliza As String, ByVal item As Integer, ByVal cargo As Decimal,
                                       ByVal Abono As Decimal, ByVal cuenta As String, ByVal cheque As String)
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Detalle_Polizas"
        sql &= "(   "
        sql &= " Id_Poliza,      "
        sql &= " ID_item,       "
        sql &= " Cargo,          "
        sql &= " Abono,         "
        sql &= " Fecha_captura,  "
        sql &= " Movto,"
        sql &= " Cuenta, "
        sql &= " No_cheque  "
        sql &= " ) "
        sql &= " VALUES "
        sql &= "( "
        sql &= " '" & id_poliza & "'	," '@id_poliza,     
        sql &= "" & item & "," '@id_item,       
        sql &= "" & cargo & "," '@cargo,         
        sql &= "" & Abono & "," '@abono,         
        sql &= "" & Eventos.Sql_hoy() & "," '@fecha_captura, 
        sql &= " 'A'	," '@movto,         
        sql &= " " & cuenta & "	," '@cuenta,        
        sql &= " '" & cheque & "'" '@no_cheque      
        sql &= " 	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarPolizD", sql)
        End If
    End Sub
    Private Function RegresaCunetaComple(ByVal cuenta As String, ByVal rfc As String, ByVal posicion As Integer, Optional ByVal tipo As Integer = 0)
        Dim Cta As String = ""
        Dim sql As String = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND Nivel3 = '" & cuenta.Substring(8, 4) & "' AND Nivel4 > 0 AND RFC = '" & rfc & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cta = ds.Tables(0).Rows(0)(0)
        Else
            'No existe la cuenta y la inserta
            If tipo = 1 Then
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "max (Nivel3 ) + 1 ", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3 >= 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), Cta.ToString.Substring(0, 4),
                               "0000", cuenta.Substring(0, 8) & Cta & "0000", Me.TablaCF.Item(RFCComplemento.Index, posicion).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaCF.Item(LetraSatComplemento.Index, posicion).Value, Me.TablaCF.Item(RFCComplemento.Index, posicion).Value)
                Cta = cuenta.Substring(0, 8) & Cta & "0000"
            Else
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "max (Nivel4 ) + 1 ", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & cuenta.ToString.Substring(8, 4) & " AND Nivel4 >= 0 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), cuenta.ToString.Substring(8, 4),
                                  Cta, cuenta.Substring(0, 12) & Cta, Me.TablaCF.Item(RFCComplemento.Index, posicion).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaCF.Item(LetraSatComplemento.Index, posicion).Value, Me.TablaCF.Item(RFCComplemento.Index, posicion).Value)
                Cta = cuenta.Substring(0, 12) & Cta
            End If
        End If
        Return Cta
    End Function
    Private Function LetraFactura(ByVal uuid As String)
        Dim letra As String
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT clave,Total_Real FROM dbo.Xml_Sat WHERE UUID = '" & uuid & "' and Tipo ='Factura' AND Id_Empresa = " & Me.lstCliente.SelectItem & "")
        If ds.Tables(0).Rows.Count > 0 Then
            letra = ds.Tables(0).Rows(0)("clave")
        Else
            letra = ""
        End If
        Return Trim(letra)
    End Function
    Private Sub Crear_cuenta(ByVal nivel1 As String, ByVal nivel2 As String, ByVal nivel3 As String,
                             ByVal nivel4 As String, ByVal cuenta As String, ByVal descripcion As String, ByVal cliente As Integer, ByVal letra As String, ByVal RFC As String)
        Dim ds As DataSet = Eventos.Obtener_DS("Select Naturaleza,Clasificacion,Balanza,Cta_ceros,Cta_Cargo_Cero,Cta_Abono_Cero from Catalogo_de_Cuentas where nivel1 ='" & cuenta.ToString.Substring(0, 4) & "' and Id_Empresa = " & cliente & "  ")

        If ds.Tables(0).Rows.Count > 0 Then
            Dim sql As String = ""
            sql = "INSERT INTO dbo.Catalogo_de_Cuentas "
            sql &= "("
            sql &= "Nivel1, "
            sql &= "Nivel2,"
            sql &= "Nivel3,"
            sql &= "Nivel4,"
            sql &= "Cuenta,"
            sql &= "Descripcion,"
            sql &= "Naturaleza,"
            sql &= "Clasificacion,"
            sql &= "Codigo_Agrupador,"
            If RFC <> "" Then
                sql &= "RFC,"
            Else
                sql &= "RFC,"
            End If
            sql &= "Id_Empresa,clave, "
            sql &= "Balanza,"
            sql &= "Cta_ceros,  "
            sql &= "Cta_Cargo_Cero,"
            sql &= "Cta_Abono_Cero  "

            sql &= "	)  "
            sql &= "VALUES  "
            sql &= "	(  "
            sql &= "	'" & nivel1 & "'," '@nivel1
            sql &= "	'" & nivel2 & "'," '@nivel2
            sql &= "	'" & nivel3 & "'," '@nivel3
            sql &= "	'" & nivel4 & "'," '@nivel4
            sql &= "	'" & cuenta & "'," '@cuenta
            sql &= "	'" & descripcion & "'," '@descripcion
            sql &= "	'" & Trim(ds.Tables(0).Rows(0)("Naturaleza")) & "'," '@naturaleza
            sql &= "	'" & Trim(ds.Tables(0).Rows(0)("Clasificacion")) & "'," '@clasificacion
            Dim su As String = ""
            If (nivel4 <> "0000" Or nivel3 <> "0000") And nivel2 <> "0000" Then
                su = nivel3.Substring(2, 2)
            End If
            sql &= "	'" & nivel1.ToString.Substring(0, 3) & su & "'," '@codigo_agrupador
            If RFC = "" Then
                sql &= "	NULL," '@RFC
            Else
                sql &= "	'" & RFC & "'," '@RFC
            End If
            sql &= "	" & cliente & " , '" & Trim(letra) & "'," '@Id_Empresa    

            sql &= "	" & Eventos.Bool2(Trim(ds.Tables(0).Rows(0)("Balanza"))) & "," '@Balanza
            sql &= "	" & Eventos.Bool2(Trim(ds.Tables(0).Rows(0)("Cta_ceros"))) & "," '@Cta_ceros
            sql &= "	" & Eventos.Bool2(Trim(ds.Tables(0).Rows(0)("Cta_Cargo_Cero"))) & "," '@Balanza
            sql &= "	" & Eventos.Bool2(Trim(ds.Tables(0).Rows(0)("Cta_Abono_Cero"))) & "" '@Cta_ceros
            sql &= "  )"
            ' Ingresar codigo para importar catalogos
            If Eventos.Comando_sql(sql) > 0 Then
                Eventos.Insertar_usuariol("Crear_CtaCarga", sql)
            End If
        Else

        End If
    End Sub
    Private Function TasaComplemeto(ByVal uuid As String)
        Dim Tipo As String = ""
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Xml_Sat.IVA_16 ,Xml_Sat.IVA_8  FROM dbo.Xml_Sat WHERE UUID = '" & uuid & "' and Id_Empresa = " & Me.lstCliente.SelectItem & "")
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) > 0 And ds.Tables(0).Rows(0)(1) = 0 Then
                Tipo = "16"
            ElseIf ds.Tables(0).Rows(0)(0) = 0 And ds.Tables(0).Rows(0)(1) > 0 Then
                Tipo = "8"
            End If
        Else
            Tipo = ""
        End If
        Return Tipo
    End Function
    Private Function CuentasPue(ByVal Clave As String) As Object()
        Dim Cuentas(6)
        Dim sql As String = " SELECT GravadoPUE, NivelG,  ExentoPUE, NivelE, IVAPUE, NivelI,Negativo "
        sql &= " FROM ClaveEgresos WHERE ClaveEgresos.Id_Empresa =" & Me.lstCliente.SelectItem & " "
        sql &= " And  Clave ='" & Clave & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cuentas(0) = ds.Tables(0).Rows(0)(0)
            Cuentas(1) = ds.Tables(0).Rows(0)(1)
            Cuentas(2) = ds.Tables(0).Rows(0)(2)
            Cuentas(3) = ds.Tables(0).Rows(0)(3)
            Cuentas(4) = ds.Tables(0).Rows(0)(4)
            Cuentas(5) = ds.Tables(0).Rows(0)(5)
            Cuentas(6) = ds.Tables(0).Rows(0)(6)
        End If
        Return Cuentas
    End Function
    Private Function CuentasAnticiposPue(ByVal Clave As String) As Object()
        Dim Cuentas(6)
        Dim sql As String = " SELECT AnticipoGPUE, NivelA,  AnticipoEPUE, NivelAE, IVAAPUE, NivelIA,Negativo "
        sql &= " FROM ClaveEgresos WHERE ClaveEgresos.Id_Empresa =" & Me.lstCliente.SelectItem & " "
        sql &= " And  Clave ='" & Clave & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cuentas(0) = ds.Tables(0).Rows(0)(0)
            Cuentas(1) = ds.Tables(0).Rows(0)(1)
            Cuentas(2) = ds.Tables(0).Rows(0)(2)
            Cuentas(3) = ds.Tables(0).Rows(0)(3)
            Cuentas(4) = ds.Tables(0).Rows(0)(4)
            Cuentas(5) = ds.Tables(0).Rows(0)(5)
            Cuentas(6) = ds.Tables(0).Rows(0)(6)
        End If
        Return Cuentas
    End Function
    Private Function CuentasPPD(ByVal Clave As String)
        Dim Cuentas(11)
        Dim sql As String = " SELECT 	GravadoPPD,	NivelGP, 	ExentoPPD,	NivelEP, 	IVAPPD,	NivelIP, Debe,NivelD	,Negativo ,DebeE,NivelDE "
        sql &= " FROM ClaveEgresos WHERE ClaveEgresos.Id_Empresa =" & Me.lstCliente.SelectItem & " "
        sql &= " And  Clave ='" & Clave & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cuentas(0) = ds.Tables(0).Rows(0)(0)
            Cuentas(1) = ds.Tables(0).Rows(0)(1)
            Cuentas(2) = ds.Tables(0).Rows(0)(2)
            Cuentas(3) = ds.Tables(0).Rows(0)(3)
            Cuentas(4) = ds.Tables(0).Rows(0)(4)
            Cuentas(5) = ds.Tables(0).Rows(0)(5)
            Cuentas(6) = ds.Tables(0).Rows(0)(6)
            Cuentas(7) = ds.Tables(0).Rows(0)(7)
            Cuentas(8) = ds.Tables(0).Rows(0)(8)
            Cuentas(9) = ds.Tables(0).Rows(0)(9)
            Cuentas(10) = ds.Tables(0).Rows(0)(10)
        End If
        Return Cuentas
    End Function
    Private Function CuentasCO(ByVal Clave As String)
        Dim Cuentas(7)
        ' "9011001000010004"
        ' 9011002000010004
        Dim sql As String = " SELECT 	COGC,	NivelCOGC, 	COGA,	NivelCOGA, 	COEC,	NivelCOEC,	COEA,	NivelCOEA "
        sql &= " FROM ClaveEgresos WHERE ClaveEgresos.Id_Empresa =" & Me.lstCliente.SelectItem & " "
        sql &= " And  Clave ='" & Clave & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cuentas(0) = ds.Tables(0).Rows(0)(0)
            Cuentas(1) = ds.Tables(0).Rows(0)(1)
            Cuentas(2) = ds.Tables(0).Rows(0)(2)
            Cuentas(3) = ds.Tables(0).Rows(0)(3)
            Cuentas(4) = ds.Tables(0).Rows(0)(4)
            Cuentas(5) = ds.Tables(0).Rows(0)(5)
            Cuentas(6) = ds.Tables(0).Rows(0)(6)
            Cuentas(7) = ds.Tables(0).Rows(0)(7)

        End If
        Return Cuentas
    End Function
    Private Function CuentasRet(ByVal Clave As String)
        Dim Cuentas(9)
        Dim sql As String = " SELECT 	RISRPUE, NivelRISR,RIVAPUE,NivelRIVA,RISRPPD, NivelRISRP, RIVAPPD, NivelRIVAP ,IVAPA,NivelIVAPA"
        sql &= " FROM ClaveEgresos WHERE ClaveEgresos.Id_Empresa =" & Me.lstCliente.SelectItem & " "
        sql &= " And  Clave ='" & Clave & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cuentas(0) = ds.Tables(0).Rows(0)(0)
            Cuentas(1) = ds.Tables(0).Rows(0)(1)
            Cuentas(2) = ds.Tables(0).Rows(0)(2)
            Cuentas(3) = ds.Tables(0).Rows(0)(3)
            Cuentas(4) = ds.Tables(0).Rows(0)(4)
            Cuentas(5) = ds.Tables(0).Rows(0)(5)
            Cuentas(6) = ds.Tables(0).Rows(0)(6)
            Cuentas(7) = ds.Tables(0).Rows(0)(7)
            Cuentas(8) = ds.Tables(0).Rows(0)(8)
            Cuentas(9) = ds.Tables(0).Rows(0)(9)
        End If
        Return Cuentas
    End Function
    Private Function RegresaCuentaCompleNuevo(ByVal Cuenta As String, ByVal RFC As String, ByVal Posicion As Integer, Optional ByVal Tipo As Integer = 0)
        Dim Cta As String = ""
        Dim sql As String = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & Cuenta.Substring(0, 4) & "' AND Nivel2= '" & Cuenta.Substring(4, 4) & "' AND Nivel3 = '" & Cuenta.Substring(8, 4) & "' AND Nivel4 > 0 AND RFC = '" & RFC & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cta = ds.Tables(0).Rows(0)(0)
        Else
            'No existe la cuenta y la inserta

            If Tipo = 3 Then
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Cuenta.ToString.Substring(4, 4) & " AND Nivel3 >= 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cuenta.ToString.Substring(4, 4), Cta.ToString.Substring(0, 4),
                               "0000", Cuenta.Substring(0, 8) & Cta & "0000", Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaCF.Item(LetraSatComplemento.Index, Posicion).Value, Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 8) & Cta & "0000"
            ElseIf Tipo = 4 Then
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & Cuenta.ToString.Substring(8, 4) & " AND Nivel4 >= 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cuenta.ToString.Substring(4, 4), Cuenta.ToString.Substring(8, 4),
                                  Cta, Cuenta.Substring(0, 12) & Cta, Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, Posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaCF.Item(LetraSatComplemento.Index, Posicion).Value, Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 12) & Cta
            ElseIf Tipo = 2 Then

                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "   AND Nivel2 >= 0 AND Nivel3 = 0000 AND Nivel4  = 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cta, "0000",
                                 "0000", Cuenta.Substring(0, 4) & Cta & "00000000", Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaCF.Item(LetraSatComplemento.Index, Posicion).Value, Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 4) & Cta & "00000000"
            End If

        End If
        Return Cta
    End Function
    Private Function RegresaNivel(ByVal Cuenta As String, ByVal RFC As String)
        Dim Nivel As String = ""
        Dim Sql As String = ""
        Dim dsconta As DataSet

        Select Case Len(Cuenta)
            Case 4
                Sql = "SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,Nivel2,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and Nivel1 ='" & Cuenta.Substring(0, 4) & "' and Nivel2 > 0 AND Nivel3 =0  ORDER BY cta "
            Case 8
                Sql = "SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,Nivel3,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and Nivel1 ='" & Cuenta.Substring(0, 4) & "' and nivel2 ='" & Cuenta.Substring(4, 4) & "' AND Nivel3 > 0  AND Nivel4 =0 ORDER BY cta "

        End Select
        dsconta = Eventos.Obtener_DS(Sql)

        Dim act(,) As String
        ReDim act(2, dsconta.Tables(0).Rows.Count + 1)
        For s As Integer = 0 To dsconta.Tables(0).Rows.Count - 1
            Dim C() As String = Split(dsconta.Tables(0).Rows(s)(0), "-")
            act(0, s) = dsconta.Tables(0).Rows(s)(0)
            Debug.Print(dsconta.Tables(0).Rows(s)(0))
            act(1, s) = "0"
        Next
        With My.Forms.DialogUnaSeleccion
            .limpiar()
            .Titulo = Eventos.titulo_app
            .Texto = "Selecciona la Cuenta Madre para el " & RFC & ":"
            .MinSeleccion = 1
            .MaxSeleccion = 1
            .elementos = act
            .ShowDialog()
            act = .elementos
            If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                Nivel = ""
            End If
        End With
        Dim Letra As String = ""
        For s As Integer = 0 To act.GetLength(1)
            If act(1, s) = "1" Then
                Letra = act(0, s)
                Exit For
            End If
        Next
        Dim cadena As String = Trim(Letra)
        Dim posi As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
        Dim cuantos As Integer = Len(cadena) - Len(cadena.Substring(0, posi))
        Nivel = cadena.Substring(posi, cuantos)

        Return Nivel
    End Function
    Private Function RegresaCuentaComple(ByVal Cuenta As String, ByVal RFC As String, ByVal Posicion As Integer, ByVal Nivel As String, Optional ByVal Tipo As Integer = 0)
        Dim sql As String = ""
        Dim Cta As String = ""
        If Tipo = 2 Then
            sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & Cuenta.Substring(0, 4) & "' AND Nivel2 > 0 AND Nivel3 =  0000 AND Nivel4 =0000 AND RFC = '" & RFC & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        ElseIf Tipo = 3 Then
            sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & Cuenta.Substring(0, 4) & "' AND Nivel2= '" & Nivel & "' AND Nivel3 > 0 AND Nivel4 = 0000 AND RFC = '" & RFC & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        ElseIf Tipo = 4 Then
            sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & Cuenta.Substring(0, 4) & "' AND Nivel2= '" & Cuenta.Substring(4, 4) & "' AND Nivel3 >= '" & Nivel & "' AND Nivel4 > 0 AND RFC = '" & RFC & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "

        End If
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cta = ds.Tables(0).Rows(0)(0)
        Else
            'No existe la cuenta y la inserta

            If Tipo = 3 Then

                If Nivel = "0000" Then
                    Nivel = RegresaNivel(Cuenta.ToString.Substring(0, 4), Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, Posicion).Value)
                End If
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Nivel & " AND Nivel3 >= 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Nivel, Cta.ToString.Substring(0, 4),
                               "0000", Cuenta.Substring(0, 8) & Cta & "0000", Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaCF.Item(LetraSatComplemento.Index, Posicion).Value, Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 4) & Nivel & Cta & "0000"
            ElseIf Tipo = 4 Then
                If Nivel = "0000" Then
                    Nivel = RegresaNivel(Cuenta.ToString.Substring(0, 8), Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, Posicion).Value)
                End If
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & Nivel & " AND Nivel4 >= 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cuenta.ToString.Substring(4, 4), Nivel,
                                  Cta, Cuenta.Substring(0, 8) & Nivel & Cta, Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, Posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaCF.Item(LetraSatComplemento.Index, Posicion).Value, Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 8) & Nivel & Cta
            ElseIf Tipo = 2 Then

                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "   AND Nivel2 >= 0 AND Nivel3 = 0000 AND Nivel4  = 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Nivel, Cta, "0000",
                                 "0000", Cuenta.Substring(0, 4) & Cta & "00000000", Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaCF.Item(LetraSatComplemento.Index, Posicion).Value, Me.TablaCF.Item(RFCComplemento.Index, Posicion).Value)
                Cta = Nivel & Cta & "00000000"
            End If




        End If
        Return Cta
    End Function


    Private Sub CrearDetalleComplementoNuevo(ByVal Fila As Integer, ByVal Poliza As String)
        Dim Item As Integer = 1
        Dim Cuenta(6)
        Dim CuentaPPD(11)
        Dim CuentaCO(7)
        Dim CuentaRetenciones(8)
        Dim Cadena()

        Dim Nivel() As String = Split(Me.TablaCF.Item(NomCtaComplemento.Index, Fila).Value.ToString.Trim(), "-")
        If Nivel(1).Substring(12, 4) = "0000" And Nivel(1).Substring(8, 4) > "0000" Then ' Segundo nivel
            Nivel(1) = Nivel(1).Substring(4, 4)
        ElseIf Nivel(1).Substring(12, 4) > "0000" And Nivel(1).Substring(8, 4) > "0000" Then 'Tercer Nivel
            Nivel(1) = Nivel(1).Substring(8, 4)
        ElseIf Nivel(1).Substring(4, 4) > "0000" And Nivel(1).Substring(8, 4) = "0000" And Nivel(1).Substring(12, 4) = "0000" Then 'Primer Nivel
            Nivel(1) = Nivel(1).Substring(0, 4)
        End If

        Cuenta = CuentasPue(LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value))
        CuentaPPD = CuentasPPD(LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value))
        CuentaRetenciones = CuentasRet(LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value))
        Dim NivelEp, NivelGp, NivelE, NivelG, NivelED, NivelGAD As String

        Try
            Select Case CuentaPPD(10)
                Case 2
                    NivelED = CuentaPPD(9).ToString.Substring(0, 4)
                Case 3
                    NivelED = CuentaPPD(9).ToString.Substring(4, 4)
                Case 4
                    NivelED = CuentaPPD(9).ToString.Substring(8, 4)
            End Select
        Catch ex As Exception
            NivelED = ""
        End Try

        Try


            Select Case CuentaPPD(7)
                Case 2
                    NivelGAD = CuentaPPD(6).ToString.Substring(0, 4)
                Case 3
                    NivelGAD = CuentaPPD(6).ToString.Substring(4, 4)
                Case 4
                    NivelGAD = CuentaPPD(6).ToString.Substring(8, 4)
            End Select
        Catch ex As Exception
            NivelGAD = ""
        End Try


        Try
            Select Case Cuenta(3)
                Case 2
                    NivelE = Cuenta(2).ToString.Substring(0, 4)
                Case 3
                    NivelE = Cuenta(2).ToString.Substring(4, 4)
                Case 4
                    NivelE = Cuenta(2).ToString.Substring(8, 4)
            End Select
        Catch ex As Exception
            NivelE = ""
        End Try
        Try
            Select Case Cuenta(1)
                Case 2
                    NivelG = Cuenta(0).ToString.Substring(0, 4)
                Case 3
                    NivelG = Cuenta(0).ToString.Substring(4, 4)
                Case 4
                    NivelG = Cuenta(0).ToString.Substring(8, 4)
            End Select
        Catch ex As Exception
            NivelG = ""
        End Try

        Try
            Select Case CuentaPPD(3)
                Case 2
                    NivelEp = CuentaPPD(2).ToString.Substring(0, 4)
                Case 3
                    NivelEp = CuentaPPD(2).ToString.Substring(4, 4)
                Case 4
                    NivelEp = CuentaPPD(2).ToString.Substring(8, 4)
            End Select
        Catch ex As Exception
            NivelEp = ""
        End Try
        Try
            Select Case CuentaPPD(1)
                Case 2
                    NivelGp = CuentaPPD(0).ToString.Substring(0, 4)
                Case 3
                    NivelGp = CuentaPPD(0).ToString.Substring(4, 4)
                Case 4
                    NivelGp = CuentaPPD(0).ToString.Substring(8, 4)
            End Select
        Catch ex As Exception
            NivelGp = ""
        End Try

        Dim Tas As String = TasaComplemeto(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value)
        Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP, RIVAC, RISRC As Decimal
        Dim pago As Decimal = Me.TablaCF.Item(ImpEfComplemento.Index, Fila).Value + Me.TablaCF.Item(ImpTComplemento.Index, Fila).Value + Me.TablaCF.Item(ImpChComplemento.Index, Fila).Value
        PorcenPro = pago / ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value, "Total_Real")

        ImpGpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value, "Imp_Grabado") * PorcenPro
        ImpEpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value, "Imp_Exento") * PorcenPro

        ImpIvaP = (ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value, "IVA_real")) * PorcenPro
        RIVAC = (ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value, "Retenido_Iva")) * PorcenPro
        RISRC = (ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value, "Retenido_ISR")) * PorcenPro
        CuentaPPD = CuentasPPD(LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value))
        If ImpEpro > 0 Then
            Crea_detalle_poliza(Poliza, Item, ImpEpro, 0, RegresaCuentaComple(CuentaPPD(9), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, NivelED, CuentaPPD(10)), "")
            Item += 1
        End If
        If ImpGpro + ImpIvaP > 0 Then
            Crea_detalle_poliza(Poliza, Item, (ImpGpro + ImpIvaP) - (RIVAC + RISRC), 0, RegresaCuentaComple(CuentaPPD(6), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, NivelGAD, CuentaPPD(7)), "")
            Item += 1
        End If

        If RIVAC > 0 Then
            Crea_detalle_poliza(Poliza, Item, RIVAC, 0, CuentaRetenciones(6), "")
            Item += 1
        End If
        If RISRC > 0 Then
            Crea_detalle_poliza(Poliza, Item, RISRC, 0, CuentaRetenciones(4), "")
            Item += 1
        End If
        If Me.TablaCF.Item(ImpEfComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, Fila).Value > 0 Then 'TODOS

            Cadena = Split(Me.TablaCF.Item(CtaEfComplemento.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, Fila).Value, Me.TablaCF.Item(CtaBancosComplemento.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, Fila).Value, Me.TablaCF.Item(CtaChequeC.Index, Fila).Value.ToString().Trim(), "")
            Item += 1

        ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, Fila).Value = 0 Then 'Efectivo y Transferencia
            Cadena = Split(Me.TablaCF.Item(CtaEfComplemento.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, Fila).Value, Me.TablaCF.Item(CtaBancosComplemento.Index, Fila).Value.ToString().Trim(), "")
            Item += 1


        ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, Fila).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, Fila).Value > 0 Then 'Cheque y Transferencia

            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, Fila).Value, Me.TablaCF.Item(CtaBancosComplemento.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, Fila).Value, Me.TablaCF.Item(CtaChequeC.Index, Fila).Value.ToString().Trim(), "")
            Item += 1

        ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, Fila).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, Fila).Value > 0 Then 'Cheque y Efecivo
            Cadena = Split(Me.TablaCF.Item(CtaEfComplemento.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, Fila).Value, Me.TablaCF.Item(CtaChequeC.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, Fila).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, Fila).Value = 0 Then ' Efecivo
            Cadena = Split(Me.TablaCF.Item(CtaEfComplemento.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, Fila).Value, Cadena(1), "")
            Item += 1

        ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, Fila).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, Fila).Value = 0 Then ' Transferencia
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, Fila).Value, Me.TablaCF.Item(CtaBancosComplemento.Index, Fila).Value.ToString().Trim(), "")
            Item += 1

        ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, Fila).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, Fila).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, Fila).Value > 0 Then 'Cheque 
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, Fila).Value, Me.TablaCF.Item(CtaChequeC.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        End If

        If Me.TablaCF.Item(AntiComplemento.Index, Fila).Value > 0 Then ' Se verifica si cuenta con anticipos +
            If Tas <> "" Then
                Cuenta = CuentasAnticiposPue(LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value))
                Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(AntiComplemento.Index, Fila).Value, 0, RegresaCuentaCompleNuevo(Cuenta(0), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(1)), "")
                Item += 1
            Else
                Cuenta = CuentasAnticiposPue(LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value))
                Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(AntiComplemento.Index, Fila).Value, 0, RegresaCuentaCompleNuevo(Cuenta(2), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(3)), "")
                Item += 1
            End If

        ElseIf Me.TablaCF.Item(AntiComplemento.Index, Fila).Value < 0 Then ' Se verifica si cuenta  anticipos -
            If Tas <> "" Then
                Cuenta = CuentasAnticiposPue(LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value))
                Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(AntiComplemento.Index, Fila).Value * -1, 0, RegresaCuentaCompleNuevo(Cuenta(0), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(1)), "")
                Item += 1
            Else
                Cuenta = CuentasAnticiposPue(LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value))
                Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(AntiComplemento.Index, Fila).Value * -1, 0, RegresaCuentaCompleNuevo(Cuenta(2), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(3)), "")
                Item += 1
            End If
        End If
        'Ajustes
        If Me.TablaCF.Item(AjusComplemento.Index, Fila).Value > 0 Then ' Se verifica si cuenta con ajuste +
            Cadena = Split(Me.TablaCF.Item(CtaAjusComplemento.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(AjusComplemento.Index, Fila).Value, Cadena(0), "")
            Item += 1
        ElseIf Me.TablaCF.Item(AjusComplemento.Index, Fila).Value < 0 Then ' Se verifica si cuenta con ajuste -
            Cadena = Split(Me.TablaCF.Item(CtaAjusComplemento.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(AjusComplemento.Index, Fila).Value * -1, Cadena(0), "")
            Item += 1
        End If
        If Me.TablaCF.Item(PCComplemento.Index, Fila).Value > 0 Then ' Se perdida cambiaria
            Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(PCComplemento.Index, Fila).Value, 0, "7010000100000000", "")
            Item += 1
        End If
        If Me.TablaCF.Item(UCComplemento.Index, Fila).Value > 0 Then ' Se utilidad cambiaria
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(UCComplemento.Index, Fila).Value, "7020000100000000", "")
            Item += 1
        End If


        If ImpEpro > 0 And ImpGpro > 0 Then ' tiene grabado y exento
            Crea_detalle_poliza(Poliza, Item, ImpGpro, 0, RegresaCuentaComple(Cuenta(0), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, NivelG, Cuenta(1)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, ImpEpro, 0, RegresaCuentaComple(Cuenta(2), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, NivelE, Cuenta(3)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, ImpIvaP - RIVAC, 0, Cuenta(4), "")
            Item += 1

            If RIVAC > 0 Then
                Crea_detalle_poliza(Poliza, Item, RIVAC, 0, CuentaRetenciones(8), "")
                Item += 1
            End If
            Crea_detalle_poliza(Poliza, Item, 0, ImpGpro, RegresaCuentaComple(CuentaPPD(0), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, NivelGp, CuentaPPD(1)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, ImpEpro, RegresaCuentaComple(CuentaPPD(2), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, NivelEp, CuentaPPD(3)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, ImpIvaP, CuentaPPD(4), "")
            Item += 1

            If RIVAC > 0 Then
                Crea_detalle_poliza(Poliza, Item, 0, RIVAC, CuentaRetenciones(2), "")
                Item += 1
            End If

        ElseIf ImpEpro > 0 And ImpGpro <= 0 Then 'Tiene Exento
            Crea_detalle_poliza(Poliza, Item, ImpEpro, 0, RegresaCuentaComple(Cuenta(2), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, NivelE, Cuenta(3)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, ImpEpro, RegresaCuentaComple(CuentaPPD(2), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, NivelEp, CuentaPPD(3)), "")
            Item += 1
        ElseIf ImpEpro <= 0 And ImpGpro > 0 Then 'Tiene Grabado
            Crea_detalle_poliza(Poliza, Item, ImpGpro, 0, RegresaCuentaComple(Cuenta(0), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, NivelG, Cuenta(1)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, ImpIvaP - RIVAC, 0, Cuenta(4), "")
            Item += 1
            If RIVAC > 0 Then
                Crea_detalle_poliza(Poliza, Item, RIVAC, 0, CuentaRetenciones(8), "")
                Item += 1
            End If
            Crea_detalle_poliza(Poliza, Item, 0, ImpGpro, RegresaCuentaComple(CuentaPPD(0), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, NivelGp, CuentaPPD(1)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, ImpIvaP, CuentaPPD(4), "")
            Item += 1
            If RIVAC > 0 Then
                Crea_detalle_poliza(Poliza, Item, 0, RIVAC, CuentaRetenciones(2), "")
                Item += 1
            End If
        End If
        If RISRC > 0 Then
            Crea_detalle_poliza(Poliza, Item, 0, RISRC, CuentaRetenciones(0), "")
            Item += 1
        End If
        If UCase(Me.TablaCF.Item(CtaOrdenC.Index, Fila).Value) = "SI" Then
            CuentaCO = CuentasCO(LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, Fila).Value))
            If Len(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value) = 12 Then ' Moral
                If Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value > 0 Then
                    Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value, 0, CuentaCO(2), "")
                    Item += 1
                    Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value, 0, CuentaCO(6), "")
                    Item += 1
                ElseIf Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value <= 0 Then 'Tiene Exento
                    Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value, 0, CuentaCO(6), "")
                    Item += 1
                ElseIf Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value <= 0 And Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value > 0 Then 'Tiene Grabado
                    Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value, 0, CuentaCO(2), "")
                    Item += 1
                End If
            Else
                If Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value > 0 Then
                    Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value, 0, CuentaCO(2), "")
                    Item += 1
                    Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value, 0, CuentaCO(6), "")
                    Item += 1
                ElseIf Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value <= 0 Then 'Tiene Exento
                    Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value, 0, CuentaCO(6), "")
                    Item += 1
                ElseIf Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value <= 0 And Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value > 0 Then 'Tiene Grabado
                    Crea_detalle_poliza(Poliza, Item, Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value, 0, CuentaCO(2), "")
                    Item += 1
                End If

            End If

            If Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value > 0 Then
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value, RegresaCuentaCompleNuevo(CuentaCO(2), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(3)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value, RegresaCuentaCompleNuevo(CuentaCO(6), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(7)), "")
                Item += 1
            ElseIf Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value <= 0 Then 'Tiene Exento
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value, RegresaCuentaCompleNuevo(CuentaCO(6), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(7)), "")
                Item += 1
            ElseIf Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value <= 0 And Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value > 0 Then 'Tiene Grabado
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value, RegresaCuentaCompleNuevo(CuentaCO(2), Trim(Me.TablaCF.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(3)), "")
                Item += 1
            End If



        End If


    End Sub




    Private Sub Crear_detalleComple(ByVal p As Integer, ByVal pol As String)
        Dim Item As Integer = 1
        Dim cadena As String = Trim(Me.TablaCF.Item(NomCtaComplemento.Index, p).Value)
        Dim posi As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
        Dim cuantos As Integer = Len(cadena) - Len(cadena.Substring(0, posi))
        Dim Cuenta_Cargo As String = cadena.Substring(posi, cuantos)
        Dim Cuenta2 As String = ""

        Dim Tas As String = TasaComplemeto(Me.TablaCF.Item(UUIDComplemento.Index, p).Value)


        If LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "C" Or LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "CPP" Then ' Compras poner registro hasta arriba
            Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP As Decimal
            PorcenPro = Me.TablaCF.Item(TotComplemento.Index, p).Value / ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Total_Real")
            ImpGpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Grabado") * PorcenPro
            ImpEpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Exento") * PorcenPro
            ImpIvaP = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "IVA_real") * PorcenPro


            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                Cuenta2 = RegresaCunetaComple("201000010001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0) ' cambiar eventos
                If ImpEpro > 0 Then
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                    Item = Item + 1
                End If

            Else
                Cuenta2 = RegresaCunetaComple("201000010002", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                If ImpEpro > 0 Then
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                    Item = Item + 1
                End If
            End If
            If Tas = "8" Then
                Cuenta2 = RegresaCunetaComple("201000010004", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)

                Crea_detalle_poliza(pol, Item, ImpGpro + ImpIvaP, 0, Cuenta2, "")
                Item = Item + 1
            Else
                Cuenta2 = RegresaCunetaComple("201000010003", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)

                Crea_detalle_poliza(pol, Item, ImpGpro + ImpIvaP, 0, Cuenta2, "")
                Item = Item + 1
            End If


            If Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'TODOS

                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")

                Item = Item + 1

                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")

                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)


            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then 'Efectivo y Transferencia
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Transferencia

                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Efecivo
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)
            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then ' Efecivo
                'Cuenta del Abono
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)

                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then ' Transferencia
                'Cuenta del Abono transferencia
                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, IIf(IsDBNull(Me.TablaCF.Item(NoChComplemento.Index, p).Value) = True, "", Me.TablaCF.Item(NoChComplemento.Index, p).Value))

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque 
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)

            End If

            If Me.TablaCF.Item(AntiComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con anticipos +
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAntiComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(AntiComplemento.Index, p).Value * -1, 0, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(AntiComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta  anticipos -
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAntiComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(AntiComplemento.Index, p).Value, Cuenta_Cargo, "")

            End If
            'Inserta Ajustes
            If Me.TablaCF.Item(AjusComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con ajuste +
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAjusComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                'cargo de Ajuste
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(AjusComplemento.Index, p).Value * -1, 0, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(AjusComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta con ajuste -
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAjusComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                'Abono de Ajuste
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(AjusComplemento.Index, p).Value, Cuenta_Cargo, "")

            End If
            If Me.TablaCF.Item(PCComplemento.Index, p).Value > 0 Then ' Se perdida cambiaria
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(PCComplemento.Index, p).Value, 0, "7010000100000000", "")
            End If
            If Me.TablaCF.Item(UCComplemento.Index, p).Value > 0 Then ' Se utilidad cambiaria
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(UCComplemento.Index, p).Value, "7020000100000000", "")
            End If
            '3° Gasto Pagado






            If ImpEpro > 0 And ImpGpro > 0 Then ' tiene grabado y exento
                If Tas = "8" Then
                    Item = Item + 1

                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Cuenta2 = RegresaCunetaComple("502000010001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0) ' cambiar eventos
                    Else
                        Cuenta2 = RegresaCunetaComple("502000010002", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    End If

                    Cuenta_Cargo = RegresaCunetaComple("502000010004", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")

                    Item = Item + 1
                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Cuenta2 = RegresaCunetaComple("502500010001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0) ' cambiar eventos
                    Else
                        Cuenta2 = RegresaCunetaComple("502500010002", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    End If

                    Cuenta_Cargo = RegresaCunetaComple("502500010004", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100110000", "")
                    Item = Item + 1

                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100110000", "")
                    Item = Item + 1
                Else
                    Item = Item + 1

                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Cuenta2 = RegresaCunetaComple("502000010001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0) ' cambiar eventos
                    Else
                        Cuenta2 = RegresaCunetaComple("502000010002", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    End If

                    Cuenta_Cargo = RegresaCunetaComple("502000010003", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")

                    Item = Item + 1
                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Cuenta2 = RegresaCunetaComple("502500010001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0) ' cambiar eventos
                    Else
                        Cuenta2 = RegresaCunetaComple("502500010002", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    End If

                    Cuenta_Cargo = RegresaCunetaComple("502500010003", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100010000", "")
                    Item = Item + 1

                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100010000", "")
                    Item = Item + 1
                End If


            ElseIf ImpEpro > 0 And ImpGpro <= 0 Then 'Tiene Exento
                Item = Item + 1
                If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                    Cuenta2 = RegresaCunetaComple("502000010001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1
                Else
                    Cuenta2 = RegresaCunetaComple("502000010002", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1
                End If

                If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                    Cuenta2 = RegresaCunetaComple("502500010001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1
                Else
                    Cuenta2 = RegresaCunetaComple("502500010002", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)

                    Item = Item + 1
                End If

            ElseIf ImpEpro <= 0 And ImpGpro > 0 Then 'Tiene Grabado

                If Tas = "8" Then
                    Item = Item + 1
                    Cuenta2 = RegresaCunetaComple("502000010004", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta2, "")
                    Item = Item + 1

                    Cuenta2 = RegresaCunetaComple("502500010004", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta2, "")
                    Item = Item + 1
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100110000", "")
                    Item = Item + 1
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100110000", "")
                    Item = Item + 1
                Else
                    Item = Item + 1
                    Cuenta2 = RegresaCunetaComple("502000010003", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta2, "")
                    Item = Item + 1

                    Cuenta2 = RegresaCunetaComple("502500010003", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta2, "")
                    Item = Item + 1
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100010000", "")
                    Item = Item + 1
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100010000", "")
                    Item = Item + 1
                End If

            End If


        ElseIf LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "GG" Or LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "GGPP" Then 'Gastos Generales
            Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP As Decimal
            PorcenPro = Me.TablaCF.Item(TotComplemento.Index, p).Value / ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Total_Real")
            ImpGpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Grabado") * PorcenPro
            ImpEpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Exento") * PorcenPro
            ImpIvaP = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "IVA_real") * PorcenPro

            '1° A creedores
            Cuenta2 = RegresaCunetaComple("205000020001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(TotComplemento.Index, p).Value, 0, Cuenta2, "")
            Item = Item + 1
            '2° Pagos
            If Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'TODOS

                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")

                Item = Item + 1

                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")

                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)


            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then 'Efectivo y Transferencia
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Transferencia

                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Efecivo
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)
            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then ' Efecivo
                'Cuenta del Abono
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)

                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then ' Transferencia
                'Cuenta del Abono transferencia
                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, IIf(IsDBNull(Me.TablaCF.Item(NoChComplemento.Index, p).Value) = True, "", Me.TablaCF.Item(NoChComplemento.Index, p).Value))

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque 
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)

            End If

            If Me.TablaCF.Item(AntiComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con anticipos +
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAntiComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(AntiComplemento.Index, p).Value * -1, 0, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(AntiComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta  anticipos -
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAntiComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(AntiComplemento.Index, p).Value, Cuenta_Cargo, "")

            End If
            'Inserta Ajustes
            If Me.TablaCF.Item(AjusComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con ajuste +
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAjusComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                'cargo de Ajuste
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(AjusComplemento.Index, p).Value * -1, 0, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(AjusComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta con ajuste -
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAjusComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                'Abono de Ajuste
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(AjusComplemento.Index, p).Value, Cuenta_Cargo, "")

            End If
            If Me.TablaCF.Item(PCComplemento.Index, p).Value > 0 Then ' Se perdida cambiaria
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(PCComplemento.Index, p).Value, 0, "7010000100000000", "")
            End If
            If Me.TablaCF.Item(UCComplemento.Index, p).Value > 0 Then ' Se utilidad cambiaria
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(UCComplemento.Index, p).Value, "7020000100000000", "")
            End If
            '3° Gasto Pagado
            cadena = Trim(Me.TablaCF.Item(NomCtaComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Item += 1
            If ImpEpro > 0 And ImpGpro > 0 Then ' tiene grabado y exento
                If Tas = "8" Then
                    'cargos
                    Cuenta2 = RegresaCunetaComple("60100010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                    Item = Item + 1

                    Cuenta2 = RegresaCunetaComple("60150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta_Cargo, "") 'Revisar Cuenta que Graba provision
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                    Item = Item + 1

                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100120000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100120000", "")
                    Item = Item + 1
                Else
                    'cargos
                    Cuenta2 = RegresaCunetaComple("60100010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                    Item = Item + 1

                    Cuenta2 = RegresaCunetaComple("60150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta_Cargo, "") 'Revisar Cuenta que Graba provision
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                    Item = Item + 1

                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100020000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100020000", "")
                    Item = Item + 1
                End If


            ElseIf ImpEpro > 0 And ImpGpro <= 0 Then 'Tiene Exento

                Cuenta2 = RegresaCunetaComple("60100010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)

                Item = Item + 1

                Cuenta2 = RegresaCunetaComple("60150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)

                Item = Item + 1
            ElseIf ImpEpro <= 0 And ImpGpro > 0 Then 'Tiene Grabado
                If Tas = "8" Then
                    Cuenta2 = RegresaCunetaComple("60100050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1


                    Cuenta2 = RegresaCunetaComple("60150050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100120000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100120000", "")
                    Item = Item + 1
                Else
                    Cuenta2 = RegresaCunetaComple("60100020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1


                    Cuenta2 = RegresaCunetaComple("60150020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100020000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100020000", "")
                    Item = Item + 1
                End If

            End If




        ElseIf LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "A" Or LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "APP" Then 'Gastos Generales
            Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP As Decimal
            PorcenPro = Me.TablaCF.Item(ProvAComplemento.Index, p).Value / ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Total_Real")
            ImpGpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Grabado") * PorcenPro
            ImpEpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Exento") * PorcenPro
            ImpIvaP = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "IVA_real") * PorcenPro


            Cuenta2 = RegresaCunetaComple("205000020002", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ProvAComplemento.Index, p).Value, 0, Cuenta2, "")

            Item = Item + 1
            If ImpEpro <> 0 Or ImpGpro <> 0 Then ' tiene grabado y exento
                If Tas = "8" Then
                    'cargos

                    Crea_detalle_poliza(pol, Item, ImpGpro + ImpEpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100130000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpGpro + ImpEpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1190000100130000", "")
                    Item = Item + 1
                Else
                    'cargos

                    Crea_detalle_poliza(pol, Item, ImpGpro + ImpEpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100030000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpGpro + ImpEpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1190000100030000", "")
                    Item = Item + 1
                End If


            End If


        ElseIf LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "GV" Or LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "GVPP" Then 'Gastos Generales
            Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP As Decimal
            PorcenPro = Me.TablaCF.Item(ProvAComplemento.Index, p).Value / ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Total_Real")
            ImpGpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Grabado") * PorcenPro
            ImpEpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Exento") * PorcenPro
            ImpIvaP = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "IVA_real") * PorcenPro


            Cuenta2 = RegresaCunetaComple("205000020001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(TotComplemento.Index, p).Value, 0, Cuenta2, "")
            Item = Item + 1
            '2° Pagos
            If Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'TODOS

                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")

                Item = Item + 1

                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")

                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)


            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then 'Efectivo y Transferencia
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Transferencia

                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Efecivo
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)
            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then ' Efecivo
                'Cuenta del Abono
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)

                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then ' Transferencia
                'Cuenta del Abono transferencia
                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, IIf(IsDBNull(Me.TablaCF.Item(NoChComplemento.Index, p).Value) = True, "", Me.TablaCF.Item(NoChComplemento.Index, p).Value))

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque 
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)

            End If

            If Me.TablaCF.Item(AntiComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con anticipos +
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAntiComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(AntiComplemento.Index, p).Value * -1, 0, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(AntiComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta  anticipos -
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAntiComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(AntiComplemento.Index, p).Value, Cuenta_Cargo, "")

            End If
            'Inserta Ajustes
            If Me.TablaCF.Item(AjusComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con ajuste +
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAjusComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                'cargo de Ajuste
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(AjusComplemento.Index, p).Value * -1, 0, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(AjusComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta con ajuste -
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAjusComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                'Abono de Ajuste
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(AjusComplemento.Index, p).Value, Cuenta_Cargo, "")

            End If
            If Me.TablaCF.Item(PCComplemento.Index, p).Value > 0 Then ' Se perdida cambiaria
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(PCComplemento.Index, p).Value, 0, "7010000100000000", "")
            End If
            If Me.TablaCF.Item(UCComplemento.Index, p).Value > 0 Then ' Se utilidad cambiaria
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(UCComplemento.Index, p).Value, "7020000100000000", "")
            End If
            '3° Gasto Pagado
            cadena = Trim(Me.TablaCF.Item(NomCtaComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Item += 1

            If ImpEpro > 0 And ImpGpro > 0 Then ' tiene grabado y exento
                If Tas = "8" Then
                    'cargos
                    Cuenta2 = RegresaCunetaComple("60200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                    Item = Item + 1

                    Cuenta2 = RegresaCunetaComple("60250010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta_Cargo, "") 'Revisar Cuenta que Graba provision
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                    Item = Item + 1

                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100120000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100120000", "")
                    Item = Item + 1
                Else
                    'cargos
                    Cuenta2 = RegresaCunetaComple("60200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                    Item = Item + 1

                    Cuenta2 = RegresaCunetaComple("60250010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta_Cargo, "") 'Revisar Cuenta que Graba provision
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                    Item = Item + 1

                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100020000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100020000", "")
                    Item = Item + 1
                End If


            ElseIf ImpEpro > 0 And ImpGpro <= 0 Then 'Tiene Exento

                Cuenta2 = RegresaCunetaComple("60200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)

                Item = Item + 1

                Cuenta2 = RegresaCunetaComple("60250010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)

                Item = Item + 1
            ElseIf ImpEpro <= 0 And ImpGpro > 0 Then 'Tiene Grabado
                If Tas = "8" Then
                    Cuenta2 = RegresaCunetaComple("60200050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1


                    Cuenta2 = RegresaCunetaComple("60250050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100120000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100120000", "")
                    Item = Item + 1
                Else
                    Cuenta2 = RegresaCunetaComple("60200020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1


                    Cuenta2 = RegresaCunetaComple("60250020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100020000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100020000", "")
                    Item = Item + 1
                End If

            End If


        ElseIf LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "GA" Or LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "GAPP" Then 'Gastos Generales
            Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP As Decimal
            PorcenPro = Me.TablaCF.Item(ProvAComplemento.Index, p).Value / ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Total_Real")
            ImpGpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Grabado") * PorcenPro
            ImpEpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Exento") * PorcenPro
            ImpIvaP = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "IVA_real") * PorcenPro


            Cuenta2 = RegresaCunetaComple("205000020001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(TotComplemento.Index, p).Value, 0, Cuenta2, "")
            Item = Item + 1
            '2° Pagos
            If Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'TODOS

                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")

                Item = Item + 1

                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")

                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)


            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then 'Efectivo y Transferencia
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Transferencia

                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Efecivo
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)
            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then ' Efecivo
                'Cuenta del Abono
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)

                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then ' Transferencia
                'Cuenta del Abono transferencia
                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, IIf(IsDBNull(Me.TablaCF.Item(NoChComplemento.Index, p).Value) = True, "", Me.TablaCF.Item(NoChComplemento.Index, p).Value))

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque 
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)

            End If

            If Me.TablaCF.Item(AntiComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con anticipos +
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAntiComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(AntiComplemento.Index, p).Value * -1, 0, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(AntiComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta  anticipos -
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAntiComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(AntiComplemento.Index, p).Value, Cuenta_Cargo, "")

            End If
            'Inserta Ajustes
            If Me.TablaCF.Item(AjusComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con ajuste +
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAjusComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                'cargo de Ajuste
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(AjusComplemento.Index, p).Value * -1, 0, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(AjusComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta con ajuste -
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAjusComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                'Abono de Ajuste
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(AjusComplemento.Index, p).Value, Cuenta_Cargo, "")

            End If
            If Me.TablaCF.Item(PCComplemento.Index, p).Value > 0 Then ' Se perdida cambiaria
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(PCComplemento.Index, p).Value, 0, "7010000100000000", "")
            End If
            If Me.TablaCF.Item(UCComplemento.Index, p).Value > 0 Then ' Se utilidad cambiaria
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(UCComplemento.Index, p).Value, "7020000100000000", "")
            End If
            '3° Gasto Pagado
            cadena = Trim(Me.TablaCF.Item(NomCtaComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)

            Item += 1
            If ImpEpro > 0 And ImpGpro > 0 Then ' tiene grabado y exento
                If Tas = "8" Then
                    'cargos
                    Cuenta2 = RegresaCunetaComple("60300010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                    Item = Item + 1

                    Cuenta2 = RegresaCunetaComple("60350010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta_Cargo, "") 'Revisar Cuenta que Graba provision
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                    Item = Item + 1

                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100120000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100120000", "")
                    Item = Item + 1
                Else
                    'cargos
                    Cuenta2 = RegresaCunetaComple("60300010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                    Item = Item + 1

                    Cuenta2 = RegresaCunetaComple("60350010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta_Cargo, "") 'Revisar Cuenta que Graba provision
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                    Item = Item + 1

                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100020000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100020000", "")
                    Item = Item + 1
                End If


            ElseIf ImpEpro > 0 And ImpGpro <= 0 Then 'Tiene Exento

                Cuenta2 = RegresaCunetaComple("60300010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)

                Item = Item + 1

                Cuenta2 = RegresaCunetaComple("60350010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)

                Item = Item + 1
            ElseIf ImpEpro <= 0 And ImpGpro > 0 Then 'Tiene Grabado
                If Tas = "8" Then
                    Cuenta2 = RegresaCunetaComple("60300050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1


                    Cuenta2 = RegresaCunetaComple("60350050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100120000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100120000", "")
                    Item = Item + 1
                Else
                    Cuenta2 = RegresaCunetaComple("60300020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1


                    Cuenta2 = RegresaCunetaComple("60350020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100020000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100020000", "")
                    Item = Item + 1
                End If

            End If
        ElseIf LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "GF" Or LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "GFPP" Then 'Gastos Generales
            Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP As Decimal
            PorcenPro = Me.TablaCF.Item(ProvAComplemento.Index, p).Value / ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Total_Real")
            ImpGpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Grabado") * PorcenPro
            ImpEpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Exento") * PorcenPro
            ImpIvaP = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "IVA_real") * PorcenPro


            Cuenta2 = RegresaCunetaComple("205000020001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(TotComplemento.Index, p).Value, 0, Cuenta2, "")
            Item = Item + 1
            '2° Pagos
            If Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'TODOS

                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")

                Item = Item + 1

                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")

                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)


            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then 'Efectivo y Transferencia
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Transferencia

                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Efecivo
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)
            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then ' Efecivo
                'Cuenta del Abono
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)

                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then ' Transferencia
                'Cuenta del Abono transferencia
                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, IIf(IsDBNull(Me.TablaCF.Item(NoChComplemento.Index, p).Value) = True, "", Me.TablaCF.Item(NoChComplemento.Index, p).Value))

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque 
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)

            End If

            If Me.TablaCF.Item(AntiComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con anticipos +
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAntiComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(AntiComplemento.Index, p).Value * -1, 0, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(AntiComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta  anticipos -
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAntiComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(AntiComplemento.Index, p).Value, Cuenta_Cargo, "")

            End If
            'Inserta Ajustes
            If Me.TablaCF.Item(AjusComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con ajuste +
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAjusComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                'cargo de Ajuste
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(AjusComplemento.Index, p).Value * -1, 0, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(AjusComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta con ajuste -
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAjusComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                'Abono de Ajuste
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(AjusComplemento.Index, p).Value, Cuenta_Cargo, "")

            End If
            If Me.TablaCF.Item(PCComplemento.Index, p).Value > 0 Then ' Se perdida cambiaria
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(PCComplemento.Index, p).Value, 0, "7010000100000000", "")
            End If
            If Me.TablaCF.Item(UCComplemento.Index, p).Value > 0 Then ' Se utilidad cambiaria
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(UCComplemento.Index, p).Value, "7020000100000000", "")
            End If
            '3° Gasto Pagado
            cadena = Trim(Me.TablaCF.Item(NomCtaComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Item += 1
            If ImpEpro > 0 And ImpGpro > 0 Then ' tiene grabado y exento
                If Tas = "8" Then
                    'cargos
                    Cuenta2 = RegresaCunetaComple("60400010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                    Item = Item + 1

                    Cuenta2 = RegresaCunetaComple("60450010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta_Cargo, "") 'Revisar Cuenta que Graba provision
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                    Item = Item + 1

                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100120000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100120000", "")
                    Item = Item + 1

                Else
                    'cargos
                    Cuenta2 = RegresaCunetaComple("60400010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                    Item = Item + 1

                    Cuenta2 = RegresaCunetaComple("60450010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta_Cargo, "") 'Revisar Cuenta que Graba provision
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                    Item = Item + 1

                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100020000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100020000", "")
                    Item = Item + 1

                End If

            ElseIf ImpEpro > 0 And ImpGpro <= 0 Then 'Tiene Exento

                Cuenta2 = RegresaCunetaComple("60400010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)

                Item = Item + 1

                Cuenta2 = RegresaCunetaComple("60450010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)

                Item = Item + 1
            ElseIf ImpEpro <= 0 And ImpGpro > 0 Then 'Tiene Grabado
                If Tas = "8" Then
                    Cuenta2 = RegresaCunetaComple("60400050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1


                    Cuenta2 = RegresaCunetaComple("60450050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100120000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100120000", "")
                    Item = Item + 1
                Else
                    Cuenta2 = RegresaCunetaComple("60400020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1


                    Cuenta2 = RegresaCunetaComple("60450020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta2, "")
                    Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100020000", "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100020000", "")
                    Item = Item + 1
                End If

            End If
        ElseIf LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "GFI" Or LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "GFIPP" Then 'Gastos Generales
            Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP As Decimal
            PorcenPro = Me.TablaCF.Item(ProvAComplemento.Index, p).Value / ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Total_Real")
            ImpGpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Grabado") * PorcenPro
            ImpEpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "Imp_Exento") * PorcenPro
            ImpIvaP = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, p).Value, "IVA_real") * PorcenPro


            Cuenta2 = RegresaCunetaComple("205000020001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(TotComplemento.Index, p).Value, 0, Cuenta2, "")
            Item = Item + 1
            '2° Pagos
            If Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'TODOS

                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")

                Item = Item + 1

                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")

                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)


            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then 'Efectivo y Transferencia
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Transferencia

                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Efecivo
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")
                Item = Item + 1
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)
            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then ' Efecivo
                'Cuenta del Abono
                cadena = Trim(Me.TablaCF.Item(CtaEfComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)

                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEfComplemento.Index, p).Value, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value = 0 Then ' Transferencia
                'Cuenta del Abono transferencia
                cadena = Trim(Me.TablaCF.Item(CtaBancosComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpTComplemento.Index, p).Value, Cuenta_Cargo, IIf(IsDBNull(Me.TablaCF.Item(NoChComplemento.Index, p).Value) = True, "", Me.TablaCF.Item(NoChComplemento.Index, p).Value))

            ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaCF.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque 
                cadena = Trim(Me.TablaCF.Item(CtaChequeC.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpChComplemento.Index, p).Value, Cuenta_Cargo, Me.TablaCF.Item(NoChComplemento.Index, p).Value)

            End If

            If Me.TablaCF.Item(AntiComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con anticipos +
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAntiComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(AntiComplemento.Index, p).Value * -1, 0, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(AntiComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta  anticipos -
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAntiComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(AntiComplemento.Index, p).Value, Cuenta_Cargo, "")

            End If
            'Inserta Ajustes
            If Me.TablaCF.Item(AjusComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con ajuste +
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAjusComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                'cargo de Ajuste
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(AjusComplemento.Index, p).Value * -1, 0, Cuenta_Cargo, "")

            ElseIf Me.TablaCF.Item(AjusComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta con ajuste -
                Item += 1
                cadena = Trim(Me.TablaCF.Item(CtaAjusComplemento.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                'Abono de Ajuste
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(AjusComplemento.Index, p).Value, Cuenta_Cargo, "")

            End If
            If Me.TablaCF.Item(PCComplemento.Index, p).Value > 0 Then ' Se perdida cambiaria
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(PCComplemento.Index, p).Value, 0, "7010000100000000", "")
            End If
            If Me.TablaCF.Item(UCComplemento.Index, p).Value > 0 Then ' Se utilidad cambiaria
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(UCComplemento.Index, p).Value, "7020000100000000", "")
            End If
            '3° Gasto Pagado
            cadena = Trim(Me.TablaCF.Item(NomCtaComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Item += 1

            If ImpEpro > 0 And ImpGpro > 0 Then ' tiene grabado y exento
                'cargos

                Cuenta2 = RegresaCunetaComple("701000040001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta_Cargo, "")
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                Item = Item + 1

                Cuenta2 = RegresaCunetaComple("701500040001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta_Cargo, "") 'Revisar Cuenta que Graba provision
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                Item = Item + 1

                Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100020000", "")
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100020000", "")
                Item = Item + 1


            ElseIf ImpEpro > 0 And ImpGpro <= 0 Then 'Tiene Exento



                Cuenta2 = RegresaCunetaComple("7010000400001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, ImpEpro, 0, Cuenta2, "")
                Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)

                Item = Item + 1

                Cuenta2 = RegresaCunetaComple("7015000400001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, ImpEpro, Cuenta2, "")
                Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)

                Item = Item + 1
            ElseIf ImpEpro <= 0 And ImpGpro > 0 Then 'Tiene Grabado




                Cuenta2 = RegresaCunetaComple("70100010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, ImpGpro, 0, Cuenta2, "")
                Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                Item = Item + 1


                Cuenta2 = RegresaCunetaComple("70150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, ImpGpro, Cuenta2, "")
                Actualiza_Registro_Xml_Comple(Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value & "-" & Cuenta2), Me.TablaCF.Item(Id_Complemento.Index, p).Value)
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, ImpIvaP, 0, "1180000100060000", "")
                Item = Item + 1
                Crea_detalle_poliza(pol, Item, 0, ImpIvaP, "1190000100060000", "")
                Item = Item + 1
            End If

        End If

        If LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "C" Or LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "CPP" Then
            If UCase(Me.TablaCF.Item(CtaOrdenC.Index, p).Value) = "SI" Then

                Item = Item + 1
                If Len(Me.TablaCF.Item(RFCComplemento.Index, p).Value) = 12 Then

                    If Me.TablaCF.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value > 0 Then

                        If Tas = "8" Then
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "9011001000010004", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "9011001000010001", "")
                            Else
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "9011001000010002", "")
                            End If
                            Item = Item + 1
                        Else
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "9011001000010003", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "9011001000010001", "")
                            Else
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "9011001000010002", "")
                            End If
                            Item = Item + 1
                        End If


                    ElseIf Me.TablaCF.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value <= 0 Then 'Tiene Exento
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "9011001000010001", "")
                        Else
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "9011001000010002", "")
                        End If
                        Item = Item + 1
                    ElseIf Me.TablaCF.Item(ImpEComplemento.Index, p).Value <= 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value > 0 Then 'Tiene Grabado
                        If Tas = "8" Then
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "9011001000010004", "")
                            Item = Item + 1
                        Else
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "9011001000010003", "")
                            Item = Item + 1
                        End If

                    End If
                Else

                    If Me.TablaCF.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value > 0 Then
                        If Tas = "8" Then
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "9011002000010004", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "9011002000010001", "")
                            Else
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "9011002000010002", "")
                            End If
                            Item = Item + 1
                        Else
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "9011002000010003", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "9011002000010001", "")
                            Else
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "9011002000010002", "")
                            End If
                            Item = Item + 1
                        End If


                    ElseIf Me.TablaCF.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value <= 0 Then 'Tiene Exento
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "9011002000010001", "")
                        Else
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "9011002000010002", "")
                        End If
                        Item = Item + 1
                    ElseIf Me.TablaCF.Item(ImpEComplemento.Index, p).Value <= 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value > 0 Then 'Tiene Grabado

                        If Tas = "8" Then
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "9011002000010004", "")
                            Item = Item + 1
                        Else
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "9011002000010003", "")
                            Item = Item + 1
                        End If

                    End If
                End If

                If Me.TablaCF.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value > 0 Then
                    If Tas = "8" Then
                        Cuenta2 = RegresaCunetaComple("901000010004", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, Cuenta2, "")
                        Item = Item + 1
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCunetaComple("901000010001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCunetaComple("901000010002", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, Cuenta2, "")
                        Item = Item + 1
                    Else
                        Cuenta2 = RegresaCunetaComple("901000010003", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, Cuenta2, "")
                        Item = Item + 1
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCunetaComple("901000010001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCunetaComple("901000010002", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, Cuenta2, "")
                        Item = Item + 1
                    End If


                ElseIf Me.TablaCF.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value <= 0 Then 'Tiene Exento
                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Cuenta2 = RegresaCunetaComple("901000010001", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Else
                        Cuenta2 = RegresaCunetaComple("901000010002", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    End If
                    Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, Cuenta2, "")
                    Item = Item + 1
                ElseIf Me.TablaCF.Item(ImpEComplemento.Index, p).Value <= 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value > 0 Then 'Tiene Grabado
                    If Tas = "8" Then
                        Cuenta2 = RegresaCunetaComple("901000010004", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, Cuenta2, "")
                        Item = Item + 1
                    Else
                        Cuenta2 = RegresaCunetaComple("901000010003", Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, Cuenta2, "")
                        Item = Item + 1
                    End If

                End If



            End If
        ElseIf LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "GG" Or LetraFactura(Me.TablaCF.Item(UUIDComplemento.Index, p).Value) = "GGPP" Then
            If UCase(Me.TablaCF.Item(CtaOrdenC.Index, p).Value) = "SI" Then
                Item = Item + 1



                If Len(Me.TablaCF.Item(RFCComplemento.Index, p).Value) = 12 Then
                    If Me.TablaCF.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value > 0 Then 'Moral
                        If Tas = "8" Then
                            'Solicitar Cuenta
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "902100100050", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "902100100010", "")
                            Else
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "902100100010", "")
                            End If
                            Item = Item + 1
                        Else
                            'Solicitar Cuenta
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "902100100020", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "902100100010", "")
                            Else
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "902100100010", "")
                            End If
                            Item = Item + 1
                        End If


                    ElseIf Me.TablaCF.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value <= 0 Then 'Tiene Exento
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "902100100010", "")
                        Else
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "902100100010", "")
                        End If
                        Item = Item + 1
                    ElseIf Me.TablaCF.Item(ImpEComplemento.Index, p).Value <= 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value > 0 Then 'Tiene Grabado

                        If Tas = "8" Then
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "902100100050", "")
                            Item = Item + 1

                        Else
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "902100100020", "")
                            Item = Item + 1
                        End If

                    End If
                Else 'Fisica 

                    If Me.TablaCF.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value > 0 Then
                        If Tas = "8" Then
                            'Solicitar Cuenta
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "902100200050", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "902100200010", "")
                            Else
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "902100200010", "")
                            End If
                            Item = Item + 1
                        Else
                            'Solicitar Cuenta
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "902100200020", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "902100200010", "")
                            Else
                                Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "902100200010", "")
                            End If
                            Item = Item + 1
                        End If


                    ElseIf Me.TablaCF.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value <= 0 Then 'Tiene Exento
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "902100200010", "")
                        Else
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, 0, "902100200010", "")
                        End If
                        Item = Item + 1
                    ElseIf Me.TablaCF.Item(ImpEComplemento.Index, p).Value <= 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value > 0 Then 'Tiene Grabado
                        If Tas = "8" Then
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "902100200050", "")
                            Item = Item + 1
                        Else
                            Crea_detalle_poliza(pol, Item, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, 0, "902100200020", "")
                            Item = Item + 1
                        End If

                    End If
                End If

                If Me.TablaCF.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value > 0 Then
                    If Tas = "8" Then
                        Cuenta2 = RegresaCunetaComple("90200050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, Cuenta2, "")
                        Item = Item + 1
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCunetaComple("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCunetaComple("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, Cuenta2, "")
                        Item = Item + 1

                    Else
                        Cuenta2 = RegresaCunetaComple("90200020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, Cuenta2, "")
                        Item = Item + 1
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCunetaComple("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCunetaComple("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, Cuenta2, "")
                        Item = Item + 1

                    End If

                ElseIf Me.TablaCF.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value <= 0 Then 'Tiene Exento
                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Cuenta2 = RegresaCunetaComple("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    Else
                        Cuenta2 = RegresaCunetaComple("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                    End If
                    Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpEComplemento.Index, p).Value, Cuenta2, "")
                    Item = Item + 1
                ElseIf Me.TablaCF.Item(ImpEComplemento.Index, p).Value <= 0 And Me.TablaCF.Item(ImpGComplemento.Index, p).Value > 0 Then 'Tiene Grabado
                    If Tas = "8" Then
                        Cuenta2 = RegresaCunetaComple("90200050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, Cuenta2, "")
                        Item = Item + 1
                    Else
                        Cuenta2 = RegresaCunetaComple("90200020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaCF.Item(RFCComplemento.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, 0, Me.TablaCF.Item(ImpGComplemento.Index, p).Value, Cuenta2, "")
                        Item = Item + 1
                    End If

                End If

            End If
        End If
        Exit Sub
    End Sub
    Private Function Obtener_ID(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer = -1
        For i As Integer = 0 To Col.Items.Count - 1
            If valor = Trim(Col.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice
    End Function
    Private Sub LstComple_Enters() Handles LstComple.Enters
        If Me.TablaCF.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In TablaCF.Rows
                If Fila.Cells(LetraContabilidadComplemento.Index).Selected = True Then

                    Try
                        If Me.LstComple.SelectText = "" Then
                            Fila.Cells(LetraContabilidadComplemento.Index).Value = ""
                        Else
                            Fila.Cells(LetraContabilidadComplemento.Index).Value = Me.LstComple.SelectText
                        End If


                    Catch ex As Exception

                    End Try
                ElseIf Fila.Cells(BancoOrigenTComplemento.Index).Selected = True Then

                    Try
                        If Me.LstComple.SelectText = "" Then
                            Fila.Cells(BancoOrigenTComplemento.Index).Value = ""

                        Else

                            If Obtener_ID(Me.LstComple.SelectText, Me.BancoOrigenTComplemento) < 0 Then
                                Fila.Cells(BancoOrigenTComplemento.Index).Value = ""
                            Else
                                'Dim s As String = Me.LetraContabilidadComplemento.Items(1)
                                Fila.Cells(BancoOrigenTComplemento.Index).Value = Me.BancoOrigenTComplemento.Items(Obtener_ID(Me.LstComple.SelectText, Me.BancoOrigenTComplemento))
                                'Fila.Cells(LetraContabilidadComplemento.Index).Value = Me.LetraContabilidadComplemento.Items(Obtener_index(Me.LstComple.SelectText))
                            End If
                        End If

                    Catch ex As Exception

                    End Try
                ElseIf Fila.Cells(BancoDtComplemento.Index).Selected = True Then

                ElseIf Fila.Cells(TipPolComplemento.Index).Selected = True Then

                    If Me.LstComple.SelectText = "" Then
                        Fila.Cells(TipPolComplemento.Index).Value = ""
                    Else
                        Try
                            If Obtener_indextipo(Me.LstComple.SelectText) < 0 Then
                                Fila.Cells(TipPolComplemento.Index).Value = ""
                            Else
                                Dim s As String = Me.TipPolComplemento.Items(1)
                                Fila.Cells(TipPolComplemento.Index).Value = Me.TipPolComplemento.Items(Obtener_indextipo(Me.LstComple.SelectText))
                            End If

                        Catch ex As Exception

                        End Try
                    End If
                Else

                    ' For I As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                    For j As Integer = 0 To Me.TablaCF.Columns.Count - 1
                        If Me.TablaCF.Item(j, Fila.Index).Selected = True And (j <> LetraContabilidadComplemento.Index And j <> BancoDtComplemento.Index And j <> TipPolComplemento.Index) Then
                            Me.TablaCF.Item(j, Fila.Index).Value = Me.LstComple.SelectText
                        ElseIf Me.TablaCF.Item(j, Fila.Index).Selected = True And (j = LetraContabilidadComplemento.Index And j = BancoDtComplemento.Index And j = TipPolComplemento.Index) Then
                            Me.TablaCF.Item(j, Fila.Index).Value = ""
                        End If
                    Next

                End If
                ComplemantosL(Fila.Index)
            Next
        End If
    End Sub
    Private Function CuentaTipoC(ByVal RFC As String, ByVal Clave As String, ByVal Fila As Integer)
        Dim Tipo As String = " "
        Dim Sql As String = ""
        Dim Cuenta As String
        Dim CTipo As String = ""

        If Convert.ToDecimal(Me.TablaCF.Item(ImpEComplemento.Index, Fila).Value) > Convert.ToDecimal(Me.TablaCF.Item(ImpGComplemento.Index, Fila).Value) Then
            Tipo = " ExentoPUE, NivelE "
            CTipo = "ExentoPUE"
        Else
            Tipo = " GravadoPUE, NivelG "
            CTipo = "GravadoPUE"
        End If

        Sql = " SELECT " & Tipo & "   FROM  ClaveEgresos WHERE ClaveEgresos.Id_Empresa  = " & Me.lstCliente.SelectItem & " and ClaveEgresos.Clave = '" & Clave & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) > "0000" Then
                Tipo = "  SUBSTRING(ClaveEgresos." & CTipo & ", 1, 4) = Catalogo_de_Cuentas.Nivel1 AND SUBSTRING(ClaveEgresos." & CTipo & ", 5, 4) = Catalogo_de_Cuentas.Nivel2 AND SUBSTRING(ClaveEgresos." & CTipo & ",9, 4) = Catalogo_de_Cuentas.Nivel3 "
            ElseIf ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(4, 4) > "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) = "0000" Then 'Tercer Nivel
                Tipo = "  SUBSTRING(ClaveEgresos." & CTipo & ", 1, 4) = Catalogo_de_Cuentas.Nivel1 AND SUBSTRING(ClaveEgresos." & CTipo & ", 5, 4) = Catalogo_de_Cuentas.Nivel2   "
            ElseIf ds.Tables(0).Rows(0)(0).ToString().Substring(4, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" Then 'Primer Nivel
                Tipo = "  SUBSTRING(ClaveEgresos." & CTipo & ", 1, 4) = Catalogo_de_Cuentas.Nivel1 "
            End If
            Sql = "SELECT Rtrim(Catalogo_de_Cuentas.Descripcion) + '-' + CONVERT(VARCHAR, Catalogo_de_Cuentas.Cuenta) AS Cuenta"
            Sql &= " FROM     ClaveEgresos INNER JOIN "
            Sql &= " Catalogo_de_Cuentas ON " & Tipo & " WHERE  (ClaveEgresos.Id_Empresa = " & Me.lstCliente.SelectItem & ") AND (ClaveEgresos.Clave = '" & Clave & "') AND (Catalogo_de_Cuentas.RFC = '" & RFC & "')"
            ds.Clear()
            ds = Eventos.Obtener_DS(Sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim C() As String = Split(ds.Tables(0).Rows(0)("Cuenta"), "-")
                Cuenta = Buscar_Madre(C(1)) & " " & ds.Tables(0).Rows(0)("Cuenta")
            Else
                Cuenta = ""
            End If
        Else
            Cuenta = ""
        End If
        Return Cuenta
    End Function
    Private Sub ComplemantosL(ByVal I As Integer)

        'Calcula cuenta cargos
        Try

            If Me.TablaCF.Item(ProvPComplemento.Index, I).Value > 0 Then
                Me.TablaCF.Item(ProvAComplemento.Index, I).Value = 0
            ElseIf Me.TablaCF.Item(ProvAComplemento.Index, I).Value > 0 Then
                Me.TablaCF.Item(ProvPComplemento.Index, I).Value = 0
            End If

            If Me.TablaCF.Item(RefcLAVE.Index, I).Value <> Nothing Then

                Dim Nueva As String = CuentaTipoC(Me.TablaCF.Item(RFCComplemento.Index, I).Value, Trim(Me.TablaCF.Item(RefcLAVE.Index, I).Value), I)

                Me.TablaCF.Item(NomCtaComplemento.Index, I).Value = Nueva
                Me.TablaCF.Item(LetraSatComplemento.Index, I).Value = Eventos.Calcula_letraSat(Me.TablaCF.Item(FpagoComplemento.Index, I).Value, Me.TablaCF.Item(UCFDIComplemento.Index, I).Value)


            Else
                Me.TablaCF.Item(NomCtaComplemento.Index, I).Value = ""
            End If

            'CALCULAR CUENTA DE EFECTIVO
            If Me.TablaCF.Item(ImpEfComplemento.Index, I).Value > 0 Then
                If Candado_Importe_Efectivocomple(I) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo

                    Me.TablaCF.Item(CtaEfComplemento.Index, I).Value = Cuenta_Efectivo()
                    Me.TablaCF.Columns(CtaEfComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    If RadMessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaCF.Item(CtaEfComplemento.Index, I).Value = Cuenta_Efectivo()
                        Me.TablaCF.Columns(CtaEfComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaCF.Item(ImpEfComplemento.Index, I).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaCF.Item(CtaEfComplemento.Index, I).Value = ""
            End If

            'CALCULAR CUENTA DE Ajustes
            If Me.TablaCF.Item(AjusComplemento.Index, I).Value <> 0 Then
                If Candado_Importe_Efectivocomple(I) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo

                    Me.TablaCF.Item(CtaAjusComplemento.Index, I).Value = Cuenta_Efectivo()
                    Me.TablaCF.Columns(CtaAjusComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    If RadMessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaCF.Item(CtaAjusComplemento.Index, I).Value = Cuenta_Efectivo()
                        Me.TablaCF.Columns(CtaAjusComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaCF.Item(AjusComplemento.Index, I).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaCF.Item(CtaAjusComplemento.Index, I).Value = ""
            End If
            'CALCULAR CUENTA DE anticipos
            If Me.TablaCF.Item(AntiComplemento.Index, I).Value > 0 Then

                Me.TablaCF.Item(CtaAntiComplemento.Index, I).Value = Cuenta_Anticipo()
                Me.TablaCF.Columns(CtaAntiComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Else
                Me.TablaCF.Item(CtaAntiComplemento.Index, I).Value = ""
            End If

            ' Importe de Transferencia
            If Me.TablaCF.Item(ImpTComplemento.Index, I).Value > 0 Then
                If Trim(Me.TablaCF.Item(BancoOrigenTComplemento.Index, I).Value) <> Nothing Then
                    If Trim(Me.TablaCF.Item(CtaOTComplemento.Index, I).Value) <> "" Then
                    Else
                        Dim posi As Integer = InStr(1, Me.TablaCF.Item(BancoOrigenTComplemento.Index, I).Value, "-", CompareMethod.Binary)
                        Dim cuantos As Integer = Len(Me.TablaCF.Item(BancoOrigenTComplemento.Index, I).Value) - Len(Me.TablaCF.Item(BancoOrigenTComplemento.Index, I).Value.Substring(0, posi))
                        Dim Al As String = Me.TablaCF.Item(BancoOrigenTComplemento.Index, I).Value.Substring(posi, cuantos)
                        Me.TablaCF.Item(CtaOTComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)

                        Me.TablaCF.Item(CtaBancosComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                    End If

                End If

                'Empieza el nuevo codigo Bnaco destino
                If Trim(Me.TablaCF.Item(BancoDtComplemento.Index, I).Value) <> Nothing Then
                    Try
                        If Trim(Me.TablaCF.Item(CtaDTComplemento.Index, I).Value) <> "" And Trim(Me.TablaCF.Item(CtaDTComplemento.Index, I).Value) <> "0" Then
                            Dim largo As Integer = Len(Me.TablaCF.Item(BancoDtComplemento.Index, I).Value)
                            Dim cadena As String = Trim(Me.TablaCF.Item(BancoDtComplemento.Index, I).Value.ToString.Substring(largo - 3, 3))
                            largo = Len(Me.TablaCF.Item(CtaDTComplemento.Index, I).Value)
                            Dim Al As String = Me.TablaCF.Item(CtaDTComplemento.Index, I).Value.Substring(largo - 3, 3)
                            If cadena <> Al Then
                                largo = InStr(1, Me.TablaCF.Item(BancoDtComplemento.Index, I).Value, "-", CompareMethod.Binary)
                                Al = Me.TablaCF.Item(BancoDtComplemento.Index, I).Value.Substring(0, largo - 1)
                                Me.TablaCF.Item(CtaDTComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaCF.Item(RFCComplemento.Index, I).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                            End If
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaCF.Item(BancoDtComplemento.Index, I).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaCF.Item(BancoDtComplemento.Index, I).Value.Substring(0, posi - 1)
                            Me.TablaCF.Item(CtaDTComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaCF.Item(RFCComplemento.Index, I).Value & "'", True)
                        End If
                    Catch ex As Exception

                    End Try
                Else
                    Me.TablaCF.Item(BancoDtComplemento.Index, I).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaCF.Item(RFCComplemento.Index, I).Value)
                    Try
                        If Trim(Me.TablaCF.Item(CtaDTComplemento.Index, I).Value) <> "" And Trim(Me.TablaCF.Item(CtaDTComplemento.Index, I).Value) <> "0" Then
                            Dim largo As Integer = Len(Me.TablaCF.Item(BancoDtComplemento.Index, I).Value)
                            Dim cadena As String = Trim(Me.TablaCF.Item(BancoDtComplemento.Index, I).Value.ToString.Substring(largo - 3, 3))
                            largo = Len(Me.TablaCF.Item(CtaDTComplemento.Index, I).Value)
                            Dim Al As String = Me.TablaCF.Item(CtaDTComplemento.Index, I).Value.Substring(largo - 3, 3)
                            If cadena <> Al Then
                                largo = InStr(1, Me.TablaCF.Item(BancoDtComplemento.Index, I).Value, "-", CompareMethod.Binary)
                                Al = Me.TablaCF.Item(BancoDtComplemento.Index, I).Value.Substring(0, largo - 1)
                                Me.TablaCF.Item(CtaDTComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaCF.Item(RFCComplemento.Index, I).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                            End If
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaCF.Item(BancoDtComplemento.Index, I).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaCF.Item(BancoDtComplemento.Index, I).Value.Substring(0, posi - 1)
                            Me.TablaCF.Item(CtaDTComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaCF.Item(RFCComplemento.Index, I).Value & "'", True)
                        End If
                    Catch ex As Exception

                    End Try
                End If
                If Trim(Me.TablaCF.Item(TipPolComplemento.Index, I).Value) <> Nothing Then
                    If Trim(Me.TablaCF.Item(BancoOrigenTComplemento.Index, I).Value) <> Nothing Then
                    Else
                        Dim fila As DataGridViewRow = Me.TablaCF.Rows(I)
                        Try
                            If Trim(Me.TablaCF.Item(TipPolComplemento.Index, I).Value) <> "" Then
                                Dim largo As Integer = Len(Me.TablaCF.Item(TipPolComplemento.Index, I).Value)
                                Dim posicion As Integer = InStr(1, Me.TablaCF.Item(TipPolComplemento.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaCF.Item(TipPolComplemento.Index, I).Value.Substring(posicion + 1, largo - posicion - 1)
                                Dim indice As Integer = Obtener_indexbancoO(Al)
                                If indice = 1000 Then
                                Else
                                    fila.Cells(BancoOrigenTComplemento.Index).Value = Me.BancoOrigenTComplemento.Items(indice)
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                End If

                'termina


                If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                    Me.TablaCF.Columns(BancoOrigenTComplemento.Index).Visible = False
                    Me.TablaCF.Columns(CtaOTComplemento.Index).Visible = False
                    Me.TablaCF.Columns(BancoDtComplemento.Index).Visible = False
                    Me.TablaCF.Columns(CtaDTComplemento.Index).Visible = False
                    Me.TablaCF.Columns(FTComplemento.Index).Visible = True

                End If


            End If
            If Me.TablaCF.Item(ImpChComplemento.Index, I).Value > 0 Then
                'Cheques

                If Trim(Me.TablaCF.Item(BancoCHComplemento.Index, I).Value) <> "" Then
                    If Trim(Me.TablaCF.Item(CtaOchComplemento.Index, I).Value) <> "" Then
                    Else
                        Dim posi As Integer = InStr(1, Me.TablaCF.Item(BancoCHComplemento.Index, I).Value, "-", CompareMethod.Binary)
                        Dim cuantos As Integer = Len(Me.TablaCF.Item(BancoCHComplemento.Index, I).Value) - Len(Me.TablaCF.Item(BancoCHComplemento.Index, I).Value.Substring(0, posi))
                        Dim Al As String = Me.TablaCF.Item(BancoCHComplemento.Index, I).Value.Substring(posi, cuantos)
                        Me.TablaCF.Item(CtaOchComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                        Me.TablaCF.Item(CtaChequeC.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                    End If
                Else
                    ' Cargar_bancosComple("Ch")
                End If
                'Se Muestran Columnas de Contabilidad Electronica
                If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                    Me.TablaCF.Columns(BancoCHComplemento.Index).Visible = False
                    Me.TablaCF.Columns(CtaOchComplemento.Index).Visible = False
                    Me.TablaCF.Columns(NoChComplemento.Index).Visible = False
                    Me.TablaCF.Columns(FCHComplemento.Index).Visible = True
                End If

            End If
            Liberar_ProcesoComple(I)
        Catch ex As Exception

        End Try


    End Sub
    Private Function Calcula_diferencia(ByVal total As Decimal, ByVal monto_Efectivo As Decimal, ByVal monto_cheque As Decimal, ByVal monto_Transferencia As Decimal, ByVal ajuste As Decimal, ByVal Anticipo As Decimal, ByVal Provision As Decimal
                                  )
        Dim Diferencia As Decimal = 0
        Diferencia = total - (monto_Efectivo + monto_cheque + monto_Transferencia + ajuste + Anticipo + Provision)
        Return Diferencia
    End Function
    Private Sub Calcula_Importes(ByVal i As Integer, ByVal Suma As Decimal)
        If Me.TablaCF.Rows.Count >= 1 Then

            Try
                Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP As Decimal
                PorcenPro = Suma / ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, i).Value, "Total_Real")
                ImpGpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, i).Value, "Imp_Grabado") * PorcenPro
                ImpEpro = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, i).Value, "Imp_Exento") * PorcenPro
                ImpIvaP = ImpFaCURA(Me.TablaCF.Item(UUIDComplemento.Index, i).Value, "IVA_real") * PorcenPro
                If Me.TablaCF.Item(TotComplemento.Index, i).Value > 0 And Me.TablaCF.Item(TotComplemento.Index, i).Value <> Nothing Then
                    'importe gravado
                    Me.TablaCF.Item(ImpGComplemento.Index, i).Value = Math.Round(ImpGpro, 2)
                    'importe exento
                    Me.TablaCF.Item(ImpEComplemento.Index, i).Value = Math.Round(ImpEpro, 2)
                    'Iva_real
                    Me.TablaCF.Item(IvaRComplemento.Index, i).Value = Math.Round(ImpIvaP, 2)
                    'calcula el % Pago Acumulado
                    If Me.TablaCF.Item(ImpEComplemento.Index, i).Value < 1 Then
                        Me.TablaCF.Item(ImpGComplemento.Index, i).Value = Me.TablaCF.Item(ImpGComplemento.Index, i).Value + Me.TablaCF.Item(ImpEComplemento.Index, i).Value
                        Me.TablaCF.Item(ImpEComplemento.Index, i).Value = 0
                    End If
                    'Total real
                    Me.TablaCF.Item(TotComplemento.Index, i).Value = Me.TablaCF.Item(ImpGComplemento.Index, i).Value + Me.TablaCF.Item(ImpEComplemento.Index, i).Value + Me.TablaCF.Item(IvaRComplemento.Index, i).Value
                End If
            Catch ex As Exception
                Me.TablaCF.Rows(i).DefaultCellStyle.BackColor = Color.Red
            End Try

        End If

    End Sub
    Private Sub Liberar_ProcesoComple(ByVal i As Integer)
        Dim contador As Integer = 0
        Dim cantidad As Decimal = Me.TablaCF.Item(ImpProviComplemento.Index, i).Value
        cantidad = cantidad + Me.TablaCF.Item(UCComplemento.Index, i).Value
        cantidad = cantidad - Me.TablaCF.Item(PCComplemento.Index, i).Value

        Dim Retencion As Decimal = 0
        Retencion = Me.TablaCF.Item(RISRComplemento.Index, i).Value + Me.TablaCF.Item(RIVAComplemento.Index, i).Value
        'Calcula la diferencia en el registro
        Me.TablaCF.Item(DifComplemento.Index, i).Value = Math.Round(Calcula_diferencia(IIf(IsDBNull(Me.TablaCF.Item(TotComplemento.Index, i).Value) = True, 0, Me.TablaCF.Item(TotComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaCF.Item(ImpEfComplemento.Index, i).Value), Convert.ToDecimal(Retencion + Me.TablaCF.Item(ImpTComplemento.Index, i).Value) + Convert.ToDecimal(Me.TablaCF.Item(ProvAComplemento.Index, i).Value) + Convert.ToDecimal(Me.TablaCF.Item(ProvPComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaCF.Item(ImpChComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaCF.Item(AjusComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaCF.Item(AntiComplemento.Index, i).Value), cantidad), 2) - Busacar_parcialidades(Me.TablaCF.Item(UUIDComplemento.Index, i).Value.trim())


        If Trim(UCase(Me.lblce.Text)) = "TRUE" Then ' Bloquera filas de Contabilidad electronica
            ' If Me.TablaC.Item(DifComplemento.Index, i).Value > 0 Or Me.TablaC.Item(TipPolComplemento.Index, i).Value = Nothing Or IIf(IsDBNull(Me.TablaC.Item(NomCtaComplemento.Index, i).Value) = True, "", Me.TablaC.Item(NomCtaComplemento.Index, i).Value) = "" Or IIf(IsDBNull(Me.TablaC.Item(NumPolComplemento.Index, i).Value) = True, "", Me.TablaC.Item(NumPolComplemento.Index, i).Value) = "" Then
            If Me.TablaCF.Item(DifComplemento.Index, i).Value < 0 Then

                Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
            Else

                If Me.TablaCF.Item(ImpTComplemento.Index, i).Value > 0 Then ' Bloqueo transferencia
                    Try
                        If Me.TablaCF.Item(BancoOrigenTComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(BancoDtComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(CtaOTComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(CtaDTComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(FTComplemento.Index, i).Value = Nothing Then
                            Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                        Else
                            Me.TablaCF.Item(ApliComplemento.Index, i).Value = True
                        End If
                    Catch ex As Exception
                        Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                    End Try

                ElseIf Me.TablaCF.Item(ImpChComplemento.Index, i).Value > 0 Then ' Bloqueo cheques
                    Try
                        If Me.TablaCF.Item(BancoCHComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(CtaOchComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(NoChComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(FCHComplemento.Index, i).Value = Nothing Then
                            Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                        Else
                            Me.TablaCF.Item(ApliComplemento.Index, i).Value = True
                        End If
                    Catch ex As Exception
                        Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                    End Try

                ElseIf Me.TablaCF.Item(BancoCHComplemento.Index, i).Value > 0 And Me.TablaCF.Item(ImpTComplemento.Index, i).Value > 0 Then ' AMbos
                    Try
                        If Me.TablaCF.Item(BancoOrigenTComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(BancoDtComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(CtaOTComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(CtaDTComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(FTComplemento.Index, i).Value = Nothing Then
                            Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                        Else
                            Me.TablaCF.Item(ApliComplemento.Index, i).Value = True
                        End If
                    Catch ex As Exception
                        Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                    End Try

                    Try
                        If Me.TablaCF.Item(BancoCHComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(CtaOchComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(NoChComplemento.Index, i).Value = Nothing Or Me.TablaCF.Item(FCHComplemento.Index, i).Value = Nothing Then
                            Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                        Else
                            Me.TablaCF.Item(ApliComplemento.Index, i).Value = True
                        End If
                    Catch ex As Exception
                        Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                    End Try
                ElseIf Me.TablaCF.Item(ImpEfComplemento.Index, i).Value > 0 Then ' AMbos
                    Me.TablaCF.Item(ApliComplemento.Index, i).Value = True
                Else
                    Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                End If

                If Me.TablaCF.Item(AjusComplemento.Index, i).Value <> 0 Then ' Bloqueo Ajuste
                    If IIf(IsDBNull(Me.TablaCF.Item(CtaAjusComplemento.Index, i).Value) = True, "", Me.TablaCF.Item(CtaAjusComplemento.Index, i).Value) = "" Then
                        Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                    Else
                        Me.TablaCF.Item(ApliComplemento.Index, i).Value = True
                    End If
                End If

                If Me.TablaCF.Item(AntiComplemento.Index, i).Value <> 0 Then ' Bloqueo Anticipo
                    If IIf(IsDBNull(Me.TablaCF.Item(CtaAntiComplemento.Index, i).Value) = True, "", Me.TablaCF.Item(CtaAntiComplemento.Index, i).Value) = "" Then
                        Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                    Else
                        Me.TablaCF.Item(ApliComplemento.Index, i).Value = True
                    End If
                End If

            End If
        Else
            'sin contabilidad electronica
            If Me.TablaCF.Item(DifComplemento.Index, i).Value <> 0 Or Me.TablaCF.Item(TipPolComplemento.Index, i).Value = Nothing Or IIf(IsDBNull(Me.TablaCF.Item(NumPolComplemento.Index, i).Value) = True, "", Me.TablaCF.Item(NumPolComplemento.Index, i).Value) = "" Then
                Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
            Else
                Me.TablaCF.Item(ApliComplemento.Index, i).Value = True
            End If
            Try
                If Me.TablaCF.Item(AjusComplemento.Index, i).Value <> 0 Then ' Bloqueo Ajuste
                    If IIf(IsDBNull(Me.TablaCF.Item(CtaAjusComplemento.Index, i).Value) = True, "", Me.TablaCF.Item(CtaAjusComplemento.Index, i).Value) = "" Then
                        Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                    Else
                        Me.TablaCF.Item(ApliComplemento.Index, i).Value = True
                    End If
                End If

                If Me.TablaCF.Item(AntiComplemento.Index, i).Value <> 0 Then ' Bloqueo Anticipo
                    If IIf(IsDBNull(Me.TablaCF.Item(CtaAntiComplemento.Index, i).Value) = True, "", Me.TablaCF.Item(CtaAntiComplemento.Index, i).Value) = "" Then
                        Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
                    Else
                        Me.TablaCF.Item(ApliComplemento.Index, i).Value = True
                    End If
                End If
            Catch ex As Exception

            End Try
        End If

        For s As Integer = 0 To Me.TablaCF.Rows.Count - 1
            If Me.TablaCF.Item(0, s).Value = True Then
                contador = contador + 1
            End If
        Next
        If contador > 0 Then
            Me.CmdProcesaComple.Enabled = True
        End If
        If Me.TablaCF.Item(ApliComplemento.Index, i).Style.BackColor = Color.Red Then
            Me.TablaCF.Item(ApliComplemento.Index, i).Value = False
        End If

    End Sub

    Private Function Obtener_indexletra(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.LetraContabilidadComplemento.Items.Count - 1
            If valor = Trim(Me.LetraContabilidadComplemento.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indextipo(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.TipPolComplemento.Items.Count - 1
            If valor = Trim(Me.TipPolComplemento.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexbancoO(ByVal valor As String)
        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BancoOrigenTComplemento.Items.Count - 1
            If valor = Trim(Me.BancoOrigenTComplemento.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexBncoCh(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BancoCHComplemento.Items.Count - 1
            If valor = Trim(Me.BancoCHComplemento.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function


    Private Sub Cargar_bancosComple(ByVal bancos As String)
        Dim sql As String = "Select  RTrim(bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias FROM Bancos_Clientes INNER JOIN Bancos ON Bancos_Clientes.Id_Banco =Bancos.Id_Banco  where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%" & bancos & "%'"
        'Dim sql As String = " SELECT rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias FROM Bancos_Clientes INNER JOIN Bancos ON Bancos_Clientes.Id_Banco =Bancos.Id_Banco  where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%" & bancos & "%'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If bancos = "Cheq" Then
                'Cheque
                If Me.BancoCHComplemento.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancoCHComplemento.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                Else
                    Try
                        Me.BancoCHComplemento.Items.Clear()
                    Catch ex As Exception

                    End Try

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancoCHComplemento.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                End If
            Else
                'Transferencia
                'Origen
                If Me.BancoOrigenTComplemento.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancoOrigenTComplemento.Items.Add(ds.Tables(0).Rows(i)("Alias"))

                    Next
                Else
                    Try
                        Me.BancoOrigenTComplemento.Items.Clear()
                    Catch ex As Exception

                    End Try


                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancoOrigenTComplemento.Items.Add(ds.Tables(0).Rows(i)("Alias"))

                    Next
                End If

            End If
        End If

    End Sub
    Private Function Banco_destinoComp(ByVal Cliente As Integer, ByVal Rfc As String)

        Dim banco As String = ""
        Dim sql2 As String = " SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Cliente & ") and RFC = '" & Rfc & "' and Favorito=1"
        Dim ds2 As DataSet = Eventos.Obtener_DS(sql2)

        If ds2.Tables(0).Rows.Count > 0 Then
            Dim largo As Integer = Len(ds2.Tables(0).Rows(0)("Clabe"))
            Try
                banco = Trim(ds2.Tables(0).Rows(0)("Banco")) & " " & Trim(ds2.Tables(0).Rows(0)("Clabe").ToString.Substring(largo - 3, 3))
            Catch ex As Exception

            End Try
        Else
            banco = ""
        End If

        Return banco

    End Function
    Private Sub CmdGuardaComple_Click(sender As Object, e As EventArgs)
        'Guardar_Complementos()
    End Sub
    Private Sub Cargar_Pol_Modelo(ByVal tabla As String, ByVal rfc As String)
        Dim sql As String = ""
        If tabla = "Facturas" Then

        ElseIf tabla = "Complementos" Then

        ElseIf tabla = "Devoluciones" Then

        End If

    End Sub
    Private Sub TablaC_KeyDown(sender As Object, e As KeyEventArgs) Handles TablaCF.KeyDown
        If e.KeyCode = Keys.E AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaCF.Item(ImpEfComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Me.TablaCF.Item(DifComplemento.Index, Me.TablaCF.CurrentRow.Index).Value
            If Me.TablaCF.Item(ImpEfComplemento.Index, Me.TablaCF.CurrentRow.Index).Value > 0 Then
                If Candado_Importe_Efectivocomple(Me.TablaCF.CurrentRow.Index) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo

                    Me.TablaCF.Item(CtaEfComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Cuenta_Efectivo()
                    Me.TablaCF.Columns(CtaEfComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    If RadMessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaCF.Item(CtaEfComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Cuenta_Efectivo()
                        Me.TablaCF.Columns(CtaEfComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaCF.Item(ImpEfComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaCF.Item(CtaEfComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = ""
            End If
        ElseIf e.KeyCode = Keys.T AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaCF.Item(ImpTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Me.TablaCF.Item(DifComplemento.Index, Me.TablaCF.CurrentRow.Index).Value
            If Me.TablaCF.Item(ImpTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value > 0 Then
                If Trim(Me.TablaCF.Item(BancoOrigenTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) <> Nothing Then
                    If Trim(Me.TablaCF.Item(CtaOTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) <> "" Then
                    Else
                        Dim posi As Integer = InStr(1, Me.TablaCF.Item(BancoOrigenTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                        Dim cuantos As Integer = Len(Me.TablaCF.Item(BancoOrigenTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) - Len(Me.TablaCF.Item(BancoOrigenTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.Substring(0, posi))
                        Dim Al As String = Me.TablaCF.Item(BancoOrigenTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.Substring(posi, cuantos)
                        Me.TablaCF.Item(CtaOTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)

                        Me.TablaCF.Item(CtaBancosComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                    End If

                End If

                'Empieza el nuevo codigo Bnaco destino
                If Trim(Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) <> Nothing Then
                    Try
                        If Trim(Me.TablaCF.Item(CtaDTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) <> "" And Trim(Me.TablaCF.Item(CtaDTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) <> "0" Then
                            Dim largo As Integer = Len(Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)
                            Dim cadena As String = Trim(Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.ToString.Substring(largo - 3, 3))
                            largo = Len(Me.TablaCF.Item(CtaDTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)
                            Dim Al As String = Me.TablaCF.Item(CtaDTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.Substring(largo - 3, 3)
                            If cadena <> Al Then
                                largo = InStr(1, Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                Al = Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.Substring(0, largo - 1)
                                Me.TablaCF.Item(CtaDTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                            End If
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.Substring(0, posi - 1)
                            Me.TablaCF.Item(CtaDTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value & "'", True)
                        End If
                    Catch ex As Exception

                    End Try
                Else
                    Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)
                    Try
                        If Trim(Me.TablaCF.Item(CtaDTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) <> "" And Trim(Me.TablaCF.Item(CtaDTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) <> "0" Then
                            Dim largo As Integer = Len(Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)
                            Dim cadena As String = Trim(Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.ToString.Substring(largo - 3, 3))
                            largo = Len(Me.TablaCF.Item(CtaDTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)
                            Dim Al As String = Me.TablaCF.Item(CtaDTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.Substring(largo - 3, 3)
                            If cadena <> Al Then
                                largo = InStr(1, Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                Al = Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.Substring(0, largo - 1)
                                Me.TablaCF.Item(CtaDTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                            End If
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.Substring(0, posi - 1)
                            Me.TablaCF.Item(CtaDTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value & "'", True)
                        End If
                    Catch ex As Exception

                    End Try
                End If
                If Trim(Me.TablaCF.Item(TipPolComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) <> Nothing Then
                    If Trim(Me.TablaCF.Item(BancoOrigenTComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) <> Nothing Then
                    Else
                        Dim fila As DataGridViewRow = Me.TablaCF.Rows(Me.TablaCF.CurrentRow.Index)
                        Try
                            If Trim(Me.TablaCF.Item(TipPolComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) <> "" Then
                                Dim largo As Integer = Len(Me.TablaCF.Item(TipPolComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)
                                Dim posicion As Integer = InStr(1, Me.TablaCF.Item(TipPolComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaCF.Item(TipPolComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.Substring(posicion + 1, largo - posicion - 1)
                                Dim indice As Integer = Obtener_indexbancoO(Al)
                                If indice = 1000 Then
                                Else
                                    fila.Cells(BancoOrigenTComplemento.Index).Value = Me.BancoOrigenTComplemento.Items(indice)
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                End If

                'termina


                If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                    Me.TablaCF.Columns(BancoOrigenTComplemento.Index).Visible = False
                    Me.TablaCF.Columns(CtaOTComplemento.Index).Visible = False
                    Me.TablaCF.Columns(BancoDtComplemento.Index).Visible = False
                    Me.TablaCF.Columns(CtaDTComplemento.Index).Visible = False
                    Me.TablaCF.Columns(FTComplemento.Index).Visible = True

                End If


            End If

        ElseIf e.KeyCode = Keys.C AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaCF.Item(ImpChComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Me.TablaCF.Item(DifComplemento.Index, Me.TablaCF.CurrentRow.Index).Value
            If Me.TablaCF.Item(ImpChComplemento.Index, Me.TablaCF.CurrentRow.Index).Value > 0 Then
                'Cheques

                If Trim(Me.TablaCF.Item(BancoCHComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) <> "" Then
                    If Trim(Me.TablaCF.Item(CtaOchComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) <> "" Then
                    Else
                        Dim posi As Integer = InStr(1, Me.TablaCF.Item(BancoCHComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                        Dim cuantos As Integer = Len(Me.TablaCF.Item(BancoCHComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) - Len(Me.TablaCF.Item(BancoCHComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.Substring(0, posi))
                        Dim Al As String = Me.TablaCF.Item(BancoCHComplemento.Index, Me.TablaCF.CurrentRow.Index).Value.Substring(posi, cuantos)
                        Me.TablaCF.Item(CtaOchComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                        Me.TablaCF.Item(CtaChequeC.Index, Me.TablaCF.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                    End If
                Else
                    ' Cargar_bancosComple("Ch")
                End If
                'Se Muestran Columnas de Contabilidad Electronica
                If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                    Me.TablaCF.Columns(BancoCHComplemento.Index).Visible = False
                    Me.TablaCF.Columns(CtaOchComplemento.Index).Visible = False
                    Me.TablaCF.Columns(NoChComplemento.Index).Visible = False
                    Me.TablaCF.Columns(FCHComplemento.Index).Visible = True
                End If

            End If


        ElseIf e.KeyCode = Keys.A AndAlso e.Modifiers = Keys.Shift Then ' anticipos

            Me.TablaCF.Item(AntiComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Me.TablaCF.Item(DifComplemento.Index, Me.TablaCF.CurrentRow.Index).Value
            If Me.TablaCF.Item(AntiComplemento.Index, Me.TablaCF.CurrentRow.Index).Value > 0 Then

                Me.TablaCF.Item(CtaAntiComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Cuenta_Anticipo()
                Me.TablaCF.Columns(CtaAntiComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Else
                Me.TablaCF.Item(CtaAntiComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = ""
            End If
        ElseIf e.KeyCode = Keys.J AndAlso e.Modifiers = Keys.Shift Then ' Ajustes
            Me.TablaCF.Item(AjusComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Me.TablaCF.Item(DifComplemento.Index, Me.TablaCF.CurrentRow.Index).Value
            If Me.TablaCF.Item(AjusComplemento.Index, Me.TablaCF.CurrentRow.Index).Value <> 0 Then
                If Candado_Importe_Efectivocomple(Me.TablaCF.CurrentRow.Index) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo
                    Me.TablaCF.Item(CtaAjusComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Cuenta_Efectivo()
                    Me.TablaCF.Columns(CtaAjusComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    If RadMessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaCF.Item(CtaAjusComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Cuenta_Efectivo()
                        Me.TablaCF.Columns(CtaAjusComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaCF.Item(AjusComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaCF.Item(CtaAjusComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = ""
            End If
        ElseIf e.KeyCode = Keys.P AndAlso e.Modifiers = Keys.Shift Then ' Provision
            Me.TablaCF.Item(ImpProviComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = Me.TablaCF.Item(DifComplemento.Index, Me.TablaCF.CurrentRow.Index).Value
        End If
        Liberar_ProcesoComple(Me.TablaCF.CurrentRow.Index)
    End Sub
    Private Function Banco_destino(ByVal Cliente As Integer, ByVal Rfc As String)
        Dim banco As String = ""
        Dim sql2 As String = " SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Cliente & ") and RFC = '" & Rfc & "' and Favorito=1"
        Dim ds2 As DataSet = Eventos.Obtener_DS(sql2)

        If ds2.Tables(0).Rows.Count > 0 Then
            Dim largo As Integer = Len(ds2.Tables(0).Rows(0)("Clabe"))
            Try
                banco = Trim(ds2.Tables(0).Rows(0)("Banco")) & " " & Trim(ds2.Tables(0).Rows(0)("Clabe").ToString.Substring(largo - 3, 3))
            Catch ex As Exception

            End Try
        Else
            banco = ""
        End If

        Return banco
    End Function
    Private Function Cuenta_Efectivo()
        Dim cuenta As String
        Dim sql As String = " Select rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1010' and Nivel2 > 0 and Nivel3 > 1 "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            cuenta = ds.Tables(0).Rows(0)("Cuenta")
        Else
            cuenta = ""
        End If
        Return cuenta
    End Function
    Private Function Cuenta_Anticipo()
        Dim cuenta As String
        Dim ds As DataSet
        If Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value <> Nothing Then
            If Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = "C" Or Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = "CPP" Then
                ds = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1200' and Nivel2 > 0   ORDER BY cta")
            ElseIf Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = "GG" Or Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = "GGPP" Then
                ds = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1210' and Nivel2 > 0   ORDER BY cta")
            End If
        End If

        If ds.Tables(0).Rows.Count > 0 Then
            cuenta = ds.Tables(0).Rows(0)("Cuenta")
        Else
            cuenta = ""
        End If
        Return cuenta
    End Function
    Private Function Candado_Importe_Efectivocomple(ByVal i As Integer)
        Dim hacer As Boolean
        If Me.TablaCF.Item(ImpEComplemento.Index, i).Value > 2000 Then

            If activo = True Then
                hacer = True
            Else
                hacer = False
            End If
        Else
            hacer = True
        End If
        Return hacer
    End Function
    Private Sub Actualiza_Registro_Xml_Comple(ByVal carg As String, ByVal registro As Integer)
        Dim sql As String = " UPDATE dbo.Parcialidades
                        SET Nombre_cuenta = '" & carg & "'
                        WHERE Id_Parcialidad = " & registro & "  "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Carga", sql)
        End If

    End Sub

    Private Sub Parcialidades_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_clientes()
        Eventos.DiseñoTablaEnca(Me.TablaCF)
    End Sub
    Private Sub Cargar_clientes()

        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        'Me.lstCliente.SelectText = ""
        Me.lstCliente.SelectItem = Inicio.Clt

    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        If Me.TablaCF.CurrentCell.ColumnIndex = Me.NomCtaComplemento.Index Then 'Cuentas del Cargo
            Dim ds As DataSet = Eventos.Obtener_DS("select Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta,Rtrim(Descripcion) as DES from catalogo_de_cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and clave= '" & Trim(Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) & "' and nivel3 >0  and rfc= '" & Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value & "' order by cta")
            Dim actividad(,) As String
            ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim cadena As String = ds.Tables(0).Rows(i)("Cuenta")
                Dim posi As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                Dim cuantos As Integer = Len(cadena) - Len(cadena.Substring(0, posi))
                Dim consecutivo As String = cadena.Substring(posi, cuantos)
                Dim Sql As String = " select Rtrim(Descripcion) as Cuenta from catalogo_de_cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and  Nivel1= '" & consecutivo.ToString.Substring(0, 4) & "' and nivel2= '" & consecutivo.ToString.Substring(4, 4) & "'  and nivel3 ='" & consecutivo.ToString.Substring(8, 4) & "'  and nivel4= '0000'"
                Dim ds2 As DataSet = Eventos.Obtener_DS(Sql)
                If Trim(ds.Tables(0).Rows(i)("DES")) = Trim(ds2.Tables(0).Rows(0)("Cuenta")) Then
                    actividad(0, i) = ds.Tables(0).Rows(i)("Cuenta")
                Else
                    actividad(0, i) = ds2.Tables(0).Rows(0)("Cuenta") & " " & ds.Tables(0).Rows(i)(0)
                End If
                'actividad(0, i) = ds2.Tables(0).Rows(0)("Cuenta") & " " & ds.Tables(0).Rows(i)(0)
                Debug.Print(ds.Tables(0).Rows(i)(0))
                actividad(1, i) = "0"
            Next
            With My.Forms.DialogUnaSeleccion
                .limpiar()
                .Titulo = Eventos.titulo_app
                .Texto = "Selecciona la Cuenta:"
                .MinSeleccion = 1
                .MaxSeleccion = 1
                .elementos = actividad
                .ShowDialog()
                actividad = .elementos
                If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            End With

            Dim descrip As String = ""
            For i As Integer = 0 To actividad.GetLength(1)
                If actividad(1, i) = "1" Then
                    descrip = actividad(0, i)
                    Exit For
                End If
            Next
            Me.TablaCF.Item(NomCtaComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = descrip

        ElseIf Me.TablaCF.CurrentCell.ColumnIndex = CtaEfComplemento.Index Then 'Cuentas del Efectivo

            Dim ds As DataSet = Eventos.Obtener_DS("Select rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta, cuenta as cta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1010' and Nivel2 > 0 and Nivel3 > 0 order by cta ")
            Dim actividad(,) As String
            ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                actividad(0, i) = ds.Tables(0).Rows(i)(0)
                Debug.Print(ds.Tables(0).Rows(i)(0))
                actividad(1, i) = "0"
            Next
            With My.Forms.DialogUnaSeleccion
                .limpiar()
                .Titulo = Eventos.titulo_app
                .Texto = "Selecciona la Cuenta para el Efectivo:"
                .MinSeleccion = 1
                .MaxSeleccion = 1
                .elementos = actividad
                .ShowDialog()
                actividad = .elementos
                If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            End With
            Dim descrip As String = ""
            For i As Integer = 0 To actividad.GetLength(1)
                If actividad(1, i) = "1" Then
                    descrip = actividad(0, i)
                    Exit For
                End If
            Next
            Me.TablaCF.Item(CtaEfComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = descrip

        ElseIf Me.TablaCF.CurrentCell.ColumnIndex = CtaAntiComplemento.Index Then 'Cuentas del Anticipos
            Dim ds As DataSet
            If Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value <> Nothing Then
                If Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = "C" Or Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = "CPP" Then
                    ds = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and Nivel1 ='1200' and Nivel2 > 0   ORDER BY cta")
                ElseIf Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = "GG" Or Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = "GGPP" Then
                    ds = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and Nivel1 ='1210' and Nivel2 > 0   ORDER BY cta")
                End If
            End If

            Dim actividad(,) As String
            ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                actividad(0, i) = ds.Tables(0).Rows(i)(0)
                Debug.Print(ds.Tables(0).Rows(i)(0))
                actividad(1, i) = "0"
            Next
            With My.Forms.DialogUnaSeleccion
                .limpiar()
                .Titulo = Eventos.titulo_app
                .Texto = "Selecciona la Cuenta para el Anticipos:"
                .MinSeleccion = 1
                .MaxSeleccion = 1
                .elementos = actividad
                .ShowDialog()
                actividad = .elementos
                If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            End With
            Dim descrip As String = ""
            For i As Integer = 0 To actividad.GetLength(1)
                If actividad(1, i) = "1" Then
                    descrip = actividad(0, i)
                    Exit For
                End If
            Next
            Me.TablaCF.Item(CtaAntiComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = descrip

        ElseIf Me.TablaCF.CurrentCell.ColumnIndex = CtaAjusComplemento.Index Then 'Cuentas del Ajuste

            Dim ds As DataSet = Eventos.Obtener_DS("Select rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta, cuenta as cta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " order by cta")
            Dim actividad(,) As String
            ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                actividad(0, i) = ds.Tables(0).Rows(i)(0)
                Debug.Print(ds.Tables(0).Rows(i)(0))
                actividad(1, i) = "0"
            Next
            With My.Forms.DialogUnaSeleccion
                .limpiar()
                .Titulo = Eventos.titulo_app
                .Texto = "Selecciona la Cuenta para el Ajuste:"
                .MinSeleccion = 1
                .MaxSeleccion = 1
                .elementos = actividad
                .ShowDialog()
                actividad = .elementos
                If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            End With
            Dim descrip As String = ""
            For i As Integer = 0 To actividad.GetLength(1)
                If actividad(1, i) = "1" Then
                    descrip = actividad(0, i)
                    Exit For
                End If
            Next
            Me.TablaCF.Item(CtaAjusComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = descrip
        End If
    End Sub
    Private Function Buscar_Madre(ByVal Cta As String)
        Dim Madre As String = ""
        If Cta.Substring(12, 4) = "0000" And Cta.Substring(8, 4) > "0000" Then ' Segundo nivel
            Dim Cuenta As String = " select Descripcion from Catalogo_de_Cuentas where Nivel1 = '" & Cta.Substring(0, 4) & "' AND Nivel2 = '" & Cta.Substring(4, 4) & "' AND Nivel3 = '0000' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
            Dim ds As DataSet = Eventos.Obtener_DS(Cuenta)
            If ds.Tables(0).Rows.Count > 0 Then
                Madre = Trim(ds.Tables(0).Rows(0)(0))
            End If
        ElseIf Cta.Substring(12, 4) > "0000" And Cta.Substring(8, 4) > "0000" Then 'Tercer Nivel
            Dim Cuenta As String = " select Descripcion from Catalogo_de_Cuentas where Nivel1 = '" & Cta.Substring(0, 4) & "' AND Nivel2 = '" & Cta.Substring(4, 4) & "' AND Nivel3 = '" & Cta.Substring(8, 4) & "' AND Nivel4 = '0000' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
            Dim ds As DataSet = Eventos.Obtener_DS(Cuenta)
            If ds.Tables(0).Rows.Count > 0 Then
                Madre = Trim(ds.Tables(0).Rows(0)(0))
            End If
        ElseIf Cta.Substring(4, 4) > "0000" And Cta.Substring(8, 4) = "0000" And Cta.Substring(12, 4) = "0000" Then 'Primer Nivel
            Dim Cuenta As String = " select Descripcion from Catalogo_de_Cuentas where Nivel1 = '" & Cta.Substring(0, 4) & "' AND Nivel2 > '0000' AND Nivel3='0000' AND Nivel4 = '0000' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
            Dim ds As DataSet = Eventos.Obtener_DS(Cuenta)
            If ds.Tables(0).Rows.Count > 0 Then
                Madre = Trim(ds.Tables(0).Rows(0)(0))
            End If
        Else

        End If
        Return Madre
    End Function
    Private Function CuentasMadres(ByVal Clave As String, ByVal Fila As Integer, ByVal P As Decimal, ByVal t As Decimal, ByVal g As Decimal, ByVal e As Decimal) As DataSet
        Dim Tipo As String = " "
        Dim Sql As String = ""
        Dim Cuenta As String

        If Convert.ToDecimal(P) = Convert.ToDecimal(t) Then
            If Convert.ToDecimal(e) > Convert.ToDecimal(g) Then
                Tipo = " ExentoPPD, NivelEP "
            Else
                Tipo = " GravadoPPD, NivelGP "
            End If
        Else
            If Convert.ToDecimal(e) > Convert.ToDecimal(g) Then
                Tipo = " ExentoPUE, NivelE "
            Else
                Tipo = " GravadoPUE, NivelG "
            End If
        End If
        Sql = " SELECT " & Tipo & "   FROM  ClaveEgresos WHERE ClaveEgresos.Id_Empresa  = " & Me.lstCliente.SelectItem & " and ClaveEgresos.Clave = '" & Clave & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) > "0000" Then
                Tipo = "  SUBSTRING(ClaveEgresos.GravadoPUE, 1, 4) = Catalogo_de_Cuentas.Nivel1 AND SUBSTRING(ClaveEgresos.GravadoPUE, 5, 4) = Catalogo_de_Cuentas.Nivel2 AND SUBSTRING(ClaveEgresos.GravadoPUE,9, 4) = Catalogo_de_Cuentas.Nivel3 "
            ElseIf ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(4, 4) > "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) = "0000" Then 'Tercer Nivel
                Tipo = "  SUBSTRING(ClaveEgresos.GravadoPUE, 1, 4) = Catalogo_de_Cuentas.Nivel1 AND SUBSTRING(ClaveEgresos.GravadoPUE, 5, 4) = Catalogo_de_Cuentas.Nivel2   "
            ElseIf ds.Tables(0).Rows(0)(0).ToString().Substring(4, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" Then 'Primer Nivel
                Tipo = "  SUBSTRING(ClaveEgresos.GravadoPUE, 1, 4) = Catalogo_de_Cuentas.Nivel1 "
            End If
            Sql = "SELECT DISTINCT Rtrim(Catalogo_de_Cuentas.Descripcion) + '-'+ convert(nvarchar,Catalogo_de_Cuentas.cuenta,103) as Cuenta"
            Sql &= " FROM     ClaveEgresos INNER JOIN "
            Sql &= " Catalogo_de_Cuentas ON " & Tipo & " WHERE Catalogo_de_Cuentas.Nivel4= 0 and  (ClaveEgresos.Id_Empresa = " & Me.lstCliente.SelectItem & ") AND (ClaveEgresos.Clave = '" & Clave & "')   "
            ds.Clear()
            ds = Eventos.Obtener_DS(Sql)
        End If
        Return ds
    End Function
    Private Sub CrearCuentaCargoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CrearCuentaCargoToolStripMenuItem.Click
        Dim leyenda As String = ""
        Dim dsconta As DataSet
        Dim columna As Integer = Me.TablaCF.CurrentCell.ColumnIndex
        Dim Nombre As String
        Nombre = Me.TablaCF.Columns.Item(Me.TablaCF.CurrentCell.ColumnIndex).Name.ToString
        Me.LstComple.Cargar(" Select '','' ")

        Select Case Nombre
            Case "LetraContabilidadComplemento"
                dsconta = CuentasMadres(Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.CurrentRow.Index, Me.TablaCF.Item(ImpProviComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.Item(TotComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.Item(ImpGComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.Item(ImpEComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)
                leyenda = Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value
            Case "NomCtaComplemento"
                dsconta = CuentasMadres(Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.CurrentRow.Index, Me.TablaCF.Item(ImpProviComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.Item(TotComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.Item(ImpGComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.Item(ImpEComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)
                leyenda = Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value
            Case "CtaEfComplemento"
                dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1010' and Nivel2 > 0 ORDER BY cta")
                leyenda = " Efectivo"
            Case "CtaBancosComplemento"
                dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1020' and Nivel2 > 0 ORDER BY cta")
                leyenda = " Bancos"
            Case "CtaAjusComplemento"
                dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & "   ORDER BY cta")
                leyenda = " Ajustes"
            Case "CtaAntiComplemento"

                If Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value <> Nothing Then
                    If Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = "C" Or Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = "CPP" Then
                        dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1200' and Nivel2 > 0   ORDER BY cta")
                        leyenda = " Anticipos Proveedores"
                    ElseIf Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = "GG" Or Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = "GGPP" Then
                        dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1210' and Nivel2 > 0   ORDER BY cta")
                        leyenda = " Anticipos Acreedores"
                    End If
                End If
        End Select
        Dim cuenta As String = ""
        Dim act(,) As String
        ReDim act(2, dsconta.Tables(0).Rows.Count + 1)
        For s As Integer = 0 To dsconta.Tables(0).Rows.Count - 1
            Dim C() As String = Split(dsconta.Tables(0).Rows(s)(0), "-")
            act(0, s) = Buscar_Madre(C(1)) & " " & dsconta.Tables(0).Rows(s)(0)
            Debug.Print(dsconta.Tables(0).Rows(s)(0))
            act(1, s) = "0"
        Next
        With My.Forms.DialogUnaSeleccion
            .limpiar()
            .Titulo = Eventos.titulo_app
            .Texto = "Selecciona la Cuenta Madre para " & leyenda & ":"
            .MinSeleccion = 1
            .MaxSeleccion = 1
            .elementos = act
            .ShowDialog()
            act = .elementos
            If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End With

        Dim Letra As String = ""
        For s As Integer = 0 To act.GetLength(1)
            If act(1, s) = "1" Then
                Letra = act(0, s)
                Exit For
            End If
        Next
        Dim cadena As String = Trim(Letra)
        Dim posi As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
        Dim cuantos As Integer = Len(cadena) - Len(cadena.Substring(0, posi))
        Letra = cadena.Substring(posi, cuantos)

        Dim ds As DataSet
        Dim Hijas As Integer
        Dim Nivel As Integer
        If Letra.Substring(12, 4) = "0000" And Letra.Substring(8, 4) > "0000" Then ' 
            Nivel = 4
            Hijas = 0
        ElseIf Letra.Substring(12, 4) = "0000" And Letra.Substring(4, 4) > "0000" And Letra.Substring(8, 4) > "0000" Then 'Tercer Nivel
            Nivel = 3
            Hijas = 0
        ElseIf Letra.Substring(4, 4) > "0000" And Letra.Substring(8, 4) = "0000" And Letra.Substring(12, 4) = "0000" Then 'Primer Nivel
            ds = Eventos.Obtener_DS("Select Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta from Catalogo_de_cuentas where nivel1 = '" & Letra.ToString.Substring(0, 4) & "' and Nivel2 = '" & Letra.ToString.Substring(4, 4) & "' and nivel3 > 0 and Nivel4 = '0000' and Id_Empresa = " & Me.lstCliente.SelectItem & " order by cta")
            Hijas = ds.Tables(0).Rows.Count
            Nivel = 2
        End If

        If Hijas > 0 Then
            Dim actv(,) As String
            ReDim actv(2, ds.Tables(0).Rows.Count + 1)
            For s As Integer = 0 To ds.Tables(0).Rows.Count - 1
                actv(0, s) = ds.Tables(0).Rows(s)(0)
                Debug.Print(ds.Tables(0).Rows(s)(0))
                actv(1, s) = "0"
            Next
            With My.Forms.DialogUnaSeleccion
                .limpiar()
                .Titulo = Eventos.titulo_app
                .Texto = "Selecciona la Sub Cuenta de " & cadena & ":"
                .MinSeleccion = 1
                .MaxSeleccion = 1
                .elementos = actv
                .ShowDialog()
                actv = .elementos
                If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            End With

            Dim Sub_cuenta As String = ""
            For s As Integer = 0 To actv.GetLength(1)
                If actv(1, s) = "1" Then
                    Sub_cuenta = actv(0, s)
                    Exit For
                End If
            Next
            ' Se crea cuenta de cuarto nivel
            cadena = Trim(Sub_cuenta)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Sub_cuenta = cadena.Substring(posi, cuantos)

            If Verifica_existencia4(Sub_cuenta.ToString.Substring(0, 4), Sub_cuenta.ToString.Substring(4, 4), Sub_cuenta.ToString.Substring(8, 4), Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) = True Then
                cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Sub_cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Sub_cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & Sub_cuenta.ToString.Substring(8, 4) & " AND Nivel4 >=" & Sub_cuenta.ToString.Substring(12, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                cuenta = Format(cuenta).PadLeft(4, "0")
                Crear_cuenta(Sub_cuenta.ToString.Substring(0, 4), Sub_cuenta.ToString.Substring(4, 4), Sub_cuenta.ToString.Substring(8, 4),
                                           cuenta, Sub_cuenta.Substring(0, 12) & cuenta, Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, Me.TablaCF.CurrentRow.Index).Value,
                                           Me.lstCliente.SelectItem, Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)
            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("La cuenta ya existe ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
            End If
            Me.TablaC_CellEndEdit(Me.TablaCF, Nothing)
        Else
            ' Se crea cuenta de tercer nivel
            If Verifica_existencia(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value) = True Then

                Select Case Nivel
                    Case 3

                        cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & Letra.ToString.Substring(0, 4) & "  AND Nivel2 =" & Letra.ToString.Substring(4, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                        cuenta = Format(cuenta).PadLeft(4, "0")
                        Crear_cuenta(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), cuenta,
                                       "0000", Letra.Substring(0, 8) & cuenta & "0000", Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, Me.TablaCF.CurrentRow.Index).Value,
                                       Me.lstCliente.SelectItem, Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)

                    Case 4

                        cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Letra.ToString.Substring(0, 4) & "  AND Nivel2 =" & Letra.ToString.Substring(4, 4) & " AND Nivel3 =" & Letra.ToString.Substring(8, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                        cuenta = Format(cuenta).PadLeft(4, "0")
                        Crear_cuenta(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), Letra.ToString.Substring(8, 4),
                                       cuenta, Letra.Substring(0, 12) & cuenta, Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, Me.TablaCF.CurrentRow.Index).Value,
                                       Me.lstCliente.SelectItem, Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)


                    Case 2

                        cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & Letra.ToString.Substring(0, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                        cuenta = Format(cuenta).PadLeft(4, "0")
                        Crear_cuenta(Letra.ToString.Substring(0, 4), cuenta, "0000",
                                       "0000", Letra.Substring(0, 4) & cuenta & "00000000", Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value & " " & Me.TablaCF.Item(NEmComplemento.Index, Me.TablaCF.CurrentRow.Index).Value,
                                       Me.lstCliente.SelectItem, Me.TablaCF.Item(LetraContabilidadComplemento.Index, Me.TablaCF.CurrentRow.Index).Value, Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)

                End Select


            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("La cuenta ya existe ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
            End If
            Me.TablaC_CellEndEdit(Me.TablaCF, Nothing)

        End If
    End Sub

    Private Function Verifica_existencia(ByVal Nivel1 As String, ByVal Nivel2 As String, ByVal clave As String, ByVal rfc As String)
        Dim hacer As Boolean
        Dim sql As String = " Select cuenta from catalogo_De_cuentas where nivel1= '" & Nivel1 & "' and nivel2 = '" & Nivel2 & "' and clave = '" & clave & "' and rfc = '" & rfc & "' AND Id_Empresa =" & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = False
        Else
            hacer = True
        End If
        Return hacer
    End Function
    Private Function Verifica_existencia4(ByVal Nivel1 As String, ByVal Nivel2 As String, ByVal Nivel3 As String, ByVal clave As String, ByVal rfc As String)
        Dim hacer As Boolean
        Dim sql As String = " Select cuenta from catalogo_De_cuentas where nivel1= '" & Nivel1 & "' and nivel2 = '" & Nivel2 & "' and nivel3 = '" & Nivel3 & "' and clave = '" & clave & "' and rfc = '" & rfc & "' AND Id_Empresa =" & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = False
        Else
            hacer = True
        End If
        Return hacer
    End Function

    Private Sub AgregarBancosRFCsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgregarBancosRFCsToolStripMenuItem.Click
        If Me.TablaCF.CurrentCell.ColumnIndex = Me.NomCtaComplemento.Index Then 'Cuentas del Cargo

            Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Me.lstCliente.SelectItem & ") and RFC = '" & Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value & "' order by Banco")
            Dim actividad(,) As String
            ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim largo As Integer = Len(ds.Tables(0).Rows(i)("Clabe"))
                Dim cadena As String = Trim(ds.Tables(0).Rows(i)("Banco")) & " " & Trim(ds.Tables(0).Rows(i)("Clabe").ToString.Substring(largo - 3, 3))

                actividad(0, i) = cadena
                Debug.Print(Me.TablaCF.Item(RFCComplemento.Index, Me.TablaCF.CurrentRow.Index).Value)
                actividad(1, i) = "0"
            Next
            With My.Forms.DialogUnaSeleccion
                .limpiar()
                .Titulo = Eventos.titulo_app
                .Texto = "Selecciona el Banco"
                .MinSeleccion = 1
                .MaxSeleccion = 1
                .elementos = actividad
                .ShowDialog()
                actividad = .elementos
                If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                    Exit Sub
                End If
            End With

            Dim descrip As String = ""
            For i As Integer = 0 To actividad.GetLength(1)
                If actividad(1, i) = "1" Then
                    descrip = actividad(0, i)
                    Exit For
                End If
            Next
            Me.TablaCF.Item(BancoDtComplemento.Index, Me.TablaCF.CurrentRow.Index).Value = descrip
            Me.TablaC_CellEndEdit(Me.TablaCF, Nothing)

        End If
    End Sub



    Private Sub SP1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP1.DoWork
        Complemento(Compl)
        Color_ColumnasC()
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Verificando informacion de pagos por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.TablaCF.Rows.Count
        For i As Integer = 0 To Me.TablaCF.Rows.Count - 1
            Liberar_ProcesoComple(i)
            frm.Barra.Value = i
        Next
        frm.Close()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
    End Sub

    Private Sub TablaC_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaCF.DataError

    End Sub

    Private Sub TablaCF_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaCF.CellContentClick

        Dim columna As Integer = Me.TablaCF.CurrentCell.ColumnIndex
        Dim Nombre As String
        Nombre = Me.TablaCF.Columns.Item(Me.TablaCF.CurrentCell.ColumnIndex).Name.ToString
        Me.LstComple.Cargar(" Select '','' ")

        Select Case Nombre
            Case "LetraContabilidadComplemento"
                ' Me.LstTextoDev.Cargar(" Select id_Contabilidad, rtrim(Clave) as Clave from Letras_Contabilidad where Id_Empresa = " & Me.lstCliente.SelectItem & " ")
                Me.LstComple.Cargar(" Select Id_Clave, rtrim(Clave) as Clave from ClaveEgresos where Id_Empresa = " & Me.lstCliente.SelectItem & " ")
                Me.LstComple.SelectText = ""
            Case "CtaEfComplemento"
                Me.LstComple.Cargar(" Select Id_cat_cuentas, rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1010' and Nivel2 > 0 and Nivel3 > 0 ")
                Me.LstComple.SelectText = ""
            Case "CtaDTComplemento"
                Me.LstComple.Cargar(" Select Id_cat_cuentas, rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1020'and Nivel2 > 0 and Nivel3 > 0  ")
                Me.LstComple.SelectText = ""
            Case "BancoOrigenTComplemento"
                'Me.LstComple.Cargar(" select Id_Bancos_Clientes,Alias from Bancos_Clientes where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%Transf%'")
                'Me.LstComple.SelectText = ""
                Me.LstComple.Cargar("SELECT rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias ,  rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Ali  FROM Bancos_Clientes INNER JOIN Bancos ON Bancos_Clientes.Id_Banco =Bancos.Id_Banco  where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%Cheq%' ")
                Me.LstComple.SelectText = ""
            Case "BancoCHComplemento"
                'Me.LstComple.Cargar(" select Id_Bancos_Clientes,Alias from Bancos_Clientes where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%Transf%'")
                'Me.LstComple.SelectText = ""
                Me.LstComple.Cargar("SELECT rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias ,  rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Ali  FROM Bancos_Clientes INNER JOIN Bancos ON Bancos_Clientes.Id_Banco =Bancos.Id_Banco  where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%Transf%' ")
                Me.LstComple.SelectText = ""
            Case "TipPolComplemento"
                Me.LstComple.Cargar(" Select Id_Tipo_Pol_Sat,convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
                Me.LstComple.SelectText = ""
            Case "CtaAjusComplemento"
                Me.LstComple.Cargar("Select  Id_cat_cuentas,  rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " ")
                Me.LstComple.SelectText = ""
            Case "CtaAntiComplemento"
                Me.LstComple.Cargar("Select Id_cat_cuentas,  rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1070' and Nivel2 > 0 and Nivel3 > 0 ")
                Me.LstComple.SelectText = ""
        End Select
    End Sub
End Class