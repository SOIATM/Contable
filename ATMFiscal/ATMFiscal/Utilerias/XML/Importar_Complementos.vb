Imports Telerik.WinControls
Public Class Importar_Complementos
    Dim Dato As DataSet
    Dim Ejecucion As Boolean
    Dim DatoC As DataSet
    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Columns.Clear()
            Me.RadEmitidas.Checked = True

            Me.lblRegistros.Text = "Total de Registros: 0"
        End If
    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        Me.CmdImportar.Enabled = False
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelXMLComplemento("XML"), Me.TablaImportar)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        My.Forms.Inicio.txtServerDB.Text = serV
        Colorear()
        RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Me.TablaImportar.Enabled = True
        Me.CmdImportar.Enabled = True
    End Sub
    Private Sub Colorear()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Try
            Me.TablaImportar.Rows.RemoveAt(0)
            If Me.RadEmitidas.Checked = True Then


                Dim ds As DataSet = Eventos.Obtener_DS("select Id_Empresa,Razon_Social FROM Empresa where Reg_fed_causantes = '" & Trim(Me.TablaImportar.Item(9, 0).Value) & "'")
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.lstCliente.SelectText = ds.Tables(0).Rows(0)("Razon_Social")
                    Colorea()
                    Me.lblRegistros.Text = "Total de Registros: " & Me.TablaImportar.Rows.Count
                Else
                    RadMessageBox.Show("El RFC " & Me.TablaImportar.Item(9, 0).Value & " no corresponde a la Empresa...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                    Me.CmdLimpiar.PerformClick()
                End If


            Else
                Dim ds As DataSet = Eventos.Obtener_DS("select Id_Empresa,Razon_Social FROM Empresa where Reg_fed_causantes = '" & Trim(Me.TablaImportar.Item(12, 0).Value) & "'")
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.lstCliente.SelectText = ds.Tables(0).Rows(0)("Razon_Social")
                    Colorea()
                    Me.lblRegistros.Text = "Total de Registros: " & Me.TablaImportar.Rows.Count
                Else
                    RadMessageBox.Show("El RFC " & Me.TablaImportar.Item(12, 0).Value & " no corresponde a la Empresa...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                    Me.CmdLimpiar.PerformClick()
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
                Try
                    If RadMessageBox.Show("La Empresa " & Me.lstCliente.SelectText & " es correcta?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        Dim consecutivo As Integer

                        If IsDBNull(Eventos.Obtener_DS("SELECT max (Consecutivo_Carga)+1 FROM Xml_Complemento ").Tables(0).Rows(0)(0)) Then '@Consecutivo_Carga
                            consecutivo = 1
                        Else
                            consecutivo = Eventos.Obtener_DS("SELECT max (Consecutivo_Carga)+1 FROM Xml_Complemento ").Tables(0).Rows(0)(0)
                        End If
                        Dim frm3 As New BarraProcesovb
                        frm3.Show()
                        frm3.Text = "Importando Datos por favor espere..."
                        frm3.Barra.Minimum = 0
                        frm3.Barra.Maximum = Me.TablaImportar.Rows.Count
                        For p As Integer = 0 To Me.TablaImportar.RowCount - 1


                            If Me.TablaImportar.Item(1, p).Style.BackColor = Color.Red Then
                                'No hace el registro
                            Else
                                If Me.TablaImportar.Item(2, p).Value <> Nothing Then
                                    Dim sql As String = "INSERT INTO dbo.Xml_Complemento"
                                    sql &= " 	("
                                    sql &= "   Tipo,"
                                    sql &= "   UUID,"
                                    sql &= "   Fecha_Emision,"
                                    sql &= "   Folio,"
                                    sql &= "   Serie    ,"
                                    sql &= "   SubTotal,"
                                    sql &= "   Moneda,"
                                    sql &= "   Total,"
                                    sql &= "   LugarExpedicion,"
                                    sql &= "   RFC_Emisor,"
                                    sql &= "   Nombre_Emisor,"
                                    sql &= "   Regimen_Fiscal,"
                                    sql &= "   RFC_Receptor,"
                                    sql &= "   Nombre_Receptor,"
                                    sql &= "   UsoCFDI,"
                                    sql &= "   ClaveProdServ,"
                                    sql &= "   Cantidad,"
                                    sql &= "   Unidad,"
                                    sql &= "   Descripcion,"
                                    sql &= "   Valor_Unitario,"
                                    sql &= "   Importe,"
                                    sql &= "   FechaPago,"
                                    sql &= "   FormaDePago,"
                                    sql &= "   MonedaP,"
                                    sql &= "   TipoCambioP,"
                                    sql &= "   Monto,"
                                    sql &= "   Num_Operacion,"
                                    sql &= "   RfcEmisorCtaOrd,"
                                    sql &= "   NomBancoOrdExt,"
                                    sql &= "   CtaOrdenante,"
                                    sql &= "   RfcEmisorCtaBen,"
                                    sql &= "   CtaBeneficiario,"
                                    sql &= "   TipoCadPago,"
                                    sql &= "   CertPago,"
                                    sql &= "   CadPago,"
                                    sql &= "   SelloPago,"
                                    sql &= "   IdDocumento,"
                                    sql &= "   SerieDR,"
                                    sql &= "   FolioDR,"
                                    sql &= "   MonedaDR,"
                                    sql &= "   TipoCambioDR,"
                                    sql &= "   MetodoDePagoDR,"
                                    sql &= "   NumParcialidad,"
                                    sql &= "   ImpSaldoAnt,"
                                    sql &= "   ImpPagado,"
                                    sql &= "   ImpSaldoInsoluto,"
                                    sql &= "   Consecutivo_Carga,"
                                    sql &= "   Id_Empresa,"
                                    sql &= "   Carga_Procesada, Emitidas ,Banco_Origen,Banco_Destino"
                                    sql &= " 	)"
                                    sql &= "         VALUES "
                                    sql &= " 	("
                                    sql &= " 	'" & Me.TablaImportar.Item(0, p).Value & "'," '@Tipo
                                    sql &= " 	'" & Me.TablaImportar.Item(1, p).Value & "'," '@UUID
                                    Dim fecha, fecha2 As Date
                                    fecha = IIf(Me.TablaImportar.Item(2, p).Value Is Nothing Or IsDBNull(Me.TablaImportar.Item(2, p).Value) = True, "", Me.TablaImportar.Item(2, p).Value)
                                    fecha2 = IIf(Me.TablaImportar.Item(21, p).Value Is Nothing Or IsDBNull(Me.TablaImportar.Item(21, p).Value) = True, Nothing, Me.TablaImportar.Item(21, p).Value)
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(2, p).Value Is Nothing Or Me.TablaImportar.Item(2, p).Value = "", "NULL", Eventos.Sql_hoy(fecha.ToString.Substring(0, 10))) & ","  '@Fecha_Emision
                                    sql &= " 	'" & Me.TablaImportar.Item(3, p).Value & "'," '@ Folio
                                    If Me.RadEmitidas.Checked = True Then
                                        sql &= " 	'" & IIf(Me.TablaImportar.Item(4, p).Value.ToString = Nothing Or Me.TablaImportar.Item(4, p).Value.ToString = "" Or IsDBNull(Me.TablaImportar.Item(4, p).Value.ToString) = True, "P", Me.TablaImportar.Item(4, p).Value.ToString) & "'," '@serie
                                    Else
                                        sql &= " 	'" & Me.TablaImportar.Item(4, p).Value & "'," '@serie
                                    End If
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(5, p).Value Is Nothing Or Me.TablaImportar.Item(5, p).Value = "", "0", Val(Me.TablaImportar.Item(5, p).Value)) & "," '@SubTotal
                                    sql &= " 	'" & Me.TablaImportar.Item(6, p).Value & "'," '@Moneda
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(7, p).Value Is Nothing Or Me.TablaImportar.Item(7, p).Value = "", "0", Val(Me.TablaImportar.Item(7, p).Value)) & "," '@Total
                                    sql &= " 	'" & Me.TablaImportar.Item(8, p).Value & "'," '@LugarExpedicion
                                    sql &= " 	'" & Me.TablaImportar.Item(9, p).Value & "'," '@RFC_Emisor
                                    sql &= " 	'" & Me.TablaImportar.Item(10, p).Value.ToString.Trim().Replace("'", " ").Replace("Â", "A").Replace("´", "") & "'," '@Nombre_Emisor
                                    sql &= " 	'" & Me.TablaImportar.Item(11, p).Value.ToString.Trim().Replace("'", " ").Replace("Â", "A").Replace("´", "") & "'," '@Regimen_Fiscal
                                    sql &= " 	'" & Me.TablaImportar.Item(12, p).Value.ToString.Trim().Replace("'", " ").Replace("Â", "A").Replace("´", "") & "'," '@RFC_Receptor
                                    sql &= " 	'" & Me.TablaImportar.Item(13, p).Value.ToString.Trim().Replace("'", " ").Replace("Â", "A").Replace("´", "") & "'," '@Nombre_Receptor
                                    Dim uso As String = UsoCFDI(Me.TablaImportar.Item(14, p).Value) '
                                    sql &= " 	'" & uso & "'," '@usocfdi
                                    sql &= " 	'" & Me.TablaImportar.Item(15, p).Value & "'," '@ClaveProdServ
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(16, p).Value Is Nothing Or Me.TablaImportar.Item(17, p).Value = "", "0", Val(Me.TablaImportar.Item(17, p).Value)) & "," '@Cantidad
                                    sql &= " 	'" & Me.TablaImportar.Item(17, p).Value & "'," '@Unidad
                                    sql &= " 	'" & Me.TablaImportar.Item(18, p).Value & "'," '@Descripcion
                                    sql &= " 	'" & Me.TablaImportar.Item(19, p).Value & "'," '@Valor_Unitario
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(20, p).Value Is Nothing Or Me.TablaImportar.Item(20, p).Value = "", "0", Val(Me.TablaImportar.Item(20, p).Value)) & "," '@Importe
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(21, p).Value Is Nothing Or Me.TablaImportar.Item(21, p).Value = "", "NULL", Eventos.Sql_hoy(fecha2.ToString.Substring(0, 10))) & ","  '@FechaPago
                                    Dim Forma_Pago As String = MetodoP(Me.TablaImportar.Item(22, p).Value)  '@FormaDePago
                                    sql &= " 	'" & Forma_Pago & "'," '@FormaDePago
                                    sql &= " 	'" & Me.TablaImportar.Item(23, p).Value & "'," '@MonedaP
                                    sql &= " 	'" & Me.TablaImportar.Item(24, p).Value & "'," '@TipoCambioP
                                    sql &= " 	" & IIf(Me.TablaImportar.Item(25, p).Value Is Nothing Or Me.TablaImportar.Item(25, p).Value = "", "0", Val(Me.TablaImportar.Item(25, p).Value)) & "," '@Monto
                                    sql &= " 	'" & Me.TablaImportar.Item(26, p).Value & "'," '@Num_Operacion
                                    sql &= " 	'" & Me.TablaImportar.Item(27, p).Value & "'," '@RfcEmisorCtaOrd
                                    sql &= " 	'" & Me.TablaImportar.Item(28, p).Value & "'," '@NomBancoOrdExt
                                    sql &= " 	'" & Me.TablaImportar.Item(29, p).Value & "'," '@CtaOrdenante
                                    sql &= " 	'" & Me.TablaImportar.Item(30, p).Value & "'," '@RfcEmisorCtaBen
                                    sql &= " 	'" & Me.TablaImportar.Item(31, p).Value & "'," '@CtaBeneficiario
                                    sql &= " 	'" & Me.TablaImportar.Item(32, p).Value & "'," '@TipoCadPago
                                    sql &= " 	'" & Me.TablaImportar.Item(33, p).Value & "'," '@CertPago
                                    sql &= " 	'" & Me.TablaImportar.Item(34, p).Value & "'," '@CadPago
                                    sql &= " 	'" & Me.TablaImportar.Item(35, p).Value & "'," '@SelloPago
                                    sql &= " 	'" & Me.TablaImportar.Item(36, p).Value & "'," '@IdDocumento
                                    sql &= " 	'" & Me.TablaImportar.Item(37, p).Value & "'," '@SerieDR
                                    sql &= " 	'" & Me.TablaImportar.Item(38, p).Value & "'," '@FolioDR
                                    sql &= " 	'" & Me.TablaImportar.Item(39, p).Value & "'," '@MonedaDR
                                    sql &= " 	'" & Me.TablaImportar.Item(40, p).Value & "'," '@TipoCambioDR
                                    sql &= " 	'" & Me.TablaImportar.Item(41, p).Value & "'," '@MetodoDePagoDR
                                    sql &= " 	'" & Me.TablaImportar.Item(42, p).Value & "'," '@NumParcialidad
                                    Try
                                        sql &= " 	" & IIf(Me.TablaImportar.Item(43, p).Value Is DBNull.Value, "0", Val(Me.TablaImportar.Item(43, p).Value)) & "," '@ImpSaldoAnt
                                    Catch ex As Exception
                                        sql &= " 0,"
                                    End Try

                                    Try
                                        sql &= " 	" & IIf(Me.TablaImportar.Item(44, p).Value Is DBNull.Value, "0", Val(Me.TablaImportar.Item(44, p).Value)) & "," '@ImpPagado
                                    Catch ex As Exception
                                        sql &= " 0,"
                                    End Try

                                    Try
                                        sql &= " 	" & IIf(Me.TablaImportar.Item(45, p).Value Is DBNull.Value, "0", Val(Me.TablaImportar.Item(45, p).Value)) & "," '@ImpSaldoInsoluto
                                    Catch ex As Exception
                                        sql &= " 0," '@ImpSaldoInsoluto
                                    End Try
                                    sql &= " " & consecutivo & "," '@Consecutivo_Carga
                                    sql &= " " & Me.lstCliente.SelectItem & ", " '@Id_Empresa
                                    sql &= " 0, " & Eventos.Bool2(Me.RadEmitidas.Checked) & " ,'',''" '@Procesada
                                    sql &= " )"
                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("Detalle", sql)
                                    Else
                                        Dim da As String = ""
                                    End If
                                End If
                            End If
                            frm3.Barra.Value = p
                        Next
                        frm3.Close()
                        RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

                    End If

                Catch ex As Exception

                End Try
            Else
                RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

            End If
        Else
            RadMessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
        Colorear()
        Me.Cmd_Procesar.Enabled = True
    End Sub

    Private Sub Importar_Complementos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Ejecucion = False
        If Permiso(Me.Tag.ToString) Then
            Cargar_clientes()
            Eventos.DiseñoTablaEnca(Me.TablaImportar)
        Else
            Me.CmdImportar.Enabled = False
            Me.Cmd_Procesar.Enabled = False
            Me.CmdLimpiar.Enabled = False
            MessageBox.Show("No tienes permiso para Importar la información...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
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
        Dim sql As String = " select * from Metodos_de_Pago where clave like ('%" & cadena & "%')"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            metodos = ds.Tables(0).Rows(0)("Clave")
        Else
            metodos = "99"
        End If


        Return metodos
    End Function
    Private Function UsoCFDI(ByVal cadena As String)
        Dim Uso As String = ""
        Dim sql As String = " select * from Uso_CFDI where Descripcion like ('%" & cadena & "%')"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Uso = Trim(ds.Tables(0).Rows(0)("Clave"))
        Else
            Uso = "P01"
        End If

        Return Uso
    End Function
    Private Sub Colorea()
        Try


            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Text = "Verificando XML en base de datos por favor espere..."
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.TablaImportar.Rows.Count
            Dim dt As DataTable
            Dim Sql As String = " SELECT  UUID+IdDocumento AS UUID FROM Xml_Complemento where Id_Empresa = " & Me.lstCliente.SelectItem & "      "
            Dim ds As DataSet = Eventos.Obtener_DS(Sql)
            If ds.Tables(0).Rows.Count > 0 Then
                dt = ds.Tables(0)
                For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                    Try
                        Dim rows = dt.Select("UUID ='" & Me.TablaImportar.Item(1, i).Value.ToString + Me.TablaImportar.Item(36, i).Value.ToString & "' ", "UUID ASC")
                        If rows.Length > 0 Then
                            Me.TablaImportar.Item(1, i).Style.BackColor = Color.Red
                        End If
                    Catch ex As Exception

                    End Try

                    frm.Barra.Value = i
                Next

            End If
            frm.Close()

            Dim frm2 As New BarraProcesovb
            frm2.Show()
            frm2.Text = "Verificando  XML validos por favor espere..."
            frm2.Barra.Minimum = 0
            frm2.Barra.Maximum = Me.TablaImportar.Rows.Count
            For p As Integer = 0 To Me.TablaImportar.RowCount - 1
                If Me.TablaImportar.Item(25, p).Value <= 0 Then
                    Me.TablaImportar.Item(1, p).Style.BackColor = Color.Red
                    Me.TablaImportar.Item(25, p).Style.BackColor = Color.Red
                End If
                frm2.Barra.Value = p
            Next
            frm2.Close()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub CmdManual_Click(sender As Object, e As EventArgs) Handles CmdManual.Click
        'Eventos.Abrir_Manual("XML Complementos")
    End Sub

    Private Sub SP1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP1.DoWork

    End Sub

    Private Sub SP2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SP2.DoWork

    End Sub
End Class