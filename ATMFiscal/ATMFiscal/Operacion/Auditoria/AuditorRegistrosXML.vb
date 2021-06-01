

Imports Telerik.WinControls
Public Class AuditorRegistrosXML
    Dim dt As New DataTable()
    Dim Sin As New DataTable()
    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Private BindingSource1 As Windows.Forms.BindingSource = New BindingSource
    Dim Regis As New List(Of Registros)
    Dim Corregir As New List(Of Correccion)
    Dim CorregirOts As New List(Of Correccion)
    Dim fil As String
    Enum e_FILTER_OPTION
        SIN_FILTRO = 0
        CADENA_QUE_COMIENCE_CON = 1
        CADENA_QUE_NO_COMIENCE_CON = 2
        CADENA_QUE_CONTENGA = 3
        CADENA_QUE_NO_CONTENGA = 4
        CADENA_IGUAL = 5
    End Enum
    Private Sub AuditorRegistrosXML_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.lstClientesMasivos.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstClientesMasivos.SelectItem = My.Forms.Inicio.Clt
        Diseño()
    End Sub

    Private Sub Diseño()
        Eventos.DiseñoTablaEnca(Me.TablaImportar)
    End Sub
    Private Sub Buscar()
        If Me.lstClientesMasivos.SelectText <> "" Then
            Dim Sql As String = " SELECT Xml_Sat.UUID , Xml_Sat.SubTotal AS [ST SAT] ,Xml_Sat.IVA_16 AS [I16 SAT],Xml_Sat.IVA_8 AS [I8 SAT] ,Xml_Sat.Total_IEPS AS [IEPS SAT], "
            Sql &= " Xml_Sat.Retenido_IVA AS [RIVA SAT],Xml_Sat.Retenido_ISR AS [RISR SAT] ,Xml_Sat.Total AS [Total SAT],XmlAuditados.Orden  ,"
            Sql &= " XmlAuditados.ImpGrabado AS [GA AUD],XmlAuditados.ImpExcento AS [EX AUD],XmlAuditados.ImpCero AS [CE AUD] ,"
            Sql &= " XmlAuditados.Tasa AS [Tasa AUD],XmlAuditados.ImpIva AS [IVA AUD],XmlAuditados.Total AS [Total AUD]"
            Sql &= " FROM XmlAuditados  "
            Sql &= " LEFT OUTER JOIN Xml_Sat ON Xml_Sat.UUID = XmlAuditados.UUID "
            Sql &= " WHERE Fecha_Emision >= " & Eventos.Sql_hoy(Me.DtInicio.Value.ToString()) & " and Fecha_Emision <= " & Eventos.Sql_hoy(Dtfin.Value.ToString()) & ""
            Dim ds As DataSet = Eventos.Obtener_DS(Sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.TablaImportar.DataSource = ds.Tables(0).DefaultView
            End If
        Else
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        Me.CmdLimpiar.PerformClick()
        Me.Cmd_Procesar.Enabled = False
        Buscartusa()
    End Sub
    Private Sub QuitacuentasceroBalanzaNormal()
        Dim filas As Integer = Me.TablaImportar.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaImportar.RowCount - 1
                If Convert.ToDecimal(Me.TablaImportar.Item(13, i).Value) <= 1 Then
                    Me.TablaImportar.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next

    End Sub
    Private Sub Buscartusa()
        If Me.TablaImportar.Columns.Count > 0 Then
            Me.TablaImportar.Columns.Clear()
            Regis.Clear()
        End If

        Dim Listas() As String
        Dim Sql As String = " SELECT XmlAuditados.UUID AS UUID,XmlAuditados.ImpGrabado,XmlAuditados.ImpExcento,XmlAuditados.ImpIVa, "
        Sql &= " XmlAuditados.Total,XmlAuditados.Id_PolizaS,XmlAuditados.Tasa,XmlAuditados.Emisor,XmlAuditados.Nombre,XmlAuditados.Fecha,XmlAuditados.Metodo From XmlAuditados  "
        Sql &= " WHERE Fecha >= " & Eventos.Sql_hoy(Me.DtInicio.Value.ToString()) & " and Fecha <= " & Eventos.Sql_hoy(Dtfin.Value.ToString()) & " Order By XmlAuditados.UUID"
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    If Listas Is Nothing Then
                        ReDim Preserve Listas(0)
                        Listas(UBound(Listas)) = "'" & ds.Tables(0).Rows(i)(0) & "'"
                    Else
                        ReDim Preserve Listas(UBound(Listas) + 1)
                        Listas(UBound(Listas)) = "'" & ds.Tables(0).Rows(i)(0) & "'"
                    End If
                    Regis.Add(New Registros With {.UUID = ds.Tables(0).Rows(i)("UUID"),
                              .Grabado = ds.Tables(0).Rows(i)("ImpGrabado"),
                              .Exento = ds.Tables(0).Rows(i)("ImpExcento"),
                              .IVAa = ds.Tables(0).Rows(i)("ImpIVa"),
                              .Total = ds.Tables(0).Rows(i)("Total"),
                              .PolizaAtm = IIf(IsDBNull(ds.Tables(0).Rows(i)("Id_PolizaS")), "", ds.Tables(0).Rows(i)("Id_PolizaS")),
                              .Tasa = ds.Tables(0).Rows(i)("Tasa"),
                              .Emisor = ds.Tables(0).Rows(i)("Emisor"),
                              .Nombre = ds.Tables(0).Rows(i)("Nombre"), .Fecha = ds.Tables(0).Rows(i)("Fecha"), .Metodo = ds.Tables(0).Rows(i)("Metodo")})
                Next
            Catch ex As Exception

            End Try


        End If
        Try
            Dim Complemanto As String = ""
            If Me.ChkCompari.Checked = True Then
                Complemanto = " > 1 "
            Else
                Complemanto = " = 1 "
            End If
            Sql = "SELECT Registro,XMLPolizas.Id_orden as Orden ,Personal.ID_matricula as Matricula,"
            Sql &= " Personal.Nombres + ' ' + Personal.Ap_Paterno + ' ' + Personal.Ap_Materno  as Operador ,Personal.Reg_fed_causantes, "
            Sql &= " case  Tipo When 1 then 'Combustible' when 2 then 'Combustible Extra' when 3 then 'Peaje' when 4 then 'Alimentos' WHEN 5 THEN  'Pasaje' WHEN 6 THEN 'Otros' END as [Tipo de Gasto],"
            Sql &= " Emisor, Receptor, Total, Iva, XMLPolizas.UUDI as UUID "
            Sql &= " FROM XMLPolizas INNER JOIN  Orden_traslados ON Orden_traslados.ID_orden = XMLPolizas.Id_orden INNER JOIN Personal ON Personal.ID_matricula = Orden_traslados.ID_matricula"
            Sql &= " INNER JOIN "
            Sql &= " (SELECT XMLPolizas.uudi,count(Xmlpolizas.UUDI )AS Veces FROM XMLPolizas GROUP BY UUDI)"
            Sql &= "  AS X ON X.UUDI = XMLPolizas.uudi"
            Sql &= "  WHERE XMLPolizas.UUDI IN (" & String.Join(",", Listas) & ") And X.Veces  " & Complemanto & " order by  UUID,XMLPolizas.Id_ORDEN "
            Dim dstusa As DataSet = Eventos.Obtener_DSTusa(Sql)
            If dstusa.Tables(0).Rows.Count > 0 Then
                Me.TablaImportar.DataSource = dstusa.Tables(0).DefaultView
                BindingSource1.DataSource = dstusa.Tables(0).DefaultView
                Dim col6 As New DataGridViewTextBoxColumn
                col6.HeaderText = "Tasa"
                col6.Name = "Tasa"
                Me.TablaImportar.Columns.Add(col6)

                Dim col As New DataGridViewTextBoxColumn
                col.HeaderText = "Total Auditado"
                col.Name = "Auditado"
                Me.TablaImportar.Columns.Add(col)

                Dim col2 As New DataGridViewTextBoxColumn
                col2.HeaderText = "IVA Auditado"
                col2.Name = "IVAa"
                Me.TablaImportar.Columns.Add(col2)

                Dim col3 As New DataGridViewTextBoxColumn
                col3.HeaderText = "Grabado Auditado"
                col3.Name = "Grabado"
                Me.TablaImportar.Columns.Add(col3)

                Dim col4 As New DataGridViewTextBoxColumn
                col4.HeaderText = "Excento Auditado"
                col4.Name = "Exento"
                Me.TablaImportar.Columns.Add(col4)

                Dim col5 As New DataGridViewTextBoxColumn
                col5.HeaderText = "Diferencia"
                col5.Name = "Diferencia"
                Me.TablaImportar.Columns.Add(col5)


                Dim col7 As New DataGridViewTextBoxColumn
                col7.HeaderText = "Poliza ATM"
                col7.Name = "Patm"
                Me.TablaImportar.Columns.Add(col7)

                Dim col8 As New DataGridViewTextBoxColumn
                col8.HeaderText = "Emisor"
                col8.Name = "Emi"
                Me.TablaImportar.Columns.Add(col8)

                Dim col9 As New DataGridViewTextBoxColumn
                col9.HeaderText = "Nombre Emisor"
                col9.Name = "Nemi"
                Me.TablaImportar.Columns.Add(col9)


                Dim Col10 As New DataGridViewTextBoxColumn
                Col10.HeaderText = "Costo de Caja"
                Col10.Name = "CC"
                Me.TablaImportar.Columns.Add(Col10)
                Dim Col11 As New DataGridViewTextBoxColumn
                Col11.HeaderText = "Fecha Timbrado"
                Col11.Name = "Fecha"
                Me.TablaImportar.Columns.Add(Col11)

                Dim Col12 As New DataGridViewTextBoxColumn
                Col12.HeaderText = "Metodo"
                Col12.Name = "Metodo"
                Me.TablaImportar.Columns.Add(Col12)

                SP1.RunWorkerAsync(Me.TablaImportar)
                Control.CheckForIllegalCrossThreadCalls = False
                Me.TablaImportar.Enabled = True





            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BackgroundWorker_RunWorkerCompleted(sender As System.Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles SP1.RunWorkerCompleted
        Dim uuid As String = ""
        If e.Cancelled Then
            MessageBox.Show("La acción ha sido cancelada.")
        ElseIf e.Error IsNot Nothing Then
            MessageBox.Show("Se ha producido un error durante la ejecución: " & e.Error.Message)
        Else
            If Me.ChkCompari.Checked = True Then
                For j As Integer = 0 To Me.TablaImportar.RowCount - 1
                    Try
                        If UCase(Me.TablaImportar.Item(10, j).Value.ToString) = UCase(Me.TablaImportar.Item(10, j - 1).Value.ToString) And (UCase(Me.TablaImportar.Item(2, j).Value.ToString) = UCase(Me.TablaImportar.Item(2, j - 1).Value.ToString)) Then
                            If Me.TablaImportar.Rows(j).DefaultCellStyle.BackColor <> Color.Purple Then
                                If Me.TablaImportar.Rows(j - 1).DefaultCellStyle.BackColor <> Color.LawnGreen And uuid <> UCase(Me.TablaImportar.Item(10, j).Value.ToString) Then
                                    Me.TablaImportar.Rows(j).DefaultCellStyle.BackColor = Color.LawnGreen
                                    uuid = UCase(Me.TablaImportar.Item(10, j).Value.ToString)
                                End If

                            End If

                        ElseIf UCase(Me.TablaImportar.Item(10, j).Value.ToString) = UCase(Me.TablaImportar.Item(10, j - 1).Value.ToString) And (UCase(Me.TablaImportar.Item(2, j).Value.ToString) <> UCase(Me.TablaImportar.Item(2, j - 1).Value.ToString)) Then
                            For s As Integer = 0 To Me.TablaImportar.RowCount - 1
                                Try
                                    If UCase(Me.TablaImportar.Item(10, s).Value.ToString) = UCase(Me.TablaImportar.Item(10, j).Value.ToString) Then
                                        Me.TablaImportar.Rows(s).DefaultCellStyle.BackColor = Color.Purple
                                    End If
                                Catch ex As Exception
                                End Try
                            Next
                        End If
                    Catch ex As Exception
                        If j = 0 Then
                            Me.TablaImportar.Rows(j).DefaultCellStyle.BackColor = Color.Green
                        End If
                    End Try
                Next
            End If
            MessageBox.Show("La acción en segundo plano ha finalizado con éxito.")
            Me.Cmd_Procesar.Enabled = True
        End If

    End Sub
    Public Class Registros
        Public Property UUID As String
        Public Property Total As String
        Public Property Grabado As String
        Public Property Exento As String
        Public Property IVAa As String
        Public Property PolizaAtm As String
        Public Property Tasa As String
        Public Property Emisor As String
        Public Property Nombre As String
        Public Property Fecha As String
        Public Property Metodo As String
    End Class

    Public Class Correccion
        Public Property UUID As String
        Public Property AbonoO As Decimal
        Public Property CajaCosto As Decimal
        Public Property Grabado As Decimal
        Public Property Exento As Decimal
        Public Property IVAa As Decimal
        Public Property PolizaAtm As String
        Public Property Tasa As String
        Public Property Matricula As String

        Public Property TipoGasto As String

        Public Property Emisor As String
        Public Property Nombre As String

        Public Property RFC_Deudor As String
        Public Property Nombres_Deudor As String
        Public Property Fecha As String
        Public Property OT As String
        Public Property Metodo As String

    End Class
    Public Class Operadores
        Public Property Matricula As String

        Public Property Importe As Decimal
        Public Property RFC_Deudor As String
        Public Property Nombres_Deudor As String

    End Class
    Public Class CorreccionOt
        Public Property UUID As String
        Public Property AbonoO As Decimal
        Public Property CajaCosto As Decimal
        Public Property Grabado As Decimal
        Public Property Exento As Decimal
        Public Property IVAa As Decimal
        Public Property PolizaAtm As String
        Public Property Tasa As String
        Public Property Matricula As String
        Public Property TipoGasto As String
        Public Property Emisor As String
        Public Property Nombre As String
        Public Property RFC_Deudor As String
        Public Property Nombres_Deudor As String
        Public Property Fecha As String
        Public Property OT As String
        Public Property Metodo As String

    End Class

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        If Me.TablaImportar.Columns.Count > 0 Then
            Me.TablaImportar.Columns.Clear()
        End If
        If dt.Rows.Count > 0 Then
            dt.Rows.Clear()
        End If
        If Sin.Rows.Count > 0 Then
            Sin.Rows.Clear()
        End If
    End Sub

    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        If Me.ChkCompari.Checked = True Then
            LLenarTablaC()
        Else
            LLenarTabla()
        End If

    End Sub

    Private Sub LLenarTabla()
        Dim salto As Integer = 0
        Dim Pol As String = ""
        Dim Sql As String = ""
        Dim where As String = ""
        Dim Polizas() As String

        If Corregir.Count > 0 Then
            Corregir.Clear()
        End If


        Dim i As Integer = 0
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Calculando la Información por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.TablaImportar.Rows.Count

        For Each Row As DataRow In dt.Rows

            If Polizas Is Nothing Then
                ReDim Preserve Polizas(0)
                If Row.Item(17) <> "" Then
                    Polizas(UBound(Polizas)) = "'" & Row.Item(17) & "'"
                End If
            Else
                If Row.Item(17) <> "" Then
                    ReDim Preserve Polizas(UBound(Polizas) + 1)
                    Polizas(UBound(Polizas)) = "'" & Row.Item(17) & "'"
                End If

            End If


            Corregir.Add(New Correccion With {.CajaCosto = IIf(IsDBNull(Convert.ToDecimal(Row.Item(20))), 0, Convert.ToDecimal(Row.Item(20))),
                 .Grabado = IIf(Convert.ToDecimal(Row.Item(16)) >= -1 And Convert.ToDecimal(Row.Item(16)) <= 1, Convert.ToDecimal(Row.Item(14)) + Convert.ToDecimal(Row.Item(16)), Convert.ToDecimal(Row.Item(14))),
                 .Exento = Convert.ToDecimal(Row.Item(15)),
                 .IVAa = Convert.ToDecimal(Row.Item(13)),
                 .AbonoO = .Grabado + .IVAa + .Exento,
                 .PolizaAtm = Row.Item(17),
                 .Tasa = Convert.ToDecimal(Row.Item(11)),
                 .Matricula = Row.Item(2),
                 .TipoGasto = Row.Item(5),
                 .UUID = Row.Item(10),
                 .Emisor = Row.Item(18),
                 .Nombre = Row.Item(19),
                 .RFC_Deudor = Row.Item(4),
                 .Nombres_Deudor = Row.Item(3), .Fecha = Row.Item(21), .OT = "", .Metodo = Row.Item(22)})
            i += 1
            frm.Barra.Value = i
        Next
        frm.Close()

        where = Join(Polizas, ",")

        Sql = "Delete  from detalle_Polizas where Id_Poliza in (" & where & ")"
        Try
            If Eventos.Comando_sql(Sql) > 0 Then
                Sql = ""
                Dim Cta1, Cta2, Cta3 As String
                Dim frm4 As New BarraProcesovb
                frm4.Show()
                frm4.Text = "Editando la Información por favor espere..."
                frm4.Barra.Minimum = 0
                frm4.Barra.Maximum = Corregir.Count
                For Each Movimiento In Corregir


                    Dim Item As Integer = 1
                    If Movimiento.TipoGasto = "Combustible" Or Movimiento.TipoGasto = "Combustible Extra" Then
                        Movimiento.Grabado += Movimiento.Exento
                        Movimiento.Exento = 0
                    End If
                    If Movimiento.PolizaAtm <> "" Then


                        Try
                            Cta1 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgG", Movimiento.TipoGasto, Movimiento.Tasa), Movimiento.Emisor, Movimiento.Nombre, 1, "GG", 0)

                        Catch ex As Exception

                        End Try
                        Try
                            If Movimiento.Exento > 0 Then
                                Cta2 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgEx", Movimiento.TipoGasto, Movimiento.Tasa), Movimiento.Emisor, Movimiento.Nombre, 1, "GG", 0)
                            End If

                        Catch ex As Exception

                        End Try

                        Try
                            Cta3 = Eventos.RegresaCunetaDeudor(Eventos.Regresa_Cuenta_Series(1, "Deudor", Movimiento.TipoGasto, Movimiento.Tasa), Movimiento.RFC_Deudor, Movimiento.Nombres_Deudor, 1, "GG", Movimiento.Matricula)
                        Catch ex As Exception

                        End Try

                        Dim fecha As String = Eventos.Sql_hoy()

                        If salto = 167 Then

                            If Movimiento.Grabado > 0 Then
                                If Movimiento.Tasa = 0.0 Then
                                    Movimiento.Tasa = 0.16
                                End If
                                If Movimiento.Tasa = 0.16 Then 'cargos

                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Grabado & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                    Item += 1
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.IVAa & ", 0," & fecha & ", 'A', 1180000100020000, '')" & vbCrLf
                                    Item += 1
                                ElseIf Movimiento.Tasa = 0.08 Then 'cargos
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Grabado & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                    Item += 1
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.IVAa & ", 0," & fecha & ", 'A', 1180000100120000,'')" & vbCrLf
                                    Item += 1
                                End If
                            End If

                            If Movimiento.Exento > 0 And Movimiento.Grabado = 0 Then 'cargo
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Exento & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                            ElseIf Movimiento.Exento > 0 And Movimiento.Grabado > 0 Then
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Exento & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                            End If


                            If Movimiento.AbonoO > 0 Then 'Abono
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  0," & Movimiento.AbonoO & "," & fecha & ", 'A', " & Cta3 & ", '')" & vbCrLf
                            End If


                            If Movimiento.CajaCosto > 0 Then ' abono
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  0," & Movimiento.CajaCosto & "," & fecha & ", 'A',  1010000400010000, '')" & vbCrLf
                            End If

                            Sql += " Update Xml_Sat set ID_poliza ='" & Movimiento.PolizaAtm & "' WHERE UUID='" & Movimiento.UUID & "'" & vbCrLf
                            If Eventos.Comando_sql(Sql) = 0 Then
                                salto += 1

                            End If
                            Sql = ""
                            salto = 1
                        Else
                            If Movimiento.Grabado > 0 Then
                                If Movimiento.Tasa = 0.0 Then
                                    Movimiento.Tasa = 0.16
                                End If

                                If Movimiento.Tasa = 0.16 Then 'cargos

                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Grabado & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                    Item += 1
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.IVAa & ", 0," & fecha & ", 'A', 1180000100020000, '')" & vbCrLf
                                    Item += 1
                                ElseIf Movimiento.Tasa = 0.08 Then 'cargos
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Grabado & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                    Item += 1
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.IVAa & ", 0," & fecha & ", 'A', 1180000100120000, '')" & vbCrLf
                                    Item += 1
                                End If
                            End If

                            If Movimiento.Exento > 0 And Movimiento.Grabado = 0 Then 'cargo
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Exento & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                                Item += 1
                            ElseIf Movimiento.Exento > 0 And Movimiento.Grabado > 0 Then
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Exento & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                                Item += 1
                            End If
                            If Movimiento.AbonoO > 0 Then 'Abono
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  0," & Movimiento.AbonoO & "," & fecha & ", 'A', " & Cta3 & ", '')" & vbCrLf
                                Item += 1
                            End If
                            If Movimiento.CajaCosto > 0 Then ' abono
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  " & Movimiento.CajaCosto & ",0," & fecha & ", 'A', " & Cta3 & ", '')" & vbCrLf
                                Item += 1
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  0," & Movimiento.CajaCosto & "," & fecha & ", 'A',  1010000400010000, '')" & vbCrLf
                                Item += 1
                            End If

                            Sql += " Update Xml_Sat set ID_poliza ='" & Movimiento.PolizaAtm & "' WHERE UUID='" & Movimiento.UUID & "'" & vbCrLf
                            salto += 1
                        End If
                    End If
                    frm4.Barra.Value += 1
                Next
                frm4.Close()
                If salto > 0 Then
                    If Eventos.Comando_sql(Sql) = 0 Then
                        salto += 1
                    End If
                End If


            End If

        Catch ex As Exception

        End Try

        If Corregir.Count > 0 Then
            Corregir.Clear()
        End If
        i = 0
        For Each Row As DataRow In Sin.Rows

            Corregir.Add(New Correccion With {.CajaCosto = IIf(IsDBNull(Convert.ToDecimal(Row.Item(20))), 0, Convert.ToDecimal(Row.Item(20))),
                .Grabado = IIf(Convert.ToDecimal(Row.Item(16)) >= -1 And Convert.ToDecimal(Row.Item(16)) <= 1, Convert.ToDecimal(Row.Item(14)) + Convert.ToDecimal(Row.Item(16)), Convert.ToDecimal(Row.Item(14))),
                .Exento = Convert.ToDecimal(Row.Item(15)),
                .IVAa = Convert.ToDecimal(Row.Item(13)),
                .AbonoO = .Grabado + .IVAa + .Exento,
                .PolizaAtm = Row.Item(17),
                .Tasa = Convert.ToDecimal(Row.Item(11)),
                .Matricula = Row.Item(2),
                .TipoGasto = Row.Item(5),
                .UUID = Row.Item(10),
                .Emisor = Row.Item(18),
                .Nombre = Row.Item(19),
                .RFC_Deudor = Row.Item(4),
                .Nombres_Deudor = Row.Item(3), .Fecha = Row.Item(21), .OT = Row.Item(1), .Metodo = Row.Item(22)})
            i += 1

        Next
        If Corregir.Count > 0 Then
            Dim frm2 As New BarraProcesovb
            frm2.Show()
            frm2.Text = "Creando polizas por favor espere..."
            frm2.Barra.Minimum = 0
            frm2.Barra.Maximum = Corregir.Count
            For Each Movimiento In Corregir
                If Movimiento.PolizaAtm = "" Then


                    Dim Polis As String = Calcula_poliza(Movimiento.Fecha.Substring(3, 2), Movimiento.Fecha.Substring(6, 4), 1, 12)
                    Dim Posi As Integer = InStr(1, Polis, "-", CompareMethod.Binary)
                    Dim Cuantos As Integer = Len(Polis) - Len(Polis.Substring(0, Posi))
                    Dim Consecutivo As Integer = Val(Polis.Substring(Posi, Cuantos))
                    Dim Cta1, Cta2, Cta3 As String
                    Dim xmlsat = Eventos.ObtenerValorDB("Xml_Sat", "Id_Registro_Xml", "   UUID = '" & Movimiento.UUID & "' ", True)
                    If xmlsat.ToString <> " " Then
                        If Creapoliza(Polis, Movimiento.Fecha.Substring(6, 4), Movimiento.Fecha.Substring(3, 2), Movimiento.Fecha.Substring(0, 2),
                                 Consecutivo, 12, Movimiento.Fecha, "Gtos / Factura " & Movimiento.UUID, "", Consecutivo,
                                 xmlsat, Movimiento.UUID, Movimiento.OT) Then
                            Movimiento.PolizaAtm = Polis
                            'Generar detalle
                            If Buscafactura(Movimiento.UUID, "C") = True Then
                                'Se inserta la Factura
                                Eventos.Inserta_Comprobante_Fiscal(Polis, Movimiento.Fecha.Substring(6, 4), Movimiento.Fecha.Substring(3, 2),
                                     Movimiento.Emisor, Movimiento.Fecha, Movimiento.UUID, "Factura " & Trim(Movimiento.Emisor) & " C", Movimiento.AbonoO)
                            Else
                                'Se Edita la Factura
                                Eventos.Edita_Factura(Movimiento.UUID, "C", Polis)
                            End If

                            If Movimiento.Metodo = "01 - Efectivo" Then
                                Eventos.Inserta_Comprobante_Fiscal_Efectivo(Polis, Movimiento.Fecha.Substring(6, 4), Movimiento.Fecha.Substring(3, 2),
                                  Movimiento.Emisor, "001", Movimiento.Fecha,
                                  "", "", "", Movimiento.AbonoO, 1)
                            Else ' Transferencia
                                Eventos.Inserta_Comprobante_Fiscal_Transf(Polis, Movimiento.Fecha.Substring(6, 4), Movimiento.Fecha.Substring(3, 2),
                                   Movimiento.Emisor, "001", Movimiento.Fecha,
                                  "", "", "", Movimiento.AbonoO, "02", 0, 1)
                            End If


                            Dim Item As Integer = 1
                            If Movimiento.TipoGasto = "Combustible" Or Movimiento.TipoGasto = "Combustible Extra" Then
                                Movimiento.Grabado += Movimiento.Exento
                                Movimiento.Exento = 0
                            End If
                            Try
                                Cta1 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgG", Movimiento.TipoGasto, Movimiento.Tasa), Movimiento.Emisor, Movimiento.Nombre, 1, "GG", 0)

                            Catch ex As Exception

                            End Try
                            Try
                                If Movimiento.Exento > 0 Then
                                    Cta2 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgEx", Movimiento.TipoGasto, Movimiento.Tasa), Movimiento.Emisor, Movimiento.Nombre, 1, "GG", 0)
                                End If

                            Catch ex As Exception

                            End Try

                            Try
                                Cta3 = Eventos.RegresaCunetaDeudor(Eventos.Regresa_Cuenta_Series(1, "Deudor", Movimiento.TipoGasto, Movimiento.Tasa), Movimiento.RFC_Deudor, Movimiento.Nombres_Deudor, 1, "GG", Movimiento.Matricula)
                            Catch ex As Exception

                            End Try

                            Dim fecha As String = Eventos.Sql_hoy()
                            Sql = ""
                            If Movimiento.Grabado > 0 Then
                                If Movimiento.Tasa = 0.16 Then 'cargos

                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Grabado & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                    Item += 1
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.IVAa & ", 0," & fecha & ", 'A', 1180000100020000, '')" & vbCrLf
                                    Item += 1
                                ElseIf Movimiento.Tasa = 0.08 Then 'cargos
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Grabado & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                    Item += 1
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.IVAa & ", 0," & fecha & ", 'A', 1180000100120000, '')" & vbCrLf
                                    Item += 1
                                End If
                            End If

                            If Movimiento.Exento > 0 And Movimiento.Grabado = 0 Then 'cargo
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Exento & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                            ElseIf Movimiento.Exento > 0 And Movimiento.Grabado > 0 Then
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Exento & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                            End If


                            If Movimiento.AbonoO > 0 Then 'Abono
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  0," & Movimiento.AbonoO & "," & fecha & ", 'A', " & Cta3 & ", '')" & vbCrLf
                            End If


                            If Movimiento.CajaCosto > 0 Then ' abono
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  " & Movimiento.CajaCosto & ",0," & fecha & ", 'A', " & Cta3 & ", '')" & vbCrLf
                                Item += 1
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  0," & Movimiento.CajaCosto & "," & fecha & ", 'A',  1010000400010000,'')" & vbCrLf
                            End If

                            Sql += " Update Xml_Sat set ID_poliza ='" & Movimiento.PolizaAtm & "' WHERE UUID='" & Movimiento.UUID & "'" & vbCrLf
                            If Eventos.Comando_sql(Sql) = 0 Then
                                salto += 1
                            End If
                        Else

                        End If

                    End If
                End If
                frm2.Barra.Value += 1
            Next
            frm2.Close()

        End If


    End Sub
    Private Sub LLenarTablaC()
        Dim salto As Integer = 0
        Dim Pol As String = ""
        Dim Sql As String = ""
        Dim where As String = ""
        Dim Ots() As String
        Dim Opera() As String
        Dim polizas() As String

        Dim Dividida As String = ""
        If CorregirOts IsNot Nothing Then
            If CorregirOts.Count > 0 Then
                CorregirOts.Clear()
            End If
        End If
        Dim Frm2 As New BarraProcesovb
        Frm2.Show()
        Frm2.Text = "Calculando movimientos combinados..."
        Frm2.Barra.Minimum = 0
        Frm2.Barra.Maximum = Me.TablaImportar.RowCount
        For i As Integer = 0 To Me.TablaImportar.RowCount - 1
            Dim Row As DataGridViewRow = Me.TablaImportar.Rows(i)
            If Row.DefaultCellStyle.BackColor = Color.LawnGreen Then

                Dim entro As String = Row.Cells(10).Value
                'Editar poliza
                Dim leyenda As String = ""
                If Ots.Count > 0 Then
                    Ots = Nothing
                End If
                If polizas Is Nothing Then
                    ReDim Preserve polizas(0)
                    If Row.Cells(17).Value <> "" Then
                        polizas(UBound(polizas)) = "'" & Row.Cells(17).Value & "'"
                    End If
                Else
                    If Row.Cells(17).Value <> "" Then
                        ReDim Preserve polizas(UBound(polizas) + 1)
                        polizas(UBound(polizas)) = "'" & Row.Cells(17).Value & "'"
                    End If

                End If
                For s As Integer = 0 To Me.TablaImportar.RowCount - 1
                    If UCase(Me.TablaImportar.Item(10, s).Value) = entro Then

                        If Ots Is Nothing Then
                            ReDim Preserve Ots(0)
                            If Me.TablaImportar.Item(1, s).Value <> "" Then
                                Ots(UBound(Ots)) = Me.TablaImportar.Item(1, s).Value
                            End If
                        Else
                            If Row.Cells(1).Value <> "" Then
                                ReDim Preserve Ots(UBound(Ots) + 1)
                                Ots(UBound(Ots)) = Me.TablaImportar.Item(1, s).Value
                            End If

                        End If
                        leyenda = Join(Ots, "/")
                    End If
                Next
                CorregirOts.Add(New Correccion With {.CajaCosto = IIf(IsDBNull(Convert.ToDecimal(Row.Cells(20).Value)), 0, Convert.ToDecimal(Row.Cells(20).Value)),
                 .Grabado = IIf(Convert.ToDecimal(Row.Cells(16).Value) >= -1 And Convert.ToDecimal(Row.Cells(16).Value) <= 1, Convert.ToDecimal(Row.Cells(14).Value) + Convert.ToDecimal(Row.Cells(16).Value), Convert.ToDecimal(Row.Cells(14).Value)),
                 .Exento = Convert.ToDecimal(Row.Cells(15).Value),
                 .IVAa = Convert.ToDecimal(Row.Cells(13).Value),
                 .AbonoO = .Grabado + .IVAa + .Exento,
                 .PolizaAtm = Row.Cells(17).Value,
                 .Tasa = Convert.ToDecimal(Row.Cells(11).Value),
                 .Matricula = Row.Cells(2).Value,
                 .TipoGasto = Row.Cells(5).Value,
                 .UUID = Row.Cells(10).Value,
                 .Emisor = Row.Cells(18).Value,
                 .Nombre = Row.Cells(19).Value,
                 .RFC_Deudor = Row.Cells(4).Value,
                 .Nombres_Deudor = Row.Cells(3).Value, .Fecha = Row.Cells(21).Value, .OT = Row.Cells(1).Value, .Metodo = Row.Cells(22).Value})






            ElseIf Row.DefaultCellStyle.BackColor = Color.Purple Then

                If Row.Cells(17).Value = "" Then
                    Dim leyenda As String = ""
                    Dim entro As String = Row.Cells(10).Value
                    If Ots IsNot Nothing Then
                        If Ots.Count > 0 Then
                            Ots = Nothing
                        End If
                    End If
                    Dim Operador As New List(Of Operadores)

                    Dim Anterior As Integer = 0
                    For s As Integer = 0 To Me.TablaImportar.RowCount - 1
                        If UCase(Me.TablaImportar.Item(10, s).Value) = entro Then

                            If Ots Is Nothing Then
                                ReDim Preserve Ots(0)
                                If Me.TablaImportar.Item(1, s).Value <> "" Then
                                    Ots(UBound(Ots)) = Me.TablaImportar.Item(1, s).Value
                                End If
                            Else
                                If Row.Cells(1).Value <> "" Then
                                    If Ots.Contains(Me.TablaImportar.Item(1, s).Value) = False Then
                                        ReDim Preserve Ots(UBound(Ots) + 1)
                                        Ots(UBound(Ots)) = Me.TablaImportar.Item(1, s).Value
                                    End If
                                End If
                            End If
                            Dim valor As Operadores


                            valor = Operador.Find(Function(W) W.Matricula = Me.TablaImportar.Item(2, s).Value.ToString.Trim())

                            If Row.Cells(2).Value <> "" Then
                                If valor IsNot Nothing Then
                                    If valor.Matricula <> Me.TablaImportar.Item(2, s).Value Then
                                        Operador.Add(New Operadores With {.Matricula = Me.TablaImportar.Item(2, s).Value, .Importe = Convert.ToDecimal(Me.TablaImportar.Item(8, s).Value), .RFC_Deudor = Me.TablaImportar.Item(4, s).Value, .Nombres_Deudor = Me.TablaImportar.Item(3, s).Value})
                                    Else
                                        Operador.Item(Operador.FindIndex(Function(W) W.Matricula = Me.TablaImportar.Item(2, s).Value.ToString.Trim())).Importe += Convert.ToDecimal(Me.TablaImportar.Item(8, s).Value)
                                    End If
                                Else
                                    Operador.Add(New Operadores With {.Matricula = Me.TablaImportar.Item(2, s).Value, .Importe = Convert.ToDecimal(Me.TablaImportar.Item(8, s).Value), .RFC_Deudor = Me.TablaImportar.Item(4, s).Value, .Nombres_Deudor = Me.TablaImportar.Item(3, s).Value})
                                End If

                            End If

                            leyenda = Join(Ots, "/")



                            'Cambiamos el color para que no se dupliquen
                            Me.TablaImportar.Rows(s).DefaultCellStyle.BackColor = Color.AliceBlue
                        End If
                    Next


                    Dim Polis As String = Calcula_poliza(Me.TablaImportar.Item(21, i).Value.ToString().Substring(3, 2), Me.TablaImportar.Item(21, i).Value.ToString.Substring(6, 4), 1, 12)
                    Dim Posi As Integer = InStr(1, Polis, "-", CompareMethod.Binary)
                    Dim Cuantos As Integer = Len(Polis) - Len(Polis.Substring(0, Posi))
                    Dim Consecutivo As Integer = Val(Polis.Substring(Posi, Cuantos))
                    Dim Cta1, Cta2, Cta3 As String
                    Dim xmlsat = Eventos.ObtenerValorDB("Xml_Sat", "Id_Registro_Xml", "   UUID = '" & Me.TablaImportar.Item(10, i).Value & "' ", True)
                    If xmlsat.ToString <> " " Then
                        If Creapoliza(Polis, Me.TablaImportar.Item(21, i).Value.ToString().Substring(6, 4), Me.TablaImportar.Item(21, i).Value.ToString().Substring(3, 2), Me.TablaImportar.Item(21, i).Value.ToString().Substring(0, 2),
                                 Consecutivo, 12, Me.TablaImportar.Item(21, i).Value.ToString(), "Gtos / Factura " & Me.TablaImportar.Item(10, i).Value.ToString(), " / Multipoliza", Consecutivo,
                                 xmlsat, Me.TablaImportar.Item(10, i).Value.ToString(), Me.TablaImportar.Item(1, i).Value.ToString()) Then

                            'Generar detalle
                            If Buscafactura(Me.TablaImportar.Item(10, i).Value.ToString(), "C") = True Then
                                'Se inserta la Factura
                                Eventos.Inserta_Comprobante_Fiscal(Polis, Me.TablaImportar.Item(21, i).Value.ToString().Substring(6, 4), Me.TablaImportar.Item(21, i).Value.ToString().Substring(3, 2),
                                     Me.TablaImportar.Item(18, i).Value.ToString(), Me.TablaImportar.Item(21, i).Value.ToString(), Me.TablaImportar.Item(10, i).Value.ToString(), "Factura " & Trim(Me.TablaImportar.Item(18, i).Value.ToString()) & " C", Me.TablaImportar.Item(12, i).Value.ToString())
                            Else
                                'Se Edita la Factura
                                Eventos.Edita_Factura(Me.TablaImportar.Item(10, i).Value.ToString(), "C", Polis)
                            End If

                            If Me.TablaImportar.Item(22, i).Value.ToString() = "01 - Efectivo" Then
                                Eventos.Inserta_Comprobante_Fiscal_Efectivo(Polis, Me.TablaImportar.Item(21, i).Value.ToString().Substring(6, 4), Me.TablaImportar.Item(21, i).Value.ToString().Substring(3, 2),
                                  Me.TablaImportar.Item(18, i).Value.ToString(), "001", Me.TablaImportar.Item(21, i).Value.ToString(),
                                  "", "", "", Me.TablaImportar.Item(12, i).Value.ToString(), 1)
                            Else ' Transferencia
                                Eventos.Inserta_Comprobante_Fiscal_Transf(Polis, Me.TablaImportar.Item(21, i).Value.ToString().Substring(6, 4), Me.TablaImportar.Item(21, i).Value.ToString().Substring(3, 2),
                                   Me.TablaImportar.Item(18, i).Value.ToString(), "001", Me.TablaImportar.Item(21, i).Value.ToString(),
                                  "", "", "", Me.TablaImportar.Item(12, i).Value.ToString(), "02", 0, 1)
                            End If


                            Dim Item As Integer = 1
                            Dim G, e As Decimal
                            G = IIf(Convert.ToDecimal(Me.TablaImportar.Item(16, i).Value) >= -1 And Convert.ToDecimal(Me.TablaImportar.Item(16, i).Value) <= 1, Convert.ToDecimal(Me.TablaImportar.Item(14, i).Value) + Convert.ToDecimal(Me.TablaImportar.Item(16, i).Value), Convert.ToDecimal(Me.TablaImportar.Item(14, i).Value))
                            e = Convert.ToDecimal(Me.TablaImportar.Item(15, i).Value)
                            If Me.TablaImportar.Item(5, i).Value.ToString() = "Combustible" Or Me.TablaImportar.Item(5, i).Value.ToString() = "Combustible Extra" Then
                                G += e
                                e = 0
                            End If
                            Try
                                Cta1 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgG", Me.TablaImportar.Item(5, i).Value.ToString(), Me.TablaImportar.Item(11, i).Value), Me.TablaImportar.Item(18, i).Value.ToString(), Me.TablaImportar.Item(19, i).Value.ToString(), 1, "GG", 0)

                            Catch ex As Exception

                            End Try
                            Try
                                If e > 0 Then
                                    Cta2 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgEx", Me.TablaImportar.Item(5, i).Value.ToString(), Me.TablaImportar.Item(11, i).Value), Me.TablaImportar.Item(18, i).Value.ToString(), Me.TablaImportar.Item(19, i).Value.ToString(), 1, "GG", 0)
                                End If

                            Catch ex As Exception

                            End Try



                            Dim fecha As String = Eventos.Sql_hoy()
                            Sql = ""
                            Dim t As Decimal = Convert.ToDecimal(Me.TablaImportar.Item(11, i).Value)
                            If G > 0 Then
                                If t = 0.16 Then 'cargos

                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Polis & "'," & Item & ", " & G & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                    Item += 1
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Polis & "'," & Item & ", " & Convert.ToDecimal(Me.TablaImportar.Item(13, i).Value) & ", 0," & fecha & ", 'A', 1180000100020000, '')" & vbCrLf
                                    Item += 1
                                ElseIf t = 0.08 Then 'cargos
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Polis & "'," & Item & ", " & G & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                    Item += 1
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Polis & "'," & Item & ", " & Convert.ToDecimal(Me.TablaImportar.Item(13, i).Value) & ", 0," & fecha & ", 'A', 1180000100120000, '')" & vbCrLf
                                    Item += 1
                                End If
                            End If

                            If e > 0 And G = 0 Then 'cargo
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Polis & "'," & Item & ", " & e & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                                Item += 1
                            ElseIf e > 0 And G > 0 Then
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Polis & "'," & Item & ", " & e & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                                Item += 1
                            End If


                            If Convert.ToDecimal(Me.TablaImportar.Item(12, i).Value) > 0 Then 'Abono operadores

                                For Each O As Operadores In Operador
                                    Try
                                        Cta3 = Eventos.RegresaCunetaDeudor(Eventos.Regresa_Cuenta_Series(1, "Deudor", Me.TablaImportar.Item(5, i).Value.ToString(), Me.TablaImportar.Item(11, i).Value), O.RFC_Deudor, O.Nombres_Deudor, 1, "GG", O.Matricula)
                                        Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                        Sql &= "  VALUES ('" & Polis & "'," & Item & ",  0," & O.Importe & "," & fecha & ", 'A', " & Cta3 & ", '')" & vbCrLf
                                        Item += 1
                                    Catch ex As Exception

                                    End Try

                                Next



                            End If

                            Sql += " Update Xml_Sat set ID_poliza ='" & Polis & "' WHERE UUID='" & Me.TablaImportar.Item(10, i).Value & "'" & vbCrLf
                            If Eventos.Comando_sql(Sql) = 0 Then
                                salto += 1
                            End If
                        Else

                        End If

                    End If


                    'codificar la poliza
                Else
                    Sql = "Delete  from detalle_Polizas where Id_Poliza in (" & Row.Cells(17).Value & ")"
                    If Eventos.Comando_sql(Sql) > 0 Then
                        Dim Polis As String = Row.Cells(17).Value
                        Dim Cta1, Cta2, Cta3 As String
                        Dim leyenda As String = ""
                        Dim entro As String = Row.Cells(10).Value
                        If Ots IsNot Nothing Then
                            If Ots.Count > 0 Then
                                Ots = Nothing
                            End If
                        End If
                        Dim Operador As New List(Of Operadores)

                        Dim Anterior As Integer = 0
                        For s As Integer = 0 To Me.TablaImportar.RowCount - 1
                            If UCase(Me.TablaImportar.Item(10, s).Value) = entro Then

                                If Ots Is Nothing Then
                                    ReDim Preserve Ots(0)
                                    If Me.TablaImportar.Item(1, s).Value <> "" Then
                                        Ots(UBound(Ots)) = Me.TablaImportar.Item(1, s).Value
                                    End If
                                Else
                                    If Row.Cells(1).Value <> "" Then
                                        If Ots.Contains(Me.TablaImportar.Item(1, s).Value) = False Then
                                            ReDim Preserve Ots(UBound(Ots) + 1)
                                            Ots(UBound(Ots)) = Me.TablaImportar.Item(1, s).Value
                                        End If
                                    End If
                                End If
                                Dim valor As Operadores


                                valor = Operador.Find(Function(W) W.Matricula = Me.TablaImportar.Item(2, s).Value.ToString.Trim())

                                If Row.Cells(2).Value <> "" Then
                                    If valor IsNot Nothing Then
                                        If valor.Matricula <> Me.TablaImportar.Item(2, s).Value Then
                                            Operador.Add(New Operadores With {.Matricula = Me.TablaImportar.Item(2, s).Value, .Importe = Convert.ToDecimal(Me.TablaImportar.Item(8, s).Value), .RFC_Deudor = Me.TablaImportar.Item(4, s).Value, .Nombres_Deudor = Me.TablaImportar.Item(3, s).Value})
                                        Else
                                            Operador.Item(Operador.FindIndex(Function(W) W.Matricula = Me.TablaImportar.Item(2, s).Value.ToString.Trim())).Importe += Convert.ToDecimal(Me.TablaImportar.Item(8, s).Value)
                                        End If
                                    Else
                                        Operador.Add(New Operadores With {.Matricula = Me.TablaImportar.Item(2, s).Value, .Importe = Convert.ToDecimal(Me.TablaImportar.Item(8, s).Value), .RFC_Deudor = Me.TablaImportar.Item(4, s).Value, .Nombres_Deudor = Me.TablaImportar.Item(3, s).Value})
                                    End If

                                End If

                                leyenda = Join(Ots, "/")



                                'Cambiamos el color para que no se dupliquen
                                Me.TablaImportar.Rows(s).DefaultCellStyle.BackColor = Color.AliceBlue
                            End If
                        Next
                        Dim Item As Integer = 1
                        Dim G, e As Decimal
                        G = IIf(Convert.ToDecimal(Me.TablaImportar.Item(16, i).Value) >= -1 And Convert.ToDecimal(Me.TablaImportar.Item(16, i).Value) <= 1, Convert.ToDecimal(Me.TablaImportar.Item(14, i).Value) + Convert.ToDecimal(Me.TablaImportar.Item(16, i).Value), Convert.ToDecimal(Me.TablaImportar.Item(14, i).Value))
                        e = Convert.ToDecimal(Me.TablaImportar.Item(15, i).Value)
                        If Me.TablaImportar.Item(5, i).Value.ToString() = "Combustible" Or Me.TablaImportar.Item(5, i).Value.ToString() = "Combustible Extra" Then
                            G += e
                            e = 0
                        End If
                        Try
                            Cta1 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgG", Me.TablaImportar.Item(5, i).Value.ToString(), Me.TablaImportar.Item(11, i).Value), Me.TablaImportar.Item(18, i).Value.ToString(), Me.TablaImportar.Item(19, i).Value.ToString(), 1, "GG", 0)

                        Catch ex As Exception

                        End Try
                        Try
                            If e > 0 Then
                                Cta2 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgEx", Me.TablaImportar.Item(5, i).Value.ToString(), Me.TablaImportar.Item(11, i).Value), Me.TablaImportar.Item(18, i).Value.ToString(), Me.TablaImportar.Item(19, i).Value.ToString(), 1, "GG", 0)
                            End If

                        Catch ex As Exception

                        End Try



                        Dim fecha As String = Eventos.Sql_hoy()
                        Sql = ""
                        Dim t As Decimal = Convert.ToDecimal(Me.TablaImportar.Item(11, i).Value)
                        If G > 0 Then
                            If t = 0.16 Then 'cargos

                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Polis & "'," & Item & ", " & G & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                Item += 1
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Polis & "'," & Item & ", " & Convert.ToDecimal(Me.TablaImportar.Item(13, i).Value) & ", 0," & fecha & ", 'A', 1180000100020000, '')" & vbCrLf
                                Item += 1
                            ElseIf t = 0.08 Then 'cargos
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Polis & "'," & Item & ", " & G & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                Item += 1
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Polis & "'," & Item & ", " & Convert.ToDecimal(Me.TablaImportar.Item(13, i).Value) & ", 0," & fecha & ", 'A', 1180000100120000, '')" & vbCrLf
                                Item += 1
                            End If
                        End If

                        If e > 0 And G = 0 Then 'cargo
                            Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                            Sql &= "  VALUES ('" & Polis & "'," & Item & ", " & e & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                            Item += 1
                        ElseIf e > 0 And G > 0 Then
                            Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                            Sql &= "  VALUES ('" & Polis & "'," & Item & ", " & e & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                            Item += 1
                        End If


                        If Convert.ToDecimal(Me.TablaImportar.Item(12, i).Value) > 0 Then 'Abono operadores

                            For Each O As Operadores In Operador
                                Try
                                    Cta3 = Eventos.RegresaCunetaDeudor(Eventos.Regresa_Cuenta_Series(1, "Deudor", Me.TablaImportar.Item(5, i).Value.ToString(), Me.TablaImportar.Item(11, i).Value), O.RFC_Deudor, O.Nombres_Deudor, 1, "GG", O.Matricula)
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Polis & "'," & Item & ",  0," & O.Importe & "," & fecha & ", 'A', " & Cta3 & ", '')" & vbCrLf
                                    Item += 1
                                Catch ex As Exception

                                End Try

                            Next



                        End If

                        Sql += " Update Xml_Sat set ID_poliza ='" & Polis & "' WHERE UUID='" & Me.TablaImportar.Item(10, i).Value & "'" & vbCrLf
                        If Eventos.Comando_sql(Sql) = 0 Then
                            salto += 1
                        End If


                    End If
                End If

            End If
            Frm2.Barra.Value += 1
        Next
        Frm2.Close()




        If CorregirOts.Count > 0 Then
            If polizas.Count > 0 Then
                where = Join(polizas, ",")
                Sql = "Delete  from detalle_Polizas where Id_Poliza in (" & where & ")"
                If Eventos.Comando_sql(Sql) > 0 Then

                End If
            End If
            Dim Frm4 As New BarraProcesovb
            Frm4.Show()
            Frm4.Text = "Editando y creando polizas por favor espere..."
            Frm4.Barra.Minimum = 0
            Frm4.Barra.Maximum = CorregirOts.Count
            For Each Movimiento In CorregirOts
                If Movimiento.PolizaAtm = "" Then

                    Dim Polis As String = Calcula_poliza(Movimiento.Fecha.Substring(3, 2), Movimiento.Fecha.Substring(6, 4), 1, 12)
                    Dim Posi As Integer = InStr(1, Polis, "-", CompareMethod.Binary)
                    Dim Cuantos As Integer = Len(Polis) - Len(Polis.Substring(0, Posi))
                    Dim Consecutivo As Integer = Val(Polis.Substring(Posi, Cuantos))
                    Dim Cta1, Cta2, Cta3 As String
                    Dim xmlsat = Eventos.ObtenerValorDB("Xml_Sat", "Id_Registro_Xml", "   UUID = '" & Movimiento.UUID & "' ", True)
                    If xmlsat.ToString <> " " Then
                        If Creapoliza(Polis, Movimiento.Fecha.Substring(6, 4), Movimiento.Fecha.Substring(3, 2), Movimiento.Fecha.Substring(0, 2),
                                 Consecutivo, 12, Movimiento.Fecha, "Gtos / Factura " & Movimiento.UUID, " / Multipoliza", Consecutivo,
                                 xmlsat, Movimiento.UUID, Movimiento.OT) Then
                            Movimiento.PolizaAtm = Polis
                            'Generar detalle
                            If Buscafactura(Movimiento.UUID, "C") = True Then
                                'Se inserta la Factura
                                Eventos.Inserta_Comprobante_Fiscal(Polis, Movimiento.Fecha.Substring(6, 4), Movimiento.Fecha.Substring(3, 2),
                                     Movimiento.Emisor, Movimiento.Fecha, Movimiento.UUID, "Factura " & Trim(Movimiento.Emisor) & " C", Movimiento.AbonoO)
                            Else
                                'Se Edita la Factura
                                Eventos.Edita_Factura(Movimiento.UUID, "C", Polis)
                            End If

                            If Movimiento.Metodo = "01 - Efectivo" Then
                                Eventos.Inserta_Comprobante_Fiscal_Efectivo(Polis, Movimiento.Fecha.Substring(6, 4), Movimiento.Fecha.Substring(3, 2),
                                  Movimiento.Emisor, "001", Movimiento.Fecha,
                                  "", "", "", Movimiento.AbonoO, 1)
                            Else ' Transferencia
                                Eventos.Inserta_Comprobante_Fiscal_Transf(Polis, Movimiento.Fecha.Substring(6, 4), Movimiento.Fecha.Substring(3, 2),
                                   Movimiento.Emisor, "001", Movimiento.Fecha,
                                  "", "", "", Movimiento.AbonoO, "02", 0, 1)
                            End If


                            Dim Item As Integer = 1
                            If Movimiento.TipoGasto = "Combustible" Or Movimiento.TipoGasto = "Combustible Extra" Then
                                Movimiento.Grabado += Movimiento.Exento
                                Movimiento.Exento = 0
                            End If
                            Try
                                Cta1 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgG", Movimiento.TipoGasto, Movimiento.Tasa), Movimiento.Emisor, Movimiento.Nombre, 1, "GG", 0)

                            Catch ex As Exception

                            End Try
                            Try
                                If Movimiento.Exento > 0 Then
                                    Cta2 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgEx", Movimiento.TipoGasto, Movimiento.Tasa), Movimiento.Emisor, Movimiento.Nombre, 1, "GG", 0)
                                End If

                            Catch ex As Exception

                            End Try

                            Try
                                Cta3 = Eventos.RegresaCunetaDeudor(Eventos.Regresa_Cuenta_Series(1, "Deudor", Movimiento.TipoGasto, Movimiento.Tasa), Movimiento.RFC_Deudor, Movimiento.Nombres_Deudor, 1, "GG", Movimiento.Matricula)
                            Catch ex As Exception

                            End Try

                            Dim fecha As String = Eventos.Sql_hoy()
                            Sql = ""
                            If Movimiento.Grabado > 0 Then
                                If Movimiento.Tasa = 0.16 Then 'cargos

                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Grabado & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                    Item += 1
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.IVAa & ", 0," & fecha & ", 'A', 1180000100020000, '')" & vbCrLf
                                    Item += 1
                                ElseIf Movimiento.Tasa = 0.08 Then 'cargos
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Grabado & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                    Item += 1
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.IVAa & ", 0," & fecha & ", 'A', 1180000100120000, '')" & vbCrLf
                                    Item += 1
                                End If
                            End If

                            If Movimiento.Exento > 0 And Movimiento.Grabado = 0 Then 'cargo
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Exento & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                            ElseIf Movimiento.Exento > 0 And Movimiento.Grabado > 0 Then
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Exento & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                            End If


                            If Movimiento.AbonoO > 0 Then 'Abono
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  0," & Movimiento.AbonoO & "," & fecha & ", 'A', " & Cta3 & ", '')" & vbCrLf
                            End If


                            If Movimiento.CajaCosto > 0 Then ' abono
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  " & Movimiento.CajaCosto & ",0," & fecha & ", 'A', " & Cta3 & ", '')" & vbCrLf
                                Item += 1
                                Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  0," & Movimiento.CajaCosto & "," & fecha & ", 'A',  1010000400010000,'')" & vbCrLf
                            End If

                            Sql += " Update Xml_Sat set ID_poliza ='" & Movimiento.PolizaAtm & "' WHERE UUID='" & Movimiento.UUID & "'" & vbCrLf
                            If Eventos.Comando_sql(Sql) = 0 Then
                                salto += 1
                            End If
                        Else

                        End If

                    End If
                Else
                    'Editar

                    Try

                        Sql = ""
                        Dim Cta1, Cta2, Cta3 As String

                        Dim Item As Integer = 1
                        If Movimiento.TipoGasto = "Combustible" Or Movimiento.TipoGasto = "Combustible Extra" Then
                            Movimiento.Grabado += Movimiento.Exento
                            Movimiento.Exento = 0
                        End If
                        If Movimiento.PolizaAtm <> "" Then


                            Try
                                Cta1 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgG", Movimiento.TipoGasto, Movimiento.Tasa), Movimiento.Emisor, Movimiento.Nombre, 1, "GG", 0)

                            Catch ex As Exception

                            End Try
                            Try
                                If Movimiento.Exento > 0 Then
                                    Cta2 = Eventos.RegresaCuneta(Eventos.Regresa_Cuenta_Series(1, "CtaEgEx", Movimiento.TipoGasto, Movimiento.Tasa), Movimiento.Emisor, Movimiento.Nombre, 1, "GG", 0)
                                End If

                            Catch ex As Exception

                            End Try

                            Try
                                Cta3 = Eventos.RegresaCunetaDeudor(Eventos.Regresa_Cuenta_Series(1, "Deudor", Movimiento.TipoGasto, Movimiento.Tasa), Movimiento.RFC_Deudor, Movimiento.Nombres_Deudor, 1, "GG", Movimiento.Matricula)
                            Catch ex As Exception

                            End Try

                            Dim fecha As String = Eventos.Sql_hoy()

                            If salto = 167 Then

                                If Movimiento.Grabado > 0 Then
                                    If Movimiento.Tasa = 0.0 Then
                                        Movimiento.Tasa = 0.16
                                    End If
                                    If Movimiento.Tasa = 0.16 Then 'cargos

                                        Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                        Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Grabado & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                        Item += 1
                                        Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                        Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.IVAa & ", 0," & fecha & ", 'A', 1180000100020000, '')" & vbCrLf
                                        Item += 1
                                    ElseIf Movimiento.Tasa = 0.08 Then 'cargos
                                        Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                        Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Grabado & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                        Item += 1
                                        Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                        Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.IVAa & ", 0," & fecha & ", 'A', 1180000100120000,'')" & vbCrLf
                                        Item += 1
                                    End If
                                End If

                                If Movimiento.Exento > 0 And Movimiento.Grabado = 0 Then 'cargo
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Exento & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                                ElseIf Movimiento.Exento > 0 And Movimiento.Grabado > 0 Then
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Exento & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                                End If


                                If Movimiento.AbonoO > 0 Then 'Abono
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  0," & Movimiento.AbonoO & "," & fecha & ", 'A', " & Cta3 & ", '')" & vbCrLf
                                End If


                                If Movimiento.CajaCosto > 0 Then ' abono
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  0," & Movimiento.CajaCosto & "," & fecha & ", 'A',  1010000400010000, '')" & vbCrLf
                                End If

                                Sql += " Update Xml_Sat set ID_poliza ='" & Movimiento.PolizaAtm & "' WHERE UUID='" & Movimiento.UUID & "'" & vbCrLf
                                If Eventos.Comando_sql(Sql) = 0 Then
                                    salto += 1

                                End If
                                Sql = ""
                                salto = 1
                            Else
                                If Movimiento.Grabado > 0 Then
                                    If Movimiento.Tasa = 0.0 Then
                                        Movimiento.Tasa = 0.16
                                    End If

                                    If Movimiento.Tasa = 0.16 Then 'cargos

                                        Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                        Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Grabado & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                        Item += 1
                                        Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                        Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.IVAa & ", 0," & fecha & ", 'A', 1180000100020000, '')" & vbCrLf
                                        Item += 1
                                    ElseIf Movimiento.Tasa = 0.08 Then 'cargos
                                        Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                        Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Grabado & ", 0," & fecha & ", 'A', " & Cta1 & ", '')" & vbCrLf
                                        Item += 1
                                        Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                        Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.IVAa & ", 0," & fecha & ", 'A', 1180000100120000, '')" & vbCrLf
                                        Item += 1
                                    End If
                                End If

                                If Movimiento.Exento > 0 And Movimiento.Grabado = 0 Then 'cargo
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Exento & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                                    Item += 1
                                ElseIf Movimiento.Exento > 0 And Movimiento.Grabado > 0 Then
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ", " & Movimiento.Exento & ", 0," & fecha & ", 'A', " & Cta2 & ", '')" & vbCrLf
                                    Item += 1
                                End If
                                If Movimiento.AbonoO > 0 Then 'Abono
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  0," & Movimiento.AbonoO & "," & fecha & ", 'A', " & Cta3 & ", '')" & vbCrLf
                                    Item += 1
                                End If
                                If Movimiento.CajaCosto > 0 Then ' abono
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  " & Movimiento.CajaCosto & ",0," & fecha & ", 'A', " & Cta3 & ", '')" & vbCrLf
                                    Item += 1
                                    Sql += " INSERT INTO dbo.Detalle_Polizas (ID_poliza, ID_item, Cargo, Abono, Fecha_captura, Movto, Cuenta, No_cheque)"
                                    Sql &= "  VALUES ('" & Movimiento.PolizaAtm & "'," & Item & ",  0," & Movimiento.CajaCosto & "," & fecha & ", 'A',  1010000400010000, '')" & vbCrLf
                                    Item += 1
                                End If

                                Sql += " Update Xml_Sat set ID_poliza ='" & Movimiento.PolizaAtm & "' WHERE UUID='" & Movimiento.UUID & "'" & vbCrLf
                                salto += 1
                            End If
                        End If

                        If salto > 0 Then
                            If Eventos.Comando_sql(Sql) = 0 Then
                                salto += 1
                            End If
                        End If




                    Catch ex As Exception

                    End Try


                End If
                Frm4.Barra.Value += 1
            Next
            Frm4.Close()

        End If



    End Sub
    Public Function Creapoliza(ByVal id_poliza As String, ByVal anio As Integer, ByVal mes As String, ByVal dia As String,
                         ByVal consecutivo As Integer, ByVal tipo As Integer, ByVal fecha As String,
                         ByVal concepto As String, ByVal movimiento As String, ByVal num_pol As Integer, ByVal registro As Integer,
                               Optional ByVal UUID As String = "", Optional ByVal OT As String = "") As Boolean

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
        sql &= "     ID_Empresa,     "
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
        sql &= " 	1," '@ID_Empresa,        
        sql &= " 	'" & movimiento & "'," '@no_mov,            
        sql &= " 	" & Eventos.Sql_hoy("" & dia & "/" & mes & "/" & anio & "") & "," '@fecha_captura,     
        sql &= " 	'A'," '@movto,             
        sql &= "  '" & UCase(Inicio.LblUsuario.Text.Trim()) & "', 1" '@usuario            
        sql &= " 	) "

        If Eventos.Comando_sql(sql) > 0 Then
            Creapoliza = True
            Eventos.Actualiza_Registro(id_poliza, registro)
            Eventos.Actualiza_RegistroXMLAuditados(id_poliza, UUID, OT)
        Else
            Creapoliza = False
        End If
        Return Creapoliza
    End Function

    Private Sub Aplicar_Filtro(ByVal Columna As String)

        ' filtrar por el campo Producto  
        ''''''''''''''''''''''''''''''''''''''''''''''''''''  
        Dim Ret As Integer = Filtrar_DataGridView(
                                        Me.TablaImportar.Columns(7).Name,
                                        Me.TxtFiltro.Text.Trim,
                                        BindingSource1,
                                        Me.TablaImportar,
                                        CType(3, e_FILTER_OPTION))

        If Ret = 0 Then

            Me.TxtFiltro.BackColor = Color.Red
        Else
            Me.TxtFiltro.BackColor = Color.White
        End If

        Me.lblRegistros.Text = "Filtro: " & Ret & " Registros encontrados"

    End Sub

    Function Filtrar_DataGridView(
        ByVal Columna As String,
        ByVal texto As String,
        ByVal BindingSource As BindingSource,
        ByVal DataGridView As DataGridView,
        Optional ByVal Opcion_Filtro As e_FILTER_OPTION = Nothing) As Integer

        ' verificar que el DataSource no esté vacio  
        If BindingSource1.DataSource Is Nothing Then
            Return 0
        End If

        Try

            Dim filtro As String = String.Empty

            ' Seleccionar la opción   
            Select Case Opcion_Filtro
                Case e_FILTER_OPTION.CADENA_QUE_COMIENCE_CON
                    filtro = "like '" & texto.Trim & "%'"
                Case e_FILTER_OPTION.CADENA_QUE_NO_COMIENCE_CON
                    filtro = "Not like '" & texto.Trim & "%'"
                Case e_FILTER_OPTION.CADENA_QUE_NO_CONTENGA
                    filtro = "Not like '%" & texto.Trim & "%'"
                Case e_FILTER_OPTION.CADENA_QUE_CONTENGA
                    filtro = "like '%" & texto.Trim & "%'"
                Case e_FILTER_OPTION.CADENA_IGUAL
                    filtro = "='" & texto.Trim & "'"
            End Select
            ' Opción para no filtrar  
            If Opcion_Filtro = e_FILTER_OPTION.SIN_FILTRO Then
                filtro = String.Empty
            End If

            ' armar el sql  
            If filtro <> String.Empty Then
                filtro = "[" & Columna & "]" & filtro
            End If

            ' asigar el criterio a la propiedad Filter del BindingSource  
            BindingSource.Filter = filtro
            ' enlzar el datagridview al BindingSource  
            DataGridView.DataSource = BindingSource.DataSource

            ' retornar la cantidad de registros encontrados  
            Return BindingSource.Count

            ' errores  
        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical)
        End Try

        Return 0

    End Function

    Private Sub TxtFiltro_Enter(sender As Object, e As EventArgs) Handles TxtFiltro.Enter

    End Sub

    Private Sub SP1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP1.DoWork
        Try


            For j As Integer = 0 To Me.TablaImportar.RowCount - 1


                For i As Integer = 0 To Regis.Count - 1
                    If UCase(Regis(i).UUID) = UCase(Me.TablaImportar.Item(10, j).Value.ToString) Then
                        Try
                            Me.TablaImportar.Item(11, j).Value = Convert.ToDecimal(Regis(i).Tasa)
                            Me.TablaImportar.Item(12, j).Value = Convert.ToDecimal(Regis(i).Total)
                            Me.TablaImportar.Item(13, j).Value = Convert.ToDecimal(Regis(i).IVAa)
                            Me.TablaImportar.Item(14, j).Value = Convert.ToDecimal(Regis(i).Grabado)
                            Me.TablaImportar.Item(15, j).Value = Convert.ToDecimal(Regis(i).Exento)
                            Me.TablaImportar.Item(16, j).Value = IIf(Convert.ToDecimal(Regis(i).Exento) <= 0, 0, Convert.ToDecimal(Regis(i).Exento)) + Convert.ToDecimal(Regis(i).Grabado) + Convert.ToDecimal(Regis(i).IVAa) - (Convert.ToDecimal(Me.TablaImportar.Item(8, j).Value))
                            If Me.TablaImportar.Item(16, j).Value < 0 Then
                                Me.TablaImportar.Item(16, j).Value *= -1
                            End If

                            Me.TablaImportar.Item(17, j).Value = Regis(i).PolizaAtm
                            Me.TablaImportar.Item(18, j).Value = Regis(i).Emisor
                            Me.TablaImportar.Item(19, j).Value = Regis(i).Nombre
                            Me.TablaImportar.Item(21, j).Value = Regis(i).Fecha
                            Me.TablaImportar.Item(22, j).Value = Regis(i).Metodo
                            If (Convert.ToDecimal(Me.TablaImportar.Item(8, j).Value) < Me.TablaImportar.Item(12, j).Value) And Me.TablaImportar.Item(9, j).Value = Me.TablaImportar.Item(13, j).Value Then
                                Me.TablaImportar.Item(20, j).Value = Me.TablaImportar.Item(16, j).Value
                            Else
                                Me.TablaImportar.Item(20, j).Value = 0
                            End If

                        Catch ex As Exception

                        End Try
                    End If
                Next



            Next
            dt.Columns.Clear()
            Sin.Columns.Clear()
            For Each col As DataGridViewColumn In Me.TablaImportar.Columns
                Dim column As New DataColumn(col.Name, Type.GetType("System.String"))
                dt.Columns.Add(column)
            Next
            Sin = dt
            For Each viewRow As DataGridViewRow In Me.TablaImportar.Rows
                Dim row As DataRow = dt.NewRow()

                For Each col As DataGridViewColumn In Me.TablaImportar.Columns
                    Dim value As Object = viewRow.Cells(col.Name).Value
                    row.Item(col.Name) = If(value Is Nothing, DBNull.Value, value)
                Next col



                If row.Item(17) <> "" Then
                    dt.Rows.Add(row)
                Else
                    Sin.Rows.Add(row)
                End If



            Next viewRow ' Siguiente fila
            Me.TablaImportar.DataSource = Nothing
            BindingSource1.DataSource = Nothing
            If Me.TablaImportar.Columns.Count > 0 Then
                Me.TablaImportar.Columns.Clear()
            End If
            BindingSource1.DataSource = dt

            Me.TablaImportar.DataSource = dt

        Catch ex As Exception

        End Try
    End Sub

    Private Sub TablaImportar_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellClick
        Try
            fil = Me.TablaImportar.Columns(e.ColumnIndex).Name
        Catch ex As Exception

        End Try

    End Sub

    Private Sub TxtFiltro_TextChanged(sender As Object, e As EventArgs) Handles TxtFiltro.TextChanged
        If Me.TxtFiltro.Text <> String.Empty Then
            Aplicar_Filtro(fil)
        End If
    End Sub

    Private Sub TablaImportar_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaImportar.DataError

    End Sub
End Class
