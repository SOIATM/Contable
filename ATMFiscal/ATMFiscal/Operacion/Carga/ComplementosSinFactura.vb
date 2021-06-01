Imports Telerik.WinControls
Public Class ComplementosSinFactura
    Dim Activo As Boolean
    Dim Compl As DataSet
    Dim serV As String = My.Forms.Inicio.txtServerDB.Text
    Private Sub CmdSalirComple_Click(sender As Object, e As EventArgs) Handles CmdSalirComple.Click
        Me.Close()
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
            RadMessageBox.Show("No hay registros de parametros para la empresa " & Me.lstCliente.SelectText & " ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            hacer = False
        End If
        Return hacer
    End Function
    Private Sub CmdLimpiaComple_Click(sender As Object, e As EventArgs) Handles CmdLimpiaComple.Click
        LimpiaC()
    End Sub
    Private Sub LimpiaC()
        If Me.TablaC.RowCount > 0 Then
            Me.TablaC.Rows.Clear()
        End If
    End Sub
    Private Sub CmdBuscarComple_Click(sender As Object, e As EventArgs) Handles CmdBuscarComple.Click
        Activo = True
        LimpiaC()
        If Me.lstCliente.SelectText <> "" Then
            If Buscar_Parametros(Me.lstCliente.SelectItem) = True Then
                Try
                    Compl.Clear()
                Catch ex As Exception
                End Try
                Buscar_Complementos(Me.lstCliente.SelectItem, " and Fecha_Emision >= " & Eventos.Sql_hoy(Me.DtFiComple.Value) & " and Fecha_Emision <= " & Eventos.Sql_hoy(Me.Dtfin.Value) & "")
                SPComp.RunWorkerAsync(Me.TablaC)
                Control.CheckForIllegalCrossThreadCalls = False
                Me.TablaC.Enabled = True
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
        Activo = False
    End Sub

    Private Sub ComplementosSinFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_Clientes()
        Eventos.DiseñoTablaEnca(Me.TablaC)
    End Sub
    Private Sub Cargar_Clientes()
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
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
        sql &= "    FROM dbo.Xml_Sat inner join Metodos_de_Pago on Metodos_de_Pago.clave = Xml_Sat.FormaDePago   where  (Xml_Sat.Emitidas = 1) AND (Xml_Sat.Id_Empresa = " & Id_Empresa & ") " & periodo & " AND Xml_Sat.Imp_Provision >0 AND Xml_Sat.Id_Poliza IS NOT NULL AND (Tiene_Comple = 0 OR Tiene_Comple IS NULL )"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaC.RowCount = 1
            Dim contab As DataSet = Eventos.Obtener_DS(" Select 'I' as Clave   ")
            If contab.Tables(0).Rows.Count > 0 Then
                If Me.LetraContabilidadComplemento.Items.Count = 0 Then

                    For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                        Me.LetraContabilidadComplemento.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                    Next
                Else
                    Me.LetraContabilidadComplemento.Items.Clear()
                    For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                        Me.LetraContabilidadComplemento.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                    Next
                End If
            End If
            Dim Tipo As DataSet = Eventos.Obtener_DS(" Select convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat INNER JOIN Tipo_Poliza ON Tipo_Poliza.Id_Tipo_poliza = Tipos_Poliza_Sat.Id_Tipo_poliza  where Id_Empresa= " & Me.lstCliente.SelectItem & "      ")
            If Tipo.Tables(0).Rows.Count > 0 Then
                If Me.TipPolComplemento.Items.Count = 0 Then
                    For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                        Me.TipPolComplemento.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                    Next
                Else
                    Me.TipPolComplemento.Items.Clear()
                    For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                        Me.TipPolComplemento.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                    Next
                End If
            End If
            Dim Ctaor As DataSet = Eventos.Obtener_DS(" Select 'SI' as Clave  UNION sELECT 'NO' as Clave")
            If Ctaor.Tables(0).Rows.Count > 0 Then
                If Me.CtaOrdenC.Items.Count = 0 Then
                    For i As Integer = 0 To Ctaor.Tables(0).Rows.Count - 1
                        Me.CtaOrdenC.Items.Add(Trim(Ctaor.Tables(0).Rows(i)("Clave")))
                    Next
                Else
                    Me.CtaOrdenC.Items.Clear()
                    For i As Integer = 0 To Ctaor.Tables(0).Rows.Count - 1
                        Me.CtaOrdenC.Items.Add(Trim(Ctaor.Tables(0).Rows(i)("Clave")))
                    Next
                End If
            End If
            Dim Series As DataSet = Eventos.Obtener_DS(" SELECT Serie FROM dbo.Series_Clientes WHERE Id_Empresa = " & Me.lstCliente.SelectItem & " ")
            If Series.Tables(0).Rows.Count > 0 Then
                If Me.SerC.Items.Count = 0 Then
                    For i As Integer = 0 To Series.Tables(0).Rows.Count - 1
                        Me.SerC.Items.Add(Trim(Series.Tables(0).Rows(i)("Serie")))
                    Next
                Else
                    Me.SerC.Items.Clear()
                    For i As Integer = 0 To Series.Tables(0).Rows.Count - 1
                        Me.SerC.Items.Add(Trim(Series.Tables(0).Rows(i)("Serie")))
                    Next
                End If
            End If
            Cargar_bancosComple("Transf")
            Cargar_bancosComple("Ch")
            Me.TablaC.RowCount = ds.Tables(0).Rows.Count
            Compl = ds
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay registros para procesar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If



    End Sub
    Private Sub Cargar_bancosComple(ByVal bancos As String)
        Dim sql As String = " SELECT rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias FROM Bancos_Clientes INNER JOIN Bancos ON Bancos_Clientes.Id_Banco =Bancos.Id_Banco  where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%" & bancos & "%'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If bancos = "Ch" Then
                'Cheque
                If Me.BancoDEC.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancoDEC.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                Else
                    Try
                        Me.BancoDEC.Items.Clear()
                    Catch ex As Exception

                    End Try

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancoDEC.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                End If
            Else
                'Transferencia
                'Origen
                If Me.BancoDtComplemento.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancoDtComplemento.Items.Add(ds.Tables(0).Rows(i)("Alias"))

                    Next
                Else
                    Try
                        Me.BancoDtComplemento.Items.Clear()
                    Catch ex As Exception

                    End Try


                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancoDtComplemento.Items.Add(ds.Tables(0).Rows(i)("Alias"))

                    Next
                End If

            End If
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
        frm.Barra.Maximum = Me.TablaC.Rows.Count
        For j As Integer = 0 To Ds.Tables(0).Rows.Count - 1
            Dim Fila As DataGridViewRow = Me.TablaC.Rows(j)
            Me.TablaC.Item(0, j).Value = False
            Me.TablaC.Item(ApliComplemento.Index, j).Value = False
            Me.TablaC.Item(Id_Complemento.Index, j).Value = Ds.Tables(0).Rows(j)("Id_Registro_Xml")

            Me.TablaC.Item(FechaEComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("Fecha_Emision").ToString()
            Me.TablaC.Item(UUIDComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("UUID")
            Me.TablaC.Item(UUIDRComplemento.Index, j).Value = IIf(IsDBNull(Ds.Tables(0).Rows(j)("UUID_Relacion")) = True, "", Ds.Tables(0).Rows(j)("UUID_Relacion"))
            Me.TablaC.Item(UCFDIComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("UsoCFDI")
            Me.TablaC.Item(RFCComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("RFC_Emisor")
            Me.TablaC.Item(NEmComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("Nombre_Emisor")
            Me.TablaC.Item(Concepto.Index, j).Value = Ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaC.Item(MPc.Index, j).Value = Ds.Tables(0).Rows(j)("Metodo_de_Pago")
            'Me.TablaC.Item(FpagoComplemento.Index, j).Value = Trim(Ds.Tables(0).Rows(j)("Forma_de_Pago"))
            Try
                Me.TablaC.Item(ClaveRef.Index, j).Value = ImpFaCURA(Me.TablaC.Item(UUIDComplemento.Index, j).Value.ToString().Trim(), "Clave").ToString().Trim()
            Catch ex As Exception

            End Try
            Dim PorcenPro As Decimal = 0 ' Calcula valores de la factura
            Try
                PorcenPro = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, j).Value, "Total_Real")
            Catch ex As Exception
                PorcenPro = 0
            End Try

            Me.TablaC.Item(SubTotComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("SubTotal")
            Me.TablaC.Item(IvaComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("IVA_16")
            Me.TablaC.Item(TotComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("Total")
            Me.TablaC.Item(LetraSatComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("Letra_Sat")
            Dim year As String = Ds.Tables(0).Rows(j)("Fecha_Emision").ToString.Substring(6, 4)
            Dim month As String = Ds.Tables(0).Rows(j)("Fecha_Emision").ToString.Substring(3, 2)
            Me.TablaC.Item(AnioComplemento.Index, j).Value = IIf(IsDBNull(Ds.Tables(0).Rows(j)("Anio_Contable")) = True, year, Ds.Tables(0).Rows(j)("Anio_Contable"))
            Me.TablaC.Item(MesComplemento.Index, j).Value = IIf(IsDBNull(Ds.Tables(0).Rows(j)("Mes_Contable")) = True, month, Ds.Tables(0).Rows(j)("Mes_Contable"))
            Me.TablaC.Item(MoneComplemento.Index, j).Value = Ds.Tables(0).Rows(j)("Moneda")


            frm.Barra.Value = j
        Next
        frm.Close()
    End Sub
    Private Function ImpFaCURA(ByVal uuid As String, ByVal CAMPO As String)
        My.Forms.Inicio.txtServerDB.Text = serV
        Dim Imp As Decimal = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT " & CAMPO & " FROM dbo.Xml_Sat WHERE UUID = '" & uuid & "' and Id_Empresa = " & Me.lstCliente.SelectItem & "")
        If ds.Tables(0).Rows.Count > 0 Then
            Imp = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
        Else
            Imp = 0
        End If
        Return Imp
    End Function
    Private Sub SPComp_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SPComp.DoWork
        Complemento(Compl)
        Color_ColumnasC()
    End Sub
    Private Sub Color_ColumnasC()
        Dim contador As Integer = 0
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Asignando codigo de colores por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.TablaC.Columns.Count
        For Each Column As DataGridViewColumn In TablaC.Columns
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
            ElseIf Column.Index = NomCtaComplemento.Index Or Column.Index = CtaBancosComplemento.Index Or Column.Index = CtaChequesC.Index Then
                Column.DefaultCellStyle.BackColor = Color.Orange
            ElseIf Column.Index = ImpEfComplemento.Index Or Column.Index = ImpTComplemento.Index Or Column.Index = ImpChComplemento.Index Then
                Column.DefaultCellStyle.BackColor = Color.OliveDrab
            End If
            frm.Barra.Value += 1
        Next
        frm.Close()
    End Sub
    Private Sub TablaCF_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)

    End Sub
    Private Sub TablaC_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaC.DataError

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
    Private Function Calcula_PolizaC(ByVal i As Integer)
        Dim mess As String = IIf(Len(Me.TablaC.Item(MesComplemento.Index, i).Value) = 1, "0" & Me.TablaC.Item(MesComplemento.Index, i).Value, Me.TablaC.Item(MesComplemento.Index, i).Value)
        Dim poliza As String = Eventos.Num_polizaS(Me.lstCliente.SelectItem, Checa_Tipo(Me.TablaC.Item(TipPolComplemento.Index, i).Value, Me.lstCliente.SelectItem), Me.TablaC.Item(AnioComplemento.Index, i).Value, mess, Busca_Tipificar(Me.TablaC.Item(TipPolComplemento.Index, i).Value))

        Return poliza
    End Function
    Private Function Busca_Tipificar(ByVal tipos As String)
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
    Private Function Checa_Tipo(ByVal tipo As String, ByVal cliente As Integer)
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
    Private Sub Actualiza_Factura(ByVal id As String)
        Dim SQL As String = "UPDATE dbo.Xml_Sat SET Tiene_Comple = 1 WHERE Id_Registro_Xml = " & id & " "
        If Eventos.Comando_sql(SQL) > 0 Then
            Eventos.Insertar_usuariol("COMPLE", SQL)
        End If
    End Sub
    Private Sub Actualiza_RegistroC(ByVal poliza As String, ByVal registro As Integer)
        Dim sql As String = " UPDATE dbo.Xml_Complemento
                        SET Id_Poliza = '" & poliza & "'
                        WHERE Id_Xml_Complemento = " & registro & "  "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Carga", sql)
        End If

    End Sub
    Private Sub Actualiza_Registro(ByVal poliza As String, ByVal registro As Integer)
        Dim sql As String = " UPDATE dbo.xml_sat
                        SET Id_Poliza = '" & poliza & "'
                        WHERE Id_Registro_Xml = " & registro & "  "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Carga", sql)
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
            If comple = True Then
                Actualiza_RegistroC(id_poliza, registro)
                Dim iD As String = "SELECT Xml_Sat.Id_Registro_Xml  FROM Xml_Sat WHERE Id_Empresa = " & Me.lstCliente.SelectItem & " AND UUID = (SELECT IdDocumento FROM Xml_Complemento WHERE Id_Xml_Complemento = " & registro & ")"
                Dim DS As DataSet = Eventos.Obtener_DS(iD)
                If DS.Tables(0).Rows.Count > 0 Then
                    Eventos.Actualiza_Factura(DS.Tables(0).Rows(0)(0))
                End If
            Else
                Actualiza_Registro(id_poliza, registro)
            End If
        Else
            hacer = False
        End If
        Return hacer
    End Function
    Private Function Buscafactura(ByVal Folio_Fiscal As String, ByVal detaclle As String)
        Dim hacer As Boolean
        Dim sql As String = "select * from Facturas where Folio_Fiscal = '" & Folio_Fiscal & "' and Detalle_Comp_Electronico ='" & detaclle & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = False
        Else
            hacer = True
        End If
        Return hacer
    End Function
    Private Sub Codificar_polizasComplementos(ByVal posicion As Integer)
        Dim poliza_Sistema As String = ""
        '  If Me.TablaImportar.Item(Psistema.Index, posicion).Value <> "" Then ' quitar linea despues del desbloqueo
        poliza_Sistema = Calcula_PolizaC(posicion)
        '  End If

        'Calcular consecutivo poliza
        Dim posi As Integer = InStr(1, poliza_Sistema, "-", CompareMethod.Binary)
        Dim cuantos As Integer = Len(poliza_Sistema) - Len(poliza_Sistema.Substring(0, posi))
        Dim consecutivo As Integer = Val(poliza_Sistema.Substring(posi, cuantos))
        'Crear poliza
        'Checar dia de la Poliza poner if
        Dim dia As String = ""
        If Me.TablaC.Item(ImpTComplemento.Index, posicion).Value > 0 Then
            dia = Me.TablaC.Item(FTComplemento.Index, posicion).Value.ToString.Substring(0, 2)
        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, posicion).Value > 0 Then
            dia = Me.TablaC.Item(FechaEComplemento.Index, posicion).Value.ToString.Substring(0, 2)
        ElseIf Me.TablaC.Item(ImpChComplemento.Index, posicion).Value > 0 Then
            dia = Me.TablaC.Item(FCHComplemento.Index, posicion).Value.ToString.Substring(0, 2)
        Else
            dia = Me.TablaC.Item(FechaEComplemento.Index, posicion).Value.ToString.Substring(0, 2)
        End If
        ' CREAR iF para calcular el dia
        If Creapoliza(poliza_Sistema, Me.TablaC.Item(AnioComplemento.Index, posicion).Value, Me.TablaC.Item(MesComplemento.Index, posicion).Value, dia,
                   consecutivo, Checa_Tipo(Me.TablaC.Item(TipPolComplemento.Index, posicion).Value, Me.lstCliente.SelectItem),
                   Me.TablaC.Item(FechaEComplemento.Index, posicion).Value, "Pago S/Fact" & " " & Trim(Me.TablaC.Item(UUIDComplemento.Index, posicion).Value), "Carga", Me.TablaC.Item(NumPolComplemento.Index, posicion).Value, Me.TablaC.Item(Id_Complemento.Index, posicion).Value, True) = True Then

            If Buscafactura(Me.TablaC.Item(UUIDComplemento.Index, posicion).Value, "C") = True Then
                'Se inserta la Factura
                Inserta_Comprobante_Fiscal(poliza_Sistema, Me.TablaC.Item(AnioComplemento.Index, posicion).Value, Me.TablaC.Item(MesComplemento.Index, posicion).Value,
                             Me.TablaC.Item(RFCComplemento.Index, posicion).Value, Me.TablaC.Item(FechaEComplemento.Index, posicion).Value,
                               Me.TablaC.Item(UUIDComplemento.Index, posicion).Value, "Factura " & Trim(Me.TablaC.Item(RFCComplemento.Index, posicion).Value) & " C", Me.TablaC.Item(TRComplemento.Index, posicion).Value)
            Else
                'Se Edita la Factura
                ' Edita_Factura(Me.TablaC.Item(UUIDComplemento.Index, posicion).Value, "C", poliza_Sistema)
            End If


            If Me.TablaC.Item(ImpEfComplemento.Index, posicion).Value > 0 Then
                ' Insertar registro contabiidad electronica efectivo
                Inserta_Comprobante_Fiscal_Efectivo(poliza_Sistema, Me.TablaC.Item(AnioComplemento.Index, posicion).Value, Me.TablaC.Item(MesComplemento.Index, posicion).Value,
                                Me.TablaC.Item(RFCComplemento.Index, posicion).Value, Me.TablaC.Item(TipPolComplemento.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaC.Item(FechaEComplemento.Index, posicion).Value,
                              "", "", "", "", Me.TablaC.Item(ImpEfComplemento.Index, posicion).Value)
            End If
            If Trim(UCase(Me.lblce.Text)) = "TRUE" Then ' SE INSERTA CONTABILIDAD ELECTRONICA

                If Me.TablaC.Item(ImpTComplemento.Index, posicion).Value > 0 Then
                    ' Insertar registro contabiidad electronica Transferencia
                    Dim cadena As String = Me.TablaC.Item(BancoOrigenTComplemento.Index, posicion).Value
                    Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim BO As String = cadena.Substring(0, posil - 1)

                    cadena = Me.TablaC.Item(BancoDtComplemento.Index, posicion).Value
                    posil = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim Bd As String = cadena.Substring(0, posil - 1)

                    Inserta_Comprobante_Fiscal_Transf(poliza_Sistema, Me.TablaC.Item(AnioComplemento.Index, posicion).Value, Me.TablaC.Item(MesComplemento.Index, posicion).Value,
                                    Me.TablaC.Item(RFCComplemento.Index, posicion).Value, Me.TablaC.Item(TipPolComplemento.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaC.Item(FTComplemento.Index, posicion).Value,
                                  "", BO, Me.TablaC.Item(CtaOTComplemento.Index, posicion).Value, Me.TablaC.Item(UUIDComplemento.Index, posicion).Value, Me.TablaC.Item(ImpTComplemento.Index, posicion).Value, Bd, Me.TablaC.Item(CtaDTComplemento.Index, posicion).Value)
                End If

                If Me.TablaC.Item(ImpChComplemento.Index, posicion).Value > 0 Then
                    ' Insertar registro contabiidad electronica Cheque
                    Dim cadena As String = Me.TablaC.Item(BancoCHComplemento.Index, posicion).Value
                    Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim BO As String = cadena.Substring(0, posil - 1)

                    Inserta_Comprobante_Fiscal_Cheque(poliza_Sistema, Me.TablaC.Item(AnioComplemento.Index, posicion).Value, Me.TablaC.Item(MesComplemento.Index, posicion).Value,
                                    Me.TablaC.Item(RFCComplemento.Index, posicion).Value, Me.TablaC.Item(TipPolComplemento.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaC.Item(FCHComplemento.Index, posicion).Value,
                                  Me.TablaC.Item(NoChComplemento.Index, posicion).Value, BO, Me.TablaC.Item(CtaOchComplemento.Index, posicion).Value, Me.TablaC.Item(UUIDComplemento.Index, posicion).Value, Me.TablaC.Item(ImpChComplemento.Index, posicion).Value)
                End If
            End If

            'Verificara el sistema las polizas automatizadas
            Crear_detalleComple(posicion, poliza_Sistema)
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

    Private Sub Crear_detalleComple(ByVal p As Integer, ByVal pol As String)
        Dim Item As Integer = 1
        Dim cadena As String = Trim(Me.TablaC.Item(NomCtaComplemento.Index, p).Value)
        Dim posi As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
        Dim cuantos As Integer = Len(cadena) - Len(cadena.Substring(0, posi))
        Dim Cuenta_Cargo As String = cadena.Substring(posi, cuantos)
        Dim Cuenta2 As String = ""

        '1° Inserta los Bancos
        If Me.TablaC.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpChComplemento.Index, p).Value > 0 Then 'TODOS

            cadena = Trim(Me.TablaC.Item(CtaEfComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEfComplemento.Index, p).Value, 0, Cuenta_Cargo, "")

            Item = Item + 1

            cadena = Trim(Me.TablaC.Item(CtaBancosComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpTComplemento.Index, p).Value, 0, Cuenta_Cargo, "")

            Item = Item + 1
            cadena = Trim(Me.TablaC.Item(CtaChequesC.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpChComplemento.Index, p).Value, 0, Cuenta_Cargo, Me.TablaC.Item(NoChComplemento.Index, p).Value)


        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpChComplemento.Index, p).Value = 0 Then 'Efectivo y Transferencia
            cadena = Trim(Me.TablaC.Item(CtaEfComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEfComplemento.Index, p).Value, 0, Cuenta_Cargo, "")
            Item = Item + 1
            cadena = Trim(Me.TablaC.Item(CtaBancosComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpTComplemento.Index, p).Value, 0, Cuenta_Cargo, "")

        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaC.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Transferencia

            cadena = Trim(Me.TablaC.Item(CtaBancosComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpTComplemento.Index, p).Value, 0, Cuenta_Cargo, "")
            Item = Item + 1
            cadena = Trim(Me.TablaC.Item(CtaChequesC.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpChComplemento.Index, p).Value, 0, Cuenta_Cargo, Me.TablaC.Item(NoChComplemento.Index, p).Value)

        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaC.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque y Efecivo
            cadena = Trim(Me.TablaC.Item(CtaEfComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEfComplemento.Index, p).Value, 0, Cuenta_Cargo, "")
            Item = Item + 1
            cadena = Trim(Me.TablaC.Item(CtaChequesC.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpChComplemento.Index, p).Value, 0, Cuenta_Cargo, Me.TablaC.Item(NoChComplemento.Index, p).Value)
        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaC.Item(ImpChComplemento.Index, p).Value = 0 Then ' Efecivo
            'Cuenta del Abono
            cadena = Trim(Me.TablaC.Item(CtaEfComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)

            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEfComplemento.Index, p).Value, 0, Cuenta_Cargo, "")

        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaC.Item(ImpTComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpChComplemento.Index, p).Value = 0 Then ' Transferencia
            'Cuenta del Abono transferencia
            cadena = Trim(Me.TablaC.Item(CtaBancosComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpTComplemento.Index, p).Value, 0, Cuenta_Cargo, IIf(IsDBNull(Me.TablaC.Item(NoChComplemento.Index, p).Value) = True, "", Me.TablaC.Item(NoChComplemento.Index, p).Value))

        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, p).Value = 0 And Me.TablaC.Item(ImpTComplemento.Index, p).Value = 0 And Me.TablaC.Item(ImpChComplemento.Index, p).Value > 0 Then 'Cheque 
            cadena = Trim(Me.TablaC.Item(CtaChequesC.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpChComplemento.Index, p).Value, 0, Cuenta_Cargo, Me.TablaC.Item(NoChComplemento.Index, p).Value)

        End If


        '2° Inserta Ajustes

        If Me.TablaC.Item(AjusComplemento.Index, p).Value < 0 Then ' Se verifica si cuenta con ajuste +
            cadena = Trim(Me.TablaC.Item(CtaAjusComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            'cargo de Ajuste
            Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(AjusComplemento.Index, p).Value * -1, Cuenta_Cargo, "")
            Item = Item + 1
        ElseIf Me.TablaC.Item(AjusComplemento.Index, p).Value > 0 Then ' Se verifica si cuenta con ajuste -

            cadena = Trim(Me.TablaC.Item(CtaAjusComplemento.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            'Abono de Ajuste
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(AjusComplemento.Index, p).Value, 0, Cuenta_Cargo, "")
            Item = Item + 1
        End If
        If Me.TablaC.Item(PCComplemento.Index, p).Value > 0 Then ' Se perdida cambiaria
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(PCComplemento.Index, p).Value, 0, "7010000100000000", "")
        End If
        If Me.TablaC.Item(UCComplemento.Index, p).Value > 0 Then ' Se utilidad cambiaria
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(UCComplemento.Index, p).Value, "7020000100000000", "")
        End If

        Item = Item + 1
        'Cancela Cuenta Cliente
        Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(Me.lstCliente.SelectItem, "Clientes", Me.TablaC.Item(SerC.Index, p).Value), Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 1)
        Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(TRComplemento.Index, p).Value, Cuenta_Cargo, "")

        Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP As Decimal
        PorcenPro = Me.TablaC.Item(TRComplemento.Index, p).Value / ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, p).Value, "Total_Real")
        ImpGpro = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, p).Value, "Imp_Grabado") * PorcenPro
        ImpEpro = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, p).Value, "Imp_Exento") * PorcenPro
        ImpIvaP = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, p).Value, "IVA_real") * PorcenPro



        If Me.TablaC.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value > 0 And Me.TablaC.Item(IvaRComplemento.Index, p).Value > 0 Then ' tiene grabado y exento

            Cuenta2 = RegresaCunetaComple(Regresa_Cuenta_Series(Me.lstCliente.SelectItem, "CtaIngPCE", Me.TablaC.Item(SerC.Index, p).Value), Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 1)
            Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(Me.lstCliente.SelectItem, "CtaIngPCG", Me.TablaC.Item(SerC.Index, p).Value), Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 1)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpGComplemento.Index, p).Value, 0, Cuenta_Cargo, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEComplemento.Index, p).Value, 0, Cuenta2, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(IvaComplemento.Index, p).Value, 0, Regresa_Cuenta_Impuestos(Me.lstCliente.SelectItem, "IVAPTras", Me.TablaC.Item(SerC.Index, p).Value), "")

            Item = Item + 1

        ElseIf Me.TablaC.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value <= 0 Then 'Tiene Exento o 0

            Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(Me.lstCliente.SelectItem, "CtaIngPCE", Me.TablaC.Item(SerC.Index, p).Value), Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 1)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEComplemento.Index, p).Value, 0, Cuenta_Cargo, "")
            Item = Item + 1

        ElseIf Me.TablaC.Item(ImpEComplemento.Index, p).Value <= 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value > 0 Then 'Tiene Grabado

            Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(Me.lstCliente.SelectItem, "CtaIngPCG", Me.TablaC.Item(SerC.Index, p).Value), Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 1)
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpGComplemento.Index, p).Value, 0, Cuenta_Cargo, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, Me.TablaC.Item(IvaComplemento.Index, p).Value, 0, Regresa_Cuenta_Impuestos(Me.lstCliente.SelectItem, "IVAPTras", Me.TablaC.Item(SerC.Index, p).Value), "")

            Item = Item + 1

        End If

        'Cargos con cuentas efectivo bancos

        If Me.TablaC.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value > 0 And Me.TablaC.Item(IvaComplemento.Index, p).Value > 0 Then ' tiene grabado y exento

            Cuenta2 = RegresaCunetaComple(Regresa_Cuenta_Series(Me.lstCliente.SelectItem, "CtaIngEx", Me.TablaC.Item(SerC.Index, p).Value), Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 1)
            Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(Me.lstCliente.SelectItem, "CtaIngG", Me.TablaC.Item(SerC.Index, p).Value), Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 1)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(ImpGComplemento.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(ImpEComplemento.Index, p).Value, Cuenta2, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(IvaComplemento.Index, p).Value, Regresa_Cuenta_Impuestos(Me.lstCliente.SelectItem, "IVATras", Me.TablaC.Item(SerC.Index, p).Value), "")

            Item = Item + 1

        ElseIf Me.TablaC.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value <= 0 Then 'Tiene Exento o 0

            Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(Me.lstCliente.SelectItem, "CtaIngEx", Me.TablaC.Item(SerC.Index, p).Value), Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 1)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(ImpEComplemento.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1

        ElseIf Me.TablaC.Item(ImpEComplemento.Index, p).Value <= 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value > 0 Then 'Tiene Grabado

            Cuenta_Cargo = RegresaCunetaComple(Regresa_Cuenta_Series(Me.lstCliente.SelectItem, "CtaIngG", Me.TablaC.Item(SerC.Index, p).Value), Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 1)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(ImpGComplemento.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(IvaComplemento.Index, p).Value, Regresa_Cuenta_Impuestos(Me.lstCliente.SelectItem, "IVATras", Me.TablaC.Item(SerC.Index, p).Value), "")

            Item = Item + 1

        End If

        '°Checar si se tiene correctas las ctas de Orden

        If UCase(Me.TablaC.Item(CtaOrdenC.Index, p).Value) = "SI" Then
            Item = Item + 1
            If Len(Me.TablaC.Item(RFCComplemento.Index, p).Value) = 12 Then

                If Me.TablaC.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value > 0 Then
                    Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpGComplemento.Index, p).Value, 0, "9011001000010003", "")
                    Item = Item + 1
                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEComplemento.Index, p).Value, 0, "9011001000010001", "")
                    Else
                        Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEComplemento.Index, p).Value, 0, "9011001000010002", "")
                    End If
                    Item = Item + 1

                ElseIf Me.TablaC.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value <= 0 Then 'Tiene Exento
                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEComplemento.Index, p).Value, 0, "9011001000010001", "")
                    Else
                        Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEComplemento.Index, p).Value, 0, "9011001000010002", "")
                    End If
                    Item = Item + 1
                ElseIf Me.TablaC.Item(ImpEComplemento.Index, p).Value <= 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value > 0 Then 'Tiene Grabado
                    Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpGComplemento.Index, p).Value, 0, "9011001000010003", "")
                    Item = Item + 1
                End If
            Else

                If Me.TablaC.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value > 0 Then
                    Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpGComplemento.Index, p).Value, 0, "9011002000010003", "")
                    Item = Item + 1
                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEComplemento.Index, p).Value, 0, "9011002000010001", "")
                    Else
                        Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEComplemento.Index, p).Value, 0, "9011002000010002", "")
                    End If
                    Item = Item + 1

                ElseIf Me.TablaC.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value <= 0 Then 'Tiene Exento
                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEComplemento.Index, p).Value, 0, "9011002000010001", "")
                    Else
                        Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpEComplemento.Index, p).Value, 0, "9011002000010002", "")
                    End If
                    Item = Item + 1
                ElseIf Me.TablaC.Item(ImpEComplemento.Index, p).Value <= 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value > 0 Then 'Tiene Grabado
                    Crea_detalle_poliza(pol, Item, Me.TablaC.Item(ImpGComplemento.Index, p).Value, 0, "9011002000010003", "")
                    Item = Item + 1
                End If
            End If

            If Me.TablaC.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value > 0 Then

                Cuenta2 = RegresaCunetaComple("901000010003", Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(ImpGComplemento.Index, p).Value, Cuenta2, "")
                Item = Item + 1
                If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                    Cuenta2 = RegresaCunetaComple("901000010001", Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 0)
                Else
                    Cuenta2 = RegresaCunetaComple("901000010002", Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 0)
                End If
                Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(ImpEComplemento.Index, p).Value, Cuenta2, "")
                Item = Item + 1

            ElseIf Me.TablaC.Item(ImpEComplemento.Index, p).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value <= 0 Then 'Tiene Exento
                If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                    Cuenta2 = RegresaCunetaComple("901000010001", Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 0)
                Else
                    Cuenta2 = RegresaCunetaComple("901000010002", Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 0)
                End If
                Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(ImpEComplemento.Index, p).Value, Cuenta2, "")
                Item = Item + 1
            ElseIf Me.TablaC.Item(ImpEComplemento.Index, p).Value <= 0 And Me.TablaC.Item(ImpGComplemento.Index, p).Value > 0 Then 'Tiene Grabado
                Cuenta2 = RegresaCunetaComple("901000010003", Trim(Me.TablaC.Item(RFCComplemento.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaC.Item(ImpGComplemento.Index, p).Value, Cuenta2, "")
                Item = Item + 1
            End If
        End If


        Exit Sub
    End Sub
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

    Private Function Regresa_Cuenta_Series(ByVal cliente As Integer, ByVal tipo As String, ByVal serie As String)
        Dim cta As String = ""
        Dim sql As String = "SELECT 
	Id_Ingreso,	Serie,	Abono,	CtaIngG,	CtaIngEx,	CtaIngC,	IVATras,	ISRRet,	IVARet,	CtaIngPCG,	CtaIngPCE,	CtaIngPCC,
	IVAPTras,	ISRRPA,	IVARetPA,DevSVentasG,DevSVentasC,DevSVentasEx,IvaSDev,Id_Empresa,Clientes FROM dbo.Series_Clientes WHERE Id_Empresa = " & cliente & " and serie  = '" & serie & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            cta = Trim(ds.Tables(0).Rows(0)(tipo)).Substring(0, 12)
        Else

            cta = ""
        End If
        Return cta
    End Function
    Private Function Regresa_Cuenta_Impuestos(ByVal cliente As Integer, ByVal tipo As String, ByVal serie As String)
        Dim cta As String = ""
        Dim sql As String = "SELECT 
	Id_Ingreso,	Serie,	Abono,	CtaIngG,	CtaIngEx,	CtaIngC,	IVATras,	ISRRet,	IVARet,	CtaIngPCG,	CtaIngPCE,	CtaIngPCC,
	IVAPTras,	ISRRPA,	IVARetPA,IvaSDev,	Id_Empresa FROM dbo.Series_Clientes WHERE Id_Empresa = " & cliente & " and Serie = '" & serie & "'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            cta = Trim(ds.Tables(0).Rows(0)(tipo))
        Else

            cta = ""
        End If
        Return cta
    End Function

    Private Function Regresa_Cuenta_Retencion(ByVal cliente As Integer, ByVal iva As Boolean, ByVal Serie As String)
        Dim cta As String = ""
        Dim sql As String = ""
        If iva = True Then
            sql = "SELECT IVARet FROM dbo.Series_Clientes WHERE Id_Empresa = " & cliente & " and serie ='" & Serie & "'"
        Else
            sql = "SELECT ISRRet FROM dbo.Series_Clientes WHERE Id_Empresa = " & cliente & "  and serie ='" & Serie & "'"
        End If

        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            cta = ds.Tables(0).Rows(0)(0).ToString
        Else
            cta = ""
        End If
        Return cta
    End Function
    Private Function RegresaCunetaComple(ByVal cuenta As String, ByVal rfc As String, ByVal posicion As Integer, Optional ByVal tipo As Integer = 0)
        Dim Cta As String = ""
        Dim sql As String = ""
        If tipo = 1 Then
            sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND Nivel3 > 0 AND RFC = '" & rfc & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        ElseIf tipo = 2 Then

            sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND Nivel3 = '" & cuenta.Substring(8, 4) & "' AND Nivel4 > 0 AND RFC = '" & rfc & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        End If
        'Dim sql As String = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND Nivel3 = '" & cuenta.Substring(8, 4) & "' AND Nivel4 > 0 AND RFC = '" & rfc & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cta = ds.Tables(0).Rows(0)(0)
        Else
            'No existe la cuenta y la inserta
            If tipo = 1 Then
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3 >= 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), Cta.ToString.Substring(0, 4),
                               "0000", cuenta.Substring(0, 8) & Cta & "0000", Me.TablaC.Item(RFCComplemento.Index, posicion).Value & " " & Me.TablaC.Item(NEmComplemento.Index, posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaC.Item(LetraSatComplemento.Index, posicion).Value, Me.TablaC.Item(RFCComplemento.Index, posicion).Value)
                Cta = cuenta.Substring(0, 8) & Cta & "0000"
            Else
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & cuenta.ToString.Substring(8, 4) & " AND Nivel4 > 0 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), cuenta.ToString.Substring(8, 4),
                                  Cta, cuenta.Substring(0, 12) & Cta, Me.TablaC.Item(RFCComplemento.Index, posicion).Value & " " & Me.TablaC.Item(NEmComplemento.Index, posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaC.Item(LetraSatComplemento.Index, posicion).Value, Me.TablaC.Item(RFCComplemento.Index, posicion).Value)
                Cta = cuenta.Substring(0, 12) & Cta
            End If
        End If
        Return Cta
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
            If Len(descripcion) >= 50 Then
                sql &= "	'" & descripcion.Substring(0, 48) & "'," '@descripcion
            Else
                sql &= "	'" & descripcion & "'," '@descripcion
            End If
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
            If Trim(ds.Tables(0).Rows(0)("DES")) = Trim(ds2.Tables(0).Rows(0)("Cuenta")) Then
                cuenta = ds.Tables(0).Rows(0)("Cuenta")
            Else
                cuenta = ds2.Tables(0).Rows(0)("Cuenta") & " " & ds.Tables(0).Rows(0)("Cuenta")
            End If

        Else
            cuenta = ""
        End If
        Return cuenta
    End Function
    Private Sub TablaC_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaC.CellEndEdit
        Liberar_ProcesoComple(Me.TablaC.CurrentRow.Index)
        'Calcula cuenta cargos

        If Me.TablaC.Item(ProvPComplemento.Index, Me.TablaC.CurrentRow.Index).Value > 0 Then
            Me.TablaC.Item(ProvAComplemento.Index, Me.TablaC.CurrentRow.Index).Value = 0
        ElseIf Me.TablaC.Item(ProvAComplemento.Index, Me.TablaC.CurrentRow.Index).Value > 0 Then
            Me.TablaC.Item(ProvPComplemento.Index, Me.TablaC.CurrentRow.Index).Value = 0
        End If

        If Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value <> Nothing Then
            Me.TablaC.Item(NomCtaComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Cuenta_cargos(Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Trim(Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value))
            Me.TablaC.Item(LetraSatComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.Calcula_letraSat(Me.TablaC.Item(FpagoComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(UCFDIComplemento.Index, Me.TablaC.CurrentRow.Index).Value)

            If Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value = "C" Or Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value = "CPP" Then
                Me.TablaC.Item(ProvAComplemento.Index, Me.TablaC.CurrentRow.Index).Value = 0
            ElseIf Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value = "GG" Or Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value = "GGPP" Then
                Me.TablaC.Item(ProvPComplemento.Index, Me.TablaC.CurrentRow.Index).Value = 0
            End If
        Else
            Me.TablaC.Item(NomCtaComplemento.Index, Me.TablaC.CurrentRow.Index).Value = ""
        End If

        'CALCULAR CUENTA DE EFECTIVO
        If Me.TablaC.Item(ImpEfComplemento.Index, Me.TablaC.CurrentRow.Index).Value > 0 Then

            Me.TablaC.Item(CtaEfComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Cuenta_Efectivo()
            Me.TablaC.Columns(CtaEfComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

        Else
            Me.TablaC.Item(CtaEfComplemento.Index, Me.TablaC.CurrentRow.Index).Value = ""
        End If

        'CALCULAR CUENTA DE Ajustes
        If Me.TablaC.Item(AjusComplemento.Index, Me.TablaC.CurrentRow.Index).Value <> 0 Then

            Me.TablaC.Item(CtaAjusComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Cuenta_Efectivo()
            Me.TablaC.Columns(CtaAjusComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

        Else
            Me.TablaC.Item(CtaAjusComplemento.Index, Me.TablaC.CurrentRow.Index).Value = ""
        End If
        'CALCULAR CUENTA DE anticipos
        'If Me.TablaC.Item(AntiComplemento.Index, Me.TablaC.CurrentRow.Index).Value > 0 Then

        '    Me.TablaC.Item(CtaAntiComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Cuenta_Anticipo()
        '    Me.TablaC.Columns(CtaAntiComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        'Else
        '    Me.TablaC.Item(CtaAntiComplemento.Index, Me.TablaC.CurrentRow.Index).Value = ""
        'End If

        If Me.TablaC.Item(ImpTComplemento.Index, Me.TablaC.CurrentRow.Index).Value > 0 Then
            If Trim(Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" Then
                If Trim(Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" Then
                Else
                    Dim posi As Integer = InStr(1, Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                    Dim cuantos As Integer = Len(Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value) - Len(Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, posi))
                    Dim Al As String = Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(posi, cuantos)
                    Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                    Me.TablaC.Item(CtaBancosComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)
                End If
            Else

            End If

            '
            Try
                If Trim(Me.TablaC.Item(CtaOTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" And Trim(Me.TablaC.Item(CtaOTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> Nothing Then
                    Dim largo As Integer = Len(Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                    Dim cadena As String = Trim(Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value.ToString.Substring(largo - 3, 3))
                    largo = Len(Me.TablaC.Item(CtaOTComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                    Dim Al As String = Me.TablaC.Item(CtaOTComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(largo - 3, 3)
                    If cadena <> Al Then
                        largo = InStr(1, Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                        Al = Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, largo - 1)
                        Me.TablaC.Item(CtaOTComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                    End If
                Else
                    Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Banco_destinoComp(Me.lstCliente.SelectItem, Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                    Dim posi As Integer = InStr(1, Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                    Dim Al As String = Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, posi - 1)
                    Me.TablaC.Item(CtaOTComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & "'", True)
                End If
            Catch ex As Exception

            End Try


            If Trim(Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" Then
                Try
                    If Trim(Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" And Trim(Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "0" Then
                    Else
                        Dim posi As Integer = InStr(1, Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                        Dim Al As String = Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, posi - 1)
                        Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & "'", True)
                    End If
                Catch ex As Exception

                End Try
            Else
                '  Banco_destinoComp()

            End If

            If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                Me.TablaC.Columns(BancoOrigenTComplemento.Index).Visible = False
                Me.TablaC.Columns(CtaOTComplemento.Index).Visible = False
                Me.TablaC.Columns(BancoDtComplemento.Index).Visible = False
                Me.TablaC.Columns(CtaDTComplemento.Index).Visible = False
                Me.TablaC.Columns(FTComplemento.Index).Visible = True
            End If

        End If

        If Me.TablaC.Item(ImpChComplemento.Index, Me.TablaC.CurrentRow.Index).Value > 0 Then

            'Cheques
            'Cargar bancos destino para ingresos de Cheques
            Dim posi As Integer = 0
            Dim Al As String = ""
            If Trim(Me.TablaC.Item(BancoDEC.Index, Me.TablaC.CurrentRow.Index).Value) <> "" Then
                If Trim(Me.TablaC.Item(CtaBancoDChC.Index, Me.TablaC.CurrentRow.Index).Value) <> "" Then


                Else
                    posi = InStr(1, Me.TablaC.Item(BancoDEC.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                    Dim cuantos As Integer = Len(Me.TablaC.Item(BancoDEC.Index, Me.TablaC.CurrentRow.Index).Value) - Len(Me.TablaC.Item(BancoDEC.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, posi))
                    Al = Me.TablaC.Item(BancoDEC.Index, Me.TablaC.CurrentRow.Index).Value.Substring(posi, cuantos)
                    Me.TablaC.Item(CtaBancoDChC.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                    Me.TablaC.Item(CtaChequesC.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)
                End If
            Else

            End If
            'Cargar bancos Origen para ingresos de Cheques
            If Trim(Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" Then
                If Trim(Me.TablaC.Item(CtaOchComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" Then
                    Dim largo As Integer = Len(Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                    Dim cadena As String = Trim(Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value.ToString.Substring(largo - 3, 3))
                    largo = Len(Me.TablaC.Item(CtaOchComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                    Al = Me.TablaC.Item(CtaOchComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(largo - 3, 3)
                    If cadena <> Al Then
                        posi = InStr(1, Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                        Al = Trim(Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, posi - 1))
                        Me.TablaC.Item(CtaOchComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                    End If
                Else
                    posi = InStr(1, Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                    Al = Trim(Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, posi - 1))
                    Me.TablaC.Item(CtaOchComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & "'", True)
                End If
            Else
                Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Banco_OrigenChe(Me.lstCliente.SelectItem, Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                Me.TablaC.Item(CtaOchComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & "  and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & "'", True)
            End If

            'Se Muestran Columnas de Contabilidad Electronica
            If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                Me.TablaC.Columns(BancoCHComplemento.Index).Visible = False
                Me.TablaC.Columns(CtaOchComplemento.Index).Visible = False
                Me.TablaC.Columns(NoChComplemento.Index).Visible = False
                Me.TablaC.Columns(FCHComplemento.Index).Visible = True
            End If

        End If

    End Sub
    Private Function Calcula_diferencia(ByVal total As Decimal, ByVal monto_Efectivo As Decimal, ByVal monto_cheque As Decimal, ByVal monto_Transferencia As Decimal, ByVal ajuste As Decimal, ByVal Anticipo As Decimal, ByVal Provision As Decimal
                                   )
        Dim Diferencia As Decimal = 0
        Diferencia = total - (monto_Efectivo + monto_cheque + monto_Transferencia + ajuste + Anticipo + Provision)
        Return Diferencia
    End Function
    Private Sub Calcula_Importes(ByVal i As Integer, ByVal Suma As Decimal)
        If Me.TablaC.Rows.Count >= 1 Then

            Try
                Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP As Decimal
                PorcenPro = Suma / ImpFaCURA(Me.TablaC.Item(UUIDComplemento.Index, i).Value, "Total_Real")
                ImpGpro = ImpFaCURA(Me.TablaC.Item(UUIDComplemento.Index, i).Value, "Imp_Grabado") * PorcenPro
                ImpEpro = ImpFaCURA(Me.TablaC.Item(UUIDComplemento.Index, i).Value, "Imp_Exento") * PorcenPro
                ImpIvaP = ImpFaCURA(Me.TablaC.Item(UUIDComplemento.Index, i).Value, "IVA_real") * PorcenPro
                If Me.TablaC.Item(TotComplemento.Index, i).Value > 0 And Me.TablaC.Item(TotComplemento.Index, i).Value <> Nothing Then
                    'importe gravado
                    Me.TablaC.Item(ImpGComplemento.Index, i).Value = Math.Round(ImpGpro, 2)
                    'importe exento
                    Me.TablaC.Item(ImpEComplemento.Index, i).Value = Math.Round(ImpEpro, 2)
                    'Iva_real
                    Me.TablaC.Item(IvaRComplemento.Index, i).Value = Math.Round(ImpIvaP, 2)
                    'calcula el % Pago Acumulado
                    If Me.TablaC.Item(ImpEComplemento.Index, i).Value < 1 Then
                        Me.TablaC.Item(ImpGComplemento.Index, i).Value = Me.TablaC.Item(ImpGComplemento.Index, i).Value + Me.TablaC.Item(ImpEComplemento.Index, i).Value
                        Me.TablaC.Item(ImpEComplemento.Index, i).Value = 0
                    End If
                    'Total real
                    Me.TablaC.Item(TRComplemento.Index, i).Value = Me.TablaC.Item(ImpGComplemento.Index, i).Value + Me.TablaC.Item(ImpEComplemento.Index, i).Value + Me.TablaC.Item(IvaRComplemento.Index, i).Value
                End If
            Catch ex As Exception
                Me.TablaC.Rows(i).DefaultCellStyle.BackColor = Color.Red
            End Try

        End If

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
    Private Sub Liberar_ProcesoComple(ByVal i As Integer)
        Dim contador As Integer = 0
        Dim cantidad As Decimal = Me.TablaC.Item(ImpProviComplemento.Index, i).Value
        cantidad = cantidad + Me.TablaC.Item(UCComplemento.Index, i).Value
        cantidad = cantidad - Me.TablaC.Item(PCComplemento.Index, i).Value

        Dim Retencion As Decimal = 0
        Retencion = Me.TablaC.Item(RISRComplemento.Index, i).Value + Me.TablaC.Item(RIVAComplemento.Index, i).Value
        'Calcula la diferencia en el registro
        Me.TablaC.Item(DifComplemento.Index, i).Value = Math.Round(Calcula_diferencia(IIf(IsDBNull(Me.TablaC.Item(TotComplemento.Index, i).Value) = True, 0, Me.TablaC.Item(TotComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaC.Item(ImpEfComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaC.Item(ImpTComplemento.Index, i).Value) + Convert.ToDecimal(Me.TablaC.Item(ProvAComplemento.Index, i).Value) + Convert.ToDecimal(Me.TablaC.Item(ProvPComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaC.Item(ImpChComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaC.Item(AjusComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaC.Item(AntiComplemento.Index, i).Value), cantidad), 2) - Busacar_parcialidades(Me.TablaC.Item(UUIDComplemento.Index, i).Value.trim())
        Calcula_Importes(i, Me.TablaC.Item(ImpEfComplemento.Index, i).Value + Me.TablaC.Item(TotComplemento.Index, i).Value + Me.TablaC.Item(ProvAComplemento.Index, i).Value + Me.TablaC.Item(ProvPComplemento.Index, i).Value + Me.TablaC.Item(ImpChComplemento.Index, i).Value + Me.TablaC.Item(AjusComplemento.Index, i).Value + Me.TablaC.Item(AntiComplemento.Index, i).Value)

        If Me.TablaC.Item(DifComplemento.Index, i).Value <> 0 Then
            Me.TablaC.Item(DifComplemento.Index, i).Style.BackColor = Color.Red
        Else
            Me.TablaC.Item(DifComplemento.Index, i).Style.BackColor = Color.Green
        End If

        If Trim(UCase(Me.lblce.Text)) = "TRUE" Then ' Bloquera filas de Contabilidad electronica
            ' If Me.TablaC.Item(DifComplemento.Index, i).Value > 0 Or Me.TablaC.Item(TipPolComplemento.Index, i).Value = Nothing Or IIf(IsDBNull(Me.TablaC.Item(NomCtaComplemento.Index, i).Value) = True, "", Me.TablaC.Item(NomCtaComplemento.Index, i).Value) = "" Or IIf(IsDBNull(Me.TablaC.Item(NumPolComplemento.Index, i).Value) = True, "", Me.TablaC.Item(NumPolComplemento.Index, i).Value) = "" Then
            If Me.TablaC.Item(DifComplemento.Index, i).Value < 0 Then
                Me.TablaC.Item(ApliComplemento.Index, i).Value = False
            Else

                If Me.TablaC.Item(ImpTComplemento.Index, i).Value > 0 Then ' Bloqueo transferencia
                    Try
                        If Me.TablaC.Item(BancoOrigenTComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(BancoDtComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(CtaOTComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(CtaDTComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(FTComplemento.Index, i).Value = Nothing Then
                            Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                        Else
                            Me.TablaC.Item(ApliComplemento.Index, i).Value = True
                        End If
                    Catch ex As Exception
                        Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                    End Try

                ElseIf Me.TablaC.Item(ImpChComplemento.Index, i).Value > 0 Then ' Bloqueo cheques
                    Try
                        If Me.TablaC.Item(BancoCHComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(CtaOchComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(NoChComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(FCHComplemento.Index, i).Value = Nothing Then
                            Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                        Else
                            Me.TablaC.Item(ApliComplemento.Index, i).Value = True
                        End If
                    Catch ex As Exception
                        Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                    End Try

                ElseIf Me.TablaC.Item(ImpChComplemento.Index, i).Value > 0 And Me.TablaC.Item(ImpTComplemento.Index, i).Value > 0 Then ' AMbos
                    Try
                        If Me.TablaC.Item(BancoOrigenTComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(BancoDtComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(CtaOTComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(CtaDTComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(FTComplemento.Index, i).Value = Nothing Then
                            Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                        Else
                            Me.TablaC.Item(ApliComplemento.Index, i).Value = True
                        End If
                    Catch ex As Exception
                        Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                    End Try

                    Try
                        If Me.TablaC.Item(BancoCHComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(CtaOchComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(NoChComplemento.Index, i).Value = Nothing Or Me.TablaC.Item(FCHComplemento.Index, i).Value = Nothing Then
                            Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                        Else
                            Me.TablaC.Item(ApliComplemento.Index, i).Value = True
                        End If
                    Catch ex As Exception
                        Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                    End Try
                ElseIf Me.TablaC.Item(ImpEfComplemento.Index, i).Value > 0 Then ' AMbos
                    Me.TablaC.Item(ApliComplemento.Index, i).Value = True
                Else
                    Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                End If

                If Me.TablaC.Item(AjusComplemento.Index, i).Value <> 0 Then ' Bloqueo Ajuste
                    If IIf(IsDBNull(Me.TablaC.Item(CtaAjusComplemento.Index, i).Value) = True, "", Me.TablaC.Item(CtaAjusComplemento.Index, i).Value) = "" Then
                        Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                    Else
                        Me.TablaC.Item(ApliComplemento.Index, i).Value = True
                    End If
                End If

                If Me.TablaC.Item(AntiComplemento.Index, i).Value <> 0 Then ' Bloqueo Anticipo
                    If IIf(IsDBNull(Me.TablaC.Item(CtaAntiComplemento.Index, i).Value) = True, "", Me.TablaC.Item(CtaAntiComplemento.Index, i).Value) = "" Then
                        Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                    Else
                        Me.TablaC.Item(ApliComplemento.Index, i).Value = True
                    End If
                End If

            End If
        Else
            'sin contabilidad electronica
            If Me.TablaC.Item(DifComplemento.Index, i).Value <> 0 Or Me.TablaC.Item(TipPolComplemento.Index, i).Value = Nothing Or IIf(IsDBNull(Me.TablaC.Item(NumPolComplemento.Index, i).Value) = True, "", Me.TablaC.Item(NumPolComplemento.Index, i).Value) = "" Then
                Me.TablaC.Item(ApliComplemento.Index, i).Value = False
            Else
                Me.TablaC.Item(ApliComplemento.Index, i).Value = True
            End If
            Try
                If Me.TablaC.Item(AjusComplemento.Index, i).Value <> 0 Then ' Bloqueo Ajuste
                    If IIf(IsDBNull(Me.TablaC.Item(CtaAjusComplemento.Index, i).Value) = True, "", Me.TablaC.Item(CtaAjusComplemento.Index, i).Value) = "" Then
                        Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                    Else
                        Me.TablaC.Item(ApliComplemento.Index, i).Value = True
                    End If
                End If

                If Me.TablaC.Item(AntiComplemento.Index, i).Value <> 0 Then ' Bloqueo Anticipo
                    If IIf(IsDBNull(Me.TablaC.Item(CtaAntiComplemento.Index, i).Value) = True, "", Me.TablaC.Item(CtaAntiComplemento.Index, i).Value) = "" Then
                        Me.TablaC.Item(ApliComplemento.Index, i).Value = False
                    Else
                        Me.TablaC.Item(ApliComplemento.Index, i).Value = True
                    End If
                End If
            Catch ex As Exception

            End Try
        End If

        For s As Integer = 0 To Me.TablaC.Rows.Count - 1
            If Me.TablaC.Item(0, s).Value = True Then
                contador = contador + 1
            End If
        Next
        If contador > 0 Then
            Me.CmdProcesaComple.Enabled = True
        End If
        If Me.TablaC.Item(ApliComplemento.Index, i).Style.BackColor = Color.Red Then
            Me.TablaC.Item(ApliComplemento.Index, i).Value = False
        End If
    End Sub
    Private Function Banco_OrigenChe(ByVal Cliente As Integer, ByVal Rfc As String)
        Dim banco As String = ""
        Dim sql2 As String = " SELECT DISTINCT CONVERT(NVARCHAR, Bancos.clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE emitidas=1 and (Bancos_RFC.Id_Empresa  = " & Cliente & ") and RFC = '" & Rfc & "' and Favorito=1"
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
    Private Function Cuenta_dev()
        Dim cuenta As String
        Dim sql As String = " Select rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1010' and Nivel2 > 0 and Nivel3 > 0 "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            cuenta = ds.Tables(0).Rows(0)("Cuenta")
        Else
            cuenta = ""
        End If
        Return cuenta
    End Function
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

    Private Sub CmdProcesaComple_Click(sender As Object, e As EventArgs) Handles CmdProcesaComple.Click

    End Sub
End Class
