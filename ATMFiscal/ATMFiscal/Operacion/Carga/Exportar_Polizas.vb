Imports Telerik.WinControls
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.ComponentModel
Imports System.Math
Imports System.Security.AccessControl
Public Class Exportar_Polizas
	Dim mes = Now.Date.Month.ToString
	Dim anio = Str(DateTime.Now.Year)
	Private Sub Exportar_Polizas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		Cargar_clientes()
		Eventos.DiseñoTabla(Me.TablaImportar)
		Dim i As Integer
		For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
			If i >= 2004 Then
				Me.comboAño.Items.Add(Str(i))
			End If
		Next
		Me.comboAño.Items.Add("*")
		Me.comboAño.Text = anio
		Me.ComboMes.Items.Add("*")
		If Len(mes) < 2 Then
			mes = "0" & mes
		End If
		Me.ComboMes.Text = mes
	End Sub

	Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
		If Me.TablaImportar.Rows.Count > 0 Then
			Me.TablaImportar.Rows.Clear()
			Me.lblRegistros.Text = "Total de Registros: "
			Me.RadMensual.Checked = True
			Me.comboAño.Text = anio
			Me.ComboMes.Text = mes
		End If
	End Sub

	Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
		Me.Close()
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
        Me.lblRegistros.Text = "Total de Registros: "
	End Sub
    Sub AddFileSecurity(ByVal fileName As String, ByVal account As String, ByVal rights As FileSystemRights, ByVal controlType As AccessControlType)
        Dim fSecurity As FileSecurity = File.GetAccessControl(fileName)
        Dim accessRule As FileSystemAccessRule = New FileSystemAccessRule(account, rights, controlType)
        fSecurity.AddAccessRule(accessRule)
        File.SetAccessControl(fileName, fSecurity)
    End Sub


    Private Sub CmdGenerar_Click(sender As Object, e As EventArgs) Handles CmdGenerar.Click
        Try
            Dim ListaPermisos As System.Security.AccessControl.AuthorizationRuleCollection
            ListaPermisos = System.IO.Directory.GetAccessControl(Application.StartupPath).GetAccessRules(True, True, GetType(System.Security.Principal.NTAccount))
            AddFileSecurity(Application.StartupPath, My.User.Name, FileSystemRights.Delete, AccessControlType.Deny)
        Catch ex As Exception
            MessageBox.Show(ex.Source)
        End Try
        If Me.TablaImportar.Rows.Count > 0 Then
            Genera_TXT()
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha cargado ningun Registro...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub
    Private Sub Genera_TXT()
        Dim fila As String = Nothing
        Dim strStreamW As Stream = Nothing
        Dim strStreamWriter As StreamWriter = Nothing
        Dim texto As String = Nothing
        ' Donde guardamos los paths (la direccion de mi sukiri) de los archivos que vamos a estar utilizando ..
        Dim PathArchivo As String
        Dim nuevo_archivo As String
        Dim Ordinal As String

        Dim Anio As String = ""
        Dim Mes As String = ""
        Dim Tipo As String = ""
        Dim Poliza As String = ""
        Dim Dia As String = ""
        Dim Detalle_CE As String = ""
        Dim rfc_Ce As String = ""
        Dim No_Cheque As String = ""
        Dim No_Banco As String = ""
        Dim No_BancoD As String = ""
        Dim Cuenta As String = ""
        Dim Descripcion As String = ""
        Dim Movimiento As String = ""
        Dim Cuenta_Origen As String = ""
        Dim Cuenta_destino As String = ""
        Dim Importe As String = ""
        Dim P_Aplicar As String = ""
        Dim Imp_Reg_Simplificado As String = ""
        Dim Nom_Cuenta As String = ""
        Dim Fecha_Mov As String = ""
        Dim Importe_Factura As String = ""
        Dim Folio_Fiscal_f As String = ""
        Dim Moneda As String = ""
        Dim Importe_cheque As String = ""
        Dim numero_comprobante As String = ""
        Dim por_ret_IVA As String = ""
        Dim por_ret_ISR As String = ""
        Dim por_ret_x As String = ""
        Dim por_ret_Y As String = ""
        Dim Tipo_Comprobante As String = ""
        Dim N As String = ""
        Dim rfcNombre As String = Eventos.ObtenerValorDB("Empresa", "Reg_fed_causantes", " Id_Empresa= " & Me.lstCliente.SelectItem & "", 1)
        Dim mess As String = IIf(Me.ComboMes.Text = "*", "13", Me.ComboMes.Text)
        Dim parte As String = rfcNombre & " " & Me.comboAño.Text & " " & mess  'Format(Today.Date, "yyyyMMdd")
        Try
            If Directory.Exists(Application.StartupPath & "\Polizas") = False Then ' si no existe la carpeta se crea
                Directory.CreateDirectory(Application.StartupPath & "\Polizas")
            End If
            Windows.Forms.Cursor.Current = Cursors.WaitCursor
            PathArchivo = Application.StartupPath & "\Polizas\" & parte & "-1.txt" ' Se determina el nombre del archivo para la tranferencia concatenando la fecha actual

            If File.Exists(PathArchivo) Then

                For Each item As String In Directory.GetFiles(Application.StartupPath & "\Polizas", "*.txt", False)
                    Dim fecha As String = File.GetLastWriteTime(item)

                    nuevo_archivo = 1 + Val(Path.GetFileName(item).ToString.Substring(9, 2))
                Next
                PathArchivo = Application.StartupPath & "\Polizas\" & parte & "-" & nuevo_archivo & ".txt" ' Se determina nuevo nombre del archivo
                strStreamW = File.Create(PathArchivo)
                strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default) ' tipo de codificacion para escritura del archivo


                Me.Barra.Maximum = Me.TablaImportar.RowCount - 1
                Me.Barra.Minimum = 0
                Me.Barra.Value1 = 0
                Dim contador As Integer = 0
                For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1


                    Anio = IIf(IsNothing(Me.TablaImportar.Item(An.Index, i).Value), "", Me.TablaImportar.Item(An.Index, i).Value).ToString
                    Mes = IIf(IsNothing(Me.TablaImportar.Item(MS.Index, i).Value), "", Me.TablaImportar.Item(MS.Index, i).Value).ToString
                    Tipo = IIf(IsNothing(Me.TablaImportar.Item(Tpol.Index, i).Value), "", Me.TablaImportar.Item(Tpol.Index, i).Value).ToString.PadLeft(3, "0")
                    Poliza = IIf(IsNothing(Me.TablaImportar.Item(Np.Index, i).Value), "", Me.TablaImportar.Item(Np.Index, i).Value).ToString.PadLeft(6, "0")
                    Dia = IIf(IsNothing(Me.TablaImportar.Item(Dp.Index, i).Value), "", Me.TablaImportar.Item(Dp.Index, i).Value).ToString.PadRight(2, "0")
                    Detalle_CE = IIf(IsNothing(Me.TablaImportar.Item(Dce.Index, i).Value), "", Me.TablaImportar.Item(Dce.Index, i).Value).ToString.PadLeft(1, " ")
                    rfc_Ce = IIf(IsNothing(Me.TablaImportar.Item(RFCce.Index, i).Value), "", Me.TablaImportar.Item(RFCce.Index, i).Value).ToString.PadRight(13, " ")
                    No_Cheque = IIf(IsNothing(Me.TablaImportar.Item(Nch.Index, i).Value), "", Me.TablaImportar.Item(Nch.Index, i).Value).ToString.PadLeft(20, " ")
                    No_Banco = IIf(IsNothing(Me.TablaImportar.Item(Nb.Index, i).Value), "", Val(Me.TablaImportar.Item(Nb.Index, i).Value)).ToString.PadLeft(3, " ")
                    No_BancoD = IIf(IsNothing(Me.TablaImportar.Item(BD.Index, i).Value), "", Val(Me.TablaImportar.Item(BD.Index, i).Value)).ToString.PadLeft(3, " ")
                    Movimiento = IIf(IsNothing(Me.TablaImportar.Item(Tm.Index, i).Value), "", Me.TablaImportar.Item(Tm.Index, i).Value).ToString.PadRight(1, " ")
                    Cuenta = IIf(IsNothing(Me.TablaImportar.Item(Cta.Index, i).Value), "", Me.TablaImportar.Item(Cta.Index, i).Value).ToString.PadRight(16, " ")
                    Descripcion = IIf(IsNothing(Me.TablaImportar.Item(Des.Index, i).Value), "", Me.TablaImportar.Item(Des.Index, i).Value).ToString.PadRight(50, " ") '50

                    Fecha_Mov = IIf(IsNothing(Me.TablaImportar.Item(FM.Index, i).Value), "", Me.TablaImportar.Item(FM.Index, i).Value).ToString
                    Cuenta_Origen = IIf(IsNothing(Me.TablaImportar.Item(CtaO.Index, i).Value), "", Me.TablaImportar.Item(CtaO.Index, i).Value).ToString.PadRight(40, " ") '40 
                    Cuenta_destino = IIf(IsNothing(Me.TablaImportar.Item(CtaD.Index, i).Value), "", Me.TablaImportar.Item(CtaD.Index, i).Value).ToString.PadRight(40, " ") '40 
                    ' Importe = IIf(IsNothing(Me.TablaImportar.Item(Imp.Index, i).Value), "", String.Format(Globalization.CultureInfo.InvariantCulture, "{0:N2}", Me.TablaImportar.Item(Imp.Index, i).Value)).ToString.PadLeft(16, " ") '17
                    Importe = IIf(IsNothing(Me.TablaImportar.Item(Imp.Index, i).Value), 0, Math.Round(Convert.ToDecimal(Me.TablaImportar.Item(Imp.Index, i).Value), 2, MidpointRounding.ToEven))
                    Importe = Importe.ToString.PadLeft(16, " ") '17
                    P_Aplicar = IIf(IsNothing(Me.TablaImportar.Item(PA.Index, i).Value), "", Me.TablaImportar.Item(PA.Index, i).Value).ToString.PadRight(1, " ") '1
                    Imp_Reg_Simplificado = IIf(IsNothing(Me.TablaImportar.Item(isr.Index, i).Value), "0.00", Me.TablaImportar.Item(isr.Index, i).Value).ToString.PadLeft(4, " ") '4
                    Nom_Cuenta = IIf(IsNothing(Me.TablaImportar.Item(NCta.Index, i).Value), "", Me.TablaImportar.Item(NCta.Index, i).Value).ToString.PadRight(53, " ") '53
                    If Len(Nom_Cuenta) > 30 Then
                        Nom_Cuenta = Nom_Cuenta.ToString.Substring(0, 30).PadRight(53, " ")
                    End If
                    Importe_Factura = IIf(IsNothing(Me.TablaImportar.Item(ImpF.Index, i).Value), 0, Math.Round(Convert.ToDecimal(Me.TablaImportar.Item(ImpF.Index, i).Value), 2, MidpointRounding.ToEven))
                    Importe_Factura = Importe_Factura.ToString.PadLeft(16, " ") '17
                    Folio_Fiscal_f = IIf(IsNothing(Me.TablaImportar.Item(Ff.Index, i).Value), "", Me.TablaImportar.Item(Ff.Index, i).Value).ToString.PadLeft(36, " ") '36
                    Moneda = IIf(IsNothing(Me.TablaImportar.Item(Mone.Index, i).Value), "", Me.TablaImportar.Item(Mone.Index, i).Value).ToString.PadLeft(3, " ") '3
                    'Importe_cheque = IIf(IsNothing(Me.TablaImportar.Item(ImpCh.Index, i).Value), "", String.Format(Globalization.CultureInfo.InvariantCulture, "{0:N2}",Me.TablaImportar.Item(ImpCh.Index, i).Value)).ToString.PadLeft(16, " ") '17
                    Importe_cheque = IIf(IsNothing(Me.TablaImportar.Item(ImpCh.Index, i).Value), 0, Math.Round(Convert.ToDecimal(Me.TablaImportar.Item(ImpCh.Index, i).Value), 2, MidpointRounding.ToEven))
                    Importe_cheque = Importe_cheque.ToString.PadLeft(16, " ") '17


                    numero_comprobante = IIf(IsNothing(Me.TablaImportar.Item(NC.Index, i).Value), "", Me.TablaImportar.Item(NC.Index, i).Value).ToString.PadLeft(7, " ") '7
                    por_ret_IVA = IIf(IsNothing(Me.TablaImportar.Item(PRIVA.Index, i).Value), "0.00", Me.TablaImportar.Item(PRIVA.Index, i).Value).ToString.PadRight(6, " ") '4
                    por_ret_ISR = IIf(IsNothing(Me.TablaImportar.Item(PRISR.Index, i).Value), "0.00", Me.TablaImportar.Item(PRISR.Index, i).Value).ToString.PadRight(6, " ") '4
                    por_ret_x = IIf(IsNothing(Me.TablaImportar.Item(PRX.Index, i).Value), "0.00", Me.TablaImportar.Item(PRX.Index, i).Value).ToString.PadRight(8, " ") '4
                    por_ret_Y = IIf(IsNothing(Me.TablaImportar.Item(PRY.Index, i).Value), "0.00000", Me.TablaImportar.Item(PRY.Index, i).Value).ToString.PadRight(7, " ") '7
                    Tipo_Comprobante = IIf(IsNothing(Me.TablaImportar.Item(TC.Index, i).Value), "", Me.TablaImportar.Item(TC.Index, i).Value).ToString.PadLeft(1, " ") '1
                    N = IIf(IsNothing(Me.TablaImportar.Item(M.Index, i).Value), "", Me.TablaImportar.Item(M.Index, i).Value).ToString
                    parte = Format(Today.Date, "ddMMyyyy")
                    Dim cadena As String = ""


                    If Me.TablaImportar.Item(Tm.Index, i).Value = 0 Then 'Titulo
                        If i = 0 Then
                        Else
                            strStreamWriter.WriteLine("//")
                        End If

                        cadena = Anio & Mes & Tipo & Poliza & Dia & "                    " & Descripcion & Movimiento & Importe & " " & "0" & "                                                               " & Fecha_Mov & " 12:00:00 a.m.    0"

                    ElseIf Me.TablaImportar.Item(Tm.Index, i).Value = 1 Or Me.TablaImportar.Item(Tm.Index, i).Value = 2 Then 'cargo o abono
                        cadena = Anio & Mes & Tipo & Poliza & Dia & Cuenta & "    " & Descripcion & Movimiento & Importe & " " & "0" & "                             " & Imp_Reg_Simplificado & Nom_Cuenta & Today.ToShortDateString & " 12:00:00 a.m.                " & por_ret_IVA & por_ret_ISR & por_ret_x & por_ret_Y & " D                    N"

                    ElseIf Me.TablaImportar.Item(Tm.Index, i).Value = 3 Then ' contabilidad electronica

                        If Me.TablaImportar.Item(Dce.Index, i).Value = "H" Then 'cheque 
                            cadena = Anio & Mes & Tipo & Poliza & "  H" & rfc_Ce & No_Cheque & No_Banco & "                                 3" & Cuenta_Origen & "                                                                                    " & Fecha_Mov & Importe_cheque & "                                                1.00000"
                        ElseIf Me.TablaImportar.Item(Dce.Index, i).Value = "T" Then ' tranferencia

                            cadena = Anio & Mes & Tipo & Poliza & "  T" & rfc_Ce & "                    " & No_Banco & "                                 3" & Cuenta_Origen & "" & No_BancoD & "       " & Cuenta_destino & Fecha_Mov & Importe_Factura & "                                                1.00000"

                        ElseIf Me.TablaImportar.Item(Dce.Index, i).Value = "P" Then ' efectivo
                            cadena = Anio & Mes & Tipo & Poliza & "  P" & rfc_Ce & "                                                        3                                                                                          " & Fecha_Mov & Importe_Factura & "                                                0.0000001"
                        ElseIf Me.TablaImportar.Item(Dce.Index, i).Value = "C" Then ' Factura
                            cadena = Anio & Mes & Tipo & Poliza & "  C" & rfc_Ce & "                                                        3                                                                                          " & Fecha_Mov & Importe_Factura & Folio_Fiscal_f & "            1.00000                                                                                                                 "
                        End If
                    ElseIf Me.TablaImportar.Item(Tm.Index, i).Value = "Pagos" Then ' Pagos
                        cadena = Anio & Mes & Tipo & Poliza & Dia & Cuenta & "    " & Descripcion & Movimiento & Importe & " " & "0" & "                 " & Imp_Reg_Simplificado & Nom_Cuenta & Today & "              " & por_ret_IVA & por_ret_ISR & por_ret_x & por_ret_Y & " D                    N"

                    End If
                    strStreamWriter.WriteLine(cadena)

                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                        Me.Barra.Minimum = 0
                        Me.Cursor = Cursors.Arrow
                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        RadMessageBox.Show("Proceso terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                        Me.Barra.Value1 = 0
                    Else
                        Me.Barra.Value1 += 1
                    End If
                Next
                strStreamWriter.Flush()
                strStreamWriter.Close() ' cerramos



            Else
                nuevo_archivo = 1
                ' si no existe se crea ys e eliminan todos los anteriores
                For Each item As String In Directory.GetFiles(Application.StartupPath & "\Polizas", "*.txt", False)
                    My.Computer.FileSystem.DeleteFile(item)
                Next
                strStreamW = File.Create(PathArchivo)
                strStreamWriter = New StreamWriter(strStreamW, System.Text.Encoding.Default) ' tipo de codificacion para escritura del archivo
                'Linea pricipal 
                Ordinal = nuevo_archivo
                Ordinal = Format(Int(Ordinal), "0000")
                Me.Barra.Maximum = Me.TablaImportar.RowCount - 1
                Me.Barra.Minimum = 0
                Me.Barra.Value1 = 0
                Dim contador As Integer = 0
                For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1

                    Anio = IIf(IsNothing(Me.TablaImportar.Item(An.Index, i).Value), "", Me.TablaImportar.Item(An.Index, i).Value).ToString
                    Mes = IIf(IsNothing(Me.TablaImportar.Item(MS.Index, i).Value), "", Me.TablaImportar.Item(MS.Index, i).Value).ToString
                    Tipo = IIf(IsNothing(Me.TablaImportar.Item(Tpol.Index, i).Value), "", Me.TablaImportar.Item(Tpol.Index, i).Value).ToString.PadLeft(3, "0")
                    Poliza = IIf(IsNothing(Me.TablaImportar.Item(Np.Index, i).Value), "", Me.TablaImportar.Item(Np.Index, i).Value).ToString.PadLeft(6, "0")
                    Dia = IIf(IsNothing(Me.TablaImportar.Item(Dp.Index, i).Value), "", Me.TablaImportar.Item(Dp.Index, i).Value).ToString.PadRight(2, "0")
                    Detalle_CE = IIf(IsNothing(Me.TablaImportar.Item(Dce.Index, i).Value), "", Me.TablaImportar.Item(Dce.Index, i).Value).ToString.PadLeft(1, " ")
                    rfc_Ce = IIf(IsNothing(Me.TablaImportar.Item(RFCce.Index, i).Value), "", Me.TablaImportar.Item(RFCce.Index, i).Value).ToString.PadRight(13, " ")
                    No_Cheque = IIf(IsNothing(Me.TablaImportar.Item(Nch.Index, i).Value), "", Me.TablaImportar.Item(Nch.Index, i).Value).ToString.PadLeft(20, " ")
                    No_Banco = IIf(IsNothing(Me.TablaImportar.Item(Nb.Index, i).Value), "", Val(Me.TablaImportar.Item(Nb.Index, i).Value)).ToString.PadLeft(3, " ")
                    No_BancoD = IIf(IsNothing(Me.TablaImportar.Item(BD.Index, i).Value), "", Val(Me.TablaImportar.Item(BD.Index, i).Value)).ToString.PadLeft(3, " ")
                    Movimiento = IIf(IsNothing(Me.TablaImportar.Item(Tm.Index, i).Value), "", Me.TablaImportar.Item(Tm.Index, i).Value).ToString.PadRight(1, " ")
                    Cuenta = IIf(IsNothing(Me.TablaImportar.Item(Cta.Index, i).Value), "", Me.TablaImportar.Item(Cta.Index, i).Value).ToString.PadRight(16, " ")
                    Descripcion = IIf(IsNothing(Me.TablaImportar.Item(Des.Index, i).Value), "", Me.TablaImportar.Item(Des.Index, i).Value).ToString.PadRight(50, " ") '50

                    Fecha_Mov = IIf(IsNothing(Me.TablaImportar.Item(FM.Index, i).Value), "", Me.TablaImportar.Item(FM.Index, i).Value).ToString
                    Cuenta_Origen = IIf(IsNothing(Me.TablaImportar.Item(CtaO.Index, i).Value), "", Me.TablaImportar.Item(CtaO.Index, i).Value).ToString.PadRight(40, " ") '40 
                    Cuenta_destino = IIf(IsNothing(Me.TablaImportar.Item(CtaD.Index, i).Value), "", Me.TablaImportar.Item(CtaD.Index, i).Value).ToString.PadRight(40, " ") '40 
                    ' Importe = IIf(IsNothing(Me.TablaImportar.Item(Imp.Index, i).Value), "", String.Format(Globalization.CultureInfo.InvariantCulture, "{0:N2}", Me.TablaImportar.Item(Imp.Index, i).Value)).ToString.PadLeft(16, " ") '17
                    Importe = IIf(IsNothing(Me.TablaImportar.Item(Imp.Index, i).Value), 0, Math.Round(Convert.ToDecimal(Me.TablaImportar.Item(Imp.Index, i).Value), 2, MidpointRounding.ToEven))
                    Importe = Importe.ToString.PadLeft(16, " ") '17
                    P_Aplicar = IIf(IsNothing(Me.TablaImportar.Item(PA.Index, i).Value), "", Me.TablaImportar.Item(PA.Index, i).Value).ToString.PadRight(1, " ") '1
                    Imp_Reg_Simplificado = IIf(IsNothing(Me.TablaImportar.Item(isr.Index, i).Value), "0.00", Me.TablaImportar.Item(isr.Index, i).Value).ToString.PadLeft(4, " ") '4
                    Nom_Cuenta = IIf(IsNothing(Me.TablaImportar.Item(NCta.Index, i).Value), "", Me.TablaImportar.Item(NCta.Index, i).Value).ToString.PadRight(53, " ") '53
                    If Len(Nom_Cuenta) > 30 Then
                        Nom_Cuenta = Nom_Cuenta.ToString.Substring(0, 30).PadRight(53, " ")
                    End If
                    Importe_Factura = IIf(IsNothing(Me.TablaImportar.Item(ImpF.Index, i).Value), 0, Math.Round(Convert.ToDecimal(Me.TablaImportar.Item(ImpF.Index, i).Value), 2, MidpointRounding.ToEven))
                    Importe_Factura = Importe_Factura.ToString.PadLeft(16, " ") '17
                    Folio_Fiscal_f = IIf(IsNothing(Me.TablaImportar.Item(Ff.Index, i).Value), "", Me.TablaImportar.Item(Ff.Index, i).Value).ToString.PadLeft(36, " ") '36
                    Moneda = IIf(IsNothing(Me.TablaImportar.Item(Mone.Index, i).Value), "", Me.TablaImportar.Item(Mone.Index, i).Value).ToString.PadLeft(3, " ") '3
                    'Importe_cheque = IIf(IsNothing(Me.TablaImportar.Item(ImpCh.Index, i).Value), "", String.Format(Globalization.CultureInfo.InvariantCulture, "{0:N2}",Me.TablaImportar.Item(ImpCh.Index, i).Value)).ToString.PadLeft(16, " ") '17
                    Importe_cheque = IIf(IsNothing(Me.TablaImportar.Item(ImpCh.Index, i).Value), 0, Math.Round(Convert.ToDecimal(Me.TablaImportar.Item(ImpCh.Index, i).Value), 2, MidpointRounding.ToEven))
                    Importe_cheque = Importe_cheque.ToString.PadLeft(16, " ") '17


                    numero_comprobante = IIf(IsNothing(Me.TablaImportar.Item(NC.Index, i).Value), "", Me.TablaImportar.Item(NC.Index, i).Value).ToString.PadLeft(7, " ") '7
                    por_ret_IVA = IIf(IsNothing(Me.TablaImportar.Item(PRIVA.Index, i).Value), "0.00", Me.TablaImportar.Item(PRIVA.Index, i).Value).ToString.PadRight(6, " ") '4
                    por_ret_ISR = IIf(IsNothing(Me.TablaImportar.Item(PRISR.Index, i).Value), "0.00", Me.TablaImportar.Item(PRISR.Index, i).Value).ToString.PadRight(6, " ") '4
                    por_ret_x = IIf(IsNothing(Me.TablaImportar.Item(PRX.Index, i).Value), "0.00", Me.TablaImportar.Item(PRX.Index, i).Value).ToString.PadRight(8, " ") '4
                    por_ret_Y = IIf(IsNothing(Me.TablaImportar.Item(PRY.Index, i).Value), "0.00000", Me.TablaImportar.Item(PRY.Index, i).Value).ToString.PadRight(7, " ") '7
                    Tipo_Comprobante = IIf(IsNothing(Me.TablaImportar.Item(TC.Index, i).Value), "", Me.TablaImportar.Item(TC.Index, i).Value).ToString.PadLeft(1, " ") '1
                    N = IIf(IsNothing(Me.TablaImportar.Item(M.Index, i).Value), "", Me.TablaImportar.Item(M.Index, i).Value).ToString
                    parte = Format(Today.Date, "ddMMyyyy")
                    Dim cadena As String = ""


                    If Me.TablaImportar.Item(Tm.Index, i).Value = 0 Then 'Titulo
                        If i = 0 Then
                        Else
                            strStreamWriter.WriteLine("//")
                        End If

                        cadena = Anio & Mes & Tipo & Poliza & Dia & "                    " & Descripcion & Movimiento & Importe & " " & "0" & "                                                               " & Fecha_Mov & " 12:00:00 a.m.    0"

                    ElseIf Me.TablaImportar.Item(Tm.Index, i).Value = 1 Or Me.TablaImportar.Item(Tm.Index, i).Value = 2 Then 'cargo o abono
                        cadena = Anio & Mes & Tipo & Poliza & Dia & Cuenta & "    " & Descripcion & Movimiento & Importe & " " & "0" & "                             " & Imp_Reg_Simplificado & Nom_Cuenta & Today.ToShortDateString & " 12:00:00 a.m.                " & por_ret_IVA & por_ret_ISR & por_ret_x & por_ret_Y & " D                    N"

                    ElseIf Me.TablaImportar.Item(Tm.Index, i).Value = 3 Then ' contabilidad electronica

                        If Me.TablaImportar.Item(Dce.Index, i).Value = "H" Then 'cheque 
                            'cadena = Anio & Mes & Tipo & Poliza & "  H" & rfc_Ce & No_Cheque & No_Banco & "                                  3" & Cuenta_Origen & "                                                                                    " & Fecha_Mov & Importe_cheque & "                                                1.00000"
                            cadena = Anio & Mes & Tipo & Poliza & "  H" & rfc_Ce & No_Cheque & No_Banco & "                                 3" & Cuenta_Origen & "                                                                                    " & Fecha_Mov & Importe_cheque & "                                                1.00000"

                        ElseIf Me.TablaImportar.Item(Dce.Index, i).Value = "T" Then ' tranferencia
                            'cadena = Anio & Mes & Tipo & Poliza & "  T" & rfc_Ce & "                    " & No_Banco & "                                3" & Cuenta_Origen & "" & No_BancoD & "         " & Cuenta_destino & Fecha_Mov & Importe_Factura & "                                                1.00000"
                            cadena = Anio & Mes & Tipo & Poliza & "  T" & rfc_Ce & "                    " & No_Banco & "                                 3" & Cuenta_Origen & "" & No_BancoD & "       " & Cuenta_destino & Fecha_Mov & Importe_Factura & "                                                1.00000"

                        ElseIf Me.TablaImportar.Item(Dce.Index, i).Value = "P" Then ' efectivo
                            cadena = Anio & Mes & Tipo & Poliza & "  P" & rfc_Ce & "                                                        3                                                                                          " & Fecha_Mov & Importe_Factura & "                                                0.0000001"
                        ElseIf Me.TablaImportar.Item(Dce.Index, i).Value = "C" Then ' Factura
                            cadena = Anio & Mes & Tipo & Poliza & "  C" & rfc_Ce & "                                                        3                                                                                          " & Fecha_Mov & Importe_Factura & Folio_Fiscal_f & "            1.00000                                                                                                                 "
                        End If
                    ElseIf Me.TablaImportar.Item(Tm.Index, i).Value = "Pagos" Then ' Pagos
                        cadena = Anio & Mes & Tipo & Poliza & Dia & Cuenta & "    " & Descripcion & Movimiento & Importe & " " & "0" & "                 " & Imp_Reg_Simplificado & Nom_Cuenta & Today & "              " & por_ret_IVA & por_ret_ISR & por_ret_x & por_ret_Y & " D                    N"

                    End If
                    strStreamWriter.WriteLine(cadena)
                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                        Me.Barra.Minimum = 0
                        Me.Cursor = Cursors.Arrow
                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        RadMessageBox.Show("Proceso terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                        Me.Barra.Value1 = 0
                    Else
                        Me.Barra.Value1 += 1
                    End If
                Next
                strStreamWriter.Flush()
                strStreamWriter.Close() ' cerramos

            End If
        Catch ex As Exception
            MsgBox("Error al Guardar la informacion en el archivo. " & ex.ToString, MsgBoxStyle.Critical, Application.ProductName)
            strStreamWriter.Close() ' cerramos
        End Try
    End Sub

    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Rows.Clear()
        End If
        Dim Tipo As String = ""
        If Me.RadRecibidas.Checked = True Then
            'Tipo = " WHERE clave <>'002'  "
            Tipo = " WHERE Id_Tipo_poliza <> 2 "
        Else
            'Tipo = " WHERE clave ='002'  "
            Tipo = " WHERE Id_Tipo_poliza = 2  "
        End If

        If Me.RadFechas.Checked = True Then
            Buscar_Polizas_Periodo(Tipo)
        Else
            Dim sql As String = ""
            If Me.ComboMes.Text = "*" Then
                sql = " Polizas.id_anio = '" & Trim(Me.comboAño.Text) & "' "
            Else
                sql = " Polizas.id_anio = '" & Trim(Me.comboAño.Text) & "' and id_mes= '" & Trim(Me.ComboMes.Text) & "'"
            End If
            Buscar_Polizas_Mensual(sql, Me.lstCliente.SelectItem, Tipo)
        End If


        ' Unir()
    End Sub
    Private Sub Buscar_Polizas_Periodo(ByVal tipo As String)
        Dim posicion As Integer
        'Dim sql As String = "SELECT * from (SELECT Polizas.ID_anio, Polizas.ID_mes, Tipos_Poliza_Sat.Clave, Polizas.Num_Pol,Polizas.ID_dia,  Polizas.Fecha_captura, Polizas.Concepto, Polizas.ID_anio + + Polizas.ID_mes + + rtrim(Polizas.Num_Pol) + + Tipos_Poliza_Sat.clave as Poliza
        Dim sql As String = "select DISTINCT * from (SELECT Polizas.ID_anio, Polizas.ID_mes, Tipos_Poliza_Sat.Clave, Polizas.Num_Pol, Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave +  + rtrim(Polizas.Num_Pol)   as Poliza 
                ,Tipos_Poliza_Sat.Id_Tipo_poliza  From Polizas INNER Join Empresa On Polizas.Id_Empresa = Empresa.Id_Empresa INNER Join
                                    Tipos_Poliza_Sat On Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat Where Polizas.Fecha  >=  " & Eventos.Sql_hoy(Me.DT_fechai.Value.ToString.Substring(0, 10)) & " And Polizas.Fecha  <= " & Eventos.Sql_hoy(Me.DT_fechaf.Value.ToString.Substring(0, 10)) & " And (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ")) As tabla  " & tipo & " order by Poliza"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count

            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.TablaImportar.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                'posicion = posicion
                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(0))
                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(1))
                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(2))
                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(3))
                Dim dscompleto As DataSet = Eventos.Obtener_DS("SELECT TOP 1 * from (SELECT Polizas.ID_dia,  Polizas.Fecha , Polizas.Concepto, Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave +  + rtrim(Polizas.Num_Pol)  as Poliza
                                     FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat 
                                    WHERE   Polizas.Fecha >= " & Eventos.Sql_hoy(Me.DT_fechai.Value.ToString.Substring(0, 10)) & " 
                            AND Polizas.Fecha <= " & Eventos.Sql_hoy(Me.DT_fechaf.Value.ToString.Substring(0, 10)) & " and  (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ") AND  Num_Pol = " & Me.TablaImportar.Item(Np.Index, posicion).Value & " AND clave= '" & Trim(ds.Tables(0).Rows(i)(2)) & "') as Tabla ")
                Me.TablaImportar.Item(Dp.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(5))
                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(dscompleto.Tables(0).Rows(0)(1))
                Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(dscompleto.Tables(0).Rows(0)(2))
                Me.TablaImportar.Item(Tm.Index, posicion).Value = 0
                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(4))

                ' Se Buscan los cargos
                ' Polizas.ID_anio + + Polizas.ID_mes + + Polizas.ID_dia + + rtrim(Polizas.Num_Pol) + + Tipos_Poliza_Sat.clave as Poliza,polizas.id_poliza
                Dim Cargos As String = "Select * FROM (
                                        SELECT Polizas.ID_anio, Polizas.ID_mes, Tipos_Poliza_Sat.Clave, Polizas.Num_Pol,Polizas.ID_dia, Detalle_Polizas.Cuenta,  '1'   AS Movimiento,Detalle_Polizas.Cargo AS Importe,
                                           rtrim(Catalogo_de_Cuentas.Descripcion ) AS Nom_Cuenta,
                                        Polizas.Concepto, Polizas.Fecha , 
                                      
                                        Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave +  + rtrim(Polizas.Num_Pol)  as Poliza,polizas.id_poliza,Detalle_Polizas.id_item
                                        FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                        INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                        WHERE    (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ") AND detalle_polizas.Cargo <>0) AS tabla 
                                        WHERE Poliza ='" & Trim(Me.TablaImportar.Item(PolC.Index, posicion).Value) & "' ORDER BY  ID_poliza , id_item"
                Dim car As DataSet = Eventos.Obtener_DS(Cargos)
                If car.Tables(0).Rows.Count > 0 Then

                    Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + car.Tables(0).Rows.Count
                    For j As Integer = 0 To car.Tables(0).Rows.Count - 1
                        posicion = posicion + 1
                        Me.TablaImportar.Item(An.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(0)) 'año
                        Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(1)) 'mes
                        Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(2)) 'clave
                        Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(3)) 'no_poliza
                        Me.TablaImportar.Item(Dp.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(4)) 'dia
                        Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(5)) 'cuenta
                        Me.TablaImportar.Item(Tm.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(6)) ' movimiento1
                        Me.TablaImportar.Item(Imp.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(7)) 'importe
                        Me.TablaImportar.Item(NCta.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(8)) 'nombre_cuenta
                        Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(9)) 'concepto
                        Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(11))

                        'Buscar facturas
                        Dim Facturas As String = " SELECT Facturas.ID_anio, Facturas.ID_mes,  Polizas.Num_Pol,Tipos_Poliza_Sat.Clave,Polizas.ID_dia, Facturas.RFC_Emisor, Facturas.Fecha_Comprobante, 
                                                    Facturas.Importe, Facturas.Folio_Fiscal,Polizas.id_poliza 
                                                    FROM     Facturas INNER JOIN   Polizas ON Facturas.ID_poliza = Polizas.ID_poliza
                                                    INNER JOIN Tipos_Poliza_Sat ON Tipos_Poliza_Sat.Id_Tipo_Pol_Sat = Polizas.Id_Tipo_Pol_Sat 
                                                    WHERE   (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(car.Tables(0).Rows(j)(12)) & "'  "
                        Dim Fact As DataSet = Eventos.Obtener_DS(Facturas)
                        If Fact.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Fact.Tables(0).Rows.Count
                            For H As Integer = 0 To Fact.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("ID_anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("ID_mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Clave")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "C"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("RFC_Emisor"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Fecha_Comprobante"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Importe"))
                                Me.TablaImportar.Item(Ff.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Folio_Fiscal"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next

                        End If
                        'Buscar Contabilidad Electronica
                        'Efectivo
                        Dim Efectivo As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce, 
                                                    Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.ID_poliza = 
                                                    Conta_E_Sistema.ID_poliza
                                                    WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(car.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'P'"
                        Dim Efect As DataSet = Eventos.Obtener_DS(Efectivo)
                        If Efect.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Efect.Tables(0).Rows.Count
                            For H As Integer = 0 To Fact.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("Tipo")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "P"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("RFC_Ce"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("Fecha_Mov"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("Importe"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next
                        End If
                        'Transferencia
                        Dim Transferencia As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce,Conta_E_Sistema.No_banco,  Conta_E_Sistema.Cuenta_Origen ,  
                                                        Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe,Conta_E_Sistema.Banco_Destino , Conta_E_Sistema.Cuenta_Destino FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.ID_poliza = 
                                                        Conta_E_Sistema.ID_poliza
                                                        WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(car.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'T'"
                        Dim trans As DataSet = Eventos.Obtener_DS(Transferencia)
                        If trans.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + trans.Tables(0).Rows.Count
                            For E As Integer = 0 To trans.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Tipo")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "T"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("RFC_Ce"))
                                Me.TablaImportar.Item(Nb.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("No_banco"))
                                Me.TablaImportar.Item(CtaO.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Cuenta_Origen"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Fecha_Mov"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Importe"))
                                Me.TablaImportar.Item(BD.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Banco_Destino"))
                                Me.TablaImportar.Item(CtaD.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Cuenta_Destino"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next
                        End If
                        'Cheque
                        Dim Cheque As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce,Conta_E_Sistema.No_Cheque,Conta_E_Sistema.No_banco,  Conta_E_Sistema.Cuenta_Origen ,  
                                                        Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.ID_poliza = 
                                                        Conta_E_Sistema.ID_poliza
                                                        WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(car.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'H'"
                        Dim Chec As DataSet = Eventos.Obtener_DS(Cheque)
                        If Chec.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Chec.Tables(0).Rows.Count
                            For E As Integer = 0 To Chec.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Tipo")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "H"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("RFC_Ce"))
                                Me.TablaImportar.Item(Nb.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("No_Cheque"))
                                Me.TablaImportar.Item(Nb.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("No_banco"))
                                Me.TablaImportar.Item(CtaO.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Cuenta_Origen"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Fecha_Mov"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Importe"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next
                        End If

                    Next

                End If
                ' Se cargan los Abonos
                Dim Abonos As String = "SELECT * FROM (
                                        SELECT Polizas.ID_anio, Polizas.ID_mes, Tipos_Poliza_Sat.Clave, Polizas.Num_Pol,Polizas.ID_dia, Detalle_Polizas.Cuenta,  '2'   AS Movimiento,Detalle_Polizas.abono AS Importe,
                                           rtrim(Catalogo_de_Cuentas.Descripcion ) AS Nom_Cuenta,
                                        Polizas.Concepto, Polizas.Fecha , 
                                      Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave +  + rtrim(Polizas.Num_Pol)  as Poliza,polizas.id_poliza,Detalle_Polizas.id_item
                                        FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                        INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                        WHERE    (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ") AND detalle_polizas.abono  <> 0) AS tabla 
                                        WHERE Poliza ='" & Trim(Me.TablaImportar.Item(PolC.Index, posicion - 1).Value) & "'  ORDER BY  ID_poliza , id_item"
                Dim Ab As DataSet = Eventos.Obtener_DS(Abonos)
                If Ab.Tables(0).Rows.Count > 0 Then
                    Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ab.Tables(0).Rows.Count
                    For j As Integer = 0 To Ab.Tables(0).Rows.Count - 1
                        posicion = posicion + 1
                        Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(0)) 'año
                        Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(1)) 'mes
                        Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(2)) 'clave
                        Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(3)) 'no_poliza
                        Me.TablaImportar.Item(Dp.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(4)) 'dia
                        Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(5)) 'cuenta
                        Me.TablaImportar.Item(Tm.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(6)) ' movimiento1
                        Me.TablaImportar.Item(Imp.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(7)) 'importe
                        Me.TablaImportar.Item(NCta.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(8)) 'nombre_cuenta
                        Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(9)) 'concepto
                        Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(11)) 'Poliza Calculada

                        'Buscar facturas
                        Dim Facturas As String = " SELECT Facturas.ID_anio, Facturas.ID_mes,  Polizas.Num_Pol,Tipos_Poliza_Sat.Clave,Polizas.ID_dia, Facturas.RFC_Emisor, Facturas.Fecha_Comprobante, 
                                                    Facturas.Importe, Facturas.Folio_Fiscal,Polizas.id_poliza 
                                                    FROM     Facturas INNER JOIN   Polizas ON Facturas.ID_poliza = Polizas.ID_poliza
                                                    INNER JOIN Tipos_Poliza_Sat ON Tipos_Poliza_Sat.Id_Tipo_Pol_Sat = Polizas.Id_Tipo_Pol_Sat 
                                                    WHERE   (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(Ab.Tables(0).Rows(j)(12)) & "' "
                        Dim Fact As DataSet = Eventos.Obtener_DS(Facturas)
                        If Fact.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1 'incremento la posicion para la factura del abono
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Fact.Tables(0).Rows.Count
                            For H As Integer = 0 To Fact.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("ID_anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("ID_mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Clave")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "C"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("RFC_Emisor"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Fecha_Comprobante"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Importe"))
                                Me.TablaImportar.Item(Ff.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Folio_Fiscal"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next

                        End If
                        'Buscar Contabilidad Electronica
                        'Efectivo
                        Dim Efectivo As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce, 
                                                    Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.ID_poliza = 
                                                    Conta_E_Sistema.ID_poliza
                                                    WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(Ab.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'P'"
                        Dim Efect As DataSet = Eventos.Obtener_DS(Efectivo)
                        If Efect.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Efect.Tables(0).Rows.Count
                            For E As Integer = 0 To Efect.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("Tipo")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "P"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("RFC_Ce"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("Fecha_Mov"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("Importe"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next
                        End If
                        'Transferencia
                        Dim Transferencia As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce,Conta_E_Sistema.No_banco,  Conta_E_Sistema.Cuenta_Origen ,  
                                                        Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe, Conta_E_Sistema.Banco_Destino , Conta_E_Sistema.Cuenta_Destino  FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.ID_poliza = 
                                                        Conta_E_Sistema.ID_poliza
                                                        WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(Ab.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'T'"
                        Dim trans As DataSet = Eventos.Obtener_DS(Transferencia)
                        If trans.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + trans.Tables(0).Rows.Count
                            For E As Integer = 0 To trans.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Tipo")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "T"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("RFC_Ce"))
                                Me.TablaImportar.Item(Nb.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("No_banco"))
                                Me.TablaImportar.Item(CtaO.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Cuenta_Origen"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Fecha_Mov"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Importe"))
                                Me.TablaImportar.Item(BD.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Banco_Destino"))
                                Me.TablaImportar.Item(CtaD.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Cuenta_Destino"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next
                        End If
                        'Cheque
                        Dim Cheque As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce,Conta_E_Sistema.No_Cheque,Conta_E_Sistema.No_banco,  Conta_E_Sistema.Cuenta_Origen ,  
                                                        Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.ID_poliza = 
                                                        Conta_E_Sistema.ID_poliza
                                                        WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(Ab.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'H'"
                        Dim Chec As DataSet = Eventos.Obtener_DS(Cheque)
                        If Chec.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Chec.Tables(0).Rows.Count
                            For E As Integer = 0 To Chec.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Tipo")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "H"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("RFC_Ce"))
                                Me.TablaImportar.Item(Nb.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("No_Cheque"))
                                Me.TablaImportar.Item(Nb.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("No_banco"))
                                Me.TablaImportar.Item(CtaO.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Cuenta_Origen"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Fecha_Mov"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Importe"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next
                        End If

                    Next
                End If
                posicion = posicion + 1
                frm.Barra.Value = i
            Next
            Colorear()

            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        Else

        End If
        Quitablancos()
    End Sub
    Private Sub Buscar_Polizas_Mensual(ByVal consulta As String, ByVal cliente As Integer, ByVal tipo As String)
        Dim posicion As Integer
        '
        Dim sql As String = "SELECT DISTINCT * from (SELECT Polizas.ID_anio, Polizas.ID_mes, Tipos_Poliza_Sat.Clave, Polizas.Num_Pol,Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + rtrim(Polizas.Num_Pol)   as Poliza 
                                ,Tipos_Poliza_Sat.Id_Tipo_poliza  From     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa INNER JOIN
                                    Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat WHERE " & consulta & " and  (Empresa.Id_Empresa = " & cliente & ")) as Tabla  " & tipo & " order by Poliza"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count

            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.TablaImportar.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                'posicion = posicion
                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(0))
                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(1))
                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(2))
                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(3))
                Dim dscompleto As DataSet = Eventos.Obtener_DS("SELECT TOP 1 * from (SELECT Polizas.ID_dia,  Polizas.Fecha , Polizas.Concepto, Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + rtrim(Polizas.Num_Pol) as Poliza
                                     FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat WHERE   " & consulta & " and  (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ") AND Num_Pol = " & Me.TablaImportar.Item(Np.Index, posicion).Value & " AND clave= '" & Trim(ds.Tables(0).Rows(i)(2)) & "') as Tabla ")
                Me.TablaImportar.Item(Dp.Index, posicion).Value = Trim(dscompleto.Tables(0).Rows(0)(0))
                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(dscompleto.Tables(0).Rows(0)(1))
                Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(dscompleto.Tables(0).Rows(0)(2))
                Me.TablaImportar.Item(Tm.Index, posicion).Value = 0
                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(ds.Tables(0).Rows(i)(4))

                ' Se Buscan los cargos Polizas.Concepto
                Dim Cargos As String = "SELECT * FROM (
                                        SELECT Polizas.ID_anio, Polizas.ID_mes, Tipos_Poliza_Sat.Clave, Polizas.Num_Pol,Polizas.ID_dia, Detalle_Polizas.Cuenta,  '1'   AS Movimiento,Detalle_Polizas.Cargo AS Importe,
                                           rtrim(Catalogo_de_Cuentas.Descripcion ) AS Nom_Cuenta,
                                        Detalle_Polizas.Descripcion, Polizas.Fecha , 
                                  Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + rtrim(Polizas.Num_Pol) as Poliza,polizas.id_poliza,Detalle_Polizas.id_item
                                        FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                        INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                        WHERE    (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ") AND detalle_polizas.Cargo <>0) AS tabla 
                                        WHERE Poliza ='" & Trim(Me.TablaImportar.Item(PolC.Index, posicion).Value) & "' ORDER BY  ID_poliza , id_item"
                Dim car As DataSet = Eventos.Obtener_DS(Cargos)
                If car.Tables(0).Rows.Count > 0 Then

                    Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + car.Tables(0).Rows.Count
                    For j As Integer = 0 To car.Tables(0).Rows.Count - 1
                        posicion = posicion + 1
                        Me.TablaImportar.Item(An.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(0)) 'año
                        Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(1)) 'mes
                        Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(2)) 'clave
                        Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(3)) 'no_poliza
                        Me.TablaImportar.Item(Dp.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(4)) 'dia
                        Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(5)) 'cuenta
                        Me.TablaImportar.Item(Tm.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(6)) ' movimiento1
                        Me.TablaImportar.Item(Imp.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(7)) 'importe
                        Me.TablaImportar.Item(NCta.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(8)) 'nombre_cuenta
                        Me.TablaImportar.Item(Des.Index, posicion).Value = IIf(IsDBNull(car.Tables(0).Rows(j)(9).ToString()), "", Trim(car.Tables(0).Rows(j)(9).ToString())) 'concepto
                        Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(11))

                        'Buscar facturas
                        Dim Facturas As String = " SELECT Facturas.ID_anio, Facturas.ID_mes,  Polizas.Num_Pol,Tipos_Poliza_Sat.Clave,Polizas.ID_dia, Facturas.RFC_Emisor, Facturas.Fecha_Comprobante, 
                                                    Facturas.Importe, Facturas.Folio_Fiscal,Polizas.id_poliza 
                                                    FROM     Facturas INNER JOIN   Polizas ON Facturas.ID_poliza = Polizas.ID_poliza
                                                    INNER JOIN Tipos_Poliza_Sat ON Tipos_Poliza_Sat.Id_Tipo_Pol_Sat = Polizas.Id_Tipo_Pol_Sat 
                                                    WHERE   (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(car.Tables(0).Rows(j)(12)) & "'  "
                        Dim Fact As DataSet = Eventos.Obtener_DS(Facturas)
                        If Fact.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Fact.Tables(0).Rows.Count
                            For H As Integer = 0 To Fact.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("ID_anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("ID_mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Clave")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "C"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("RFC_Emisor"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Fecha_Comprobante"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Importe"))
                                Me.TablaImportar.Item(Ff.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Folio_Fiscal"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next

                        End If
                        'Buscar Contabilidad Electronica
                        'Efectivo
                        Dim Efectivo As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce, 
                                                    Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.ID_poliza = 
                                                    Conta_E_Sistema.ID_poliza
                                                    WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(car.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'P'"
                        Dim Efect As DataSet = Eventos.Obtener_DS(Efectivo)
                        If Efect.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Efect.Tables(0).Rows.Count
                            For H As Integer = 0 To Fact.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("Tipo")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "P"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("RFC_Ce"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("Fecha_Mov"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(Efect.Tables(0).Rows(H)("Importe"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next
                        End If
                        'Transferencia
                        Dim Transferencia As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce,Conta_E_Sistema.No_banco,  Conta_E_Sistema.Cuenta_Origen ,  
                                                        Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe , Conta_E_Sistema.Banco_Destino , Conta_E_Sistema.Cuenta_Destino FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.ID_poliza = 
                                                        Conta_E_Sistema.ID_poliza
                                                        WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(car.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'T'"
                        Dim trans As DataSet = Eventos.Obtener_DS(Transferencia)
                        If trans.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + trans.Tables(0).Rows.Count
                            For E As Integer = 0 To trans.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Tipo")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "T"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("RFC_Ce"))
                                Me.TablaImportar.Item(Nb.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("No_banco"))
                                Me.TablaImportar.Item(CtaO.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Cuenta_Origen"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Fecha_Mov"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Importe"))
                                Me.TablaImportar.Item(BD.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Banco_Destino"))
                                Me.TablaImportar.Item(CtaD.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Cuenta_Destino"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next
                        End If
                        'Cheque
                        Dim Cheque As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce,Conta_E_Sistema.No_Cheque,Conta_E_Sistema.No_banco,  Conta_E_Sistema.Cuenta_Origen ,  
                                                        Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.ID_poliza = 
                                                        Conta_E_Sistema.ID_poliza
                                                        WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(car.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'H'"
                        Dim Chec As DataSet = Eventos.Obtener_DS(Cheque)
                        If Chec.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Chec.Tables(0).Rows.Count
                            For E As Integer = 0 To Chec.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Tipo")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "H"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("RFC_Ce"))
                                Me.TablaImportar.Item(Nb.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("No_Cheque"))
                                Me.TablaImportar.Item(Nb.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("No_banco"))
                                Me.TablaImportar.Item(CtaO.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Cuenta_Origen"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Fecha_Mov"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Importe"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(car.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next
                        End If

                    Next

                End If
                ' Se cargan los Abonos Polizas.Concepto
                Dim Abonos As String = "SELECT * FROM (
                                        SELECT Polizas.ID_anio, Polizas.ID_mes, Tipos_Poliza_Sat.Clave, Polizas.Num_Pol,Polizas.ID_dia, Detalle_Polizas.Cuenta,  '2'   AS Movimiento,Detalle_Polizas.abono AS Importe,
                                           rtrim(Catalogo_de_Cuentas.Descripcion ) AS Nom_Cuenta,
                                        Detalle_Polizas.Descripcion , Polizas.Fecha , 
                                       Polizas.ID_anio + + Polizas.ID_mes + + Tipos_Poliza_Sat.clave + + rtrim(Polizas.Num_Pol) as Poliza,polizas.id_poliza,Detalle_Polizas.id_item
                                        FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                        INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                        WHERE    (Empresa.Id_Empresa = " & Me.lstCliente.SelectItem & ") AND detalle_polizas.abono  <> 0) AS tabla 
                                        WHERE Poliza ='" & Trim(Me.TablaImportar.Item(PolC.Index, posicion - 1).Value) & "' ORDER BY  ID_poliza , id_item"
                Dim Ab As DataSet = Eventos.Obtener_DS(Abonos)
                If Ab.Tables(0).Rows.Count > 0 Then
                    Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ab.Tables(0).Rows.Count
                    For j As Integer = 0 To Ab.Tables(0).Rows.Count - 1
                        posicion = posicion + 1
                        Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(0)) 'año
                        Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(1)) 'mes
                        Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(2)) 'clave
                        Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(3)) 'no_poliza
                        Me.TablaImportar.Item(Dp.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(4)) 'dia
                        Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(5)) 'cuenta
                        Me.TablaImportar.Item(Tm.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(6)) ' movimiento1
                        Me.TablaImportar.Item(Imp.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(7)) 'importe
                        Me.TablaImportar.Item(NCta.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(8)) 'nombre_cuenta
                        Me.TablaImportar.Item(Des.Index, posicion).Value = IIf(IsDBNull(Ab.Tables(0).Rows(j)(9).ToString()), "", Trim(Ab.Tables(0).Rows(j)(9).ToString()))
                        Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(11)) 'Poliza Calculada

                        'Buscar facturas
                        Dim Facturas As String = " SELECT Facturas.ID_anio, Facturas.ID_mes,  Polizas.Num_Pol,Tipos_Poliza_Sat.Clave,Polizas.ID_dia, Facturas.RFC_Emisor, Facturas.Fecha_Comprobante, 
                                                    Facturas.Importe, Facturas.Folio_Fiscal,Polizas.id_poliza 
                                                    FROM     Facturas INNER JOIN   Polizas ON Facturas.ID_poliza = Polizas.ID_poliza
                                                    INNER JOIN Tipos_Poliza_Sat ON Tipos_Poliza_Sat.Id_Tipo_Pol_Sat = Polizas.Id_Tipo_Pol_Sat 
                                                    WHERE   (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(Ab.Tables(0).Rows(j)(12)) & "' "
                        Dim Fact As DataSet = Eventos.Obtener_DS(Facturas)
                        If Fact.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1 'incremento la posicion para la factura del abono
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Fact.Tables(0).Rows.Count
                            For H As Integer = 0 To Fact.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("ID_anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("ID_mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Clave")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "C"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("RFC_Emisor"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Fecha_Comprobante"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Importe"))
                                Me.TablaImportar.Item(Ff.Index, posicion).Value = Trim(Fact.Tables(0).Rows(H)("Folio_Fiscal"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next

                        End If
                        'Buscar Contabilidad Electronica
                        'Efectivo
                        Dim Efectivo As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce, 
                                                    Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.ID_poliza = 
                                                    Conta_E_Sistema.ID_poliza
                                                    WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(Ab.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'P'"
                        Dim Efect As DataSet = Eventos.Obtener_DS(Efectivo)
                        If Efect.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Efect.Tables(0).Rows.Count
                            For E As Integer = 0 To Efect.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("Tipo")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "P"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("RFC_Ce"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("Fecha_Mov"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(Efect.Tables(0).Rows(E)("Importe"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next
                        End If
                        'Transferencia
                        Dim Transferencia As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce,Conta_E_Sistema.No_banco,  Conta_E_Sistema.Cuenta_Origen ,  
                                                        Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe,Conta_E_Sistema.Banco_Destino , Conta_E_Sistema.Cuenta_Destino FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.ID_poliza = 
                                                        Conta_E_Sistema.ID_poliza
                                                        WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(Ab.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'T'"
                        Dim trans As DataSet = Eventos.Obtener_DS(Transferencia)
                        If trans.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + trans.Tables(0).Rows.Count
                            For E As Integer = 0 To trans.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Tipo")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "T"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("RFC_Ce"))
                                Me.TablaImportar.Item(Nb.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("No_banco"))
                                Me.TablaImportar.Item(CtaO.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Cuenta_Origen"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Fecha_Mov"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Importe"))
                                Me.TablaImportar.Item(BD.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Banco_Destino"))
                                Me.TablaImportar.Item(CtaD.Index, posicion).Value = Trim(trans.Tables(0).Rows(E)("Cuenta_Destino"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next
                        End If
                        'Cheque
                        Dim Cheque As String = " SELECT Conta_E_Sistema.Anio, Conta_E_Sistema.Mes, Conta_E_Sistema.Tipo, Polizas.Num_Pol, Conta_E_Sistema.RFC_Ce,Conta_E_Sistema.No_Cheque,Conta_E_Sistema.No_banco,  Conta_E_Sistema.Cuenta_Origen ,  
                                                        Conta_E_Sistema.Fecha_Mov, Conta_E_Sistema.Importe FROM     Polizas INNER JOIN  Conta_E_Sistema ON Polizas.ID_poliza = 
                                                        Conta_E_Sistema.ID_poliza
                                                        WHERE (Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & ") and Polizas.ID_poliza = '" & Trim(Ab.Tables(0).Rows(j)(12)) & "' AND Conta_E_Sistema.Tipo_CE = 'H'"
                        Dim Chec As DataSet = Eventos.Obtener_DS(Cheque)
                        If Chec.Tables(0).Rows.Count > 0 Then
                            posicion = posicion + 1  'incremento la posicion para la factura del cargo
                            Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Chec.Tables(0).Rows.Count
                            For E As Integer = 0 To Chec.Tables(0).Rows.Count - 1
                                Me.TablaImportar.Item(An.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("anio")) 'año
                                Me.TablaImportar.Item(MS.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("mes")) 'mes
                                Me.TablaImportar.Item(Tpol.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Tipo")) 'clave
                                Me.TablaImportar.Item(Np.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Num_Pol")) 'no_poliza
                                Me.TablaImportar.Item(Dce.Index, posicion).Value = "H"
                                Me.TablaImportar.Item(RFCce.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("RFC_Ce"))
                                Me.TablaImportar.Item(Nb.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("No_Cheque"))
                                Me.TablaImportar.Item(Nb.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("No_banco"))
                                Me.TablaImportar.Item(CtaO.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Cuenta_Origen"))
                                Me.TablaImportar.Item(FM.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Fecha_Mov"))
                                Me.TablaImportar.Item(ImpF.Index, posicion).Value = Trim(Chec.Tables(0).Rows(E)("Importe"))
                                Me.TablaImportar.Item(Tm.Index, posicion).Value = 3
                                Me.TablaImportar.Item(PolC.Index, posicion).Value = Trim(Ab.Tables(0).Rows(j)(11)) 'Poliza Calculada
                            Next
                        End If

                    Next
                End If
                posicion = posicion + 1
                frm.Barra.Value = i
            Next
            Colorear()

            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        Else

        End If
        Quitablancos()
    End Sub


    Private Sub Colorear()
        Dim contador As Integer = 0
        Dim total As Decimal = 0
        For Each Fila As DataGridViewRow In TablaImportar.Rows
            If Fila.Cells(Tm.Index).Value = 0 And Fila.Cells(Tm.Index).Value IsNot Nothing Then
                Fila.DefaultCellStyle.BackColor = Color.PaleGreen
                contador = contador + 1
            End If
        Next



        Me.lblRegistros.Text = "Total de Registros: " & contador
        For Each Fila As DataGridViewRow In TablaImportar.Rows
            If Fila.DefaultCellStyle.BackColor = Color.PaleGreen Then
                Dim poliza As String = Fila.Cells(PolC.Index).Value

                For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                    If Me.TablaImportar.Item(PolC.Index, i).Value = poliza And Me.TablaImportar.Item(Tm.Index, i).Value = "1" Then
                        total = total + Me.TablaImportar.Item(Imp.Index, i).Value
                    End If
                Next
                Fila.Cells(Imp.Index).Value = total
                total = 0
            End If
        Next
    End Sub
    Private Sub Quitablancos()
        Dim filas As Integer = Me.TablaImportar.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaImportar.RowCount - 1
                If Me.TablaImportar.Item(Tm.Index, i).Value Is Nothing Then
                    If i < Me.TablaImportar.RowCount - 1 Then
                        Me.TablaImportar.Rows.RemoveAt(i)
                        Exit For
                    End If
                End If
            Next
        Next

    End Sub

    Private Sub CmdAbrir_Click(sender As Object, e As EventArgs) Handles CmdAbrir.Click
        Eventos.Abrir_Capeta(Application.StartupPath & "\Polizas")
    End Sub
    Private Sub UnirBancos(ByVal poliza As String)
        Dim filas As Integer = Me.TablaImportar.RowCount - 1
        Dim suma As Decimal = 0
        Dim Posiciones() As Integer
        Dim Tam As Integer = 0


        For i As Integer = 0 To Me.TablaImportar.RowCount - 1
            If Me.TablaImportar.Item(Cta.Index, i).Value <> Nothing Then
                If Me.TablaImportar.Item(Cta.Index, i).Value.ToString.Substring(0, 4) Like "1020*" And Me.TablaImportar.Item(PolC.Index, i).Value = poliza Then
                    ReDim Preserve Posiciones(Tam)
                    Posiciones(Tam) = i
                    suma = suma + Me.TablaImportar.Item(Imp.Index, i).Value
                    Tam = Tam + 1
                End If
            End If
        Next
        If Posiciones IsNot Nothing Then


            For Each item In Posiciones
                Me.TablaImportar.Item(Imp.Index, item).Value = 0
            Next
            Me.TablaImportar.Item(Imp.Index, Posiciones(0)).Value = suma
            For i As Integer = 0 To Me.TablaImportar.RowCount - 1
                If Me.TablaImportar.Item(Dce.Index, i).Value = "T" And Me.TablaImportar.Item(PolC.Index, i).Value = poliza Then
                    Me.TablaImportar.Item(ImpF.Index, i).Value = suma
                End If
            Next
            For i As Integer = 0 To Me.TablaImportar.RowCount - 1
                If Me.TablaImportar.Item(Dce.Index, i).Value = "H" And Me.TablaImportar.Item(PolC.Index, i).Value = poliza Then
                    Me.TablaImportar.Item(ImpF.Index, i).Value = suma
                End If
            Next
        End If
    End Sub

    Private Sub CmdUnir_Click(sender As Object, e As EventArgs) Handles CmdUnir.Click

        'Dim Tipo As String = ""
        'If Me.RadRecibidas.Checked = True Then
        '    'Tipo = " WHERE clave <>'002'  "
        '    Tipo = " WHERE Id_Tipo_poliza <> 2 "
        'Else
        '    'Tipo = " WHERE clave ='002'  "
        '    Tipo = " WHERE Id_Tipo_poliza = 2  "
        'End If
        'If RadSistema.Checked = True Then
        '    'poner codigo sistema
        '    If Me.RadFechas.Checked = True Then
        '        Buscar_Polizas_PeriodoS(Tipo)
        '    Else
        '        Dim sql As String = ""
        '        If Me.ComboMes.Text = "*" Then
        '            sql = " Polizas_Sistema.id_anio = '" & Trim(Me.comboAño.Text) & "' "
        '        Else
        '            sql = " Polizas_Sistema.id_anio = '" & Trim(Me.comboAño.Text) & "' and Polizas_Sistema.id_mes= '" & Trim(Me.ComboMes.Text) & "'"
        '        End If
        '        Buscar_Polizas_MensualS(sql, Me.lstCliente.SelectItem, Tipo)
        '    End If
        'Else
        '    If Me.RadFechas.Checked = True Then
        '        Buscar_Polizas_Periodo(Tipo)
        '    Else
        '        Dim sql As String = ""
        '        If Me.ComboMes.Text = "*" Then
        '            sql = " Polizas.id_anio = '" & Trim(Me.comboAño.Text) & "' "
        '        Else
        '            sql = " Polizas.id_anio = '" & Trim(Me.comboAño.Text) & "' and id_mes= '" & Trim(Me.ComboMes.Text) & "'"
        '        End If
        '        Buscar_Polizas_Mensual(sql, Me.lstCliente.SelectItem, Tipo)
        '    End If

        'End If
        Unir()
    End Sub

    Private Sub Unir()
        Dim filas As Integer = Me.TablaImportar.RowCount - 1
        Dim valor As String = ""
        Dim descrip As String = ""
        Dim contador As Integer = 0
        For Each Fila As DataGridViewRow In TablaImportar.Rows
            If Fila.Cells(Tm.Index).Value = 0 Then
                If Fila.DefaultCellStyle.BackColor = Color.PaleGreen Then
                    valor = Fila.Cells(PolC.Index).Value
                    descrip = Fila.Cells(Des.Index).Value


                    For i As Integer = 0 To Me.TablaImportar.RowCount - 1

                        If Me.TablaImportar.Item(PolC.Index, i).Value = valor Then

                            For P As Integer = 0 To Me.TablaImportar.RowCount - 1
                                If Me.TablaImportar.Item(Des.Index, P).Value <> descrip Then
                                    REM Me.TablaImportar.Item(Cta.Index, P).Value.ToString.Substring(0, 4
                                    If Me.TablaImportar.Item(Cta.Index, P).Value <> Nothing Then
                                        If Me.TablaImportar.Item(Cta.Index, P).Value.ToString.Substring(0, 4) Like "1020*" Then
                                            contador = contador + 1

                                        End If
                                    End If

                                End If
                            Next
                            If contador > 1 Then
                                UnirBancos(valor)
                                Exit For
                            End If

                            'If i < Me.TablaImportar.RowCount - 1 Then
                            '    Me.TablaImportar.Rows.RemoveAt(i)
                            '    Exit For
                            'End If
                        End If
                    Next



                End If



            End If
        Next
    End Sub


End Class