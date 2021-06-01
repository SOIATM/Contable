Imports Telerik.WinControls
Public Class ControldeEgresos

    Dim Activo As Boolean
    Dim Dato As DataSet
    Dim DatoC As DataSet
    Public serV As String = My.Forms.Inicio.txtServerDB.Text

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
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
        Me.LstTexto.Cargar(" Select '','' ")
    End Sub
    Private Sub ControldeEgresos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Diseño()
        Me.DtInicio.Value = Today
        Me.Dtfin.Value = Today

        Cargar_Clientes()
    End Sub


    Private Sub Diseño()
        Eventos.DiseñoTablaEnca(TablaImportar)
        Eventos.DiseñoTablaEnca(TablaC)
        Eventos.DiseñoTablaEnca(TablaD)
    End Sub



    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        If Me.TablaImportar.Rows.Count > 0 Then
            Limpia()
            Me.LstTexto.SelectText = ""
        End If
    End Sub
    Private Sub Limpia()
        Me.TablaImportar.Rows.Clear()
        Me.lbl16.Text = ""
        Me.lbl0.Text = ""
        Me.lble.Text = ""
    End Sub

    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            If Me.lstCliente.SelectText <> "" Then
                If Verifica_catalogo_cliente(Me.lstCliente.SelectItem) = True Then
                    Guardar_Carga()
                    If RadMessageBox.Show("La Empresa " & Me.lstCliente.SelectText & " es correcta?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Me.Barra.Maximum = Me.TablaImportar.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0

                        For p As Integer = 0 To Me.TablaImportar.RowCount - 1
                            If Me.TablaImportar.Item(Aplic.Index, p).Value = True Then ' se paso todos los filtros de creacion
                                Codificar_polizas(p)
                            End If
                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                Me.Barra.Value1 = 0
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    End If
                Else
                    RadMessageBox.Show("No existe Catalogo de cuentas para: " & Me.lstCliente.SelectText & "", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

                End If
                Me.CmdImportar.PerformClick()
            Else
                RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

            End If



        Else
            RadMessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

        End If
    End Sub
    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        Activo = True
        Limpia()
        Dim sql As String = ""
        If Me.lstCliente.SelectText <> "" Then
            If Buscar_Parametros(Me.lstCliente.SelectItem) = True Then
                Try
                    If Dato.Tables(0).Rows.Count > 0 Then
                        Dato.Clear()
                    End If
                Catch ex As Exception

                End Try

                Buscar_xml(Me.lstCliente.SelectItem, " and Fecha_Emision >= " & Eventos.Sql_hoy(Me.DtInicio.Value) & " and Fecha_Emision <= " & Eventos.Sql_hoy(Dtfin.Value) & "")
                SP1.RunWorkerAsync(Me.TablaImportar)
                Control.CheckForIllegalCrossThreadCalls = False
                Me.TablaImportar.Enabled = True
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If




        Activo = False
    End Sub

    Private Sub Buscar_Facturas(ByVal ds As DataSet)
        Try
            If ds.Tables(0).Rows.Count > 0 Then

            End If

        Catch ex As Exception
            Exit Sub
        End Try
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Calculando Facturas por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = ds.Tables(0).Rows.Count

        For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(j)
            Me.TablaImportar.Item(0, j).Value = False
            Me.TablaImportar.Item(Id_Xml.Index, j).Value = ds.Tables(0).Rows(j)("Id_Registro_Xml")
            Me.TablaImportar.Item(Tip.Index, j).Value = ds.Tables(0).Rows(j)("Tipo")
            Me.TablaImportar.Item(Fecha_Emi.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Emision")
            Me.TablaImportar.Item(UUI.Index, j).Value = ds.Tables(0).Rows(j)("UUID")
            Me.TablaImportar.Item(UCFDI.Index, j).Value = ds.Tables(0).Rows(j)("UsoCFDI")
            Me.TablaImportar.Item(RFCE.Index, j).Value = ds.Tables(0).Rows(j)("RFC_Emisor")
            Me.TablaImportar.Item(NomEmisor.Index, j).Value = ds.Tables(0).Rows(j)("Nombre_Emisor").ToString.Replace("-", "")
            Me.TablaImportar.Item(Conc.Index, j).Value = ds.Tables(0).Rows(j)("Conceptos")
            Me.TablaImportar.Item(MPago.Index, j).Value = ds.Tables(0).Rows(j)("Metodo_de_Pago")
            Me.TablaImportar.Item(Fpago.Index, j).Value = Trim(ds.Tables(0).Rows(j)("Descripcion"))
            Me.TablaImportar.Item(Subtotal.Index, j).Value = ds.Tables(0).Rows(j)("SubTotal")
            Me.TablaImportar.Item(CIVA.Index, j).Value = ds.Tables(0).Rows(j)("IVA_16")

            Me.TablaImportar.Item(iva8.Index, j).Value = ds.Tables(0).Rows(j)("IVA_8")

            Me.TablaImportar.Item(Tot.Index, j).Value = ds.Tables(0).Rows(j)("Total")

            Me.TablaImportar.Item(LetraS.Index, j).Value = ds.Tables(0).Rows(j)("Letra_Sat")
            Me.TablaImportar.Item(NCuenta.Index, j).Value = ds.Tables(0).Rows(j)("Nombre_cuenta")
            Try
                If Trim(ds.Tables(0).Rows(j)("Clave").ToString) <> "" Or IsDBNull(ds.Tables(0).Rows(j)("Clave")) = False Then
                    Fila.Cells(ContabilizacionC.Index).Value = Trim(ds.Tables(0).Rows(j)("Clave"))
                End If

            Catch ex As Exception

            End Try

            Dim year As String = ds.Tables(0).Rows(j)("Fecha_Emision").ToString.Substring(6, 4)
            Dim month As String = ds.Tables(0).Rows(j)("Fecha_Emision").ToString.Substring(3, 2)
            Me.TablaImportar.Item(anio.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Anio_Contable")) = True, year, ds.Tables(0).Rows(j)("Anio_Contable"))
            Me.TablaImportar.Item(Mes.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Mes_Contable")) = True, month, ds.Tables(0).Rows(j)("Mes_Contable"))
            Me.TablaImportar.Item(ImpEf.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Efectivo")) = True, 0, ds.Tables(0).Rows(j)("Imp_Efectivo"))
            Me.TablaImportar.Item(CuentasEfectivo.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Efectivo")
            Me.TablaImportar.Item(ImpT.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Transf")) = True, 0, ds.Tables(0).Rows(j)("Imp_Transf"))


            Try
                If Trim(ds.Tables(0).Rows(j)("Banco_Origen")) <> "" Then
                    Fila.Cells(BancoOrigen.Index).Value = Me.BancoOrigen.Items(Obtener_indexB(Trim(ds.Tables(0).Rows(j)("Banco_Origen"))))
                End If
            Catch ex As Exception

            End Try

            Me.TablaImportar.Item(CuentaO.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Origen")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Origen"))
            Me.TablaImportar.Item(Bancodestino.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Banco_Destino")) = True, "", ds.Tables(0).Rows(j)("Banco_Destino"))
            Me.TablaImportar.Item(CtaBD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Destino")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Destino"))
            Me.TablaImportar.Item(Fechat.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Transaccion")

            'Implementar las 10 Transferencias

            Try
                Me.TablaImportar.Item(ImpT2.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Transf1")) = True, 0, ds.Tables(0).Rows(j)("Imp_Transf1"))
                Try
                    If Trim(ds.Tables(0).Rows(j)("Banco_Origen1")) <> "" Then
                        Fila.Cells(BankOT2.Index).Value = Me.BankOT2.Items(Obtener_indexBancoOrigenT2(Trim(ds.Tables(0).Rows(j)("Banco_Origen1"))))
                    End If
                Catch ex As Exception

                End Try
                Me.TablaImportar.Item(CtaOT2.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Origen1")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Origen1"))
                Me.TablaImportar.Item(BankDT2.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Banco_Destino1")) = True, "", ds.Tables(0).Rows(j)("Banco_Destino1"))
                Me.TablaImportar.Item(CtaDT2.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Destino1")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Destino1"))
                Me.TablaImportar.Item(FT2.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Transaccion1")
                Me.TablaImportar.Item(CtaT2.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Bancos1")
            Catch ex As Exception

            End Try
            Try
                Me.TablaImportar.Item(ImpT3.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Transf2")) = True, 0, ds.Tables(0).Rows(j)("Imp_Transf2"))
                Try
                    If Trim(ds.Tables(0).Rows(j)("Banco_Origen2")) <> "" Then
                        Fila.Cells(BankOT3.Index).Value = Me.BankOT3.Items(Obtener_indexBancoOrigenT3(Trim(ds.Tables(0).Rows(j)("Banco_Origen2"))))
                    End If
                Catch ex As Exception

                End Try
                Me.TablaImportar.Item(CtaOT3.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Origen2")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Origen2"))
                Me.TablaImportar.Item(BankDT3.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Banco_Destino2")) = True, "", ds.Tables(0).Rows(j)("Banco_Destino2"))
                Me.TablaImportar.Item(CtaDT3.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Destino2")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Destino2"))
                Me.TablaImportar.Item(FT3.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Transaccion2")
                Me.TablaImportar.Item(CtaT3.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Bancos2")
            Catch ex As Exception

            End Try


            Try
                Me.TablaImportar.Item(ImpT4.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Transf3")) = True, 0, ds.Tables(0).Rows(j)("Imp_Transf3"))
                Try
                    If Trim(ds.Tables(0).Rows(j)("Banco_Origen3")) <> "" Then
                        Fila.Cells(BankOT4.Index).Value = Me.BankOT4.Items(Obtener_indexBancoOrigenT4(Trim(ds.Tables(0).Rows(j)("Banco_Origen3"))))
                    End If
                Catch ex As Exception

                End Try
                Me.TablaImportar.Item(CtaOT4.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Origen3")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Origen3"))
                Me.TablaImportar.Item(BankDT4.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Banco_Destino3")) = True, "", ds.Tables(0).Rows(j)("Banco_Destino3"))
                Me.TablaImportar.Item(CtaDT4.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Destino3")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Destino3"))
                Me.TablaImportar.Item(FT4.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Transaccion3")
                Me.TablaImportar.Item(CtaT4.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Bancos3")
            Catch ex As Exception

            End Try
            Try
                Me.TablaImportar.Item(ImpT5.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Transf4")) = True, 0, ds.Tables(0).Rows(j)("Imp_Transf4"))
                Try
                    If Trim(ds.Tables(0).Rows(j)("Banco_Origen4")) <> "" Then
                        Fila.Cells(BankOT5.Index).Value = Me.BankOT5.Items(Obtener_indexBancoOrigenT5(Trim(ds.Tables(0).Rows(j)("Banco_Origen4"))))
                    End If
                Catch ex As Exception

                End Try
                Me.TablaImportar.Item(CtaOT5.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Origen4")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Origen4"))
                Me.TablaImportar.Item(BankDT5.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Banco_Destino4")) = True, "", ds.Tables(0).Rows(j)("Banco_Destino4"))
                Me.TablaImportar.Item(CtaDT5.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Destino4")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Destino4"))
                Me.TablaImportar.Item(FT5.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Transaccion4")
                Me.TablaImportar.Item(CtaT5.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Bancos4")
            Catch ex As Exception

            End Try
            Try
                Me.TablaImportar.Item(ImpT6.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Transf5")) = True, 0, ds.Tables(0).Rows(j)("Imp_Transf5"))
                Try
                    If Trim(ds.Tables(0).Rows(j)("Banco_Origen4")) <> "" Then
                        Fila.Cells(BankOT6.Index).Value = Me.BankOT5.Items(Obtener_indexBancoOrigenT6(Trim(ds.Tables(0).Rows(j)("Banco_Origen5"))))
                    End If
                Catch ex As Exception

                End Try
                Me.TablaImportar.Item(CtaOT6.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Origen5")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Origen5"))
                Me.TablaImportar.Item(BankDT6.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Banco_Destino5")) = True, "", ds.Tables(0).Rows(j)("Banco_Destino5"))
                Me.TablaImportar.Item(CtaDT6.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Destino5")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Destino5"))
                Me.TablaImportar.Item(FT6.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Transaccion5")
                Me.TablaImportar.Item(CtaT6.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Bancos5")
            Catch ex As Exception

            End Try
            Try
                Me.TablaImportar.Item(ImpT7.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Transf6")) = True, 0, ds.Tables(0).Rows(j)("Imp_Transf6"))
                Try
                    If Trim(ds.Tables(0).Rows(j)("Banco_Origen6")) <> "" Then
                        Fila.Cells(BankOT7.Index).Value = Me.BankOT5.Items(Obtener_indexBancoOrigenT7(Trim(ds.Tables(0).Rows(j)("Banco_Origen6"))))
                    End If
                Catch ex As Exception

                End Try
                Me.TablaImportar.Item(CtaOT7.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Origen6")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Origen6"))
                Me.TablaImportar.Item(BankDT7.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Banco_Destino6")) = True, "", ds.Tables(0).Rows(j)("Banco_Destino6"))
                Me.TablaImportar.Item(CtaDT7.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Destino6")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Destino6"))
                Me.TablaImportar.Item(FT7.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Transaccion6")
                Me.TablaImportar.Item(CtaT7.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Bancos6")
            Catch ex As Exception

            End Try




            Me.TablaImportar.Item(ImpC.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Cheque")) = True, 0, ds.Tables(0).Rows(j)("Imp_Cheque"))


            Try
                If Trim(ds.Tables(0).Rows(j)("Nom_Banco_Ch")) <> "" Then
                    Fila.Cells(BancosCheques.Index).Value = Me.BancosCheques.Items(Obtener_indexbN(Trim(ds.Tables(0).Rows(j)("Nom_Banco_Ch"))))
                End If
            Catch ex As Exception

            End Try

            Me.TablaImportar.Item(CuentaC.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Origen_Ch")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Origen_Ch"))
            Me.TablaImportar.Item(NoCheque.Index, j).Value = ds.Tables(0).Rows(j)("No_Cheque")
            Me.TablaImportar.Item(FechaC.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Ch")

            Me.TablaImportar.Item(CuentaBancos.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Bancos")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Bancos"))
            Me.TablaImportar.Item(CtaCheque.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Cheques")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Cheques"))
            Me.TablaImportar.Item(ProvA.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Provision_Acreedor")) = True, 0, ds.Tables(0).Rows(j)("Provision_Acreedor"))
            Me.TablaImportar.Item(ProvP.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Provision_Proveedor")) = True, 0, ds.Tables(0).Rows(j)("Provision_Proveedor"))
            Me.TablaImportar.Item(Dif.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Diferencia")) = True, 0, ds.Tables(0).Rows(j)("Diferencia"))

            Try
                If Trim(ds.Tables(0).Rows(j)("Tipo_Poliza").ToString) <> "" Then
                    Fila.Cells(TipoPoliza.Index).Value = Trim(ds.Tables(0).Rows(j)("Tipo_Poliza"))
                End If
            Catch ex As Exception

            End Try

            Me.TablaImportar.Item(ImpG.Index, j).Value = ds.Tables(0).Rows(j)("Imp_Grabado")
            Me.TablaImportar.Item(ImpEx.Index, j).Value = ds.Tables(0).Rows(j)("Imp_Exento")
            Me.TablaImportar.Item(IVAR.Index, j).Value = ds.Tables(0).Rows(j)("IVA_real")
            Me.TablaImportar.Item(PagoA.Index, j).Value = ds.Tables(0).Rows(j)("Prc_Pago_Acumulado")
            Me.TablaImportar.Item(TReal.Index, j).Value = ds.Tables(0).Rows(j)("Total_Real")
            Me.TablaImportar.Item(Mone.Index, j).Value = ds.Tables(0).Rows(j)("Moneda")
            Me.TablaImportar.Item(UCambaria.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Utilidad_Cambiaria")) = True, 0, ds.Tables(0).Rows(j)("Utilidad_Cambiaria"))
            Me.TablaImportar.Item(PCambiaria.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Perdida_Cambiaria")) = True, 0, ds.Tables(0).Rows(j)("Perdida_Cambiaria"))
            Me.TablaImportar.Item(ImpD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Devolucion")) = True, 0, ds.Tables(0).Rows(j)("Imp_Devolucion"))
            Me.TablaImportar.Item(CuntaDev.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Devolucion")

            Me.TablaImportar.Item(NumPol.Index, j).Value = ds.Tables(0).Rows(j)("numpol")

            Me.TablaImportar.Item(RISR.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Retenido_ISR")) = True, 0, ds.Tables(0).Rows(j)("Retenido_ISR"))
            Me.TablaImportar.Item(RIVA.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Retenido_IVA")) = True, 0, ds.Tables(0).Rows(j)("Retenido_IVA"))

            'Ajutes y Anticipos
            Me.TablaImportar.Item(Anti.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Anticipos")) = True, 0, ds.Tables(0).Rows(j)("Anticipos"))
            Me.TablaImportar.Item(CtaAnti.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cta_Anticipos")) = True, "", ds.Tables(0).Rows(j)("Cta_Anticipos"))
            Me.TablaImportar.Item(Ajus.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ajuste")) = True, 0, ds.Tables(0).Rows(j)("Ajuste"))
            Me.TablaImportar.Item(CtaAjustes.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cta_Ajuste")) = True, "", ds.Tables(0).Rows(j)("Cta_Ajuste"))
            Me.TablaImportar.Item(ImpProvis.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Provision")) = True, 0, ds.Tables(0).Rows(j)("Imp_Provision"))


            Try
                If Trim(ds.Tables(0).Rows(j)("Cta_Orden")) <> "" Then
                    Fila.Cells(CtaOrden.Index).Value = Me.CtaOrden.Items(Obtener_indexCtaOrden(Trim(IIf(IsDBNull(ds.Tables(0).Rows(j)("Cta_Orden")) = True, "NO", ds.Tables(0).Rows(j)("Cta_Orden")))))
                End If
            Catch ex As Exception

            End Try

            Try
                If Trim(ds.Tables(0).Rows(j)("Cta_Terceros").ToString) <> "" Then
                    Fila.Cells(CtaAcreedoresTerceros.Index).Value = Trim(ds.Tables(0).Rows(j)("Cta_Terceros"))
                End If

            Catch ex As Exception

            End Try
            Me.TablaImportar.Item(AcreedoresTerceros.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Terceros")) = True, 0, ds.Tables(0).Rows(j)("Imp_Terceros"))


            frm.Barra.Value = j
        Next
        frm.Close()
    End Sub

    Private Sub Buscar_xml(ByVal Id_Empresa As Integer, ByVal periodo As String)
        Dim sql As String = " SELECT "
        sql &= " 	Id_Registro_Xml,	Verificado_Asoc,	Estado_SAT,	Version,	Tipo,	Fecha_Emision,"
        sql &= " 	Fecha_Timbrado,	EstadoPago,	FechaPago,	Serie,	Folio,	UUID,	UUID_Relacion,	RFC_Emisor,"
        sql &= " 	Nombre_Emisor,	LugarDeExpedicion,	RFC_Receptor,	Nombre_Receptor,	ResidenciaFiscal,	NumRegIdTrib,"
        sql &= " 	UsoCFDI,Retenido_IVA + Retenido_ISR  + Total -IVA_16 -IVA_8 AS SubTotal,	Descuento,	Total_IEPS,	IVA_16,	Retenido_IVA,	Retenido_ISR,	ISH,	Total,"
        sql &= " 	TotalOriginal,	Total_Trasladados,	Total_Retenidos,	Total_LocalTrasladado,	Total_LocalRetenido,	Complemento,"
        sql &= " 	Moneda,	Tipo_De_Cambio,	Metodos_de_Pago.descripcion   ,	Metodo_de_Pago ,	NumCtaPago,	Condicion_de_Pago,	Conceptos,"
        sql &= " 	Combustible,	IEPS_3,	IEPS_6,	IEPS_7,	IEPS_8,	IEPS_9,	IEPS_26,	IEPS_30,	IEPS_53,	IEPS_160,"
        sql &= " 	Archivo_XML,	Direccion_Emisor,	Localidad_Emisor,	Direccion_Receptor,	Localidad_Receptor,	Autorizada,	Consecutivo_Carga,"
        sql &= " 	Id_Empresa,	Carga_Procesada,	ID_poliza,Letra_Sat,	Nombre_cuenta,	Xml_Sat.Clave,	Anio_Contable,	Mes_Contable,	Imp_Efectivo,	Cuenta_Efectivo,"
        sql &= " 	Imp_Transf,	Banco_Origen,	Cuenta_Origen,	Banco_Destino,Cuenta_Destino,	Fecha_Transaccion,	Imp_Cheque,"
        sql &= " 	Nom_Banco_Ch,	Cuenta_Origen_Ch,	No_Cheque,	Fecha_Ch, Cuenta_Cheques ,	Cuenta_Bancos,	Provision_Acreedor,	Provision_Proveedor,"
        sql &= " 	Diferencia,	Tipo_Poliza,	Imp_Grabado,	Imp_Exento,	IVA_real,	Prc_Pago_Acumulado,	Total_Real,	Utilidad_Cambiaria,"
        sql &= " 	Perdida_Cambiaria,	Imp_Devolucion,	Cuenta_Devolucion 	,Numpol,	RIRS,	RIVA,Ajuste , Anticipos, Cta_Ajuste, Cta_Anticipos,Imp_Provision ,Cta_Orden,"
        sql &= " Imp_Transf1,        Banco_Origen1,	 Cuenta_Origen1,	 Banco_Destino1, Cuenta_Destino1 	,	 Fecha_Transaccion1,	 Cuenta_Bancos1 "
        sql &= " ,Imp_Transf2	 ,Banco_Origen2,	 Cuenta_Origen2	, Banco_Destino2	 , Cuenta_Destino2	, Fecha_Transaccion2,Cuenta_Bancos2	"
        sql &= " ,Imp_Transf3	 ,Banco_Origen3,	 Cuenta_Origen3,	 Banco_Destino3,Cuenta_Destino3	,	 Fecha_Transaccion3,	 Cuenta_Bancos3	 "
        sql &= " ,Imp_Transf4	 ,Banco_Origen4,	 Cuenta_Origen4	, Banco_Destino4	 , Cuenta_Destino4	,Fecha_Transaccion4, Cuenta_Bancos4 "
        sql &= " ,Imp_Transf5 ,Banco_Origen5,	 Cuenta_Origen5,	 Banco_Destino5,Cuenta_Destino5	,	 Fecha_Transaccion5,	 Cuenta_Bancos5	"
        sql &= " ,Imp_Transf6	 ,Banco_Origen6,	 Cuenta_Origen6,	 Banco_Destino6,Cuenta_Destino7	,	 Fecha_Transaccion6, Cuenta_Bancos6	"
        sql &= " ,Imp_Transf7	 ,Banco_Origen7,	 Cuenta_Origen7,	 Banco_Destino7,Cuenta_Destino7	,	 Fecha_Transaccion7,	 Cuenta_Bancos7,"
        sql &= " Imp_Transf8	 ,Banco_Origen8,	 Cuenta_Origen8,	 Banco_Destino8,Cuenta_Destino8	, Fecha_Transaccion8,	 Cuenta_Bancos8	"
        sql &= " ,Imp_Transf9	 ,Banco_Origen9,	 Cuenta_Origen9,	 Banco_Destino9,Cuenta_Destino9	,	 Fecha_Transaccion9,	 Cuenta_Bancos9,"
        sql &= " Imp_Transf10,	 Banco_Origen10,	 Cuenta_Origen10, Banco_Destino10,Cuenta_Destino10	, Fecha_Transaccion10,	 Cuenta_Bancos10, Cta_Terceros,Imp_Terceros,IVA_8,IEPS_30_4"
        sql &= " FROM dbo.Xml_Sat inner join Metodos_de_Pago on Metodos_de_Pago.clave = Xml_Sat.FormaDePago   where  tipo = 'Factura' and emitidas= " & Eventos.Bool2(False) & " and Id_Empresa =" & Id_Empresa & " and ID_poliza IS NULL AND  (Carga_Procesada =0	OR Carga_Procesada IS NULL) " & periodo & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaImportar.RowCount = 1
            Dim Tipo As DataSet = Eventos.Obtener_DS(" Select convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
            If Tipo.Tables(0).Rows.Count > 0 Then
                Me.TipoPoliza.DataSource = Tipo.Tables(0)
                Me.TipoPoliza.DisplayMember = Tipo.Tables(0).Columns(0).Caption.ToString
            End If
            Dim Ctaor As DataSet = Eventos.Obtener_DS(" Select 'SI' as Clave  UNION sELECT 'NO' as Clave")
            If Ctaor.Tables(0).Rows.Count > 0 Then
                If Me.CtaOrden.Items.Count = 0 Then
                    For i As Integer = 0 To Ctaor.Tables(0).Rows.Count - 1
                        Me.CtaOrden.Items.Add(Trim(Ctaor.Tables(0).Rows(i)("Clave")))
                    Next
                Else
                    Me.CtaOrden.Items.Clear()
                    For i As Integer = 0 To Ctaor.Tables(0).Rows.Count - 1
                        Me.CtaOrden.Items.Add(Trim(Ctaor.Tables(0).Rows(i)("Clave")))
                    Next
                End If
            End If

            'Acreedores Terceros
            Dim Cuentas As DataSet = Eventos.Obtener_DS(" SELECT  rtrim(Nivel1)  + '-'+  rtrim(Nivel2) + '-'+  rtrim(Nivel3) + '-'+  rtrim(Nivel4) + '/'+  Descripcion AS Alias FROM Catalogo_de_Cuentas WHERE   Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='2050' and nivel2 = '0002' and nivel3= '0003' and nivel4 >0  order by Alias ")
            If Cuentas.Tables(0).Rows.Count > 0 Then
                Me.CtaAcreedoresTerceros.DataSource = Cuentas.Tables(0)
                Me.CtaAcreedoresTerceros.DisplayMember = Cuentas.Tables(0).Columns(0).Caption.ToString
            End If

            Cargar_bancos("Transf")
            Cargar_bancos("Cheq")
            Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count




            Dato = ds
        Else

            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay registros para procesar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
    Private Function Obtener_IndexCampo(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer = -1
        Select Case Len(valor)
            Case 4
                valor = valor & "-0000-0000-0000"
            Case 9
                valor = valor & "-0000-0000"
            Case 14
                valor = valor & "-0000"
            Case 20
                valor = valor
        End Select
        For i As Integer = 0 To Col.Items.Count - 1
            If valor = Trim(Col.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next

        Return Indice
    End Function
    Private Sub Buscar_xml_Complemento(ByVal Id_Empresa As Integer, ByVal periodo As String)
        Dim sql As String = "SELECT Id_Xml_Complemento, Verificado_Asoc, Estado_SAT, Version, Tipo, Fecha_Emision, "
        sql &= " Serie, Folio, UUID, RFC_Emisor, Nombre_Emisor, RFC_Receptor, Nombre_Receptor, UsoCFDI, "
        sql &= " FechaPago, Metodos_de_Pago.Descripcion, Moneda, Monto, UUIDRel, "
        sql &= "  Num_Operacion, Cuenta_Destino, Cuenta_Origen, RfcEmisorCtaDestino, RfcEmisorCtaOrigen, "
        sql &= "  NomBancoOrdExtranjero, TipoCadPago, CadPago, Conceptos, Archivo_XML, Total, Consecutivo_Carga, "
        sql &= "  Id_Empresa, Carga_Procesada, ID_poliza ,Letra_Sat,	Nombre_cuenta,	Xml_Complemento.Clave,	Anio_Contable,	Mes_Contable,	Imp_Efectivo,	Cuenta_Efectivo,
	              Imp_Transf,	Banco_Origen,	Cuenta_Origen,	Banco_Destino,	Fecha_Transaccion,	Imp_Cheque,
	              Nom_Banco_Ch,	Cuenta_Origen_Ch,	No_Cheque,	Fecha_Ch,	Cuenta_Bancos,	Provision_Acreedor,	Provision_Proveedor,
	              Diferencia,	Tipo_Poliza,	Imp_Grabado,	Imp_Exento,	IVA_real,	Prc_Pago_Acumulado,	Total_Real,	Utilidad_Cambiaria,
	              Perdida_Cambiaria,	Imp_Devolucion,	Cuenta_Devolucion ,Numpol,	RIRS,	RIVA"
        sql &= " FROM   Xml_Complemento INNER JOIN Metodos_de_Pago ON Metodos_de_Pago.clave = Xml_Complemento.FormaDePago where Emitidas= " & Eventos.Bool2(False) & " and Id_Empresa =" & Id_Empresa & " and ID_poliza IS NULL AND  (Carga_Procesada =0	OR Carga_Procesada IS NULL)  " & periodo & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim a As Integer = Me.TablaImportar.RowCount - 1
            Me.TablaImportar.RowCount = Me.TablaImportar.Rows.Count + ds.Tables(0).Rows.Count

            Me.Barra.Maximum = ds.Tables(0).Rows.Count - 1
            Me.Barra.Minimum = 0
            Me.Barra.Value1 = 0
            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(j)
                Me.TablaImportar.Item(0, j).Value = False
                Me.TablaImportar.Item(Id_Xml.Index, j).Value = ds.Tables(0).Rows(j)("Id_Xml_Complemento")
                Me.TablaImportar.Item(Tip.Index, j).Value = ds.Tables(0).Rows(j)("Tipo")
                Me.TablaImportar.Item(Fecha_Emi.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Emision")
                Me.TablaImportar.Item(UUI.Index, j).Value = ds.Tables(0).Rows(j)("UUID")
                Me.TablaImportar.Item(UCFDI.Index, j).Value = ds.Tables(0).Rows(j)("UsoCFDI")
                Me.TablaImportar.Item(RFCE.Index, j).Value = ds.Tables(0).Rows(j)("RFC_Emisor")
                Me.TablaImportar.Item(NomEmisor.Index, j).Value = ds.Tables(0).Rows(j)("Nombre_Emisor")
                Me.TablaImportar.Item(Conc.Index, j).Value = ds.Tables(0).Rows(j)("Conceptos")
                Me.TablaImportar.Item(MPago.Index, j).Value = ds.Tables(0).Rows(j)("Descripcion")


                Me.TablaImportar.Item(Tot.Index, j).Value = ds.Tables(0).Rows(j)("Monto")



                Me.TablaImportar.Item(LetraS.Index, j).Value = ds.Tables(0).Rows(j)("Letra_Sat")
                Me.TablaImportar.Item(NCuenta.Index, j).Value = ds.Tables(0).Rows(j)("Nombre_cuenta")
                Try
                    If Trim(ds.Tables(0).Rows(j)("Clave")) <> "" Then
                        Fila.Cells(LetraContabilidadComplemento.Index).Value = Trim(ds.Tables(0).Rows(j)("Clave"))

                    End If

                Catch ex As Exception

                End Try
                ' cargar bancos

                Me.TablaImportar.Item(anio.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Anio_Contable")) = True, Now.Year, ds.Tables(0).Rows(j)("Anio_Contable"))
                Me.TablaImportar.Item(Mes.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Mes_Contable")) = True, Now.Month, ds.Tables(0).Rows(j)("Mes_Contable"))
                Me.TablaImportar.Item(ImpEf.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Efectivo")) = True, 0, ds.Tables(0).Rows(j)("Imp_Efectivo"))
                Me.TablaImportar.Item(CuentasEfectivo.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Efectivo")
                Me.TablaImportar.Item(ImpT.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Transf")) = True, 0, ds.Tables(0).Rows(j)("Imp_Transf"))
                Me.TablaImportar.Item(BancoOrigen.Index, j).Value = ds.Tables(0).Rows(j)("Banco_Origen")
                Me.TablaImportar.Item(CuentaO.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Origen")
                Me.TablaImportar.Item(Bancodestino.Index, j).Value = ds.Tables(0).Rows(j)("Banco_Destino")
                Me.TablaImportar.Item(Fechat.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Transaccion")
                Me.TablaImportar.Item(ImpC.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Cheque")) = True, 0, ds.Tables(0).Rows(j)("Imp_Cheque"))
                Me.TablaImportar.Item(BancosCheques.Index, j).Value = ds.Tables(0).Rows(j)("Nom_Banco_Ch")
                Me.TablaImportar.Item(CuentaC.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Origen_Ch")
                Me.TablaImportar.Item(NoCheque.Index, j).Value = ds.Tables(0).Rows(j)("No_Cheque")
                Me.TablaImportar.Item(FechaC.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Ch")
                Me.TablaImportar.Item(CuentaBancos.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Bancos")
                Me.TablaImportar.Item(ProvA.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Provision_Acreedor")) = True, 0, ds.Tables(0).Rows(j)("Provision_Acreedor"))
                Me.TablaImportar.Item(ProvP.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Provision_Proveedor")) = True, 0, ds.Tables(0).Rows(j)("Provision_Proveedor"))
                Me.TablaImportar.Item(Dif.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Diferencia")) = True, 0, ds.Tables(0).Rows(j)("Diferencia"))

                Try
                    If Trim(ds.Tables(0).Rows(j)("Tipo_Poliza")) <> "" Then
                        Fila.Cells(TipoPoliza.Index).Value = Me.TipoPoliza.Items(Obtener_index2(Trim(ds.Tables(0).Rows(j)("Tipo_Poliza"))))
                        ' Me.TablaImportar.Item(tipopoliza.INDEX, j).Value = Me.TipoPoliza.Items(Obtener_index(ds.Tables(0).Rows(j)("Tipo_Poliza")))
                    End If

                Catch ex As Exception

                End Try
                '   Me.TablaImportar.Item(tipopoliza.INDEX, j).Value = ds.Tables(0).Rows(j)("Tipo_Poliza")
                Me.TablaImportar.Item(ImpG.Index, j).Value = ds.Tables(0).Rows(j)("Imp_Grabado")
                Me.TablaImportar.Item(ImpEx.Index, j).Value = ds.Tables(0).Rows(j)("Imp_Exento")
                Me.TablaImportar.Item(IVAR.Index, j).Value = ds.Tables(0).Rows(j)("IVA_real")
                Me.TablaImportar.Item(PagoA.Index, j).Value = ds.Tables(0).Rows(j)("Prc_Pago_Acumulado")
                Me.TablaImportar.Item(TReal.Index, j).Value = ds.Tables(0).Rows(j)("Total_Real")
                Me.TablaImportar.Item(Mone.Index, j).Value = ds.Tables(0).Rows(j)("Moneda")
                Me.TablaImportar.Item(UCambaria.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Utilidad_Cambiaria")) = True, 0, ds.Tables(0).Rows(j)("Utilidad_Cambiaria"))
                Me.TablaImportar.Item(PCambiaria.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Perdida_Cambiaria")) = True, 0, ds.Tables(0).Rows(j)("Perdida_Cambiaria"))
                Me.TablaImportar.Item(ImpD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Devolucion")) = True, 0, ds.Tables(0).Rows(j)("Imp_Devolucion"))
                Me.TablaImportar.Item(CuntaDev.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Devolucion")

                Me.TablaImportar.Item(NumPol.Index, j).Value = ds.Tables(0).Rows(j)("numpol")
                Me.TablaImportar.Item(ImpD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("RIRS")) = True, 0, ds.Tables(0).Rows(j)("RIRS"))
                Me.TablaImportar.Item(ImpD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("RIVA")) = True, 0, ds.Tables(0).Rows(j)("RIVA"))

                If Me.Barra.Value1 = Me.Barra.Maximum Then
                    Me.Barra.Minimum = 0
                    Me.Cursor = Cursors.Arrow
                    MessageBox.Show("Pagos Cargados ...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Barra.Value1 = 0
                Else
                    Me.Barra.Value1 += 1
                End If
            Next
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay registros para procesar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub

    Private Sub Liberar_Proceso(ByVal i As Integer)
        Dim contador As Integer = 0
        Dim cantidad As Decimal = Me.TablaImportar.Item(ImpProvis.Index, i).Value
        cantidad = cantidad + Me.TablaImportar.Item(UCambaria.Index, i).Value
        cantidad = cantidad - Me.TablaImportar.Item(PCambiaria.Index, i).Value
        'Calcula la diferencia en el registro
        Dim Retencion As Decimal = 0
        Retencion = Me.TablaImportar.Item(RISR.Index, i).Value + Me.TablaImportar.Item(RIVA.Index, i).Value
        Me.TablaImportar.Item(Dif.Index, i).Value = Math.Round(Calcula_diferencia(IIf(IsDBNull(Me.TablaImportar.Item(TReal.Index, i).Value) = True, 0, Me.TablaImportar.Item(TReal.Index, i).Value) - Retencion, Me.TablaImportar.Item(ImpEf.Index, i).Value, Me.TablaImportar.Item(ImpC.Index, i).Value, (Convert.ToDecimal(Me.TablaImportar.Item(ImpT.Index, i).Value) + Convert.ToDecimal(Me.TablaImportar.Item(ImpT2.Index, i).Value) + Convert.ToDecimal(Me.TablaImportar.Item(ImpT3.Index, i).Value) + Convert.ToDecimal(Me.TablaImportar.Item(ImpT4.Index, i).Value) + Convert.ToDecimal(Me.TablaImportar.Item(ImpT5.Index, i).Value) + Convert.ToDecimal(Me.TablaImportar.Item(ImpT6.Index, i).Value) + Convert.ToDecimal(Me.TablaImportar.Item(ImpT7.Index, i).Value) + Convert.ToDecimal(Me.TablaImportar.Item(AcreedoresTerceros.Index, i).Value)), Me.TablaImportar.Item(Ajus.Index, i).Value, Me.TablaImportar.Item(Anti.Index, i).Value, cantidad), 2)
        If Me.TablaImportar.Item(Dif.Index, i).Value <> 0 Then
            Me.TablaImportar.Item(Dif.Index, i).Style.BackColor = Color.Red
        Else
            Me.TablaImportar.Item(Dif.Index, i).Style.BackColor = Color.Green
        End If

        If Trim(UCase(Me.lblce.Text)) = "TRUE" Then ' Bloquera filas de Contabilidad electronica
            Try

                If Me.TablaImportar.Item(Dif.Index, i).Value > 0 Or Me.TablaImportar.Item(TipoPoliza.Index, i).Value = Nothing Or IIf(IsDBNull(Me.TablaImportar.Item(NCuenta.Index, i).Value) = True, "", Me.TablaImportar.Item(NCuenta.Index, i).Value) = "" Or IIf(IsDBNull(Me.TablaImportar.Item(NumPol.Index, i).Value) = True, "", Me.TablaImportar.Item(NumPol.Index, i).Value.ToString) = "" Then
                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                Else
                    Try

                        If Me.TablaImportar.Item(ImpT.Index, i).Value > 0 And Me.TablaImportar.Item(ImpC.Index, i).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, i).Value = 0 Then ' Bloqueo transferencia
                            If Me.TablaImportar.Item(BancoOrigen.Index, i).Value = Nothing Or Me.TablaImportar.Item(Bancodestino.Index, i).Value = Nothing Or Me.TablaImportar.Item(CuentaO.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaBD.Index, i).Value = Nothing Or Me.TablaImportar.Item(Fechat.Index, i).Value = Nothing Then
                                Me.TablaImportar.Item(Aplic.Index, i).Value = False
                            Else
                                Me.TablaImportar.Item(Aplic.Index, i).Value = True
                            End If
                            ' transferencias1
                            If Me.TablaImportar.Item(ImpT2.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT2.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If

                            ' transferencias2
                            If Me.TablaImportar.Item(ImpT3.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT3.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            ' transferencias3
                            If Me.TablaImportar.Item(ImpT4.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT3.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If

                            If Me.TablaImportar.Item(ImpT5.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT5.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            If Me.TablaImportar.Item(ImpT6.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT6.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            If Me.TablaImportar.Item(ImpT7.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT7.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                        ElseIf Me.TablaImportar.Item(ImpC.Index, i).Value > 0 And Me.TablaImportar.Item(ImpT.Index, i).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, i).Value = 0 Then ' Bloqueo cheques
                            If Me.TablaImportar.Item(BancosCheques.Index, i).Value = Nothing Or Me.TablaImportar.Item(CuentaC.Index, i).Value = Nothing Or Me.TablaImportar.Item(NoCheque.Index, i).Value = Nothing Or Me.TablaImportar.Item(FechaC.Index, i).Value = Nothing Then
                                Me.TablaImportar.Item(Aplic.Index, i).Value = False
                            Else
                                Me.TablaImportar.Item(Aplic.Index, i).Value = True
                            End If
                        ElseIf Me.TablaImportar.Item(ImpC.Index, i).Value > 0 And Me.TablaImportar.Item(ImpT.Index, i).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, i).Value = 0 Then ' AMbos
                            If Me.TablaImportar.Item(BancoOrigen.Index, i).Value = Nothing Or Me.TablaImportar.Item(Bancodestino.Index, i).Value = Nothing Or Me.TablaImportar.Item(CuentaO.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaBD.Index, i).Value = Nothing Or Me.TablaImportar.Item(Fechat.Index, i).Value = Nothing Then
                                Me.TablaImportar.Item(Aplic.Index, i).Value = False
                            Else
                                Me.TablaImportar.Item(Aplic.Index, i).Value = True
                            End If
                            ' transferencias1
                            If Me.TablaImportar.Item(ImpT2.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT2.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If

                            ' transferencias2
                            If Me.TablaImportar.Item(ImpT3.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT3.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            ' transferencias3
                            If Me.TablaImportar.Item(ImpT4.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT3.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            If Me.TablaImportar.Item(ImpT5.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT5.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            If Me.TablaImportar.Item(ImpT6.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT6.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            If Me.TablaImportar.Item(ImpT7.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT7.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            If Me.TablaImportar.Item(BancosCheques.Index, i).Value = Nothing Or Me.TablaImportar.Item(CuentaC.Index, i).Value = Nothing Or Me.TablaImportar.Item(NoCheque.Index, i).Value = Nothing Or Me.TablaImportar.Item(FechaC.Index, i).Value = Nothing Then
                                Me.TablaImportar.Item(Aplic.Index, i).Value = False
                            Else
                                Me.TablaImportar.Item(Aplic.Index, i).Value = True
                            End If

                        ElseIf Me.TablaImportar.Item(ImpC.Index, i).Value > 0 And Me.TablaImportar.Item(ImpT.Index, i).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, i).Value > 0 Then ' AMbos



                            If Me.TablaImportar.Item(BancoOrigen.Index, i).Value = Nothing Or Me.TablaImportar.Item(Bancodestino.Index, i).Value = Nothing Or Me.TablaImportar.Item(CuentaO.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaBD.Index, i).Value = Nothing Or Me.TablaImportar.Item(Fechat.Index, i).Value = Nothing Then
                                Me.TablaImportar.Item(Aplic.Index, i).Value = False
                            Else
                                Me.TablaImportar.Item(Aplic.Index, i).Value = True
                            End If
                            ' transferencias1
                            If Me.TablaImportar.Item(ImpT2.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT2.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT2.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If

                            ' transferencias2
                            If Me.TablaImportar.Item(ImpT3.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT3.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT3.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            ' transferencias3
                            If Me.TablaImportar.Item(ImpT4.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT4.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT3.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            If Me.TablaImportar.Item(ImpT5.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT5.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT5.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            If Me.TablaImportar.Item(ImpT6.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT6.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT6.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            If Me.TablaImportar.Item(ImpT7.Index, i).Value > 0 Then

                                If Me.TablaImportar.Item(BankOT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaOT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(BankDT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaDT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(FT7.Index, i).Value = Nothing Or Me.TablaImportar.Item(CtaT7.Index, i).Value = Nothing Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If
                            If Me.TablaImportar.Item(BancosCheques.Index, i).Value = Nothing Or Me.TablaImportar.Item(CuentaC.Index, i).Value = Nothing Or Me.TablaImportar.Item(NoCheque.Index, i).Value = Nothing Or Me.TablaImportar.Item(FechaC.Index, i).Value = Nothing Then
                                Me.TablaImportar.Item(Aplic.Index, i).Value = False
                            Else
                                Me.TablaImportar.Item(Aplic.Index, i).Value = True
                            End If

                            If Me.TablaImportar.Item(AcreedoresTerceros.Index, i).Value <> 0 Then
                                If IIf(IsDBNull(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, i).Value) = True, "", Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, i).Value) = "" Then
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = False
                                Else
                                    Me.TablaImportar.Item(Aplic.Index, i).Value = True
                                End If
                            End If


                        Else
                            Me.TablaImportar.Item(Aplic.Index, i).Value = True
                        End If
                        If Me.TablaImportar.Item(Ajus.Index, i).Value <> 0 Then ' Bloqueo Ajuste
                            If IIf(IsDBNull(Me.TablaImportar.Item(CtaAjustes.Index, i).Value) = True, "", Me.TablaImportar.Item(CtaAjustes.Index, i).Value) = "" Then
                                Me.TablaImportar.Item(Aplic.Index, i).Value = False
                            Else
                                Me.TablaImportar.Item(Aplic.Index, i).Value = True
                            End If
                        End If

                        If Me.TablaImportar.Item(Anti.Index, i).Value <> 0 Then ' Bloqueo Anticipo
                            If IIf(IsDBNull(Me.TablaImportar.Item(CtaAnti.Index, i).Value) = True, "", Me.TablaImportar.Item(CtaAnti.Index, i).Value) = "" Then
                                Me.TablaImportar.Item(Aplic.Index, i).Value = False
                            Else
                                Me.TablaImportar.Item(Aplic.Index, i).Value = True
                            End If
                        End If
                    Catch ex As Exception
                        Me.TablaImportar.Item(Aplic.Index, i).Value = False
                    End Try

                End If

            Catch ex As Exception

            End Try
        Else
            'sin contabilidad electronica
            If Me.TablaImportar.Item(Dif.Index, i).Value > 0 Or Me.TablaImportar.Item(TipoPoliza.Index, i).Value = Nothing Or IIf(IsDBNull(Me.TablaImportar.Item(NCuenta.Index, i).Value) = True, "", Me.TablaImportar.Item(NCuenta.Index, i).Value) = "" Or IIf(IsDBNull(Me.TablaImportar.Item(NumPol.Index, i).Value) = True, "", Me.TablaImportar.Item(NumPol.Index, i).Value) = "" Then
                Me.TablaImportar.Item(Aplic.Index, i).Value = False
            Else
                Me.TablaImportar.Item(Aplic.Index, i).Value = True
            End If
            Try
                If Me.TablaImportar.Item(Ajus.Index, i).Value <> 0 Then ' Bloqueo Ajuste
                    If IIf(IsDBNull(Me.TablaImportar.Item(CtaAjustes.Index, i).Value) = True, "", Me.TablaImportar.Item(CtaAjustes.Index, i).Value) = "" Then
                        Me.TablaImportar.Item(Aplic.Index, i).Value = False
                    Else
                        Me.TablaImportar.Item(Aplic.Index, i).Value = True
                    End If
                End If

                If Me.TablaImportar.Item(Anti.Index, i).Value <> 0 Then ' Bloqueo Anticipo
                    If IIf(IsDBNull(Me.TablaImportar.Item(CtaAnti.Index, i).Value) = True, "", Me.TablaImportar.Item(CtaAnti.Index, i).Value) = "" Then
                        Me.TablaImportar.Item(Aplic.Index, i).Value = False
                    Else
                        Me.TablaImportar.Item(Aplic.Index, i).Value = True
                    End If
                End If
            Catch ex As Exception

            End Try

        End If

        For s As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            If Me.TablaImportar.Item(0, s).Value = True Then
                contador = contador + 1
            End If
        Next
        If contador > 0 Then
            Me.Cmd_Procesar.Enabled = True
        End If
    End Sub

    Private Function Calcula_Diferencia(ByVal total As Decimal, ByVal monto_Efectivo As Decimal, ByVal monto_cheque As Decimal, ByVal monto_Transferencia As Decimal, ByVal ajuste As Decimal, ByVal Anticipo As Decimal, ByVal Provision As Decimal
                                   )
        Dim Diferencia As Decimal = 0
        Diferencia = total - (monto_Efectivo + monto_cheque + monto_Transferencia + ajuste + Anticipo + Provision)
        Return Diferencia
    End Function
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

    Private Function Obtener_index2(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.TipoPoliza.Items.Count - 1
            If valor = Trim(Me.TipoPoliza.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexB(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BancoOrigen.Items.Count - 1
            If valor = Trim(Me.BancoOrigen.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function

    Private Function Obtener_indexBancoOrigenT2(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BankOT2.Items.Count - 1
            If valor = Trim(Me.BankOT2.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexBancoOrigenT3(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BankOT3.Items.Count - 1
            If valor = Trim(Me.BankOT3.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice


    End Function
    Private Function Obtener_indexBancoOrigenT4(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BankOT4.Items.Count - 1
            If valor = Trim(Me.BankOT4.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexBancoOrigenT5(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BankOT5.Items.Count - 1
            If valor = Trim(Me.BankOT5.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexBancoOrigenT6(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BankOT6.Items.Count - 1
            If valor = Trim(Me.BankOT6.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexBancoOrigenT7(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BankOT7.Items.Count - 1
            If valor = Trim(Me.BankOT7.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexBT(ByVal valor As String)

        Dim Indice As Integer = 1000
        For i As Integer = 0 To Me.BancoOrigen.Items.Count - 1

            Dim largo As Integer = Len(Trim(Me.BancoOrigen.Items(i)))
            Dim posicion As Integer = InStr(1, Trim(Me.BancoOrigen.Items(i)), "-", CompareMethod.Binary)
            Dim Al As String = Trim(Me.BancoOrigen.Items(i)).Substring(posicion, largo - posicion)



            If Al = valor Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function


    'Private Function Obtener_indexd(ByVal valor As String)

    '    Dim Indice As Integer
    '    For i As Integer = 0 To Me.Bancodestino.Items.Count - 1
    '        If valor = Trim(Me.Bancodestino.Items(i)) Then
    '            Indice = i
    '            Exit For
    '        End If
    '    Next
    '    Return Indice

    'End Function
    Private Function Obtener_indexbN(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BancosCheques.Items.Count - 1
            If valor = Trim(Me.BancosCheques.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexCtaOrden(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.CtaOrden.Items.Count - 1
            If valor = Trim(Me.CtaOrden.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexCtaOrdenC(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.CtaOrdenC.Items.Count - 1
            If valor = Trim(Me.CtaOrdenC.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function

    Private Sub TablaImportar_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellClick

        Dim columna As Integer = Me.TablaImportar.CurrentCell.ColumnIndex
        Dim Nombre As String
        Nombre = Me.TablaImportar.Columns.Item(Me.TablaImportar.CurrentCell.ColumnIndex).Name.ToString
        Me.LstTexto.cargar(" Select '','' ")

        Select Case Nombre
            Case "ContabilizacionC"
                ' Me.LstTexto.Cargar(" Select id_Contabilidad, rtrim(Clave) as Clave from Letras_Contabilidad where Id_Empresa = " & Me.lstCliente.SelectItem & " ")
                Me.LstTexto.Cargar(" Select Id_Clave, rtrim(Clave) as Clave from ClaveEgresos where ClaveEgresos.Id_Empresa = " & Me.lstCliente.SelectItem & " ")
                Me.LstTexto.SelectText = ""
            Case "CuentasEfectivo"
                Me.LstTexto.cargar(" Select Id_cat_cuentas, rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1010' and Nivel2 > 0 and Nivel3 > 0 ")
                Me.LstTexto.SelectText = ""
            Case "CuentaBancos"
                Me.LstTexto.cargar(" Select Id_cat_cuentas, rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1020'and Nivel2 > 0 and Nivel3 > 0  ")
                Me.LstTexto.SelectText = ""
            Case "BancosCheques"
                Me.LstTexto.cargar("select Id_Bancos_Clientes, rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias from Bancos_Clientes INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_Clientes.Id_Banco where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%Cheq%'")
                Me.LstTexto.SelectText = ""
            Case "BancoOrigen"
                Me.LstTexto.cargar(" select Id_Bancos_Clientes, rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias from Bancos_Clientes INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_Clientes.Id_Banco where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%Transf%'")
                Me.LstTexto.SelectText = ""
            Case "Bancodestino"
                'Me.LstTexto.cargar(" SELECT DISTINCT Bancos.Id_Banco, CONVERT(NVARCHAR, Bancos_RFC.Id_Banco, 103) + '-' + Bancos.Nombre AS Banco   FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco WHERE  (Bancos_RFC.Id_Empresa  = " & Me.lstCliente.SelectItem & ")")
                'Me.LstTexto.SelectText = ""
            Case "TipoPoliza"
                Me.LstTexto.cargar(" Select Id_Tipo_Pol_Sat,convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
                Me.LstTexto.SelectText = ""
            Case "CtaAjustes"
                Me.LstTexto.cargar("Select  Id_cat_cuentas,  rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " ")
                Me.LstTexto.SelectText = ""
            Case "CtaAnti"
                Me.LstTexto.cargar("Select Id_cat_cuentas,  rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1070' and Nivel2 > 0 and Nivel3 > 0 ")
                Me.LstTexto.SelectText = ""
        End Select
    End Sub
    Private Sub LstTexto_Enters() Handles LstTexto.Enters
        Try

            If Me.TablaImportar.Rows.Count > 0 Then
                For Each Fila As DataGridViewRow In TablaImportar.Rows
                    If Fila.Cells(ContabilizacionC.Index).Selected = True Then
                        Try
                            If Me.LstTexto.SelectText = "" Then
                                Fila.Cells(ContabilizacionC.Index).Value = ""
                            Else
                                Fila.Cells(ContabilizacionC.Index).Value = Me.LstTexto.SelectText.Trim()
                            End If

                        Catch ex As Exception

                        End Try
                        Dim ee As New System.Windows.Forms.DataGridViewCellEventArgs(ContabilizacionC.Index, Fila.Index)
                        'Me.TablaImportar.CurrentCell = Me.TablaImportar.Rows(Fila.Index).Cells(ContabilizacionC.Index)
                        Me.TablaImportar_CellEndEdit(Me.TablaImportar, ee)
                    ElseIf Fila.Cells(BancoOrigen.Index).Selected = True Then
                        Try
                            If Me.LstTexto.SelectText = "" Then
                                Fila.Cells(BancoOrigen.Index).Value = ""

                            Else
                                If Obtener_indexB(Me.LstTexto.SelectText) = 0 Then
                                    Fila.Cells(BancoOrigen.Index).Value = ""
                                Else
                                    Dim s As String = Me.BancoOrigen.Items(1)
                                    Fila.Cells(BancoOrigen.Index).Value = Me.BancoOrigen.Items(Obtener_indexB(Me.LstTexto.SelectText))
                                End If
                            End If

                        Catch ex As Exception

                        End Try
                        Dim ee As New System.Windows.Forms.DataGridViewCellEventArgs(BancoOrigen.Index, Fila.Index)
                        Me.TablaImportar_CellEndEdit(Me.TablaImportar, ee)
                    ElseIf Fila.Cells(Bancodestino.Index).Selected = True Then
                        'Try
                        '    Dim s As String = Me.Bancodestino.Items(1)
                        '    Fila.Cells(Bancodestino.Index).Value = Me.Bancodestino.Items(Obtener_indexd(Me.LstTexto.SelectText))
                        'Catch ex As Exception

                        'End Try
                    ElseIf Fila.Cells(TipoPoliza.Index).Selected = True Then
                        If Me.LstTexto.SelectText = "" Then
                            Fila.Cells(TipoPoliza.Index).Value = ""
                        Else
                            Try
                                'If Obtener_index2(Me.LstTexto.SelectText) < 0 Then
                                '    Fila.Cells(TipoPoliza.Index).Value = ""
                                'Else
                                '    Dim s As String = Me.TipoPoliza.Items(1)
                                Fila.Cells(TipoPoliza.Index).Value = Me.LstTexto.SelectText
                                ' End If

                            Catch ex As Exception

                            End Try
                        End If

                        Dim ee As New System.Windows.Forms.DataGridViewCellEventArgs(TipoPoliza.Index, Fila.Index)
                        Me.TablaImportar_CellEndEdit(Me.TablaImportar, ee)
                    Else
                        ' For I As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                        For j As Integer = 0 To Me.TablaImportar.Columns.Count - 1
                            If Me.TablaImportar.Item(j, Fila.Index).Selected = True And (j <> ContabilizacionC.Index And j <> BancoOrigen.Index And j <> TipoPoliza.Index) Then
                                Me.TablaImportar.Item(j, Fila.Index).Value = Me.LstTexto.SelectText
                            ElseIf Me.TablaImportar.Item(j, Fila.Index).Selected = True And (j = ContabilizacionC.Index And j = BancoOrigen.Index And j = TipoPoliza.Index) Then
                                Me.TablaImportar.Item(j, Fila.Index).Value = ""
                            End If
                        Next
                        Dim ee As New System.Windows.Forms.DataGridViewCellEventArgs(TipoPoliza.Index, Fila.Index)
                        Me.TablaImportar_CellEndEdit(Me.TablaImportar, ee)
                        ' Next
                    End If

                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        'If Me.TablaImportar.Rows.Count > 0 Then
        If Me.TcRecibidas.SelectedIndex = 0 Then
            'Verificar la columna del click secundario
            If Me.TablaImportar.CurrentCell.ColumnIndex = Me.NCuenta.Index Then 'Cuentas del Cargo

                Dim ds As DataSet = Eventos.Obtener_DS("select Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta ,Rtrim(Descripcion)  as DES from catalogo_de_cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & "     and rfc= '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "' order by cta")
                Dim actividad(,) As String
                ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim cadena As String = ds.Tables(0).Rows(i)("Cuenta")
                    Dim posi As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim cuantos As Integer = Len(cadena) - Len(cadena.Substring(0, posi))
                    Dim consecutivo As String = cadena.Substring(posi, cuantos)
                    Dim Sql As String = " select Rtrim(Descripcion) as Cuenta from catalogo_de_cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and Nivel1= '" & consecutivo.ToString.Substring(0, 4) & "' and nivel2= '" & consecutivo.ToString.Substring(4, 4) & "'  and nivel3 ='" & consecutivo.ToString.Substring(8, 4) & "'  and nivel4= '0000'"
                    Dim ds2 As DataSet = Eventos.Obtener_DS(Sql)
                    If Trim(ds.Tables(0).Rows(i)("DES")) = Trim(ds2.Tables(0).Rows(0)("Cuenta")) Then
                        actividad(0, i) = ds.Tables(0).Rows(i)("Cuenta")
                    Else
                        actividad(0, i) = ds2.Tables(0).Rows(0)("Cuenta") & " " & ds.Tables(0).Rows(i)(0)
                    End If
                    ' actividad(0, i) = ds2.Tables(0).Rows(0)("Cuenta") & " " & ds.Tables(0).Rows(i)(0)
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
                Me.TablaImportar.Item(NCuenta.Index, Me.TablaImportar.CurrentRow.Index).Value = descrip
            ElseIf Me.TablaImportar.CurrentCell.ColumnIndex = CuentasEfectivo.Index Then 'Cuentas del Efectivo

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
                Me.TablaImportar.Item(CuentasEfectivo.Index, Me.TablaImportar.CurrentRow.Index).Value = descrip
            ElseIf Me.TablaImportar.CurrentCell.ColumnIndex = CtaAnti.Index Then 'Cuentas del Anticipos
                Dim ds As DataSet
                If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
                    If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value = "C" Or Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value = "CPP" Then
                        ds = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1200' and Nivel2 > 0   ORDER BY cta")

                    ElseIf Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value = "GG" Or Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value = "GGPP" Then
                        ds = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1210' and Nivel2 > 0   ORDER BY cta")

                    End If
                End If



                '  ds = Eventos.Obtener_DS("Select rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta, cuenta as cta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1070' and Nivel2 > 0 and Nivel3 > 0 order by cta")
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
                Me.TablaImportar.Item(CtaAnti.Index, Me.TablaImportar.CurrentRow.Index).Value = descrip
            ElseIf Me.TablaImportar.CurrentCell.ColumnIndex = CtaAjustes.Index Then 'Cuentas del Ajuste



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
                Me.TablaImportar.Item(CtaAjustes.Index, Me.TablaImportar.CurrentRow.Index).Value = descrip
            End If
        ElseIf Me.TcRecibidas.SelectedIndex = 1 Then
            If Me.TablaD.CurrentCell.ColumnIndex = Me.NomCuentaD.Index Then 'Cuentas del Cargo
                Dim ds As DataSet = Eventos.Obtener_DS("select Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta,Rtrim(Descripcion) as DES from catalogo_de_cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & "  and rfc= '" & Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value & "' order by cta")
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
                    '  actividad(0, i) = ds2.Tables(0).Rows(0)("Cuenta") & " " & ds.Tables(0).Rows(i)(0)
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
                Me.TablaD.Item(NomCuentaD.Index, Me.TablaD.CurrentRow.Index).Value = descrip

            ElseIf Me.TablaD.CurrentCell.ColumnIndex = CtaEfD.Index Then 'Cuentas del Efectivo

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
                Me.TablaD.Item(CtaEfD.Index, Me.TablaD.CurrentRow.Index).Value = descrip

            ElseIf Me.TablaD.CurrentCell.ColumnIndex = CtaAntiD.Index Then 'Cuentas del Anticipos

                Dim ds As DataSet = Eventos.Obtener_DS("Select rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta, cuenta as cta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1070' and Nivel2 > 0 and Nivel3 > 0 order by cta")
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
                Me.TablaD.Item(CtaAntiD.Index, Me.TablaD.CurrentRow.Index).Value = descrip

            ElseIf Me.TablaD.CurrentCell.ColumnIndex = CtaAD.Index Then 'Cuentas del Ajuste

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
                Me.TablaD.Item(CtaAD.Index, Me.TablaD.CurrentRow.Index).Value = descrip
            End If
        ElseIf Me.TcRecibidas.SelectedIndex = 2 Then
            If Me.TablaC.CurrentCell.ColumnIndex = Me.NomCtaComplemento.Index Then 'Cuentas del Cargo
                Dim ds As DataSet = Eventos.Obtener_DS("select Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta,Rtrim(Descripcion) as DES from catalogo_de_cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " rfc= '" & Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & "' order by cta")
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
                Me.TablaC.Item(NomCtaComplemento.Index, Me.TablaC.CurrentRow.Index).Value = descrip

            ElseIf Me.TablaC.CurrentCell.ColumnIndex = CtaEfComplemento.Index Then 'Cuentas del Efectivo

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
                Me.TablaC.Item(CtaEfComplemento.Index, Me.TablaC.CurrentRow.Index).Value = descrip

            ElseIf Me.TablaC.CurrentCell.ColumnIndex = CtaAntiComplemento.Index Then 'Cuentas del Anticipos
                Dim ds As DataSet
                If Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value <> Nothing Then
                    If Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value = "C" Or Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value = "CPP" Then
                        ds = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and Nivel1 ='1200' and Nivel2 > 0   ORDER BY cta")
                    ElseIf Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value = "GG" Or Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value = "GGPP" Then
                        ds = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and Nivel1 ='1210' and Nivel2 > 0   ORDER BY cta")
                    End If
                End If


                ' ds = Eventos.Obtener_DS("Select rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta, cuenta as cta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1070' and Nivel2 > 0 and Nivel3 > 0 order by cta")




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
                Me.TablaC.Item(CtaAntiComplemento.Index, Me.TablaC.CurrentRow.Index).Value = descrip

            ElseIf Me.TablaC.CurrentCell.ColumnIndex = CtaAjusComplemento.Index Then 'Cuentas del Ajuste

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
                Me.TablaC.Item(CtaAjusComplemento.Index, Me.TablaC.CurrentRow.Index).Value = descrip
            End If
        End If

    End Sub
    Private Sub TablaImportar_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellEndEdit

        'Calcula cuenta cargos
        Try
            Finalizamanaual(e.RowIndex)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Finalizamanaual(ByVal I As Integer)
        Try

            If Me.TablaImportar.Item(ContabilizacionC.Index, I).Value <> Nothing Then
                If Me.TablaImportar.Item(ContabilizacionC.Index, I).Value.ToString.Contains("PP") Then
                    Me.TablaImportar.Item(ProvA.Index, I).Value = 0
                    Me.TablaImportar.Item(ProvP.Index, I).Value = 0
                End If
                Try
                    If Me.TablaImportar.Item(NCuenta.Index, I).Value.ToString() <> "" Then
                    Else


                        Dim Nueva As String = CuentaTipo(Me.TablaImportar.Item(RFCE.Index, I).Value, Me.TablaImportar.Item(ContabilizacionC.Index, I).Value, I)
                        'Me.TablaImportar.Item(NCuenta.Index, Me.TablaImportar.CurrentRow.Index).Value = Cuenta_cargos(Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value, Trim(Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value))
                        Me.TablaImportar.Item(NCuenta.Index, I).Value = Nueva
                        Me.TablaImportar.Item(LetraS.Index, I).Value = Eventos.Calcula_letraSat(Me.TablaImportar.Item(MPago.Index, I).Value, Me.TablaImportar.Item(UCFDI.Index, I).Value)
                    End If
                Catch ex As Exception

                End Try
            Else

                Me.TablaImportar.Item(NCuenta.Index, I).Value = ""
            End If

            'CALCULAR CUENTA DE EFECTIVO
            If Me.TablaImportar.Item(ImpEf.Index, I).Value > 0 Then
                If Candado_Importe_Efectivo(I) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo

                    Me.TablaImportar.Item(CuentasEfectivo.Index, I).Value = Cuenta_Efectivo()
                    Me.TablaImportar.Columns(CuentasEfectivo.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    If MessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.Titulo_APP, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaImportar.Item(CuentasEfectivo.Index, I).Value = Cuenta_Efectivo()
                        Me.TablaImportar.Columns(CuentasEfectivo.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaImportar.Item(ImpEf.Index, I).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaImportar.Item(CuentasEfectivo.Index, I).Value = ""
            End If

            'CALCULAR CUENTA DE Ajustes
            If Me.TablaImportar.Item(Ajus.Index, I).Value <> 0 Then
                If Candado_Importe_Efectivo(I) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo

                    Me.TablaImportar.Item(CtaAjustes.Index, I).Value = Cuenta_Efectivo()
                    Me.TablaImportar.Columns(CtaAjustes.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    If MessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.Titulo_APP, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaImportar.Item(CtaAjustes.Index, I).Value = Cuenta_Efectivo()
                        Me.TablaImportar.Columns(CtaAjustes.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaImportar.Item(Ajus.Index, I).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaImportar.Item(CtaAjustes.Index, I).Value = ""
            End If
            'CALCULAR CUENTA DE anticipos
            If Me.TablaImportar.Item(Anti.Index, I).Value > 0 Then

                Me.TablaImportar.Item(CtaAnti.Index, I).Value = Cuenta_Anticipo()
                Me.TablaImportar.Columns(CtaAnti.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Else
                Me.TablaImportar.Item(CtaAnti.Index, I).Value = ""
            End If



            ' Importe de Transferencia
            If Me.TablaImportar.Item(ImpT.Index, I).Value > 0 Then
                ' BUSCAR BANCOS EN AUTOMATICO DEL DESTINO
                If Trim(Me.TablaImportar.Item(BancoOrigen.Index, I).Value) <> Nothing Then
                    If Trim(Me.TablaImportar.Item(CuentaO.Index, I).Value) <> "" And Me.TablaImportar.Item(CuentaBancos.Index, I).Value.ToString <> "" Then
                    Else
                        Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BancoOrigen.Index, I).Value, "-", CompareMethod.Binary)
                        Dim cuantos As Integer = Len(Me.TablaImportar.Item(BancoOrigen.Index, I).Value) - Len(Me.TablaImportar.Item(BancoOrigen.Index, I).Value.Substring(0, posi))
                        Dim Al As String = Me.TablaImportar.Item(BancoOrigen.Index, I).Value.Substring(posi, cuantos)
                        Me.TablaImportar.Item(CuentaO.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)

                        Me.TablaImportar.Item(CuentaBancos.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                    End If

                End If

                If Trim(Me.TablaImportar.Item(Bancodestino.Index, I).Value) <> Nothing Then
                    Try
                        If Trim(Me.TablaImportar.Item(CtaBD.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaBD.Index, I).Value) <> "0" Then
                            Dim largo As Integer = Len(Me.TablaImportar.Item(Bancodestino.Index, I).Value)
                            Dim cadena As String = Trim(Me.TablaImportar.Item(Bancodestino.Index, I).Value.ToString.Substring(largo - 3, 3))
                            largo = Len(Me.TablaImportar.Item(CtaBD.Index, I).Value)
                            Dim Al As String = Me.TablaImportar.Item(CtaBD.Index, I).Value.Substring(largo - 3, 3)
                            If cadena <> Al Then
                                largo = InStr(1, Me.TablaImportar.Item(Bancodestino.Index, I).Value, "-", CompareMethod.Binary)
                                Al = Me.TablaImportar.Item(Bancodestino.Index, I).Value.Substring(0, largo - 1)
                                Me.TablaImportar.Item(CtaBD.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                            End If
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaImportar.Item(Bancodestino.Index, I).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaImportar.Item(Bancodestino.Index, I).Value.Substring(0, posi - 1)
                            Me.TablaImportar.Item(CtaBD.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                        End If
                    Catch ex As Exception

                    End Try
                Else
                    Me.TablaImportar.Item(Bancodestino.Index, I).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaImportar.Item(RFCE.Index, I).Value)
                    Try
                        If Trim(Me.TablaImportar.Item(CtaBD.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaBD.Index, I).Value) <> "0" Then
                            Dim largo As Integer = Len(Me.TablaImportar.Item(Bancodestino.Index, I).Value)
                            Dim cadena As String = Trim(Me.TablaImportar.Item(Bancodestino.Index, I).Value.ToString.Substring(largo - 3, 3))
                            largo = Len(Me.TablaImportar.Item(CtaBD.Index, I).Value)
                            Dim Al As String = Me.TablaImportar.Item(CtaBD.Index, I).Value.Substring(largo - 3, 3)
                            If cadena <> Al Then
                                largo = InStr(1, Me.TablaImportar.Item(Bancodestino.Index, I).Value, "-", CompareMethod.Binary)
                                Al = Me.TablaImportar.Item(Bancodestino.Index, I).Value.Substring(0, largo - 1)
                                Me.TablaImportar.Item(CtaBD.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                            End If
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaImportar.Item(Bancodestino.Index, I).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaImportar.Item(Bancodestino.Index, I).Value.Substring(0, posi - 1)
                            Me.TablaImportar.Item(CtaBD.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                        End If
                    Catch ex As Exception

                    End Try
                End If

                'Tipos Polizas

                If Trim(Me.TablaImportar.Item(TipoPoliza.Index, I).Value) <> Nothing Then
                    If Trim(Me.TablaImportar.Item(BancoOrigen.Index, I).Value) <> Nothing Then
                    Else
                        Dim fila As DataGridViewRow = Me.TablaImportar.Rows(I)
                        Try
                            If Trim(Me.TablaImportar.Item(TipoPoliza.Index, I).Value) <> "" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(TipoPoliza.Index, I).Value)
                                Dim posicion As Integer = InStr(1, Me.TablaImportar.Item(TipoPoliza.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(TipoPoliza.Index, I).Value.Substring(posicion + 1, largo - posicion - 1)
                                Dim indice As Integer = Obtener_indexBT(Al)
                                If indice = 1000 Then
                                Else
                                    fila.Cells(BancoOrigen.Index).Value = Me.BancoOrigen.Items(indice)
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                End If


                If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                    Me.TablaImportar.Columns(Fechat.Index).Visible = True
                    Me.TablaImportar.Columns(BancoOrigen.Index).Visible = False
                    Me.TablaImportar.Columns(CuentaO.Index).Visible = False
                    Me.TablaImportar.Columns(Bancodestino.Index).Visible = False
                    Me.TablaImportar.Columns(CtaBD.Index).Visible = False
                End If


                If Me.TablaImportar.Item(ImpT2.Index, I).Value > 0 Then
                    If Trim(Me.TablaImportar.Item(BankOT2.Index, I).Value) <> Nothing Then
                        If Trim(Me.TablaImportar.Item(CtaOT2.Index, I).Value) <> "" Then
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankOT2.Index, I).Value, "-", CompareMethod.Binary)
                            Dim cuantos As Integer = Len(Me.TablaImportar.Item(BankOT2.Index, I).Value) - Len(Me.TablaImportar.Item(BankOT2.Index, I).Value.Substring(0, posi))
                            Dim Al As String = Me.TablaImportar.Item(BankOT2.Index, I).Value.Substring(posi, cuantos)
                            Me.TablaImportar.Item(CtaOT2.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                            Me.TablaImportar.Item(CtaT2.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)
                        End If

                    End If

                    If Trim(Me.TablaImportar.Item(BankDT2.Index, I).Value) <> Nothing Then
                        Try
                            If Trim(Me.TablaImportar.Item(CtaDT2.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaDT2.Index, I).Value) <> "0" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(BankDT2.Index, I).Value)
                                Dim cadena As String = Trim(Me.TablaImportar.Item(BankDT2.Index, I).Value.ToString.Substring(largo - 3, 3))
                                largo = Len(Me.TablaImportar.Item(CtaDT2.Index, I).Value)
                                Dim Al As String = Me.TablaImportar.Item(CtaDT2.Index, I).Value.Substring(largo - 3, 3)
                                If cadena <> Al Then
                                    largo = InStr(1, Me.TablaImportar.Item(BankDT2.Index, I).Value, "-", CompareMethod.Binary)
                                    Al = Me.TablaImportar.Item(BankDT2.Index, I).Value.Substring(0, largo - 1)
                                    Me.TablaImportar.Item(CtaDT2.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                                End If
                            Else
                                Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankDT2.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(BankDT2.Index, I).Value.Substring(0, posi - 1)
                                Me.TablaImportar.Item(CtaDT2.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                            End If
                        Catch ex As Exception

                        End Try
                    Else
                        Me.TablaImportar.Item(BankDT2.Index, I).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaImportar.Item(RFCE.Index, I).Value)
                        Try
                            If Trim(Me.TablaImportar.Item(CtaDT2.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaDT2.Index, I).Value) <> "0" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(BankDT2.Index, I).Value)
                                Dim cadena As String = Trim(Me.TablaImportar.Item(BankDT2.Index, I).Value.ToString.Substring(largo - 3, 3))
                                largo = Len(Me.TablaImportar.Item(CtaDT2.Index, I).Value)
                                Dim Al As String = Me.TablaImportar.Item(CtaDT2.Index, I).Value.Substring(largo - 3, 3)
                                If cadena <> Al Then
                                    largo = InStr(1, Me.TablaImportar.Item(BankDT2.Index, I).Value, "-", CompareMethod.Binary)
                                    Al = Me.TablaImportar.Item(BankDT2.Index, I).Value.Substring(0, largo - 1)
                                    Me.TablaImportar.Item(CtaDT2.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                                End If
                            Else
                                Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankDT2.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(BankDT2.Index, I).Value.Substring(0, posi - 1)
                                Me.TablaImportar.Item(CtaDT2.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                    If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                        Me.TablaImportar.Columns(FT2.Index).Visible = True
                        Me.TablaImportar.Columns(BankOT2.Index).Visible = False
                        Me.TablaImportar.Columns(CtaOT2.Index).Visible = False
                        Me.TablaImportar.Columns(BankDT2.Index).Visible = False
                        Me.TablaImportar.Columns(CtaDT2.Index).Visible = False
                    End If
                End If


                If Me.TablaImportar.Item(ImpT3.Index, I).Value > 0 Then
                    If Trim(Me.TablaImportar.Item(BankOT3.Index, I).Value) <> Nothing Then
                        If Trim(Me.TablaImportar.Item(CtaOT3.Index, I).Value) <> "" Then
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankOT3.Index, I).Value, "-", CompareMethod.Binary)
                            Dim cuantos As Integer = Len(Me.TablaImportar.Item(BankOT3.Index, I).Value) - Len(Me.TablaImportar.Item(BankOT3.Index, I).Value.Substring(0, posi))
                            Dim Al As String = Me.TablaImportar.Item(BankOT3.Index, I).Value.Substring(posi, cuantos)
                            Me.TablaImportar.Item(CtaOT3.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                            Me.TablaImportar.Item(CtaT3.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)
                        End If

                    End If

                    If Trim(Me.TablaImportar.Item(BankDT3.Index, I).Value) <> Nothing Then
                        Try
                            If Trim(Me.TablaImportar.Item(CtaDT3.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaDT3.Index, I).Value) <> "0" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(BankDT3.Index, I).Value)
                                Dim cadena As String = Trim(Me.TablaImportar.Item(BankDT3.Index, I).Value.ToString.Substring(largo - 3, 3))
                                largo = Len(Me.TablaImportar.Item(CtaDT3.Index, I).Value)
                                Dim Al As String = Me.TablaImportar.Item(CtaDT3.Index, I).Value.Substring(largo - 3, 3)
                                If cadena <> Al Then
                                    largo = InStr(1, Me.TablaImportar.Item(BankDT3.Index, I).Value, "-", CompareMethod.Binary)
                                    Al = Me.TablaImportar.Item(BankDT3.Index, I).Value.Substring(0, largo - 1)
                                    Me.TablaImportar.Item(CtaDT3.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                                End If
                            Else
                                Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankDT3.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(BankDT3.Index, I).Value.Substring(0, posi - 1)
                                Me.TablaImportar.Item(CtaDT3.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                            End If
                        Catch ex As Exception

                        End Try
                    Else
                        Me.TablaImportar.Item(BankDT3.Index, I).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaImportar.Item(RFCE.Index, I).Value)
                        Try
                            If Trim(Me.TablaImportar.Item(CtaDT3.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaDT3.Index, I).Value) <> "0" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(BankDT3.Index, I).Value)
                                Dim cadena As String = Trim(Me.TablaImportar.Item(BankDT3.Index, I).Value.ToString.Substring(largo - 3, 3))
                                largo = Len(Me.TablaImportar.Item(CtaDT3.Index, I).Value)
                                Dim Al As String = Me.TablaImportar.Item(CtaDT3.Index, I).Value.Substring(largo - 3, 3)
                                If cadena <> Al Then
                                    largo = InStr(1, Me.TablaImportar.Item(BankDT3.Index, I).Value, "-", CompareMethod.Binary)
                                    Al = Me.TablaImportar.Item(BankDT3.Index, I).Value.Substring(0, largo - 1)
                                    Me.TablaImportar.Item(CtaDT3.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                                End If
                            Else
                                Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankDT3.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(BankDT3.Index, I).Value.Substring(0, posi - 1)
                                Me.TablaImportar.Item(CtaDT3.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                    If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                        Me.TablaImportar.Columns(FT3.Index).Visible = True
                        Me.TablaImportar.Columns(BankOT3.Index).Visible = False
                        Me.TablaImportar.Columns(CtaOT3.Index).Visible = False
                        Me.TablaImportar.Columns(BankDT3.Index).Visible = False
                        Me.TablaImportar.Columns(CtaDT3.Index).Visible = False
                    End If
                End If

                If Me.TablaImportar.Item(ImpT4.Index, I).Value > 0 Then
                    If Trim(Me.TablaImportar.Item(BankOT4.Index, I).Value) <> Nothing Then
                        If Trim(Me.TablaImportar.Item(CtaOT4.Index, I).Value) <> "" Then
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankOT4.Index, I).Value, "-", CompareMethod.Binary)
                            Dim cuantos As Integer = Len(Me.TablaImportar.Item(BankOT4.Index, I).Value) - Len(Me.TablaImportar.Item(BankOT4.Index, I).Value.Substring(0, posi))
                            Dim Al As String = Me.TablaImportar.Item(BankOT4.Index, I).Value.Substring(posi, cuantos)
                            Me.TablaImportar.Item(CtaOT4.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                            Me.TablaImportar.Item(CtaT4.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)
                        End If

                    End If

                    If Trim(Me.TablaImportar.Item(BankDT4.Index, I).Value) <> Nothing Then
                        Try
                            If Trim(Me.TablaImportar.Item(CtaDT4.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaDT4.Index, I).Value) <> "0" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(BankDT4.Index, I).Value)
                                Dim cadena As String = Trim(Me.TablaImportar.Item(BankDT4.Index, I).Value.ToString.Substring(largo - 3, 3))
                                largo = Len(Me.TablaImportar.Item(CtaDT4.Index, I).Value)
                                Dim Al As String = Me.TablaImportar.Item(CtaDT4.Index, I).Value.Substring(largo - 3, 3)
                                If cadena <> Al Then
                                    largo = InStr(1, Me.TablaImportar.Item(BankDT4.Index, I).Value, "-", CompareMethod.Binary)
                                    Al = Me.TablaImportar.Item(BankDT4.Index, I).Value.Substring(0, largo - 1)
                                    Me.TablaImportar.Item(CtaDT4.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                                End If
                            Else
                                Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankDT4.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(BankDT4.Index, I).Value.Substring(0, posi - 1)
                                Me.TablaImportar.Item(CtaDT4.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                            End If
                        Catch ex As Exception

                        End Try
                    Else
                        Me.TablaImportar.Item(BankDT4.Index, I).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaImportar.Item(RFCE.Index, I).Value)
                        Try
                            If Trim(Me.TablaImportar.Item(CtaDT4.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaDT4.Index, I).Value) <> "0" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(BankDT4.Index, I).Value)
                                Dim cadena As String = Trim(Me.TablaImportar.Item(BankDT4.Index, I).Value.ToString.Substring(largo - 3, 3))
                                largo = Len(Me.TablaImportar.Item(CtaDT4.Index, I).Value)
                                Dim Al As String = Me.TablaImportar.Item(CtaDT4.Index, I).Value.Substring(largo - 3, 3)
                                If cadena <> Al Then
                                    largo = InStr(1, Me.TablaImportar.Item(BankDT4.Index, I).Value, "-", CompareMethod.Binary)
                                    Al = Me.TablaImportar.Item(BankDT4.Index, I).Value.Substring(0, largo - 1)
                                    Me.TablaImportar.Item(CtaDT4.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                                End If
                            Else
                                Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankDT4.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(BankDT4.Index, I).Value.Substring(0, posi - 1)
                                Me.TablaImportar.Item(CtaDT4.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                    If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                        Me.TablaImportar.Columns(FT4.Index).Visible = True
                        Me.TablaImportar.Columns(BankOT4.Index).Visible = False
                        Me.TablaImportar.Columns(CtaOT4.Index).Visible = False
                        Me.TablaImportar.Columns(BankDT4.Index).Visible = False
                        Me.TablaImportar.Columns(CtaDT4.Index).Visible = False
                    End If
                End If

                If Me.TablaImportar.Item(ImpT5.Index, I).Value > 0 Then
                    If Trim(Me.TablaImportar.Item(BankOT5.Index, I).Value) <> Nothing Then
                        If Trim(Me.TablaImportar.Item(CtaOT5.Index, I).Value) <> "" Then
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankOT5.Index, I).Value, "-", CompareMethod.Binary)
                            Dim cuantos As Integer = Len(Me.TablaImportar.Item(BankOT5.Index, I).Value) - Len(Me.TablaImportar.Item(BankOT5.Index, I).Value.Substring(0, posi))
                            Dim Al As String = Me.TablaImportar.Item(BankOT5.Index, I).Value.Substring(posi, cuantos)
                            Me.TablaImportar.Item(CtaOT5.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                            Me.TablaImportar.Item(CtaT5.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)
                        End If

                    End If

                    If Trim(Me.TablaImportar.Item(BankDT5.Index, I).Value) <> Nothing Then
                        Try
                            If Trim(Me.TablaImportar.Item(CtaDT5.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaDT5.Index, I).Value) <> "0" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(BankDT5.Index, I).Value)
                                Dim cadena As String = Trim(Me.TablaImportar.Item(BankDT5.Index, I).Value.ToString.Substring(largo - 3, 3))
                                largo = Len(Me.TablaImportar.Item(CtaDT5.Index, I).Value)
                                Dim Al As String = Me.TablaImportar.Item(CtaDT5.Index, I).Value.Substring(largo - 3, 3)
                                If cadena <> Al Then
                                    largo = InStr(1, Me.TablaImportar.Item(BankDT5.Index, I).Value, "-", CompareMethod.Binary)
                                    Al = Me.TablaImportar.Item(BankDT5.Index, I).Value.Substring(0, largo - 1)
                                    Me.TablaImportar.Item(CtaDT5.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                                End If
                            Else
                                Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankDT5.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(BankDT5.Index, I).Value.Substring(0, posi - 1)
                                Me.TablaImportar.Item(CtaDT5.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                            End If
                        Catch ex As Exception

                        End Try
                    Else
                        Me.TablaImportar.Item(BankDT5.Index, I).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaImportar.Item(RFCE.Index, I).Value)
                        Try
                            If Trim(Me.TablaImportar.Item(CtaDT5.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaDT5.Index, I).Value) <> "0" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(BankDT5.Index, I).Value)
                                Dim cadena As String = Trim(Me.TablaImportar.Item(BankDT5.Index, I).Value.ToString.Substring(largo - 3, 3))
                                largo = Len(Me.TablaImportar.Item(CtaDT5.Index, I).Value)
                                Dim Al As String = Me.TablaImportar.Item(CtaDT5.Index, I).Value.Substring(largo - 3, 3)
                                If cadena <> Al Then
                                    largo = InStr(1, Me.TablaImportar.Item(BankDT5.Index, I).Value, "-", CompareMethod.Binary)
                                    Al = Me.TablaImportar.Item(BankDT5.Index, I).Value.Substring(0, largo - 1)
                                    Me.TablaImportar.Item(CtaDT5.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                                End If
                            Else
                                Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankDT5.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(BankDT5.Index, I).Value.Substring(0, posi - 1)
                                Me.TablaImportar.Item(CtaDT5.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                    If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                        Me.TablaImportar.Columns(FT5.Index).Visible = True
                        Me.TablaImportar.Columns(BankOT5.Index).Visible = False
                        Me.TablaImportar.Columns(CtaOT5.Index).Visible = False
                        Me.TablaImportar.Columns(BankDT5.Index).Visible = False
                        Me.TablaImportar.Columns(CtaDT5.Index).Visible = False
                    End If
                End If
                If Me.TablaImportar.Item(ImpT6.Index, I).Value > 0 Then
                    If Trim(Me.TablaImportar.Item(BankOT6.Index, I).Value) <> Nothing Then
                        If Trim(Me.TablaImportar.Item(CtaOT6.Index, I).Value) <> "" Then
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankOT6.Index, I).Value, "-", CompareMethod.Binary)
                            Dim cuantos As Integer = Len(Me.TablaImportar.Item(BankOT6.Index, I).Value) - Len(Me.TablaImportar.Item(BankOT6.Index, I).Value.Substring(0, posi))
                            Dim Al As String = Me.TablaImportar.Item(BankOT6.Index, I).Value.Substring(posi, cuantos)
                            Me.TablaImportar.Item(CtaOT6.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                            Me.TablaImportar.Item(CtaT6.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)
                        End If

                    End If

                    If Trim(Me.TablaImportar.Item(BankDT6.Index, I).Value) <> Nothing Then
                        Try
                            If Trim(Me.TablaImportar.Item(CtaDT6.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaDT6.Index, I).Value) <> "0" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(BankDT6.Index, I).Value)
                                Dim cadena As String = Trim(Me.TablaImportar.Item(BankDT6.Index, I).Value.ToString.Substring(largo - 3, 3))
                                largo = Len(Me.TablaImportar.Item(CtaDT6.Index, I).Value)
                                Dim Al As String = Me.TablaImportar.Item(CtaDT6.Index, I).Value.Substring(largo - 3, 3)
                                If cadena <> Al Then
                                    largo = InStr(1, Me.TablaImportar.Item(BankDT6.Index, I).Value, "-", CompareMethod.Binary)
                                    Al = Me.TablaImportar.Item(BankDT6.Index, I).Value.Substring(0, largo - 1)
                                    Me.TablaImportar.Item(CtaDT6.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                                End If
                            Else
                                Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankDT6.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(BankDT6.Index, I).Value.Substring(0, posi - 1)
                                Me.TablaImportar.Item(CtaDT6.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                            End If
                        Catch ex As Exception

                        End Try
                    Else
                        Me.TablaImportar.Item(BankDT6.Index, I).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaImportar.Item(RFCE.Index, I).Value)
                        Try
                            If Trim(Me.TablaImportar.Item(CtaDT6.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaDT6.Index, I).Value) <> "0" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(BankDT6.Index, I).Value)
                                Dim cadena As String = Trim(Me.TablaImportar.Item(BankDT6.Index, I).Value.ToString.Substring(largo - 3, 3))
                                largo = Len(Me.TablaImportar.Item(CtaDT6.Index, I).Value)
                                Dim Al As String = Me.TablaImportar.Item(CtaDT6.Index, I).Value.Substring(largo - 3, 3)
                                If cadena <> Al Then
                                    largo = InStr(1, Me.TablaImportar.Item(BankDT6.Index, I).Value, "-", CompareMethod.Binary)
                                    Al = Me.TablaImportar.Item(BankDT6.Index, I).Value.Substring(0, largo - 1)
                                    Me.TablaImportar.Item(CtaDT6.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                                End If
                            Else
                                Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankDT6.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(BankDT6.Index, I).Value.Substring(0, posi - 1)
                                Me.TablaImportar.Item(CtaDT6.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                    If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                        Me.TablaImportar.Columns(FT6.Index).Visible = True
                        Me.TablaImportar.Columns(BankOT6.Index).Visible = False
                        Me.TablaImportar.Columns(CtaOT6.Index).Visible = False
                        Me.TablaImportar.Columns(BankDT6.Index).Visible = False
                        Me.TablaImportar.Columns(CtaDT6.Index).Visible = False
                    End If
                End If
                If Me.TablaImportar.Item(ImpT7.Index, I).Value > 0 Then
                    If Trim(Me.TablaImportar.Item(BankOT7.Index, I).Value) <> Nothing Then
                        If Trim(Me.TablaImportar.Item(CtaOT7.Index, I).Value) <> "" Then
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankOT7.Index, I).Value, "-", CompareMethod.Binary)
                            Dim cuantos As Integer = Len(Me.TablaImportar.Item(BankOT7.Index, I).Value) - Len(Me.TablaImportar.Item(BankOT7.Index, I).Value.Substring(0, posi))
                            Dim Al As String = Me.TablaImportar.Item(BankOT7.Index, I).Value.Substring(posi, cuantos)
                            Me.TablaImportar.Item(CtaOT7.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                            Me.TablaImportar.Item(CtaT7.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)
                        End If

                    End If

                    If Trim(Me.TablaImportar.Item(BankDT7.Index, I).Value) <> Nothing Then
                        Try
                            If Trim(Me.TablaImportar.Item(CtaDT7.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaDT7.Index, I).Value) <> "0" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(BankDT7.Index, I).Value)
                                Dim cadena As String = Trim(Me.TablaImportar.Item(BankDT7.Index, I).Value.ToString.Substring(largo - 3, 3))
                                largo = Len(Me.TablaImportar.Item(CtaDT7.Index, I).Value)
                                Dim Al As String = Me.TablaImportar.Item(CtaDT7.Index, I).Value.Substring(largo - 3, 3)
                                If cadena <> Al Then
                                    largo = InStr(1, Me.TablaImportar.Item(BankDT7.Index, I).Value, "-", CompareMethod.Binary)
                                    Al = Me.TablaImportar.Item(BankDT7.Index, I).Value.Substring(0, largo - 1)
                                    Me.TablaImportar.Item(CtaDT7.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                                End If
                            Else
                                Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankDT7.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(BankDT7.Index, I).Value.Substring(0, posi - 1)
                                Me.TablaImportar.Item(CtaDT7.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                            End If
                        Catch ex As Exception

                        End Try
                    Else
                        Me.TablaImportar.Item(BankDT7.Index, I).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaImportar.Item(RFCE.Index, I).Value)
                        Try
                            If Trim(Me.TablaImportar.Item(CtaDT7.Index, I).Value) <> "" And Trim(Me.TablaImportar.Item(CtaDT7.Index, I).Value) <> "0" Then
                                Dim largo As Integer = Len(Me.TablaImportar.Item(BankDT7.Index, I).Value)
                                Dim cadena As String = Trim(Me.TablaImportar.Item(BankDT7.Index, I).Value.ToString.Substring(largo - 3, 3))
                                largo = Len(Me.TablaImportar.Item(CtaDT7.Index, I).Value)
                                Dim Al As String = Me.TablaImportar.Item(CtaDT7.Index, I).Value.Substring(largo - 3, 3)
                                If cadena <> Al Then
                                    largo = InStr(1, Me.TablaImportar.Item(BankDT7.Index, I).Value, "-", CompareMethod.Binary)
                                    Al = Me.TablaImportar.Item(BankDT7.Index, I).Value.Substring(0, largo - 1)
                                    Me.TablaImportar.Item(CtaDT7.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                                End If
                            Else
                                Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BankDT7.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaImportar.Item(BankDT7.Index, I).Value.Substring(0, posi - 1)
                                Me.TablaImportar.Item(CtaDT7.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, I).Value & "'", True)
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                    If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                        Me.TablaImportar.Columns(FT7.Index).Visible = True
                        Me.TablaImportar.Columns(BankOT7.Index).Visible = False
                        Me.TablaImportar.Columns(CtaOT7.Index).Visible = False
                        Me.TablaImportar.Columns(BankDT7.Index).Visible = False
                        Me.TablaImportar.Columns(CtaDT7.Index).Visible = False
                    End If
                End If
            End If
            'Importe cheque
            If Me.TablaImportar.Item(ImpC.Index, I).Value > 0 Then
                'Cheques
                Dim posi As Integer = 0
                Dim largo As Integer = 0
                Dim cuantos As Integer = 0
                Dim Al As String = ""
                If Trim(Me.TablaImportar.Item(BancosCheques.Index, I).Value) <> "" Then
                    If Trim(Me.TablaImportar.Item(CuentaC.Index, I).Value) <> "" Then
                        largo = Len(Me.TablaImportar.Item(BancosCheques.Index, I).Value)
                        Dim cadena As String = Trim(Me.TablaImportar.Item(BancosCheques.Index, I).Value.ToString.Substring(largo - 3, 3))
                        largo = Len(Me.TablaImportar.Item(CuentaC.Index, I).Value.ToString)
                        Al = Me.TablaImportar.Item(CuentaC.Index, I).Value.ToString.Substring(largo - 3, 3)
                        If cadena <> Al Then
                            posi = InStr(1, Me.TablaImportar.Item(BancosCheques.Index, I).Value, "-", CompareMethod.Binary)
                            cuantos = Len(Me.TablaImportar.Item(BancosCheques.Index, I).Value) - Len(Me.TablaImportar.Item(BancosCheques.Index, I).Value.Substring(0, posi))
                            Al = Me.TablaImportar.Item(BancosCheques.Index, I).Value.Substring(posi, cuantos)
                            Me.TablaImportar.Item(CuentaC.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                            Me.TablaImportar.Item(CtaCheque.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                        End If

                    Else
                        posi = InStr(1, Me.TablaImportar.Item(BancosCheques.Index, I).Value, "-", CompareMethod.Binary)
                        cuantos = Len(Me.TablaImportar.Item(BancosCheques.Index, I).Value) - Len(Me.TablaImportar.Item(BancosCheques.Index, I).Value.Substring(0, posi))
                        Al = Me.TablaImportar.Item(BancosCheques.Index, I).Value.Substring(posi, cuantos)
                        Me.TablaImportar.Item(CuentaC.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                        Me.TablaImportar.Item(CtaCheque.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                    End If
                Else

                End If
                'Se Muestran Columnas de Contabilidad Electronica
                If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                    Me.TablaImportar.Columns(BancosCheques.Index).Visible = False
                    Me.TablaImportar.Columns(CuentaC.Index).Visible = False
                    Me.TablaImportar.Columns(NoCheque.Index).Visible = False
                    Me.TablaImportar.Columns(FechaC.Index).Visible = True

                End If

            End If




            'candados
            Liberar_Proceso(I)

            Candados(I)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Cargar_bancos(ByVal bancos As String)
        Dim sql As String = " SELECT rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias FROM Bancos_Clientes INNER JOIN Bancos ON Bancos_Clientes.Id_Banco =Bancos.Id_Banco  where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%" & bancos & "%'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If bancos = "Cheq" Then
                'Cheque
                If Me.BancosCheques.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancosCheques.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                Else
                    Try
                        Me.BancosCheques.Items.Clear()
                    Catch ex As Exception

                    End Try

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancosCheques.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                End If
            Else
                'Transferencia
                'Origen
                If Me.BancoOrigen.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancoOrigen.Items.Add(ds.Tables(0).Rows(i)("Alias"))

                    Next
                Else
                    Try
                        Me.BancoOrigen.Items.Clear()
                    Catch ex As Exception

                    End Try


                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancoOrigen.Items.Add(ds.Tables(0).Rows(i)("Alias"))

                    Next
                End If
                If Me.BankOT2.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BankOT2.Items.Add(ds.Tables(0).Rows(i)("Alias"))

                    Next
                Else
                    Try
                        Me.BankOT2.Items.Clear()
                    Catch ex As Exception

                    End Try


                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BankOT2.Items.Add(ds.Tables(0).Rows(i)("Alias"))

                    Next
                End If
            End If
        End If

    End Sub
    Private Sub CargarBancos(ByVal bancos As String)
        Dim sql As String = " SELECT rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias FROM Bancos_Clientes INNER JOIN Bancos ON Bancos_Clientes.Id_Banco =Bancos.Id_Banco  where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%" & bancos & "%'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If bancos = "Cheq" Then
                Me.BancosCheques.DataSource = ds.Tables(0)
                Me.BancosCheques.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            Else
                Me.BancoOrigen.DataSource = ds.Tables(0)
                Me.BancoOrigen.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
                Me.BankOT2.DataSource = ds.Tables(0)
                Me.BankOT2.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
                Me.BankOT3.DataSource = ds.Tables(0)
                Me.BankOT3.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
                Me.BankOT4.DataSource = ds.Tables(0)
                Me.BankOT4.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
                Me.BankOT5.DataSource = ds.Tables(0)
                Me.BankOT5.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
                Me.BankOT6.DataSource = ds.Tables(0)
                Me.BankOT6.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
            End If
        End If

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
    Private Function CuentaTipo(ByVal RFC As String, ByVal Clave As String, ByVal Fila As Integer)
        Dim Tipo As String = " "
        Dim Sql As String = ""
        Dim Cuenta As String

        If Convert.ToDecimal(Me.TablaImportar.Item(ImpProvis.Index, Fila).Value) = Convert.ToDecimal(Me.TablaImportar.Item(TReal.Index, Fila).Value) Then
            If Convert.ToDecimal(Me.TablaImportar.Item(ImpEx.Index, Fila).Value) > Convert.ToDecimal(Me.TablaImportar.Item(ImpG.Index, Fila).Value) Then
                Tipo = " ExentoPPD, NivelEP "
                Cuenta = "ExentoPPD"
            Else
                Tipo = " GravadoPPD, NivelGP "
                Cuenta = "GravadoPPD"
            End If
        Else
            If Convert.ToDecimal(Me.TablaImportar.Item(ImpEx.Index, Fila).Value) > Convert.ToDecimal(Me.TablaImportar.Item(ImpG.Index, Fila).Value) Then
                Tipo = " ExentoPUE, NivelE "
                Cuenta = "ExentoPUE"
            Else
                Tipo = " GravadoPUE, NivelG "
                Cuenta = "GravadoPUE"
            End If
        End If
        Sql = " SELECT " & Tipo & "   FROM  ClaveEgresos WHERE ClaveEgresos.Id_Empresa  = " & Me.lstCliente.SelectItem & " and ClaveEgresos.Clave = '" & Clave & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) > "0000" Then
                Tipo = "  SUBSTRING(ClaveEgresos." & Cuenta & " , 1, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel1 AND SUBSTRING(ClaveEgresos." & Cuenta & " , 5, 4) COLLATE SQL_Latin1_General_CP1_CI_AS  = Catalogo_de_Cuentas.Nivel2 AND SUBSTRING(ClaveEgresos." & Cuenta & " ,9, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel3 "
            ElseIf ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(4, 4) > "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) = "0000" Then 'Tercer Nivel
                Tipo = "  SUBSTRING(ClaveEgresos." & Cuenta & " , 1, 4) COLLATE SQL_Latin1_General_CP1_CI_AS  = Catalogo_de_Cuentas.Nivel1 AND SUBSTRING(ClaveEgresos." & Cuenta & " , 5, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel2   "
            ElseIf ds.Tables(0).Rows(0)(0).ToString().Substring(4, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" Then 'Primer Nivel
                Tipo = "  SUBSTRING(ClaveEgresos." & Cuenta & " , 1, 4) COLLATE SQL_Latin1_General_CP1_CI_AS  = Catalogo_de_Cuentas.Nivel1 "
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

    Private Function CuentasMadres(ByVal Clave As String, ByVal Fila As Integer, ByVal P As Decimal, ByVal t As Decimal, ByVal g As Decimal, ByVal e As Decimal) As DataSet
        Dim Tipo As String = " "
        Dim Sql As String = ""
        Dim Cuenta As String
        Dim CTipo As String = ""
        If Convert.ToDecimal(P) = Convert.ToDecimal(t) Then
            If Convert.ToDecimal(e) > Convert.ToDecimal(g) Then
                Tipo = " ExentoPPD, NivelEP "
                CTipo = "ExentoPUE"
            Else
                Tipo = " GravadoPPD, NivelGP "
                CTipo = "GravadoPPD"
            End If
        Else
            If Convert.ToDecimal(e) > Convert.ToDecimal(g) Then
                Tipo = " ExentoPUE, NivelE "
                CTipo = "ExentoPUE"
            Else
                Tipo = " GravadoPUE, NivelG "
                CTipo = "GravadoPUE"
            End If
        End If
        Sql = " SELECT " & Tipo & "   FROM  ClaveEgresos WHERE ClaveEgresos.Id_Empresa  = " & Me.lstCliente.SelectItem & " and ClaveEgresos.Clave = '" & Clave & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) > "0000" Then
                Tipo = "  SUBSTRING(ClaveEgresos." & CTipo & ", 1, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel1 AND SUBSTRING(ClaveEgresos." & CTipo & ", 5, 4) COLLATE SQL_Latin1_General_CP1_CI_AS= Catalogo_de_Cuentas.Nivel2 AND SUBSTRING(ClaveEgresos." & CTipo & ",9, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel3 "
            ElseIf ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(4, 4) > "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) = "0000" Then 'Tercer Nivel
                Tipo = "  SUBSTRING(ClaveEgresos." & CTipo & ", 1, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel1 AND SUBSTRING(ClaveEgresos." & CTipo & ", 5, 4) COLLATE SQL_Latin1_General_CP1_CI_AS= Catalogo_de_Cuentas.Nivel2   "
            ElseIf ds.Tables(0).Rows(0)(0).ToString().Substring(4, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" Then 'Primer Nivel
                Tipo = "  SUBSTRING(ClaveEgresos." & CTipo & ", 1, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel1 "
            End If
            Sql = "SELECT DISTINCT Rtrim(Catalogo_de_Cuentas.Descripcion) + '-'+ convert(nvarchar,Catalogo_de_Cuentas.cuenta,103) as Cuenta"
            Sql &= " FROM     ClaveEgresos INNER JOIN "
            Sql &= " Catalogo_de_Cuentas ON " & Tipo & " WHERE (Catalogo_de_Cuentas.RFC ='' OR Catalogo_de_Cuentas.RFC = 'NULL' ) AND Catalogo_de_Cuentas.Nivel4= 0 and  (ClaveEgresos.Id_Empresa = " & Me.lstCliente.SelectItem & ") AND (ClaveEgresos.Clave = '" & Clave & "')   "
            ds.Clear()
            ds = Eventos.Obtener_DS(Sql)
        End If
        Return ds
    End Function
    Private Function CuentaTipoC(ByVal RFC As String, ByVal Clave As String, ByVal Fila As Integer)
        Dim Tipo As String = " "
        Dim Sql As String = ""
        Dim Cuenta As String
        Dim CTipo As String = ""

        If Convert.ToDecimal(Me.TablaC.Item(ImpEComplemento.Index, Fila).Value) > Convert.ToDecimal(Me.TablaC.Item(ImpGComplemento.Index, Fila).Value) Then
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
                Tipo = "  SUBSTRING(ClaveEgresos." & CTipo & ", 1, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel1 AND SUBSTRING(ClaveEgresos." & CTipo & ", 5, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel2 AND SUBSTRING(ClaveEgresos." & CTipo & ",9, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel3 "
            ElseIf ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(4, 4) > "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) = "0000" Then 'Tercer Nivel
                Tipo = "  SUBSTRING(ClaveEgresos." & CTipo & ", 1, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel1 AND SUBSTRING(ClaveEgresos." & CTipo & ", 5, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel2   "
            ElseIf ds.Tables(0).Rows(0)(0).ToString().Substring(4, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" Then 'Primer Nivel
                Tipo = "  SUBSTRING(ClaveEgresos." & CTipo & ", 1, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel1 "
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
    Private Function CuentaTipoD(ByVal RFC As String, ByVal Clave As String, ByVal Fila As Integer)
        Dim Tipo As String = " "
        Dim Sql As String = ""
        Dim Cuenta As String
        Dim CTipo As String = ""

        If Convert.ToDecimal(Me.TablaD.Item(ImpEDV.Index, Fila).Value) > Convert.ToDecimal(Me.TablaD.Item(ImpGD.Index, Fila).Value) Then
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
                Tipo = "  SUBSTRING(ClaveEgresos." & CTipo & ", 1, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel1 AND SUBSTRING(ClaveEgresos." & CTipo & ", 5, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel2 AND SUBSTRING(ClaveEgresos." & CTipo & ",9, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel3 "
            ElseIf ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(4, 4) > "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) = "0000" Then 'Tercer Nivel
                Tipo = "  SUBSTRING(ClaveEgresos." & CTipo & ", 1, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel1 AND SUBSTRING(ClaveEgresos." & CTipo & ", 5, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel2   "
            ElseIf ds.Tables(0).Rows(0)(0).ToString().Substring(4, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(8, 4) = "0000" And ds.Tables(0).Rows(0)(0).ToString().Substring(12, 4) = "0000" Then 'Primer Nivel
                Tipo = "  SUBSTRING(ClaveEgresos." & CTipo & ", 1, 4) COLLATE SQL_Latin1_General_CP1_CI_AS = Catalogo_de_Cuentas.Nivel1 "
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
    Private Function Cuenta_cargos(ByVal rfc As String, ByVal letra As String)
        Dim cuenta As String = ""
        'Dim sql As String = " select Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, Rtrim(Descripcion) as DES from catalogo_de_cuentas where  clave= '" & Trim(letra) & "'  and nivel3 >0  and rfc= '" & rfc & "' and Id_Empresa = " & Me.lstCliente.SelectItem & ""
        'Dim ds As DataSet = Eventos.Obtener_DS(sql)
        'If ds.Tables(0).Rows.Count > 0 Then
        '    Dim cadena As String = ds.Tables(0).Rows(0)("Cuenta")
        '    Dim posi As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
        '    Dim cuantos As Integer = Len(cadena) - Len(cadena.Substring(0, posi))
        '    Dim consecutivo As String = cadena.Substring(posi, cuantos)

        '    sql = " select Rtrim(Descripcion) as Cuenta from catalogo_de_cuentas where  Nivel1= '" & consecutivo.ToString.Substring(0, 4) & "' and nivel2= '" & consecutivo.ToString.Substring(4, 4) & "'  and nivel3 ='" & consecutivo.ToString.Substring(8, 4) & "'  and nivel4= '0000'  and Id_Empresa = " & Me.lstCliente.SelectItem & ""

        '    Dim ds2 As DataSet = Eventos.Obtener_DS(sql)

        '    If ds2.Tables(0).Rows.Count > 0 Then
        '        If Trim(ds.Tables(0).Rows(0)("DES")) = Trim(ds2.Tables(0).Rows(0)("Cuenta")) Then
        '            cuenta = ds.Tables(0).Rows(0)("Cuenta")
        '        Else
        '            cuenta = ds2.Tables(0).Rows(0)("Cuenta") & " " & ds.Tables(0).Rows(0)("Cuenta")
        '        End If
        '    Else
        '        cuenta = ds.Tables(0).Rows(0)("Cuenta")
        '    End If
        'Else
        '    cuenta = ""
        'End If








        Return cuenta
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
        Dim Nivel(6)

        Dim ds As DataSet
        'AnticipoGPUE, NivelA,  AnticipoEPUE, NivelAE, IVAAPUE, NivelIA,Negativo
        If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
            Nivel = CuentasAnticiposPue(Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value)
            Try
                If Len(Nivel(0).ToString()) > 4 Then
                    cuenta = "'" & Nivel(0).ToString().Substring(0, 4) & "'"
                End If
            Catch ex As Exception
                cuenta = ""
            End Try
            Try
                If Len(Nivel(2).ToString()) > 4 Then
                    cuenta += ",'" & Nivel(2).ToString().Substring(0, 4) & "'"
                End If
            Catch ex As Exception

            End Try
            ds = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 in (" & cuenta & ") and Nivel2 > 0   ORDER BY cta")

        End If
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
    Private Function Candado_Importe_Efectivo(ByVal i As Integer)
        Dim hacer As Boolean
        If Me.TablaImportar.Item(ImpEf.Index, i).Value > 2000 Then
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
    Private Function Candado_Importe_EfectiC(ByVal i As Integer)
        Dim hacer As Boolean
        If Me.TablaC.Item(ImpEfComplemento.Index, i).Value > 2000 Then

            hacer = False
        Else
            hacer = True
        End If
        Return hacer
    End Function
    Private Sub Cargar_valores_contables()
        If Me.TablaImportar.Rows.Count >= 1 Then
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Text = "Calculando Importes Gravados, Exentos e IVA's por favor espere..."
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.TablaImportar.Rows.Count
            For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                If Trim(Me.TablaImportar.Item(Tot.Index, i).Value) <> "P" Then
                    Try
                        If Trim(Me.TablaImportar.Item(Mone.Index, i).Value.ToString) <> "USD" Then
                            If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, i).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, i).Value) = 0.00 Then
                                If Me.TablaImportar.Item(Tot.Index, i).Value > 0 And Me.TablaImportar.Item(Tot.Index, i).Value <> Nothing Then
                                    'importe gravado
                                    Me.TablaImportar.Item(ImpG.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaImportar.Item(iva8.Index, i).Value) = True, 0, Me.TablaImportar.Item(iva8.Index, i).Value / 0.08), 2)
                                    'importe exento
                                    Me.TablaImportar.Item(ImpEx.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaImportar.Item(Subtotal.Index, i).Value) = True, 0, Me.TablaImportar.Item(Subtotal.Index, i).Value - Me.TablaImportar.Item(ImpG.Index, i).Value), 2)
                                    'Iva_real
                                    Me.TablaImportar.Item(IVAR.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaImportar.Item(ImpG.Index, i).Value) = True, 0, Me.TablaImportar.Item(ImpG.Index, i).Value * 0.08), 2)
                                    'calcula el % Pago Acumulado
                                    If Me.TablaImportar.Item(ImpEx.Index, i).Value < 1 Then
                                        Me.TablaImportar.Item(ImpG.Index, i).Value = Me.TablaImportar.Item(ImpG.Index, i).Value + Me.TablaImportar.Item(ImpEx.Index, i).Value
                                        Me.TablaImportar.Item(ImpEx.Index, i).Value = 0
                                    End If
                                    'Total real
                                    Me.TablaImportar.Item(TReal.Index, i).Value = Me.TablaImportar.Item(ImpG.Index, i).Value + Me.TablaImportar.Item(ImpEx.Index, i).Value + Me.TablaImportar.Item(IVAR.Index, i).Value
                                End If
                            ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, i).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, i).Value) >= 0.00 Then
                                If Me.TablaImportar.Item(Tot.Index, i).Value > 0 And Me.TablaImportar.Item(Tot.Index, i).Value <> Nothing Then
                                    'importe gravado
                                    Me.TablaImportar.Item(ImpG.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaImportar.Item(CIVA.Index, i).Value) = True, 0, Me.TablaImportar.Item(CIVA.Index, i).Value / 0.16), 2)
                                    'importe exento
                                    Me.TablaImportar.Item(ImpEx.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaImportar.Item(Subtotal.Index, i).Value) = True, 0, Me.TablaImportar.Item(Subtotal.Index, i).Value - Me.TablaImportar.Item(ImpG.Index, i).Value), 2)
                                    'Iva_real
                                    Me.TablaImportar.Item(IVAR.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaImportar.Item(ImpG.Index, i).Value) = True, 0, Me.TablaImportar.Item(ImpG.Index, i).Value * 0.16), 2)
                                    'calcula el % Pago Acumulado
                                    If Me.TablaImportar.Item(ImpEx.Index, i).Value < 1 Then
                                        Me.TablaImportar.Item(ImpG.Index, i).Value = Me.TablaImportar.Item(ImpG.Index, i).Value + Me.TablaImportar.Item(ImpEx.Index, i).Value
                                        Me.TablaImportar.Item(ImpEx.Index, i).Value = 0
                                    End If
                                    'Total real
                                    Me.TablaImportar.Item(TReal.Index, i).Value = Me.TablaImportar.Item(ImpG.Index, i).Value + Me.TablaImportar.Item(ImpEx.Index, i).Value + Me.TablaImportar.Item(IVAR.Index, i).Value
                                End If
                            End If
                        Else
                            ' calcular en dolares
                            If Me.TablaImportar.Item(Tot.Index, i).Value > 0 And Me.TablaImportar.Item(Tot.Index, i).Value <> Nothing Then
                                If Calcula_Moneda(Me.TablaImportar.Item(Fecha_Emi.Index, i).Value.ToString.Substring(0, 10)) = 0 Then
                                    Me.TablaImportar.Rows(i).DefaultCellStyle.BackColor = Color.Red
                                End If
                                'importe gravado
                                Me.TablaImportar.Item(ImpG.Index, i).Value = IIf(IsDBNull(Me.TablaImportar.Item(CIVA.Index, i).Value) = True, 0, (Me.TablaImportar.Item(CIVA.Index, i).Value * Calcula_Moneda(Me.TablaImportar.Item(Fecha_Emi.Index, i).Value.ToString.Substring(0, 10))) / 0.16)
                                'importe exento
                                Me.TablaImportar.Item(ImpEx.Index, i).Value = IIf(IsDBNull(Me.TablaImportar.Item(Subtotal.Index, i).Value) = True, 0, (Me.TablaImportar.Item(Subtotal.Index, i).Value * Calcula_Moneda(Me.TablaImportar.Item(Fecha_Emi.Index, i).Value.ToString.Substring(0, 10))) - Me.TablaImportar.Item(ImpG.Index, i).Value)
                                'Iva_real
                                Me.TablaImportar.Item(IVAR.Index, i).Value = IIf(IsDBNull(Me.TablaImportar.Item(ImpG.Index, i).Value) = True, 0, Me.TablaImportar.Item(ImpG.Index, i).Value * 0.16)
                                'calcula el % Pago Acumulado
                                'Total real
                                If Me.TablaImportar.Item(ImpEx.Index, i).Value < 1 Then
                                    Me.TablaImportar.Item(ImpG.Index, i).Value = Me.TablaImportar.Item(ImpG.Index, i).Value + Me.TablaImportar.Item(ImpEx.Index, i).Value
                                    Me.TablaImportar.Item(ImpEx.Index, i).Value = 0
                                End If
                                Me.TablaImportar.Item(TReal.Index, i).Value = Me.TablaImportar.Item(ImpG.Index, i).Value + Me.TablaImportar.Item(ImpEx.Index, i).Value + Me.TablaImportar.Item(IVAR.Index, i).Value
                            End If
                        End If
                    Catch ex As Exception
                        If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, i).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, i).Value) = 0.00 Then
                            If Me.TablaImportar.Item(Tot.Index, i).Value > 0 And Me.TablaImportar.Item(Tot.Index, i).Value <> Nothing Then
                                'importe gravado
                                Me.TablaImportar.Item(ImpG.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaImportar.Item(iva8.Index, i).Value) = True, 0, Me.TablaImportar.Item(iva8.Index, i).Value / 0.08), 2)
                                'importe exento
                                Me.TablaImportar.Item(ImpEx.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaImportar.Item(Subtotal.Index, i).Value) = True, 0, Me.TablaImportar.Item(Subtotal.Index, i).Value - Me.TablaImportar.Item(ImpG.Index, i).Value), 2)
                                'Iva_real
                                Me.TablaImportar.Item(IVAR.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaImportar.Item(ImpG.Index, i).Value) = True, 0, Me.TablaImportar.Item(ImpG.Index, i).Value * 0.08), 2)
                                'calcula el % Pago Acumulado
                                If Me.TablaImportar.Item(ImpEx.Index, i).Value < 1 Then
                                    Me.TablaImportar.Item(ImpG.Index, i).Value = Me.TablaImportar.Item(ImpG.Index, i).Value + Me.TablaImportar.Item(ImpEx.Index, i).Value
                                    Me.TablaImportar.Item(ImpEx.Index, i).Value = 0
                                End If
                                'Total real
                                Me.TablaImportar.Item(TReal.Index, i).Value = Me.TablaImportar.Item(ImpG.Index, i).Value + Me.TablaImportar.Item(ImpEx.Index, i).Value + Me.TablaImportar.Item(IVAR.Index, i).Value
                            End If
                        ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, i).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, i).Value) >= 0.00 Then
                            If Me.TablaImportar.Item(Tot.Index, i).Value > 0 And Me.TablaImportar.Item(Tot.Index, i).Value <> Nothing Then
                                'importe gravado
                                Me.TablaImportar.Item(ImpG.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaImportar.Item(CIVA.Index, i).Value) = True, 0, Me.TablaImportar.Item(CIVA.Index, i).Value / 0.16), 2)
                                'importe exento
                                Me.TablaImportar.Item(ImpEx.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaImportar.Item(Subtotal.Index, i).Value) = True, 0, Me.TablaImportar.Item(Subtotal.Index, i).Value - Me.TablaImportar.Item(ImpG.Index, i).Value), 2)
                                'Iva_real
                                Me.TablaImportar.Item(IVAR.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaImportar.Item(ImpG.Index, i).Value) = True, 0, Me.TablaImportar.Item(ImpG.Index, i).Value * 0.16), 2)
                                'calcula el % Pago Acumulado
                                If Me.TablaImportar.Item(ImpEx.Index, i).Value < 1 Then
                                    Me.TablaImportar.Item(ImpG.Index, i).Value = Me.TablaImportar.Item(ImpG.Index, i).Value + Me.TablaImportar.Item(ImpEx.Index, i).Value
                                    Me.TablaImportar.Item(ImpEx.Index, i).Value = 0
                                End If
                                'Total real
                                Me.TablaImportar.Item(TReal.Index, i).Value = Me.TablaImportar.Item(ImpG.Index, i).Value + Me.TablaImportar.Item(ImpEx.Index, i).Value + Me.TablaImportar.Item(IVAR.Index, i).Value
                            End If
                        End If
                        Me.TablaImportar.Rows(i).DefaultCellStyle.BackColor = Color.LightCoral
                    End Try
                End If
                frm.Barra.Value = i
            Next
            frm.Close()
        End If
    End Sub

    Private Sub Codificar_polizas(ByVal posicion As Integer)
        Dim poliza_Sistema As String = ""
        poliza_Sistema = Calcula_poliza(posicion)
        Dim posi As Integer = InStr(1, poliza_Sistema, "-", CompareMethod.Binary)
        Dim cuantos As Integer = Len(poliza_Sistema) - Len(poliza_Sistema.Substring(0, posi))
        Dim consecutivo As Integer = Val(poliza_Sistema.Substring(posi, cuantos))
        Dim dia As String = ""
        Dim CHK As String = ""
        If Me.TablaImportar.Item(ImpT.Index, posicion).Value > 0 Then
            dia = Me.TablaImportar.Item(Fechat.Index, posicion).Value.ToString.Substring(0, 2)
        ElseIf Me.TablaImportar.Item(ImpEf.Index, posicion).Value > 0 Then
            dia = Me.TablaImportar.Item(Fecha_Emi.Index, posicion).Value.ToString.Substring(0, 2)
        ElseIf Me.TablaImportar.Item(ImpC.Index, posicion).Value > 0 Then
            CHK = "Ch/ " & Me.TablaImportar.Item(NoCheque.Index, posicion).Value.ToString() & " F/ "
            dia = Me.TablaImportar.Item(FechaC.Index, posicion).Value.ToString.Substring(0, 2)
        Else
            dia = Me.TablaImportar.Item(Fecha_Emi.Index, posicion).Value.ToString.Substring(0, 2)
        End If
        If Creapoliza(poliza_Sistema, Me.TablaImportar.Item(anio.Index, posicion).Value, Me.TablaImportar.Item(Mes.Index, posicion).Value, dia,
                   consecutivo, Checa_tipo(Me.TablaImportar.Item(TipoPoliza.Index, posicion).Value, Me.lstCliente.SelectItem),
                   Me.TablaImportar.Item(Fecha_Emi.Index, posicion).Value, Leyenda(Trim(Me.TablaImportar.Item(ContabilizacionC.Index, posicion).Value), CHK) & " " & Trim(Me.TablaImportar.Item(UUI.Index, posicion).Value), "Carga", Me.TablaImportar.Item(NumPol.Index, posicion).Value, Me.TablaImportar.Item(Id_Xml.Index, posicion).Value, False) = True Then

            If Buscafactura(Me.TablaImportar.Item(UUI.Index, posicion).Value, "C") = True Then
                Inserta_Comprobante_Fiscal(poliza_Sistema, Me.TablaImportar.Item(anio.Index, posicion).Value, Me.TablaImportar.Item(Mes.Index, posicion).Value,
                             Me.TablaImportar.Item(RFCE.Index, posicion).Value, Me.TablaImportar.Item(Fecha_Emi.Index, posicion).Value,
                               Me.TablaImportar.Item(UUI.Index, posicion).Value, "Factura " & Trim(Me.TablaImportar.Item(RFCE.Index, posicion).Value) & " C", Me.TablaImportar.Item(TReal.Index, posicion).Value)
            Else
                Edita_Factura(Me.TablaImportar.Item(UUI.Index, posicion).Value, "C", poliza_Sistema)
            End If
            If Me.TablaImportar.Item(ImpEf.Index, posicion).Value > 0 Then
                Inserta_Comprobante_Fiscal_Efectivo(poliza_Sistema, Me.TablaImportar.Item(anio.Index, posicion).Value, Me.TablaImportar.Item(Mes.Index, posicion).Value,
                                Me.TablaImportar.Item(RFCE.Index, posicion).Value, Me.TablaImportar.Item(TipoPoliza.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaImportar.Item(Fecha_Emi.Index, posicion).Value,
                              "", "", "", "", Me.TablaImportar.Item(ImpEf.Index, posicion).Value)
            End If
            If Trim(UCase(Me.lblce.Text)) = "TRUE" Then

                If Me.TablaImportar.Item(ImpT.Index, posicion).Value > 0 Then

                    Dim cadena As String = Me.TablaImportar.Item(BancoOrigen.Index, posicion).Value
                    Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim BO As String = cadena.Substring(0, posil - 1)

                    cadena = Me.TablaImportar.Item(Bancodestino.Index, posicion).Value
                    posil = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim Bd As String = cadena.Substring(0, posil - 1)

                    Inserta_Comprobante_Fiscal_Transf(poliza_Sistema, Me.TablaImportar.Item(anio.Index, posicion).Value, Me.TablaImportar.Item(Mes.Index, posicion).Value,
                                    Me.TablaImportar.Item(RFCE.Index, posicion).Value, Me.TablaImportar.Item(TipoPoliza.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaImportar.Item(Fechat.Index, posicion).Value,
                                  "", BO, Me.TablaImportar.Item(CuentaO.Index, posicion).Value, Me.TablaImportar.Item(UUI.Index, posicion).Value, Me.TablaImportar.Item(ImpT.Index, posicion).Value, Bd, Me.TablaImportar.Item(CtaBD.Index, posicion).Value)
                End If

                If Me.TablaImportar.Item(ImpT2.Index, posicion).Value > 0 Then

                    Dim cadena As String = Me.TablaImportar.Item(BankOT2.Index, posicion).Value
                    Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim BO As String = cadena.Substring(0, posil - 1)

                    cadena = Me.TablaImportar.Item(BankDT2.Index, posicion).Value
                    posil = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim Bd As String = cadena.Substring(0, posil - 1)

                    Inserta_Comprobante_Fiscal_Transf(poliza_Sistema, Me.TablaImportar.Item(anio.Index, posicion).Value, Me.TablaImportar.Item(Mes.Index, posicion).Value,
                                    Me.TablaImportar.Item(RFCE.Index, posicion).Value, Me.TablaImportar.Item(TipoPoliza.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaImportar.Item(FT2.Index, posicion).Value,
                                  "", BO, Me.TablaImportar.Item(CtaOT2.Index, posicion).Value, Me.TablaImportar.Item(UUI.Index, posicion).Value, Me.TablaImportar.Item(ImpT2.Index, posicion).Value, Bd, Me.TablaImportar.Item(CtaDT2.Index, posicion).Value)
                End If


                If Me.TablaImportar.Item(ImpT3.Index, posicion).Value > 0 Then

                    Dim cadena As String = Me.TablaImportar.Item(BankOT3.Index, posicion).Value
                    Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim BO As String = cadena.Substring(0, posil - 1)
                    cadena = Me.TablaImportar.Item(BankDT3.Index, posicion).Value
                    posil = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim Bd As String = cadena.Substring(0, posil - 1)
                    Inserta_Comprobante_Fiscal_Transf(poliza_Sistema, Me.TablaImportar.Item(anio.Index, posicion).Value, Me.TablaImportar.Item(Mes.Index, posicion).Value,
                                    Me.TablaImportar.Item(RFCE.Index, posicion).Value, Me.TablaImportar.Item(TipoPoliza.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaImportar.Item(FT3.Index, posicion).Value,
                                  "", BO, Me.TablaImportar.Item(CtaOT3.Index, posicion).Value, Me.TablaImportar.Item(UUI.Index, posicion).Value, Me.TablaImportar.Item(ImpT3.Index, posicion).Value, Bd, Me.TablaImportar.Item(CtaDT3.Index, posicion).Value)
                End If



                If Me.TablaImportar.Item(ImpT4.Index, posicion).Value > 0 Then

                    Dim cadena As String = Me.TablaImportar.Item(BankOT4.Index, posicion).Value
                    Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim BO As String = cadena.Substring(0, posil - 1)
                    cadena = Me.TablaImportar.Item(BankDT4.Index, posicion).Value
                    posil = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim Bd As String = cadena.Substring(0, posil - 1)
                    Inserta_Comprobante_Fiscal_Transf(poliza_Sistema, Me.TablaImportar.Item(anio.Index, posicion).Value, Me.TablaImportar.Item(Mes.Index, posicion).Value,
                                    Me.TablaImportar.Item(RFCE.Index, posicion).Value, Me.TablaImportar.Item(TipoPoliza.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaImportar.Item(FT4.Index, posicion).Value,
                                  "", BO, Me.TablaImportar.Item(CtaOT4.Index, posicion).Value, Me.TablaImportar.Item(UUI.Index, posicion).Value, Me.TablaImportar.Item(ImpT4.Index, posicion).Value, Bd, Me.TablaImportar.Item(CtaDT4.Index, posicion).Value)
                End If

                If Me.TablaImportar.Item(ImpT5.Index, posicion).Value > 0 Then

                    Dim cadena As String = Me.TablaImportar.Item(BankOT5.Index, posicion).Value
                    Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim BO As String = cadena.Substring(0, posil - 1)
                    cadena = Me.TablaImportar.Item(BankDT5.Index, posicion).Value
                    posil = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim Bd As String = cadena.Substring(0, posil - 1)
                    Inserta_Comprobante_Fiscal_Transf(poliza_Sistema, Me.TablaImportar.Item(anio.Index, posicion).Value, Me.TablaImportar.Item(Mes.Index, posicion).Value,
                                    Me.TablaImportar.Item(RFCE.Index, posicion).Value, Me.TablaImportar.Item(TipoPoliza.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaImportar.Item(FT5.Index, posicion).Value,
                                  "", BO, Me.TablaImportar.Item(CtaOT5.Index, posicion).Value, Me.TablaImportar.Item(UUI.Index, posicion).Value, Me.TablaImportar.Item(ImpT5.Index, posicion).Value, Bd, Me.TablaImportar.Item(CtaDT5.Index, posicion).Value)
                End If
                If Me.TablaImportar.Item(ImpT6.Index, posicion).Value > 0 Then

                    Dim cadena As String = Me.TablaImportar.Item(BankOT6.Index, posicion).Value
                    Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim BO As String = cadena.Substring(0, posil - 1)
                    cadena = Me.TablaImportar.Item(BankDT6.Index, posicion).Value
                    posil = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim Bd As String = cadena.Substring(0, posil - 1)
                    Inserta_Comprobante_Fiscal_Transf(poliza_Sistema, Me.TablaImportar.Item(anio.Index, posicion).Value, Me.TablaImportar.Item(Mes.Index, posicion).Value,
                                    Me.TablaImportar.Item(RFCE.Index, posicion).Value, Me.TablaImportar.Item(TipoPoliza.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaImportar.Item(FT6.Index, posicion).Value,
                                  "", BO, Me.TablaImportar.Item(CtaOT6.Index, posicion).Value, Me.TablaImportar.Item(UUI.Index, posicion).Value, Me.TablaImportar.Item(ImpT6.Index, posicion).Value, Bd, Me.TablaImportar.Item(CtaDT6.Index, posicion).Value)
                End If
                If Me.TablaImportar.Item(ImpT7.Index, posicion).Value > 0 Then

                    Dim cadena As String = Me.TablaImportar.Item(BankOT7.Index, posicion).Value
                    Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim BO As String = cadena.Substring(0, posil - 1)
                    cadena = Me.TablaImportar.Item(BankDT7.Index, posicion).Value
                    posil = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim Bd As String = cadena.Substring(0, posil - 1)
                    Inserta_Comprobante_Fiscal_Transf(poliza_Sistema, Me.TablaImportar.Item(anio.Index, posicion).Value, Me.TablaImportar.Item(Mes.Index, posicion).Value,
                                    Me.TablaImportar.Item(RFCE.Index, posicion).Value, Me.TablaImportar.Item(TipoPoliza.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaImportar.Item(FT7.Index, posicion).Value,
                                  "", BO, Me.TablaImportar.Item(CtaOT7.Index, posicion).Value, Me.TablaImportar.Item(UUI.Index, posicion).Value, Me.TablaImportar.Item(ImpT7.Index, posicion).Value, Bd, Me.TablaImportar.Item(CtaDT7.Index, posicion).Value)
                End If
                If Me.TablaImportar.Item(ImpC.Index, posicion).Value > 0 Then

                    Dim cadena As String = Me.TablaImportar.Item(BancosCheques.Index, posicion).Value
                    Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
                    Dim BO As String = cadena.Substring(0, posil - 1)

                    Inserta_Comprobante_Fiscal_Cheque(poliza_Sistema, Me.TablaImportar.Item(anio.Index, posicion).Value, Me.TablaImportar.Item(Mes.Index, posicion).Value,
                                    Me.TablaImportar.Item(RFCE.Index, posicion).Value, Me.TablaImportar.Item(TipoPoliza.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaImportar.Item(FechaC.Index, posicion).Value,
                                  Me.TablaImportar.Item(NoCheque.Index, posicion).Value, BO, Me.TablaImportar.Item(CuentaC.Index, posicion).Value, Me.TablaImportar.Item(UUI.Index, posicion).Value, Me.TablaImportar.Item(ImpC.Index, posicion).Value)
                End If
            End If
            NuevoDetalle(posicion, poliza_Sistema)
        End If
    End Sub
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
    Private Function Creapoliza(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String, ByVal dia As String,
                         ByVal consecutivo As Integer, ByVal tipo As Integer, ByVal fecha As String,
                         ByVal concepto As String, ByVal movimiento As String, ByVal num_pol As Integer, ByVal registro As Integer, Optional ByVal comple As Boolean = False)
        Dim hacer As Boolean
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Polizas"
        sql &= "("
        sql &= " 	ID_poliza,      "
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
        sql &= "  '" & Eventos.Usuario(Inicio.lblusuario.Text) & "', 1" '@usuario            
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
    Private Sub Crea_detalle_poliza(ByVal id_poliza As String, ByVal item As Integer, ByVal cargo As Decimal,
                                       ByVal Abono As Decimal, ByVal cuenta As String, ByVal cheque As String)
        Dim sql As String = ""
        sql &= "         INSERT INTO dbo.Detalle_Polizas"
        sql &= "(   "
        sql &= " ID_poliza,      "
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
        Else
            Dim erro = 0

        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Cheque(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
	(
    anio,    mes,    tipo,      RFC_Ce,
    No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
    ID_poliza,    Tipo_CE	) VALUES	("

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
    ID_poliza,    Tipo_CE	) VALUES	("

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
        Else
            Dim erro = 0
        End If
    End Sub
    Private Sub Inserta_Comprobante_Fiscal_Transf(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String,
                           ByVal Rfc_Emisor As String, ByVal tipo As String, ByVal fecha As String,
                           ByVal No_cheque As String, ByVal no_banco As String, ByVal cuenta_origen As String, ByVal Referencia As String, ByVal Importe As Decimal, ByVal bancoD As String, ByVal cuentaD As String)
        Dim sql As String = "  INSERT INTO dbo.Conta_E_Sistema
	(
    anio,    mes,    tipo,       RFC_Ce,
    No_Cheque,    No_Banco,    Cuenta_Origen,    Fecha_Mov,    Importe,
    ID_poliza,    Tipo_CE,Banco_Destino,Cuenta_Destino	) VALUES	("

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
        sql &= " 	ID_poliza,                  "
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
        Else
            Dim erro = 0
        End If
    End Sub
    Private Sub Edita_Factura(ByVal Folio_Fiscal As String, ByVal detaclle As String, ByVal Poliza As String)
        Dim sql As String = " UPDATE dbo.Facturas
                        SET ID_poliza = '" & Poliza & "'
                        WHERE Folio_Fiscal = '" & Folio_Fiscal & "' and Detalle_Comp_Electronico ='" & detaclle & "' "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("EditaFacturas", sql)

        End If
    End Sub
    Private Function Calcula_poliza(ByVal i As Integer)
        Dim mess As String = IIf(Len(Me.TablaImportar.Item(Mes.Index, i).Value) = 1, "0" & Me.TablaImportar.Item(Mes.Index, i).Value, Me.TablaImportar.Item(Mes.Index, i).Value)
        Dim poliza As String = Eventos.Num_polizaS(Me.lstCliente.SelectItem, Checa_tipo(Me.TablaImportar.Item(TipoPoliza.Index, i).Value, Me.lstCliente.SelectItem), Me.TablaImportar.Item(anio.Index, i).Value, mess, Busca_tipificar(Me.TablaImportar.Item(TipoPoliza.Index, i).Value))
        Me.TablaImportar.Item(Psistema.Index, i).Value = poliza
        Return poliza
    End Function
    Private Function Calcula_polizaD(ByVal i As Integer)
        Dim mess As String = IIf(Len(Me.TablaD.Item(MesCD.Index, i).Value) = 1, "0" & Me.TablaD.Item(MesCD.Index, i).Value, Me.TablaD.Item(MesCD.Index, i).Value)
        Dim poliza As String = Eventos.Num_polizaS(Me.lstCliente.SelectItem, Checa_tipo(Me.TablaD.Item(TipPolD.Index, i).Value, Me.lstCliente.SelectItem), Me.TablaD.Item(AnioCD.Index, i).Value, mess, Busca_tipificar(Me.TablaD.Item(TipPolD.Index, i).Value))
        Me.TablaD.Item(PolSD.Index, i).Value = poliza
        Return poliza
    End Function
    Private Function Calcula_polizaC(ByVal i As Integer)
        Dim mess As String = IIf(Len(Me.TablaC.Item(MesComplemento.Index, i).Value) = 1, "0" & Me.TablaC.Item(MesComplemento.Index, i).Value, Me.TablaC.Item(MesComplemento.Index, i).Value)
        Dim poliza As String = Eventos.Num_polizaS(Me.lstCliente.SelectItem, Checa_tipo(Me.TablaC.Item(TipPolComplemento.Index, i).Value, Me.lstCliente.SelectItem), Me.TablaC.Item(AnioComplemento.Index, i).Value, mess, Busca_tipificar(Me.TablaC.Item(TipPolComplemento.Index, i).Value))
        Me.TablaC.Item(PolSD.Index, i).Value = poliza
        Return poliza
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

    Private Sub Guardar_Carga()
        If Me.TablaImportar.Rows.Count >= 1 Then
            For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                Dim tabla As String = ""
                If Me.TablaImportar.Item(2, i).Value = "P" Then
                    tabla = "Xml_Complemento"
                Else
                    tabla = "Xml_Sat"
                End If
                Guardar_XML(IIf(IsDBNull(Me.TablaImportar.Item(LetraS.Index, i).Value), "", Me.TablaImportar.Item(LetraS.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(NCuenta.Index, i).Value), "", Me.TablaImportar.Item(NCuenta.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(ContabilizacionC.Index, i).Value), "", Me.TablaImportar.Item(ContabilizacionC.Index, i).Value),
                           Me.TablaImportar.Item(anio.Index, i).Value, Me.TablaImportar.Item(Mes.Index, i).Value, Me.TablaImportar.Item(ImpEf.Index, i).Value, IIf(IsDBNull(Me.TablaImportar.Item(CuentasEfectivo.Index, i).Value), "", Me.TablaImportar.Item(CuentasEfectivo.Index, i).Value),
                           Me.TablaImportar.Item(ImpT.Index, i).Value, IIf(IsDBNull(Me.TablaImportar.Item(BancoOrigen.Index, i).Value), "", Me.TablaImportar.Item(BancoOrigen.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(CuentaO.Index, i).Value), "", Me.TablaImportar.Item(CuentaO.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(Bancodestino.Index, i).Value), "", Me.TablaImportar.Item(Bancodestino.Index, i).Value),
                           IIf(IsDBNull(Me.TablaImportar.Item(Fechat.Index, i).Value), "", Me.TablaImportar.Item(Fechat.Index, i).Value), Me.TablaImportar.Item(ImpC.Index, i).Value, IIf(IsDBNull(Me.TablaImportar.Item(BancosCheques.Index, i).Value), "", Me.TablaImportar.Item(BancosCheques.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(CuentaC.Index, i).Value), "", Me.TablaImportar.Item(CuentaC.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(NoCheque.Index, i).Value), "", Me.TablaImportar.Item(NoCheque.Index, i).Value),
                          IIf(IsDBNull(Me.TablaImportar.Item(FechaC.Index, i).Value), "", Me.TablaImportar.Item(FechaC.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(CuentaBancos.Index, i).Value), "", Me.TablaImportar.Item(CuentaBancos.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(ProvA.Index, i).Value), 0, Me.TablaImportar.Item(ProvA.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(ProvP.Index, i).Value), 0, Me.TablaImportar.Item(ProvP.Index, i).Value),
                           IIf(IsDBNull(Me.TablaImportar.Item(Dif.Index, i).Value), 0, Me.TablaImportar.Item(Dif.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(TipoPoliza.Index, i).Value), "", Me.TablaImportar.Item(TipoPoliza.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(ImpG.Index, i).Value), 0, Me.TablaImportar.Item(ImpG.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(ImpEx.Index, i).Value), 0, Me.TablaImportar.Item(ImpEx.Index, i).Value),
                          IIf(IsDBNull(Me.TablaImportar.Item(IVAR.Index, i).Value), 0, Me.TablaImportar.Item(IVAR.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(PagoA.Index, i).Value), 0, Me.TablaImportar.Item(PagoA.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(TReal.Index, i).Value), 0, Me.TablaImportar.Item(TReal.Index, i).Value), Me.TablaImportar.Item(UCambaria.Index, i).Value, IIf(IsDBNull(Me.TablaImportar.Item(PCambiaria.Index, i).Value), 0, Me.TablaImportar.Item(PCambiaria.Index, i).Value),
                           Me.TablaImportar.Item(ImpD.Index, i).Value, IIf(IsDBNull(Me.TablaImportar.Item(CuntaDev.Index, i).Value), "", Me.TablaImportar.Item(CuntaDev.Index, i).Value), Me.TablaImportar.Item(Id_Xml.Index, i).Value, IIf(IsDBNull(Me.TablaImportar.Item(NumPol.Index, i).Value), "", Me.TablaImportar.Item(NumPol.Index, i).Value),
                           Me.TablaImportar.Item(RISR.Index, i).Value, Me.TablaImportar.Item(RIVA.Index, i).Value, tabla, IIf(IsDBNull(Me.TablaImportar.Item(CtaBD.Index, i).Value), "", Me.TablaImportar.Item(CtaBD.Index, i).Value),
                           IIf(IsDBNull(Me.TablaImportar.Item(Ajus.Index, i).Value), 0, Me.TablaImportar.Item(Ajus.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(CtaAjustes.Index, i).Value), "", Me.TablaImportar.Item(CtaAjustes.Index, i).Value),
                           IIf(IsDBNull(Me.TablaImportar.Item(Anti.Index, i).Value), 0, Me.TablaImportar.Item(Anti.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(CtaAnti.Index, i).Value), "", Me.TablaImportar.Item(CtaAnti.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(ImpProvis.Index, i).Value), 0, Me.TablaImportar.Item(ImpProvis.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(CtaOrden.Index, i).Value), "", Me.TablaImportar.Item(CtaOrden.Index, i).Value),
                                IIf(IsDBNull(Me.TablaImportar.Item(ImpT2.Index, i).Value), 0, Me.TablaImportar.Item(ImpT2.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(BankOT2.Index, i).Value), "", Me.TablaImportar.Item(BankOT2.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaOT2.Index, i).Value), "", Me.TablaImportar.Item(CtaOT2.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(BankDT2.Index, i).Value), "", Me.TablaImportar.Item(BankDT2.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaDT2.Index, i).Value), "", Me.TablaImportar.Item(CtaDT2.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(FT2.Index, i).Value), "", Me.TablaImportar.Item(FT2.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaT2.Index, i).Value), "", Me.TablaImportar.Item(CtaT2.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(ImpT3.Index, i).Value), 0, Me.TablaImportar.Item(ImpT3.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(BankOT3.Index, i).Value), "", Me.TablaImportar.Item(BankOT3.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaOT3.Index, i).Value), "", Me.TablaImportar.Item(CtaOT3.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(BankDT3.Index, i).Value), "", Me.TablaImportar.Item(BankDT3.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaDT3.Index, i).Value), "", Me.TablaImportar.Item(CtaDT3.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(FT3.Index, i).Value), "", Me.TablaImportar.Item(FT3.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaT3.Index, i).Value), "", Me.TablaImportar.Item(CtaT3.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(ImpT4.Index, i).Value), 0, Me.TablaImportar.Item(ImpT4.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(BankOT4.Index, i).Value), "", Me.TablaImportar.Item(BankOT4.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaOT4.Index, i).Value), "", Me.TablaImportar.Item(CtaOT4.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(BankDT4.Index, i).Value), "", Me.TablaImportar.Item(BankDT4.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaDT4.Index, i).Value), "", Me.TablaImportar.Item(CtaDT4.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(FT4.Index, i).Value), "", Me.TablaImportar.Item(FT4.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaT4.Index, i).Value), "", Me.TablaImportar.Item(CtaT4.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(ImpT5.Index, i).Value), 0, Me.TablaImportar.Item(ImpT5.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(BankOT5.Index, i).Value), "", Me.TablaImportar.Item(BankOT5.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaOT5.Index, i).Value), "", Me.TablaImportar.Item(CtaOT4.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(BankDT5.Index, i).Value), "", Me.TablaImportar.Item(BankDT5.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaDT5.Index, i).Value), "", Me.TablaImportar.Item(CtaDT5.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(FT5.Index, i).Value), "", Me.TablaImportar.Item(FT5.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaT5.Index, i).Value), "", Me.TablaImportar.Item(CtaT5.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(ImpT6.Index, i).Value), 0, Me.TablaImportar.Item(ImpT6.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(BankOT6.Index, i).Value), "", Me.TablaImportar.Item(BankOT6.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaOT6.Index, i).Value), "", Me.TablaImportar.Item(CtaOT6.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(BankDT6.Index, i).Value), "", Me.TablaImportar.Item(BankDT6.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaDT6.Index, i).Value), "", Me.TablaImportar.Item(CtaDT6.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(FT6.Index, i).Value), "", Me.TablaImportar.Item(FT6.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaT6.Index, i).Value), "", Me.TablaImportar.Item(CtaT6.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(ImpT7.Index, i).Value), 0, Me.TablaImportar.Item(ImpT7.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(BankOT7.Index, i).Value), "", Me.TablaImportar.Item(BankOT7.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaOT7.Index, i).Value), "", Me.TablaImportar.Item(CtaOT7.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(BankDT7.Index, i).Value), "", Me.TablaImportar.Item(BankDT7.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaDT7.Index, i).Value), "", Me.TablaImportar.Item(CtaDT7.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(FT7.Index, i).Value), "", Me.TablaImportar.Item(FT7.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaT7.Index, i).Value), "", Me.TablaImportar.Item(CtaT7.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(CtaCheque.Index, i).Value), "", Me.TablaImportar.Item(CtaCheque.Index, i).Value),
                    IIf(IsDBNull(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, i).Value), "", Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, i).Value), IIf(IsDBNull(Me.TablaImportar.Item(AcreedoresTerceros.Index, i).Value), 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, i).Value))








            Next
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            MessageBox.Show("Proceso Guardado Completado ...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub Guardar_XML(ByVal Letra_Sat As String, ByVal Nombre_cuenta As String, ByVal Clave As String,
                            ByVal Anio_Contable As String, ByVal Mes_Contable As String, ByVal Imp_Efectivo As Decimal, ByVal Cuenta_Efectivo As String,
                            ByVal Imp_Transf As Decimal, ByVal Banco_Origen As String, ByVal Cuenta_Origen As String, ByVal Banco_Destino As String,
                            ByVal Fecha_Transaccion As String, ByVal Imp_Cheque As Decimal, ByVal Nom_Banco_Ch As String, ByVal Cuenta_Origen_Ch As String, ByVal No_Cheque As String,
                            ByVal Fecha_Ch As String, ByVal Cuenta_Bancos As String, ByVal Provision_Acreedor As Decimal, ByVal Provision_Proveedor As Decimal,
                            ByVal Diferencia As Decimal, ByVal Tipo_Poliza As String, ByVal Imp_Grabado As Decimal, ByVal Imp_Exento As Decimal,
                            ByVal IVA_real As Decimal, ByVal Prc_Pago_Acumulado As Decimal, ByVal Total_Real As Decimal, ByVal Utilidad_Cambiaria As Decimal, ByVal Perdida_Cambiaria As Decimal,
                            ByVal Imp_Devolucion As Decimal, ByVal Cuenta_Devolucion As String, ByVal Id_Registro_Xml As Integer, ByVal numpol As String, ByVal risr As Decimal, ByVal riva As Decimal,
                            ByVal Tabla As String, ByVal ctadestino As String, ByVal ajus As Decimal, ByVal ctaajus As String, ByVal anti As Decimal, ByVal ctaanti As String, ByVal ImpProvis As Decimal,
                            ByVal Cta_Orden As String, ByVal Imp_Transf1 As Decimal, ByVal Banco_Origen1 As String, ByVal Cuenta_Origen1 As String, ByVal Banco_Destino1 As String,
                            ByVal Cuenta_Destino1 As String, ByVal Fecha_Transaccion1 As String, ByVal Cuenta_Bancos1 As String, ByVal Imp_Transf2 As Decimal, ByVal Banco_Origen2 As String, ByVal Cuenta_Origen2 As String, ByVal Banco_Destino2 As String,
                            ByVal Cuenta_Destino2 As String, ByVal Fecha_Transaccion2 As String, ByVal Cuenta_Bancos2 As String, ByVal Imp_Transf3 As Decimal, ByVal Banco_Origen3 As String, ByVal Cuenta_Origen3 As String, ByVal Banco_Destino3 As String,
                            ByVal Cuenta_Destino3 As String, ByVal Fecha_Transaccion3 As String, ByVal Cuenta_Bancos3 As String, ByVal Imp_Transf4 As Decimal, ByVal Banco_Origen4 As String, ByVal Cuenta_Origen4 As String, ByVal Banco_Destino4 As String,
                            ByVal Cuenta_Destino4 As String, ByVal Fecha_Transaccion4 As String, ByVal Cuenta_Bancos4 As String, ByVal Imp_Transf5 As Decimal, ByVal Banco_Origen5 As String, ByVal Cuenta_Origen5 As String, ByVal Banco_Destino5 As String,
                            ByVal Cuenta_Destino5 As String, ByVal Fecha_Transaccion5 As String, ByVal Cuenta_Bancos5 As String, ByVal Imp_Transf6 As Decimal, ByVal Banco_Origen6 As String, ByVal Cuenta_Origen6 As String, ByVal Banco_Destino6 As String,
                            ByVal Cuenta_Destino6 As String, ByVal Fecha_Transaccion6 As String, ByVal Cuenta_Bancos6 As String, ByVal Cuenta_Cheques As String, ByVal Cta_Terceros As String, ByVal Imp_Terceros As Decimal)
        Dim sql As String = "UPDATE dbo." & Tabla & "
                SET 		Letra_Sat = '" & Trim(Letra_Sat) & "',
                	Nombre_cuenta = '" & Nombre_cuenta & "',
                	Clave = '" & Clave & "',
                	Anio_Contable = '" & Anio_Contable & "',
                	Mes_Contable = '" & Mes_Contable & "',
                	Imp_Efectivo = " & Imp_Efectivo & ",
                	Cuenta_Efectivo = '" & Cuenta_Efectivo & "',
                	Imp_Transf = " & Imp_Transf & ",
                	Banco_Origen = '" & Banco_Origen & "',
                	Cuenta_Origen = '" & Cuenta_Origen & "',
                	Banco_Destino = '" & Banco_Destino & "',
                	Fecha_Transaccion = " & Eventos.Sql_hoy(Fecha_Transaccion) & ",
                	Imp_Cheque =" & Imp_Cheque & ",
                	Nom_Banco_Ch = '" & Nom_Banco_Ch & "',
                	Cuenta_Origen_Ch = '" & Cuenta_Origen_Ch & "',
                	No_Cheque = '" & No_Cheque & "',
                	Fecha_Ch = " & Eventos.Sql_hoy(Fecha_Ch) & ",
                	Cuenta_Bancos = '" & Cuenta_Bancos & "',
                	Provision_Acreedor = " & Provision_Acreedor & ",
                	Provision_Proveedor = " & Provision_Proveedor & ",
                	Diferencia = " & Diferencia & ",
                	Tipo_Poliza = '" & Tipo_Poliza & "',
                	Imp_Grabado =  " & Imp_Grabado & ",
                	Imp_Exento =  " & Imp_Exento & ",
                	IVA_real =  " & IVA_real & ",
                	Prc_Pago_Acumulado =  " & Prc_Pago_Acumulado & ",
                	Total_Real =  " & Total_Real & ",
                	Utilidad_Cambiaria =  " & Utilidad_Cambiaria & ",
                	Perdida_Cambiaria =  " & Perdida_Cambiaria & ",
                	Imp_Devolucion =  " & Imp_Devolucion & ",
                	Cuenta_Devolucion = '" & Cuenta_Devolucion & "', 
                    Numpol= '" & Trim(numpol) & "', 
                     RIRS= " & risr & ", 
                     RIVA= " & riva & ", 
                     Cuenta_Destino= '" & ctadestino & "',
                      Ajuste= " & ajus & ", 
                     Anticipos= " & anti & ",  
                       Cta_Ajuste= '" & ctaajus & "', 
                     Cta_Anticipos= '" & ctaanti & "',
                         Imp_Provision= " & ImpProvis & ",
                       Cta_Orden= '" & Cta_Orden & "',

                         Imp_Transf1 = " & Imp_Transf1 & " ,       
                       Banco_Origen1 = '" & Banco_Origen1 & "',	 
                       Cuenta_Origen1 = '" & Cuenta_Origen1 & "',	 
                       Banco_Destino1 = '" & Banco_Destino1 & "',
                       Cuenta_Destino1 = '" & Cuenta_Destino1 & "'	,	 
                       Fecha_Transaccion1 = " & Eventos.Sql_hoy(Fecha_Transaccion1) & ",	 
                       Cuenta_Bancos1  = '" & Cuenta_Bancos1 & "',
                       
                        Imp_Transf2 = " & Imp_Transf2 & " ,       
                       Banco_Origen2 = '" & Banco_Origen2 & "',	 
                       Cuenta_Origen2 = '" & Cuenta_Origen2 & "',	 
                       Banco_Destino2 = '" & Banco_Destino2 & "',
                       Cuenta_Destino2 = '" & Cuenta_Destino2 & "'	,	 
                       Fecha_Transaccion2 = " & Eventos.Sql_hoy(Fecha_Transaccion2) & ",	 
                       Cuenta_Bancos2  = '" & Cuenta_Bancos2 & "',

                        Imp_Transf3 = " & Imp_Transf3 & " ,       
                       Banco_Origen3 = '" & Banco_Origen3 & "',	 
                       Cuenta_Origen3 = '" & Cuenta_Origen3 & "',	 
                       Banco_Destino3 = '" & Banco_Destino3 & "',
                       Cuenta_Destino3 = '" & Cuenta_Destino3 & "'	,	 
                       Fecha_Transaccion3 = " & Eventos.Sql_hoy(Fecha_Transaccion3) & ",	 
                       Cuenta_Bancos3  = '" & Cuenta_Bancos3 & "',	 

                       Imp_Transf4 = " & Imp_Transf4 & " ,       
                       Banco_Origen4 = '" & Banco_Origen4 & "',	 
                       Cuenta_Origen4 = '" & Cuenta_Origen4 & "',	 
                       Banco_Destino4 = '" & Banco_Destino4 & "',
                       Cuenta_Destino4 = '" & Cuenta_Destino4 & "'	,	 
                       Fecha_Transaccion4 = " & Eventos.Sql_hoy(Fecha_Transaccion4) & ",	 
                       Cuenta_Bancos4  = '" & Cuenta_Bancos4 & "',
                       
                        Imp_Transf5 = " & Imp_Transf4 & " ,       
                       Banco_Origen5 = '" & Banco_Origen4 & "',	 
                       Cuenta_Origen5 = '" & Cuenta_Origen4 & "',	 
                       Banco_Destino5 = '" & Banco_Destino4 & "',
                       Cuenta_Destino5 = '" & Cuenta_Destino4 & "'	,	 
                       Fecha_Transaccion5 = " & Eventos.Sql_hoy(Fecha_Transaccion4) & ",	 
                       Cuenta_Bancos5  = '" & Cuenta_Bancos4 & "',
                       
                        Imp_Transf6 = " & Imp_Transf6 & " ,       
                       Banco_Origen6 = '" & Banco_Origen6 & "',	 
                       Cuenta_Origen6 = '" & Cuenta_Origen6 & "',	 
                       Banco_Destino6 = '" & Banco_Destino6 & "',
                       Cuenta_Destino6 = '" & Cuenta_Destino6 & "'	,	 
                       Fecha_Transaccion6 = " & Eventos.Sql_hoy(Fecha_Transaccion6) & ",	 
                       Cuenta_Bancos6  = '" & Cuenta_Bancos6 & "' , Cuenta_Cheques = '" & Cuenta_Cheques & "',	
	                   Cta_Terceros  = '" & Cta_Terceros & "',
                       Imp_Terceros = " & Imp_Terceros & "	
                WHERE Id_Registro_Xml = " & Id_Registro_Xml & ""
        If Eventos.Comando_sql(sql) > 0 Then
            If Len(sql) > 3990 Then
                sql = sql.ToString.Substring(0, 3900)
            Else
                sql = sql
            End If
            Eventos.Insertar_usuariol("GuardaXMLC", sql)
        End If
    End Sub
    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        Guardar_Carga()
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
    Private Sub CrearCuentaCargoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CrearCuentaCargoToolStripMenuItem.Click
        If Me.TcRecibidas.SelectedIndex = 0 Then
            Dim leyenda As String = ""
            Dim dsconta As DataSet
            Dim columna As Integer = Me.TablaImportar.CurrentCell.ColumnIndex
            Dim Nombre As String
            Nombre = Me.TablaImportar.Columns.Item(Me.TablaImportar.CurrentCell.ColumnIndex).Name.ToString
            Me.LstTexto.Cargar(" Select '','' ")

            Select Case Nombre
                Case "ContabilizacionC"
                    If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
                        If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value = "GN" Or Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value = "GNPP" Then
                            dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='6120' and Nivel2 ='0001'   ORDER BY cta")
                            leyenda = "GN"

                        Else
                            dsconta = CuentasMadres(Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.CurrentRow.Index, Me.TablaImportar.Item(ImpProvis.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(TReal.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(ImpG.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(ImpEx.Index, Me.TablaImportar.CurrentRow.Index).Value)
                            leyenda = Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value

                        End If
                    End If

                Case "NCuenta"
                    If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
                        If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value = "GN" Or Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value = "GNPP" Then
                            dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='6120' and Nivel2 ='0001'   ORDER BY cta")
                            leyenda = "GN"

                        Else
                            dsconta = CuentasMadres(Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.CurrentRow.Index, Me.TablaImportar.Item(ImpProvis.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(TReal.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(ImpG.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(ImpEx.Index, Me.TablaImportar.CurrentRow.Index).Value)
                            leyenda = Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value

                        End If
                    End If
                Case "CuentasEfectivo"
                    dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1010' and Nivel2 > 0 ORDER BY cta")
                    leyenda = " Efectivo"
                Case "CuentaBancos"
                    dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1020' and Nivel2 > 0 ORDER BY cta")
                    leyenda = " Bancos"
                Case "CtaAjustes"
                    dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & "   ORDER BY cta")
                    leyenda = " Ajustes"
                Case "CtaAnti"
                    If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
                        If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value = "C" Or Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value = "CPP" Then
                            dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1200' and Nivel2 > 0   ORDER BY cta")
                            leyenda = " Anticipos Proveedores"
                        ElseIf Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value = "GG" Or Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value = "GGPP" Then
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
            ElseIf Letra.Substring(12, 4) = "0000" And Letra.Substring(4, 4) > "0000" And Letra.Substring(8, 4) = "0000" Then 'Tercer Nivel
                Nivel = 3
                Hijas = 0
            ElseIf Letra.Substring(0, 4) > "0000" And Letra.Substring(4, 4) = "0000" And Letra.Substring(8, 4) = "0000" And Letra.Substring(12, 4) = "0000" Then 'Primer Nivel
                ds = Eventos.Obtener_DS("Select Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta from Catalogo_de_cuentas where nivel1 = '" & Letra.ToString.Substring(0, 4) & "' and Nivel2 = '" & Letra.ToString.Substring(4, 4) & "' and nivel3 > 0 and Nivel4 = '0000' and Id_Empresa = " & Me.lstCliente.SelectItem & " order by cta")
                Hijas = ds.Tables(0).Rows.Count
                Nivel = 2
            End If


            If Hijas > 0 And (Hijas > 0 And leyenda <> "GN" And Hijas > 0 And leyenda <> "GNPP" And Letra <> "7010001000000000") Then
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

                If Verifica_existencia4(Sub_cuenta.ToString.Substring(0, 4), Sub_cuenta.ToString.Substring(4, 4), Sub_cuenta.ToString.Substring(8, 4), Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value) = True Then
                    cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Sub_cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Sub_cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & Sub_cuenta.ToString.Substring(8, 4) & " AND Nivel4 >=" & Sub_cuenta.ToString.Substring(12, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & " ", True))
                    cuenta = Format(cuenta).PadLeft(4, "0")
                    Crear_cuenta(Sub_cuenta.ToString.Substring(0, 4), Sub_cuenta.ToString.Substring(4, 4), Sub_cuenta.ToString.Substring(8, 4),
                                           cuenta, Sub_cuenta.Substring(0, 12) & cuenta, Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, Me.TablaImportar.CurrentRow.Index).Value,
                                           Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value)
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    RadMessageBox.Show("La cuenta ya existe ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

                End If

            Else
                ' Se crea cuenta de tercer nivel
                If Nivel = 4 Then
                    If Verifica_existencia4(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), Letra.ToString.Substring(8, 4), Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value) = True Then
                    Else
                        Exit Sub
                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        RadMessageBox.Show("La cuenta ya existe ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                    End If
                Else
                    If Verifica_existencia(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value) = True Then

                    Else
                        Exit Sub
                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        RadMessageBox.Show("La cuenta ya existe ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                    End If
                End If

                Select Case Nivel
                    Case 3
                        cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & Letra.ToString.Substring(0, 4) & "  AND Nivel2 =" & Letra.ToString.Substring(4, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & " ", True))
                        cuenta = Format(cuenta).PadLeft(4, "0")
                        Crear_cuenta(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), cuenta,
                                       "0000", Letra.Substring(0, 8) & cuenta & "0000", Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, Me.TablaImportar.CurrentRow.Index).Value,
                                       Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value)

                    Case 4
                        cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Letra.ToString.Substring(0, 4) & "  AND Nivel2 =" & Letra.ToString.Substring(4, 4) & " AND Nivel3 =" & Letra.ToString.Substring(8, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & " ", True))
                        cuenta = Format(cuenta).PadLeft(4, "0")
                        Crear_cuenta(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), Letra.ToString.Substring(8, 4),
                                       cuenta, Letra.Substring(0, 12) & cuenta, Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, Me.TablaImportar.CurrentRow.Index).Value,
                                       Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value)

                    Case 2
                        cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & Letra.ToString.Substring(0, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & " ", True))
                        cuenta = Format(cuenta).PadLeft(4, "0")
                        Crear_cuenta(Letra.ToString.Substring(0, 4), cuenta, "0000",
                                       "0000", Letra.Substring(0, 4) & cuenta & "00000000", Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, Me.TablaImportar.CurrentRow.Index).Value,
                                       Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value)
                End Select



            End If
            Me.TablaImportar_CellEndEdit(Me.TablaImportar, Nothing)
            Finalizamanaual(Me.TablaImportar.CurrentCell.RowIndex)
        ElseIf Me.TcRecibidas.SelectedIndex = 1 Then
            Dim leyenda As String = ""
            Dim dsconta As DataSet
            Dim columna As Integer = Me.TablaD.CurrentCell.ColumnIndex
            Dim Nombre As String
            Nombre = Me.TablaD.Columns.Item(Me.TablaD.CurrentCell.ColumnIndex).Name.ToString
            Me.LstTexto.Cargar(" Select '','' ")

            Select Case Nombre
                Case "LetraCd"
                    dsconta = CuentasMadres(Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.CurrentRow.Index, Me.TablaD.Item(ImpPd.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(TRD.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(ImpGD.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(ImpEDV.Index, Me.TablaD.CurrentRow.Index).Value)
                    leyenda = Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value
                Case "NomCuentaD"
                    dsconta = CuentasMadres(Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.CurrentRow.Index, Me.TablaD.Item(ImpPd.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(TRD.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(ImpGD.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(ImpEDV.Index, Me.TablaD.CurrentRow.Index).Value)
                    leyenda = Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value
                Case "CtaEfD"
                    dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1010' and Nivel2 > 0  ORDER BY cta")
                    leyenda = " Efectivo"
                Case "CtaBancosD"
                    dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1020' and Nivel2 > 0 ORDER BY cta")
                    leyenda = " Bancos"
                Case "CtaAD"
                    dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & "   ORDER BY cta")
                    leyenda = " Ajustes"
                Case "CtaAntiD"
                    dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1070' and Nivel2 > 0   ORDER BY cta")
                    leyenda = " Anticipos"
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
            ElseIf Letra.Substring(12, 4) = "0000" And Letra.Substring(4, 4) > "0000" And Letra.Substring(8, 4) = "0000" Then 'Tercer Nivel
                Nivel = 3
                Hijas = 0
            ElseIf Letra.Substring(0, 4) > "0000" And Letra.Substring(4, 4) = "0000" And Letra.Substring(8, 4) = "0000" And Letra.Substring(12, 4) = "0000" Then 'Primer Nivel
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

                If Verifica_existencia4(Sub_cuenta.ToString.Substring(0, 4), Sub_cuenta.ToString.Substring(4, 4), Sub_cuenta.ToString.Substring(8, 4), Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value) = True Then
                    cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Sub_cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Sub_cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & Sub_cuenta.ToString.Substring(8, 4) & " AND Nivel4 >=" & Sub_cuenta.ToString.Substring(12, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                    cuenta = Format(cuenta).PadLeft(4, "0")
                    Crear_cuenta(Sub_cuenta.ToString.Substring(0, 4), Sub_cuenta.ToString.Substring(4, 4), Sub_cuenta.ToString.Substring(8, 4),
                                           cuenta, Sub_cuenta.Substring(0, 12) & cuenta, Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value & " " & Me.TablaD.Item(NomEd.Index, Me.TablaD.CurrentRow.Index).Value,
                                           Me.lstCliente.SelectItem, Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value)
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    RadMessageBox.Show("La cuenta ya existe ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                End If
                Me.TablaD_CellEndEdit(Me.TablaD, Nothing)
            Else
                If Nivel = 4 Then
                    If Verifica_existencia4(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), Letra.ToString.Substring(8, 4), Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value) = True Then
                    Else
                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        RadMessageBox.Show("La cuenta ya existe ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                        Exit Sub

                    End If
                Else
                    If Verifica_existencia(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value) = True Then

                    Else
                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        RadMessageBox.Show("La cuenta ya existe ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                        Exit Sub

                    End If
                End If
                ' Se crea cuenta de tercer nivel

                Select Case Nivel
                    Case 3
                        cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & Letra.ToString.Substring(0, 4) & "  AND Nivel2 =" & Letra.ToString.Substring(4, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & " ", True))
                        cuenta = Format(cuenta).PadLeft(4, "0")
                        Crear_cuenta(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), cuenta,
                                       "0000", Letra.Substring(0, 8) & cuenta & "0000", Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value & " " & Me.TablaD.Item(NomEd.Index, Me.TablaD.CurrentRow.Index).Value,
                                       Me.lstCliente.SelectItem, Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value)
                    Case 4
                        cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Letra.ToString.Substring(0, 4) & "  AND Nivel2 =" & Letra.ToString.Substring(4, 4) & " AND Nivel3 =" & Letra.ToString.Substring(8, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & " ", True))
                        cuenta = Format(cuenta).PadLeft(4, "0")
                        Crear_cuenta(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), Letra.ToString.Substring(8, 4),
                                       cuenta, Letra.Substring(0, 12) & cuenta, Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value & " " & Me.TablaD.Item(NomEd.Index, Me.TablaD.CurrentRow.Index).Value,
                                       Me.lstCliente.SelectItem, Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value)
                    Case 2
                        cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & Letra.ToString.Substring(0, 4) & "   and Id_Empresa = " & Me.lstCliente.SelectItem & " ", True))
                        cuenta = Format(cuenta).PadLeft(4, "0")
                        Crear_cuenta(Letra.ToString.Substring(0, 4), cuenta, "0000",
                                       "0000", Letra.Substring(0, 4) & cuenta & "00000000", Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value & " " & Me.TablaD.Item(NomEd.Index, Me.TablaD.CurrentRow.Index).Value,
                                       Me.lstCliente.SelectItem, Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value)
                End Select


                Me.TablaD_CellEndEdit(Me.TablaD, Nothing)
            End If
        ElseIf Me.TcRecibidas.SelectedIndex = 2 Then
            Dim leyenda As String = ""
            Dim dsconta As DataSet
            Dim columna As Integer = Me.TablaC.CurrentCell.ColumnIndex
            Dim Nombre As String
            Nombre = Me.TablaC.Columns.Item(Me.TablaC.CurrentCell.ColumnIndex).Name.ToString
            Me.LstTexto.cargar(" Select '','' ")

            Select Case Nombre
                Case "LetraContabilidadComplemento"
                    dsconta = CuentasMadres(Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.CurrentRow.Index, Me.TablaC.Item(ImpProviComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(TRComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(ImpGComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(ImpEComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                    leyenda = Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value
                Case "NomCtaComplemento"
                    dsconta = CuentasMadres(Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.CurrentRow.Index, Me.TablaC.Item(ImpProviComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(TRComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(ImpGComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(ImpEComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                    leyenda = Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value
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

                    If Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value <> Nothing Then
                        If Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value = "C" Or Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value = "CPP" Then
                            dsconta = Eventos.Obtener_DS("SELECT  Rtrim(Descripcion) + '-'+ convert(nvarchar,cuenta,103) as Cuenta, cuenta as cta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1200' and Nivel2 > 0   ORDER BY cta")
                            leyenda = " Anticipos Proveedores"
                        ElseIf Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value = "GG" Or Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value = "GGPP" Then
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
            ElseIf Letra.Substring(12, 4) = "0000" And Letra.Substring(4, 4) > "0000" And Letra.Substring(8, 4) = "0000" Then 'Tercer Nivel
                Nivel = 3
                Hijas = 0
            ElseIf Letra.Substring(0, 4) > "0000" And Letra.Substring(4, 4) = "0000" And Letra.Substring(8, 4) = "0000" And Letra.Substring(12, 4) = "0000" Then 'Primer Nivel
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

                If Verifica_existencia4(Sub_cuenta.ToString.Substring(0, 4), Sub_cuenta.ToString.Substring(4, 4), Sub_cuenta.ToString.Substring(8, 4), Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value) = True Then
                    cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Sub_cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Sub_cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & Sub_cuenta.ToString.Substring(8, 4) & " AND Nivel4 >=" & Sub_cuenta.ToString.Substring(12, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                    cuenta = Format(cuenta).PadLeft(4, "0")
                    Crear_cuenta(Sub_cuenta.ToString.Substring(0, 4), Sub_cuenta.ToString.Substring(4, 4), Sub_cuenta.ToString.Substring(8, 4),
                                           cuenta, Sub_cuenta.Substring(0, 12) & cuenta, Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & " " & Me.TablaC.Item(NEmComplemento.Index, Me.TablaC.CurrentRow.Index).Value,
                                           Me.lstCliente.SelectItem, Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    RadMessageBox.Show("La cuenta ya existe ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                End If
                ComplemantosL(Me.TablaC.CurrentRow.Index)
            Else
                ' Se crea cuenta de tercer nivel

                If Nivel = 4 Then
                    If Verifica_existencia4(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), Letra.ToString.Substring(8, 4), Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value) = True Then
                    Else
                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        RadMessageBox.Show("La cuenta ya existe ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                        Exit Sub

                    End If
                Else
                    If Verifica_existencia(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value) = True Then

                    Else
                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        RadMessageBox.Show("La cuenta ya existe ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                        Exit Sub

                    End If
                End If

                Select Case Nivel
                    Case 3

                        cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & Letra.ToString.Substring(0, 4) & "  AND Nivel2 =" & Letra.ToString.Substring(4, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                        cuenta = Format(cuenta).PadLeft(4, "0")
                        Crear_cuenta(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), cuenta,
                                       "0000", Letra.Substring(0, 8) & cuenta & "0000", Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & " " & Me.TablaC.Item(NEmComplemento.Index, Me.TablaC.CurrentRow.Index).Value,
                                       Me.lstCliente.SelectItem, Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value)

                    Case 4

                        cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Letra.ToString.Substring(0, 4) & "  AND Nivel2 =" & Letra.ToString.Substring(4, 4) & " AND Nivel3 =" & Letra.ToString.Substring(8, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                        cuenta = Format(cuenta).PadLeft(4, "0")
                        Crear_cuenta(Letra.ToString.Substring(0, 4), Letra.ToString.Substring(4, 4), Letra.ToString.Substring(8, 4),
                                       cuenta, Letra.Substring(0, 12) & cuenta, Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & " " & Me.TablaC.Item(NEmComplemento.Index, Me.TablaC.CurrentRow.Index).Value,
                                       Me.lstCliente.SelectItem, Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value)


                    Case 2

                        cuenta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & Letra.ToString.Substring(0, 4) & " and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                        cuenta = Format(cuenta).PadLeft(4, "0")
                        Crear_cuenta(Letra.ToString.Substring(0, 4), cuenta, "0000",
                                       "0000", Letra.Substring(0, 4) & cuenta & "00000000", Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & " " & Me.TablaC.Item(NEmComplemento.Index, Me.TablaC.CurrentRow.Index).Value,
                                       Me.lstCliente.SelectItem, Me.TablaC.Item(LetraContabilidadComplemento.Index, Me.TablaC.CurrentRow.Index).Value, Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value)

                End Select



                ComplemantosL(Me.TablaC.CurrentRow.Index)

            End If
        End If
    End Sub
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
                sql &= "	'NULL'," '@RFC
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
    Private Function Verifica_existencia(ByVal Nivel1 As String, ByVal Nivel2 As String, ByVal clave As String, ByVal rfc As String)
        Dim hacer As Boolean
        Dim sql As String = " Select cuenta from catalogo_De_cuentas where nivel1= '" & Nivel1 & "' and nivel2 = '" & Nivel2 & "'   and rfc = '" & rfc & "' AND Id_Empresa =" & Me.lstCliente.SelectItem & ""
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
        Dim sql As String = " Select cuenta from catalogo_De_cuentas where nivel1= '" & Nivel1 & "' and nivel2 = '" & Nivel2 & "' and nivel3 = '" & Nivel3 & "'   and rfc = '" & rfc & "' AND Id_Empresa =" & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = False
        Else
            hacer = True
        End If
        Return hacer
    End Function
    Private Sub Crear_detalle(ByVal p As Integer, ByVal pol As String)
        Dim Item As Integer = 1
        Dim PorcenProv, ImpGprov, ImpEprov, ImpIvaPr As Decimal
        PorcenProv = Math.Round(Me.TablaImportar.Item(ImpProvis.Index, p).Value, 2) / (Math.Round(Me.TablaImportar.Item(TReal.Index, p).Value, 2) - (Math.Round(IIf(Me.TablaImportar.Item(RIVA.Index, p).Value < 0, 0, Me.TablaImportar.Item(RIVA.Index, p).Value), 2) + Math.Round(IIf(Me.TablaImportar.Item(RISR.Index, p).Value < 0, 0, Me.TablaImportar.Item(RISR.Index, p).Value), 2)))
        If PorcenProv = 1 Then
            ImpGprov = 0
            ImpEprov = 0
            ImpIvaPr = 0
        Else
            ImpGprov = Me.TablaImportar.Item(ImpG.Index, p).Value * PorcenProv
            ImpEprov = Me.TablaImportar.Item(ImpEx.Index, p).Value * PorcenProv
            ImpIvaPr = Me.TablaImportar.Item(IVAR.Index, p).Value * PorcenProv
        End If
        Dim cadena As String = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
        Dim posi As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
        Dim cuantos As Integer = Len(cadena) - Len(cadena.Substring(0, posi))
        Dim Cuenta_Cargo As String = cadena.Substring(posi, cuantos)
        Dim Cuenta2 As String = ""



        'Nuevo codigo
        If Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "C" Then ' Compras 


            If Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then ' tiene grabado y exento
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 12) = "502000010004" Then
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCuneta("502000010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCuneta("502000010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Else
                        Cuenta2 = RegresaCuneta("502000010004", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100110000", "")
                    Item = 4
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 12) = "502000010003" Then
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCuneta("502000010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCuneta("502000010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Else
                        Cuenta2 = RegresaCuneta("502000010003", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100010000", "")
                    Item = 4
                End If


            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value <= 0 Then 'Tiene Exento
                If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                    If Cuenta_Cargo.Substring(0, 12) = "502000010001" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                    Else
                        Cuenta2 = RegresaCuneta("502000010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                    End If
                    Item = 2
                Else
                    If Cuenta_Cargo.Substring(0, 12) = "502000010002" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                    Else
                        Cuenta2 = RegresaCuneta("502000010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                    End If
                    Item = 2
                End If
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then 'Tiene Grabado
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 12) = "502000010004" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100010000", "")

                    Else
                        Cuenta2 = RegresaCuneta("502000010004", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100110000", "")

                    End If
                    Item = 3
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 12) = "502000010003" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100010000", "")

                    Else
                        Cuenta2 = RegresaCuneta("502000010003", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100010000", "")

                    End If
                    Item = 3
                End If

            End If

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GG" Then

            If Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then ' tiene grabado y exento
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then

                    If Cuenta_Cargo.Substring(0, 8) = "60100010" Then
                        Cuenta2 = RegresaCuneta("60100050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60100050" Then
                        Cuenta2 = RegresaCuneta("60100010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100120000", "")
                    Item = 4
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60100010" Then
                        Cuenta2 = RegresaCuneta("60100020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60100020" Then
                        Cuenta2 = RegresaCuneta("60100010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100020000", "")
                    Item = 4
                End If
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value <= 0 Then 'Tiene Exento
                If Cuenta_Cargo.Substring(0, 8) = "60100010" Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                Else
                    Cuenta2 = RegresaCuneta("60100010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                End If
                Item = 2
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then 'Tiene Grabado
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60100050" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100120000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60100050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100120000", "")

                    End If
                    Item = 3
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60100020" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100020000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60100020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100020000", "")

                    End If
                    Item = 3
                End If



            End If

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "CPP" Then

            If Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then ' tiene grabado y exento
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 12) = "502500010004" Then
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCuneta("502500010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCuneta("502500010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Else
                        Cuenta2 = RegresaCuneta("502500010004", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100110000", "")
                    Item = 4
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 12) = "502500010003" Then
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCuneta("502500010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCuneta("502500010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Else
                        Cuenta2 = RegresaCuneta("502500010003", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100010000", "")
                    Item = 4
                End If

            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value <= 0 Then 'Tiene Exento


                If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                    If Cuenta_Cargo.Substring(0, 12) = "502500010001" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                    Else
                        Cuenta2 = RegresaCuneta("502500010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                    End If
                    Item = 2
                Else
                    If Cuenta_Cargo.Substring(0, 12) = "502500010002" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                    Else
                        Cuenta2 = RegresaCuneta("502500010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                    End If
                    Item = 2
                End If
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then 'Tiene Grabado
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 12) = "502500010004" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100110000", "")

                    Else
                        Cuenta2 = RegresaCuneta("502500010004", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100110000", "")

                    End If
                    Item = 3
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 12) = "502500010003" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100010000", "")

                    Else
                        Cuenta2 = RegresaCuneta("502500010003", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100010000", "")

                    End If
                    Item = 3
                End If

            End If
            'Abonos con cuentas efectivo bancos
            cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Cuenta_Cargo = RegresaCuneta("20100001" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GGPP" Then


            If Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then ' tiene grabado y exento
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60150010" Then
                        Cuenta2 = RegresaCuneta("60150050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60150050" Then
                        Cuenta2 = RegresaCuneta("60150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100120000", "")
                    Item = 4
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60150010" Then
                        Cuenta2 = RegresaCuneta("60150020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60150020" Then
                        Cuenta2 = RegresaCuneta("60150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100020000", "")
                    Item = 4
                End If



            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value <= 0 Then 'Tiene Exento
                If Cuenta_Cargo.Substring(0, 8) = "60150010" Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                Else
                    Cuenta2 = RegresaCuneta("60150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                End If
                Item = 2
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then 'Tiene Grabado
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60150050" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100120000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60150050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100120000", "")

                    End If
                    Item = 3
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60150020" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100020000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60150020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100020000", "")

                    End If
                    Item = 3
                End If

            End If
            'Abonos con cuentas efectivo bancos

            cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Cuenta_Cargo = RegresaCuneta("205000020001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GVPP" Then
            If Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then ' tiene grabado y exento
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60250010" Then
                        Cuenta2 = RegresaCuneta("60250050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60250050" Then
                        Cuenta2 = RegresaCuneta("60250010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100120000", "")
                    Item = 4
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60250010" Then
                        Cuenta2 = RegresaCuneta("60250020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60250020" Then
                        Cuenta2 = RegresaCuneta("60250010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100020000", "")
                    Item = 4
                End If


            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value <= 0 Then 'Tiene Exento
                If Cuenta_Cargo.Substring(0, 8) = "60250010" Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                Else
                    Cuenta2 = RegresaCuneta("60250010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                End If
                Item = 2
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then 'Tiene Grabado
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60250050" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100120000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60250050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100120000", "")

                    End If
                    Item = 3
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60250020" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100020000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60250020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100020000", "")

                    End If
                    Item = 3
                End If


            End If
            'Abonos con cuentas efectivo bancos
            cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Cuenta_Cargo = RegresaCuneta("205000020001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GAPP" Then

            If Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then ' tiene grabado y exento
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60350010" Then
                        Cuenta2 = RegresaCuneta("60350050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60350050" Then
                        Cuenta2 = RegresaCuneta("60350010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100120000", "")
                    Item = 4
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60350010" Then
                        Cuenta2 = RegresaCuneta("60350020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60350020" Then
                        Cuenta2 = RegresaCuneta("60350010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100020000", "")
                    Item = 4
                End If

            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value <= 0 Then 'Tiene Exento
                If Cuenta_Cargo.Substring(0, 8) = "60350010" Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                Else
                    Cuenta2 = RegresaCuneta("60350010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                End If
                Item = 2
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then 'Tiene Grabado
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60350050" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100120000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60350050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100120000", "")

                    End If
                    Item = 3
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60350020" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100020000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60350020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100020000", "")

                    End If
                    Item = 3
                End If

            End If
            'Abonos con cuentas efectivo bancos
            cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Cuenta_Cargo = RegresaCuneta("205000020001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GFPP" Then

            If Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then ' tiene grabado y exento

                If Cuenta_Cargo.Substring(0, 8) = "60450010" Then
                    Cuenta2 = RegresaCuneta("60450020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                    Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                ElseIf Cuenta_Cargo.Substring(0, 8) = "60450020" Then
                    Cuenta2 = RegresaCuneta("60450010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                    Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                End If
                'Iva Gastos
                Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100020000", "")
                Item = 4
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value <= 0 Then 'Tiene Exento
                If Cuenta_Cargo.Substring(0, 8) = "60450010" Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                Else
                    Cuenta2 = RegresaCuneta("60450010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                End If
                Item = 2
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then 'Tiene Grabado
                If Cuenta_Cargo.Substring(0, 8) = "60450020" Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                    Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100020000", "")

                Else
                    Cuenta2 = RegresaCuneta("60450020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                    Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100020000", "")

                End If
                Item = 3
            End If
            'Abonos con cuentas efectivo bancos

            cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Cuenta_Cargo = RegresaCuneta("205000020001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GFI" Then

            If Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then ' tiene grabado y exento
                If Cuenta_Cargo.Substring(0, 12) = "701000040001" Then
                    Cuenta2 = RegresaCuneta("701000040002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    Item = Item + 1
                ElseIf Cuenta_Cargo.Substring(0, 12) = "701000040002" Then
                    Cuenta2 = RegresaCuneta("701000040001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Item = Item + 1
                End If
                'Iva Gastos
                Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100060000", "")
                Item = Item + 1
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value <= 0 Then 'Tiene Exento
                If Cuenta_Cargo.Substring(0, 12) = "701000040001" Then
                    Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                Else
                    Cuenta2 = RegresaCuneta("701000040001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Item = Item + 1
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                End If

            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then 'Tiene Grabado
                If Cuenta_Cargo.Substring(0, 8) = "70100010" Then
                    Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100060000", "")

                Else
                    Cuenta2 = RegresaCuneta("70100010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 1)
                    Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    Item = Item + 1
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                    Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100060000", "")

                End If
                Item = Item + 1
            End If

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GFIPP" Then
            If Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then ' tiene grabado y exento
                If Cuenta_Cargo.Substring(0, 12) = "701500040001" Then
                    Cuenta2 = RegresaCuneta("70150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                    Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                ElseIf Cuenta_Cargo.Substring(0, 8) = "70150010" Then
                    Cuenta2 = RegresaCuneta("701500040001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                    Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                End If
                'Iva Gastos
                Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100060000", "")
                Item = 4
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value <= 0 Then 'Tiene Exento
                If Cuenta_Cargo.Substring(0, 12) = "701500040001" Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                Else
                    Cuenta2 = RegresaCuneta("701500040001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                End If
                Item = 2
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then 'Tiene Grabado
                If Cuenta_Cargo.Substring(0, 8) = "70150010" Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                    Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100060000", "")

                Else
                    Cuenta2 = RegresaCuneta("70150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 1)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                    Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1190000100060000", "")

                End If
                Item = 3
            End If
            'Abonos con cuentas efectivo bancos
            cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Cuenta_Cargo = RegresaCuneta("205000020001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GN" Or Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GNPP" Then

            If Me.TablaImportar.Item(TReal.Index, p).Value > 0 Then ' 
                If Cuenta_Cargo.Substring(0, 8) = "61200001" Then
                    Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(TReal.Index, p).Value, 0, Cuenta_Cargo, "")
                Else
                    Cuenta2 = RegresaCuneta("61200001" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(TReal.Index, p).Value, 0, Cuenta2, "")
                End If
                Item = Item + 1
            End If

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GV" Then

            If Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then ' tiene grabado y exento
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60200010" Then
                        Cuenta2 = RegresaCuneta("60200050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60200050" Then
                        Cuenta2 = RegresaCuneta("60200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100120000", "")
                    Item = 4
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60200010" Then
                        Cuenta2 = RegresaCuneta("60200020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60200020" Then
                        Cuenta2 = RegresaCuneta("60200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100020000", "")
                    Item = 4
                End If

            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value <= 0 Then 'Tiene Exento
                If Cuenta_Cargo.Substring(0, 8) = "60200010" Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                Else
                    Cuenta2 = RegresaCuneta("60200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                End If
                Item = 2
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then 'Tiene Grabado
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60200050" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100120000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60200050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100120000", "")

                    End If
                    Item = 3
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60200020" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100020000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60200020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100020000", "")

                    End If
                    Item = 3
                End If

            End If

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GA" Then

            If Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then ' tiene grabado y exento
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60300010" Then
                        Cuenta2 = RegresaCuneta("60300050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60300050" Then
                        Cuenta2 = RegresaCuneta("60300010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100120000", "")
                    Item = 4
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60300010" Then
                        Cuenta2 = RegresaCuneta("60300020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60300020" Then
                        Cuenta2 = RegresaCuneta("60300010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100020000", "")
                    Item = 4
                End If

            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value <= 0 Then 'Tiene Exento
                If Cuenta_Cargo.Substring(0, 8) = "60300010" Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                Else
                    Cuenta2 = RegresaCuneta("60300010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                End If
                Item = 2
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then 'Tiene Grabado
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60300050" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100120000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60300050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100120000", "")

                    End If
                    Item = 3
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60300020" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100020000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60300020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100020000", "")

                    End If
                    Item = 3
                End If

            End If

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GF" Then
            If Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then ' tiene grabado y exento
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60400010" Then
                        Cuenta2 = RegresaCuneta("60400050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60400050" Then
                        Cuenta2 = RegresaCuneta("60400010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100120000", "")
                    Item = 4
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60400010" Then
                        Cuenta2 = RegresaCuneta("60400020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                    ElseIf Cuenta_Cargo.Substring(0, 8) = "60400020" Then
                        Cuenta2 = RegresaCuneta("60400010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    End If
                    'Iva Gastos
                    Crea_detalle_poliza(pol, 3, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100020000", "")
                    Item = 4
                End If

            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value > 0 And Me.TablaImportar.Item(ImpG.Index, p).Value <= 0 Then 'Tiene Exento
                If Cuenta_Cargo.Substring(0, 8) = "60400010" Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                Else
                    Cuenta2 = RegresaCuneta("60400010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta2, "")
                    Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                End If
                Item = 2
            ElseIf Me.TablaImportar.Item(ImpEx.Index, p).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, p).Value > 0 Then 'Tiene Grabado
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60400050" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100120000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60400050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100120000", "")

                    End If
                    Item = 3
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(0, 8) = "60400020" Then
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta_Cargo, "")
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100020000", "")

                    Else
                        Cuenta2 = RegresaCuneta("60400020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov, 0, Cuenta2, "")
                        Actualiza_Registro_Xml(Trim(Me.TablaImportar.Item(RFCE.Index, p).Value & "-" & Cuenta2), Me.TablaImportar.Item(Id_Xml.Index, p).Value)
                        Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100020000", "")

                    End If
                    Item = 3
                End If

            End If

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "A" Then ' Activos Fijo
            If Me.TablaImportar.Item(ImpEx.Index, p).Value <> 0 Or Me.TablaImportar.Item(ImpG.Index, p).Value <> 0 Then ' tiene grabado y exento
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov + Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                    Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100130000", "")
                    Item = 3
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    Crea_detalle_poliza(pol, 1, Me.TablaImportar.Item(ImpG.Index, p).Value - ImpGprov + Me.TablaImportar.Item(ImpEx.Index, p).Value - ImpEprov, 0, Cuenta_Cargo, "")
                    Crea_detalle_poliza(pol, 2, Me.TablaImportar.Item(IVAR.Index, p).Value - ImpIvaPr, 0, "1180000100030000", "")
                    Item = 3
                End If
            End If
        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "APP" Then ' Activos Fijo
            If ImpEprov <> 0 And ImpGprov <> 0 Then ' tiene grabado y exento
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    Crea_detalle_poliza(pol, Item, ImpGprov + ImpEprov, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100130000", "")
                    Item = Item + 1
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    Crea_detalle_poliza(pol, Item, ImpGprov + ImpEprov, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100030000", "")
                    Item = Item + 1
                End If

            End If
            'Abonos con cuentas efectivo bancos
            cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Cuenta_Cargo = RegresaCuneta("205000020002" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

        End If
        'Caja y Bancos
        If Me.TablaImportar.Item(ImpEf.Index, p).Value > 0 And Me.TablaImportar.Item(ImpT.Index, p).Value > 0 And Me.TablaImportar.Item(ImpC.Index, p).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value > 0 Then 'TODOS

            cadena = Trim(Me.TablaImportar.Item(CuentasEfectivo.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpEf.Index, p).Value, Cuenta_Cargo, "")

            Item = Item + 1

            cadena = Trim(Me.TablaImportar.Item(CuentaBancos.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT.Index, p).Value, Cuenta_Cargo, "")

            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaCheque.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpC.Index, p).Value, Cuenta_Cargo, Me.TablaImportar.Item(NoCheque.Index, p).Value)
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, p).Value.ToString.Substring(0, 19).Replace("-", ""))
            Cuenta_Cargo = cadena
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value, Cuenta_Cargo, "")


        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value > 0 And Me.TablaImportar.Item(ImpT.Index, p).Value > 0 And Me.TablaImportar.Item(ImpC.Index, p).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value = 0 Then 'Efectivo y Transferencia
            cadena = Trim(Me.TablaImportar.Item(CuentasEfectivo.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpEf.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CuentaBancos.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT.Index, p).Value, Cuenta_Cargo, "")

        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value > 0 And Me.TablaImportar.Item(ImpT.Index, p).Value > 0 And Me.TablaImportar.Item(ImpC.Index, p).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value > 0 Then 'Efectivo y Transferencia
            cadena = Trim(Me.TablaImportar.Item(CuentasEfectivo.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpEf.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CuentaBancos.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, p).Value.ToString.Substring(0, 19).Replace("-", ""))
            Cuenta_Cargo = cadena
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value, Cuenta_Cargo, "")

        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value = 0 And Me.TablaImportar.Item(ImpT.Index, p).Value > 0 And Me.TablaImportar.Item(ImpC.Index, p).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value = 0 Then 'Cheque y Transferencia

            cadena = Trim(Me.TablaImportar.Item(CuentaBancos.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaCheque.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpC.Index, p).Value, Cuenta_Cargo, Me.TablaImportar.Item(NoCheque.Index, p).Value)
        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value = 0 And Me.TablaImportar.Item(ImpT.Index, p).Value > 0 And Me.TablaImportar.Item(ImpC.Index, p).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value > 0 Then 'Cheque y Transferencia

            cadena = Trim(Me.TablaImportar.Item(CuentaBancos.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaCheque.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpC.Index, p).Value, Cuenta_Cargo, Me.TablaImportar.Item(NoCheque.Index, p).Value)
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, p).Value.ToString.Substring(0, 19).Replace("-", ""))
            Cuenta_Cargo = cadena
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value, Cuenta_Cargo, "")


        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value > 0 And Me.TablaImportar.Item(ImpT.Index, p).Value = 0 And Me.TablaImportar.Item(ImpC.Index, p).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value = 0 Then 'Cheque y Efecivo
            cadena = Trim(Me.TablaImportar.Item(CuentasEfectivo.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpEf.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaCheque.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpC.Index, p).Value, Cuenta_Cargo, Me.TablaImportar.Item(NoCheque.Index, p).Value)

        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value > 0 And Me.TablaImportar.Item(ImpT.Index, p).Value = 0 And Me.TablaImportar.Item(ImpC.Index, p).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value > 0 Then 'Cheque y Efecivo
            cadena = Trim(Me.TablaImportar.Item(CuentasEfectivo.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpEf.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaCheque.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpC.Index, p).Value, Cuenta_Cargo, Me.TablaImportar.Item(NoCheque.Index, p).Value)
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, p).Value.ToString.Substring(0, 19).Replace("-", ""))
            Cuenta_Cargo = cadena
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value, Cuenta_Cargo, "")
        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value > 0 And Me.TablaImportar.Item(ImpT.Index, p).Value = 0 And Me.TablaImportar.Item(ImpC.Index, p).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value = 0 Then ' Efecivo
            'Cuenta del Abono
            cadena = Trim(Me.TablaImportar.Item(CuentasEfectivo.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)

            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpEf.Index, p).Value, Cuenta_Cargo, "")
        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value > 0 And Me.TablaImportar.Item(ImpT.Index, p).Value = 0 And Me.TablaImportar.Item(ImpC.Index, p).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value > 0 Then ' Efecivo
            'Cuenta del Abono
            cadena = Trim(Me.TablaImportar.Item(CuentasEfectivo.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpEf.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, p).Value.ToString.Substring(0, 19).Replace("-", ""))
            Cuenta_Cargo = cadena
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value, Cuenta_Cargo, "")

        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value = 0 And Me.TablaImportar.Item(ImpT.Index, p).Value > 0 And Me.TablaImportar.Item(ImpC.Index, p).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value = 0 Then ' Transferencia
            'Cuenta del Abono transferencia
            cadena = Trim(Me.TablaImportar.Item(CuentaBancos.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT.Index, p).Value, Cuenta_Cargo, IIf(IsDBNull(Me.TablaImportar.Item(NoCheque.Index, p).Value) = True, "", Me.TablaImportar.Item(NoCheque.Index, p).Value))
        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value = 0 And Me.TablaImportar.Item(ImpT.Index, p).Value > 0 And Me.TablaImportar.Item(ImpC.Index, p).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value > 0 Then ' Transferencia
            'Cuenta del Abono transferencia
            cadena = Trim(Me.TablaImportar.Item(CuentaBancos.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT.Index, p).Value, Cuenta_Cargo, IIf(IsDBNull(Me.TablaImportar.Item(NoCheque.Index, p).Value) = True, "", Me.TablaImportar.Item(NoCheque.Index, p).Value))
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, p).Value.ToString.Substring(0, 19).Replace("-", ""))
            Cuenta_Cargo = cadena
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value, Cuenta_Cargo, "")
        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value = 0 And Me.TablaImportar.Item(ImpT.Index, p).Value = 0 And Me.TablaImportar.Item(ImpC.Index, p).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value = 0 Then 'Cheque 
            cadena = Trim(Me.TablaImportar.Item(CtaCheque.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpC.Index, p).Value, Cuenta_Cargo, Me.TablaImportar.Item(NoCheque.Index, p).Value)
        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value = 0 And Me.TablaImportar.Item(ImpT.Index, p).Value = 0 And Me.TablaImportar.Item(ImpC.Index, p).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value > 0 Then 'Cheque 
            cadena = Trim(Me.TablaImportar.Item(CtaCheque.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpC.Index, p).Value, Cuenta_Cargo, Me.TablaImportar.Item(NoCheque.Index, p).Value)
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, p).Value.ToString.Substring(0, 19).Replace("-", ""))
            Cuenta_Cargo = cadena
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value, Cuenta_Cargo, "")
        ElseIf Me.TablaImportar.Item(ImpEf.Index, p).Value = 0 And Me.TablaImportar.Item(ImpT.Index, p).Value = 0 And Me.TablaImportar.Item(ImpC.Index, p).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value > 0 Then 'Cheque 

            cadena = Trim(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, p).Value.ToString.Substring(0, 19).Replace("-", ""))
            Cuenta_Cargo = cadena
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, p).Value, Cuenta_Cargo, "")
        End If

        'si tienes mas tranf2
        If Me.TablaImportar.Item(ImpT2.Index, p).Value > 0 Then
            cadena = Trim(Me.TablaImportar.Item(CtaT2.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT2.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
        End If
        'si tienes mas tranf3
        If Me.TablaImportar.Item(ImpT3.Index, p).Value > 0 Then
            cadena = Trim(Me.TablaImportar.Item(CtaT3.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT3.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
        End If
        'si tienes mas tranf4
        If Me.TablaImportar.Item(ImpT4.Index, p).Value > 0 Then
            cadena = Trim(Me.TablaImportar.Item(CtaT4.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT4.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
        End If

        If Me.TablaImportar.Item(ImpT5.Index, p).Value > 0 Then
            cadena = Trim(Me.TablaImportar.Item(CtaT5.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT5.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
        End If
        If Me.TablaImportar.Item(ImpT6.Index, p).Value > 0 Then
            cadena = Trim(Me.TablaImportar.Item(CtaT6.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT6.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
        End If
        If Me.TablaImportar.Item(ImpT7.Index, p).Value > 0 Then
            cadena = Trim(Me.TablaImportar.Item(CtaT7.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpT7.Index, p).Value, Cuenta_Cargo, "")
            Item = Item + 1
        End If
        'Inserta Anticipos
        If Me.TablaImportar.Item(Anti.Index, p).Value > 0 Then ' Se verifica si cuenta con anticipos +
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaAnti.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(Anti.Index, p).Value, Cuenta_Cargo, "")
        ElseIf Me.TablaImportar.Item(Anti.Index, p).Value < 0 Then ' Se verifica si cuenta  anticipos -
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaAnti.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(Anti.Index, p).Value * -1, 0, Cuenta_Cargo, "")
        End If
        'Inserta Ajustes
        If Me.TablaImportar.Item(Ajus.Index, p).Value > 0 Then ' Se verifica si cuenta con ajuste +
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaAjustes.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            'cargo de Ajuste
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(Ajus.Index, p).Value, Cuenta_Cargo, "")

        ElseIf Me.TablaImportar.Item(Ajus.Index, p).Value < 0 Then ' Se verifica si cuenta con ajuste -
            Item = Item + 1
            cadena = Trim(Me.TablaImportar.Item(CtaAjustes.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            'Abono de Ajuste
            Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(Ajus.Index, p).Value * -1, 0, Cuenta_Cargo, "")
        End If
        'Perdida cambiaria
        If Me.TablaImportar.Item(PCambiaria.Index, p).Value > 0 Then ' Se perdida cambiaria
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, Me.TablaImportar.Item(PCambiaria.Index, p).Value, 0, "7010000100000000", "")
        End If
        'utilidad Cambiaria
        If Me.TablaImportar.Item(UCambaria.Index, p).Value > 0 Then ' Se utilidad cambiaria
            Item = Item + 1
            Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(UCambaria.Index, p).Value, "7020000100000000", "")
        End If
        ' Retenciones
        If Me.TablaImportar.Item(RIVA.Index, p).Value > 0 Then
            If Me.TablaImportar.Item(ImpProvis.Index, p).Value = (Me.TablaImportar.Item(TReal.Index, p).Value - (Me.TablaImportar.Item(RIVA.Index, p).Value + Me.TablaImportar.Item(RISR.Index, p).Value)) Then
                Item = Item + 1
                cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    If Cuenta_Cargo.Substring(8, 4) = "0034" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, p).Value), "2165001300010000", "")
                    ElseIf Cuenta_Cargo.Substring(8, 4) = "0045" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, p).Value), "2165001300020000", "")
                    ElseIf Cuenta_Cargo.Substring(8, 4) = "0072" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, p).Value), "2165001300030000", "")
                    End If
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    If Cuenta_Cargo.Substring(8, 4) = "0034" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, p).Value), "2165001000010000", "")
                    ElseIf Cuenta_Cargo.Substring(8, 4) = "0045" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, p).Value), "2165001000020000", "")
                    ElseIf Cuenta_Cargo.Substring(8, 4) = "0072" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, p).Value), "2165001000030000", "")
                    End If
                End If

            Else
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                    Item = Item + 1
                    Edita_iva(pol, "1180000100120000", Me.TablaImportar.Item(RIVA.Index, p).Value * (1 - PorcenProv))
                    If Math.Round(Me.TablaImportar.Item(RIVA.Index, p).Value) <> Math.Round(Me.TablaImportar.Item(IVAR.Index, p).Value) Then
                        Crea_detalle_poliza(pol, Item, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * (1 - PorcenProv), Me.TablaImportar.Item(RIVA.Index, p).Value), 0, "1190000100170000", "")
                    End If
                    cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                    posi = InStr(1, cadena, "-", CompareMethod.Binary)
                    cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                    Cuenta_Cargo = cadena.Substring(posi, cuantos)
                    If Cuenta_Cargo.Substring(8, 4) = "0034" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * (1 - PorcenProv), Me.TablaImportar.Item(RIVA.Index, p).Value), "2160001300010000", "")
                    ElseIf Cuenta_Cargo.Substring(8, 4) = "0045" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * (1 - PorcenProv), Me.TablaImportar.Item(RIVA.Index, p).Value), "2160001300020000", "")
                    ElseIf Cuenta_Cargo.Substring(8, 4) = "0072" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * (1 - PorcenProv), Me.TablaImportar.Item(RIVA.Index, p).Value), "2160001300030000", "")
                    ElseIf Cuenta_Cargo.Substring(8, 4) = "0003" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * (1 - PorcenProv), Me.TablaImportar.Item(RIVA.Index, p).Value), "2160001300040000", "")

                    End If
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                    Item = Item + 1
                    Edita_iva(pol, "1180000100020000", Me.TablaImportar.Item(RIVA.Index, p).Value * (1 - PorcenProv))
                    If Math.Round(Me.TablaImportar.Item(RIVA.Index, p).Value) <> Math.Round(Me.TablaImportar.Item(IVAR.Index, p).Value) Then
                        Crea_detalle_poliza(pol, Item, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * (1 - PorcenProv), Me.TablaImportar.Item(RIVA.Index, p).Value), 0, "1190000100070000", "")
                    End If
                    cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                    posi = InStr(1, cadena, "-", CompareMethod.Binary)
                    cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                    Cuenta_Cargo = cadena.Substring(posi, cuantos)
                    If Cuenta_Cargo.Substring(8, 4) = "0034" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * (1 - PorcenProv), Me.TablaImportar.Item(RIVA.Index, p).Value), "2160001000010000", "")
                    ElseIf Cuenta_Cargo.Substring(8, 4) = "0045" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * (1 - PorcenProv), Me.TablaImportar.Item(RIVA.Index, p).Value), "2160001000020000", "")
                    ElseIf Cuenta_Cargo.Substring(8, 4) = "0072" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * (1 - PorcenProv), Me.TablaImportar.Item(RIVA.Index, p).Value), "2160001000030000", "")
                    ElseIf Cuenta_Cargo.Substring(8, 4) = "0003" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * (1 - PorcenProv), Me.TablaImportar.Item(RIVA.Index, p).Value), "2160001000040000", "")

                    End If
                End If


            End If




        End If

        If Me.TablaImportar.Item(RISR.Index, p).Value > 0 Then
            cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
            posi = InStr(1, cadena, "-", CompareMethod.Binary)
            cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
            Cuenta_Cargo = cadena.Substring(posi, cuantos)
            If Me.TablaImportar.Item(ImpProvis.Index, p).Value = (Me.TablaImportar.Item(TReal.Index, p).Value - (Me.TablaImportar.Item(RIVA.Index, p).Value + Me.TablaImportar.Item(RISR.Index, p).Value)) Then
                If Cuenta_Cargo.Substring(8, 4) = "0034" Then

                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RISR.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RISR.Index, p).Value), "2165000400000000", "")
                ElseIf Cuenta_Cargo.Substring(8, 4) = "0045" Then
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RISR.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RISR.Index, p).Value), "2165000300000000", "")
                End If
            Else
                If Cuenta_Cargo.Substring(8, 4) = "0034" Then

                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RISR.Index, p).Value * (1 - PorcenProv), Me.TablaImportar.Item(RISR.Index, p).Value), "2160000400000000", "")
                ElseIf Cuenta_Cargo.Substring(8, 4) = "0045" Then
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RISR.Index, p).Value * (1 - PorcenProv), Me.TablaImportar.Item(RISR.Index, p).Value), "2160000300000000", "")
                End If
            End If

        End If


        'Insertar Provisiones O Ctas De orden
        If Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "C" Then ' Compras 
            'Inserta Registros Combinada Provision Compras
            If Me.TablaImportar.Item(ImpProvis.Index, p).Value > 0 Then
                cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Item = Item + 1
                If ImpEprov > 0 And ImpGprov > 0 Then ' tiene grabado y exento
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta_Cargo = RegresaCuneta("502500010004", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCuneta("502500010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCuneta("502500010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        End If

                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta_Cargo, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        'Iva Gastos
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100110000", "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta_Cargo = RegresaCuneta("502500010003", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCuneta("502500010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCuneta("502500010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        End If

                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta_Cargo, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        'Iva Gastos
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100010000", "")
                        Item = Item + 1
                    End If

                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Cuenta2 = RegresaCuneta("502500010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Else
                        Cuenta2 = RegresaCuneta("502500010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    End If
                    Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                    Item = Item + 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta2 = RegresaCuneta("502500010004", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100110000", "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta2 = RegresaCuneta("502500010003", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100010000", "")
                        Item = Item + 1
                    End If
                End If

                cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Cuenta_Cargo = RegresaCuneta("20100001" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")


                If UCase(Me.TablaImportar.Item(CtaOrden.Index, p).Value) = "SI" Then
                    Item = Item + 1
                    If ImpEprov > 0 And ImpGprov > 0 Then
                        If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                            Cuenta2 = RegresaCuneta("901000010004", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Cuenta2 = RegresaCuneta("901000010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            Else
                                Cuenta2 = RegresaCuneta("901000010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            End If
                            Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                            Item = Item + 1
                        ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                            Cuenta2 = RegresaCuneta("901000010003", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Cuenta2 = RegresaCuneta("901000010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            Else
                                Cuenta2 = RegresaCuneta("901000010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            End If
                            Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                            Item = Item + 1
                        End If


                    ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCuneta("901000010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCuneta("901000010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        Item = Item + 1
                    ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                        If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                            Cuenta2 = RegresaCuneta("901000010004", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                            Item = Item + 1
                        ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                            Cuenta2 = RegresaCuneta("901000010003", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                            Item = Item + 1
                        End If

                    End If

                    If ImpEprov > 0 And ImpGprov > 0 Then
                        If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                            Crea_detalle_poliza(pol, Item, 0, ImpGprov, "9011001000010004", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "9011001000010001", "")
                            Else
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "9011001000010002", "")
                            End If
                            Item = Item + 1
                        ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                            Crea_detalle_poliza(pol, Item, 0, ImpGprov, "9011001000010003", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "9011001000010001", "")
                            Else
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "9011001000010002", "")
                            End If
                            Item = Item + 1
                        End If


                    ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Crea_detalle_poliza(pol, Item, 0, ImpEprov, "9011001000010001", "")
                        Else
                            Crea_detalle_poliza(pol, Item, 0, ImpEprov, "9011001000010002", "")
                        End If
                        Item = Item + 1
                    ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                        If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                            Crea_detalle_poliza(pol, Item, 0, ImpGprov, "9011001000010004", "")
                            Item = Item + 1
                        ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                            Crea_detalle_poliza(pol, Item, 0, ImpGprov, "9011001000010003", "")
                            Item = Item + 1
                        End If

                    End If

                End If
            End If
            Exit Sub
        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GG" Then
            'Inserta Registros Combinada Provision Gastos
            If Me.TablaImportar.Item(ImpProvis.Index, p).Value > 0 Then
                cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Item = Item + 1
                If ImpEprov > 0 And ImpGprov > 0 Then ' tiene grabado y exento
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta_Cargo = RegresaCuneta("60150050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Cuenta2 = RegresaCuneta("60150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta_Cargo, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        'Iva Gastos
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100120000", "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta_Cargo = RegresaCuneta("60150020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Cuenta2 = RegresaCuneta("60150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta_Cargo, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        'Iva Gastos
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100020000", "")
                        Item = Item + 1
                    End If



                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    Cuenta2 = RegresaCuneta("60150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                    Item = Item + 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta2 = RegresaCuneta("60150050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100120000", "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta2 = RegresaCuneta("60150020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100020000", "")
                        Item = Item + 1
                    End If

                End If
                Cuenta_Cargo = RegresaCuneta("205000020001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")


                If UCase(Me.TablaImportar.Item(CtaOrden.Index, p).Value) = "SI" Then
                    Item = Item + 1
                    If ImpEprov > 0 And ImpGprov > 0 Then
                        If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                            Cuenta2 = RegresaCuneta("90200050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Cuenta2 = RegresaCuneta("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            Else
                                Cuenta2 = RegresaCuneta("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            End If
                            Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                            Item = Item + 1
                        ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                            Cuenta2 = RegresaCuneta("90200020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Cuenta2 = RegresaCuneta("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            Else
                                Cuenta2 = RegresaCuneta("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            End If
                            Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                            Item = Item + 1
                        End If


                    ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCuneta("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCuneta("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        Item = Item + 1
                    ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                        If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                            Cuenta2 = RegresaCuneta("90200050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                            Item = Item + 1
                        ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                            Cuenta2 = RegresaCuneta("90200020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                            Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                            Item = Item + 1
                        End If

                    End If



                    If Len(Me.TablaImportar.Item(RFCE.Index, p).Value) = 12 Then
                        If ImpEprov > 0 And ImpGprov > 0 Then 'Moral

                            If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                                'Solicitar Cuenta
                                Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100100050", "")
                                Item = Item + 1
                                If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                    Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100100010", "")
                                Else
                                    Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100100010", "")
                                End If
                                Item = Item + 1
                            ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                                'Solicitar Cuenta
                                Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100100020", "")
                                Item = Item + 1
                                If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                    Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100100010", "")
                                Else
                                    Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100100010", "")
                                End If
                                Item = Item + 1
                            End If


                        ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100100010", "")
                            Else
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100100010", "")
                            End If
                            Item = Item + 1
                        ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                            If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                                Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100100050", "")
                                Item = Item + 1
                            ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                                Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100100020", "")
                                Item = Item + 1
                            End If

                        End If
                    Else 'Fisica

                        If ImpEprov > 0 And ImpGprov > 0 Then
                            If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                                'Solicitar Cuenta
                                Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100200050", "")
                                Item = Item + 1
                                If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                    Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100200010", "")
                                Else
                                    Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100200010", "")
                                End If
                                Item = Item + 1
                            ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                                'Solicitar Cuenta
                                Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100200020", "")
                                Item = Item + 1
                                If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                    Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100200010", "")
                                Else
                                    Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100200010", "")
                                End If
                                Item = Item + 1
                            End If


                        ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100200010", "")
                            Else
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100200010", "")
                            End If
                            Item = Item + 1
                        ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                            If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                                Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100200050", "")
                                Item = Item + 1
                            ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                                Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100200020", "")
                                Item = Item + 1
                            End If

                        End If
                    End If
                End If


                If Me.TablaImportar.Item(RIVA.Index, p).Value > 0 Then

                    cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                    posi = InStr(1, cadena, "-", CompareMethod.Binary)
                    cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                    Cuenta_Cargo = cadena.Substring(posi, cuantos)
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        If Cuenta_Cargo.Substring(8, 4) = "0034" Then
                            Item = Item + 1
                            Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, p).Value), "2165001300010000", "")
                        ElseIf Cuenta_Cargo.Substring(8, 4) = "0045" Then
                            Item = Item + 1
                            Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, p).Value), "2165001300020000", "")
                        ElseIf Cuenta_Cargo.Substring(8, 4) = "0072" Then
                            Item = Item + 1
                            Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, p).Value), "2165001300030000", "")
                        End If

                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        If Cuenta_Cargo.Substring(8, 4) = "0034" Then
                            Item = Item + 1
                            Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, p).Value), "2165001000010000", "")
                        ElseIf Cuenta_Cargo.Substring(8, 4) = "0045" Then
                            Item = Item + 1
                            Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, p).Value), "2165001000020000", "")
                        ElseIf Cuenta_Cargo.Substring(8, 4) = "0072" Then
                            Item = Item + 1
                            Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, p).Value), "2165001000030000", "")
                        End If
                    End If


                End If
                If Me.TablaImportar.Item(RISR.Index, p).Value > 0 Then
                    cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                    posi = InStr(1, cadena, "-", CompareMethod.Binary)
                    cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                    Cuenta_Cargo = cadena.Substring(posi, cuantos)

                    If Cuenta_Cargo.Substring(8, 4) = "0034" Then

                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RISR.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RISR.Index, p).Value), "2165000400000000", "")
                    ElseIf Cuenta_Cargo.Substring(8, 4) = "0045" Then
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RISR.Index, p).Value * PorcenProv, Me.TablaImportar.Item(RISR.Index, p).Value), "2165000300000000", "")
                    End If


                End If


            End If
            Exit Sub
        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "CPP" Then
            If UCase(Me.TablaImportar.Item(CtaOrden.Index, p).Value) = "SI" Then
                Item = Item + 1
                If ImpEprov > 0 And ImpGprov > 0 Then
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta2 = RegresaCuneta("901000010004", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCuneta("901000010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCuneta("901000010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta2 = RegresaCuneta("901000010003", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCuneta("901000010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCuneta("901000010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        Item = Item + 1
                    End If


                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Cuenta2 = RegresaCuneta("901000010001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Else
                        Cuenta2 = RegresaCuneta("901000010002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    End If
                    Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                    Item = Item + 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta2 = RegresaCuneta("901000010004", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta2 = RegresaCuneta("901000010003", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                    End If

                End If

                If ImpEprov > 0 And ImpGprov > 0 Then
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Crea_detalle_poliza(pol, Item, 0, ImpGprov, "9011001000010004", "")
                        Item = Item + 1
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Crea_detalle_poliza(pol, Item, 0, ImpEprov, "9011001000010001", "")
                        Else
                            Crea_detalle_poliza(pol, Item, 0, ImpEprov, "9011001000010002", "")
                        End If
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Crea_detalle_poliza(pol, Item, 0, ImpGprov, "9011001000010003", "")
                        Item = Item + 1
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Crea_detalle_poliza(pol, Item, 0, ImpEprov, "9011001000010001", "")
                        Else
                            Crea_detalle_poliza(pol, Item, 0, ImpEprov, "9011001000010002", "")
                        End If
                        Item = Item + 1
                    End If


                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Crea_detalle_poliza(pol, Item, 0, ImpEprov, "9011001000010001", "")
                    Else
                        Crea_detalle_poliza(pol, Item, 0, ImpEprov, "9011001000010002", "")
                    End If
                    Item = Item + 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Crea_detalle_poliza(pol, Item, 0, ImpGprov, "9011001000010004", "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Crea_detalle_poliza(pol, Item, 0, ImpGprov, "9011001000010003", "")
                        Item = Item + 1
                    End If

                End If

            End If
            Exit Sub
        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GGPP" Then
            If UCase(Me.TablaImportar.Item(CtaOrden.Index, p).Value) = "SI" Then
                Item = Item + 1
                If ImpEprov > 0 And ImpGprov > 0 Then
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta2 = RegresaCuneta("90200050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCuneta("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCuneta("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta2 = RegresaCuneta("90200020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Cuenta2 = RegresaCuneta("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Else
                            Cuenta2 = RegresaCuneta("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        End If
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        Item = Item + 1
                    End If


                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                        Cuenta2 = RegresaCuneta("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Else
                        Cuenta2 = RegresaCuneta("90200010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    End If
                    Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                    Item = Item + 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta2 = RegresaCuneta("90200050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta2 = RegresaCuneta("90200020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                    End If

                End If



                If Len(Me.TablaImportar.Item(RFCE.Index, p).Value) = 12 Then
                    If ImpEprov > 0 And ImpGprov > 0 Then 'Moral
                        'Solicitar Cuenta
                        If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                            Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100100050", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100100010", "")
                            Else
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100100010", "")
                            End If
                            Item = Item + 1
                        ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                            Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100100020", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100100010", "")
                            Else
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100100010", "")
                            End If
                            Item = Item + 1
                        End If


                    ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100100010", "")
                        Else
                            Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100100010", "")
                        End If
                        Item = Item + 1
                    ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                        If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                            Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100100050", "")
                            Item = Item + 1
                        ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                            Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100100020", "")
                            Item = Item + 1
                        End If

                    End If
                Else 'Fisica

                    If ImpEprov > 0 And ImpGprov > 0 Then
                        If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                            'Solicitar Cuenta
                            Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100200050", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100200010", "")
                            Else
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100200010", "")
                            End If
                            Item = Item + 1
                        ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                            'Solicitar Cuenta
                            Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100200020", "")
                            Item = Item + 1
                            If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100200010", "")
                            Else
                                Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100200010", "")
                            End If
                            Item = Item + 1
                        End If


                    ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                        If Trim(UCase(Me.lble.Text)) = "TRUE" Then
                            Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100200010", "")
                        Else
                            Crea_detalle_poliza(pol, Item, 0, ImpEprov, "902100200010", "")
                        End If
                        Item = Item + 1
                    ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                        If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                            Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100200050", "")
                            Item = Item + 1
                        ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                            Crea_detalle_poliza(pol, Item, 0, ImpGprov, "902100200020", "")
                            Item = Item + 1
                        End If

                    End If
                End If
            End If
            Exit Sub

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GFI" Then
            If Me.TablaImportar.Item(ImpProvis.Index, p).Value > 0 Then
                cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)

                If ImpEprov > 0 And ImpGprov > 0 Then ' tiene grabado y exento
                    Cuenta_Cargo = RegresaCuneta("70150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Cuenta2 = RegresaCuneta("701500040001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                    'Iva Gastos
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100060000", "")
                    Item = Item + 1
                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    Cuenta2 = RegresaCuneta("701500040001" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                    Item = Item + 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    Cuenta2 = RegresaCuneta("70150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100060000", "")
                    Item = Item + 1
                End If
                Cuenta_Cargo = RegresaCuneta("205000020001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

            End If
            Exit Sub
        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GN" Then
            'Inserta Registros Combinada Provision Gastos
            If Me.TablaImportar.Item(ImpProvis.Index, p).Value > 0 Then
                cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Item = Item + 1
                If ImpEprov > 0 And ImpGprov > 0 Then ' tiene grabado y exento
                    Cuenta_Cargo = RegresaCuneta("70150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Cuenta2 = RegresaCuneta("701500040001" & Cuenta_Cargo.Substring(12, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta_Cargo, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                    'Iva Gastos
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100060000", "")
                    Item = Item + 1
                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    Cuenta2 = RegresaCuneta("701500040001" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                    Item = Item + 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    Cuenta2 = RegresaCuneta("70150010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                    Item = Item + 1
                    Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100060000", "")
                    Item = Item + 1
                End If
                Cuenta_Cargo = RegresaCuneta("205000020001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

            End If
            Exit Sub
        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GV" Then
            'Inserta Registros Combinada Provision Gastos
            If Me.TablaImportar.Item(ImpProvis.Index, p).Value > 0 Then
                cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Item = Item + 1
                If ImpEprov > 0 And ImpGprov > 0 Then ' tiene grabado y exento

                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta_Cargo = RegresaCuneta("60250050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Cuenta2 = RegresaCuneta("60250010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta_Cargo, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        'Iva Gastos
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100120000", "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta_Cargo = RegresaCuneta("60250020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Cuenta2 = RegresaCuneta("60250010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta_Cargo, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        'Iva Gastos
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100020000", "")
                        Item = Item + 1
                    End If

                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    Cuenta2 = RegresaCuneta("60250010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                    Item = Item + 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta2 = RegresaCuneta("60250050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100120000", "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta2 = RegresaCuneta("60250020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100020000", "")
                        Item = Item + 1
                    End If

                End If
                Cuenta_Cargo = RegresaCuneta("205000020001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

            End If
            Exit Sub
        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GA" Then
            'Inserta Registros Combinada Provision Gastos
            If Me.TablaImportar.Item(ImpProvis.Index, p).Value > 0 Then
                cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Item = Item + 1
                If ImpEprov > 0 And ImpGprov > 0 Then ' tiene grabado y exento
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta_Cargo = RegresaCuneta("60350050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Cuenta2 = RegresaCuneta("60350010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta_Cargo, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        'Iva Gastos
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100120000", "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta_Cargo = RegresaCuneta("60350020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Cuenta2 = RegresaCuneta("60350010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta_Cargo, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        'Iva Gastos
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100020000", "")
                        Item = Item + 1
                    End If

                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    Cuenta2 = RegresaCuneta("60350010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                    Item = Item + 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta2 = RegresaCuneta("60350050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100120000", "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta2 = RegresaCuneta("60350020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100020000", "")
                        Item = Item + 1
                    End If

                End If
                Cuenta_Cargo = RegresaCuneta("205000020001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

            End If
            Exit Sub
        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GF" Then
            'Inserta Registros Combinada Provision Gastos
            If Me.TablaImportar.Item(ImpProvis.Index, p).Value > 0 Then
                cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Item = Item + 1
                If ImpEprov > 0 And ImpGprov > 0 Then ' tiene grabado y exento
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta_Cargo = RegresaCuneta("60450050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Cuenta2 = RegresaCuneta("60450010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta_Cargo, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        'Iva Gastos
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100120000", "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta_Cargo = RegresaCuneta("60450020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Cuenta2 = RegresaCuneta("60450010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta_Cargo, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                        'Iva Gastos
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100020000", "")
                        Item = Item + 1
                    End If

                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    Cuenta2 = RegresaCuneta("60450010" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, ImpEprov, 0, Cuenta2, "")
                    Item = Item + 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Cuenta2 = RegresaCuneta("60450050" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100120000", "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Cuenta2 = RegresaCuneta("60450020" & Cuenta_Cargo.Substring(8, 4), Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                        Crea_detalle_poliza(pol, Item, ImpGprov, 0, Cuenta2, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100020000", "")
                        Item = Item + 1
                    End If

                End If
                Cuenta_Cargo = RegresaCuneta("205000020001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

            End If
            Exit Sub
        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "A" Then ' Activos Fijo
            If Me.TablaImportar.Item(ImpProvis.Index, p).Value > 0 Then
                cadena = Trim(Me.TablaImportar.Item(NCuenta.Index, p).Value)
                posi = InStr(1, cadena, "-", CompareMethod.Binary)
                cuantos = Len(cadena) - Len(cadena.Substring(0, posi))
                Cuenta_Cargo = cadena.Substring(posi, cuantos)
                Item = Item + 1
                If ImpEprov <> 0 And ImpGprov <> 0 Then ' tiene grabado y exento
                    If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) = 0.00 Then
                        Crea_detalle_poliza(pol, Item, ImpGprov + ImpEprov, 0, Cuenta_Cargo, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100030000", "")
                        Item = Item + 1
                    ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, p).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(CIVA.Index, p).Value) >= 0.00 Then
                        Crea_detalle_poliza(pol, Item, ImpGprov + ImpEprov, 0, Cuenta_Cargo, "")
                        Item = Item + 1
                        Crea_detalle_poliza(pol, Item, ImpIvaPr, 0, "1190000100130000", "")
                        Item = Item + 1
                    End If

                End If
                Cuenta_Cargo = RegresaCuneta("205000020002", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(ImpProvis.Index, p).Value, Cuenta_Cargo, "")

            End If
            Exit Sub

        ElseIf Trim(Me.TablaImportar.Item(ContabilizacionC.Index, p).Value) = "GNPP" Then ' Activos Fijo
            If Me.TablaImportar.Item(ImpProvis.Index, p).Value > 0 Then

                If Me.TablaImportar.Item(TReal.Index, p).Value > 0 Then ' 
                    Cuenta2 = RegresaCuneta("2050000600001", Trim(Me.TablaImportar.Item(RFCE.Index, p).Value), p, 0)
                    Crea_detalle_poliza(pol, Item, 0, Me.TablaImportar.Item(TReal.Index, p).Value, Cuenta2, "")
                    Item = Item + 1
                End If

            End If
            Exit Sub
        End If

    End Sub
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

    Private Function CuentasAnticiposPPD(ByVal Clave As String)

    End Function
    Private Sub NuevoDetalle(ByVal Fila As Integer, ByVal Poliza As String)
        Dim Item As Integer = 1
        Dim Cuenta(6)
        Dim CuentaPPD(11)
        Dim CuentaCO(7)
        Dim CuentaRetenciones(8)
        'RegresaCuentaNuevoFactura
        Dim Nivel() As String = Split(Me.TablaImportar.Item(NCuenta.Index, Fila).Value.ToString.Trim(), "-")

        Dim PorcenProv, ImpGprov, ImpEprov, ImpIvaPr As Decimal
        PorcenProv = Math.Round(Convert.ToDecimal(Me.TablaImportar.Item(ImpProvis.Index, Fila).Value), 2) / (Math.Round(Me.TablaImportar.Item(TReal.Index, Fila).Value, 2) - (Math.Round(IIf(Me.TablaImportar.Item(RIVA.Index, Fila).Value < 0, 0, Me.TablaImportar.Item(RIVA.Index, Fila).Value), 2) + Math.Round(IIf(Me.TablaImportar.Item(RISR.Index, Fila).Value < 0, 0, Me.TablaImportar.Item(RISR.Index, Fila).Value), 2)))
        If PorcenProv = 1 Then
            ImpGprov = Me.TablaImportar.Item(ImpG.Index, Fila).Value
            ImpEprov = Me.TablaImportar.Item(ImpEx.Index, Fila).Value
            ImpIvaPr = Me.TablaImportar.Item(IVAR.Index, Fila).Value - Me.TablaImportar.Item(RIVA.Index, Fila).Value
        Else
            ImpGprov = Me.TablaImportar.Item(ImpG.Index, Fila).Value * PorcenProv
            ImpEprov = Me.TablaImportar.Item(ImpEx.Index, Fila).Value * PorcenProv
            ImpIvaPr = (Me.TablaImportar.Item(IVAR.Index, Fila).Value - Me.TablaImportar.Item(RIVA.Index, Fila).Value) * PorcenProv
        End If


        Cuenta = CuentasPue(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())

        Dim NivelEp, NivelGp As String

        Try
            Select Case Cuenta(3)
                Case 2
                    NivelEp = Cuenta(2).ToString.Substring(0, 4)
                Case 3
                    NivelEp = Cuenta(2).ToString.Substring(4, 4)
                Case 4
                    NivelEp = Cuenta(2).ToString.Substring(8, 4)
            End Select
        Catch ex As Exception
            NivelEp = ""
        End Try
        Try
            Select Case Cuenta(1)
                Case 2
                    NivelGp = Cuenta(0).ToString.Substring(0, 4)
                Case 3
                    NivelGp = Cuenta(0).ToString.Substring(4, 4)
                Case 4
                    NivelGp = Cuenta(0).ToString.Substring(8, 4)
            End Select
        Catch ex As Exception
            NivelGp = ""
        End Try


        If Nivel(1).Substring(12, 4) = "0000" And Nivel(1).Substring(8, 4) > "0000" Then ' Segundo nivel
            Nivel(1) = Nivel(1).Substring(4, 4)
        ElseIf Nivel(1).Substring(12, 4) > "0000" And Nivel(1).Substring(8, 4) > "0000" Then 'Tercer Nivel
            Nivel(1) = Nivel(1).Substring(8, 4)
        ElseIf Nivel(1).Substring(4, 4) > "0000" And Nivel(1).Substring(8, 4) = "0000" And Nivel(1).Substring(12, 4) = "0000" Then 'Primer Nivel
            Nivel(1) = Nivel(1).Substring(0, 4)
        End If
        If PorcenProv < 1 Then
            If Me.TablaImportar.Item(ImpEx.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpG.Index, Fila).Value > 0 Then ' tiene grabado y exento
                Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(ImpG.Index, Fila).Value - ImpGprov, 0, RegresaCuentaNuevoFactura(Cuenta(0), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelGp, Cuenta(1)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(ImpEx.Index, Fila).Value - ImpEprov, 0, RegresaCuentaNuevoFactura(Cuenta(2), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelEp, Cuenta(3)), "")
                Item += 1
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) = 0.00 Then
                    Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(iva8.Index, Fila).Value - Me.TablaImportar.Item(RIVA.Index, Fila).Value - ImpIvaPr, 0, Cuenta(4), "")
                    Item += 1
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) >= 0.00 Then
                    Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(IVAR.Index, Fila).Value - Me.TablaImportar.Item(RIVA.Index, Fila).Value - ImpIvaPr, 0, Cuenta(4), "")
                    Item += 1
                End If
            ElseIf Me.TablaImportar.Item(ImpEx.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpG.Index, Fila).Value <= 0 Then 'Tiene Exento
                Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(ImpEx.Index, Fila).Value - ImpEprov, 0, RegresaCuentaNuevoFactura(Cuenta(2), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelEp, Cuenta(3)), "")
                Item += 1
            ElseIf Me.TablaImportar.Item(ImpEx.Index, Fila).Value <= 0 And Me.TablaImportar.Item(ImpG.Index, Fila).Value > 0 Then 'Tiene Grabado
                Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(ImpG.Index, Fila).Value - ImpGprov, 0, RegresaCuentaNuevoFactura(Cuenta(0), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelGp, Cuenta(1)), "")
                Item += 1
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) = 0.00 Then
                    Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(iva8.Index, Fila).Value - Me.TablaImportar.Item(RIVA.Index, Fila).Value - ImpIvaPr, 0, Cuenta(4), "")
                    Item += 1
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) >= 0.00 Then
                    Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(IVAR.Index, Fila).Value - Me.TablaImportar.Item(RIVA.Index, Fila).Value - ImpIvaPr, 0, Cuenta(4), "")
                    Item += 1
                End If
            End If

        End If
        Dim Cadena()
        'Pagos
        If Me.TablaImportar.Item(ImpEf.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value > 0 Then 'TODOS

            Cadena = Split(Me.TablaImportar.Item(CuentasEfectivo.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpEf.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT.Index, Fila).Value, Me.TablaImportar.Item(CuentaBancos.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpC.Index, Fila).Value, Me.TablaImportar.Item(CtaCheque.Index, Fila).Value.ToString.Trim(), "")
            Item += 1

            Cadena = Split(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, Fila).Value.ToString().Trim(), "/")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value, Cadena(1).ToString().Replace("-", ""), "")
            Item += 1

        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value = 0 Then 'Efectivo y Transferencia
            Cadena = Split(Me.TablaImportar.Item(CuentasEfectivo.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpEf.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT.Index, Fila).Value, Me.TablaImportar.Item(CuentaBancos.Index, Fila).Value.ToString().Trim(), "")
            Item += 1

        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value > 0 Then 'Efectivo y Transferencia
            Cadena = Split(Me.TablaImportar.Item(CuentasEfectivo.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpEf.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT.Index, Fila).Value, Me.TablaImportar.Item(CuentaBancos.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
            Cadena = Split(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, Fila).Value.ToString().Trim(), "/")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value, Cadena(1).ToString().Replace("-", ""), "")
            Item += 1

        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value = 0 Then 'Cheque y Transferencia
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT.Index, Fila).Value, Me.TablaImportar.Item(CuentaBancos.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpC.Index, Fila).Value, Me.TablaImportar.Item(CtaCheque.Index, Fila).Value.ToString.Trim(), "")
            Item += 1
        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value > 0 Then 'Cheque y Transferencia

            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT.Index, Fila).Value, Me.TablaImportar.Item(CuentaBancos.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
            Cadena = Split(Me.TablaImportar.Item(CtaCheque.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpC.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Cadena = Split(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, Fila).Value.ToString().Trim(), "/")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value, Cadena(1).ToString().Replace("-", ""), "")
            Item += 1

        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value = 0 Then 'Cheque y Efecivo
            Cadena = Split(Me.TablaImportar.Item(CuentasEfectivo.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpEf.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpC.Index, Fila).Value, Me.TablaImportar.Item(CtaCheque.Index, Fila).Value.ToString.Trim(), "")
            Item += 1

        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value > 0 Then 'Cheque y Efecivo
            Cadena = Split(Me.TablaImportar.Item(CuentasEfectivo.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpEf.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpC.Index, Fila).Value, Me.TablaImportar.Item(CtaCheque.Index, Fila).Value.ToString.Trim(), "")
            Item += 1
            Cadena = Split(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, Fila).Value.ToString().Trim(), "/")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value, Cadena(1).ToString().Replace("-", ""), "")
            Item += 1
        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value = 0 Then ' Efecivo
            Cadena = Split(Me.TablaImportar.Item(CuentasEfectivo.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpEf.Index, Fila).Value, Cadena(1), "")
            Item += 1

        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value > 0 Then ' Efecivo
            Cadena = Split(Me.TablaImportar.Item(CuentasEfectivo.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpEf.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Cadena = Split(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, Fila).Value.ToString().Trim(), "/")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value, Cadena(1).ToString().Replace("-", ""), "")
            Item += 1

        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value = 0 Then ' Transferencia

            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT.Index, Fila).Value, Me.TablaImportar.Item(CuentaBancos.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value > 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value > 0 Then ' Transferencia
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT.Index, Fila).Value, Me.TablaImportar.Item(CuentaBancos.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
            Cadena = Split(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, Fila).Value.ToString().Trim(), "/")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value, Cadena(1).ToString().Replace("-", ""), "")
            Item += 1
        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value = 0 Then 'Cheque 
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpC.Index, Fila).Value, Me.TablaImportar.Item(CtaCheque.Index, Fila).Value.ToString.Trim(), "")
            Item += 1
        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value > 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value > 0 Then 'Cheque 
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpC.Index, Fila).Value, Me.TablaImportar.Item(CtaCheque.Index, Fila).Value.ToString.Trim(), "")
            Item += 1
            Cadena = Split(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, Fila).Value.ToString().Trim(), "/")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value, Cadena(1).ToString().Replace("-", ""), "")
            Item += 1
        ElseIf Me.TablaImportar.Item(ImpEf.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpT.Index, Fila).Value = 0 And Me.TablaImportar.Item(ImpC.Index, Fila).Value = 0 And Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value > 0 Then 'Cheque 
            Cadena = Split(Me.TablaImportar.Item(CtaAcreedoresTerceros.Index, Fila).Value.ToString().Trim(), "/")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(AcreedoresTerceros.Index, Fila).Value, Cadena(1).ToString().Replace("-", ""), "")
            Item += 1
        End If
        'si tienes mas tranf2
        If Me.TablaImportar.Item(ImpT2.Index, Fila).Value > 0 Then
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT2.Index, Fila).Value, Me.TablaImportar.Item(CtaT2.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        End If
        'si tienes mas tranf3
        If Me.TablaImportar.Item(ImpT3.Index, Fila).Value > 0 Then


            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT3.Index, Fila).Value, Me.TablaImportar.Item(CtaT3.Index, Fila).Value.ToString().Trim(), "")
            Item += 1

        End If
        'si tienes mas tranf4
        If Me.TablaImportar.Item(ImpT4.Index, Fila).Value > 0 Then
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT4.Index, Fila).Value, Me.TablaImportar.Item(CtaT4.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        End If
        If Me.TablaImportar.Item(ImpT5.Index, Fila).Value > 0 Then
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT5.Index, Fila).Value, Me.TablaImportar.Item(CtaT5.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        End If
        If Me.TablaImportar.Item(ImpT6.Index, Fila).Value > 0 Then
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT6.Index, Fila).Value, Me.TablaImportar.Item(CtaT6.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        End If
        If Me.TablaImportar.Item(ImpT7.Index, Fila).Value > 0 Then
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(ImpT7.Index, Fila).Value, Me.TablaImportar.Item(CtaT7.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        End If
        'Anticipos
        If Me.TablaImportar.Item(Anti.Index, Fila).Value > 0 Then ' Se verifica si cuenta con anticipos +
            If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) = 0.00 Then
                Cuenta = CuentasAnticiposPue(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(Anti.Index, Fila).Value, 0, Cuenta(0), "")
                Item += 1
            ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) >= 0.00 Then
                Cuenta = CuentasAnticiposPue(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(Anti.Index, Fila).Value, 0, Cuenta(0), "")
                Item += 1
            Else
                Cuenta = CuentasAnticiposPue(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(Anti.Index, Fila).Value, 0, Cuenta(2), "")
                Item += 1
            End If

        ElseIf Me.TablaImportar.Item(Anti.Index, Fila).Value < 0 Then ' Se verifica si cuenta  anticipos -
            If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) = 0.00 Then
                Cuenta = CuentasAnticiposPue(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(Anti.Index, Fila).Value * -1, 0, Cuenta(0), "")
                Item += 1
            ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) >= 0.00 Then
                Cuenta = CuentasAnticiposPue(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(Anti.Index, Fila).Value * -1, 0, Cuenta(0), "")
                Item += 1
            Else
                Cuenta = CuentasAnticiposPue(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(Anti.Index, Fila).Value * -1, 0, Cuenta(2), "")
                Item += 1
            End If
        End If
        'Ajustes
        If Me.TablaImportar.Item(Ajus.Index, Fila).Value > 0 Then ' Se verifica si cuenta con ajuste +
            Cadena = Split(Me.TablaImportar.Item(CtaAjustes.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(Ajus.Index, Fila).Value, Cadena(0), "")
            Item += 1
        ElseIf Me.TablaImportar.Item(Ajus.Index, Fila).Value < 0 Then ' Se verifica si cuenta con ajuste -
            Cadena = Split(Me.TablaImportar.Item(CtaAjustes.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(Ajus.Index, Fila).Value * -1, Cadena(0), "")
            Item += 1
        End If
        'Perdida cambiaria
        If Me.TablaImportar.Item(PCambiaria.Index, Fila).Value > 0 Then ' Se perdida cambiaria
            Item = Item + 1
            Crea_detalle_poliza(Poliza, Item, Me.TablaImportar.Item(PCambiaria.Index, Fila).Value, 0, "7010000100000000", "")
        End If
        'utilidad Cambiaria
        If Me.TablaImportar.Item(UCambaria.Index, Fila).Value > 0 Then ' Se utilidad cambiaria
            Item = Item + 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaImportar.Item(UCambaria.Index, Fila).Value, "7020000100000000", "")
        End If
        ' Retenciones
        If Me.TablaImportar.Item(RIVA.Index, Fila).Value > 0 Then
            If Me.TablaImportar.Item(ImpProvis.Index, Fila).Value = (Me.TablaImportar.Item(TReal.Index, Fila).Value - (Me.TablaImportar.Item(RIVA.Index, Fila).Value + Me.TablaImportar.Item(RISR.Index, Fila).Value)) Then
                CuentaRetenciones = CuentasRet(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Crea_detalle_poliza(Poliza, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, Fila).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, Fila).Value), CuentaRetenciones(6), "")
                Item += 1
            Else
                CuentaRetenciones = CuentasRet(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Edita_iva(Poliza, CuentaRetenciones(2), Me.TablaImportar.Item(RIVA.Index, Fila).Value * (1 - PorcenProv))
                If Math.Round(Me.TablaImportar.Item(RIVA.Index, Fila).Value) <> Math.Round(Me.TablaImportar.Item(IVAR.Index, Fila).Value) Then
                    Crea_detalle_poliza(Poliza, Item, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, Fila).Value * (1 - PorcenProv), Me.TablaImportar.Item(RIVA.Index, Fila).Value), 0, CuentaRetenciones(8), "")
                    Item += 1
                End If
                Crea_detalle_poliza(Poliza, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, Fila).Value * (1 - PorcenProv), Me.TablaImportar.Item(RIVA.Index, Fila).Value), CuentaRetenciones(2), "")
                Item += 1
            End If

        End If
        If Me.TablaImportar.Item(RISR.Index, Fila).Value > 0 Then

            If Me.TablaImportar.Item(ImpProvis.Index, Fila).Value = (Me.TablaImportar.Item(TReal.Index, Fila).Value - (Me.TablaImportar.Item(RIVA.Index, Fila).Value + Me.TablaImportar.Item(RISR.Index, Fila).Value)) Then
                CuentaRetenciones = CuentasRet(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Crea_detalle_poliza(Poliza, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RISR.Index, Fila).Value * PorcenProv, Me.TablaImportar.Item(RISR.Index, Fila).Value), CuentaRetenciones(4), "")
                Item += 1
            Else
                CuentaRetenciones = CuentasRet(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Crea_detalle_poliza(Poliza, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RISR.Index, Fila).Value * (1 - PorcenProv), Me.TablaImportar.Item(RISR.Index, Fila).Value), CuentaRetenciones(0), "")
                Item += 1
            End If

        End If
        'Provision
        If Me.TablaImportar.Item(ImpProvis.Index, Fila).Value > 0 Then

            Dim NivelE, NivelG As String


            CuentaPPD = CuentasPPD(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
            Try
                Select Case CuentaPPD(3)
                    Case 2
                        NivelE = CuentaPPD(2).ToString.Substring(0, 4)
                    Case 3
                        NivelE = CuentaPPD(2).ToString.Substring(4, 4)
                    Case 4
                        NivelE = CuentaPPD(2).ToString.Substring(8, 4)
                End Select
            Catch ex As Exception
                NivelE = ""
            End Try

            Try


                Select Case CuentaPPD(1)
                    Case 2
                        NivelG = CuentaPPD(0).ToString.Substring(0, 4)
                    Case 3
                        NivelG = CuentaPPD(0).ToString.Substring(4, 4)
                    Case 4
                        NivelG = CuentaPPD(0).ToString.Substring(8, 4)
                End Select
            Catch ex As Exception
                NivelG = ""
            End Try
            If ImpEprov > 0 And ImpGprov > 0 Then

                Crea_detalle_poliza(Poliza, Item, ImpGprov, 0, RegresaCuentaNuevoFactura(CuentaPPD(0), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelG, CuentaPPD(1)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, ImpEprov, 0, RegresaCuentaNuevoFactura(CuentaPPD(2), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelE, CuentaPPD(3)), "")
                Item += 1
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) = 0.00 Then
                    Crea_detalle_poliza(Poliza, Item, Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) * PorcenProv, 0, CuentaPPD(4), "")
                    Item += 1
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) >= 0.00 Then
                    Crea_detalle_poliza(Poliza, Item, Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) * PorcenProv, 0, CuentaPPD(4), "")
                    Item += 1
                End If
            ElseIf ImpEprov > 0 And ImpGprov <= 0 Then
                Crea_detalle_poliza(Poliza, Item, ImpEprov, 0, RegresaCuentaNuevoFactura(CuentaPPD(2), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelE, CuentaPPD(3)), "")
                Item += 1
            ElseIf ImpEprov <= 0 And ImpGprov > 0 Then
                Crea_detalle_poliza(Poliza, Item, ImpGprov, 0, RegresaCuentaNuevoFactura(CuentaPPD(0), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelG, CuentaPPD(1)), "")
                Item += 1
                If Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) > 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) = 0.00 Then
                    Crea_detalle_poliza(Poliza, Item, Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) * PorcenProv, 0, CuentaPPD(4), "")
                    Item += 1
                ElseIf Convert.ToDecimal(Me.TablaImportar.Item(iva8.Index, Fila).Value) = 0.00 And Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) >= 0.00 Then
                    Crea_detalle_poliza(Poliza, Item, Convert.ToDecimal(Me.TablaImportar.Item(IVAR.Index, Fila).Value) * PorcenProv, 0, CuentaPPD(4), "")
                    Item += 1
                End If
            End If
            Dim resta As Decimal = 0
            Try
                resta = Me.TablaImportar.Item(RIVA.Index, Fila).Value * PorcenProv + Me.TablaImportar.Item(RISR.Index, Fila).Value * PorcenProv

            Catch ex As Exception

            End Try
            Try
                Select Case CuentaPPD(10)
                    Case 2
                        NivelE = CuentaPPD(9).ToString.Substring(0, 4)
                    Case 3
                        NivelE = CuentaPPD(9).ToString.Substring(4, 4)
                    Case 4
                        NivelE = CuentaPPD(9).ToString.Substring(8, 4)
                End Select
            Catch ex As Exception
                NivelE = ""
            End Try

            Try


                Select Case CuentaPPD(7)
                    Case 2
                        NivelG = CuentaPPD(6).ToString.Substring(0, 4)
                    Case 3
                        NivelG = CuentaPPD(6).ToString.Substring(4, 4)
                    Case 4
                        NivelG = CuentaPPD(6).ToString.Substring(8, 4)
                End Select
            Catch ex As Exception
                NivelG = ""
            End Try
            If ImpEprov > 0 And ImpGprov > 0 Then
                Crea_detalle_poliza(Poliza, Item, 0, (ImpGprov + (Me.TablaImportar.Item(IVAR.Index, Fila).Value + Me.TablaImportar.Item(iva8.Index, Fila).Value) * PorcenProv) - resta, RegresaCuentaNuevoFactura(CuentaPPD(6), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelG, CuentaPPD(7)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, 0, ImpEprov, RegresaCuentaNuevoFactura(CuentaPPD(9), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelE, CuentaPPD(10)), "")
            ElseIf ImpEprov > 0 And ImpGprov <= 0 Then
                Crea_detalle_poliza(Poliza, Item, 0, ImpEprov, RegresaCuentaNuevoFactura(CuentaPPD(9), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelE, CuentaPPD(10)), "")
            ElseIf ImpEprov <= 0 And ImpGprov > 0 Then
                Crea_detalle_poliza(Poliza, Item, 0, (ImpGprov + (Me.TablaImportar.Item(IVAR.Index, Fila).Value + Me.TablaImportar.Item(iva8.Index, Fila).Value) * PorcenProv) - resta, RegresaCuentaNuevoFactura(CuentaPPD(6), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelG, CuentaPPD(7)), "")
            End If

            Item += 1
            If Me.TablaImportar.Item(RIVA.Index, Fila).Value > 0 Then
                CuentaRetenciones = CuentasRet(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Crea_detalle_poliza(Poliza, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RIVA.Index, Fila).Value * PorcenProv, Me.TablaImportar.Item(RIVA.Index, Fila).Value), CuentaRetenciones(6), "")
                Item += 1
            End If

            If Me.TablaImportar.Item(RISR.Index, Fila).Value > 0 Then
                CuentaRetenciones = CuentasRet(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Crea_detalle_poliza(Poliza, Item, 0, IIf(PorcenProv > 0, Me.TablaImportar.Item(RISR.Index, Fila).Value * PorcenProv, Me.TablaImportar.Item(RISR.Index, Fila).Value), CuentaRetenciones(4), "")
                Item += 1

            End If
        End If
        If UCase(Me.TablaImportar.Item(CtaOrden.Index, Fila).Value) = "SI" Then

            If Me.TablaImportar.Item(ImpProvis.Index, Fila).Value > 0 Then
                Dim NivelEc, NivelGc, NivelEa, NivelGa As String

                CuentaCO = CuentasCO(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Try
                    Select Case CuentaCO(5)
                        Case 2
                            NivelEc = CuentaPPD(2).ToString.Substring(0, 4)
                        Case 3
                            NivelEc = CuentaPPD(2).ToString.Substring(4, 4)
                        Case 4
                            NivelEc = CuentaPPD(2).ToString.Substring(8, 4)
                    End Select
                Catch ex As Exception
                    NivelEc = ""
                End Try
                Try
                    Select Case CuentaCO(7)
                        Case 2
                            NivelEa = CuentaPPD(2).ToString.Substring(0, 4)
                        Case 3
                            NivelEa = CuentaPPD(2).ToString.Substring(4, 4)
                        Case 4
                            NivelEa = CuentaPPD(2).ToString.Substring(8, 4)
                    End Select
                Catch ex As Exception
                    NivelEa = ""
                End Try

                Try


                    Select Case CuentaCO(3)
                        Case 2
                            NivelGa = CuentaPPD(0).ToString.Substring(0, 4)
                        Case 3
                            NivelGa = CuentaPPD(0).ToString.Substring(4, 4)
                        Case 4
                            NivelGa = CuentaPPD(0).ToString.Substring(8, 4)
                    End Select
                Catch ex As Exception
                    NivelGa = ""
                End Try
                Try


                    Select Case CuentaCO(1)
                        Case 2
                            NivelGc = CuentaPPD(0).ToString.Substring(0, 4)
                        Case 3
                            NivelGc = CuentaPPD(0).ToString.Substring(4, 4)
                        Case 4
                            NivelGc = CuentaPPD(0).ToString.Substring(8, 4)
                    End Select
                Catch ex As Exception
                    NivelGc = ""
                End Try

                If ImpEprov > 0 And ImpGprov > 0 Then
                    Crea_detalle_poliza(Poliza, Item, ImpGprov, 0, RegresaCuentaNuevoFactura(CuentaCO(0), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelGc, CuentaCO(1)), "")
                    Item += 1
                    Crea_detalle_poliza(Poliza, Item, ImpEprov, 0, RegresaCuentaNuevoFactura(CuentaCO(4), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelEc, CuentaCO(5)), "")
                    Item += 1
                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    Crea_detalle_poliza(Poliza, Item, ImpEprov, 0, RegresaCuentaNuevoFactura(CuentaCO(4), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelEc, CuentaCO(5)), "")
                    Item += 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    Crea_detalle_poliza(Poliza, Item, ImpGprov, 0, RegresaCuentaNuevoFactura(CuentaCO(0), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelGc, CuentaCO(1)), "")
                    Item += 1
                End If

                If ImpEprov > 0 And ImpGprov > 0 Then
                    Crea_detalle_poliza(Poliza, Item, 0, ImpGprov, RegresaCuentaNuevoFactura(CuentaCO(2), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelGa, CuentaCO(3)), "")
                    Item += 1
                    Crea_detalle_poliza(Poliza, Item, 0, ImpEprov, RegresaCuentaNuevoFactura(CuentaCO(6), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelEa, CuentaCO(7)), "")
                    Item += 1
                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    Crea_detalle_poliza(Poliza, Item, 0, ImpEprov, RegresaCuentaNuevoFactura(CuentaCO(6), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelEa, CuentaCO(7)), "")
                    Item += 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    Crea_detalle_poliza(Poliza, Item, 0, ImpGprov, RegresaCuentaNuevoFactura(CuentaCO(2), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelGa, CuentaCO(3)), "")
                    Item += 1
                End If

            Else
                Dim NivelEc, NivelGc, NivelEa, NivelGa As String

                CuentaCO = CuentasCO(Me.TablaImportar.Item(ContabilizacionC.Index, Fila).Value.ToString.Trim())
                Try
                    Select Case CuentaCO(5)
                        Case 2
                            NivelEc = CuentaPPD(2).ToString.Substring(0, 4)
                        Case 3
                            NivelEc = CuentaPPD(2).ToString.Substring(4, 4)
                        Case 4
                            NivelEc = CuentaPPD(2).ToString.Substring(8, 4)
                    End Select
                Catch ex As Exception
                    NivelEc = ""
                End Try
                Try
                    Select Case CuentaCO(7)
                        Case 2
                            NivelEa = CuentaPPD(2).ToString.Substring(0, 4)
                        Case 3
                            NivelEa = CuentaPPD(2).ToString.Substring(4, 4)
                        Case 4
                            NivelEa = CuentaPPD(2).ToString.Substring(8, 4)
                    End Select
                Catch ex As Exception
                    NivelEa = ""
                End Try

                Try


                    Select Case CuentaCO(3)
                        Case 2
                            NivelGa = CuentaPPD(0).ToString.Substring(0, 4)
                        Case 3
                            NivelGa = CuentaPPD(0).ToString.Substring(4, 4)
                        Case 4
                            NivelGa = CuentaPPD(0).ToString.Substring(8, 4)
                    End Select
                Catch ex As Exception
                    NivelGa = ""
                End Try
                Try


                    Select Case CuentaCO(1)
                        Case 2
                            NivelGc = CuentaPPD(0).ToString.Substring(0, 4)
                        Case 3
                            NivelGc = CuentaPPD(0).ToString.Substring(4, 4)
                        Case 4
                            NivelGc = CuentaPPD(0).ToString.Substring(8, 4)
                    End Select
                Catch ex As Exception
                    NivelGc = ""
                End Try

                If ImpEprov > 0 And ImpGprov > 0 Then
                    Crea_detalle_poliza(Poliza, Item, ImpGprov, 0, RegresaCuentaNuevoFactura(CuentaCO(2), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelGa, CuentaCO(3)), "")
                    Item += 1
                    Crea_detalle_poliza(Poliza, Item, ImpEprov, 0, RegresaCuentaNuevoFactura(CuentaCO(6), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelEa, CuentaCO(7)), "")
                    Item += 1
                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    Crea_detalle_poliza(Poliza, Item, ImpEprov, 0, RegresaCuentaNuevoFactura(CuentaCO(6), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelEa, CuentaCO(7)), "")
                    Item += 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    Crea_detalle_poliza(Poliza, Item, ImpGprov, 0, RegresaCuentaNuevoFactura(CuentaCO(2), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelGa, CuentaCO(3)), "")
                    Item += 1
                End If
                If ImpEprov > 0 And ImpGprov > 0 Then
                    Crea_detalle_poliza(Poliza, Item, 0, ImpGprov, RegresaCuentaNuevoFactura(CuentaCO(0), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelGc, CuentaCO(1)), "")
                    Item += 1
                    Crea_detalle_poliza(Poliza, Item, 0, ImpEprov, RegresaCuentaNuevoFactura(CuentaCO(4), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelEc, CuentaCO(5)), "")
                    Item += 1
                ElseIf ImpEprov > 0 And ImpGprov <= 0 Then 'Tiene Exento
                    Crea_detalle_poliza(Poliza, Item, 0, ImpEprov, RegresaCuentaNuevoFactura(CuentaCO(4), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelEc, CuentaCO(5)), "")
                    Item += 1
                ElseIf ImpEprov <= 0 And ImpGprov > 0 Then 'Tiene Grabado
                    Crea_detalle_poliza(Poliza, Item, 0, ImpGprov, RegresaCuentaNuevoFactura(CuentaCO(0), Trim(Me.TablaImportar.Item(RFCE.Index, Fila).Value), Fila, NivelGc, CuentaCO(1)), "")
                    Item += 1
                End If

            End If
        End If

    End Sub
    Private Sub Edita_iva(ByVal pol As String, ByVal cta As String, ByVal imp As Decimal)

        Dim sql As String = "UPDATE dbo.Detalle_Polizas SET Cargo = cargo-" & imp & "  WHERE ID_poliza = '" & Trim(pol) & "' AND Cuenta ='" & Trim(cta) & "' "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("recalculoiva", sql)
        End If
    End Sub
    Private Sub Actualiza_Registro(ByVal poliza As String, ByVal registro As Integer)
        Dim sql As String = " UPDATE dbo.xml_sat
                        SET ID_poliza = '" & poliza & "'
                        WHERE Id_Registro_Xml = " & registro & "  "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Carga", sql)
        End If

    End Sub
    Private Sub Actualiza_RegistroC(ByVal poliza As String, ByVal registro As Integer)
        Dim sql As String = " UPDATE dbo.Xml_Complemento
                        SET ID_poliza = '" & poliza & "'
                        WHERE Id_Xml_Complemento = " & registro & "  "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Carga", sql)
        End If

    End Sub

    Private Function Buscar_Cuenta_Abono(ByVal where As String)
        Dim cuenta As String = ""
        Dim sql As String = " select cuenta from catalogo_de_cuentas where " & where & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            cuenta = ds.Tables(0).Rows(0)(0)
        End If
        Return cuenta
    End Function
    Private Sub Color_Columnas()
        Dim contador As Integer = 0
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Ajustando color de las Columnas por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.TablaImportar.Columns.Count
        For Each Column As DataGridViewColumn In TablaImportar.Columns
            If Column.Index = ContabilizacionC.Index Or Column.Index = anio.Index Or Column.Index = Mes.Index Or Column.Index = BancoOrigen.Index Or Column.Index = Bancodestino.Index Or Column.Index = Fechat.Index Or Column.Index = BancosCheques.Index Or Column.Index = NoCheque.Index Or Column.Index = FechaC.Index Or Column.Index = ProvA.Index Or Column.Index = ProvP.Index Or Column.Index = UCambaria.Index Or Column.Index = PCambiaria.Index Or Column.Index = ImpD.Index Or Column.Index = CuntaDev.Index Or Column.Index = ImpD.Index Or Column.Index = ImpD.Index Then
                Column.DefaultCellStyle.BackColor = Color.RoyalBlue
            ElseIf Column.Index = Ajus.Index Then
                Column.DefaultCellStyle.BackColor = Color.GreenYellow
            ElseIf Column.Index = Anti.Index Then
                Column.DefaultCellStyle.BackColor = Color.LawnGreen
            ElseIf Column.Index = ImpProvis.Index Then
                Column.DefaultCellStyle.BackColor = Color.MediumPurple
            ElseIf Column.Index = TipoPoliza.Index Or Column.Index = NumPol.Index Then
                Column.DefaultCellStyle.BackColor = Color.Orange
            ElseIf Column.Index = NCuenta.Index Or Column.Index = CuentaBancos.Index Or Column.Index = CtaCheque.Index Then
                Column.DefaultCellStyle.BackColor = Color.Orange
            ElseIf Column.Index = ImpEf.Index Or Column.Index = ImpT.Index Or Column.Index = ImpC.Index Or Column.Index = ImpT2.Index Or Column.Index = ImpT3.Index Or Column.Index = ImpT4.Index Or Column.Index = ImpT5.Index Or Column.Index = ImpT6.Index Or Column.Index = ImpT7.Index Then
                Column.DefaultCellStyle.BackColor = Color.OliveDrab
            End If
            frm.Barra.Value += 1
        Next
        frm.Close()
    End Sub
    Private Function RegresaCuneta(ByVal cuenta As String, ByVal rfc As String, ByVal posicion As Integer, Optional ByVal tipo As Integer = 0)
        Dim Cta As String = ""
        Dim sql As String = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND Nivel3 = '" & cuenta.Substring(8, 4) & "' AND Nivel4 > 0 AND RFC = '" & rfc & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cta = ds.Tables(0).Rows(0)(0)
        Else
            'No existe la cuenta y la inserta
            If tipo = 1 Then
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3 >= 0 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), Cta.ToString.Substring(0, 4),
                               "0000", cuenta.Substring(0, 8) & Cta & "0000", Me.TablaImportar.Item(RFCE.Index, posicion).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, posicion).Value, Me.TablaImportar.Item(RFCE.Index, posicion).Value)
                Cta = cuenta.Substring(0, 8) & Cta & "0000"
            Else
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & cuenta.ToString.Substring(8, 4) & " AND Nivel4 >= 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), cuenta.ToString.Substring(8, 4),
                                  Cta, cuenta.Substring(0, 12) & Cta, Me.TablaImportar.Item(RFCE.Index, posicion).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, posicion).Value, Me.TablaImportar.Item(RFCE.Index, posicion).Value)
                Cta = cuenta.Substring(0, 12) & Cta
            End If
        End If
        Return Cta
    End Function


    Private Sub Actualiza_Registro_Xml(ByVal carg As String, ByVal registro As Integer)
        Dim sql As String = " UPDATE dbo.xml_sat
                        SET Nombre_cuenta = '" & carg & "'
                        WHERE Id_Registro_Xml = " & registro & "  "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Carga", sql)
        End If

    End Sub
    Private Sub Actualiza_Registro_Xml_Comple(ByVal carg As String, ByVal registro As Integer)
        Dim sql As String = " UPDATE dbo.Xml_Complemento
                        SET Nombre_cuenta = '" & carg & "'
                        WHERE Id_Xml_Complemento = " & registro & "  "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Carga", sql)
        End If

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
            RadMessageBox.Show("No hay registros de parametros para la Empresa " & Me.lstCliente.SelectText & " ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

            hacer = False
        End If
        Return hacer
    End Function
    Private Function BuscarcuentaRFC(ByVal RFC As String, ByVal cuenta As String, ByVal posicion As Integer)
        Dim Cta As String = ""
        Dim sql As String = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & cuenta.Substring(0, 4) & "' AND Nivel2= '" & cuenta.Substring(4, 4) & "' AND Nivel3 = '" & cuenta.Substring(8, 4) & "' AND Nivel4 > 0 AND RFC = '" & RFC & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cta = ds.Tables(0).Rows(0)(0)
        Else
            'No existe la cuenta y la inserta
            Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "max (Nivel4 ) + 1 ", "  Nivel1 =" & cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & cuenta.ToString.Substring(8, 4) & " AND Nivel4 > 0 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
            Cta = Format(Cta).PadLeft(4, "0")
            Crear_cuenta(cuenta.ToString.Substring(0, 4), cuenta.ToString.Substring(4, 4), cuenta.ToString.Substring(8, 4),
                                   Cta, cuenta.Substring(0, 12) & Cta, Me.TablaImportar.Item(RFCE.Index, posicion).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, posicion).Value,
                                   Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, posicion).Value, Me.TablaImportar.Item(RFCE.Index, posicion).Value)
            Cta = cuenta.Substring(0, 12) & Cta
        End If
        Return Cta
    End Function

    Private Function Leyenda(ByVal Letra As String, ByVal CHK As String)
        Dim Concepto As String = ""
        If CHK = "" Then
            Concepto = Eventos.ObtenerValorDB("ClaveEgresos", "Concepto", " Clave = '" & Letra & "' and Id_Empresa = " & Me.lstCliente.SelectItem & "", True)
        Else
            Concepto = CHK
        End If

        Return Concepto
    End Function
    '****************************************** Codigo de las Devoluciones y descuentos 
    Private Sub CmdSalir2_Click(sender As Object, e As EventArgs) Handles CmdSalir2.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiaDev_Click(sender As Object, e As EventArgs) Handles CmdLimpiaDev.Click
        If Me.TablaD.Rows.Count > 0 Then
            LimpiaDev()
            Me.LstTextoDev.SelectText = ""
        End If
    End Sub
    Private Sub LimpiaDev()
        Me.TablaD.Rows.Clear()
    End Sub

    Private Sub CmdBuscarDev_Click(sender As Object, e As EventArgs) Handles CmdBuscarDev.Click
        activo = True
        LimpiaDev()
        If Me.lstCliente.SelectText <> "" Then
            If Buscar_Parametros(Me.lstCliente.SelectItem) = True Then
                Buscar_Devoluciones(Me.lstCliente.SelectItem, " and Fecha_Emision >= " & Eventos.Sql_hoy(Me.DtIniDev.Value) & " and Fecha_Emision <= " & Eventos.Sql_hoy(DtFinDev.Value) & "")
                Cargar_valores_contablesD()
                Color_ColumnasD()
                For i As Integer = 0 To Me.TablaD.Rows.Count - 1
                    Liberar_ProcesoDev(i)
                    Cargar_Pol_Modelo_Notas(Me.TablaD.Item(RFCED.Index, i).Value, Me.TablaD.Item(FechaED.Index, i).Value, i)
                    Me.TablaD_CellEndEdit(Me.TablaD, Nothing)
                Next
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado un cliente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

        End If
        activo = False
    End Sub

    Private Sub Buscar_Devoluciones(ByVal Id_Empresa As Integer, ByVal periodo As String)
        Dim sql As String = " SELECT "
        sql &= " 	Id_Registro_Xml,	Verificado_Asoc,	Estado_SAT,	Version,	Tipo,	Fecha_Emision,"
        sql &= " 	Fecha_Timbrado,	EstadoPago,	FechaPago,	Serie,	Folio,	UUID,	UUID_Relacion,	RFC_Emisor,"
        sql &= " 	Nombre_Emisor,	LugarDeExpedicion,	RFC_Receptor,	Nombre_Receptor,	ResidenciaFiscal,	NumRegIdTrib,"
        sql &= " 	UsoCFDI,Retenido_IVA + Retenido_ISR  + Total -IVA_16 -IVA_8 AS SubTotal,	Descuento,	Total_IEPS,	IVA_16,	Retenido_IVA,	Retenido_ISR,	ISH,	Total,"
        sql &= " 	TotalOriginal,	Total_Trasladados,	Total_Retenidos,	Total_LocalTrasladado,	Total_LocalRetenido,	Complemento,"
        sql &= " 	Moneda,	Tipo_De_Cambio,	Metodos_de_Pago.Descripcion,	FormaDePago,	NumCtaPago,	Condicion_de_Pago,	Conceptos,"
        sql &= " 	Combustible,	IEPS_3,	IEPS_6,	IEPS_7,	IEPS_8,	IEPS_9,	IEPS_26,	IEPS_30,	IEPS_53,	IEPS_160,"
        sql &= " 	Archivo_XML,	Direccion_Emisor,	Localidad_Emisor,	Direccion_Receptor,	Localidad_Receptor,	Autorizada,	Consecutivo_Carga,"
        sql &= " 	Id_Empresa,	Carga_Procesada,	ID_poliza,Letra_Sat,	Nombre_cuenta,	Xml_Sat.Clave,	Anio_Contable,	Mes_Contable,	Imp_Efectivo,	Cuenta_Efectivo,
	Imp_Transf,	Banco_Origen,	Cuenta_Origen,	Banco_Destino,Cuenta_Destino,	Fecha_Transaccion,	Imp_Cheque,
	Nom_Banco_Ch,	Cuenta_Origen_Ch,	No_Cheque,	Fecha_Ch, Cuenta_Cheques,	Cuenta_Bancos,	Provision_Acreedor,	Provision_Proveedor,
	Diferencia,	Tipo_Poliza,	Imp_Grabado,	Imp_Exento,	IVA_real,	Prc_Pago_Acumulado,	Total_Real,	Utilidad_Cambiaria,
	Perdida_Cambiaria,	Imp_Devolucion,	Cuenta_Devolucion 	,Numpol,	RIRS,	RIVA,Ajuste , Anticipos, Cta_Ajuste, Cta_Anticipos,Imp_Provision,IVA_8 "
        sql &= " FROM dbo.Xml_Sat inner join Metodos_de_pago on Metodos_de_pago.clave = Xml_Sat.FormaDePago   where tipo = 'NotaCredito' and emitidas= " & Eventos.Bool2(False) & " and Id_Empresa =" & Id_Empresa & " and ID_poliza IS NULL AND  (Carga_Procesada =0	OR Carga_Procesada IS NULL) " & periodo & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaD.RowCount = 1
            Dim contab As DataSet = Eventos.Obtener_DS(" Select Rtrim(Clave) as Clave from ClaveEgresos where ClaveEgresos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Negativo=1 or (CargoG =0 or CargoE = 0) ")
            If contab.Tables(0).Rows.Count > 0 Then
                If Me.LetraCd.Items.Count = 0 Then

                    For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                        Me.LetraCd.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                    Next
                Else
                    Me.LetraCd.Items.Clear()
                    For i As Integer = 0 To contab.Tables(0).Rows.Count - 1
                        Me.LetraCd.Items.Add(Trim(contab.Tables(0).Rows(i)("Clave")))
                    Next
                End If
            End If
            Dim Tipo As DataSet = Eventos.Obtener_DS(" Select convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
            If Tipo.Tables(0).Rows.Count > 0 Then
                If Me.TipPolD.Items.Count = 0 Then
                    For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                        Me.TipPolD.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                    Next
                Else
                    Me.TipPolD.Items.Clear()
                    For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                        Me.TipPolD.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                    Next
                End If
            End If
            Me.TablaD.RowCount = ds.Tables(0).Rows.Count
            Me.BarraD.Maximum = Me.TablaD.RowCount - 1
            Me.BarraD.Minimum = 0
            Me.BarraD.Value1 = 0

            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Fila As DataGridViewRow = Me.TablaD.Rows(j)
                Me.TablaD.Item(0, j).Value = False
                Me.TablaD.Item(IdDev.Index, j).Value = ds.Tables(0).Rows(j)("Id_Registro_Xml")
                Me.TablaD.Item(Tp.Index, j).Value = ds.Tables(0).Rows(j)("Tipo")
                Me.TablaD.Item(FechaED.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Emision")
                Me.TablaD.Item(UUIDD.Index, j).Value = ds.Tables(0).Rows(j)("UUID")
                Me.TablaD.Item(UcfdiD.Index, j).Value = ds.Tables(0).Rows(j)("UsoCFDI")
                Me.TablaD.Item(RFCED.Index, j).Value = ds.Tables(0).Rows(j)("RFC_Emisor")
                Me.TablaD.Item(NomEd.Index, j).Value = ds.Tables(0).Rows(j)("Nombre_Emisor")
                Me.TablaD.Item(ConDev.Index, j).Value = ds.Tables(0).Rows(j)("Conceptos")
                Me.TablaD.Item(MetoD.Index, j).Value = ds.Tables(0).Rows(j)("FormaDePago")
                Me.TablaD.Item(ForPD.Index, j).Value = Trim(ds.Tables(0).Rows(j)("Descripcion"))
                Me.TablaD.Item(SubD.Index, j).Value = ds.Tables(0).Rows(j)("SubTotal")
                Me.TablaD.Item(IvaD.Index, j).Value = ds.Tables(0).Rows(j)("IVA_16")
                Me.TablaD.Item(IVA8D.Index, j).Value = ds.Tables(0).Rows(j)("IVA_8")
                Me.TablaD.Item(TotD.Index, j).Value = ds.Tables(0).Rows(j)("Total")
                Me.TablaD.Item(UIDr.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("UUID_Relacion")) = True, "", ds.Tables(0).Rows(j)("UUID_Relacion"))
                Me.TablaD.Item(LetraSD.Index, j).Value = ds.Tables(0).Rows(j)("Letra_Sat")

                Me.TablaD.Item(NomCuentaD.Index, j).Value = ds.Tables(0).Rows(j)("Nombre_cuenta")
                Try
                    If Trim(ds.Tables(0).Rows(j)("Clave")) <> "" Then
                        Fila.Cells(LetraCd.Index).Value = Me.LetraCd.Items(Obtener_indexDV(Trim(ds.Tables(0).Rows(j)("Clave"))))
                    End If
                    Fila.Cells(Refe.Index).Value = ImpFaCURA(Me.TablaD.Item(UIDr.Index, j).Value.ToString().Trim(), "Clave").ToString().Trim()

                Catch ex As Exception

                End Try
                Dim year As String = ds.Tables(0).Rows(j)("Fecha_Emision").ToString.Substring(6, 4)
                Dim month As String = ds.Tables(0).Rows(j)("Fecha_Emision").ToString.Substring(3, 2)
                Me.TablaD.Item(AnioCD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Anio_Contable")) = True, year, ds.Tables(0).Rows(j)("Anio_Contable"))
                Me.TablaD.Item(MesCD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Mes_Contable")) = True, month, ds.Tables(0).Rows(j)("Mes_Contable"))
                Me.TablaD.Item(ImpED.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Efectivo")) = True, 0, ds.Tables(0).Rows(j)("Imp_Efectivo"))
                Me.TablaD.Item(CtaEfD.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Efectivo")
                Me.TablaD.Item(ImpTD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Transf")) = True, 0, ds.Tables(0).Rows(j)("Imp_Transf"))


                Try
                    If Trim(ds.Tables(0).Rows(j)("Banco_Origen")) <> "" Then
                        Fila.Cells(BancoOd.Index).Value = Me.BancoOd.Items(Obtener_indexBDv(Trim(ds.Tables(0).Rows(j)("Banco_Origen"))))
                    End If
                Catch ex As Exception

                End Try

                Me.TablaD.Item(CtaOD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Origen")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Origen"))


                Try
                    If Trim(ds.Tables(0).Rows(j)("Banco_Destino")) <> "" Then
                        Fila.Cells(BancoDD.Index).Value = Me.BancoDD.Items(Obtener_indexdDv(Trim(ds.Tables(0).Rows(j)("Banco_Destino"))))
                    End If
                Catch ex As Exception

                End Try
                'Me.TablaImportar.Item(Bancodestino.Index, j).Value = ds.Tables(0).Rows(j)("Banco_Destino")

                Me.TablaD.Item(CtaDD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Destino")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Destino"))
                Me.TablaD.Item(FecTD.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Transaccion")
                Me.TablaD.Item(ImpCHD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Cheque")) = True, 0, ds.Tables(0).Rows(j)("Imp_Cheque"))

                Try
                    If Trim(ds.Tables(0).Rows(j)("Nom_Banco_Ch")) <> "" Then
                        Fila.Cells(NomBankCHD.Index).Value = Me.NomBankCHD.Items(Obtener_indexBDvBH(Trim(ds.Tables(0).Rows(j)("Nom_Banco_Ch"))))
                    End If
                Catch ex As Exception

                End Try
                'Me.TablaD.Item(NomBankCHD.Index, j).Value = ds.Tables(0).Rows(j)("Nom_Banco_Ch")
                Me.TablaD.Item(CtaOrigCHD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Origen_Ch")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Origen_Ch"))
                Me.TablaD.Item(NchD.Index, j).Value = ds.Tables(0).Rows(j)("No_Cheque")
                Me.TablaD.Item(FcHd.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Ch")
                Me.TablaD.Item(CtaBancosD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Bancos")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Bancos"))
                Me.TablaD.Item(CtaChequeD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Cheques")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Cheques"))
                Me.TablaD.Item(ProvAD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Provision_Acreedor")) = True, 0, ds.Tables(0).Rows(j)("Provision_Acreedor"))
                Me.TablaD.Item(ProvPD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Provision_Proveedor")) = True, 0, ds.Tables(0).Rows(j)("Provision_Proveedor"))
                Me.TablaD.Item(DifD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Diferencia")) = True, 0, ds.Tables(0).Rows(j)("Diferencia"))

                Try
                    If Trim(ds.Tables(0).Rows(j)("Tipo_Poliza")) <> "" Then
                        Fila.Cells(TipPolD.Index).Value = Me.TipPolD.Items(Obtener_index2Dv(Trim(ds.Tables(0).Rows(j)("Tipo_Poliza"))))
                    End If

                Catch ex As Exception

                End Try
                '   Me.TablaImportar.Item(tipopoliza.INDEX, j).Value = ds.Tables(0).Rows(j)("Tipo_Poliza")
                Me.TablaD.Item(ImpGD.Index, j).Value = ds.Tables(0).Rows(j)("Imp_Grabado")
                Me.TablaD.Item(ImpEDV.Index, j).Value = ds.Tables(0).Rows(j)("Imp_Exento")
                Me.TablaD.Item(IVARD.Index, j).Value = ds.Tables(0).Rows(j)("IVA_real")
                Me.TablaD.Item(PACD.Index, j).Value = ds.Tables(0).Rows(j)("Prc_Pago_Acumulado")
                Me.TablaD.Item(TRD.Index, j).Value = ds.Tables(0).Rows(j)("Total_Real")
                Me.TablaD.Item(MD.Index, j).Value = ds.Tables(0).Rows(j)("Moneda")
                Me.TablaD.Item(UCD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Utilidad_Cambiaria")) = True, 0, ds.Tables(0).Rows(j)("Utilidad_Cambiaria"))
                Me.TablaD.Item(PCD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Perdida_Cambiaria")) = True, 0, ds.Tables(0).Rows(j)("Perdida_Cambiaria"))
                Me.TablaD.Item(ImpDevD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Devolucion")) = True, 0, ds.Tables(0).Rows(j)("Imp_Devolucion"))
                Me.TablaD.Item(CtaDevD.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Devolucion")

                Me.TablaD.Item(NPolD.Index, j).Value = ds.Tables(0).Rows(j)("numpol")
                Me.TablaD.Item(RisrD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("RIRS")) = True, 0, ds.Tables(0).Rows(j)("RIRS"))
                Me.TablaD.Item(RIVAD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("RIVA")) = True, 0, ds.Tables(0).Rows(j)("RIVA"))


                'Ajutes y Anticipos
                Me.TablaD.Item(AntiD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Anticipos")) = True, 0, ds.Tables(0).Rows(j)("Anticipos"))
                Me.TablaD.Item(CtaAntiD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cta_Ajuste")) = True, "", ds.Tables(0).Rows(j)("Cta_Ajuste"))
                Me.TablaD.Item(AjusD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ajuste")) = True, 0, ds.Tables(0).Rows(j)("Ajuste"))
                Me.TablaD.Item(CtaAD.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cta_Anticipos")) = True, "", ds.Tables(0).Rows(j)("Cta_Anticipos"))
                Me.TablaD.Item(ImpPd.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Provision")) = True, 0, ds.Tables(0).Rows(j)("Imp_Provision"))

                If Me.BarraD.Value1 = Me.BarraD.Maximum Then
                    Me.BarraD.Minimum = 0
                    Me.Cursor = Cursors.Arrow
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    RadMessageBox.Show("Movimientos Cargados ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

                    Me.BarraD.Value1 = 0
                Else
                    Me.BarraD.Value1 += 1
                End If
            Next
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay registros para procesar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

        End If



    End Sub
    Private Sub Cargar_valores_contablesD()
        If Me.TablaD.Rows.Count >= 1 Then
            For i As Integer = 0 To Me.TablaD.Rows.Count - 1
                If Trim(Me.TablaD.Item(TotD.Index, i).Value) <> "P" Then
                    If Trim(Me.TablaD.Item(MD.Index, i).Value.ToString) <> "USD" Then
                        If Convert.ToDecimal(Me.TablaD.Item(IVA8D.Index, i).Value) > 0.00 And Convert.ToDecimal(Me.TablaD.Item(IvaD.Index, i).Value) = 0.00 Then
                            If Me.TablaD.Item(TotD.Index, i).Value > 0 And Me.TablaD.Item(TotD.Index, i).Value <> Nothing Then
                                'importe gravado
                                Me.TablaD.Item(ImpGD.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(IVA8D.Index, i).Value) = True, 0, Me.TablaD.Item(IVA8D.Index, i).Value / 0.08), 2)
                                'importe exento
                                Me.TablaD.Item(ImpEDV.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(SubD.Index, i).Value) = True, 0, Me.TablaD.Item(SubD.Index, i).Value - Me.TablaD.Item(ImpGD.Index, i).Value), 2)
                                'Iva_real
                                Me.TablaD.Item(IVARD.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(ImpGD.Index, i).Value) = True, 0, Me.TablaD.Item(ImpGD.Index, i).Value * 0.08), 2)
                                'calcula el % Pago Acumulado
                                If Me.TablaD.Item(ImpEDV.Index, i).Value < 1 Then
                                    Me.TablaD.Item(ImpGD.Index, i).Value = Me.TablaD.Item(ImpGD.Index, i).Value + Me.TablaD.Item(ImpEDV.Index, i).Value
                                    Me.TablaD.Item(ImpEDV.Index, i).Value = 0
                                End If
                                'Total real
                                Me.TablaD.Item(TRD.Index, i).Value = Me.TablaD.Item(ImpGD.Index, i).Value + Me.TablaD.Item(ImpEDV.Index, i).Value + Me.TablaD.Item(IVARD.Index, i).Value
                            End If
                        ElseIf Convert.ToDecimal(Me.TablaD.Item(IVA8D.Index, i).Value) = 0.00 And Convert.ToDecimal(Me.TablaD.Item(IvaD.Index, i).Value) >= 0.00 Then
                            If Me.TablaD.Item(TotD.Index, i).Value > 0 And Me.TablaD.Item(TotD.Index, i).Value <> Nothing Then
                                'importe gravado
                                Me.TablaD.Item(ImpGD.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(IvaD.Index, i).Value) = True, 0, Me.TablaD.Item(IvaD.Index, i).Value / 0.16), 2)
                                'importe exento
                                Me.TablaD.Item(ImpEDV.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(SubD.Index, i).Value) = True, 0, Me.TablaD.Item(SubD.Index, i).Value - Me.TablaD.Item(ImpGD.Index, i).Value), 2)
                                'Iva_real
                                Me.TablaD.Item(IVARD.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(ImpGD.Index, i).Value) = True, 0, Me.TablaD.Item(ImpGD.Index, i).Value * 0.16), 2)
                                'calcula el % Pago Acumulado
                                If Me.TablaD.Item(ImpEDV.Index, i).Value < 1 Then
                                    Me.TablaD.Item(ImpGD.Index, i).Value = Me.TablaD.Item(ImpGD.Index, i).Value + Me.TablaD.Item(ImpEDV.Index, i).Value
                                    Me.TablaD.Item(ImpEDV.Index, i).Value = 0
                                End If
                                'Total real
                                Me.TablaD.Item(TRD.Index, i).Value = Me.TablaD.Item(ImpGD.Index, i).Value + Me.TablaD.Item(ImpEDV.Index, i).Value + Me.TablaD.Item(IVARD.Index, i).Value
                            End If
                        End If

                    Else
                        If Convert.ToDecimal(Me.TablaD.Item(IVA8D.Index, i).Value) > 0.00 And Convert.ToDecimal(Me.TablaD.Item(IvaD.Index, i).Value) = 0.00 Then
                            ' calcular en dolares
                            If Me.TablaD.Item(TotD.Index, i).Value > 0 And Me.TablaD.Item(TotD.Index, i).Value <> Nothing Then
                                'importe gravado
                                Me.TablaD.Item(ImpGD.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(iva8.Index, i).Value) = True, 0, (Me.TablaD.Item(iva8.Index, i).Value * Calcula_Moneda(Me.TablaD.Item(FechaED.Index, i).Value.ToString.Substring(0, 10))) / 0.08), 2)
                                'importe exento
                                Me.TablaD.Item(ImpEDV.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(SubD.Index, i).Value) = True, 0, (Me.TablaD.Item(SubD.Index, i).Value * Calcula_Moneda(Me.TablaD.Item(FechaED.Index, i).Value.ToString.Substring(0, 10))) - Me.TablaD.Item(ImpGD.Index, i).Value), 2)
                                'Iva_real
                                Me.TablaD.Item(IVARD.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(ImpGD.Index, i).Value) = True, 0, Me.TablaD.Item(ImpGD.Index, i).Value * 0.08), 2)
                                'calcula el % Pago Acumulado
                                'Total real
                                If Me.TablaD.Item(ImpEDV.Index, i).Value < 1 Then
                                    Me.TablaD.Item(ImpGD.Index, i).Value = Me.TablaD.Item(ImpGD.Index, i).Value + Me.TablaD.Item(ImpEDV.Index, i).Value
                                    Me.TablaD.Item(ImpEDV.Index, i).Value = 0
                                End If
                                Me.TablaD.Item(TRD.Index, i).Value = Me.TablaD.Item(ImpGD.Index, i).Value + Me.TablaD.Item(ImpEDV.Index, i).Value + Me.TablaD.Item(IVARD.Index, i).Value
                            End If
                        ElseIf Convert.ToDecimal(Me.TablaD.Item(IVA8D.Index, i).Value) = 0.00 And Convert.ToDecimal(Me.TablaD.Item(IvaD.Index, i).Value) >= 0.00 Then
                            ' calcular en dolares
                            If Me.TablaD.Item(TotD.Index, i).Value > 0 And Me.TablaD.Item(TotD.Index, i).Value <> Nothing Then
                                'importe gravado
                                Me.TablaD.Item(ImpGD.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(IvaD.Index, i).Value) = True, 0, (Me.TablaD.Item(IvaD.Index, i).Value * Calcula_Moneda(Me.TablaD.Item(FechaED.Index, i).Value.ToString.Substring(0, 10))) / 0.16), 2)
                                'importe exento
                                Me.TablaD.Item(ImpEDV.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(SubD.Index, i).Value) = True, 0, (Me.TablaD.Item(SubD.Index, i).Value * Calcula_Moneda(Me.TablaD.Item(FechaED.Index, i).Value.ToString.Substring(0, 10))) - Me.TablaD.Item(ImpGD.Index, i).Value), 2)
                                'Iva_real
                                Me.TablaD.Item(IVARD.Index, i).Value = Math.Round(IIf(IsDBNull(Me.TablaD.Item(ImpGD.Index, i).Value) = True, 0, Me.TablaD.Item(ImpGD.Index, i).Value * 0.16), 2)
                                'calcula el % Pago Acumulado
                                'Total real
                                If Me.TablaD.Item(ImpEDV.Index, i).Value < 1 Then
                                    Me.TablaD.Item(ImpGD.Index, i).Value = Me.TablaD.Item(ImpGD.Index, i).Value + Me.TablaD.Item(ImpEDV.Index, i).Value
                                    Me.TablaD.Item(ImpEDV.Index, i).Value = 0
                                End If
                                Me.TablaD.Item(TRD.Index, i).Value = Me.TablaD.Item(ImpGD.Index, i).Value + Me.TablaD.Item(ImpEDV.Index, i).Value + Me.TablaD.Item(IVARD.Index, i).Value
                            End If
                        End If


                    End If
                End If
            Next
        End If
    End Sub
    Private Sub Color_ColumnasD()
        Dim contador As Integer = 0
        For Each Column As DataGridViewColumn In TablaD.Columns
            If Column.Index = AnioCD.Index Or Column.Index = MesCD.Index Or Column.Index = BancoOd.Index Or Column.Index = BancoDD.Index Or Column.Index = FecTD.Index Or Column.Index = NomBankCHD.Index Or Column.Index = NchD.Index Or Column.Index = FcHd.Index Or Column.Index = ProvAD.Index Or Column.Index = ProvPD.Index Or Column.Index = UCD.Index Or Column.Index = PCD.Index Or Column.Index = ImpDevD.Index Or Column.Index = CtaDevD.Index Then
                Column.DefaultCellStyle.BackColor = Color.RoyalBlue
            ElseIf Column.Index = AjusD.Index Then
                Column.DefaultCellStyle.BackColor = Color.GreenYellow
            ElseIf Column.Index = AntiD.Index Then
                Column.DefaultCellStyle.BackColor = Color.LawnGreen
            ElseIf Column.Index = ImpPd.Index Then
                Column.DefaultCellStyle.BackColor = Color.MediumPurple
            ElseIf Column.Index = TipPolD.Index Or Column.Index = NPolD.Index Then
                Column.DefaultCellStyle.BackColor = Color.Orange
            ElseIf Column.Index = NomCuentaD.Index Or Column.Index = CtaBancosD.Index Or Column.Index = CtaChequed.Index Then
                Column.DefaultCellStyle.BackColor = Color.Orange
            ElseIf Column.Index = ImpED.Index Or Column.Index = ImpTD.Index Or Column.Index = ImpCHD.Index Then
                Column.DefaultCellStyle.BackColor = Color.OliveDrab
            End If
        Next
    End Sub

    Private Sub CmdGuardarDev_Click(sender As Object, e As EventArgs) Handles CmdGuardarDev.Click
        Guardar_Dev()
    End Sub
    Private Sub Guardar_Dev()
        If Me.TablaD.Rows.Count >= 1 Then
            For i As Integer = 0 To Me.TablaD.Rows.Count - 1
                Dim tabla As String = ""
                If Me.TablaD.Item(2, i).Value = "P" Then
                    tabla = "Xml_Complemento"
                Else
                    tabla = "Xml_Sat"
                End If
                Guardar_XML(IIf(IsDBNull(Me.TablaD.Item(LetraSD.Index, i).Value), "", Me.TablaD.Item(LetraSD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(NomCuentaD.Index, i).Value), "", Me.TablaD.Item(NomCuentaD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(LetraCd.Index, i).Value), "", Me.TablaD.Item(LetraCd.Index, i).Value),
                           Me.TablaD.Item(AnioCD.Index, i).Value, Me.TablaD.Item(MesCD.Index, i).Value, Me.TablaD.Item(ImpED.Index, i).Value, IIf(IsDBNull(Me.TablaD.Item(CtaEfD.Index, i).Value), "", Me.TablaD.Item(CtaEfD.Index, i).Value),
                           Me.TablaD.Item(ImpTD.Index, i).Value, IIf(IsDBNull(Me.TablaD.Item(BancoOd.Index, i).Value), "", Me.TablaD.Item(BancoOd.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(CtaOD.Index, i).Value), "", Me.TablaD.Item(CtaOD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(BancoDD.Index, i).Value), "", Me.TablaD.Item(BancoDD.Index, i).Value),
                           IIf(IsDBNull(Me.TablaD.Item(FecTD.Index, i).Value), "", Me.TablaD.Item(FecTD.Index, i).Value), Me.TablaD.Item(ImpCHD.Index, i).Value, IIf(IsDBNull(Me.TablaD.Item(NomBankCHD.Index, i).Value), "", Me.TablaD.Item(NomBankCHD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(CtaOrigCHD.Index, i).Value), "", Me.TablaD.Item(CtaOrigCHD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(NchD.Index, i).Value), "", Me.TablaD.Item(NchD.Index, i).Value),
                          IIf(IsDBNull(Me.TablaD.Item(FcHd.Index, i).Value), "", Me.TablaD.Item(FcHd.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(CtaBancosD.Index, i).Value), "", Me.TablaD.Item(CtaBancosD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(ProvAD.Index, i).Value), 0, Me.TablaD.Item(ProvAD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(ProvPD.Index, i).Value), 0, Me.TablaD.Item(ProvPD.Index, i).Value),
                           IIf(IsDBNull(Me.TablaD.Item(DifD.Index, i).Value), 0, Me.TablaD.Item(DifD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(TipPolD.Index, i).Value), "", Me.TablaD.Item(TipPolD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(ImpGD.Index, i).Value), 0, Me.TablaD.Item(ImpGD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(ImpEDV.Index, i).Value), 0, Me.TablaD.Item(ImpEDV.Index, i).Value),
                         IIf(IsDBNull(Me.TablaD.Item(IVARD.Index, i).Value), 0, Me.TablaD.Item(IVARD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(PACD.Index, i).Value), 0, Me.TablaD.Item(PACD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(TRD.Index, i).Value), 0, Me.TablaD.Item(TRD.Index, i).Value), Me.TablaD.Item(UCD.Index, i).Value, IIf(IsDBNull(Me.TablaD.Item(PCD.Index, i).Value), 0, Me.TablaD.Item(PCD.Index, i).Value),
                           Me.TablaD.Item(ImpDevD.Index, i).Value, IIf(IsDBNull(Me.TablaD.Item(CtaDevD.Index, i).Value), "", Me.TablaD.Item(CtaDevD.Index, i).Value), Me.TablaD.Item(IdDev.Index, i).Value, IIf(IsDBNull(Me.TablaD.Item(NPolD.Index, i).Value), "", Me.TablaD.Item(NPolD.Index, i).Value), Me.TablaD.Item(RisrD.Index, i).Value, Me.TablaD.Item(RIVAD.Index, i).Value, tabla, IIf(IsDBNull(Me.TablaD.Item(CtaDD.Index, i).Value), "", Me.TablaD.Item(CtaDD.Index, i).Value),
                           IIf(IsDBNull(Me.TablaD.Item(AjusD.Index, i).Value), 0, Me.TablaD.Item(AjusD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(CtaAD.Index, i).Value), "", Me.TablaD.Item(CtaAD.Index, i).Value),
                           IIf(IsDBNull(Me.TablaD.Item(AntiD.Index, i).Value), 0, Me.TablaD.Item(AntiD.Index, i).Value), IIf(IsDBNull(Me.TablaD.Item(CtaAntiD.Index, i).Value), "", Me.TablaD.Item(CtaAntiD.Index, i).Value),
                           IIf(IsDBNull(Me.TablaD.Item(ImpPd.Index, i).Value), 0, Me.TablaD.Item(ImpPd.Index, i).Value), "", 0, "", "", "", "", "", "", 0, "", "", "", "", "", "", 0, "", "", "", "", "", "", 0, "", "", "", "", "", "", 0, "", "", "", "", "", "", 0, "", "", "", "", "", "", IIf(IsDBNull(Me.TablaD.Item(CtaChequeD.Index, i).Value), "", Me.TablaD.Item(CtaChequeD.Index, i).Value), "", 0)
            Next
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Guardado Completado ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

        End If
    End Sub

    Private Sub CmdProcesoDev_Click(sender As Object, e As EventArgs) Handles CmdProcesoDev.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaD.Rows.Count > 0 Then
            If Me.lstCliente.SelectText <> "" Then
                If Verifica_catalogo_cliente(Me.lstCliente.SelectItem) = True Then
                    Guardar_Dev()
                    If RadMessageBox.Show("El cliente " & Me.lstCliente.SelectText & " es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Me.BarraD.Maximum = Me.TablaD.RowCount - 1
                        Me.BarraD.Minimum = 0
                        Me.BarraD.Value1 = 0

                        For p As Integer = 0 To Me.TablaD.RowCount - 1
                            If Me.TablaD.Item(AplD.Index, p).Value = True Then ' se paso todos los filtros de creacion
                                Codificar_polizasDev(p)
                            End If
                            If Me.BarraD.Value1 = Me.BarraD.Maximum Then
                                Me.BarraD.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                Me.BarraD.Value1 = 0
                            Else
                                Me.BarraD.Value1 += 1
                            End If
                        Next
                    End If
                Else
                    RadMessageBox.Show("No existe Catalogo de cuentas para: " & Me.lstCliente.SelectText & "", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                End If
                Me.CmdBuscarDev.PerformClick()
            Else
                RadMessageBox.Show("No se ha seleccionado una empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
        Else
            RadMessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
    Private Sub Codificar_polizasDev(ByVal posicion As Integer)
        Dim poliza_Sistema As String = ""
        '  If Me.TablaImportar.Item(Psistema.Index, posicion).Value <> "" Then ' quitar linea despues del desbloqueo
        poliza_Sistema = Calcula_polizaD(posicion)
        '  End If

        'Calcular consecutivo poliza
        Dim posi As Integer = InStr(1, poliza_Sistema, "-", CompareMethod.Binary)
        Dim cuantos As Integer = Len(poliza_Sistema) - Len(poliza_Sistema.Substring(0, posi))
        Dim consecutivo As Integer = Val(poliza_Sistema.Substring(posi, cuantos))
        'Crear poliza
        'Checar dia de la Poliza poner if
        Dim dia As String = ""
        Dim Chk As String = ""
        If Me.TablaD.Item(ImpTD.Index, posicion).Value > 0 Then
            dia = Me.TablaD.Item(FecTD.Index, posicion).Value.ToString.Substring(0, 2)
        ElseIf Me.TablaD.Item(ImpED.Index, posicion).Value > 0 Then
            dia = Me.TablaD.Item(FechaED.Index, posicion).Value.ToString.Substring(0, 2)
        ElseIf Me.TablaD.Item(ImpCHD.Index, posicion).Value > 0 Then
            dia = Me.TablaD.Item(FcHd.Index, posicion).Value.ToString.Substring(0, 2)
            Chk = "Ch/ " & Me.TablaD.Item(NchD.Index, posicion).Value.ToString & " F/ "
        Else
            dia = Me.TablaD.Item(FechaED.Index, posicion).Value.ToString.Substring(0, 2)
        End If
        Dim leyenda As String
        If Chk = "" Then
            leyenda = "Nta Cred Fact" & " " & Trim(Me.TablaD.Item(UUIDD.Index, posicion).Value)
        Else
            leyenda = Chk & " " & Trim(Me.TablaD.Item(UUIDD.Index, posicion).Value)
        End If
        ' CREAR iF para calcular el dia
        If Creapoliza(poliza_Sistema, Me.TablaD.Item(AnioCD.Index, posicion).Value, Me.TablaD.Item(MesCD.Index, posicion).Value, dia,
                   consecutivo, Checa_tipo(Me.TablaD.Item(TipPolD.Index, posicion).Value, Me.lstCliente.SelectItem),
                   Me.TablaD.Item(FechaED.Index, posicion).Value, leyenda, "Carga", Me.TablaD.Item(NPolD.Index, posicion).Value, Me.TablaD.Item(IdDev.Index, posicion).Value, False) = True Then



            If Buscafactura(Me.TablaD.Item(UUIDD.Index, posicion).Value, "C") = True Then
                'Se inserta la Factura
                Inserta_Comprobante_Fiscal(poliza_Sistema, Me.TablaD.Item(AnioCD.Index, posicion).Value, Me.TablaD.Item(MesCD.Index, posicion).Value,
                             Me.TablaD.Item(RFCED.Index, posicion).Value, Me.TablaD.Item(FechaED.Index, posicion).Value,
                               Me.TablaD.Item(UUIDD.Index, posicion).Value, "Factura " & Trim(Me.TablaD.Item(RFCED.Index, posicion).Value) & " C", Me.TablaD.Item(TRD.Index, posicion).Value)
            Else
                'Se Edita la Factura
                Edita_Factura(Me.TablaD.Item(UUIDD.Index, posicion).Value, "C", poliza_Sistema)
            End If


            If Me.TablaD.Item(ImpED.Index, posicion).Value > 0 Then
                ' Insertar registro contabiidad electronica efectivo
                Inserta_Comprobante_Fiscal_Efectivo(poliza_Sistema, Me.TablaD.Item(AnioCD.Index, posicion).Value, Me.TablaD.Item(MesCD.Index, posicion).Value,
                                Me.TablaD.Item(RFCED.Index, posicion).Value, Me.TablaD.Item(TipPolD.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaD.Item(FechaED.Index, posicion).Value,
                              "", "", "", "", Me.TablaD.Item(ImpED.Index, posicion).Value)
            End If
            If Trim(UCase(Me.lblce.Text)) = "TRUE" Then ' SE INSERTA CONTABILIDAD ELECTRONICA

                If Me.TablaD.Item(ImpTD.Index, posicion).Value > 0 Then


                    Inserta_Comprobante_Fiscal_Transf(poliza_Sistema, Me.TablaD.Item(AnioCD.Index, posicion).Value, Me.TablaD.Item(MesCD.Index, posicion).Value,
                                    Me.TablaD.Item(RFCED.Index, posicion).Value, Me.TablaD.Item(TipPolD.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaD.Item(FecTD.Index, posicion).Value,
                                  "", "", "", Me.TablaD.Item(UUIDD.Index, posicion).Value, Me.TablaD.Item(ImpTD.Index, posicion).Value, "", Me.TablaD.Item(CtaBancosD.Index, posicion).Value)
                End If

                If Me.TablaD.Item(ImpCHD.Index, posicion).Value > 0 Then


                    Inserta_Comprobante_Fiscal_Cheque(poliza_Sistema, Me.TablaD.Item(AnioCD.Index, posicion).Value, Me.TablaD.Item(MesCD.Index, posicion).Value,
                                    Me.TablaD.Item(RFCED.Index, posicion).Value, Me.TablaD.Item(TipPolD.Index, posicion).Value.ToString.Substring(0, 3), Me.TablaD.Item(FcHd.Index, posicion).Value,
                                  Me.TablaD.Item(NchD.Index, posicion).Value, "", Me.TablaD.Item(CtaOrigCHD.Index, posicion).Value, Me.TablaD.Item(UUIDD.Index, posicion).Value, Me.TablaD.Item(ImpCHD.Index, posicion).Value)
                End If
            End If

            'Verificara el sistema las polizas automatizadas
            CrearDetalleDevolucionesNuevo(posicion, poliza_Sistema)
        End If
    End Sub

    Private Sub CrearDetalleDevolucionesNuevo(ByVal Fila As Integer, ByVal Poliza As String)
        Dim Item As Integer = 1
        Dim Cuenta(6)
        Dim CuentaPPD(11)
        Dim CuentaCO(7)
        Dim CuentaRetenciones(8)
        Dim Cadena()
        Dim Nivel() As String = Split(Me.TablaD.Item(NCuenta.Index, Fila).Value.ToString.Trim(), "-")

        If Nivel(1).Substring(12, 4) = "0000" And Nivel(1).Substring(8, 4) > "0000" Then ' Segundo nivel
            Nivel(1) = Nivel(1).Substring(4, 4)
        ElseIf Nivel(1).Substring(12, 4) > "0000" And Nivel(1).Substring(8, 4) > "0000" Then 'Tercer Nivel
            Nivel(1) = Nivel(1).Substring(8, 4)
        ElseIf Nivel(1).Substring(4, 4) > "0000" And Nivel(1).Substring(8, 4) = "0000" And Nivel(1).Substring(12, 4) = "0000" Then 'Primer Nivel
            Nivel(1) = Nivel(1).Substring(0, 4)
        End If


        If Me.TablaD.Item(ImpED.Index, Fila).Value > 0 And Me.TablaD.Item(ImpTD.Index, Fila).Value > 0 And Me.TablaD.Item(ImpCHD.Index, Fila).Value > 0 Then 'TODOS


            Cadena = Split(Me.TablaD.Item(CtaEfD.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpED.Index, Fila).Value, 0, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpTD.Index, Fila).Value, 0, Me.TablaD.Item(CtaEfD.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpCHD.Index, Fila).Value, 0, Me.TablaD.Item(CtaChequeD.Index, Fila).Value.ToString().Trim(), "")
            Item += 1

        ElseIf Me.TablaD.Item(ImpED.Index, Fila).Value > 0 And Me.TablaD.Item(ImpTD.Index, Fila).Value > 0 And Me.TablaD.Item(ImpCHD.Index, Fila).Value = 0 Then 'Efectivo y Transferencia
            Cadena = Split(Me.TablaD.Item(CtaEfD.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpED.Index, Fila).Value, 0, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpTD.Index, Fila).Value, 0, Me.TablaD.Item(CtaEfD.Index, Fila).Value.ToString().Trim(), "")
            Item += 1

        ElseIf Me.TablaD.Item(ImpED.Index, Fila).Value = 0 And Me.TablaD.Item(ImpTD.Index, Fila).Value > 0 And Me.TablaD.Item(ImpCHD.Index, Fila).Value > 0 Then 'Cheque y Transferencia
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpTD.Index, Fila).Value, 0, Me.TablaD.Item(CtaEfD.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpCHD.Index, Fila).Value, 0, Me.TablaD.Item(CtaChequeD.Index, Fila).Value.ToString().Trim(), "")
            Item += 1

        ElseIf Me.TablaD.Item(ImpED.Index, Fila).Value > 0 And Me.TablaD.Item(ImpTD.Index, Fila).Value = 0 And Me.TablaD.Item(ImpCHD.Index, Fila).Value > 0 Then 'Cheque y Efecivo
            Cadena = Split(Me.TablaD.Item(CtaEfD.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpED.Index, Fila).Value, 0, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpCHD.Index, Fila).Value, 0, Me.TablaD.Item(CtaChequeD.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        ElseIf Me.TablaD.Item(ImpED.Index, Fila).Value > 0 And Me.TablaD.Item(ImpTD.Index, Fila).Value = 0 And Me.TablaD.Item(ImpCHD.Index, Fila).Value = 0 Then ' Efecivo
            Cadena = Split(Me.TablaD.Item(CtaEfD.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpED.Index, Fila).Value, 0, Cadena(1), "")
            Item += 1
        ElseIf Me.TablaD.Item(ImpED.Index, Fila).Value = 0 And Me.TablaD.Item(ImpTD.Index, Fila).Value > 0 And Me.TablaD.Item(ImpCHD.Index, Fila).Value = 0 Then ' Transferencia
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpTD.Index, Fila).Value, 0, Me.TablaD.Item(CtaEfD.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        ElseIf Me.TablaD.Item(ImpED.Index, Fila).Value = 0 And Me.TablaD.Item(ImpTD.Index, Fila).Value = 0 And Me.TablaD.Item(ImpCHD.Index, Fila).Value > 0 Then 'Cheque 
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpCHD.Index, Fila).Value, 0, Me.TablaD.Item(CtaChequeD.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        End If

        If Me.TablaD.Item(AntiD.Index, Fila).Value > 0 Then ' Se verifica si cuenta con anticipos +
            Cuenta = CuentasAnticiposPue(Me.TablaD.Item(LetraCd.Index, Fila).Value.ToString.Trim())
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(AntiD.Index, Fila).Value, 0, Cuenta(0), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(AntiD.Index, Fila).Value, 0, Cuenta(2), "")
            Item += 1
        ElseIf Me.TablaD.Item(AntiD.Index, Fila).Value < 0 Then ' Se verifica si cuenta  anticipos -
            Cuenta = CuentasAnticiposPue(Me.TablaD.Item(LetraCd.Index, Fila).Value.ToString.Trim())
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(AntiD.Index, Fila).Value * -1, 0, Cuenta(0), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(AntiD.Index, Fila).Value * -1, 0, Cuenta(2), "")
            Item += 1
        End If
        If Me.TablaD.Item(AjusD.Index, Fila).Value > 0 Then ' Se verifica si cuenta con ajuste +
            Cadena = Split(Me.TablaD.Item(CtaAD.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(AjusD.Index, Fila).Value, 0, Cadena(0), "")
        ElseIf Me.TablaD.Item(AjusD.Index, fila).Value < 0 Then ' Se verifica si cuenta con ajuste -
            Cadena = Split(Me.TablaD.Item(CtaAD.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaD.Item(AjusD.Index, Fila).Value * -1, Cadena(0), "")
        End If

        If Me.TablaD.Item(PCD.Index, Fila).Value > 0 Then ' Se perdida cambiaria
            Item = Item + 1
            Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(PCD.Index, Fila).Value, 0, "7010000100000000", "")
        End If
        If Me.TablaD.Item(UCD.Index, Fila).Value > 0 Then ' Se utilidad cambiaria
            Item = Item + 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaD.Item(UCD.Index, Fila).Value, "7020000100000000", "")
        End If
        Cuenta = CuentasPue(Me.TablaD.Item(LetraCd.Index, Fila).Value.ToString.Trim())
        Dim NivelEp, NivelGp, NivelE, NivelG As String

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


        If Me.TablaD.Item(ImpEDV.Index, Fila).Value > 0 And Me.TablaD.Item(ImpGD.Index, Fila).Value > 0 Then ' tiene grabado y exento

            If Cuenta(6) = True Then
                Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpGD.Index, Fila).Value * -1, 0, RegresaCunetaDev(Cuenta(0), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelG, Cuenta(1)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpEDV.Index, Fila).Value * -1, 0, RegresaCunetaDev(Cuenta(2), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelE, Cuenta(3)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(IVARD.Index, Fila).Value * -1, 0, Cuenta(4), "")
                Item += 1
            Else
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaD.Item(ImpGD.Index, Fila).Value, RegresaCunetaDev(Cuenta(0), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelG, Cuenta(1)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaD.Item(ImpEDV.Index, Fila).Value, RegresaCunetaDev(Cuenta(2), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelE, Cuenta(3)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaD.Item(IVARD.Index, Fila).Value, Cuenta(4), "")
                Item += 1
            End If

        ElseIf Me.TablaD.Item(ImpEDV.Index, Fila).Value > 0 And Me.TablaD.Item(ImpGD.Index, Fila).Value <= 0 Then 'Tiene Exento

            If Cuenta(6) = True Then
                Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpEDV.Index, Fila).Value * -1, 0, RegresaCunetaDev(Cuenta(2), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelE, Cuenta(3)), "")
                Item += 1
            Else
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaD.Item(ImpEDV.Index, Fila).Value, RegresaCunetaDev(Cuenta(2), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelE, Cuenta(3)), "")
                Item += 1
            End If
            Item = Item + 1
        ElseIf Me.TablaD.Item(ImpEDV.Index, Fila).Value <= 0 And Me.TablaD.Item(ImpGD.Index, Fila).Value > 0 Then 'Tiene Grabado
            If Cuenta(6) = True Then
                Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(ImpGD.Index, Fila).Value * -1, 0, RegresaCunetaDev(Cuenta(0), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelG, Cuenta(1)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, Me.TablaD.Item(IVARD.Index, Fila).Value * -1, 0, Cuenta(4), "")
                Item += 1
            Else
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaD.Item(ImpGD.Index, Fila).Value, RegresaCunetaDev(Cuenta(0), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelG, Cuenta(1)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaD.Item(IVARD.Index, Fila).Value, Cuenta(4), "")
                Item += 1
            End If

        End If

        If Trim(Me.TablaD.Item(ProvPD.Index, Fila).Value + Me.TablaD.Item(ProvAD.Index, Fila).Value) > 0 Then ' Compras  
            Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP As Decimal
            PorcenPro = Me.TablaD.Item(ProvPD.Index, Fila).Value + Me.TablaD.Item(ProvAD.Index, Fila).Value / ImpFaCURA(Me.TablaD.Item(UUIDD.Index, Fila).Value, "Total_Real")
            ImpGpro = ImpFaCURA(Me.TablaD.Item(UUIDD.Index, Fila).Value, "Imp_Grabado") * PorcenPro
            ImpEpro = ImpFaCURA(Me.TablaD.Item(UUIDD.Index, Fila).Value, "Imp_Exento") * PorcenPro
            ImpIvaP = ImpFaCURA(Me.TablaD.Item(UUIDD.Index, Fila).Value, "IVA_real") * PorcenPro
            Dim clave As String = Eventos.ObtenerValorDB("ClaveEgresos", "Ligar", " ClaveEgresos.clave ='" & Me.TablaD.Item(LetraCd.Index, Fila).Value.ToString.Trim() & "' and ClaveEgresos.Id_Empresa= " & Me.lstCliente.SelectItem & "", True).ToString()
            CuentaPPD = CuentasPPD(clave)
            Cuenta = CuentasPue(clave)



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




            If ImpEpro > 0 Then
                Crea_detalle_poliza(Poliza, Item, ImpEpro, 0, RegresaCunetaDevNueva(CuentaPPD(9), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, CuentaPPD(10)), "")
                Item += 1
            End If
            If ImpGpro > 0 Then
                Crea_detalle_poliza(Poliza, Item, ImpGpro + ImpIvaP, 0, RegresaCunetaDevNueva(CuentaPPD(6), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, CuentaPPD(7)), "")
                Item += 1

            End If


            'Cancelacion de la provicion
            If ImpEpro > 0 And ImpGpro > 0 Then ' tiene grabado y exento


                Crea_detalle_poliza(Poliza, Item, ImpGpro, 0, RegresaCunetaDev(Cuenta(0), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelG, Cuenta(1)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, ImpEpro, 0, RegresaCunetaDev(Cuenta(2), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelE, Cuenta(3)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, ImpIvaP, 0, Cuenta(4), "")
                Item += 1

                Crea_detalle_poliza(Poliza, Item, 0, ImpGpro, RegresaCunetaDev(CuentaPPD(0), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelGp, CuentaPPD(1)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, 0, ImpEpro, RegresaCunetaDev(CuentaPPD(2), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelEp, CuentaPPD(3)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, 0, ImpIvaP, CuentaPPD(4), "")
                Item += 1




            ElseIf ImpEpro > 0 And ImpGpro <= 0 Then 'Tiene Exento
                'cargos
                Crea_detalle_poliza(Poliza, Item, ImpEpro, 0, RegresaCunetaDev(Cuenta(2), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelE, Cuenta(3)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, 0, ImpEpro, RegresaCunetaDev(CuentaPPD(2), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelEp, CuentaPPD(3)), "")
                Item += 1

            ElseIf ImpEpro <= 0 And ImpGpro > 0 Then 'Tiene Grabado
                Crea_detalle_poliza(Poliza, Item, ImpGpro, 0, RegresaCunetaDev(Cuenta(0), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelG, Cuenta(1)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, ImpIvaP, 0, Cuenta(4), "")
                Item += 1

                Crea_detalle_poliza(Poliza, Item, 0, ImpGpro, RegresaCunetaDev(CuentaPPD(0), Trim(Me.TablaD.Item(RFCED.Index, Fila).Value), Fila, NivelGp, CuentaPPD(1)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, 0, ImpIvaP, CuentaPPD(4), "")
                Item += 1

            End If


        End If
    End Sub



    Private Function LetraFactura(ByVal uuid As String)
        Dim letra As String
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT clave,Total_Real FROM dbo.Xml_Sat WHERE UUID = '" & uuid & "' and Tipo ='Factura' and Id_Empresa= " & Me.lstCliente.SelectItem & "")
        If ds.Tables(0).Rows.Count > 0 Then
            letra = ds.Tables(0).Rows(0)("clave")
        Else
            letra = ""
        End If
        Return Trim(letra)
    End Function
    Private Function ImpFaCURA(ByVal uuid As String, ByVal CAMPO As String)
        Dim iMP
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT " & CAMPO & " FROM dbo.Xml_Sat WHERE UUID = '" & uuid & "' and (Clave is not null and Clave <> ' ')    and Id_Empresa = " & Me.lstCliente.SelectItem & "")
        If ds.Tables(0).Rows.Count > 0 Then
            iMP = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
        Else
            iMP = 0
        End If
        Return iMP
    End Function
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

    Private Sub TablaD_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaD.CellClick

        Dim columna As Integer = Me.TablaD.CurrentCell.ColumnIndex
        Dim Nombre As String
        Nombre = Me.TablaD.Columns.Item(Me.TablaD.CurrentCell.ColumnIndex).Name.ToString
        Me.LstTextoDev.cargar(" Select '','' ")

        Select Case Nombre
            Case "LetraCd"
                'Me.LstTextoDev.cargar(" Select id_Contabilidad, rtrim(Clave) as Clave from Letras_Contabilidad where Id_Empresa = " & Me.lstCliente.SelectItem & " ")
                Me.LstTextoDev.Cargar(" Select Id_Clave, rtrim(Clave) as Clave from ClaveEgresos where ClaveEgresos.Id_Empresa = " & Me.lstCliente.SelectItem & " ")
                Me.LstTextoDev.SelectText = ""
            Case "CtaEfD"
                Me.LstTextoDev.cargar(" Select Id_cat_cuentas, rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1010' and Nivel2 > 0 and Nivel3 > 0 ")
                Me.LstTextoDev.SelectText = ""
            Case "CtaBancosD"
                Me.LstTextoDev.cargar(" Select Id_cat_cuentas, rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1020'and Nivel2 > 0 and Nivel3 > 0  ")
                Me.LstTextoDev.SelectText = ""
            Case "NomBankCHD"
                Me.LstTextoDev.cargar("select Id_Bancos_Clientes,Alias from Bancos_Clientes where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%Cheq%'")
                Me.LstTextoDev.SelectText = ""
            Case "BancoOd"
                Me.LstTextoDev.cargar(" select Id_Bancos_Clientes,Alias from Bancos_Clientes where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%Transf%'")
                Me.LstTextoDev.SelectText = ""
            Case "TipPolD"
                Me.LstTextoDev.cargar(" Select Id_Tipo_Pol_Sat,convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
                Me.LstTextoDev.SelectText = ""
            Case "CtaAD"
                Me.LstTextoDev.cargar("Select  Id_cat_cuentas,  rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " ")
                Me.LstTexto.SelectText = ""
            Case "CtaAntiD"
                Me.LstTextoDev.cargar("Select Id_cat_cuentas,  rtrim(Descripcion) +' - '+ convert(NVARCHAR,cuenta,103) as Cuenta from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 ='1070' and Nivel2 > 0 and Nivel3 > 0 ")
                Me.LstTextoDev.SelectText = ""
        End Select

    End Sub
    Private Sub Liberar_ProcesoDev(ByVal i As Integer)
        Dim contador As Integer = 0
        Dim cantidad As Decimal = Me.TablaD.Item(ImpPd.Index, i).Value
        cantidad = cantidad + Me.TablaD.Item(UCD.Index, i).Value
        cantidad = cantidad - Me.TablaD.Item(PCD.Index, i).Value
        cantidad = cantidad + Me.TablaD.Item(ProvAD.Index, i).Value
        cantidad = cantidad + Me.TablaD.Item(ProvPD.Index, i).Value

        'Calcula la diferencia en el registro
        Me.TablaD.Item(DifD.Index, i).Value = Math.Round(Calcula_diferencia(IIf(IsDBNull(Me.TablaD.Item(TRD.Index, i).Value) = True, 0, Me.TablaD.Item(TRD.Index, i).Value), Me.TablaD.Item(ImpED.Index, i).Value, Me.TablaD.Item(ImpTD.Index, i).Value, Me.TablaD.Item(ImpCHD.Index, i).Value, Me.TablaD.Item(AjusD.Index, i).Value, Me.TablaD.Item(AntiD.Index, i).Value, cantidad), 2)
        If Me.TablaD.Item(DifD.Index, i).Value <> 0 Then
            Me.TablaD.Item(DifD.Index, i).Style.BackColor = Color.Red
        Else
            Me.TablaD.Item(DifD.Index, i).Style.BackColor = Color.Green
        End If

        If Trim(UCase(Me.lblce.Text)) = "TRUE" Then ' Bloquera filas de Contabilidad electronica
            ' If Me.TablaD.Item(DifD.Index, i).Value > 0 Or Me.TablaD.Item(TipPolD.Index, i).Value = Nothing Or IIf(IsDBNull(Me.TablaD.Item(NomCuentaD.Index, i).Value) = True, "", Me.TablaD.Item(NomCuentaD.Index, i).Value) = "" Or IIf(IsDBNull(Me.TablaD.Item(NPolD.Index, i).Value) = True, "", Me.TablaD.Item(NPolD.Index, i).Value) = "" Then
            If Me.TablaD.Item(DifD.Index, i).Value <> 0 Or Me.TablaD.Item(TipPolD.Index, i).Value = Nothing Or IIf(IsDBNull(Me.TablaD.Item(NPolD.Index, i).Value.ToString) = True, "", Me.TablaD.Item(NPolD.Index, i).Value.ToString) = "" Or IIf(IsDBNull(Me.TablaD.Item(NomCuentaD.Index, i).Value) = True, "", Me.TablaD.Item(NomCuentaD.Index, i).Value.ToString) = "" Then

                Me.TablaD.Item(AplD.Index, i).Value = False
            Else

                If Me.TablaD.Item(ImpTD.Index, i).Value > 0 Then ' Bloqueo transferencia
                    Try
                        If Me.TablaD.Item(FecTD.Index, i).Value = Nothing Then
                            Me.TablaD.Item(AplD.Index, i).Value = False
                        Else
                            Me.TablaD.Item(AplD.Index, i).Value = True
                        End If
                    Catch ex As Exception
                        Me.TablaD.Item(AplD.Index, i).Value = False
                    End Try

                ElseIf Me.TablaD.Item(ImpCHD.Index, i).Value > 0 Then ' Bloqueo cheques
                    Try
                        If Me.TablaD.Item(FcHd.Index, i).Value = Nothing Then
                            Me.TablaD.Item(AplD.Index, i).Value = False
                        Else
                            Me.TablaD.Item(AplD.Index, i).Value = True
                        End If
                    Catch ex As Exception
                        Me.TablaD.Item(AplD.Index, i).Value = False
                    End Try

                ElseIf Me.TablaD.Item(ImpCHD.Index, i).Value > 0 And Me.TablaD.Item(ImpTD.Index, i).Value > 0 Then ' AMbos
                    Try
                        If Me.TablaD.Item(FecTD.Index, i).Value = Nothing Then
                            Me.TablaD.Item(AplD.Index, i).Value = False
                        Else
                            Me.TablaD.Item(AplD.Index, i).Value = True
                        End If
                    Catch ex As Exception
                        Me.TablaD.Item(AplD.Index, i).Value = False
                    End Try
                    Try
                        If Me.TablaD.Item(FcHd.Index, i).Value = Nothing Then
                            Me.TablaD.Item(AplD.Index, i).Value = False
                        Else
                            Me.TablaD.Item(AplD.Index, i).Value = True
                        End If
                    Catch ex As Exception
                        Me.TablaD.Item(AplD.Index, i).Value = False
                    End Try


                Else
                    Me.TablaD.Item(AplD.Index, i).Value = True
                End If
                If Me.TablaD.Item(AjusD.Index, i).Value <> 0 Then ' Bloqueo Ajuste
                    If IIf(IsDBNull(Me.TablaD.Item(CtaAD.Index, i).Value) = True, "", Me.TablaD.Item(CtaAD.Index, i).Value) = "" Then
                        Me.TablaD.Item(AplD.Index, i).Value = False
                    Else
                        Me.TablaD.Item(AplD.Index, i).Value = True
                    End If
                End If

                If Me.TablaD.Item(AntiD.Index, i).Value <> 0 Then ' Bloqueo Anticipo
                    If IIf(IsDBNull(Me.TablaD.Item(CtaAntiD.Index, i).Value) = True, "", Me.TablaD.Item(CtaAntiD.Index, i).Value) = "" Then
                        Me.TablaD.Item(AplD.Index, i).Value = False
                    Else
                        Me.TablaD.Item(AplD.Index, i).Value = True
                    End If
                End If

            End If
        Else
            'sin contabilidad electronica
            If Me.TablaD.Item(DifD.Index, i).Value <> 0 Or Me.TablaD.Item(TipPolD.Index, i).Value = Nothing Or IIf(IsDBNull(Me.TablaD.Item(NPolD.Index, i).Value) = True, "", Me.TablaD.Item(NPolD.Index, i).Value) = "" Then
                Me.TablaD.Item(AplD.Index, i).Value = False
            Else
                Me.TablaD.Item(AplD.Index, i).Value = True
            End If
            Try
                If Me.TablaD.Item(Ajus.Index, i).Value <> 0 Then ' Bloqueo Ajuste
                    If IIf(IsDBNull(Me.TablaD.Item(CtaAD.Index, i).Value) = True, "", Me.TablaD.Item(CtaAD.Index, i).Value) = "" Then
                        Me.TablaD.Item(AplD.Index, i).Value = False
                    Else
                        Me.TablaD.Item(AplD.Index, i).Value = True
                    End If
                End If

                If Me.TablaD.Item(Anti.Index, i).Value <> 0 Then ' Bloqueo Anticipo
                    If IIf(IsDBNull(Me.TablaD.Item(CtaAntiD.Index, i).Value) = True, "", Me.TablaD.Item(CtaAntiD.Index, i).Value) = "" Then
                        Me.TablaD.Item(AplD.Index, i).Value = False
                    Else
                        Me.TablaD.Item(AplD.Index, i).Value = True
                    End If
                End If
            Catch ex As Exception

            End Try
        End If

        For s As Integer = 0 To Me.TablaD.Rows.Count - 1
            If Me.TablaD.Item(0, s).Value = True Then
                contador = contador + 1
            End If
        Next
        If contador > 0 Then
            Me.CmdProcesoDev.Enabled = True
        End If
    End Sub

    Private Function Obtener_indexDV(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.LetraCd.Items.Count - 1
            If valor = Trim(Me.LetraCd.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_index2Dv(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.TipPolD.Items.Count - 1
            If valor = Trim(Me.TipPolD.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexBDv(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BancoOd.Items.Count - 1
            If valor = Trim(Me.BancoOd.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexBDvBH(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.NomBankCHD.Items.Count - 1
            If valor = Trim(Me.NomBankCHD.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function
    Private Function Obtener_indexdDv(ByVal valor As String)

        Dim Indice As Integer = -1
        For i As Integer = 0 To Me.BancoDD.Items.Count - 1
            If valor = Trim(Me.BancoDD.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice

    End Function

    Private Sub LstTextoDev_Enters() Handles LstTextoDev.Enters
        If Me.TablaD.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In TablaD.Rows
                If Fila.Cells(LetraCd.Index).Selected = True Then

                    Try
                        If Me.LstComple.SelectText = "" Then
                            Fila.Cells(LetraCd.Index).Value = ""
                        Else
                            Fila.Cells(LetraCd.Index).Value = Me.LstTextoDev.SelectText
                        End If


                    Catch ex As Exception

                    End Try
                    Dim ee As New System.Windows.Forms.DataGridViewCellEventArgs(LetraCd.Index, Fila.Index)
                    Me.TablaD_CellEndEdit(Me.TablaD, ee)
                ElseIf Fila.Cells(BancoOd.Index).Selected = True Then

                    Try
                        If Me.LstTextoDev.SelectText = "" Then
                            Fila.Cells(BancoOd.Index).Value = ""

                        Else
                            If Obtener_indexB(Me.LstTextoDev.SelectText) = 0 Then
                                Fila.Cells(BancoOd.Index).Value = ""
                            Else
                                Dim s As String = Me.BancoOd.Items(1)
                                Fila.Cells(BancoOd.Index).Value = Me.BancoOd.Items(Obtener_indexB(Me.LstTextoDev.SelectText))
                            End If
                        End If

                    Catch ex As Exception

                    End Try
                    Dim ee As New System.Windows.Forms.DataGridViewCellEventArgs(BancoOd.Index, Fila.Index)
                    Me.TablaD_CellEndEdit(Me.TablaD, ee)
                ElseIf Fila.Cells(BancoDD.Index).Selected = True Then
                    'Try
                    '    Dim s As String = Me.BancoDD.Items(1)
                    '    Fila.Cells(BancoDD.Index).Value = Me.BancoDD.Items(Obtener_indexd(Me.LstTextoDev.SelectText))
                    'Catch ex As Exception

                    'End Try
                ElseIf Fila.Cells(TipPolD.Index).Selected = True Then

                    If Me.LstTextoDev.SelectText = "" Then
                        Fila.Cells(TipPolD.Index).Value = ""
                    Else
                        Try
                            'If Obtener_index2(Me.LstTextoDev.SelectText) < 0 Then
                            '    Fila.Cells(TipPolD.Index).Value = ""
                            'Else
                            '    Dim s As String = Me.TipPolD.Items(1)
                            Fila.Cells(TipPolD.Index).Value = Me.LstTextoDev.SelectText
                            'End If

                        Catch ex As Exception

                        End Try
                    End If
                    Dim ee As New System.Windows.Forms.DataGridViewCellEventArgs(TipPolD.Index, Fila.Index)
                    Me.TablaD_CellEndEdit(Me.TablaD, ee)
                Else
                    For I As Integer = 0 To Me.TablaD.Rows.Count - 1

                        For j As Integer = 0 To Me.TablaD.Columns.Count - 1
                            If Me.TablaD.Item(j, Fila.Index).Selected = True And (j <> LetraCd.Index And j <> BancoOd.Index And j <> TipPolD.Index) Then
                                Me.TablaD.Item(j, Fila.Index).Value = Me.LstComple.SelectText
                            ElseIf Me.TablaD.Item(j, Fila.Index).Selected = True And (j = LetraCd.Index And j = BancoOd.Index And j = TipPolD.Index) Then
                                Me.TablaD.Item(j, Fila.Index).Value = ""
                            End If
                        Next
                    Next
                End If

            Next
        End If


    End Sub

    Private Sub TablaD_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaD.CellEndEdit
        Try
            Liberar_ProcesoDev(e.RowIndex)
            'Calcula cuenta cargos

            If Me.TablaD.Item(ProvPD.Index, e.RowIndex).Value > 0 Then
                Me.TablaD.Item(ProvAD.Index, e.RowIndex).Value = 0
            ElseIf Me.TablaD.Item(ProvAD.Index, e.RowIndex).Value > 0 Then
                Me.TablaD.Item(ProvPD.Index, e.RowIndex).Value = 0
            End If

            If Me.TablaD.Item(LetraCd.Index, e.RowIndex).Value <> Nothing Then

                If Me.TablaD.Item(LetraCd.Index, e.RowIndex).Value.ToString().Contains("PP") Then
                    Me.TablaD.Item(ProvAD.Index, e.RowIndex).Value = 0
                    Me.TablaD.Item(ProvPD.Index, e.RowIndex).Value = 0
                End If
                Try
                    Dim Nuevo As String = CuentaTipoD(Me.TablaD.Item(RFCED.Index, e.RowIndex).Value, Trim(Me.TablaD.Item(LetraCd.Index, e.RowIndex).Value), e.RowIndex)
                    Me.TablaD.Item(NomCuentaD.Index, e.RowIndex).Value = Nuevo
                    Me.TablaD.Item(LetraSD.Index, e.RowIndex).Value = Eventos.Calcula_letraSat(Me.TablaD.Item(Fpago.Index, e.RowIndex).Value, Me.TablaD.Item(UCFDI.Index, e.RowIndex).Value)

                Catch ex As Exception

                End Try

            Else
                Me.TablaD.Item(NomCuentaD.Index, e.RowIndex).Value = ""
            End If

            'CALCULAR CUENTA DE EFECTIVO
            If Me.TablaD.Item(ImpED.Index, e.RowIndex).Value > 0 Then
                If Candado_Importe_EfectivoDev(e.RowIndex) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo

                    Me.TablaD.Item(CtaEfD.Index, e.RowIndex).Value = Cuenta_Efectivo()
                    Me.TablaD.Columns(CtaEfD.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    If RadMessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaD.Item(CtaEfD.Index, e.RowIndex).Value = Cuenta_Efectivo()
                        Me.TablaD.Columns(CtaEfD.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaD.Item(ImpED.Index, e.RowIndex).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaD.Item(CtaEfD.Index, e.RowIndex).Value = ""
            End If

            'CALCULAR CUENTA DE Ajustes
            If Me.TablaD.Item(AjusD.Index, e.RowIndex).Value <> 0 Then
                If Candado_Importe_EfectivoDev(e.RowIndex) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo

                    Me.TablaD.Item(CtaAD.Index, e.RowIndex).Value = Cuenta_Efectivo()
                    Me.TablaD.Columns(CtaAD.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    If RadMessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaD.Item(CtaAD.Index, e.RowIndex).Value = Cuenta_Efectivo()
                        Me.TablaD.Columns(CtaAD.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaD.Item(AjusD.Index, e.RowIndex).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaD.Item(CtaAD.Index, e.RowIndex).Value = ""
            End If
            'CALCULAR CUENTA DE anticipos
            If Me.TablaD.Item(AntiD.Index, e.RowIndex).Value > 0 Then

                Me.TablaD.Item(CtaAntiD.Index, e.RowIndex).Value = Cuenta_Anticipo()
                Me.TablaD.Columns(CtaAntiD.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Else
                Me.TablaD.Item(CtaAntiD.Index, e.RowIndex).Value = ""
            End If

            ' Importe de Transferencia
            If Me.TablaD.Item(ImpTD.Index, e.RowIndex).Value > 0 Then

                Me.TablaD.Item(CtaBancosD.Index, e.RowIndex).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias like '%Tr%'", True)

                If Trim(UCase(Me.lblce.Text)) = "TRUE" Then
                    Me.TablaD.Columns(FecTD.Index).Visible = True
                Else
                    Me.TablaD.Columns(FecTD.Index).Visible = True
                End If


            Else
                Me.TablaD.Columns(BancoOd.Index).Visible = False
                Me.TablaD.Columns(CtaOD.Index).Visible = False
                Me.TablaD.Columns(BancoDD.Index).Visible = False
                Me.TablaD.Columns(CtaDD.Index).Visible = False
            End If
            If Me.TablaD.Item(ImpCHD.Index, e.RowIndex).Value > 0 Then
                'Cheques

                Me.TablaD.Item(CtaChequeD.Index, e.RowIndex).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias like '%Ch%'", True)


                If Trim(UCase(Me.lblce.Text)) = "TRUE" Then

                    Me.TablaD.Columns(FcHd.Index).Visible = True
                Else
                    Me.TablaD.Columns(FcHd.Index).Visible = True
                End If
            Else
                'Se Ocutan Columnas de Contabilidad Electronica
                Me.TablaD.Columns(NomBankCHD.Index).Visible = False
                Me.TablaD.Columns(CtaOrigCHD.Index).Visible = False
                Me.TablaD.Columns(NchD.Index).Visible = False


            End If
        Catch ex As Exception

        End Try


    End Sub
    Private Function Candado_Importe_EfectivoDev(ByVal i As Integer)
        Dim hacer As Boolean
        If Me.TablaD.Item(ImpED.Index, i).Value > 2000 Then
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
    Private Function Candado_Importe_Efectivocomple(ByVal i As Integer)
        Dim hacer As Boolean
        If Me.TablaC.Item(ImpEComplemento.Index, i).Value > 2000 Then

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
    Private Sub Cargar_bancosDev(ByVal bancos As String)
        Dim sql As String = " SELECT rtrim(Bancos.Clave) +'-'+ Bancos_Clientes.Alias AS Alias FROM Bancos_Clientes INNER JOIN Bancos ON Bancos_Clientes.Id_Banco =Bancos.Id_Banco  where Id_Empresa = " & Me.lstCliente.SelectItem & " and alias like '%" & bancos & "%'"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If bancos = "Cheq" Then
                'Cheque
                If Me.NomBankCHD.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.NomBankCHD.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                Else
                    Me.NomBankCHD.Items.Clear()
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.NomBankCHD.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                    Next
                End If
            Else
                'Transferencia
                'Origen
                If Me.BancoOd.Items.Count = 0 Then
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancoOd.Items.Add(ds.Tables(0).Rows(i)("Alias"))

                    Next
                Else
                    Me.BancoOd.Items.Clear()

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.BancoOd.Items.Add(ds.Tables(0).Rows(i)("Alias"))

                    Next
                End If

            End If
        End If

    End Sub

    Private Function RegresaCunetaDevNueva(ByVal Cuenta As String, ByVal RFC As String, ByVal Posicion As Integer, Optional ByVal Tipo As Integer = 0)
        Dim Cta As String = ""
        Dim sql As String = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & Cuenta.Substring(0, 4) & "' AND Nivel2= '" & Cuenta.Substring(4, 4) & "' AND Nivel3 = '" & Cuenta.Substring(8, 4) & "' AND Nivel4 > 0 AND RFC = '" & RFC & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cta = ds.Tables(0).Rows(0)(0)
        Else
            If Tipo = 3 Then
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Cuenta.ToString.Substring(4, 4) & " AND Nivel3 >= 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cuenta.ToString.Substring(4, 4), Cta.ToString.Substring(0, 4),
                               "0000", Cuenta.Substring(0, 8) & Cta & "0000", Me.TablaD.Item(RFCED.Index, Posicion).Value & " " & Me.TablaD.Item(NomEd.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaD.Item(LetraSD.Index, Posicion).Value, Me.TablaD.Item(RFCED.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 8) & Cta & "0000"
            ElseIf Tipo = 4 Then
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", " CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & Cuenta.ToString.Substring(8, 4) & " AND Nivel4 > 0 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cuenta.ToString.Substring(4, 4), Cuenta.ToString.Substring(8, 4),
                                  Cta, Cuenta.Substring(0, 12) & Cta, Me.TablaD.Item(RFCED.Index, Posicion).Value & " " & Me.TablaD.Item(NomEd.Index, Posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaD.Item(LetraSD.Index, Posicion).Value, Me.TablaD.Item(RFCED.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 12) & Cta
            ElseIf Tipo = 2 Then
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 >= 0 AND Nivel3 = 0000 AND Nivel4  = 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cta, "0000",
                                  "0000", Cuenta.Substring(0, 4) & Cta & "00000000", Me.TablaD.Item(RFCED.Index, Posicion).Value & " " & Me.TablaD.Item(NomEd.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaD.Item(LetraSD.Index, Posicion).Value, Me.TablaD.Item(RFCED.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 4) & Cta & "00000000"

            End If

        End If
        Return Cta
    End Function
    Private Function RegresaCunetaDev(ByVal Cuenta As String, ByVal RFC As String, ByVal Posicion As Integer, ByVal Nivel As String, Optional ByVal Tipo As Integer = 0)
        Dim sql As String = ""
        Dim Cta As String = ""
        If Tipo = 2 Then
            sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & Cuenta.Substring(0, 4) & "' AND Nivel2 > 0 AND Nivel3 =  0000 AND Nivel4 =0000 AND RFC = '" & RFC & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        ElseIf Tipo = 3 Then
            sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & Cuenta.Substring(0, 4) & "' AND Nivel2= '" & Nivel & "' AND Nivel3 > 0 AND Nivel4 = 0000 AND RFC = '" & RFC & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        ElseIf Tipo = 4 Then
            sql = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & Cuenta.Substring(0, 4) & "' AND Nivel2= '" & Cuenta.Substring(4, 4) & "' AND Nivel3 = '" & Nivel & "' AND Nivel4 > 0 AND RFC = '" & RFC & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "

        End If
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cta = ds.Tables(0).Rows(0)(0)
        Else
            If Tipo = 3 Then
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Cuenta.ToString.Substring(4, 4) & " AND Nivel3 >= 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Nivel, Cta.ToString.Substring(0, 4),
                               "0000", Cuenta.Substring(0, 4) & Nivel & Cta & "0000", Me.TablaD.Item(RFCED.Index, Posicion).Value & " " & Me.TablaD.Item(NomEd.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaD.Item(LetraSD.Index, Posicion).Value, Me.TablaD.Item(RFCED.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 4) & Nivel & Cta & "0000"
            ElseIf Tipo = 4 Then
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", " CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & Cuenta.ToString.Substring(8, 4) & " AND Nivel4 > 0 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cuenta.ToString.Substring(4, 4), Nivel,
                                  Cta, Cuenta.Substring(0, 8) & Nivel & Cta, Me.TablaD.Item(RFCED.Index, Posicion).Value & " " & Me.TablaD.Item(NomEd.Index, Posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaD.Item(LetraSD.Index, Posicion).Value, Me.TablaD.Item(RFCED.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 12) & Cta
            ElseIf Tipo = 2 Then
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 >= 0 AND Nivel3 = 0000 AND Nivel4  = 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Nivel, Cta, "0000",
                                  "0000", Nivel & Cta & "00000000", Me.TablaD.Item(RFCED.Index, Posicion).Value & " " & Me.TablaD.Item(NomEd.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaD.Item(LetraSD.Index, Posicion).Value, Me.TablaD.Item(RFCED.Index, Posicion).Value)
                Cta = Nivel & Cta & "00000000"

            End If

        End If
        Return Cta
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
                               "0000", Cuenta.Substring(0, 8) & Cta & "0000", Me.TablaC.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaC.Item(NEmComplemento.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaC.Item(LetraSatComplemento.Index, Posicion).Value, Me.TablaC.Item(RFCComplemento.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 8) & Cta & "0000"
            ElseIf Tipo = 4 Then
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & Cuenta.ToString.Substring(8, 4) & " AND Nivel4 >= 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cuenta.ToString.Substring(4, 4), Cuenta.ToString.Substring(8, 4),
                                  Cta, Cuenta.Substring(0, 12) & Cta, Me.TablaC.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaC.Item(NEmComplemento.Index, Posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaC.Item(LetraSatComplemento.Index, Posicion).Value, Me.TablaC.Item(RFCComplemento.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 12) & Cta
            ElseIf Tipo = 2 Then

                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "   AND Nivel2 >= 0 AND Nivel3 = 0000 AND Nivel4  = 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cta, "0000",
                                 "0000", Cuenta.Substring(0, 4) & Cta & "00000000", Me.TablaC.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaC.Item(NEmComplemento.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaC.Item(LetraSatComplemento.Index, Posicion).Value, Me.TablaC.Item(RFCComplemento.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 4) & Cta & "00000000"
            End If




        End If
        Return Cta
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
                    Nivel = RegresaNivel(Cuenta.ToString.Substring(0, 4), Me.TablaC.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaC.Item(NEmComplemento.Index, Posicion).Value)
                End If
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Nivel & " AND Nivel3 >= 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Nivel, Cta.ToString.Substring(0, 4),
                               "0000", Cuenta.Substring(0, 8) & Cta & "0000", Me.TablaC.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaC.Item(NEmComplemento.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaC.Item(LetraSatComplemento.Index, Posicion).Value, Me.TablaC.Item(RFCComplemento.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 4) & Nivel & Cta & "0000"
            ElseIf Tipo = 4 Then
                If Nivel = "0000" Then
                    Nivel = RegresaNivel(Cuenta.ToString.Substring(0, 8), Me.TablaC.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaC.Item(NEmComplemento.Index, Posicion).Value)
                End If
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & Nivel & " AND Nivel4 >= 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cuenta.ToString.Substring(4, 4), Nivel,
                                  Cta, Cuenta.Substring(0, 8) & Nivel & Cta, Me.TablaC.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaC.Item(NEmComplemento.Index, Posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaC.Item(LetraSatComplemento.Index, Posicion).Value, Me.TablaC.Item(RFCComplemento.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 8) & Nivel & Cta
            ElseIf Tipo = 2 Then

                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "   AND Nivel2 >= 0 AND Nivel3 = 0000 AND Nivel4  = 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Nivel, Cta, "0000",
                                 "0000", Cuenta.Substring(0, 4) & Cta & "00000000", Me.TablaC.Item(RFCComplemento.Index, Posicion).Value & " " & Me.TablaC.Item(NEmComplemento.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaC.Item(LetraSatComplemento.Index, Posicion).Value, Me.TablaC.Item(RFCComplemento.Index, Posicion).Value)
                Cta = Nivel & Cta & "00000000"
            End If




        End If
        Return Cta
    End Function


    Private Function RegresaCuentaNuevo(ByVal Cuenta As String, ByVal RFC As String, ByVal Posicion As Integer, Optional ByVal Tipo As Integer = 0)
        Dim Cta As String = ""
        Dim sql As String = "SELECT cuenta FROM Catalogo_de_Cuentas WHERE Nivel1='" & Cuenta.Substring(0, 4) & "' AND Nivel2= '" & Cuenta.Substring(4, 4) & "' AND Nivel3 = '" & Cuenta.Substring(8, 4) & "' AND Nivel4 > 0 AND RFC = '" & RFC & "' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Cta = ds.Tables(0).Rows(0)(0)
        Else

            If Tipo = 3 Then



                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Cuenta.ToString.Substring(4, 4) & " AND Nivel3 >= 0 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cuenta.ToString.Substring(4, 4), Cta.ToString.Substring(0, 4),
                               "0000", Cuenta.Substring(0, 8) & Cta & "0000", Me.TablaImportar.Item(RFCE.Index, Posicion).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, Posicion).Value, Me.TablaImportar.Item(RFCE.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 8) & Cta & "0000"
            ElseIf Tipo = 4 Then

                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & Cuenta.ToString.Substring(8, 4) & " AND Nivel4 >= 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cuenta.ToString.Substring(4, 4), Cuenta.ToString.Substring(8, 4),
                                  Cta, Cuenta.Substring(0, 12) & Cta, Me.TablaImportar.Item(RFCE.Index, Posicion).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, Posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, Posicion).Value, Me.TablaImportar.Item(RFCE.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 12) & Cta

            ElseIf Tipo = 2 Then

                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 >= 0 AND Nivel3 = 0000 AND Nivel4  = 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cta, "0000",
                                  "0000", Cuenta.Substring(0, 4) & Cta & "00000000", Me.TablaImportar.Item(RFCE.Index, Posicion).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, Posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, Posicion).Value, Me.TablaImportar.Item(RFCE.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 4) & Cta & "00000000"

            End If
        End If
        Return Cta
    End Function


    Private Function RegresaCuentaNuevoFactura(ByVal Cuenta As String, ByVal RFC As String, ByVal Posicion As Integer, ByVal Nivel As String, Optional ByVal Tipo As Integer = 0)
        ' GG 16  NI 4  CUENTA "6020-0020  " NIVEL = 0048
        ' GG 16  NI 3  CUENTA "1180  " NIVEL = 0001
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

            If Tipo = 3 Then
                If Nivel = "0000" Then
                    Nivel = RegresaNivel(Cuenta.ToString.Substring(0, 4), Me.TablaImportar.Item(RFCE.Index, Posicion).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, Posicion).Value)
                End If
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel3 ) + 1 IS NULL THEN 1 WHEN max (Nivel3 ) + 1 IS NOT NULL THEN   max (Nivel3 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Nivel & " AND Nivel3 >= 0 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Nivel, Cta,
                               "0000", Cuenta.Substring(0, 4) & Nivel & Cta & "0000", Me.TablaImportar.Item(RFCE.Index, Posicion).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, Posicion).Value,
                                Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, Posicion).Value, Me.TablaImportar.Item(RFCE.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 4) & Nivel & Cta & "0000"
            ElseIf Tipo = 4 Then
                If Nivel = "0000" Then
                    Nivel = RegresaNivel(Cuenta.ToString.Substring(0, 8), Me.TablaImportar.Item(RFCE.Index, Posicion).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, Posicion).Value)
                End If
                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel4 ) + 1 IS NULL THEN 1 WHEN max (Nivel4 ) + 1 IS NOT NULL THEN   max (Nivel4 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 =" & Cuenta.ToString.Substring(4, 4) & " AND Nivel3=" & Nivel & " AND Nivel4 >= 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Cuenta.ToString.Substring(0, 4), Cuenta.ToString.Substring(4, 4), Nivel,
                                  Cta, Cuenta.Substring(0, 8) & Nivel & Cta, Me.TablaImportar.Item(RFCE.Index, Posicion).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, Posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, Posicion).Value, Me.TablaImportar.Item(RFCE.Index, Posicion).Value)
                Cta = Cuenta.Substring(0, 8) & Nivel & Cta

            ElseIf Tipo = 2 Then

                Cta = Val(ObtenerValorDB("Catalogo_de_cuentas", "CASE WHEN max (Nivel2 ) + 1 IS NULL THEN 1 WHEN max (Nivel2 ) + 1 IS NOT NULL THEN   max (Nivel2 ) + 1 END AS C ", "  Nivel1 =" & Cuenta.ToString.Substring(0, 4) & "  AND Nivel2 >= 0 AND Nivel3 = 0000 AND Nivel4  = 0000 and Id_Empresa = " & Me.lstCliente.SelectItem & "", True))
                Cta = Format(Cta).PadLeft(4, "0")
                Crear_cuenta(Nivel, Cta, "0000",
                                  "0000", Nivel & Cta & "00000000", Me.TablaImportar.Item(RFCE.Index, Posicion).Value & " " & Me.TablaImportar.Item(NomEmisor.Index, Posicion).Value,
                                  Me.lstCliente.SelectItem, Me.TablaImportar.Item(ContabilizacionC.Index, Posicion).Value, Me.TablaImportar.Item(RFCE.Index, Posicion).Value)
                Cta = Nivel & Cta & "00000000"

            End If
        End If
        Return Cta
    End Function

    '************************************************ Complementos de pago *********************************************************************************

    Private Sub CmdSalirComple_Click(sender As Object, e As EventArgs) Handles CmdSalirComple.Click
        Me.Close()
    End Sub
    Private Sub CmdLimpiaComple_Click(sender As Object, e As EventArgs) Handles CmdLimpiaComple.Click
        If Me.TablaC.Rows.Count > 0 Then
            LimpiaC()
            Me.LstComple.SelectText = ""
        End If
    End Sub
    Private Sub LimpiaC()
        Me.TablaC.Rows.Clear()
    End Sub
    Private Sub CmdBuscarComple_Click(sender As Object, e As EventArgs) Handles CmdBuscarComple.Click
        activo = True
        LimpiaC()
        If Me.lstCliente.SelectText <> "" Then
            If Buscar_Parametros(Me.lstCliente.SelectItem) = True Then
                Try
                    If DatoC.Tables(0).Rows.Count > 0 Then
                        DatoC.Clear()
                    End If
                Catch ex As Exception

                End Try
                Buscar_Complementos(Me.lstCliente.SelectItem, " and Fecha_Emision >= " & Eventos.Sql_hoy(Me.DtFiComple.Value) & " and Fecha_Emision <= " & Eventos.Sql_hoy(DtFFComple.Value) & "")
                SP2.RunWorkerAsync(Me.TablaC)
                Control.CheckForIllegalCrossThreadCalls = False
                Me.TablaC.Enabled = True
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado un cliente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
        Activo = False
    End Sub
    Private Sub Buscar_Complementos(ByVal Id_Empresa As Integer, ByVal periodo As String)
        Dim sql As String = "SELECT Xml_Complemento.Id_Xml_Complemento, Xml_Complemento.Tipo, Xml_Complemento.UUID, Metodos_de_Pago.Descripcion AS Forma_de_Pago, Xml_Complemento.Fecha_Emision, Xml_Complemento.Folio, Xml_Complemento.Serie, 
                  Xml_Complemento.SubTotal, Xml_Complemento.Moneda, Xml_Complemento.Total, Xml_Complemento.LugarExpedicion, Xml_Complemento.RFC_Emisor, Xml_Complemento.Nombre_Emisor, Xml_Complemento.Regimen_Fiscal, 
                  Xml_Complemento.RFC_Receptor, Xml_Complemento.Nombre_Receptor, Xml_Complemento.UsoCFDI, Xml_Complemento.ClaveProdServ, Xml_Complemento.Cantidad, Xml_Complemento.Unidad, Xml_Complemento.Descripcion, 
                  Xml_Complemento.Valor_Unitario, Xml_Complemento.Importe, Xml_Complemento.FechaPago, Xml_Complemento.MonedaP, Xml_Complemento.TipoCambioP, Xml_Complemento.Monto, Xml_Complemento.Num_Operacion, 
                  Xml_Complemento.RfcEmisorCtaOrd, Xml_Complemento.NomBancoOrdExt, Xml_Complemento.CtaOrdenante, Xml_Complemento.RfcEmisorCtaBen, Xml_Complemento.CtaBeneficiario, Xml_Complemento.TipoCadPago, 
                  Xml_Complemento.CertPago, Xml_Complemento.CadPago, Xml_Complemento.SelloPago, Xml_Complemento.Conceptos, Xml_Complemento.IdDocumento, Xml_Complemento.SerieDR, Xml_Complemento.FolioDR, 
                  Xml_Complemento.MonedaDR, Xml_Complemento.TipoCambioDR, Xml_Complemento.MetodoDePagoDR, Xml_Complemento.NumParcialidad, Xml_Complemento.ImpSaldoAnt, Xml_Complemento.ImpPagado, 
                  Xml_Complemento.ImpSaldoInsoluto, Xml_Complemento.Consecutivo_Carga, Xml_Complemento.Id_Empresa, Xml_Complemento.Carga_Procesada, Xml_Complemento.ID_poliza, Xml_Complemento.Emitidas, Xml_Complemento.Letra_Sat, 
                  Xml_Complemento.Nombre_cuenta, Xml_Complemento.Clave, Xml_Complemento.Anio_Contable, Xml_Complemento.Mes_Contable, Xml_Complemento.Imp_Efectivo, Xml_Complemento.Cuenta_Efectivo, Xml_Complemento.Imp_Transf, 
                  Xml_Complemento.Banco_Origen, Xml_Complemento.Cuenta_Origen, Xml_Complemento.Banco_Destino, Xml_Complemento.Fecha_Transaccion, Xml_Complemento.Imp_Cheque, Xml_Complemento.Nom_Banco_Ch, 
                  Xml_Complemento.Cuenta_Origen_Ch, Xml_Complemento.No_Cheque, Xml_Complemento.Fecha_Ch, Xml_Complemento.Cuenta_Cheques, Xml_Complemento.Cuenta_Bancos, Xml_Complemento.Provision_Acreedor, Xml_Complemento.Provision_Proveedor, 
                  Xml_Complemento.Diferencia, Xml_Complemento.Tipo_Poliza, Xml_Complemento.Imp_Grabado, Xml_Complemento.Imp_Exento, Xml_Complemento.IVA_real, Xml_Complemento.Prc_Pago_Acumulado, Xml_Complemento.Total_Real, 
                  Xml_Complemento.Utilidad_Cambiaria, Xml_Complemento.Perdida_Cambiaria, Xml_Complemento.Imp_Devolucion, Xml_Complemento.Cuenta_Devolucion, Xml_Complemento.Numpol, Xml_Complemento.RIRS, Xml_Complemento.RIVA, 
                  Xml_Complemento.Cuenta_Destino, Xml_Complemento.Ajuste, Xml_Complemento.Anticipos, Xml_Complemento.Cta_Ajuste, Xml_Complemento.Cta_Anticipos, Xml_Complemento.Cta_Orden,

Imp_Transf1,        Banco_Origen1,	 Cuenta_Origen1,	 Banco_Destino1, Cuenta_Destino1 	,	 Fecha_Transaccion1,	 Cuenta_Bancos1 
,Imp_Transf2	 ,Banco_Origen2,	 Cuenta_Origen2	, Banco_Destino2	 , Cuenta_Destino2	, Fecha_Transaccion2,Cuenta_Bancos2	 
,Imp_Transf3	 ,Banco_Origen3,	 Cuenta_Origen3,	 Banco_Destino3,Cuenta_Destino3	,	 Fecha_Transaccion3,	 Cuenta_Bancos3	 
,Imp_Transf4	 ,Banco_Origen4,	 Cuenta_Origen4	, Banco_Destino4	 , Cuenta_Destino4	,Fecha_Transaccion4, Cuenta_Bancos4 
,Imp_Transf5 ,Banco_Origen5,	 Cuenta_Origen5,	 Banco_Destino5,Cuenta_Destino5	,	 Fecha_Transaccion5,	 Cuenta_Bancos5	 
,Imp_Transf6	 ,Banco_Origen6,	 Cuenta_Origen6,	 Banco_Destino6,Cuenta_Destino7	,	 Fecha_Transaccion6, Cuenta_Bancos6	 
,Imp_Transf7	 ,Banco_Origen7,	 Cuenta_Origen7,	 Banco_Destino7,Cuenta_Destino7	,	 Fecha_Transaccion7,	 Cuenta_Bancos7,	 
Imp_Transf8	 ,Banco_Origen8,	 Cuenta_Origen8,	 Banco_Destino8,Cuenta_Destino8	, Fecha_Transaccion8,	 Cuenta_Bancos8	 
,Imp_Transf9	 ,Banco_Origen9,	 Cuenta_Origen9,	 Banco_Destino9,Cuenta_Destino9	,	 Fecha_Transaccion9,	 Cuenta_Bancos9,
Imp_Transf10,	 Banco_Origen10,	 Cuenta_Origen10, Banco_Destino10,Cuenta_Destino10	, Fecha_Transaccion10,	 Cuenta_Bancos10
                  FROM     Xml_Complemento INNER JOIN
                                    Metodos_de_Pago ON Xml_Complemento.FormaDePago = Metodos_de_Pago.Clave
                  WHERE  (Xml_Complemento.Emitidas = " & Eventos.Bool2(False) & ") AND (Xml_Complemento.Id_Empresa = " & Id_Empresa & ") " & periodo & " and ID_poliza IS NULL "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaC.RowCount = 1
            Dim contab As DataSet = Eventos.Obtener_DS(" Select Rtrim(Clave) as Clave from ClaveEgresos where ClaveEgresos.Id_Empresa = " & Me.lstCliente.SelectItem & " and (CargoG =1 or CargoE = 1 ) and Negativo=0 ")
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
            Dim Tipo As DataSet = Eventos.Obtener_DS(" Select convert(NVARCHAR,Clave,103)  +' - ' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
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
            Cargar_bancosComple("Transf")
            Cargar_bancosComple("Cheq")
            Me.TablaC.RowCount = ds.Tables(0).Rows.Count
            DatoC = ds

        Else
            MessageBox.Show("No hay registros para procesar", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If



    End Sub
    Private Sub Complementos(ByVal ds As DataSet)
        Try
            If ds.Tables(0).Rows.Count > 0 Then

            End If
        Catch ex As Exception
            Exit Sub
        End Try
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Calculando Pagos por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = ds.Tables(0).Rows.Count
        For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim Fila As DataGridViewRow = Me.TablaC.Rows(j)
            Me.TablaC.Item(0, j).Value = False
            Me.TablaC.Item(Id_Complemento.Index, j).Value = ds.Tables(0).Rows(j)("Id_Xml_Complemento")
            Me.TablaC.Item(Tp.Index, j).Value = ds.Tables(0).Rows(j)("Tipo")
            Me.TablaC.Item(FechaEComplemento.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Emision")
            Me.TablaC.Item(UUIDComplemento.Index, j).Value = ds.Tables(0).Rows(j)("UUID")
            Me.TablaC.Item(UUIDRComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("IdDocumento")) = True, "", ds.Tables(0).Rows(j)("IdDocumento"))
            Me.TablaC.Item(UCFDIComplemento.Index, j).Value = ds.Tables(0).Rows(j)("UsoCFDI")
            Me.TablaC.Item(RFCComplemento.Index, j).Value = ds.Tables(0).Rows(j)("RFC_Emisor")
            Me.TablaC.Item(NEmComplemento.Index, j).Value = ds.Tables(0).Rows(j)("Nombre_Emisor").ToString.Replace("-", "")
            Me.TablaC.Item(Conc.Index, j).Value = ds.Tables(0).Rows(j)("Descripcion")
            Me.TablaC.Item(MPc.Index, j).Value = ds.Tables(0).Rows(j)("MetodoDePagoDR")
            Me.TablaC.Item(FpagoComplemento.Index, j).Value = Trim(ds.Tables(0).Rows(j)("Forma_de_Pago"))
            Me.TablaC.Item(NomCtaComplemento.Index, j).Value = ds.Tables(0).Rows(j)("Nombre_cuenta")
            Try
                Me.TablaC.Item(RefFact.Index, j).Value = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, j).Value.ToString().Trim(), "Clave").ToString().Trim()
                ComplemantosL(j)
            Catch ex As Exception

            End Try

            Dim PorcenPro, ImpSubt, ImpIvaP As Decimal ' Calcula valores de la factura
            Try
                PorcenPro = ds.Tables(0).Rows(j)("Monto") / ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, j).Value, "Total_Real")
            Catch ex As Exception
                PorcenPro = 0
            End Try
            'PorcenPro = ds.Tables(0).Rows(j)("Total") / ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, j).Value, "Total_Real")
            ImpIvaP = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, j).Value, "IVA_real") * PorcenPro
            ImpSubt = ds.Tables(0).Rows(j)("Monto") - ImpIvaP
            Me.TablaC.Item(SubTotComplemento.Index, j).Value = ImpSubt
            Me.TablaC.Item(IvaComplemento.Index, j).Value = ImpIvaP
            Me.TablaC.Item(TotComplemento.Index, j).Value = ds.Tables(0).Rows(j)("ImpPagado")
            Me.TablaC.Item(LetraSatComplemento.Index, j).Value = ds.Tables(0).Rows(j)("Letra_Sat")


            Try

                Fila.Cells(LetraContabilidadComplemento.Index).Value = Me.LetraContabilidadComplemento.Items(Obtener_indexletra(Trim(ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, j).Value, "Clave"))))


            Catch ex As Exception

            End Try
            Dim year As String = ds.Tables(0).Rows(j)("Fecha_Emision").ToString.Substring(6, 4)
            Dim month As String = ds.Tables(0).Rows(j)("Fecha_Emision").ToString.Substring(3, 2)
            Me.TablaC.Item(AnioComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Anio_Contable")) = True, year, ds.Tables(0).Rows(j)("Anio_Contable"))
            Me.TablaC.Item(MesComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Mes_Contable")) = True, month, ds.Tables(0).Rows(j)("Mes_Contable"))
            Me.TablaC.Item(ImpEfComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Efectivo")) = True, 0, ds.Tables(0).Rows(j)("Imp_Efectivo"))
            Me.TablaC.Item(CtaEfComplemento.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Efectivo")
            Me.TablaC.Item(ImpTComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Transf")) = True, 0, ds.Tables(0).Rows(j)("Imp_Transf"))
            Try
                If Trim(ds.Tables(0).Rows(j)("Banco_Origen")) <> "" Then
                    Fila.Cells(BancoOrigenTComplemento.Index).Value = Me.BancoOrigenTComplemento.Items(Obtener_indexbancoO(Trim(ds.Tables(0).Rows(j)("Banco_Origen"))))
                End If
            Catch ex As Exception

            End Try
            Me.TablaC.Item(CtaOTComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Origen")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Origen"))
            Me.TablaC.Item(BancoDtComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Banco_Destino")) = True, "", ds.Tables(0).Rows(j)("Banco_Destino"))
            Me.TablaC.Item(CtaDTComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Destino")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Destino"))
            Me.TablaC.Item(FTComplemento.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Transaccion")
            Me.TablaC.Item(ImpChComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Cheque")) = True, 0, ds.Tables(0).Rows(j)("Imp_Cheque"))
            Try
                If Trim(ds.Tables(0).Rows(j)("Nom_Banco_Ch")) <> "" Then
                    Fila.Cells(BancoCHComplemento.Index).Value = Me.BancoCHComplemento.Items(Obtener_indexBncoCh(Trim(ds.Tables(0).Rows(j)("Nom_Banco_Ch"))))
                End If
            Catch ex As Exception

            End Try

            Me.TablaC.Item(CtaOchComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Origen_Ch")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Origen_Ch"))
            Me.TablaC.Item(NoChComplemento.Index, j).Value = ds.Tables(0).Rows(j)("No_Cheque")
            Me.TablaC.Item(FCHComplemento.Index, j).Value = ds.Tables(0).Rows(j)("Fecha_Ch")
            Me.TablaC.Item(CtaBancosComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Bancos")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Bancos"))
            Me.TablaC.Item(CtaChequeC.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cuenta_Cheques")) = True, "", ds.Tables(0).Rows(j)("Cuenta_Cheques"))
            Me.TablaC.Item(ProvAComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Provision_Acreedor")) = True, 0, ds.Tables(0).Rows(j)("Provision_Acreedor"))
            Me.TablaC.Item(ProvPComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Provision_Proveedor")) = True, 0, ds.Tables(0).Rows(j)("Provision_Proveedor"))
            Me.TablaC.Item(DifComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Diferencia")) = True, 0, ds.Tables(0).Rows(j)("Diferencia"))
            Try
                If Trim(ds.Tables(0).Rows(j)("Tipo_Poliza")) <> "" Then
                    Fila.Cells(TipPolComplemento.Index).Value = Me.TipPolComplemento.Items(Obtener_indextipo(Trim(ds.Tables(0).Rows(j)("Tipo_Poliza"))))
                End If

            Catch ex As Exception

            End Try
            Me.TablaC.Item(ImpGComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Grabado")) = True, 0, ds.Tables(0).Rows(j)("Imp_Grabado"))
            Me.TablaC.Item(ImpEComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Exento")) = True, 0, ds.Tables(0).Rows(j)("Imp_Exento"))
            Me.TablaC.Item(IvaRComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("IVA_real")) = True, 0, ds.Tables(0).Rows(j)("IVA_real"))
            Me.TablaC.Item(PPAComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Prc_Pago_Acumulado")) = True, 0, ds.Tables(0).Rows(j)("Prc_Pago_Acumulado"))
            Me.TablaC.Item(TRComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Total_Real")) = True, 0, ds.Tables(0).Rows(j)("Total_Real"))
            Me.TablaC.Item(MoneComplemento.Index, j).Value = ds.Tables(0).Rows(j)("Moneda")
            Me.TablaC.Item(UCComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Utilidad_Cambiaria")) = True, 0, ds.Tables(0).Rows(j)("Utilidad_Cambiaria"))
            Me.TablaC.Item(PCComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Perdida_Cambiaria")) = True, 0, ds.Tables(0).Rows(j)("Perdida_Cambiaria"))
            Me.TablaC.Item(ImpDevComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Imp_Devolucion")) = True, 0, ds.Tables(0).Rows(j)("Imp_Devolucion"))
            Me.TablaC.Item(CtaDevComplemento.Index, j).Value = ds.Tables(0).Rows(j)("Cuenta_Devolucion")
            Me.TablaC.Item(NumPolComplemento.Index, j).Value = ds.Tables(0).Rows(j)("numpol")
            Me.TablaC.Item(RISRComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("RIRS")) = True, 0, ds.Tables(0).Rows(j)("RIRS"))
            Me.TablaC.Item(RIVAComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("RIVA")) = True, 0, ds.Tables(0).Rows(j)("RIVA"))
            'Ajutes y Anticipos
            Me.TablaC.Item(AntiComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Anticipos")) = True, 0, ds.Tables(0).Rows(j)("Anticipos"))
            Me.TablaC.Item(CtaAntiComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cta_Ajuste")) = True, "", ds.Tables(0).Rows(j)("Cta_Ajuste"))
            Me.TablaC.Item(AjusComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Ajuste")) = True, 0, ds.Tables(0).Rows(j)("Ajuste"))
            Me.TablaC.Item(CtaAjusComplemento.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cta_Anticipos")) = True, "", ds.Tables(0).Rows(j)("Cta_Anticipos"))

            Try
                If Trim(ds.Tables(0).Rows(j)("Cta_Orden")) <> "" Then
                    Fila.Cells(CtaOrdenC.Index).Value = Me.CtaOrdenC.Items(Obtener_indexCtaOrdenC(Trim(IIf(IsDBNull(ds.Tables(0).Rows(j)("Cta_Orden")) = True, "NO", ds.Tables(0).Rows(j)("Cta_Orden")))))
                End If
            Catch ex As Exception

            End Try

            frm.Barra.Value = j
        Next
        frm.Close()
    End Sub
    Private Sub Cargar_valores_contablesC()
        If Me.TablaC.Rows.Count >= 1 Then
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Text = "Calculando importes Gravados, Exentos e IVA´s de Pagos por favor espere..."
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.TablaC.Rows.Count
            For i As Integer = 0 To Me.TablaC.Rows.Count - 1

                Try


                    Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP, IVARP, ISRRP As Decimal
                    PorcenPro = Me.TablaC.Item(TotComplemento.Index, i).Value / ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, i).Value, "Total")
                    ImpGpro = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, i).Value, "Imp_Grabado") * PorcenPro
                    ImpEpro = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, i).Value, "Imp_Exento") * PorcenPro
                    ImpIvaP = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, i).Value, "IVA_real") * PorcenPro
                    IVARP = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, i).Value, "Retenido_IVA") * PorcenPro
                    ISRRP = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, i).Value, "Retenido_ISR") * PorcenPro
                    Me.TablaC.Item(SubTotComplemento.Index, i).Value = ImpFaCURA(TablaC.Item(UUIDRComplemento.Index, i).Value, "Total") * PorcenPro
                    Me.TablaC.Item(IvaComplemento.Index, i).Value = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, i).Value, "IVA_real") * PorcenPro

                    Me.TablaC.Item(RIVAComplemento.Index, i).Value = IVARP
                    Me.TablaC.Item(RISRComplemento.Index, i).Value = ISRRP
                    If Trim(Me.TablaC.Item(MoneComplemento.Index, i).Value.ToString) <> "USD" Then
                        If Me.TablaC.Item(TotComplemento.Index, i).Value > 0 And Me.TablaC.Item(TotComplemento.Index, i).Value <> Nothing Then
                            'importe gravado
                            Me.TablaC.Item(ImpGComplemento.Index, i).Value = ImpGpro
                            'importe exento
                            Me.TablaC.Item(ImpEComplemento.Index, i).Value = ImpEpro
                            'Iva_real
                            Me.TablaC.Item(IvaRComplemento.Index, i).Value = ImpIvaP
                            'calcula el % Pago Acumulado
                            If Me.TablaC.Item(ImpEComplemento.Index, i).Value < 1 Then
                                Me.TablaC.Item(ImpGComplemento.Index, i).Value = Me.TablaC.Item(ImpGComplemento.Index, i).Value + Me.TablaC.Item(ImpEComplemento.Index, i).Value
                                Me.TablaC.Item(ImpEComplemento.Index, i).Value = 0
                            End If
                            'Total real

                            If Me.TablaC.Item(IvaComplemento.Index, i).Value = 0 Then
                                Me.TablaC.Item(TRComplemento.Index, i).Value = Me.TablaC.Item(TotComplemento.Index, i).Value
                            Else
                                Me.TablaC.Item(TRComplemento.Index, i).Value = Me.TablaC.Item(ImpGComplemento.Index, i).Value + Me.TablaC.Item(ImpEComplemento.Index, i).Value + Me.TablaC.Item(IvaRComplemento.Index, i).Value
                            End If

                        End If
                    Else
                        ' calcular en dolares
                        If Me.TablaC.Item(TotComplemento.Index, i).Value > 0 And Me.TablaC.Item(TotComplemento.Index, i).Value <> Nothing Then
                            'importe gravado
                            Me.TablaC.Item(ImpGComplemento.Index, i).Value = ImpGpro * Calcula_Moneda(Me.TablaC.Item(FechaEComplemento.Index, i).Value.ToString.Substring(0, 10)) / 0.16
                            'importe exento
                            Me.TablaC.Item(ImpEComplemento.Index, i).Value = ImpEpro * Calcula_Moneda(Me.TablaC.Item(FechaEComplemento.Index, i).Value.ToString.Substring(0, 10))
                            'Iva_real
                            Me.TablaC.Item(IvaRComplemento.Index, i).Value = ImpIvaP
                            'calcula el % Pago Acumulado
                            'Total real
                            If Me.TablaC.Item(ImpEComplemento.Index, i).Value < 1 Then
                                Me.TablaC.Item(ImpGComplemento.Index, i).Value = Me.TablaC.Item(ImpGComplemento.Index, i).Value + Me.TablaC.Item(ImpEComplemento.Index, i).Value
                                Me.TablaC.Item(ImpEComplemento.Index, i).Value = 0
                            End If
                            If Me.TablaC.Item(IvaComplemento.Index, i).Value = 0 Then
                                Me.TablaC.Item(TRComplemento.Index, i).Value = Me.TablaC.Item(TotComplemento.Index, i).Value
                            Else
                                Me.TablaC.Item(TRComplemento.Index, i).Value = Me.TablaC.Item(ImpGComplemento.Index, i).Value + Me.TablaC.Item(ImpEComplemento.Index, i).Value + Me.TablaC.Item(IvaRComplemento.Index, i).Value
                            End If
                        End If

                    End If
                Catch ex As Exception

                    Me.TablaC.Rows(i).DefaultCellStyle.BackColor = Color.Red


                End Try
                frm.Barra.Value = i
            Next
            frm.Close()
        End If
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
            ElseIf Column.Index = NomCtaComplemento.Index Or Column.Index = CtaBancosComplemento.Index Or Column.Index = CtaChequec.Index Then
                Column.DefaultCellStyle.BackColor = Color.Orange
            ElseIf Column.Index = ImpEfComplemento.Index Or Column.Index = ImpTComplemento.Index Or Column.Index = ImpChComplemento.Index Then
                Column.DefaultCellStyle.BackColor = Color.OliveDrab
            End If
            frm.Barra.Value += 1
        Next
        frm.Close()
    End Sub
    Private Sub CmdProcesaComple_Click(sender As Object, e As EventArgs) Handles CmdProcesaComple.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaC.Rows.Count > 0 Then
            If Me.lstCliente.SelectText <> "" Then
                If Verifica_catalogo_cliente(Me.lstCliente.SelectItem) = True Then
                    Guardar_Complementos()
                    If RadMessageBox.Show("La empresa " & Me.lstCliente.SelectText & " es correcta?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        Me.BarraComple.Maximum = Me.TablaC.RowCount - 1
                        Me.BarraComple.Minimum = 0
                        Me.BarraComple.Value1 = 0

                        For p As Integer = 0 To Me.TablaC.RowCount - 1
                            If Me.TablaC.Item(ApliComplemento.Index, p).Value = True Then ' se paso todos los filtros de creacion
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
                    RadMessageBox.Show("No existe Catalogo de cuentas para: " & Me.lstCliente.SelectText & "", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                End If
                Me.CmdBuscarComple.PerformClick()
            Else
                RadMessageBox.Show("No se ha seleccionado un cliente", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If



        Else
            RadMessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
    Private Sub Guardar_Complementos()
        If Me.TablaC.Rows.Count >= 1 Then
            For i As Integer = 0 To Me.TablaC.Rows.Count - 1

                Guardar_Cmpl(IIf(IsDBNull(Me.TablaC.Item(LetraSatComplemento.Index, i).Value), "", Me.TablaC.Item(LetraSatComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(NomCtaComplemento.Index, i).Value), "", Me.TablaC.Item(NomCtaComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(LetraContabilidadComplemento.Index, i).Value), "", Me.TablaC.Item(LetraContabilidadComplemento.Index, i).Value),
                           Me.TablaC.Item(AnioComplemento.Index, i).Value, Me.TablaC.Item(MesComplemento.Index, i).Value, Me.TablaC.Item(ImpEfComplemento.Index, i).Value, IIf(IsDBNull(Me.TablaC.Item(CtaEfComplemento.Index, i).Value), "", Me.TablaC.Item(CtaEfComplemento.Index, i).Value),
                           Me.TablaC.Item(ImpTComplemento.Index, i).Value, IIf(IsDBNull(Me.TablaC.Item(BancoOrigenTComplemento.Index, i).Value), "", Me.TablaC.Item(BancoOrigenTComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(CtaOTComplemento.Index, i).Value), "", Me.TablaC.Item(CtaOTComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(BancoDtComplemento.Index, i).Value), "", Me.TablaC.Item(BancoDtComplemento.Index, i).Value),
                           IIf(IsDBNull(Me.TablaC.Item(FTComplemento.Index, i).Value), "", Me.TablaC.Item(FTComplemento.Index, i).Value), Me.TablaC.Item(ImpChComplemento.Index, i).Value, IIf(IsDBNull(Me.TablaC.Item(BancoCHComplemento.Index, i).Value), "", Me.TablaC.Item(BancoCHComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(CtaOchComplemento.Index, i).Value), "", Me.TablaC.Item(CtaOchComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(NoChComplemento.Index, i).Value), "", Me.TablaC.Item(NoChComplemento.Index, i).Value),
                          IIf(IsDBNull(Me.TablaC.Item(FCHComplemento.Index, i).Value), "", Me.TablaC.Item(FCHComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(CtaBancosComplemento.Index, i).Value), "", Me.TablaC.Item(CtaBancosComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(ProvAComplemento.Index, i).Value), 0, Me.TablaC.Item(ProvAComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(ProvPComplemento.Index, i).Value), 0, Me.TablaC.Item(ProvPComplemento.Index, i).Value),
                           IIf(IsDBNull(Me.TablaC.Item(DifComplemento.Index, i).Value), 0, Me.TablaC.Item(DifComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(TipPolComplemento.Index, i).Value), "", Me.TablaC.Item(TipPolComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(ImpGComplemento.Index, i).Value), 0, Me.TablaC.Item(ImpGComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(ImpEComplemento.Index, i).Value), 0, Me.TablaC.Item(ImpEComplemento.Index, i).Value),
                          IIf(IsDBNull(Me.TablaC.Item(IvaRComplemento.Index, i).Value), 0, Me.TablaC.Item(IvaRComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(PPAComplemento.Index, i).Value), 0, Me.TablaC.Item(PPAComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(TRComplemento.Index, i).Value), 0, Me.TablaC.Item(TRComplemento.Index, i).Value), Me.TablaC.Item(UCComplemento.Index, i).Value, IIf(IsDBNull(Me.TablaC.Item(PCComplemento.Index, i).Value), 0, Me.TablaC.Item(PCComplemento.Index, i).Value),
                           Me.TablaC.Item(ImpDevComplemento.Index, i).Value, IIf(IsDBNull(Me.TablaC.Item(CtaDevComplemento.Index, i).Value), "", Me.TablaC.Item(CtaDevComplemento.Index, i).Value), Me.TablaC.Item(Id_Complemento.Index, i).Value, IIf(IsDBNull(Me.TablaC.Item(NumPolComplemento.Index, i).Value), "", Me.TablaC.Item(NumPolComplemento.Index, i).Value), Me.TablaC.Item(RISRComplemento.Index, i).Value, Me.TablaC.Item(RIVAComplemento.Index, i).Value, IIf(IsDBNull(Me.TablaC.Item(CtaDTComplemento.Index, i).Value), "", Me.TablaC.Item(CtaDTComplemento.Index, i).Value),
                           IIf(IsDBNull(Me.TablaC.Item(AjusComplemento.Index, i).Value), 0, Me.TablaC.Item(AjusComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(CtaAjusComplemento.Index, i).Value), "", Me.TablaC.Item(CtaAjusComplemento.Index, i).Value),
                           IIf(IsDBNull(Me.TablaC.Item(AntiComplemento.Index, i).Value), 0, Me.TablaC.Item(AntiComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(CtaAntiComplemento.Index, i).Value), "", Me.TablaC.Item(CtaAntiComplemento.Index, i).Value),
                           IIf(IsDBNull(Me.TablaC.Item(ImpProviComplemento.Index, i).Value), 0, Me.TablaC.Item(ImpProviComplemento.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(CtaOrdenC.Index, i).Value), "", Me.TablaC.Item(CtaOrdenC.Index, i).Value), IIf(IsDBNull(Me.TablaC.Item(CtaChequeC.Index, i).Value), "", Me.TablaC.Item(CtaChequeC.Index, i).Value))
            Next
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Guardado Completado ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub

    Private Sub Guardar_Cmpl(ByVal Letra_Sat As String, ByVal Nombre_cuenta As String, ByVal Clave As String,
                            ByVal Anio_Contable As String, ByVal Mes_Contable As String, ByVal Imp_Efectivo As Decimal, ByVal Cuenta_Efectivo As String,
                            ByVal Imp_Transf As Decimal, ByVal Banco_Origen As String, ByVal Cuenta_Origen As String, ByVal Banco_Destino As String,
                            ByVal Fecha_Transaccion As String, ByVal Imp_Cheque As Decimal, ByVal Nom_Banco_Ch As String, ByVal Cuenta_Origen_Ch As String, ByVal No_Cheque As String,
                            ByVal Fecha_Ch As String, ByVal Cuenta_Bancos As String, ByVal Provision_Acreedor As Decimal, ByVal Provision_Proveedor As Decimal,
                            ByVal Diferencia As Decimal, ByVal Tipo_Poliza As String, ByVal Imp_Grabado As Decimal, ByVal Imp_Exento As Decimal,
                            ByVal IVA_real As Decimal, ByVal Prc_Pago_Acumulado As Decimal, ByVal Total_Real As Decimal, ByVal Utilidad_Cambiaria As Decimal, ByVal Perdida_Cambiaria As Decimal,
                            ByVal Imp_Devolucion As Decimal, ByVal Cuenta_Devolucion As String, ByVal Id_Registro_Xml As Integer, ByVal numpol As String, ByVal risr As Decimal, ByVal riva As Decimal,
                            ByVal ctadestino As String, ByVal ajus As Decimal, ByVal ctaajus As String, ByVal anti As Decimal, ByVal ctaanti As String, ByVal ImpProvis As Decimal, ByVal CtaOrden As String, ByVal Cuenta_Cheques As String)
        Dim sql As String = "UPDATE dbo.Xml_Complemento
                SET 		Letra_Sat = '" & Letra_Sat & "',
                	Nombre_cuenta = '" & Nombre_cuenta & "',
                	Clave = '" & Clave & "',
                	Anio_Contable = '" & Anio_Contable & "',
                	Mes_Contable = '" & Mes_Contable & "',
                	Imp_Efectivo = " & Imp_Efectivo & ",
                	Cuenta_Efectivo = '" & Cuenta_Efectivo & "',
                	Imp_Transf = " & Imp_Transf & ",
                	Banco_Origen = '" & Banco_Origen & "',
                	Cuenta_Origen = '" & Cuenta_Origen & "',
                	Banco_Destino = '" & Banco_Destino & "',
                	Fecha_Transaccion = " & Eventos.Sql_hoy(Fecha_Transaccion) & ",
                	Imp_Cheque =" & Imp_Cheque & ",
                	Nom_Banco_Ch = '" & Nom_Banco_Ch & "',
                	Cuenta_Origen_Ch = '" & Cuenta_Origen_Ch & "',
                	No_Cheque = '" & No_Cheque & "',
                	Fecha_Ch = " & Eventos.Sql_hoy(Fecha_Ch) & ",
                	Cuenta_Bancos = '" & Cuenta_Bancos & "',
                	Provision_Acreedor = " & Provision_Acreedor & ",
                	Provision_Proveedor = " & Provision_Proveedor & ",
                	Diferencia = " & Diferencia & ",
                	Tipo_Poliza = '" & Tipo_Poliza & "',
                	Imp_Grabado =  " & Imp_Grabado & ",
                	Imp_Exento =  " & Imp_Exento & ",
                	IVA_real =  " & IVA_real & ",
                	Prc_Pago_Acumulado =  " & Prc_Pago_Acumulado & ",
                	Total_Real =  " & Total_Real & ",
                	Utilidad_Cambiaria =  " & Utilidad_Cambiaria & ",
                	Perdida_Cambiaria =  " & Perdida_Cambiaria & ",
                	Imp_Devolucion =  " & Imp_Devolucion & ",
                	Cuenta_Devolucion = '" & Cuenta_Devolucion & "', 
                    Numpol= '" & Trim(numpol) & "', 
                     RIRS= " & risr & ", 
                     RIVA= " & riva & ", 
                     Cuenta_Destino= '" & ctadestino & "',
                     Ajuste= " & ajus & ", 
                     Anticipos= " & anti & ",  
                     Cta_Ajuste= '" & ctaajus & "', 
                     Cta_Anticipos= '" & ctaanti & "',
                     Cta_Orden= '" & CtaOrden & "', Cuenta_Cheques = '" & Cuenta_Cheques & "'

	 
                WHERE Id_Xml_Complemento = " & Id_Registro_Xml & ""
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("GuardaXMLC", sql)
        End If
    End Sub

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
        If Me.TablaC.Item(ImpTComplemento.Index, posicion).Value > 0 Then
            dia = Me.TablaC.Item(FTComplemento.Index, posicion).Value.ToString.Substring(0, 2)
            Chk = ""
        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, posicion).Value > 0 Then
            dia = Me.TablaC.Item(FechaEComplemento.Index, posicion).Value.ToString.Substring(0, 2)
            Chk = ""
        ElseIf Me.TablaC.Item(ImpChComplemento.Index, posicion).Value > 0 Then
            dia = Me.TablaC.Item(FCHComplemento.Index, posicion).Value.ToString.Substring(0, 2)
            Chk = "Ch/ " & Me.TablaC.Item(NoChComplemento.Index, posicion).Value.ToString() & " F/ "
        Else
            dia = Me.TablaC.Item(FechaEComplemento.Index, posicion).Value.ToString.Substring(0, 2)
        End If
        Dim leyenda As String = ""
        If Chk = "" Then
            leyenda = "Pago S/Fact" & " " & Trim(Me.TablaC.Item(UUIDComplemento.Index, posicion).Value)
        Else
            leyenda = Chk
        End If

        ' CREAR iF para calcular el dia
        If Creapoliza(poliza_Sistema, Me.TablaC.Item(AnioComplemento.Index, posicion).Value, Me.TablaC.Item(MesComplemento.Index, posicion).Value, dia,
                   consecutivo, Checa_tipo(Me.TablaC.Item(TipPolComplemento.Index, posicion).Value, Me.lstCliente.SelectItem),
                   Me.TablaC.Item(FechaEComplemento.Index, posicion).Value, leyenda, "Carga", Me.TablaC.Item(NumPolComplemento.Index, posicion).Value, Me.TablaC.Item(Id_Complemento.Index, posicion).Value, True) = True Then



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
            CrearDetalleComplementoNuevo(posicion, poliza_Sistema)
        End If
    End Sub

    Private Sub CrearDetalleComplementoNuevo(ByVal Fila As Integer, ByVal Poliza As String)
        Dim Item As Integer = 1
        Dim Cuenta(6)
        Dim CuentaPPD(11)
        Dim CuentaCO(7)
        Dim CuentaRetenciones(8)
        Dim Cadena()

        Dim Nivel() As String = Split(Me.TablaC.Item(NomCtaComplemento.Index, Fila).Value.ToString.Trim(), "-")
        If Nivel(1).Substring(12, 4) = "0000" And Nivel(1).Substring(8, 4) > "0000" Then ' Segundo nivel
            Nivel(1) = Nivel(1).Substring(4, 4)
        ElseIf Nivel(1).Substring(12, 4) > "0000" And Nivel(1).Substring(8, 4) > "0000" Then 'Tercer Nivel
            Nivel(1) = Nivel(1).Substring(8, 4)
        ElseIf Nivel(1).Substring(4, 4) > "0000" And Nivel(1).Substring(8, 4) = "0000" And Nivel(1).Substring(12, 4) = "0000" Then 'Primer Nivel
            Nivel(1) = Nivel(1).Substring(0, 4)
        End If

        Cuenta = CuentasPue(LetraFactura(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value))
        CuentaPPD = CuentasPPD(LetraFactura(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value))
        CuentaRetenciones = CuentasRet(LetraFactura(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value))
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

        Dim Tas As String = TasaComplemeto(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value)
        Dim PorcenPro, ImpGpro, ImpEpro, ImpIvaP, RIVAC, RISRC As Decimal
        PorcenPro = Me.TablaC.Item(SubTotComplemento.Index, Fila).Value / ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value, "Total")
        ImpGpro = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value, "Imp_Grabado") * PorcenPro
        ImpEpro = ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value, "Imp_Exento") * PorcenPro
        ImpIvaP = (ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value, "IVA_real")) * PorcenPro
        RIVAC = (ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value, "Retenido_Iva")) * PorcenPro
        RISRC = (ImpFaCURA(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value, "Retenido_ISR")) * PorcenPro
        CuentaPPD = CuentasPPD(LetraFactura(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value))
        If ImpEpro > 0 Then
            Crea_detalle_poliza(Poliza, Item, ImpEpro, 0, RegresaCuentaComple(CuentaPPD(9), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, NivelED, CuentaPPD(10)), "")
            Item += 1
        End If
        If ImpGpro + ImpIvaP > 0 Then
            Crea_detalle_poliza(Poliza, Item, (ImpGpro + ImpIvaP) - (RIVAC + RISRC), 0, RegresaCuentaComple(CuentaPPD(6), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, NivelGAD, CuentaPPD(7)), "")
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
        If Me.TablaC.Item(ImpEfComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpTComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpChComplemento.Index, Fila).Value > 0 Then 'TODOS

            Cadena = Split(Me.TablaC.Item(CtaEfComplemento.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpEfComplemento.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpTComplemento.Index, Fila).Value, Me.TablaC.Item(CtaBancosComplemento.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpChComplemento.Index, Fila).Value, Me.TablaC.Item(CtaChequeC.Index, Fila).Value.ToString().Trim(), "")
            Item += 1

        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpTComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpChComplemento.Index, Fila).Value = 0 Then 'Efectivo y Transferencia
            Cadena = Split(Me.TablaC.Item(CtaEfComplemento.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpEfComplemento.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpTComplemento.Index, Fila).Value, Me.TablaC.Item(CtaBancosComplemento.Index, Fila).Value.ToString().Trim(), "")
            Item += 1


        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, Fila).Value = 0 And Me.TablaC.Item(ImpTComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpChComplemento.Index, Fila).Value > 0 Then 'Cheque y Transferencia

            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpTComplemento.Index, Fila).Value, Me.TablaC.Item(CtaBancosComplemento.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpChComplemento.Index, Fila).Value, Me.TablaC.Item(CtaChequeC.Index, Fila).Value.ToString().Trim(), "")
            Item += 1

        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpTComplemento.Index, Fila).Value = 0 And Me.TablaC.Item(ImpChComplemento.Index, Fila).Value > 0 Then 'Cheque y Efecivo
            Cadena = Split(Me.TablaC.Item(CtaEfComplemento.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpEfComplemento.Index, Fila).Value, Cadena(1), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpChComplemento.Index, Fila).Value, Me.TablaC.Item(CtaChequeC.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpTComplemento.Index, Fila).Value = 0 And Me.TablaC.Item(ImpChComplemento.Index, Fila).Value = 0 Then ' Efecivo
            Cadena = Split(Me.TablaC.Item(CtaEfComplemento.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpEfComplemento.Index, Fila).Value, Cadena(1), "")
            Item += 1

        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, Fila).Value = 0 And Me.TablaC.Item(ImpTComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpChComplemento.Index, Fila).Value = 0 Then ' Transferencia
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpTComplemento.Index, Fila).Value, Me.TablaC.Item(CtaBancosComplemento.Index, Fila).Value.ToString().Trim(), "")
            Item += 1

        ElseIf Me.TablaC.Item(ImpEfComplemento.Index, Fila).Value = 0 And Me.TablaC.Item(ImpTComplemento.Index, Fila).Value = 0 And Me.TablaC.Item(ImpChComplemento.Index, Fila).Value > 0 Then 'Cheque 
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpChComplemento.Index, Fila).Value, Me.TablaC.Item(CtaChequeC.Index, Fila).Value.ToString().Trim(), "")
            Item += 1
        End If

        If Me.TablaC.Item(AntiComplemento.Index, Fila).Value > 0 Then ' Se verifica si cuenta con anticipos +
            If Tas <> "" Then
                Cuenta = CuentasAnticiposPue(LetraFactura(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value))
                Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(AntiComplemento.Index, Fila).Value, 0, RegresaCuentaCompleNuevo(Cuenta(0), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(1)), "")
                Item += 1
            Else
                Cuenta = CuentasAnticiposPue(LetraFactura(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value))
                Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(AntiComplemento.Index, Fila).Value, 0, RegresaCuentaCompleNuevo(Cuenta(2), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(3)), "")
                Item += 1
            End If

        ElseIf Me.TablaC.Item(AntiComplemento.Index, Fila).Value < 0 Then ' Se verifica si cuenta  anticipos -
            If Tas <> "" Then
                Cuenta = CuentasAnticiposPue(LetraFactura(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value))
                Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(AntiComplemento.Index, Fila).Value * -1, 0, RegresaCuentaCompleNuevo(Cuenta(0), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(1)), "")
                Item += 1
            Else
                Cuenta = CuentasAnticiposPue(LetraFactura(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value))
                Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(AntiComplemento.Index, Fila).Value * -1, 0, RegresaCuentaCompleNuevo(Cuenta(2), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(3)), "")
                Item += 1
            End If
        End If
        'Ajustes
        If Me.TablaC.Item(AjusComplemento.Index, Fila).Value > 0 Then ' Se verifica si cuenta con ajuste +
            Cadena = Split(Me.TablaC.Item(CtaAjusComplemento.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(AjusComplemento.Index, Fila).Value, Cadena(0), "")
            Item += 1
        ElseIf Me.TablaC.Item(AjusComplemento.Index, Fila).Value < 0 Then ' Se verifica si cuenta con ajuste -
            Cadena = Split(Me.TablaC.Item(CtaAjusComplemento.Index, Fila).Value.ToString().Trim(), " - ")
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(AjusComplemento.Index, Fila).Value * -1, Cadena(0), "")
            Item += 1
        End If
        If Me.TablaC.Item(PCComplemento.Index, Fila).Value > 0 Then ' Se perdida cambiaria
            Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(PCComplemento.Index, Fila).Value, 0, "7010000100000000", "")
            Item += 1
        End If
        If Me.TablaC.Item(UCComplemento.Index, Fila).Value > 0 Then ' Se utilidad cambiaria
            Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(UCComplemento.Index, Fila).Value, "7020000100000000", "")
            Item += 1
        End If


        If ImpEpro > 0 And ImpGpro > 0 Then ' tiene grabado y exento
            Crea_detalle_poliza(Poliza, Item, ImpGpro, 0, RegresaCuentaComple(Cuenta(0), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, NivelG, Cuenta(1)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, ImpEpro, 0, RegresaCuentaComple(Cuenta(2), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, NivelE, Cuenta(3)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, ImpIvaP - RIVAC, 0, Cuenta(4), "")
            Item += 1

            If RIVAC > 0 Then
                Crea_detalle_poliza(Poliza, Item, RIVAC, 0, CuentaRetenciones(8), "")
                Item += 1
            End If
            Crea_detalle_poliza(Poliza, Item, 0, ImpGpro, RegresaCuentaComple(CuentaPPD(0), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, NivelGp, CuentaPPD(1)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, ImpEpro, RegresaCuentaComple(CuentaPPD(2), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, NivelEp, CuentaPPD(3)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, ImpIvaP, CuentaPPD(4), "")
            Item += 1

            If RIVAC > 0 Then
                Crea_detalle_poliza(Poliza, Item, 0, RIVAC, CuentaRetenciones(2), "")
                Item += 1
            End If

        ElseIf ImpEpro > 0 And ImpGpro <= 0 Then 'Tiene Exento
            Crea_detalle_poliza(Poliza, Item, ImpEpro, 0, RegresaCuentaComple(Cuenta(2), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, NivelE, Cuenta(3)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, 0, ImpEpro, RegresaCuentaComple(CuentaPPD(2), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, NivelEp, CuentaPPD(3)), "")
            Item += 1
        ElseIf ImpEpro <= 0 And ImpGpro > 0 Then 'Tiene Grabado
            Crea_detalle_poliza(Poliza, Item, ImpGpro, 0, RegresaCuentaComple(Cuenta(0), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, NivelG, Cuenta(1)), "")
            Item += 1
            Crea_detalle_poliza(Poliza, Item, ImpIvaP - RIVAC, 0, Cuenta(4), "")
            Item += 1
            If RIVAC > 0 Then
                Crea_detalle_poliza(Poliza, Item, RIVAC, 0, CuentaRetenciones(8), "")
                Item += 1
            End If
            Crea_detalle_poliza(Poliza, Item, 0, ImpGpro, RegresaCuentaComple(CuentaPPD(0), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, NivelGp, CuentaPPD(1)), "")
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
        If UCase(Me.TablaC.Item(CtaOrdenC.Index, Fila).Value) = "SI" Then
            CuentaCO = CuentasCO(LetraFactura(Me.TablaC.Item(UUIDRComplemento.Index, Fila).Value))
            If Len(Me.TablaC.Item(RFCComplemento.Index, Fila).Value) = 12 Then ' Moral
                If Me.TablaC.Item(ImpEComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, Fila).Value > 0 Then
                    Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(ImpGComplemento.Index, Fila).Value, 0, CuentaCO(2), "")
                    Item += 1
                    Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(ImpEComplemento.Index, Fila).Value, 0, CuentaCO(6), "")
                    Item += 1
                ElseIf Me.TablaC.Item(ImpEComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, Fila).Value <= 0 Then 'Tiene Exento
                    Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(ImpEComplemento.Index, Fila).Value, 0, CuentaCO(6), "")
                    Item += 1
                ElseIf Me.TablaC.Item(ImpEComplemento.Index, Fila).Value <= 0 And Me.TablaC.Item(ImpGComplemento.Index, Fila).Value > 0 Then 'Tiene Grabado
                    Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(ImpGComplemento.Index, Fila).Value, 0, CuentaCO(2), "")
                    Item += 1
                End If
            Else
                If Me.TablaC.Item(ImpEComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, Fila).Value > 0 Then
                    Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(ImpGComplemento.Index, Fila).Value, 0, CuentaCO(2), "")
                    Item += 1
                    Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(ImpEComplemento.Index, Fila).Value, 0, CuentaCO(6), "")
                    Item += 1
                ElseIf Me.TablaC.Item(ImpEComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, Fila).Value <= 0 Then 'Tiene Exento
                    Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(ImpEComplemento.Index, Fila).Value, 0, CuentaCO(6), "")
                    Item += 1
                ElseIf Me.TablaC.Item(ImpEComplemento.Index, Fila).Value <= 0 And Me.TablaC.Item(ImpGComplemento.Index, Fila).Value > 0 Then 'Tiene Grabado
                    Crea_detalle_poliza(Poliza, Item, Me.TablaC.Item(ImpGComplemento.Index, Fila).Value, 0, CuentaCO(2), "")
                    Item += 1
                End If

            End If

            If Me.TablaC.Item(ImpEComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, Fila).Value > 0 Then
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpGComplemento.Index, Fila).Value, RegresaCuentaCompleNuevo(CuentaCO(2), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(3)), "")
                Item += 1
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpEComplemento.Index, Fila).Value, RegresaCuentaCompleNuevo(CuentaCO(6), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(7)), "")
                Item += 1
            ElseIf Me.TablaC.Item(ImpEComplemento.Index, Fila).Value > 0 And Me.TablaC.Item(ImpGComplemento.Index, Fila).Value <= 0 Then 'Tiene Exento
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpEComplemento.Index, Fila).Value, RegresaCuentaCompleNuevo(CuentaCO(6), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(7)), "")
                Item += 1
            ElseIf Me.TablaC.Item(ImpEComplemento.Index, Fila).Value <= 0 And Me.TablaC.Item(ImpGComplemento.Index, Fila).Value > 0 Then 'Tiene Grabado
                Crea_detalle_poliza(Poliza, Item, 0, Me.TablaC.Item(ImpGComplemento.Index, Fila).Value, RegresaCuentaCompleNuevo(CuentaCO(2), Trim(Me.TablaC.Item(RFCComplemento.Index, Fila).Value), Fila, Cuenta(3)), "")
                Item += 1
            End If



        End If


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
        If Me.TablaC.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In TablaC.Rows
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
                            'If Obtener_indexB(Me.LstComple.SelectText) = 0 Then
                            '    Fila.Cells(BancoOrigenTComplemento.Index).Value = ""
                            'Else
                            '    Dim s As String = Me.BancoOrigenTComplemento.Items(1)
                            '    Fila.Cells(BancoOrigenTComplemento.Index).Value = Me.BancoOrigenTComplemento.Items(Obtener_indexbancoO(Me.LstComple.SelectText))
                            'End If
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
                    'Try
                    '    Dim s As String = Me.BancoDtComplemento.Items(1)
                    '    Fila.Cells(BancoDtComplemento.Index).Value = Me.BancoDtComplemento.Items(Obtener_indexBancodes(Me.LstComple.SelectText))
                    'Catch ex As Exception

                    'End Try
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
                    For j As Integer = 0 To Me.TablaC.Columns.Count - 1
                        If Me.TablaC.Item(j, Fila.Index).Selected = True And (j <> LetraContabilidadComplemento.Index And j <> BancoDtComplemento.Index And j <> TipPolComplemento.Index) Then
                            Me.TablaC.Item(j, Fila.Index).Value = Me.LstComple.SelectText
                        ElseIf Me.TablaC.Item(j, Fila.Index).Selected = True And (j = LetraContabilidadComplemento.Index And j = BancoDtComplemento.Index And j = TipPolComplemento.Index) Then
                            Me.TablaC.Item(j, Fila.Index).Value = ""
                        End If
                    Next

                End If
                ComplemantosL(Fila.Index)
            Next
        End If
    End Sub
    Private Sub TablaC_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaC.CellClick

        Dim columna As Integer = Me.TablaC.CurrentCell.ColumnIndex
        Dim Nombre As String
        Nombre = Me.TablaC.Columns.Item(Me.TablaC.CurrentCell.ColumnIndex).Name.ToString
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
    Private Sub TablaC_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaC.CellEndEdit
        ComplemantosL(Me.TablaC.CurrentRow.Index)
    End Sub

    Private Sub ComplemantosL(ByVal I As Integer)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        'Calcula cuenta cargos
        Try

            If Me.TablaC.Item(ProvPComplemento.Index, I).Value > 0 Then
                Me.TablaC.Item(ProvAComplemento.Index, I).Value = 0
            ElseIf Me.TablaC.Item(ProvAComplemento.Index, I).Value > 0 Then
                Me.TablaC.Item(ProvPComplemento.Index, I).Value = 0
            End If

            If Me.TablaC.Item(RefFact.Index, I).Value <> Nothing Then

                Dim Nueva As String = CuentaTipoC(Me.TablaC.Item(RFCComplemento.Index, I).Value, Trim(Me.TablaC.Item(RefFact.Index, I).Value), I)

                Me.TablaC.Item(NomCtaComplemento.Index, I).Value = Nueva
                Me.TablaC.Item(LetraSatComplemento.Index, I).Value = Eventos.Calcula_letraSat(Me.TablaC.Item(FpagoComplemento.Index, I).Value, Me.TablaC.Item(UCFDIComplemento.Index, I).Value)


            Else
                Me.TablaC.Item(NomCtaComplemento.Index, I).Value = ""
            End If

            'CALCULAR CUENTA DE EFECTIVO
            If Me.TablaC.Item(ImpEfComplemento.Index, I).Value > 0 Then
                If Candado_Importe_Efectivocomple(I) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo

                    Me.TablaC.Item(CtaEfComplemento.Index, I).Value = Cuenta_Efectivo()
                    Me.TablaC.Columns(CtaEfComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    If RadMessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaC.Item(CtaEfComplemento.Index, I).Value = Cuenta_Efectivo()
                        Me.TablaC.Columns(CtaEfComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaC.Item(ImpEfComplemento.Index, I).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaC.Item(CtaEfComplemento.Index, I).Value = ""
            End If

            'CALCULAR CUENTA DE Ajustes
            If Me.TablaC.Item(AjusComplemento.Index, I).Value <> 0 Then
                If Candado_Importe_Efectivocomple(I) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo

                    Me.TablaC.Item(CtaAjusComplemento.Index, I).Value = Cuenta_Efectivo()
                    Me.TablaC.Columns(CtaAjusComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    If RadMessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaC.Item(CtaAjusComplemento.Index, I).Value = Cuenta_Efectivo()
                        Me.TablaC.Columns(CtaAjusComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaC.Item(AjusComplemento.Index, I).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaC.Item(CtaAjusComplemento.Index, I).Value = ""
            End If
            'CALCULAR CUENTA DE anticipos
            If Me.TablaC.Item(AntiComplemento.Index, I).Value > 0 Then

                Me.TablaC.Item(CtaAntiComplemento.Index, I).Value = Cuenta_Anticipo()
                Me.TablaC.Columns(CtaAntiComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Else
                Me.TablaC.Item(CtaAntiComplemento.Index, I).Value = ""
            End If

            ' Importe de Transferencia
            If Me.TablaC.Item(ImpTComplemento.Index, I).Value > 0 Then
                If Trim(Me.TablaC.Item(BancoOrigenTComplemento.Index, I).Value) <> Nothing Then
                    If Trim(Me.TablaC.Item(CtaOTComplemento.Index, I).Value) <> "" Then
                    Else
                        Dim posi As Integer = InStr(1, Me.TablaC.Item(BancoOrigenTComplemento.Index, I).Value, "-", CompareMethod.Binary)
                        Dim cuantos As Integer = Len(Me.TablaC.Item(BancoOrigenTComplemento.Index, I).Value) - Len(Me.TablaC.Item(BancoOrigenTComplemento.Index, I).Value.Substring(0, posi))
                        Dim Al As String = Me.TablaC.Item(BancoOrigenTComplemento.Index, I).Value.Substring(posi, cuantos)
                        Me.TablaC.Item(CtaOTComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)

                        Me.TablaC.Item(CtaBancosComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                    End If

                End If

                'Empieza el nuevo codigo Bnaco destino
                If Trim(Me.TablaC.Item(BancoDtComplemento.Index, I).Value) <> Nothing Then
                    Try
                        If Trim(Me.TablaC.Item(CtaDTComplemento.Index, I).Value) <> "" And Trim(Me.TablaC.Item(CtaDTComplemento.Index, I).Value) <> "0" Then
                            Dim largo As Integer = Len(Me.TablaC.Item(BancoDtComplemento.Index, I).Value)
                            Dim cadena As String = Trim(Me.TablaC.Item(BancoDtComplemento.Index, I).Value.ToString.Substring(largo - 3, 3))
                            largo = Len(Me.TablaC.Item(CtaDTComplemento.Index, I).Value)
                            Dim Al As String = Me.TablaC.Item(CtaDTComplemento.Index, I).Value.Substring(largo - 3, 3)
                            If cadena <> Al Then
                                largo = InStr(1, Me.TablaC.Item(BancoDtComplemento.Index, I).Value, "-", CompareMethod.Binary)
                                Al = Me.TablaC.Item(BancoDtComplemento.Index, I).Value.Substring(0, largo - 1)
                                Me.TablaC.Item(CtaDTComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, I).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                            End If
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaC.Item(BancoDtComplemento.Index, I).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaC.Item(BancoDtComplemento.Index, I).Value.Substring(0, posi - 1)
                            Me.TablaC.Item(CtaDTComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, I).Value & "'", True)
                        End If
                    Catch ex As Exception

                    End Try
                Else
                    Me.TablaC.Item(BancoDtComplemento.Index, I).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaC.Item(RFCComplemento.Index, I).Value)
                    Try
                        If Trim(Me.TablaC.Item(CtaDTComplemento.Index, I).Value) <> "" And Trim(Me.TablaC.Item(CtaDTComplemento.Index, I).Value) <> "0" Then
                            Dim largo As Integer = Len(Me.TablaC.Item(BancoDtComplemento.Index, I).Value)
                            Dim cadena As String = Trim(Me.TablaC.Item(BancoDtComplemento.Index, I).Value.ToString.Substring(largo - 3, 3))
                            largo = Len(Me.TablaC.Item(CtaDTComplemento.Index, I).Value)
                            Dim Al As String = Me.TablaC.Item(CtaDTComplemento.Index, I).Value.Substring(largo - 3, 3)
                            If cadena <> Al Then
                                largo = InStr(1, Me.TablaC.Item(BancoDtComplemento.Index, I).Value, "-", CompareMethod.Binary)
                                Al = Me.TablaC.Item(BancoDtComplemento.Index, I).Value.Substring(0, largo - 1)
                                Me.TablaC.Item(CtaDTComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, I).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                            End If
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaC.Item(BancoDtComplemento.Index, I).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaC.Item(BancoDtComplemento.Index, I).Value.Substring(0, posi - 1)
                            Me.TablaC.Item(CtaDTComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, I).Value & "'", True)
                        End If
                    Catch ex As Exception

                    End Try
                End If
                If Trim(Me.TablaC.Item(TipPolComplemento.Index, I).Value) <> Nothing Then
                    If Trim(Me.TablaC.Item(BancoOrigenTComplemento.Index, I).Value) <> Nothing Then
                    Else
                        Dim fila As DataGridViewRow = Me.TablaC.Rows(I)
                        Try
                            If Trim(Me.TablaC.Item(TipPolComplemento.Index, I).Value) <> "" Then
                                Dim largo As Integer = Len(Me.TablaC.Item(TipPolComplemento.Index, I).Value)
                                Dim posicion As Integer = InStr(1, Me.TablaC.Item(TipPolComplemento.Index, I).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaC.Item(TipPolComplemento.Index, I).Value.Substring(posicion + 1, largo - posicion - 1)
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
                    Me.TablaC.Columns(BancoOrigenTComplemento.Index).Visible = False
                    Me.TablaC.Columns(CtaOTComplemento.Index).Visible = False
                    Me.TablaC.Columns(BancoDtComplemento.Index).Visible = False
                    Me.TablaC.Columns(CtaDTComplemento.Index).Visible = False
                    Me.TablaC.Columns(FTComplemento.Index).Visible = True

                End If


            End If
            If Me.TablaC.Item(ImpChComplemento.Index, I).Value > 0 Then
                'Cheques

                If Trim(Me.TablaC.Item(BancoCHComplemento.Index, I).Value) <> "" Then
                    If Trim(Me.TablaC.Item(CtaOchComplemento.Index, I).Value) <> "" Then
                    Else
                        Dim posi As Integer = InStr(1, Me.TablaC.Item(BancoCHComplemento.Index, I).Value, "-", CompareMethod.Binary)
                        Dim cuantos As Integer = Len(Me.TablaC.Item(BancoCHComplemento.Index, I).Value) - Len(Me.TablaC.Item(BancoCHComplemento.Index, I).Value.Substring(0, posi))
                        Dim Al As String = Me.TablaC.Item(BancoCHComplemento.Index, I).Value.Substring(posi, cuantos)
                        Me.TablaC.Item(CtaOchComplemento.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                        Me.TablaC.Item(CtaChequeC.Index, I).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                    End If
                Else
                    ' Cargar_bancosComple("Ch")
                End If
                'Se Muestran Columnas de Contabilidad Electronica
                If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                    Me.TablaC.Columns(BancoCHComplemento.Index).Visible = False
                    Me.TablaC.Columns(CtaOchComplemento.Index).Visible = False
                    Me.TablaC.Columns(NoChComplemento.Index).Visible = False
                    Me.TablaC.Columns(FCHComplemento.Index).Visible = True
                End If

            End If
            Liberar_ProcesoComple(I)
        Catch ex As Exception

        End Try


    End Sub


    Private Sub Liberar_ProcesoComple(ByVal i As Integer)
        Dim contador As Integer = 0
        Dim cantidad As Decimal = Me.TablaC.Item(ImpProviComplemento.Index, i).Value
        cantidad = cantidad + Me.TablaC.Item(UCComplemento.Index, i).Value
        cantidad = cantidad - Me.TablaC.Item(PCComplemento.Index, i).Value

        Dim Retencion As Decimal = 0
        Retencion = Me.TablaC.Item(RISRComplemento.Index, i).Value + Me.TablaC.Item(RIVAComplemento.Index, i).Value
        'Calcula la diferencia en el registro
        Me.TablaC.Item(DifComplemento.Index, i).Value = Math.Round(Calcula_Diferencia(IIf(IsDBNull(Me.TablaC.Item(TRComplemento.Index, i).Value) = True, 0, Me.TablaC.Item(TRComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaC.Item(ImpEfComplemento.Index, i).Value), Convert.ToDecimal(Retencion + Me.TablaC.Item(ImpTComplemento.Index, i).Value) + Convert.ToDecimal(Me.TablaC.Item(ProvAComplemento.Index, i).Value) + Convert.ToDecimal(Me.TablaC.Item(ProvPComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaC.Item(ImpChComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaC.Item(AjusComplemento.Index, i).Value), Convert.ToDecimal(Me.TablaC.Item(AntiComplemento.Index, i).Value), cantidad), 2)
        If Me.TablaC.Item(DifComplemento.Index, i).Value <> 0 Then
            Me.TablaC.Item(DifComplemento.Index, i).Style.BackColor = Color.Red
        Else
            Me.TablaC.Item(DifComplemento.Index, i).Style.BackColor = Color.Green
        End If

        If Trim(UCase(Me.lblce.Text)) = "TRUE" Then ' Bloquera filas de Contabilidad electronica
            ' If Me.TablaC.Item(DifComplemento.Index, i).Value > 0 Or Me.TablaC.Item(TipPolComplemento.Index, i).Value = Nothing Or IIf(IsDBNull(Me.TablaC.Item(NomCtaComplemento.Index, i).Value) = True, "", Me.TablaC.Item(NomCtaComplemento.Index, i).Value) = "" Or IIf(IsDBNull(Me.TablaC.Item(NumPolComplemento.Index, i).Value) = True, "", Me.TablaC.Item(NumPolComplemento.Index, i).Value) = "" Then
            If Me.TablaC.Item(DifComplemento.Index, i).Value <> 0 Or Me.TablaC.Item(TipPolComplemento.Index, i).Value = Nothing Or IIf(IsDBNull(Me.TablaC.Item(NumPolComplemento.Index, i).Value) = True, "", Me.TablaC.Item(NumPolComplemento.Index, i).Value.ToString) = "" Or IIf(IsDBNull(Me.TablaC.Item(NomCtaComplemento.Index, i).Value) = True, "", Me.TablaC.Item(NomCtaComplemento.Index, i).Value) = "" Then

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

                ElseIf Me.TablaC.Item(BancoCHComplemento.Index, i).Value > 0 And Me.TablaC.Item(ImpTComplemento.Index, i).Value > 0 Then ' AMbos
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
                Else
                    Me.TablaC.Item(ApliComplemento.Index, i).Value = True
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

        Dim Indice As Integer = 1000
        For i As Integer = 0 To Me.BancoOrigenTComplemento.Items.Count - 1

            Dim largo As Integer = Len(Trim(Me.BancoOrigenTComplemento.Items(i)))
            Dim posicion As Integer = InStr(1, Trim(Me.BancoOrigenTComplemento.Items(i)), "-", CompareMethod.Binary)
            Dim Al As String = Trim(Me.BancoOrigenTComplemento.Items(i)).Substring(posicion, largo - posicion)
            If Al = valor Then
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
    Private Function Obtener_indexBancodes(ByVal valor As String)
        'Dim Indice As Integer
        'For i As Integer = 0 To Me.BancoDtComplemento.Items.Count - 1
        '    If valor = Trim(Me.BancoDtComplemento.Items(i)) Then

        '        Indice = i


        '        Exit For
        '    End If
        'Next
        'Return Indice

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
    Private Sub CmdGuardaComple_Click(sender As Object, e As EventArgs) Handles CmdGuardaComple.Click
        Guardar_Complementos()
    End Sub

    Private Sub Candados(ByVal Posicion As Integer)
        Dim decima As Decimal = Me.TablaImportar.Item(ImpProvis.Index, Posicion).Value
        If (Math.Round(decima) = Math.Round(Me.TablaImportar.Item(TReal.Index, Posicion).Value) - (Me.TablaImportar.Item(RIVA.Index, Posicion).Value + Me.TablaImportar.Item(RISR.Index, Posicion).Value)) And Me.TablaImportar.Item(Dif.Index, Posicion).Value = 0 Then
            If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
                Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(Posicion)
                Try
                    If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
                        If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value Like "*PP*" Then
                        Else
                            Fila.Cells(ContabilizacionC.Index).Value = Trim(Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value)
                        End If

                    End If
                Catch ex As Exception

                End Try
            End If
        ElseIf Me.TablaImportar.Item(ImpProvis.Index, Posicion).Value > 0 And (Me.TablaImportar.Item(ImpProvis.Index, Posicion).Value <> (Me.TablaImportar.Item(TReal.Index, Posicion).Value - (Me.TablaImportar.Item(RIVA.Index, Posicion).Value + Me.TablaImportar.Item(RISR.Index, Posicion).Value))) Then
            Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(Posicion)
            Try
                If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
                    If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value Like "*PP*" Then
                        Dim posi As Integer = InStr(1, Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value, "PP", CompareMethod.Binary)
                        Dim Letra As String = Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(0, posi - 1)
                        Fila.Cells(ContabilizacionC.Index).Value = Letra
                    End If
                End If
            Catch ex As Exception

            End Try

            Try
                If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
                    If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value Like "*GN*" Then
                        Me.TablaImportar.Item(ImpProvis.Index, Posicion).Value = 0
                    End If
                End If
            Catch ex As Exception

            End Try
        End If





    End Sub


    Private Sub Cargar_Pol_Modelo(ByVal tabla As String, ByVal rfc As String)
        Dim sql As String = ""
        If tabla = "Facturas" Then

        ElseIf tabla = "Complementos" Then

        ElseIf tabla = "Devoluciones" Then

        End If

    End Sub


    Private Sub TablaImportar_KeyDown(sender As Object, e As KeyEventArgs) Handles TablaImportar.KeyDown
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If e.KeyCode = Keys.E AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaImportar.Item(ImpEf.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Dif.Index, Me.TablaImportar.CurrentRow.Index).Value
            If Me.TablaImportar.Item(ImpEf.Index, Me.TablaImportar.CurrentRow.Index).Value > 0 Then
                If Candado_Importe_Efectivo(Me.TablaImportar.CurrentRow.Index) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo

                    Me.TablaImportar.Item(CuentasEfectivo.Index, Me.TablaImportar.CurrentRow.Index).Value = Cuenta_Efectivo()
                    Me.TablaImportar.Columns(CuentasEfectivo.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    If RadMessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaImportar.Item(CuentasEfectivo.Index, Me.TablaImportar.CurrentRow.Index).Value = Cuenta_Efectivo()
                        Me.TablaImportar.Columns(CuentasEfectivo.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaImportar.Item(ImpEf.Index, Me.TablaImportar.CurrentRow.Index).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaImportar.Item(CuentasEfectivo.Index, Me.TablaImportar.CurrentRow.Index).Value = ""
            End If
        ElseIf e.KeyCode = Keys.T AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaImportar.Item(ImpT.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Dif.Index, Me.TablaImportar.CurrentRow.Index).Value
            ' Importe de Transferencia
            If Me.TablaImportar.Item(ImpT.Index, Me.TablaImportar.CurrentRow.Index).Value > 0 Then
                ' BUSCAR BANCOS EN AUTOMATICO DEL DESTINO
                If Trim(Me.TablaImportar.Item(BancoOrigen.Index, Me.TablaImportar.CurrentRow.Index).Value) <> Nothing Then
                    If Trim(Me.TablaImportar.Item(CuentaO.Index, Me.TablaImportar.CurrentRow.Index).Value) <> "" Then
                    Else
                        Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BancoOrigen.Index, Me.TablaImportar.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                        Dim cuantos As Integer = Len(Me.TablaImportar.Item(BancoOrigen.Index, Me.TablaImportar.CurrentRow.Index).Value) - Len(Me.TablaImportar.Item(BancoOrigen.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(0, posi))
                        Dim Al As String = Me.TablaImportar.Item(BancoOrigen.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(posi, cuantos)
                        Me.TablaImportar.Item(CuentaO.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)

                        Me.TablaImportar.Item(CuentaBancos.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                    End If

                End If

                If Trim(Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value) <> Nothing Then
                    Try
                        If Trim(Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value) <> "" And Trim(Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value) <> 0 Then
                            Dim largo As Integer = Len(Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value)
                            Dim cadena As String = Trim(Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value.ToString.Substring(largo - 3, 3))
                            largo = Len(Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value)
                            Dim Al As String = Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(largo - 3, 3)
                            If cadena <> Al Then
                                largo = InStr(1, Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                Al = Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(0, largo - 1)
                                Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                            End If
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(0, posi - 1)
                            Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "'", True)
                        End If
                    Catch ex As Exception

                    End Try
                Else
                    Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value)
                    Try
                        If Trim(Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value) <> "" And Trim(Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value) <> "0" Then
                            Dim largo As Integer = Len(Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value)
                            Dim cadena As String = Trim(Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value.ToString.Substring(largo - 3, 3))
                            largo = Len(Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value)
                            Dim Al As String = Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(largo - 3, 3)
                            If cadena <> Al Then
                                largo = InStr(1, Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                Al = Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(0, largo - 1)
                                Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                            End If
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(0, posi - 1)
                            Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "'", True)
                        End If
                    Catch ex As Exception

                    End Try
                End If

                If Trim(Me.TablaImportar.Item(TipoPoliza.Index, Me.TablaImportar.CurrentRow.Index).Value) <> Nothing Then
                    If Trim(Me.TablaImportar.Item(BancoOrigen.Index, Me.TablaImportar.CurrentRow.Index).Value) <> Nothing Then
                    Else
                        Dim fila As DataGridViewRow = Me.TablaImportar.Rows(Me.TablaImportar.CurrentRow.Index)
                        Try
                            If Trim(Me.TablaImportar.Item(TipoPoliza.Index, Me.TablaImportar.CurrentRow.Index).Value) <> "" Then
                                fila.Cells(BancoOrigen.Index).Value = Me.BancoOrigen.Items(Obtener_indexB(Trim(Me.TablaImportar.Item(TipoPoliza.Index, Me.TablaImportar.CurrentRow.Index).Value)))
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                End If

                If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                    Me.TablaImportar.Columns(Fechat.Index).Visible = True
                    Me.TablaImportar.Columns(BancoOrigen.Index).Visible = False
                    Me.TablaImportar.Columns(CuentaO.Index).Visible = False
                    Me.TablaImportar.Columns(Bancodestino.Index).Visible = False
                    Me.TablaImportar.Columns(CtaBD.Index).Visible = False
                End If

            End If
        ElseIf e.KeyCode = Keys.C AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaImportar.Item(ImpC.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Dif.Index, Me.TablaImportar.CurrentRow.Index).Value
            If Me.TablaImportar.Item(ImpC.Index, Me.TablaImportar.CurrentRow.Index).Value > 0 Then
                'Cheques

                If Trim(Me.TablaImportar.Item(BancosCheques.Index, Me.TablaImportar.CurrentRow.Index).Value) <> "" Then
                    If Trim(Me.TablaImportar.Item(CuentaC.Index, Me.TablaImportar.CurrentRow.Index).Value) <> "" Then
                    Else
                        Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BancosCheques.Index, Me.TablaImportar.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                        Dim cuantos As Integer = Len(Me.TablaImportar.Item(BancosCheques.Index, Me.TablaImportar.CurrentRow.Index).Value) - Len(Me.TablaImportar.Item(BancosCheques.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(0, posi))
                        Dim Al As String = Me.TablaImportar.Item(BancosCheques.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(posi, cuantos)
                        Me.TablaImportar.Item(CuentaC.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                        Me.TablaImportar.Item(CtaCheque.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                    End If
                Else

                End If
                'Se Muestran Columnas de Contabilidad Electronica
                If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                    Me.TablaImportar.Columns(BancosCheques.Index).Visible = False
                    Me.TablaImportar.Columns(CuentaC.Index).Visible = False
                    Me.TablaImportar.Columns(NoCheque.Index).Visible = False
                    Me.TablaImportar.Columns(FechaC.Index).Visible = True

                End If

            End If
        ElseIf e.KeyCode = Keys.A AndAlso e.Modifiers = Keys.Shift Then ' anticipos

            Me.TablaImportar.Item(Anti.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Dif.Index, Me.TablaImportar.CurrentRow.Index).Value
            If Me.TablaImportar.Item(Anti.Index, Me.TablaImportar.CurrentRow.Index).Value > 0 Then

                Me.TablaImportar.Item(CtaAnti.Index, Me.TablaImportar.CurrentRow.Index).Value = Cuenta_Anticipo()
                Me.TablaImportar.Columns(CtaAnti.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Else
                Me.TablaImportar.Item(CtaAnti.Index, Me.TablaImportar.CurrentRow.Index).Value = ""
            End If

        ElseIf e.KeyCode = Keys.J AndAlso e.Modifiers = Keys.Shift Then ' Ajustes
            Me.TablaImportar.Item(Ajus.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Dif.Index, Me.TablaImportar.CurrentRow.Index).Value
            If Me.TablaImportar.Item(Ajus.Index, Me.TablaImportar.CurrentRow.Index).Value <> 0 Then
                If Candado_Importe_Efectivo(Me.TablaImportar.CurrentRow.Index) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo
                    Me.TablaImportar.Item(CtaAjustes.Index, Me.TablaImportar.CurrentRow.Index).Value = Cuenta_Efectivo()
                    Me.TablaImportar.Columns(CtaAjustes.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    If RadMessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaImportar.Item(CtaAjustes.Index, Me.TablaImportar.CurrentRow.Index).Value = Cuenta_Efectivo()
                        Me.TablaImportar.Columns(CtaAjustes.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaImportar.Item(Ajus.Index, Me.TablaImportar.CurrentRow.Index).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaImportar.Item(CtaAjustes.Index, Me.TablaImportar.CurrentRow.Index).Value = ""
            End If
        ElseIf e.KeyCode = Keys.P AndAlso e.Modifiers = Keys.Shift Then ' Provision
            Me.TablaImportar.Item(ImpProvis.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Dif.Index, Me.TablaImportar.CurrentRow.Index).Value
        ElseIf e.KeyCode = Keys.O AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaImportar.Item(AcreedoresTerceros.Index, Me.TablaImportar.CurrentRow.Index).Value = Me.TablaImportar.Item(Dif.Index, Me.TablaImportar.CurrentRow.Index).Value
        End If
        If Me.TablaImportar.Rows.Count > 0 Then
            Liberar_Proceso(Me.TablaImportar.CurrentRow.Index)
        End If
        If e.KeyCode = Keys.B AndAlso e.Modifiers = Keys.Control Then
            If Me.Buscador1.Visible = True Then
                Me.Buscador1.Visible = False
                Me.Buscador1.TxtFiltro.Text = ""
            Else
                Me.Buscador1.Visible = True
                Me.Buscador1.TxtFiltro.Text = ""
            End If

        End If
        If e.KeyCode = Keys.A AndAlso e.Modifiers = Keys.Control Then
            'Programar calculo automatico de numeros de poliza
            Dim Inicial As Integer = InputBox("Teclea el numero de poliza Inicial:", Eventos.titulo_app, 1)
            For Each Fila As DataGridViewRow In TablaImportar.Rows
                If Fila.Cells(NumPol.Index).Selected = True Then
                    Fila.Cells(NumPol.Index).Value = Inicial
                    Finalizamanaual(NumPol.Index)
                    Inicial += 1
                End If
            Next
        End If
    End Sub

    Private Sub AgregarBancosRFCsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AgregarBancosRFCsToolStripMenuItem.Click
        If Me.TcRecibidas.SelectedIndex = 0 Then
            If Me.TablaImportar.CurrentCell.ColumnIndex = Me.Bancodestino.Index Then 'Cuentas del Cargo

                Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Me.lstCliente.SelectItem & ") and RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "' order by Banco")
                Dim actividad(,) As String
                ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim largo As Integer = Len(ds.Tables(0).Rows(i)("Clabe"))
                    Dim cadena As String = Trim(ds.Tables(0).Rows(i)("Banco")) & " " & Trim(ds.Tables(0).Rows(i)("Clabe").ToString.Substring(largo - 3, 3))

                    actividad(0, i) = cadena
                    Debug.Print(Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value)
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
                Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value = descrip
                Me.TablaImportar_CellEndEdit(Me.TablaImportar, Nothing)
            ElseIf Me.TablaImportar.CurrentCell.ColumnIndex = Me.BankDT2.Index Then

                Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Me.lstCliente.SelectItem & ") and RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "' order by Banco")
                Dim actividad(,) As String
                ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim largo As Integer = Len(ds.Tables(0).Rows(i)("Clabe"))
                    Dim cadena As String = Trim(ds.Tables(0).Rows(i)("Banco")) & " " & Trim(ds.Tables(0).Rows(i)("Clabe").ToString.Substring(largo - 3, 3))

                    actividad(0, i) = cadena
                    Debug.Print(Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value)
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
                Me.TablaImportar.Item(BankDT2.Index, Me.TablaImportar.CurrentRow.Index).Value = descrip
                Me.TablaImportar_CellEndEdit(Me.TablaImportar, Nothing)
            ElseIf Me.TablaImportar.CurrentCell.ColumnIndex = Me.BankDT3.Index Then

                Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Me.lstCliente.SelectItem & ") and RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "' order by Banco")
                Dim actividad(,) As String
                ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim largo As Integer = Len(ds.Tables(0).Rows(i)("Clabe"))
                    Dim cadena As String = Trim(ds.Tables(0).Rows(i)("Banco")) & " " & Trim(ds.Tables(0).Rows(i)("Clabe").ToString.Substring(largo - 3, 3))

                    actividad(0, i) = cadena
                    Debug.Print(Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value)
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
                Me.TablaImportar.Item(BankDT3.Index, Me.TablaImportar.CurrentRow.Index).Value = descrip
                Me.TablaImportar_CellEndEdit(Me.TablaImportar, Nothing)
            ElseIf Me.TablaImportar.CurrentCell.ColumnIndex = Me.BankDT4.Index Then ' cuarta Transferencia

                Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Me.lstCliente.SelectItem & ") and RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "' order by Banco")
                Dim actividad(,) As String
                ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim largo As Integer = Len(ds.Tables(0).Rows(i)("Clabe"))
                    Dim cadena As String = Trim(ds.Tables(0).Rows(i)("Banco")) & " " & Trim(ds.Tables(0).Rows(i)("Clabe").ToString.Substring(largo - 3, 3))

                    actividad(0, i) = cadena
                    Debug.Print(Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value)
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
                Me.TablaImportar.Item(BankDT4.Index, Me.TablaImportar.CurrentRow.Index).Value = descrip
                Me.TablaImportar_CellEndEdit(Me.TablaImportar, Nothing)

            ElseIf Me.TablaImportar.CurrentCell.ColumnIndex = Me.BankDT5.Index Then ' Quinta Transferencia

                Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Me.lstCliente.SelectItem & ") and RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "' order by Banco")
                Dim actividad(,) As String
                ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim largo As Integer = Len(ds.Tables(0).Rows(i)("Clabe"))
                    Dim cadena As String = Trim(ds.Tables(0).Rows(i)("Banco")) & " " & Trim(ds.Tables(0).Rows(i)("Clabe").ToString.Substring(largo - 3, 3))

                    actividad(0, i) = cadena
                    Debug.Print(Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value)
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
                Me.TablaImportar.Item(BankDT5.Index, Me.TablaImportar.CurrentRow.Index).Value = descrip
                Me.TablaImportar_CellEndEdit(Me.TablaImportar, Nothing)

            ElseIf Me.TablaImportar.CurrentCell.ColumnIndex = Me.BankDT6.Index Then ' Sexta Transferencia

                Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Me.lstCliente.SelectItem & ") and RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "' order by Banco")
                Dim actividad(,) As String
                ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim largo As Integer = Len(ds.Tables(0).Rows(i)("Clabe"))
                    Dim cadena As String = Trim(ds.Tables(0).Rows(i)("Banco")) & " " & Trim(ds.Tables(0).Rows(i)("Clabe").ToString.Substring(largo - 3, 3))

                    actividad(0, i) = cadena
                    Debug.Print(Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value)
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
                Me.TablaImportar.Item(BankDT6.Index, Me.TablaImportar.CurrentRow.Index).Value = descrip
                Me.TablaImportar_CellEndEdit(Me.TablaImportar, Nothing)

            ElseIf Me.TablaImportar.CurrentCell.ColumnIndex = Me.BankDT7.Index Then ' Septima Transferencia

                Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Me.lstCliente.SelectItem & ") and RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "' order by Banco")
                Dim actividad(,) As String
                ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim largo As Integer = Len(ds.Tables(0).Rows(i)("Clabe"))
                    Dim cadena As String = Trim(ds.Tables(0).Rows(i)("Banco")) & " " & Trim(ds.Tables(0).Rows(i)("Clabe").ToString.Substring(largo - 3, 3))

                    actividad(0, i) = cadena
                    Debug.Print(Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value)
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
                Me.TablaImportar.Item(BankDT7.Index, Me.TablaImportar.CurrentRow.Index).Value = descrip
                Me.TablaImportar_CellEndEdit(Me.TablaImportar, Nothing)
            End If
        ElseIf Me.TcRecibidas.SelectedIndex = 1 Then

        ElseIf Me.TcRecibidas.SelectedIndex = 2 Then
            If Me.TablaC.CurrentCell.ColumnIndex = Me.NomCtaComplemento.Index Then 'Cuentas del Cargo

                Dim ds As DataSet = Eventos.Obtener_DS("SELECT DISTINCT CONVERT(NVARCHAR, Bancos.Clave, 103) + '-' + Bancos.Nombre AS Banco ,clabe  FROM     Bancos INNER JOIN     Bancos_RFC ON Bancos.Id_Banco = Bancos_RFC.Id_Banco  WHERE  (Bancos_RFC.Id_Empresa  = " & Me.lstCliente.SelectItem & ") and RFC = '" & Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & "' order by Banco")
                Dim actividad(,) As String
                ReDim actividad(2, ds.Tables(0).Rows.Count + 1)

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim largo As Integer = Len(ds.Tables(0).Rows(i)("Clabe"))
                    Dim cadena As String = Trim(ds.Tables(0).Rows(i)("Banco")) & " " & Trim(ds.Tables(0).Rows(i)("Clabe").ToString.Substring(largo - 3, 3))

                    actividad(0, i) = cadena
                    Debug.Print(Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
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
                Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value = descrip
                ComplemantosL(Me.TablaC.CurrentRow.Index)

            End If
        End If

    End Sub

    Private Sub CmdEfectivo_Click(sender As Object, e As EventArgs) Handles CmdEfectivo.Click
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            If Me.TablaImportar.Item(ImpEf.Index, i).Value > 0 Then
                Me.TablaImportar.Item(CuentasEfectivo.Index, i).Value = Cuenta_Efectivo()
                Me.TablaImportar.Columns(CuentasEfectivo.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Liberar_Proceso(i)
            Else
                If Me.TablaImportar.Item(Tot.Index, i).Value <= 2000 And Me.TablaImportar.Item(ImpEf.Index, i).Value = 0 And Me.TablaImportar.Item(Dif.Index, i).Value = Me.TablaImportar.Item(Tot.Index, i).Value Then
                    If UCase(Trim(Me.TablaImportar.Item(Fpago.Index, i).Value)) Like "*TRANSF*" Then
                    Else
                        Me.TablaImportar.Item(ImpEf.Index, i).Value = Me.TablaImportar.Item(Tot.Index, i).Value
                        Me.TablaImportar.Item(CuentasEfectivo.Index, i).Value = Cuenta_Efectivo()
                        Me.TablaImportar.Columns(CuentasEfectivo.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                        Liberar_Proceso(i)
                        Try
                            Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(i)
                            If Trim(Me.TablaImportar.Item(TipoPoliza.Index, i).Value) <> Nothing Then
                            Else

                                Fila.Cells(TipoPoliza.Index).Value = Me.TipoPoliza.Items(Obtener_index2(Trim("004 - Efectivo")))

                            End If
                        Catch ex As Exception

                        End Try
                    End If
                End If
            End If
        Next
    End Sub
    Private Sub Cargar_Pol_Modelo(ByVal RFC As String, ByVal Fecha As String, ByVal i As Integer)
        My.Forms.Inicio.txtServerDB.Text = serV
        If Me.TablaImportar.Item(Aplic.Index, i).Value = False Then
            If Me.TablaImportar.Item(Dif.Index, i).Value <> 0 Then

                Dim Sql As String = " SELECT 	Id_Pol_Mod_Factura,	RFC,	Nombre,	FechaAntesde,	FechaDespuesde,	Clave,	Efectivo,
	                        Transferencia,	Banco_Origen,	Banco_Destino,	Cheque,	Nom_Banco_Ch,	Tipo_Poliza,	Provision,	Anticipo,	Id_Empresa,	Emitidas
                            FROM dbo.Facturas_Poliza_Modelo where Id_Empresa = " & Me.lstCliente.SelectItem & " and Emitidas = 0 and Rfc= '" & RFC & "'   "'and  " & Fecha.ToString.Substring(0, 2) & " BETWEEN  FechaDespuesde  and FechaAntesde

                '  From  dbo.Facturas_Poliza_Modelo where Rfc= '" & RFC & "'  and FechaAntesde <= " & Fecha.ToString.Substring(0, 2) & " and FechaDespuesde >= " & Fecha.ToString.Substring(0, 2) & "  "
                Dim ds As DataSet = Eventos.Obtener_DS(Sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(i)

                    Try
                        If Trim(Me.TablaImportar.Item(ContabilizacionC.Index, i).Value) <> Nothing Then
                        Else
                            If Trim(ds.Tables(0).Rows(0)("Clave")) <> "" Then
                                Fila.Cells(ContabilizacionC.Index).Value = Trim(ds.Tables(0).Rows(0)("Clave"))
                                'Calcula cuenta cargos
                                If Me.TablaImportar.Item(NCuenta.Index, Me.TablaImportar.CurrentRow.Index).Value = Nothing Then
                                    Dim Nueva As String = CuentaTipo(Me.TablaImportar.Item(RFCE.Index, i).Value, Me.TablaImportar.Item(ContabilizacionC.Index, i).Value, i)
                                    'Me.TablaImportar.Item(NCuenta.Index, Me.TablaImportar.CurrentRow.Index).Value = Cuenta_cargos(Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value, Trim(Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value))
                                    Me.TablaImportar.Item(NCuenta.Index, i).Value = Nueva

                                End If
                                If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then

                                    If Me.TablaImportar.Item(NCuenta.Index, Me.TablaImportar.CurrentRow.Index).Value = Nothing Then

                                        If Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).Value.ToString.Contains("PP") Then
                                            Me.TablaImportar.Item(ProvA.Index, Me.TablaImportar.CurrentRow.Index).Value = 0
                                            Me.TablaImportar.Item(ProvP.Index, Me.TablaImportar.CurrentRow.Index).Value = 0
                                        End If
                                        Me.TablaImportar.Item(NCuenta.Index, Me.TablaImportar.CurrentRow.Index).Value = CuentaTipo(Me.TablaImportar.Item(RFCE.Index, i).Value, Me.TablaImportar.Item(ContabilizacionC.Index, i).Value, i)

                                        Me.TablaImportar.Item(LetraS.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.Calcula_letraSat(Me.TablaImportar.Item(MPago.Index, Me.TablaImportar.CurrentRow.Index).Value, Me.TablaImportar.Item(UCFDI.Index, Me.TablaImportar.CurrentRow.Index).Value)
                                    End If
                                Else
                                    Me.TablaImportar.Item(NCuenta.Index, Me.TablaImportar.CurrentRow.Index).Value = ""
                                End If
                            End If
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        If Trim(Me.TablaImportar.Item(TipoPoliza.Index, i).Value) <> Nothing Then
                        Else
                            If Trim(ds.Tables(0).Rows(0)("Tipo_Poliza")) <> "" Then
                                Fila.Cells(TipoPoliza.Index).Value = Trim(ds.Tables(0).Rows(0)("Tipo_Poliza"))
                            End If
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        If Trim(ds.Tables(0).Rows(0)("Transferencia")) = True Then

                            Me.TablaImportar.Item(ImpT.Index, i).Value = Me.TablaImportar.Item(Dif.Index, i).Value
                            Me.TablaImportar.Item(Dif.Index, i).Value = 0
                            If Trim(Me.TablaImportar.Item(BancoOrigen.Index, i).Value) <> Nothing Then
                            Else


                                If Trim(ds.Tables(0).Rows(0)("Banco_Origen")) <> "" Then
                                    Fila.Cells(BancoOrigen.Index).Value = Me.BancoOrigen.Items(Obtener_indexB(Trim(ds.Tables(0).Rows(0)("Banco_Origen"))))
                                    If Trim(Me.TablaImportar.Item(CuentaO.Index, Me.TablaImportar.CurrentRow.Index).Value) <> "" Then
                                    Else
                                        Dim posi As Integer = InStr(1, Me.TablaImportar.Item(BancoOrigen.Index, Me.TablaImportar.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                        Dim cuantos As Integer = Len(Me.TablaImportar.Item(BancoOrigen.Index, Me.TablaImportar.CurrentRow.Index).Value) - Len(Me.TablaImportar.Item(BancoOrigen.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(0, posi))
                                        Dim Al As String = Me.TablaImportar.Item(BancoOrigen.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(posi, cuantos)
                                        Me.TablaImportar.Item(CuentaO.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)

                                        Me.TablaImportar.Item(CuentaBancos.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                                    End If

                                End If
                                Me.TablaImportar.Item(Bancodestino.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(0)("Banco_Destino")) = True, "", ds.Tables(0).Rows(0)("Banco_Destino"))

                                Try
                                    If Trim(Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value) <> "" And Trim(Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value) <> "0" Then
                                        Dim largo As Integer = Len(Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value)
                                        Dim cadena As String = Trim(Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value.ToString.Substring(largo - 3, 3))
                                        largo = Len(Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value)
                                        Dim Al As String = Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(largo - 3, 3)
                                        If cadena <> Al Then
                                            largo = InStr(1, Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                            Al = Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(0, largo - 1)
                                            Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                                        End If
                                    Else
                                        Dim posi As Integer = InStr(1, Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                        Dim Al As String = Me.TablaImportar.Item(Bancodestino.Index, Me.TablaImportar.CurrentRow.Index).Value.Substring(0, posi - 1)
                                        Me.TablaImportar.Item(CtaBD.Index, Me.TablaImportar.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaImportar.Item(RFCE.Index, Me.TablaImportar.CurrentRow.Index).Value & "'", True)
                                    End If
                                Catch ex As Exception

                                End Try

                            End If
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        If Trim(ds.Tables(0).Rows(0)("Cheque")) = True Then
                            Me.TablaImportar.Item(ImpC.Index, i).Value = Me.TablaImportar.Item(Dif.Index, i).Value
                            Me.TablaImportar.Item(Dif.Index, i).Value = 0
                            If Trim(Me.TablaImportar.Item(BancosCheques.Index, i).Value) <> Nothing Then
                            Else
                                If Trim(ds.Tables(0).Rows(0)("Nom_Banco_Ch")) <> "" Then
                                    Fila.Cells(BancosCheques.Index).Value = Me.BancosCheques.Items(Obtener_indexbN(Trim(ds.Tables(0).Rows(0)("Nom_Banco_Ch"))))
                                End If
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        If Trim(ds.Tables(0).Rows(0)("Efectivo")) = True Then
                            Me.TablaImportar.Item(ImpEf.Index, i).Value = Me.TablaImportar.Item(Dif.Index, i).Value
                            Me.TablaImportar.Item(Dif.Index, i).Value = 0
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        If Trim(ds.Tables(0).Rows(0)("Anticipo")) = True Then
                            Me.TablaImportar.Item(Anti.Index, i).Value = Me.TablaImportar.Item(Dif.Index, i).Value
                            Me.TablaImportar.Item(Dif.Index, i).Value = 0
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        If Trim(ds.Tables(0).Rows(0)("Provision")) = True Then
                            Me.TablaImportar.Item(ImpProvis.Index, i).Value = Me.TablaImportar.Item(Dif.Index, i).Value
                            Me.TablaImportar.Item(Dif.Index, i).Value = 0
                        End If
                    Catch ex As Exception

                    End Try
                    Finalizamanaual(i)
                    'SendKeys.Send("{F2}")
                    'SendKeys.Send("{ENTER}")
                End If
            End If

        End If
    End Sub

    Private Sub Cargar_Pol_Modelo_Notas(ByVal RFC As String, ByVal Fecha As String, ByVal i As Integer)
        If Me.TablaD.Item(AplD.Index, i).Value = False Then
            If Me.TablaD.Item(DifD.Index, i).Value <> 0 Then

                Dim Sql As String = " SELECT 	Id_Pol_Mod_Nota,	RFC,	Nombre,	FechaAntesde,	FechaDespuesde,	Clave,	Efectivo,	
                                Transferencia,	Cheque,	Tipo_Poliza,	ProvisionAcred,	ProvisionProveed,	Id_Empresa FROM dbo.Notas_Pol_Modelo 
                                where Id_Empresa = " & Me.lstCliente.SelectItem & " and  Emitidas = 0 and Rfc= '" & RFC & "'  and  " & Fecha.ToString.Substring(0, 2) & " BETWEEN   FechaDespuesde  and FechaAntesde "

                Dim ds As DataSet = Eventos.Obtener_DS(Sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim Fila As DataGridViewRow = Me.TablaD.Rows(i)

                    Try
                        If Trim(Me.TablaD.Item(LetraCd.Index, i).Value) <> Nothing Then

                            Dim Nuevo As String = CuentaTipoD(Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value, Trim(Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value), Me.TablaD.CurrentRow.Index)
                            Me.TablaD.Item(NomCuentaD.Index, Me.TablaD.CurrentRow.Index).Value = Nuevo

                        Else
                            If Trim(ds.Tables(0).Rows(0)("Clave")) <> "" Then
                                Fila.Cells(LetraCd.Index).Value = Me.LetraCd.Items(Obtener_indexDV(Trim(ds.Tables(0).Rows(0)("Clave"))))
                                'Calcula cuenta cargos
                                If Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value <> Nothing Then

                                    If Me.TablaD.Item(NomCuentaD.Index, Me.TablaD.CurrentRow.Index).Value = Nothing Then
                                        If Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value.ToString.Contains("PP") Then
                                            Me.TablaD.Item(ProvAD.Index, Me.TablaD.CurrentRow.Index).Value = 0
                                            Me.TablaD.Item(ProvPD.Index, Me.TablaD.CurrentRow.Index).Value = 0
                                        End If

                                        Dim Nuevo As String = CuentaTipoD(Me.TablaD.Item(RFCED.Index, Me.TablaD.CurrentRow.Index).Value, Trim(Me.TablaD.Item(LetraCd.Index, Me.TablaD.CurrentRow.Index).Value), Me.TablaD.CurrentRow.Index)
                                        Me.TablaD.Item(NomCuentaD.Index, Me.TablaD.CurrentRow.Index).Value = Nuevo


                                        Me.TablaD.Item(LetraSD.Index, Me.TablaD.CurrentRow.Index).Value = Eventos.Calcula_letraSat(Me.TablaD.Item(MetoD.Index, Me.TablaD.CurrentRow.Index).Value, Me.TablaD.Item(UcfdiD.Index, Me.TablaD.CurrentRow.Index).Value)
                                    End If
                                Else
                                    Me.TablaD.Item(NomCuentaD.Index, Me.TablaD.CurrentRow.Index).Value = ""
                                End If
                            End If
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        If Trim(Me.TablaD.Item(TipPolD.Index, i).Value) <> Nothing Then
                        Else
                            If Trim(ds.Tables(0).Rows(0)("Tipo_Poliza")) <> "" Then
                                Fila.Cells(TipPolD.Index).Value = Me.TipPolD.Items(Obtener_index2Dv(Trim(ds.Tables(0).Rows(0)("Tipo_Poliza"))))
                            End If
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        If Trim(ds.Tables(0).Rows(0)("Transferencia")) = True Then

                            Me.TablaD.Item(ImpTD.Index, i).Value = Me.TablaD.Item(DifD.Index, i).Value
                            Me.TablaD.Item(DifD.Index, i).Value = 0
                            If Trim(Me.TablaD.Item(BancoOd.Index, i).Value) <> Nothing Then
                            Else


                                If Trim(ds.Tables(0).Rows(0)("Banco_Origen")) <> "" Then
                                    Fila.Cells(BancoOd.Index).Value = Me.BancoOd.Items(Obtener_indexBDv(Trim(ds.Tables(0).Rows(0)("Banco_Origen"))))
                                    If Trim(Me.TablaD.Item(CtaOD.Index, Me.TablaD.CurrentRow.Index).Value) <> "" Then
                                    Else
                                        Dim posi As Integer = InStr(1, Me.TablaD.Item(BancoOd.Index, Me.TablaD.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                        Dim cuantos As Integer = Len(Me.TablaD.Item(BancoOd.Index, Me.TablaD.CurrentRow.Index).Value) - Len(Me.TablaD.Item(BancoOd.Index, Me.TablaD.CurrentRow.Index).Value.Substring(0, posi))
                                        Dim Al As String = Me.TablaD.Item(BancoOd.Index, Me.TablaD.CurrentRow.Index).Value.Substring(posi, cuantos)
                                        Me.TablaD.Item(CtaOD.Index, Me.TablaD.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)

                                        Me.TablaD.Item(CtaBancosD.Index, Me.TablaD.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                                    End If

                                End If
                                Me.TablaD.Item(BancoDD.Index, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(0)("Banco_Destino")) = True, "", ds.Tables(0).Rows(0)("Banco_Destino"))

                                Try
                                    If Trim(Me.TablaD.Item(CtaDD.Index, Me.TablaD.CurrentRow.Index).Value) <> "" And Trim(Me.TablaD.Item(CtaDD.Index, Me.TablaD.CurrentRow.Index).Value) <> "0" Then
                                        Dim largo As Integer = Len(Me.TablaD.Item(BancoDD.Index, Me.TablaD.CurrentRow.Index).Value)
                                        Dim cadena As String = Trim(Me.TablaD.Item(BancoDD.Index, Me.TablaD.CurrentRow.Index).Value.ToString.Substring(largo - 3, 3))
                                        largo = Len(Me.TablaD.Item(CtaDD.Index, Me.TablaD.CurrentRow.Index).Value)
                                        Dim Al As String = Me.TablaD.Item(CtaDD.Index, Me.TablaD.CurrentRow.Index).Value.Substring(largo - 3, 3)
                                        If cadena <> Al Then
                                            largo = InStr(1, Me.TablaD.Item(BancoDD.Index, Me.TablaD.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                            Al = Me.TablaD.Item(BancoDD.Index, Me.TablaD.CurrentRow.Index).Value.Substring(0, largo - 1)
                                            Me.TablaD.Item(CtaDD.Index, Me.TablaD.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaD.Item(RFCE.Index, Me.TablaD.CurrentRow.Index).Value & "'", True)
                                        End If
                                    Else
                                        Dim posi As Integer = InStr(1, Me.TablaD.Item(BancoDD.Index, Me.TablaD.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                        Dim Al As String = Me.TablaD.Item(BancoDD.Index, Me.TablaD.CurrentRow.Index).Value.Substring(0, posi - 1)
                                        Me.TablaD.Item(CtaDD.Index, Me.TablaD.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC", "Clabe", " Id_Empresa =" & Me.lstCliente.SelectItem & " and id_banco = '" & Trim(Al) & "' and  RFC = '" & Me.TablaD.Item(RFCE.Index, Me.TablaD.CurrentRow.Index).Value & "'", True)
                                    End If
                                Catch ex As Exception

                                End Try

                            End If
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        If Trim(ds.Tables(0).Rows(0)("Cheque")) = True Then
                            Me.TablaD.Item(ImpCHD.Index, i).Value = Me.TablaD.Item(DifD.Index, i).Value
                            Me.TablaD.Item(DifD.Index, i).Value = 0
                            If Trim(Me.TablaD.Item(NomBankCHD.Index, i).Value) <> Nothing Then
                            Else
                                If Trim(ds.Tables(0).Rows(0)("Nom_Banco_Ch")) <> "" Then
                                    Fila.Cells(NomBankCHD.Index).Value = Me.NomBankCHD.Items(Obtener_indexBDvBH(Trim(ds.Tables(0).Rows(0)("Nom_Banco_Ch"))))
                                End If
                            End If
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        If Trim(ds.Tables(0).Rows(0)("Efectivo")) = True Then
                            Me.TablaD.Item(ImpED.Index, i).Value = Me.TablaD.Item(DifD.Index, i).Value
                            Me.TablaD.Item(DifD.Index, i).Value = 0
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        If Trim(ds.Tables(0).Rows(0)("ProvisionAcred")) = True Then
                            Me.TablaD.Item(ProvAD.Index, i).Value = Me.TablaD.Item(DifD.Index, i).Value
                            Me.TablaD.Item(DifD.Index, i).Value = 0
                        End If
                    Catch ex As Exception

                    End Try
                    Try
                        If Trim(ds.Tables(0).Rows(0)("ProvisionProveed")) = True Then
                            Me.TablaD.Item(ProvPD.Index, i).Value = Me.TablaD.Item(DifD.Index, i).Value
                            Me.TablaD.Item(DifD.Index, i).Value = 0
                        End If
                    Catch ex As Exception

                    End Try
                    Me.TablaD_CellEndEdit(Me.TablaD, Nothing)
                End If
            End If

        End If
    End Sub

    Private Sub TablaC_KeyDown(sender As Object, e As KeyEventArgs) Handles TablaC.KeyDown
        If e.KeyCode = Keys.E AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaC.Item(ImpEfComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Me.TablaC.Item(DifComplemento.Index, Me.TablaC.CurrentRow.Index).Value
            If Me.TablaC.Item(ImpEfComplemento.Index, Me.TablaC.CurrentRow.Index).Value > 0 Then
                If Candado_Importe_Efectivocomple(Me.TablaC.CurrentRow.Index) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo

                    Me.TablaC.Item(CtaEfComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Cuenta_Efectivo()
                    Me.TablaC.Columns(CtaEfComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    If MessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.Titulo_APP, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaC.Item(CtaEfComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Cuenta_Efectivo()
                        Me.TablaC.Columns(CtaEfComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaC.Item(ImpEfComplemento.Index, Me.TablaC.CurrentRow.Index).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaC.Item(CtaEfComplemento.Index, Me.TablaC.CurrentRow.Index).Value = ""
            End If
            ComplemantosL(Me.TablaC.CurrentRow.Index)
        ElseIf e.KeyCode = Keys.T AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaC.Item(ImpTComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Me.TablaC.Item(DifComplemento.Index, Me.TablaC.CurrentRow.Index).Value
            If Me.TablaC.Item(ImpTComplemento.Index, Me.TablaC.CurrentRow.Index).Value > 0 Then
                If Trim(Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> Nothing Then
                    If Trim(Me.TablaC.Item(CtaOTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" Then
                    Else
                        Dim posi As Integer = InStr(1, Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                        Dim cuantos As Integer = Len(Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) - Len(Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, posi))
                        Dim Al As String = Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(posi, cuantos)
                        Me.TablaC.Item(CtaOTComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)

                        Me.TablaC.Item(CtaBancosComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                    End If

                End If

                'Empieza el nuevo codigo Bnaco destino
                If Trim(Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> Nothing Then
                    Try
                        If Trim(Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" And Trim(Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "0" Then
                            Dim largo As Integer = Len(Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                            Dim cadena As String = Trim(Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value.ToString.Substring(largo - 3, 3))
                            largo = Len(Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                            Dim Al As String = Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(largo - 3, 3)
                            If cadena <> Al Then
                                largo = InStr(1, Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                Al = Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, largo - 1)
                                Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                            End If
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, posi - 1)
                            Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & "'", True)
                        End If
                    Catch ex As Exception

                    End Try
                Else
                    Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Banco_destino(Me.lstCliente.SelectItem, Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                    Try
                        If Trim(Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" And Trim(Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "0" Then
                            Dim largo As Integer = Len(Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                            Dim cadena As String = Trim(Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value.ToString.Substring(largo - 3, 3))
                            largo = Len(Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                            Dim Al As String = Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(largo - 3, 3)
                            If cadena <> Al Then
                                largo = InStr(1, Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                Al = Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, largo - 1)
                                Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & "' and Bancos_RFC.clabe like '%" & cadena & "%'", True)
                            End If
                        Else
                            Dim posi As Integer = InStr(1, Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                            Dim Al As String = Me.TablaC.Item(BancoDtComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, posi - 1)
                            Me.TablaC.Item(CtaDTComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_RFC INNER JOIN Bancos ON Bancos.Id_Banco = Bancos_RFC.Id_Banco ", "Clabe", "    Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos.Clave = " & Trim(Al) & " and  RFC = '" & Me.TablaC.Item(RFCComplemento.Index, Me.TablaC.CurrentRow.Index).Value & "'", True)
                        End If
                    Catch ex As Exception

                    End Try
                End If
                If Trim(Me.TablaC.Item(TipPolComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> Nothing Then
                    If Trim(Me.TablaC.Item(BancoOrigenTComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> Nothing Then
                    Else
                        Dim fila As DataGridViewRow = Me.TablaC.Rows(Me.TablaC.CurrentRow.Index)
                        Try
                            If Trim(Me.TablaC.Item(TipPolComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" Then
                                Dim largo As Integer = Len(Me.TablaC.Item(TipPolComplemento.Index, Me.TablaC.CurrentRow.Index).Value)
                                Dim posicion As Integer = InStr(1, Me.TablaC.Item(TipPolComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                                Dim Al As String = Me.TablaC.Item(TipPolComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(posicion + 1, largo - posicion - 1)
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
                    Me.TablaC.Columns(BancoOrigenTComplemento.Index).Visible = False
                    Me.TablaC.Columns(CtaOTComplemento.Index).Visible = False
                    Me.TablaC.Columns(BancoDtComplemento.Index).Visible = False
                    Me.TablaC.Columns(CtaDTComplemento.Index).Visible = False
                    Me.TablaC.Columns(FTComplemento.Index).Visible = True

                End If


            End If
            ComplemantosL(Me.TablaC.CurrentRow.Index)
        ElseIf e.KeyCode = Keys.C AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaC.Item(ImpChComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Me.TablaC.Item(DifComplemento.Index, Me.TablaC.CurrentRow.Index).Value
            If Me.TablaC.Item(ImpChComplemento.Index, Me.TablaC.CurrentRow.Index).Value > 0 Then
                'Cheques

                If Trim(Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" Then
                    If Trim(Me.TablaC.Item(CtaOchComplemento.Index, Me.TablaC.CurrentRow.Index).Value) <> "" Then
                    Else
                        Dim posi As Integer = InStr(1, Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value, "-", CompareMethod.Binary)
                        Dim cuantos As Integer = Len(Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value) - Len(Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(0, posi))
                        Dim Al As String = Me.TablaC.Item(BancoCHComplemento.Index, Me.TablaC.CurrentRow.Index).Value.Substring(posi, cuantos)
                        Me.TablaC.Item(CtaOchComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes", "No_Cuenta", " Id_Empresa =" & Me.lstCliente.SelectItem & " and Alias = '" & Trim(Al) & "'", True)
                        Me.TablaC.Item(CtaChequeC.Index, Me.TablaC.CurrentRow.Index).Value = Eventos.ObtenerValorDB("Bancos_Clientes INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Id_cat_Cuentas = Bancos_Clientes.Id_cat_Cuentas", "Catalogo_de_Cuentas.Cuenta", " Bancos_Clientes.Id_Empresa =" & Me.lstCliente.SelectItem & " and Bancos_Clientes.Alias = '" & Trim(Al) & "'", True)

                    End If
                Else
                    ' Cargar_bancosComple("Ch")
                End If
                'Se Muestran Columnas de Contabilidad Electronica
                If Trim(UCase(Me.lblce.Text)) = "FALSE" Then
                    Me.TablaC.Columns(BancoCHComplemento.Index).Visible = False
                    Me.TablaC.Columns(CtaOchComplemento.Index).Visible = False
                    Me.TablaC.Columns(NoChComplemento.Index).Visible = False
                    Me.TablaC.Columns(FCHComplemento.Index).Visible = True
                End If

            End If

            ComplemantosL(Me.TablaC.CurrentRow.Index)
        ElseIf e.KeyCode = Keys.A AndAlso e.Modifiers = Keys.Shift Then ' anticipos

            Me.TablaC.Item(AntiComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Me.TablaC.Item(DifComplemento.Index, Me.TablaC.CurrentRow.Index).Value
            If Me.TablaC.Item(AntiComplemento.Index, Me.TablaC.CurrentRow.Index).Value > 0 Then

                Me.TablaC.Item(CtaAntiComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Cuenta_Anticipo()
                Me.TablaC.Columns(CtaAntiComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Else
                Me.TablaC.Item(CtaAntiComplemento.Index, Me.TablaC.CurrentRow.Index).Value = ""
            End If
            ComplemantosL(Me.TablaC.CurrentRow.Index)
        ElseIf e.KeyCode = Keys.J AndAlso e.Modifiers = Keys.Shift Then ' Ajustes
            Me.TablaC.Item(AjusComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Me.TablaC.Item(DifComplemento.Index, Me.TablaC.CurrentRow.Index).Value
            If Me.TablaC.Item(AjusComplemento.Index, Me.TablaC.CurrentRow.Index).Value <> 0 Then
                If Candado_Importe_Efectivocomple(Me.TablaC.CurrentRow.Index) = True Then ' se verifica candado
                    ' IMPORTE DE EFECTIVO Antiguo Codigo
                    Me.TablaC.Item(CtaAjusComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Cuenta_Efectivo()
                    Me.TablaC.Columns(CtaAjusComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Else
                    If MessageBox.Show("El importe de efectivo Excede los $2,000 deseas usarlo?", Eventos.Titulo_APP, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Me.TablaC.Item(CtaAjusComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Cuenta_Efectivo()
                        Me.TablaC.Columns(CtaAjusComplemento.Index).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                    Else
                        Me.TablaC.Item(AjusComplemento.Index, Me.TablaC.CurrentRow.Index).Value = 0
                        Exit Sub
                    End If
                End If
            Else
                Me.TablaC.Item(CtaAjusComplemento.Index, Me.TablaC.CurrentRow.Index).Value = ""
            End If
            ComplemantosL(Me.TablaC.CurrentRow.Index)
        ElseIf e.KeyCode = Keys.P AndAlso e.Modifiers = Keys.Shift Then ' Provision
            Me.TablaC.Item(ImpProviComplemento.Index, Me.TablaC.CurrentRow.Index).Value = Me.TablaC.Item(DifComplemento.Index, Me.TablaC.CurrentRow.Index).Value
            ComplemantosL(Me.TablaC.CurrentRow.Index)
        End If
        If e.KeyCode = Keys.A AndAlso e.Modifiers = Keys.Control Then
            'Programar calculo automatico de numeros de poliza
            Dim Inicial As Integer = InputBox("Teclea el numero de poliza Inicial:", Eventos.titulo_app, 1)
            For Each Fila As DataGridViewRow In TablaC.Rows
                If Fila.Cells(NumPolComplemento.Index).Selected = True Then
                    Fila.Cells(NumPolComplemento.Index).Value = Inicial
                    ComplemantosL(Fila.Index)
                    Inicial += 1
                End If
            Next
        End If
        'Liberar_ProcesoComple(Me.TablaC.CurrentRow.Index)
    End Sub

    Private Sub Buscador1_Buscar() Handles Buscador1.Buscar
        'Por defecto, indico buscar en la primera columna
        Dim indiceColumna As Integer = Me.TablaImportar.CurrentCell.ColumnIndex
        Dim Busqueda As String = Trim(UCase(Me.Buscador1.TxtFiltro.Text.ToLower))
        Dim cadena As String = ""
        Dim row As DataGridViewRow
        'Recorro filas del DataGridView
        For i As Integer = Me.TablaImportar.CurrentRow.Index To Me.TablaImportar.Rows.Count - 1
            row = Me.TablaImportar.Rows(i)
            cadena = Trim(UCase(row.Cells(indiceColumna).Value))
            'Si el contenido de la columna coinside con el valor del TextBox
            If cadena Like "*" & Busqueda & "*" Then
                'Selecciono fila y abandono bucle
                ' row.Selected = True
                If Me.TablaImportar.CurrentRow.Index = row.Index Then
                    Me.TablaImportar.Rows(row.Index).Cells(indiceColumna).Selected = True
                    Me.TablaImportar.CurrentCell = Me.TablaImportar.Rows(row.Index).Cells(indiceColumna)
                Else
                    Me.TablaImportar.Rows(row.Index).Cells(indiceColumna).Selected = True
                    Me.TablaImportar.CurrentCell = Me.TablaImportar.Rows(row.Index).Cells(indiceColumna)
                    Exit For
                End If

            End If
        Next
    End Sub

    Private Sub Buscador2_Buscar() Handles Buscador2.Buscar
        Dim indiceColumna As Integer = Me.TablaD.CurrentCell.ColumnIndex
        Dim Busqueda As String = Trim(UCase(Me.Buscador2.TxtFiltro.Text.ToLower))
        Dim cadena As String = ""
        Dim row As DataGridViewRow
        For i As Integer = Me.TablaD.CurrentRow.Index To Me.TablaD.Rows.Count - 1
            row = Me.TablaD.Rows(i)
            cadena = Trim(UCase(row.Cells(indiceColumna).Value))
            If cadena Like "*" & Busqueda & "*" Then
                If Me.TablaD.CurrentRow.Index = row.Index Then
                    Me.TablaD.Rows(row.Index).Cells(indiceColumna).Selected = True
                    Me.TablaD.CurrentCell = Me.TablaD.Rows(row.Index).Cells(indiceColumna)
                Else
                    Me.TablaD.Rows(row.Index).Cells(indiceColumna).Selected = True
                    Me.TablaD.CurrentCell = Me.TablaD.Rows(row.Index).Cells(indiceColumna)
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub Buscador3_Buscar() Handles Buscador3.Buscar
        Dim indiceColumna As Integer = Me.TablaC.CurrentCell.ColumnIndex
        Dim Busqueda As String = Trim(UCase(Me.Buscador3.TxtFiltro.Text.ToLower))
        Dim cadena As String = ""
        Dim row As DataGridViewRow
        For i As Integer = Me.TablaC.CurrentRow.Index To Me.TablaC.Rows.Count - 1
            row = Me.TablaC.Rows(i)
            cadena = Trim(UCase(row.Cells(indiceColumna).Value))
            If cadena Like "*" & Busqueda & "*" Then
                If Me.TablaC.CurrentRow.Index = row.Index Then
                    Me.TablaC.Rows(row.Index).Cells(indiceColumna).Selected = True
                    Me.TablaC.CurrentCell = Me.TablaC.Rows(row.Index).Cells(indiceColumna)
                Else
                    Me.TablaC.Rows(row.Index).Cells(indiceColumna).Selected = True
                    Me.TablaC.CurrentCell = Me.TablaC.Rows(row.Index).Cells(indiceColumna)
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub TablaD_KeyDown(sender As Object, e As KeyEventArgs) Handles TablaD.KeyDown
        If e.KeyCode = Keys.E AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaD.Item(ImpED.Index, Me.TablaD.CurrentRow.Index).Value = Me.TablaD.Item(DifD.Index, Me.TablaD.CurrentRow.Index).Value
        ElseIf e.KeyCode = Keys.T AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaD.Item(ImpTD.Index, Me.TablaD.CurrentRow.Index).Value = Me.TablaD.Item(DifD.Index, Me.TablaD.CurrentRow.Index).Value

        ElseIf e.KeyCode = Keys.C AndAlso e.Modifiers = Keys.Shift Then
            Me.TablaD.Item(ImpCHD.Index, Me.TablaD.CurrentRow.Index).Value = Me.TablaD.Item(DifD.Index, Me.TablaD.CurrentRow.Index).Value

        ElseIf e.KeyCode = Keys.A AndAlso e.Modifiers = Keys.Shift Then ' anticipos
            Me.TablaD.Item(AntiD.Index, Me.TablaD.CurrentRow.Index).Value = Me.TablaD.Item(DifD.Index, Me.TablaD.CurrentRow.Index).Value
        End If
        If e.KeyCode = Keys.A AndAlso e.Modifiers = Keys.Control Then
            'Programar calculo automatico de numeros de poliza
            Dim Inicial As Integer = InputBox("Teclea el numero de poliza Inicial:", Eventos.titulo_app, 1)
            For Each Fila As DataGridViewRow In TablaD.Rows
                If Fila.Cells(NPolD.Index).Selected = True Then
                    Fila.Cells(NPolD.Index).Value = Inicial
                    Inicial += 1
                End If
            Next
        End If
        Liberar_ProcesoDev(Me.TablaD.CurrentRow.Index)
    End Sub

    Private Sub CrearPolizaManualToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CrearPolizaManualToolStripMenuItem.Click
        Eventos.Abrir_form(Control_de_Polizas)
    End Sub



    Private Sub TablaC_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaC.DataError

    End Sub

    Private Sub TablaD_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaD.DataError

    End Sub

    Private Sub TablaImportar_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaImportar.DataError

    End Sub

    Private Sub TablaImportar_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellDoubleClick
        Try
            If Me.TablaImportar.CurrentCell.ColumnIndex = ContabilizacionC.Index Then

                ' creamos el control comboBox   And CargoG =1 And CargoE = 1 And Negativo=0 
                Dim Combo As New DataGridViewComboBoxCell
                Dim Sql = "Select Rtrim(Clave) as Clave from ClaveEgresos where ClaveEgresos.Id_Empresa = " & Me.lstCliente.SelectItem & " "

                Dim RP = Eventos.Obtener_DS(Sql)
                If RP.Tables(0).Rows.Count > 0 Then
                    Try
                        For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                            Combo.Items.Add(Trim(RP.Tables(0).Rows(i)("Clave")))
                        Next
                        Combo.ReadOnly = False
                        Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index) = Combo.Clone
                        Me.TablaImportar.Item(ContabilizacionC.Index, Me.TablaImportar.CurrentRow.Index).ReadOnly = False
                        Dim ee As New System.Windows.Forms.DataGridViewCellEventArgs(TipoPoliza.Index, Me.TablaImportar.CurrentRow.Index)
                        Me.TablaImportar_CellEndEdit(Me.TablaImportar, ee)
                    Catch ex As Exception

                    End Try
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SegundoPlano_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP1.DoWork
        My.Forms.Inicio.txtServerDB.Text = serV
        Buscar_Facturas(Dato)
        Cargar_valores_contables()
        Color_Columnas()
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Cargando Valores predefinidos por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.TablaImportar.Rows.Count
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            Liberar_Proceso(i)
            Cargar_Pol_Modelo(Me.TablaImportar.Item(RFCE.Index, i).Value, Me.TablaImportar.Item(Fecha_Emi.Index, i).Value, i)
            frm.Barra.Value = i
        Next
        frm.Close()
        My.Forms.Inicio.txtServerDB.Text = serV
        'Dim Sql = "SELECT Tipos_Poliza_Sat.Clave + ' - '+ Tipos_Poliza_Sat.Nombre  AS Tipo, max(convert(INT ,Polizas.Num_Pol,103)) AS Ultima_Poliza 
        '            FROM Polizas 
        '            INNER JOIN Tipos_Poliza_Sat ON Tipos_Poliza_Sat.Id_Tipo_Pol_Sat = Polizas.Id_Tipo_Pol_Sat 
        '            WHERE Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " AND Polizas.ID_anio = " & Me.Dtfin.Value.ToString.Substring(6, 4) & " AND Polizas.ID_mes =" & Me.Dtfin.Value.ToString.Substring(3, 2) & "
        '             GROUP BY Tipos_Poliza_Sat.Clave,Tipos_Poliza_Sat.Nombre ORDER BY Tipo"
        'Me.Tipos_Polizas1.Datos = Sql
        'Me.Tipos_Polizas1.Mostrar_Datos()

        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

    End Sub

    Private Sub SP2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP2.DoWork
        My.Forms.Inicio.txtServerDB.Text = serV
        Complementos(DatoC)
        Cargar_valores_contablesC()
        Color_ColumnasC()
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Verificando informacion de pagos por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.TablaC.Rows.Count
        For i As Integer = 0 To Me.TablaC.Rows.Count - 1
            Liberar_ProcesoComple(i)
            frm.Barra.Value = i
        Next
        frm.Close()
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
    End Sub

    Private Sub RadPanel1_Paint(sender As Object, e As PaintEventArgs) Handles RadPanel1.Paint

    End Sub
End Class