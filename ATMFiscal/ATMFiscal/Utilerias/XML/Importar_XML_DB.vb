Imports Telerik.WinControls
Public Class Importar_XML_DB
    Dim Dato As DataSet
    Dim DatoC As DataSet
    Dim Ejecucion As Boolean
    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Columns.Clear()
            Me.lblRegistros.Text = "Total de Registros: 0"
            Me.RadEmitidas.Checked = True
        End If
    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        Me.CmdImportar.Enabled = False
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelXMLSat("XML"), Me.TablaImportar)
        Colorear()
        Me.CmdImportar.Enabled = True
    End Sub
    Private Sub Colorear()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Try
            Me.TablaImportar.Rows.RemoveAt(0)
            If Me.TablaImportar.Rows.Count > 0 Then
                If Me.RadEmitidas.Checked = True Then
                    Dim ds As DataSet = Eventos.Obtener_DS("select Id_Empresa,Razon_Social FROM Empresa where Reg_fed_causantes = '" & Trim(Me.TablaImportar.Item(12, 0).Value) & "'")
                    If ds.Tables(0).Rows.Count > 0 Then
                        Me.lstCliente.SelectText = ds.Tables(0).Rows(0)("Razon_Social")
                        Colorea()
                        Me.lblRegistros.Text = "Total de Registros: " & Me.TablaImportar.Rows.Count
                    Else
                        RadMessageBox.Show("El RFC " & Me.TablaImportar.Item(12, 0).Value & " no corresponde a la Empresa...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                        Me.CmdLimpiar.PerformClick()
                    End If
                Else
                    Dim ds As DataSet = Eventos.Obtener_DS("select Id_Empresa,Razon_Social FROM Empresa where Reg_fed_causantes = '" & Trim(Me.TablaImportar.Item(15, 0).Value) & "'")
                    If ds.Tables(0).Rows.Count > 0 Then
                        Me.lstCliente.SelectText = ds.Tables(0).Rows(0)("Razon_Social")
                        Colorea()
                        Me.lblRegistros.Text = "Total de Registros: " & Me.TablaImportar.Rows.Count
                    Else
                        RadMessageBox.Show("El RFC " & Me.TablaImportar.Item(15, 0).Value & " no corresponde a la Empresa...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                        Me.CmdLimpiar.PerformClick()
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        Me.Cmd_Procesar.Enabled = False
        Me.TablaImportar.Enabled = False

        RadMessageBox.SetThemeName("MaterialBlueGrey")
        My.Forms.Inicio.txtServerDB.Text = serV
        If Me.TablaImportar.Rows.Count > 0 Then
            If Me.lstCliente.SelectText <> "" Then
                If RadMessageBox.Show("La Empresa " & Me.lstCliente.SelectText & " es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim consecutivo As Integer
                    If IsDBNull(Eventos.Obtener_DS("SELECT max (Consecutivo_Carga)+1 FROM Xml_Sat ").Tables(0).Rows(0)(0)) Then '@Consecutivo_Carga
                        consecutivo = 1
                    Else
                        consecutivo = Eventos.Obtener_DS("SELECT max (Consecutivo_Carga)+1 FROM Xml_Sat ").Tables(0).Rows(0)(0)
                    End If
                    Dim frm3 As New BarraProcesovb
                    frm3.Show()
                    frm3.Text = "Importando Datos por favor espere..."
                    frm3.Barra.Minimum = 0
                    frm3.Barra.Maximum = Me.TablaImportar.Rows.Count
                    For p As Integer = 0 To Me.TablaImportar.RowCount - 1
                        Try
                            If Me.TablaImportar.Item(10, p).Style.BackColor = Color.Red Then

                            Else
                                'If Me.TablaImportar.Item(2, p).Value <> Nothing Then
                                '    Dim sql As String = "INSERT INTO dbo.Xml_Sat"
                                '    sql &= " 	("
                                '    sql &= "         Verificado_Asoc,"
                                '    sql &= "         Estado_SAT,"
                                '    sql &= "         Version,"
                                '    sql &= "         Tipo, "
                                '    sql &= "         Fecha_Emision,"
                                '    sql &= "         Fecha_Timbrado,"
                                '    sql &= "         EstadoPago,"
                                '    sql &= "         FechaPago,"
                                '    sql &= "         Serie,"
                                '    sql &= "         Folio,"
                                '    sql &= "         UUID,"
                                '    sql &= "         UUID_Relacion,"
                                '    sql &= "         RFC_Emisor,"
                                '    sql &= "         Nombre_Emisor,"
                                '    sql &= "         LugarDeExpedicion,"
                                '    sql &= "         RFC_Receptor,"
                                '    sql &= "         Nombre_Receptor,"
                                '    sql &= "         ResidenciaFiscal,"
                                '    sql &= "         NumRegIdTrib,"
                                '    sql &= "         UsoCFDI,"
                                '    sql &= "         SubTotal,"
                                '    sql &= "         Descuento,"
                                '    sql &= "         Total_IEPS,"
                                '    sql &= "         IVA_16,"
                                '    sql &= "         Retenido_IVA,"
                                '    sql &= "         Retenido_ISR,"
                                '    sql &= "         ISH,"
                                '    sql &= "         Total,"
                                '    sql &= "         TotalOriginal,"
                                '    sql &= "         Total_Trasladados,"
                                '    sql &= "         Total_Retenidos,"
                                '    sql &= "         Total_LocalTrasladado,"
                                '    sql &= "         Total_LocalRetenido,"
                                '    sql &= "         Complemento,"
                                '    sql &= "         Moneda,"
                                '    sql &= "         Tipo_De_Cambio,"
                                '    sql &= "         FormaDePago,"
                                '    sql &= "         Metodo_de_Pago,"
                                '    sql &= "         NumCtaPago,"
                                '    sql &= "         Condicion_de_Pago,"
                                '    sql &= "         Conceptos,"
                                '    sql &= "         Combustible,"
                                '    sql &= "         IEPS_3,"
                                '    sql &= "         IEPS_6,"
                                '    sql &= "         IEPS_7,"
                                '    sql &= "         IEPS_8,"
                                '    sql &= "         IEPS_9,"
                                '    sql &= "         IEPS_26,"
                                '    sql &= "         IEPS_30,"
                                '    sql &= "         IEPS_53,"
                                '    sql &= "         IEPS_160,"
                                '    sql &= "         Archivo_XML,"
                                '    sql &= "         Direccion_Emisor,"
                                '    sql &= "         Localidad_Emisor,"
                                '    sql &= "         Direccion_Receptor,"
                                '    sql &= "         Localidad_Receptor,"
                                '    sql &= "         Consecutivo_Carga,"
                                '    sql &= "         Id_Empresa, "
                                '    sql &= "         Carga_Procesada,Emitidas,Banco_Origen,Banco_Destino ,IVA_8,IEPS_30_4"
                                '    sql &= " 	)"
                                '    sql &= "         VALUES "
                                '    sql &= " 	("
                                '    sql &= " 	'" & Me.TablaImportar.Item(0, p).Value & "'," '@verificado_asoc
                                '    sql &= " 	'" & Me.TablaImportar.Item(1, p).Value & "'," '@estado_sat
                                '    sql &= " 	'" & Me.TablaImportar.Item(2, p).Value & "'," '@version 
                                '    sql &= " 	'" & Me.TablaImportar.Item(3, p).Value & "'," '@ tipo
                                '    Dim fecha, fecha2, fecha3 As Date
                                '    fecha = IIf(Me.TablaImportar.Item(4, p).Value Is Nothing, "", Me.TablaImportar.Item(4, p).Value)
                                '    fecha2 = IIf(Me.TablaImportar.Item(5, p).Value Is Nothing, "", Me.TablaImportar.Item(5, p).Value)
                                '    fecha3 = IIf(Me.TablaImportar.Item(7, p).Value Is Nothing Or Me.TablaImportar.Item(7, p).Value.ToString() = "" Or IsDBNull(Me.TablaImportar.Item(7, p).Value), "01/01/2018", Me.TablaImportar.Item(7, p).Value)
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(4, p).Value Is Nothing Or Me.TablaImportar.Item(4, p).Value = "", "NULL", Eventos.Sql_hoy(fecha.ToString.Substring(0, 10))) & ","  '@fecha_emision
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(5, p).Value Is Nothing Or Me.TablaImportar.Item(5, p).Value = "", "NULL", Eventos.Sql_hoy(fecha2.ToString.Substring(0, 10))) & ","  '@fecha_timbrado
                                '    sql &= " 	'" & Me.TablaImportar.Item(6, p).Value & "'," '@estadopago
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(7, p).Value Is Nothing Or Me.TablaImportar.Item(7, p).Value.ToString() = "" Or IsDBNull(Me.TablaImportar.Item(7, p).Value), "NULL", Eventos.Sql_hoy(fecha3.ToString.Substring(0, 10))) & "," '@fechapago
                                '    If Me.RadEmitidas.Checked = True Then
                                '        sql &= " 	'" & IIf(Me.TablaImportar.Item(8, p).Value Is Nothing Or Me.TablaImportar.Item(8, p).Value = "", "I", Me.TablaImportar.Item(8, p).Value.ToString) & "'," '@serie
                                '    Else
                                '        sql &= " 	'" & Me.TablaImportar.Item(8, p).Value & "'," '@serie
                                '    End If

                                '    sql &= " 	'" & Me.TablaImportar.Item(9, p).Value & "'," '@folio
                                '    sql &= " 	'" & Me.TablaImportar.Item(10, p).Value & "'," '@uuid
                                '    sql &= " 	'" & Me.TablaImportar.Item(11, p).Value & "'," '@uuid_Relacion
                                '    sql &= " 	'" & Me.TablaImportar.Item(12, p).Value & "'," '@rfc_emisor
                                '    sql &= " 	'" & Me.TablaImportar.Item(13, p).Value.ToString.Replace("'", "") & "'," '@nombre_emisor
                                '    sql &= " 	'" & Me.TablaImportar.Item(14, p).Value.ToString.Replace("'", "") & "'," '@lugardeexpedicion
                                '    sql &= " 	'" & Me.TablaImportar.Item(15, p).Value.ToString.Replace("'", "") & "'," '@rfc_receptor
                                '    sql &= " 	'" & Me.TablaImportar.Item(16, p).Value.ToString.Replace("'", "") & "'," '@nombre_receptor
                                '    sql &= " 	'" & Me.TablaImportar.Item(17, p).Value.ToString.Replace("'", "") & "'," '@residenciafiscal
                                '    sql &= " 	'" & Me.TablaImportar.Item(18, p).Value & "'," '@numregidtrib
                                '    Try
                                '        Dim uso As String = UsoCFDI(Me.TablaImportar.Item(19, p).Value.ToString.Substring(0, 3))
                                '        sql &= " 	'" & uso & "'," '@usocfdi
                                '    Catch ex As Exception
                                '        sql &= " 	''," '@usocfdi
                                '    End Try
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(20, p).Value Is Nothing Or Me.TablaImportar.Item(20, p).Value = "", "0", Val(Me.TablaImportar.Item(20, p).Value)) & "," '@subtotal
                                '    Try
                                '        sql &= " 	" & IIf(Me.TablaImportar.Item(21, p).Value Is Nothing Or Me.TablaImportar.Item(21, p).Value = "", "0", Val(Me.TablaImportar.Item(21, p).Value)) & "," '@descuento
                                '    Catch ex As Exception
                                '        sql &= " 	0," '@descuento
                                '    End Try
                                '    Try
                                '        sql &= " 	" & IIf(Me.TablaImportar.Item(22, p).Value Is Nothing Or Me.TablaImportar.Item(22, p).Value = "", "0", Val(Me.TablaImportar.Item(22, p).Value)) & "," '@total_ieps
                                '    Catch ex As Exception
                                '        sql &= " 	0," '@descuento
                                '    End Try

                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(23, p).Value Is Nothing Or Me.TablaImportar.Item(23, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(23, p).Value)) & "," '@iva_16
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(24, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(24, p).Value)) & "," '@retenido_iva se Suma el IVA RETENIDO DE TASA 6%
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(25, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(25, p).Value)) & "," '@retenido_isr
                                '    sql &= " 	" & Me.TablaImportar.Item(26, p).Value & "," '@ish
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(27, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(27, p).Value)) & "," '@total
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(28, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(28, p).Value)) & "," '@totaloriginal
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(29, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(29, p).Value)) & "," '@total_trasladados
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(30, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(30, p).Value)) & "," '@total_retenidos
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(31, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(31, p).Value)) & "," '@total_localtrasladado
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(32, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(32, p).Value)) & "," '@total_localretenido
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(33, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(33, p).Value)) & "," '@complemento
                                '    sql &= " 	'" & Me.TablaImportar.Item(34, p).Value & "'," '@moneda
                                '    Dim tipo_cambio As String = SoloNumeros(Me.TablaImportar.Item(35, p).Value)
                                '    sql &= " 	" & IIf(tipo_cambio = "", "NULL", tipo_cambio) & ","
                                '    'sql &= " 	" & Me.TablaImportar.Item(35, p).Value & "," '@tipo_de_cambio
                                '    Try
                                '        Dim forma As String = FormaP(Me.TablaImportar.Item(36, p).Value.ToString.Substring(0, 2))
                                '        If forma = "" Then
                                '            sql &= " 	'99'," '@formadepago
                                '        Else
                                '            sql &= " 	'" & forma & "'," '@formadepago
                                '        End If
                                '    Catch ex As Exception
                                '        sql &= " 	'99'," '@formadepago
                                '    End Try


                                '    Try
                                '        Dim metodo As String = MetodoP(Me.TablaImportar.Item(37, p).Value.ToString.Substring(0, 3)) 'Me.TablaImportar.Item(37, p).Value
                                '        sql &= " 	'" & metodo & "'," '@metodo_de_pago
                                '    Catch ex As Exception

                                '        sql &= " 	'99'," '@metodo_de_pago
                                '    End Try


                                '    Dim NumCuenta As String = SoloNumeros(Me.TablaImportar.Item(38, p).Value.ToString)
                                '    sql &= " 	" & IIf(NumCuenta = "", "NULL", NumCuenta) & ","
                                '    'sql &= " 	" & IIf(Me.TablaImportar.Item(38, p).Value Is Nothing, "NULL", Me.TablaImportar.Item(38, p).Value) & "," '@numctapago
                                '    sql &= " 	'" & Me.TablaImportar.Item(39, p).Value & "'," '@condicion_de_pago
                                '    Dim concepto As String = Me.TablaImportar.Item(40, p).Value.Replace("'", " ")
                                '    If Len(concepto) > 2000 Then
                                '        concepto = Me.TablaImportar.Item(40, p).Value.Replace("'", " ").ToString.Substring(0, 1000)
                                '    Else
                                '        concepto = Me.TablaImportar.Item(40, p).Value.Replace("'", " ")
                                '    End If
                                '    sql &= " 	'" & concepto & "'," '@conceptos
                                '    sql &= " 	'" & Me.TablaImportar.Item(41, p).Value & "'," '@combustible
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(42, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(42, p).Value)) & "," '@ieps_3
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(43, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(43, p).Value)) & "," '@ieps_6
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(44, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(44, p).Value)) & "," '@ieps_7
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(45, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(45, p).Value)) & "," '@ieps_8
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(46, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(46, p).Value)) & "," '@ieps_9
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(47, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(47, p).Value)) & "," '@ieps_26
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(48, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(48, p).Value)) & "," '@ieps_30
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(49, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(49, p).Value)) & "," '@ieps_53
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(50, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(50, p).Value)) & "," '@ieps_160
                                '    sql &= " 	'" & Me.TablaImportar.Item(51, p).Value & "'," '@archivo_xml
                                '    sql &= " 	'" & Me.TablaImportar.Item(52, p).Value & "'," '@direccion_emisor
                                '    sql &= " 	'" & Me.TablaImportar.Item(53, p).Value & "'," '@localidad_emisor
                                '    sql &= " 	'" & Me.TablaImportar.Item(54, p).Value & "'," '@direccion_receptor
                                '    sql &= " 	'" & Me.TablaImportar.Item(55, p).Value & "', " '@localidad_receptor
                                '    sql &= " " & consecutivo & "," '@Consecutivo_Carga
                                '    sql &= " " & Me.lstCliente.SelectItem & ", " '@Id_Empresa
                                '    sql &= " 0, " & Eventos.Bool2(Me.RadEmitidas.Checked) & " ,'',''," '@Procesada
                                '    sql &= " 	" & IIf(Me.TablaImportar.Item(56, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(56, p).Value)) & "," '@iva_8
                                '    sql &= " 	" & IIf(IsDBNull(Me.TablaImportar.Item(57, p).Value), "0", Val(Me.TablaImportar.Item(57, p).Value)) & "" '@IEPS30.4
                                '    sql &= " )"


                                '    If Eventos.Comando_sql(sql) > 0 Then
                                '        If Len(sql) >= 4000 Then
                                '            sql = sql.Substring(0, 3999)
                                '        End If
                                '        Eventos.Insertar_usuariol("Detalle", sql)
                                '    End If

                                'End If
                                If Me.TablaImportar.Item(2, p).Value <> Nothing Then
                                    Dim sql As String = "INSERT INTO dbo.Xml_Sat"
                                    sql &= " 	("
                                    sql &= "         Verificado_Asoc,"
                                    sql &= "         Estado_SAT,"
                                    sql &= "         Version,"
                                    sql &= "         Tipo, "
                                    sql &= "         Fecha_Emision,"
                                    sql &= "         Fecha_Timbrado,"
                                    sql &= "         EstadoPago,"
                                    sql &= "         FechaPago,"
                                    sql &= "         Serie,"
                                    sql &= "         Folio,"
                                    sql &= "         UUID,"
                                    sql &= "         UUID_Relacion,"
                                    sql &= "         RFC_Emisor,"
                                    sql &= "         Nombre_Emisor,"
                                    sql &= "         LugarDeExpedicion,"
                                    sql &= "         RFC_Receptor,"
                                    sql &= "         Nombre_Receptor,"
                                    sql &= "         ResidenciaFiscal,"
                                    sql &= "         NumRegIdTrib,"
                                    sql &= "         UsoCFDI,"
                                    sql &= "         SubTotal,"
                                    sql &= "         Descuento,"
                                    sql &= "         Total_IEPS,"
                                    sql &= "         IVA_16,"
                                    sql &= "         Retenido_IVA,"
                                    sql &= "         Retenido_ISR,"
                                    sql &= "         ISH,"
                                    sql &= "         Total,"
                                    sql &= "         TotalOriginal,"
                                    sql &= "         Total_Trasladados,"
                                    sql &= "         Total_Retenidos,"
                                    sql &= "         Total_LocalTrasladado,"
                                    sql &= "         Total_LocalRetenido,"
                                    sql &= "         Complemento,"
                                    sql &= "         Moneda,"
                                    sql &= "         Tipo_De_Cambio,"
                                    sql &= "         FormaDePago,"
                                    sql &= "         Metodo_de_Pago,"
                                    sql &= "         NumCtaPago,"
                                    sql &= "         Condicion_de_Pago,"
                                    sql &= "         Conceptos,"
                                    sql &= "         Combustible,"
                                    sql &= "         IEPS_3,"
                                    sql &= "         IEPS_6,"
                                    sql &= "         IEPS_7,"
                                    sql &= "         IEPS_8,"
                                    sql &= "         IEPS_9,"
                                    sql &= "         IEPS_26,"
                                    sql &= "         IEPS_30,"
                                    sql &= "         IEPS_53,"
                                    sql &= "         IEPS_160,"
                                    sql &= "         Archivo_XML,"
                                    sql &= "         Direccion_Emisor,"
                                    sql &= "         Localidad_Emisor,"
                                    sql &= "         Direccion_Receptor,"
                                    sql &= "         Localidad_Receptor,"
                                    sql &= "         Consecutivo_Carga,"
                                    sql &= "         Id_Empresa, "
                                    sql &= "         Carga_Procesada,Emitidas,Banco_Origen,Banco_Destino ,IVA_8,IEPS_30_4"
                                    sql &= " 	)"
                                    sql &= "         VALUES "
                                    sql &= " 	("
                                    sql &= " 	'" & Me.TablaImportar.Item(0, p).Value.ToString() & "'," '@verificado_asoc
                                    sql &= " 	'" & Me.TablaImportar.Item(1, p).Value & "'," '@estado_sat
                                    sql &= " 	'" & Me.TablaImportar.Item(2, p).Value & "'," '@version 
                                    sql &= " 	'" & Me.TablaImportar.Item(3, p).Value & "'," '@ tipo
                                    Dim fecha, fecha2, fecha3 As Date
                                    fecha = IIf(Me.TablaImportar.Item(4, p).Value Is Nothing, "", Me.TablaImportar.Item(4, p).Value)
                                    fecha2 = IIf(Me.TablaImportar.Item(5, p).Value Is Nothing, "", Me.TablaImportar.Item(5, p).Value)
                                    fecha3 = IIf(Me.TablaImportar.Item(7, p).Value Is Nothing Or Me.TablaImportar.Item(7, p).Value.ToString() = "", "01/01/2018", Me.TablaImportar.Item(7, p).Value)
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(4, p).Value Is Nothing Or Me.TablaImportar.Item(4, p).Value = "", "NULL", Eventos.Sql_hoy(fecha.ToString.Substring(0, 10))) & ","  '@fecha_emision
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(5, p).Value Is Nothing Or Me.TablaImportar.Item(5, p).Value = "", "NULL", Eventos.Sql_hoy(fecha2.ToString.Substring(0, 10))) & ","  '@fecha_timbrado
                                    sql &= " 	'" & Me.TablaImportar.Item(6, p).Value.ToString() & "'," '@estadopago
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(7, p).Value Is Nothing Or Me.TablaImportar.Item(7, p).Value.ToString() = "", "NULL", Eventos.Sql_hoy(fecha3.ToString.Substring(0, 10))) & "," '@fechapago
                                    If Me.RadEmitidas.Checked = True Then
                                        sql &= " 	'" & IIf(Me.TablaImportar.Item(8, p).Value Is Nothing Or Me.TablaImportar.Item(8, p).Value.ToString() = "", "I", Me.TablaImportar.Item(8, p).Value.ToString) & "'," '@serie
                                    Else
                                        sql &= " 	'" & Me.TablaImportar.Item(8, p).Value.ToString() & "'," '@serie
                                    End If

                                    sql &= " 	'" & Me.TablaImportar.Item(9, p).Value.ToString() & "'," '@folio
                                    sql &= " 	'" & Me.TablaImportar.Item(10, p).Value.ToString() & "'," '@uuid
                                    sql &= " 	'" & Me.TablaImportar.Item(11, p).Value.ToString() & "'," '@uuid_Relacion
                                    sql &= " 	'" & Me.TablaImportar.Item(12, p).Value.ToString() & "'," '@rfc_emisor
                                    sql &= " 	'" & Me.TablaImportar.Item(13, p).Value.ToString.Trim().Replace("'", " ").Replace("Â", "A").Replace("´", "") & "'," '@nombre_emisor
                                    sql &= " 	'" & Me.TablaImportar.Item(14, p).Value.ToString.Trim().Replace("'", " ").Replace("Â", "A").Replace("´", "") & "'," '@lugardeexpedicion
                                    sql &= " 	'" & Me.TablaImportar.Item(15, p).Value.ToString.Trim().Replace("'", " ").Replace("Â", "A").Replace("´", "") & "'," '@rfc_receptor
                                    sql &= " 	'" & Me.TablaImportar.Item(16, p).Value.ToString.Trim().Replace("'", " ").Replace("Â", "A").Replace("´", "") & "'," '@nombre_receptor
                                    sql &= " 	'" & Me.TablaImportar.Item(17, p).Value.ToString.Trim().Replace("'", " ").Replace("Â", "A").Replace("´", "") & "'," '@residenciafiscal
                                    sql &= " 	'" & Me.TablaImportar.Item(18, p).Value.ToString() & "'," '@numregidtrib
                                    Try
                                        Dim uso As String = UsoCFDI(Me.TablaImportar.Item(19, p).Value.ToString.Substring(0, 3))
                                        sql &= " 	'" & uso & "'," '@usocfdi
                                    Catch ex As Exception
                                        sql &= " 	''," '@usocfdi
                                    End Try
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(20, p).Value Is Nothing Or Me.TablaImportar.Item(20, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(20, p).Value)) & "," '@subtotal
                                    Try
                                        sql &= " 	" & IIf(Me.TablaImportar.Item(21, p).Value Is Nothing Or Me.TablaImportar.Item(21, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(21, p).Value)) & "," '@descuento
                                    Catch ex As Exception
                                        sql &= " 	0," '@descuento
                                    End Try
                                    Try
                                        sql &= " 	" & IIf(Me.TablaImportar.Item(22, p).Value Is Nothing Or Me.TablaImportar.Item(22, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(22, p).Value)) & "," '@total_ieps
                                    Catch ex As Exception
                                        sql &= " 	0," '@descuento
                                    End Try

                                    sql &= " 	" & IIf(Me.TablaImportar.Item(23, p).Value Is Nothing Or Me.TablaImportar.Item(23, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(23, p).Value)) & "," '@iva_16
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(24, p).Value Is Nothing Or Me.TablaImportar.Item(24, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(24, p).Value)) & "," '@retenido_iva se Suma el IVA RETENIDO DE TASA 6%
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(25, p).Value Is Nothing Or Me.TablaImportar.Item(25, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(25, p).Value)) & "," '@retenido_isr
                                    sql &= " 	" & Me.TablaImportar.Item(26, p).Value.ToString() & "," '@ish
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(27, p).Value Is Nothing Or Me.TablaImportar.Item(27, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(27, p).Value)) & "," '@total
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(28, p).Value Is Nothing Or Me.TablaImportar.Item(28, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(28, p).Value)) & "," '@totaloriginal
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(29, p).Value Is Nothing Or Me.TablaImportar.Item(29, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(29, p).Value)) & "," '@total_trasladados
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(30, p).Value Is Nothing Or Me.TablaImportar.Item(30, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(30, p).Value)) & "," '@total_retenidos
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(31, p).Value Is Nothing Or Me.TablaImportar.Item(31, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(31, p).Value)) & "," '@total_localtrasladado
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(32, p).Value Is Nothing Or Me.TablaImportar.Item(32, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(32, p).Value)) & "," '@total_localretenido
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(33, p).Value Is Nothing Or Me.TablaImportar.Item(33, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(33, p).Value)) & "," '@complemento
                                    sql &= " 	'" & Me.TablaImportar.Item(34, p).Value & "'," '@moneda
                                    Dim tipo_cambio As String = SoloNumeros(Me.TablaImportar.Item(35, p).Value)
                                    sql &= " 	" & IIf(tipo_cambio = "", "NULL", tipo_cambio) & ","
                                    'sql &= " 	" & Me.TablaImportar.Item(35, p).Value & "," '@tipo_de_cambio
                                    Try
                                        Dim forma As String = FormaP(Me.TablaImportar.Item(36, p).Value.ToString.Substring(0, 2))
                                        If forma = "" Then
                                            sql &= " 	'99'," '@formadepago
                                        Else
                                            sql &= " 	'" & forma & "'," '@formadepago
                                        End If
                                    Catch ex As Exception
                                        sql &= " 	'99'," '@formadepago
                                    End Try


                                    Try
                                        Dim metodo As String = MetodoP(Me.TablaImportar.Item(37, p).Value.ToString.Substring(0, 3)) 'Me.TablaImportar.Item(37, p).Value
                                        sql &= " 	'" & metodo & "'," '@metodo_de_pago
                                    Catch ex As Exception

                                        sql &= " 	'99'," '@metodo_de_pago
                                    End Try


                                    Dim NumCuenta As String = SoloNumeros(Me.TablaImportar.Item(38, p).Value.ToString())
                                    sql &= " 	" & IIf(NumCuenta = "", "NULL", NumCuenta) & ","
                                    'sql &= " 	" & IIf(Me.TablaImportar.Item(38, p).Value Is Nothing, "NULL", Me.TablaImportar.Item(38, p).Value) & "," '@numctapago
                                    sql &= " 	'" & Me.TablaImportar.Item(39, p).Value & "'," '@condicion_de_pago
                                    Dim concepto As String = Me.TablaImportar.Item(40, p).Value.ToString().Replace("'", " ")
                                    If Len(concepto) > 2000 Then
                                        concepto = Me.TablaImportar.Item(40, p).Value.ToString().Replace("'", " ").ToString.Substring(0, 1000)
                                    Else
                                        concepto = Me.TablaImportar.Item(40, p).Value.ToString().Replace("'", " ")
                                    End If
                                    sql &= " 	'" & concepto & "'," '@conceptos
                                    sql &= " 	'" & Me.TablaImportar.Item(41, p).Value.ToString() & "'," '@combustible
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(42, p).Value Is Nothing Or Me.TablaImportar.Item(42, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(42, p).Value)) & "," '@ieps_3
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(43, p).Value Is Nothing Or Me.TablaImportar.Item(43, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(43, p).Value)) & "," '@ieps_6
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(44, p).Value Is Nothing Or Me.TablaImportar.Item(44, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(44, p).Value)) & "," '@ieps_7
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(45, p).Value Is Nothing Or Me.TablaImportar.Item(45, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(45, p).Value)) & "," '@ieps_8
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(46, p).Value Is Nothing Or Me.TablaImportar.Item(46, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(46, p).Value)) & "," '@ieps_9
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(47, p).Value Is Nothing Or Me.TablaImportar.Item(47, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(47, p).Value)) & "," '@ieps_26
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(48, p).Value Is Nothing Or Me.TablaImportar.Item(48, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(48, p).Value)) & "," '@ieps_30
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(49, p).Value Is Nothing Or Me.TablaImportar.Item(49, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(49, p).Value)) & "," '@ieps_53
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(50, p).Value Is Nothing Or Me.TablaImportar.Item(50, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(50, p).Value)) & "," '@ieps_160
                                    sql &= " 	'" & Me.TablaImportar.Item(51, p).Value.ToString() & "'," '@archivo_xml
                                    sql &= " 	'" & Me.TablaImportar.Item(52, p).Value.ToString() & "'," '@direccion_emisor
                                    sql &= " 	'" & Me.TablaImportar.Item(53, p).Value.ToString() & "'," '@localidad_emisor
                                    sql &= " 	'" & Me.TablaImportar.Item(54, p).Value.ToString() & "'," '@direccion_receptor
                                    sql &= " 	'" & Me.TablaImportar.Item(55, p).Value.ToString() & "', " '@localidad_receptor
                                    sql &= " " & consecutivo & "," '@Consecutivo_Carga
                                    sql &= " " & Me.lstCliente.SelectItem & ", " '@Id_Empresa
                                    sql &= " 0, " & Eventos.Bool2(Me.RadEmitidas.Checked) & " ,'',''," '@Procesada
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(56, p).Value Is Nothing Or Me.TablaImportar.Item(56, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(56, p).Value)) & "," '@iva_8
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(57, p).Value Is Nothing Or Me.TablaImportar.Item(57, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(57, p).Value)) & "" '@IEPS30.4
                                    sql &= " )"


                                    If Eventos.Comando_sql(sql) > 0 Then
                                        If Len(sql) >= 4000 Then
                                            sql = sql.Substring(0, 3999)
                                        End If
                                        Eventos.Insertar_usuariol("Detalle", sql)
                                    End If

                                End If
                            End If
                        Catch ex As Exception

                        End Try

                        frm3.Barra.Value = p
                    Next
                    frm3.Close()
                    RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                End If

            Else
                RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

            End If
        Else
            RadMessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
        Me.Cmd_Procesar.Enabled = True




        Me.TablaImportar.Enabled = True

    End Sub

    Private Sub Importar_XML_DB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Ejecucion = False
        If Permiso(Me.Tag.ToString) Then
            Cargar_clientes()
        Else
            Me.CmdImportar.Enabled = False
            Me.Cmd_Procesar.Enabled = False
            Me.CmdLimpiar.Enabled = False
            RadMessageBox.Show("No tienes permiso para Importar la información...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
        Eventos.DiseñoTabla(Me.TablaImportar)
    End Sub
    Private Sub Cargar_clientes()
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt

        Eventos.DiseñoTabla(TablaImportar)

    End Sub
    Public Function SoloNumeros(ByVal strCadena As String) As Object
        Dim SoloNumero As String = ""
        Dim index As Integer
        For index = 1 To Len(strCadena)
            If (Mid$(strCadena, index, 1) Like "#") _
                Or Mid$(strCadena, index, 1) = "-" Then
                SoloNumero = SoloNumero & Mid$(strCadena, index, 1)
            End If
        Next
        Return SoloNumero
    End Function
    Private Function MetodoP(ByVal cadena As String)
        Dim metodos As String = ""
        Dim sql As String = " select * from Forma_de_Pago  where clave like ('%" & cadena & "%')"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            metodos = ds.Tables(0).Rows(0)("Clave")
        Else
            metodos = "99"
        End If


        Return metodos
    End Function
    Private Function FormaP(ByVal cadena As String)
        Dim metodos As String = ""
        Dim sql As String = " select * from Metodos_de_pago where clave like ('%" & cadena & "%')"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            metodos = Trim(ds.Tables(0).Rows(0)("Clave"))

        End If

        Return metodos
    End Function
    Private Function UsoCFDI(ByVal cadena As String)
        Dim Uso As String = ""
        Dim sql As String = " select * from Uso_CFDI where clave like ('%" & cadena & "%')"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Uso = Trim(ds.Tables(0).Rows(0)("Clave"))
        Else
            Uso = "P01"
        End If

        Return Uso
    End Function
    Private Sub Colorea()
        'Dim frm As New BarraProcesovb
        'frm.Show()
        'frm.Text = "Verificando XML en base de datos por favor espere..."
        'frm.Barra.Minimum = 0
        'frm.Barra.Maximum = Me.TablaImportar.Rows.Count
        Dim dt As DataTable
        Dim Sql As String = " SELECT  UUID FROM Xml_Sat where Id_Empresa = " & Me.lstCliente.SelectItem & "      "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                Dim rows = dt.Select("UUID ='" & Me.TablaImportar.Item(10, i).Value & "'", "UUID ASC")
                If rows.Length > 0 Then
                    Me.TablaImportar.Item(10, i).Style.BackColor = Color.Red
                End If
                'frm.Barra.value = i
            Next

        End If
        ' frm.Close()

        'Dim frm2 As New BarraProcesovb
        'frm2.Show()
        'frm2.Text = "Verificando  XML validos por favor espere..."
        'frm2.Barra.Minimum = 0
        'frm2.Barra.Maximum = Me.TablaImportar.Rows.Count
        For p As Integer = 0 To Me.TablaImportar.RowCount - 1
            If Me.TablaImportar.Item(27, p).Value <= 0 Then
                Me.TablaImportar.Item(10, p).Style.BackColor = Color.Red
                Me.TablaImportar.Item(27, p).Style.BackColor = Color.Red
            End If
            'frm2.Barra.value = p
        Next
        'frm2.Close()
    End Sub

    Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
        'Eventos.Abrir_Manual("Importación XML")
    End Sub

    Private Sub SP1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP1.DoWork
        'My.Forms.Inicio.txtServerDB.Text = serV

    End Sub

    Private Sub SP2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP2.DoWork
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        My.Forms.Inicio.txtServerDB.Text = serV
        If Me.TablaImportar.Rows.Count > 0 Then
            If Me.lstCliente.SelectText <> "" Then
                If RadMessageBox.Show("La Empresa " & Me.lstCliente.SelectText & " es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim consecutivo As Integer
                    If IsDBNull(Eventos.Obtener_DS("SELECT max (Consecutivo_Carga)+1 FROM Xml_Sat ").Tables(0).Rows(0)(0)) Then '@Consecutivo_Carga
                        consecutivo = 1
                    Else
                        consecutivo = Eventos.Obtener_DS("SELECT max (Consecutivo_Carga)+1 FROM Xml_Sat ").Tables(0).Rows(0)(0)
                    End If
                    Dim frm3 As New BarraProcesovb
                    frm3.Show()
                    frm3.Text = "Importando Datos por favor espere..."
                    frm3.Barra.Minimum = 0
                    frm3.Barra.Maximum = Me.TablaImportar.Rows.Count
                    For p As Integer = 0 To Me.TablaImportar.RowCount - 1
                        Try
                            If Me.TablaImportar.Item(10, p).Style.BackColor = Color.Red Then

                            Else
                                If Me.TablaImportar.Item(2, p).Value <> Nothing Then
                                    Dim sql As String = "INSERT INTO dbo.Xml_Sat"
                                    sql &= " 	("
                                    sql &= "         Verificado_Asoc,"
                                    sql &= "         Estado_SAT,"
                                    sql &= "         Version,"
                                    sql &= "         Tipo, "
                                    sql &= "         Fecha_Emision,"
                                    sql &= "         Fecha_Timbrado,"
                                    sql &= "         EstadoPago,"
                                    sql &= "         FechaPago,"
                                    sql &= "         Serie,"
                                    sql &= "         Folio,"
                                    sql &= "         UUID,"
                                    sql &= "         UUID_Relacion,"
                                    sql &= "         RFC_Emisor,"
                                    sql &= "         Nombre_Emisor,"
                                    sql &= "         LugarDeExpedicion,"
                                    sql &= "         RFC_Receptor,"
                                    sql &= "         Nombre_Receptor,"
                                    sql &= "         ResidenciaFiscal,"
                                    sql &= "         NumRegIdTrib,"
                                    sql &= "         UsoCFDI,"
                                    sql &= "         SubTotal,"
                                    sql &= "         Descuento,"
                                    sql &= "         Total_IEPS,"
                                    sql &= "         IVA_16,"
                                    sql &= "         Retenido_IVA,"
                                    sql &= "         Retenido_ISR,"
                                    sql &= "         ISH,"
                                    sql &= "         Total,"
                                    sql &= "         TotalOriginal,"
                                    sql &= "         Total_Trasladados,"
                                    sql &= "         Total_Retenidos,"
                                    sql &= "         Total_LocalTrasladado,"
                                    sql &= "         Total_LocalRetenido,"
                                    sql &= "         Complemento,"
                                    sql &= "         Moneda,"
                                    sql &= "         Tipo_De_Cambio,"
                                    sql &= "         FormaDePago,"
                                    sql &= "         Metodo_de_Pago,"
                                    sql &= "         NumCtaPago,"
                                    sql &= "         Condicion_de_Pago,"
                                    sql &= "         Conceptos,"
                                    sql &= "         Combustible,"
                                    sql &= "         IEPS_3,"
                                    sql &= "         IEPS_6,"
                                    sql &= "         IEPS_7,"
                                    sql &= "         IEPS_8,"
                                    sql &= "         IEPS_9,"
                                    sql &= "         IEPS_26,"
                                    sql &= "         IEPS_30,"
                                    sql &= "         IEPS_53,"
                                    sql &= "         IEPS_160,"
                                    sql &= "         Archivo_XML,"
                                    sql &= "         Direccion_Emisor,"
                                    sql &= "         Localidad_Emisor,"
                                    sql &= "         Direccion_Receptor,"
                                    sql &= "         Localidad_Receptor,"
                                    sql &= "         Consecutivo_Carga,"
                                    sql &= "         Id_Empresa, "
                                    sql &= "         Carga_Procesada,Emitidas,Banco_Origen,Banco_Destino ,IVA_8,IEPS_30_4"
                                    sql &= " 	)"
                                    sql &= "         VALUES "
                                    sql &= " 	("
                                    sql &= " 	'" & Me.TablaImportar.Item(0, p).Value & "'," '@verificado_asoc
                                    sql &= " 	'" & Me.TablaImportar.Item(1, p).Value & "'," '@estado_sat
                                    sql &= " 	'" & Me.TablaImportar.Item(2, p).Value & "'," '@version 
                                    sql &= " 	'" & Me.TablaImportar.Item(3, p).Value & "'," '@ tipo
                                    Dim fecha, fecha2, fecha3 As Date
                                    fecha = IIf(Me.TablaImportar.Item(4, p).Value Is Nothing, "", Me.TablaImportar.Item(4, p).Value)
                                    fecha2 = IIf(Me.TablaImportar.Item(5, p).Value Is Nothing, "", Me.TablaImportar.Item(5, p).Value)
                                    fecha3 = IIf(Me.TablaImportar.Item(7, p).Value Is Nothing Or Me.TablaImportar.Item(7, p).Value.ToString() = "" Or IsDBNull(Me.TablaImportar.Item(7, p).Value), "01/01/2018", Me.TablaImportar.Item(7, p).Value)
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(4, p).Value Is Nothing Or Me.TablaImportar.Item(4, p).Value = "", "NULL", Eventos.Sql_hoy(fecha.ToString.Substring(0, 10))) & ","  '@fecha_emision
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(5, p).Value Is Nothing Or Me.TablaImportar.Item(5, p).Value = "", "NULL", Eventos.Sql_hoy(fecha2.ToString.Substring(0, 10))) & ","  '@fecha_timbrado
                                    sql &= " 	'" & Me.TablaImportar.Item(6, p).Value & "'," '@estadopago
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(7, p).Value Is Nothing Or Me.TablaImportar.Item(7, p).Value.ToString() = "" Or IsDBNull(Me.TablaImportar.Item(7, p).Value), "NULL", Eventos.Sql_hoy(fecha3.ToString.Substring(0, 10))) & "," '@fechapago
                                    If Me.RadEmitidas.Checked = True Then
                                        sql &= " 	'" & IIf(Me.TablaImportar.Item(8, p).Value Is Nothing Or Me.TablaImportar.Item(8, p).Value = "", "I", Me.TablaImportar.Item(8, p).Value.ToString) & "'," '@serie
                                    Else
                                        sql &= " 	'" & Me.TablaImportar.Item(8, p).Value & "'," '@serie
                                    End If

                                    sql &= " 	'" & Me.TablaImportar.Item(9, p).Value & "'," '@folio
                                    sql &= " 	'" & Me.TablaImportar.Item(10, p).Value & "'," '@uuid
                                    sql &= " 	'" & Me.TablaImportar.Item(11, p).Value & "'," '@uuid_Relacion
                                    sql &= " 	'" & Me.TablaImportar.Item(12, p).Value & "'," '@rfc_emisor
                                    sql &= " 	'" & Me.TablaImportar.Item(13, p).Value.ToString.Replace("'", "") & "'," '@nombre_emisor
                                    sql &= " 	'" & Me.TablaImportar.Item(14, p).Value.ToString.Replace("'", "") & "'," '@lugardeexpedicion
                                    sql &= " 	'" & Me.TablaImportar.Item(15, p).Value.ToString.Replace("'", "") & "'," '@rfc_receptor
                                    sql &= " 	'" & Me.TablaImportar.Item(16, p).Value.ToString.Replace("'", "") & "'," '@nombre_receptor
                                    sql &= " 	'" & Me.TablaImportar.Item(17, p).Value.ToString.Replace("'", "") & "'," '@residenciafiscal
                                    sql &= " 	'" & Me.TablaImportar.Item(18, p).Value & "'," '@numregidtrib
                                    Try
                                        Dim uso As String = UsoCFDI(Me.TablaImportar.Item(19, p).Value.ToString.Substring(0, 3))
                                        sql &= " 	'" & uso & "'," '@usocfdi
                                    Catch ex As Exception
                                        sql &= " 	''," '@usocfdi
                                    End Try
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(20, p).Value Is Nothing Or Me.TablaImportar.Item(20, p).Value = "", "0", Val(Me.TablaImportar.Item(20, p).Value)) & "," '@subtotal
                                    Try
                                        sql &= " 	" & IIf(Me.TablaImportar.Item(21, p).Value Is Nothing Or Me.TablaImportar.Item(21, p).Value = "", "0", Val(Me.TablaImportar.Item(21, p).Value)) & "," '@descuento
                                    Catch ex As Exception
                                        sql &= " 	0," '@descuento
                                    End Try
                                    Try
                                        sql &= " 	" & IIf(Me.TablaImportar.Item(22, p).Value Is Nothing Or Me.TablaImportar.Item(22, p).Value = "", "0", Val(Me.TablaImportar.Item(22, p).Value)) & "," '@total_ieps
                                    Catch ex As Exception
                                        sql &= " 	0," '@descuento
                                    End Try

                                    sql &= " 	" & IIf(Me.TablaImportar.Item(23, p).Value Is Nothing Or Me.TablaImportar.Item(23, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(23, p).Value)) & "," '@iva_16
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(24, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(24, p).Value)) & "," '@retenido_iva se Suma el IVA RETENIDO DE TASA 6%
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(25, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(25, p).Value)) & "," '@retenido_isr
                                    sql &= " 	" & Me.TablaImportar.Item(26, p).Value & "," '@ish
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(27, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(27, p).Value)) & "," '@total
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(28, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(28, p).Value)) & "," '@totaloriginal
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(29, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(29, p).Value)) & "," '@total_trasladados
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(30, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(30, p).Value)) & "," '@total_retenidos
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(31, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(31, p).Value)) & "," '@total_localtrasladado
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(32, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(32, p).Value)) & "," '@total_localretenido
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(33, p).Value.ToString() = "", "0", Val(Me.TablaImportar.Item(33, p).Value)) & "," '@complemento
                                    sql &= " 	'" & Me.TablaImportar.Item(34, p).Value & "'," '@moneda
                                    Dim tipo_cambio As String = SoloNumeros(Me.TablaImportar.Item(35, p).Value)
                                    sql &= " 	" & IIf(tipo_cambio = "", "NULL", tipo_cambio) & ","
                                    'sql &= " 	" & Me.TablaImportar.Item(35, p).Value & "," '@tipo_de_cambio
                                    Try
                                        Dim forma As String = FormaP(Me.TablaImportar.Item(36, p).Value.ToString.Substring(0, 2))
                                        If forma = "" Then
                                            sql &= " 	'99'," '@formadepago
                                        Else
                                            sql &= " 	'" & forma & "'," '@formadepago
                                        End If
                                    Catch ex As Exception
                                        sql &= " 	'99'," '@formadepago
                                    End Try


                                    Try
                                        Dim metodo As String = MetodoP(Me.TablaImportar.Item(37, p).Value.ToString.Substring(0, 3)) 'Me.TablaImportar.Item(37, p).Value
                                        sql &= " 	'" & metodo & "'," '@metodo_de_pago
                                    Catch ex As Exception

                                        sql &= " 	'99'," '@metodo_de_pago
                                    End Try


                                    Dim NumCuenta As String = SoloNumeros(Me.TablaImportar.Item(38, p).Value.ToString)
                                    sql &= " 	" & IIf(NumCuenta = "", "NULL", NumCuenta) & ","
                                    'sql &= " 	" & IIf(Me.TablaImportar.Item(38, p).Value Is Nothing, "NULL", Me.TablaImportar.Item(38, p).Value) & "," '@numctapago
                                    sql &= " 	'" & Me.TablaImportar.Item(39, p).Value & "'," '@condicion_de_pago
                                    Dim concepto As String = Me.TablaImportar.Item(40, p).Value.Replace("'", " ")
                                    If Len(concepto) > 2000 Then
                                        concepto = Me.TablaImportar.Item(40, p).Value.Replace("'", " ").ToString.Substring(0, 1000)
                                    Else
                                        concepto = Me.TablaImportar.Item(40, p).Value.Replace("'", " ")
                                    End If
                                    sql &= " 	'" & concepto & "'," '@conceptos
                                    sql &= " 	'" & Me.TablaImportar.Item(41, p).Value & "'," '@combustible
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(42, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(42, p).Value)) & "," '@ieps_3
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(43, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(43, p).Value)) & "," '@ieps_6
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(44, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(44, p).Value)) & "," '@ieps_7
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(45, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(45, p).Value)) & "," '@ieps_8
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(46, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(46, p).Value)) & "," '@ieps_9
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(47, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(47, p).Value)) & "," '@ieps_26
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(48, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(48, p).Value)) & "," '@ieps_30
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(49, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(49, p).Value)) & "," '@ieps_53
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(50, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(50, p).Value)) & "," '@ieps_160
                                    sql &= " 	'" & Me.TablaImportar.Item(51, p).Value & "'," '@archivo_xml
                                    sql &= " 	'" & Me.TablaImportar.Item(52, p).Value & "'," '@direccion_emisor
                                    sql &= " 	'" & Me.TablaImportar.Item(53, p).Value & "'," '@localidad_emisor
                                    sql &= " 	'" & Me.TablaImportar.Item(54, p).Value & "'," '@direccion_receptor
                                    sql &= " 	'" & Me.TablaImportar.Item(55, p).Value & "', " '@localidad_receptor
                                    sql &= " " & consecutivo & "," '@Consecutivo_Carga
                                    sql &= " " & Me.lstCliente.SelectItem & ", " '@Id_Empresa
                                    sql &= " 0, " & Eventos.Bool2(Me.RadEmitidas.Checked) & " ,'',''," '@Procesada
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(56, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(56, p).Value)) & "," '@iva_8
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(57, p).Value.ToString = "", "0", Val(Me.TablaImportar.Item(57, p).Value)) & "" '@IEPS30.4
                                    sql &= " )"


                                    If Eventos.Comando_sql(sql) > 0 Then
                                        If Len(sql) >= 4000 Then
                                            sql = sql.Substring(0, 3999)
                                        End If
                                        Eventos.Insertar_usuariol("Detalle", sql)
                                    End If

                                End If
                            End If
                        Catch ex As Exception

                        End Try

                        frm3.Barra.Value = p
                    Next
                    frm3.Close()
                    RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                End If

            Else
                RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)

            End If
        Else
            RadMessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
        Me.Cmd_Procesar.Enabled = True
    End Sub


End Class
