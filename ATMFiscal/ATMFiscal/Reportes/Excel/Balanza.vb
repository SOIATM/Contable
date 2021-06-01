Imports CrystalDecisions.Shared
Imports System.ComponentModel
Imports System.IO
Imports Telerik.WinControls
Public Class Balanza
    Dim activo As Boolean
    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Dim Fila As Integer = 0
    Private Sub Balanza_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        activo = True
        Cargar_clientes()
        Eventos.DiseñoTablaEnca(Tabla)
        Eventos.DiseñoTabla(TablaAbonos)
        Eventos.DiseñoTabla(TablaAbril)
        Eventos.DiseñoTabla(TablaAgosto)
        Eventos.DiseñoTabla(TablaAnual)
        Eventos.DiseñoTabla(TablaCargos)
        Eventos.DiseñoTabla(TablaDiciembre)
        Eventos.DiseñoTabla(Tablaenero)
        Eventos.DiseñoTabla(TablaFebrero)
        Eventos.DiseñoTabla(TablaMarzo)
        Eventos.DiseñoTabla(TablaMayo)
        Eventos.DiseñoTabla(TablaJunio)
        Eventos.DiseñoTabla(TablaJulio)
        Eventos.DiseñoTabla(TablaSeptiembre)
        Eventos.DiseñoTabla(TablaOctubre)
        Eventos.DiseñoTabla(TablaNoviembre)
        Eventos.DiseñoTabla(TablaMensual)

        activo = False
    End Sub
    Private Sub Cargar_clientes()
        Me.LstCliente.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social FROM     Empresa  ")
        Me.LstCliente.SelectItem = 1
        Me.LstNiveles.Cargar("SELECT 1, 'Nivel1' UNION SELECT 2, 'Nivel2' UNION SELECT 3, 'Nivel3' UNION SELECT 4, 'Nivel4' ")
        Me.LstNiveles.Combo.SelectedIndex = 0

        Me.Lstclientes2.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social FROM     Empresa  ")
        Me.Lstclientes2.SelectItem = 1

        Me.lstNivel2.Cargar("SELECT 1, 'Nivel1' UNION SELECT 2, 'Nivel2' UNION SELECT 3, 'Nivel3' UNION SELECT 4, 'Nivel4' ")
        Me.lstNivel2.Combo.SelectedIndex = 0

        Me.lstClientesMasivos.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social FROM     Empresa  ")
        Me.lstClientesMasivos.SelectItem = 1

        Me.LstNivelBalanza.Cargar("SELECT 1, 'Nivel1' UNION SELECT 2, 'Nivel2' UNION SELECT 3, 'Nivel3' UNION SELECT 4, 'Nivel4' ")
        Me.LstNivelBalanza.Combo.SelectedIndex = 0

        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2010 Then
                Me.ComboAñoB.Items.Add(Str(i))
            End If
        Next
        Me.ComboAñoB.Text = Str(DateTime.Now.Year)
        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2010 Then
                Me.Anio2.Items.Add(Str(i))
            End If
        Next
        Me.Anio2.Text = Str(DateTime.Now.Year)
        Me.Lstctainicial.Cargar("SELECT cuenta, Rtrim(Descripcion) + '/' +  Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4  from Catalogo_de_Cuentas where Id_Empresa = " & Me.LstCliente.SelectItem & " order by nivel1,nivel2,nivel3,nivel4")
        Me.Lstctainicial.Combo.SelectedIndex = 0

        Me.Lstctafinal.Cargar("SELECT cuenta, Rtrim(Descripcion) + '/' + Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4   from Catalogo_de_Cuentas where Id_Empresa = " & Me.LstCliente.SelectItem & " order by nivel1,nivel2,nivel3,nivel4")
        Me.Lstctafinal.Combo.SelectedIndex = Me.Lstctafinal.Combo.Items.Count - 1
    End Sub

    Private Sub Buscar(ByVal Fi As String, ByVal FF As String, ByVal Nivel As String, ByVal Fil As Integer)
        Me.Barra.Maximum = IIf(Me.Tabla.RowCount = 0, 0, Me.Tabla.RowCount - 1)
        Me.Barra.Minimum = 0
        Me.Barra.Value1 = 0
        For i As Integer = 0 To Fil
            Buscar_Cargos(Fi, FF, Me.Tabla.Item(1, i).Value.ToString, i, Me.Tabla.Item(0, i).Value)
            Buscar_Abonos(Fi, FF, Me.Tabla.Item(1, i).Value.ToString, i, Me.Tabla.Item(0, i).Value)
            Dim acumulado As Decimal = Calcula_Saldos_InicialesP(Me.Tabla.Item(1, i).Value.ToString, Fi.ToString.Substring(6, 4), Me.LstCliente.SelectItem, "  ( Polizas.Fecha_captura <" & Eventos.Sql_hoy(Fi) & " )")
            Me.Tabla.Item(4, i).Value = Eventos.Calcula_Saldos_Iniciales(Me.Tabla.Item(1, i).Value.ToString, Fi.ToString.Substring(6, 4) - 1, Me.LstCliente.SelectItem) + acumulado
            If Me.Barra.Value1 = Me.Barra.Maximum Then
                Me.Barra.Minimum = 0
                Me.Cursor = Cursors.Arrow
                Me.Barra.Value1 = 0

            Else
                Me.Barra.Value1 += 1
            End If
        Next
        Final()
    End Sub
    Private Sub Buscar_Cargos(ByVal Fi As String, ByVal FF As String, ByVal cuenta As String, ByVal i As Integer, ByVal nivel As String)


        Dim Cuentas As String = ""
        Dim sql As String = ""
        If Me.Lstctainicial.SelectText <> "" And Me.Lstctafinal.SelectText <> "" Then
            Cuentas = " and ( Detalle_Polizas.Cuenta >= " & Me.Lstctainicial.SelectItem & " and Detalle_Polizas.Cuenta <=" & Me.Lstctafinal.SelectItem & " )"

        End If
        Dim where As String = ""
        Select Case Len(Trim(nivel.Replace("-", "")))
            Case 4
                sql = "CargosBalanza " & "1" & "," & Me.LstCliente.SelectItem & ",'" & Fi & "','" & FF & "'," & Me.Lstctainicial.SelectItem & "," & Me.Lstctafinal.SelectItem & ",'" & Trim(nivel.Replace("-", "").ToString.Substring(0, 4)) & "','0000','0000','0000'"
            Case 8
                sql = "CargosBalanza " & "2" & "," & Me.LstCliente.SelectItem & ",'" & Fi & "','" & FF & "'," & Me.Lstctainicial.SelectItem & "," & Me.Lstctafinal.SelectItem & ",'" & Trim(nivel.Replace("-", "").ToString.Substring(0, 4)) & "','" & Trim(nivel.Replace("-", "").ToString.Substring(4, 4)) & "','0000','0000'"
            Case 12
                sql = "CargosBalanza " & "3" & "," & Me.LstCliente.SelectItem & ",'" & Fi & "','" & FF & "'," & Me.Lstctainicial.SelectItem & "," & Me.Lstctafinal.SelectItem & ",'" & Trim(nivel.Replace("-", "").ToString.Substring(0, 4)) & "','" & Trim(nivel.Replace("-", "").ToString.Substring(4, 4)) & "','" & Trim(nivel.Replace("-", "").ToString.Substring(8, 4)) & "','0000'"
            Case 16
                sql = "CargosBalanza " & "4" & "," & Me.LstCliente.SelectItem & ",'" & Fi & "','" & FF & "'," & Me.Lstctainicial.SelectItem & "," & Me.Lstctafinal.SelectItem & ",'" & Trim(nivel.Replace("-", "").ToString.Substring(0, 4)) & "','" & Trim(nivel.Replace("-", "").ToString.Substring(4, 4)) & "','" & Trim(nivel.Replace("-", "").ToString.Substring(8, 4)) & "','" & Trim(nivel.Replace("-", "").ToString.Substring(12, 4) & "'")
        End Select
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.Item(5, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0)) 'Recupera el valor de la suma de cargos de cada cuenta
        Else
            Me.Tabla.Item(5, i).Value = 0
        End If

    End Sub
    Private Sub Buscar_Abonos(ByVal Fi As String, ByVal FF As String, ByVal cuenta As String, ByVal i As Integer, ByVal nivel As String)
        Dim Cuentas As String = ""
        Dim sql As String = ""
        If Me.Lstctainicial.SelectText <> "" And Me.Lstctafinal.SelectText <> "" Then
            Cuentas = " and ( Detalle_Polizas.Cuenta >= " & Me.Lstctainicial.SelectItem & " and Detalle_Polizas.Cuenta <=" & Me.Lstctafinal.SelectItem & " )"

        End If
        Dim where As String = ""
        Select Case Len(Trim(nivel.Replace("-", "")))
            Case 4
                sql = "AbonosBalanza " & "1" & "," & Me.LstCliente.SelectItem & ",'" & Fi & "','" & FF & "'," & Me.Lstctainicial.SelectItem & "," & Me.Lstctafinal.SelectItem & ",'" & Trim(nivel.Replace("-", "").ToString.Substring(0, 4)) & "','0000','0000','0000'"
            Case 8
                sql = "AbonosBalanza " & "2" & "," & Me.LstCliente.SelectItem & ",'" & Fi & "','" & FF & "'," & Me.Lstctainicial.SelectItem & "," & Me.Lstctafinal.SelectItem & ",'" & Trim(nivel.Replace("-", "").ToString.Substring(0, 4)) & "','" & Trim(nivel.Replace("-", "").ToString.Substring(4, 4)) & "','0000','0000'"
            Case 12
                sql = "AbonosBalanza " & "3" & "," & Me.LstCliente.SelectItem & ",'" & Fi & "','" & FF & "'," & Me.Lstctainicial.SelectItem & "," & Me.Lstctafinal.SelectItem & ",'" & Trim(nivel.Replace("-", "").ToString.Substring(0, 4)) & "','" & Trim(nivel.Replace("-", "").ToString.Substring(4, 4)) & "','" & Trim(nivel.Replace("-", "").ToString.Substring(8, 4)) & "','0000'"
            Case 16
                sql = "AbonosBalanza " & "4" & "," & Me.LstCliente.SelectItem & ",'" & Fi & "','" & FF & "'," & Me.Lstctainicial.SelectItem & "," & Me.Lstctafinal.SelectItem & ",'" & Trim(nivel.Replace("-", "").ToString.Substring(0, 4)) & "','" & Trim(nivel.Replace("-", "").ToString.Substring(4, 4)) & "','" & Trim(nivel.Replace("-", "").ToString.Substring(8, 4)) & "','" & Trim(nivel.Replace("-", "").ToString.Substring(12, 4) & "'")
        End Select



        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.Item(6, i).Value = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0)) 'Recupera el valor de la suma de cargos de cada cuenta
        Else
            Me.Tabla.Item(6, i).Value = 0
        End If
        ' Next
    End Sub

    Private Sub Buscar_Balanzas(ByVal Anio As String, ByVal Nivel As String)
        Dim Where As String = ""
        'Dim sql As String = "CuentasAñoBalanza " & Nivel & "," & Me.lstClientesMasivos.SelectItem & "," & Me.Anio2.Text.Trim()
        Dim sql As String = ""
        Select Case Nivel
            Case 1
                Where = " And  Nivel1 > 0 and Nivel2='0000' "
            Case 2
                Where = " And  Nivel1 > 0 And  Nivel2 >='0000' and Nivel3='0000' "
            Case 3
                Where = " And  Nivel1 > 0 And  Nivel2  >='0000' And  Nivel3  >='0000' and Nivel4='0000' "
            Case 4
                Where = " And  Nivel1 > 0 And  Nivel2  >='0000' And  Nivel3  >='0000' and Nivel4 >='0000' "
        End Select

        sql = "SELECT * FROM (
                            Select cuenta,   CASE WHEN nivel4 >'0000' THEN nivel1 + '-' + nivel2 + '-' + nivel3 + '-' + nivel4 
                            WHEN nivel3 >'0000' THEN nivel1 + '-' + nivel2 + '-' + nivel3 
                            WHEN nivel2 >'0000' THEN nivel1 + '-' + nivel2 
                            WHEN  nivel2 ='0000' then nivel1 END  AS ID_cuenta,  Rtrim(Descripcion) as Descripcion ,Naturaleza,
                            CASE WHEN nivel4 >'0000' THEN 4 
                            WHEN nivel3 >'0000' THEN 3 
                            WHEN nivel2 >'0000' THEN 2 
                            WHEN  nivel2 ='0000' then 1 END  AS Nivel,RFC
                            from catalogo_de_cuentas where Id_Empresa= " & Me.LstCliente.SelectItem & "
                            " & Where & " AND catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.Anio2.Text.Trim() & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  )) AS Tabla    order by cuenta"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tablaenero.RowCount = ds.Tables(0).Rows.Count
            Me.TablaFebrero.RowCount = ds.Tables(0).Rows.Count
            Me.TablaMarzo.RowCount = ds.Tables(0).Rows.Count
            Me.TablaAbril.RowCount = ds.Tables(0).Rows.Count
            Me.TablaMayo.RowCount = ds.Tables(0).Rows.Count
            Me.TablaJunio.RowCount = ds.Tables(0).Rows.Count
            Me.TablaJulio.RowCount = ds.Tables(0).Rows.Count
            Me.TablaAgosto.RowCount = ds.Tables(0).Rows.Count
            Me.TablaSeptiembre.RowCount = ds.Tables(0).Rows.Count
            Me.TablaOctubre.RowCount = ds.Tables(0).Rows.Count
            Me.TablaNoviembre.RowCount = ds.Tables(0).Rows.Count
            Me.TablaDiciembre.RowCount = ds.Tables(0).Rows.Count
            Me.TablaAnual.RowCount = ds.Tables(0).Rows.Count
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.Tablaenero.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            Dim mes As String = ""
            Where = ""
            'Enero

            For j As Integer = 0 To Me.Tablaenero.Rows.Count - 1
                Me.Tablaenero.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.Tablaenero.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.Tablaenero.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.Tablaenero.Item(3, j).Value = ds.Tables(0).Rows(j)(2)
                'Febrero
                Me.TablaFebrero.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaFebrero.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaFebrero.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaFebrero.Item(3, j).Value = ds.Tables(0).Rows(j)(2)
                'Marzo
                Me.TablaMarzo.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaMarzo.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaMarzo.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaMarzo.Item(3, j).Value = ds.Tables(0).Rows(j)(2)
                'Abril
                Me.TablaAbril.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaAbril.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaAbril.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaAbril.Item(3, j).Value = ds.Tables(0).Rows(j)(2)
                'Mayo
                Me.TablaMayo.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaMayo.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaMayo.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaMayo.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'Junio
                Me.TablaJunio.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaJunio.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaJunio.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaJunio.Item(3, j).Value = ds.Tables(0).Rows(j)(2)
                'julio
                Me.TablaJulio.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaJulio.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaJulio.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaJulio.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'Agosto
                Me.TablaAgosto.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaAgosto.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaAgosto.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaAgosto.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'Septiembre
                Me.TablaSeptiembre.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaSeptiembre.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaSeptiembre.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaSeptiembre.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'octubre
                Me.TablaOctubre.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaOctubre.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaOctubre.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaOctubre.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'Noviembre
                Me.TablaNoviembre.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaNoviembre.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaNoviembre.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaNoviembre.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'Diciembre
                Me.TablaDiciembre.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaDiciembre.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaDiciembre.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaDiciembre.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                'Anual
                Me.TablaAnual.Item(0, j).Value = ds.Tables(0).Rows(j)(1) 'Recupera el valor de la cuenta de Mayor
                Me.TablaAnual.Item(1, j).Value = ds.Tables(0).Rows(j)(0)
                Me.TablaAnual.Item(2, j).Value = ds.Tables(0).Rows(j)(3)
                Me.TablaAnual.Item(3, j).Value = ds.Tables(0).Rows(j)(2)

                Select Case Len(Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "")))
                    Case 4
                        Where = "   Nivel1 = '" & Trim(Me.Tablaenero.Item(0, j).Value) & "' and Nivel2 >= '0000' "
                    Case 8
                        Where = "  Nivel1 = '" & Trim(Me.Tablaenero.Item(0, j).Value.ToString.Substring(0, 4)) & "' and Nivel2   = '" & Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "").ToString.Substring(4, 4)) & "' and Nivel3 >= '0000' "
                    Case 12
                        Where = "  Nivel1 = '" & Trim(Me.Tablaenero.Item(0, j).Value.ToString.Substring(0, 4)) & "' and Nivel2   = '" & Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "").ToString.Substring(4, 4)) & "' and Nivel3='" & Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "").ToString.Substring(8, 4)) & "' and Nivel4 >= '0000' "
                    Case 16
                        Where = "   Nivel1 = '" & Trim(Me.Tablaenero.Item(0, j).Value.ToString.Substring(0, 4)) & "' and Nivel2   = '" & Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "").ToString.Substring(4, 4)) & "' and Nivel3='" & Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "").ToString.Substring(8, 4)) & "' and Nivel4 ='" & Trim(Me.Tablaenero.Item(0, j).Value.Replace("-", "").ToString.Substring(12, 4)) & "'  "
                End Select
                For a As Integer = 1 To 12
                    mes = a
                    If Len(mes) < 2 Then
                        mes = "0" & mes
                    End If
                    Dim Procedimiento As String = "BalanzasMeses '" & Me.lstClientesMasivos.SelectItem & "','" & Me.Anio2.SelectedItem & "','" & mes & "','" & Convert.ToInt32(Me.Anio2.Text) - 1 & "','" & Where.Replace("'", "") & "'"

                    ' Dim sql As String = "CuentasBalanza " & Nivel & "," & Me.LstCliente.SelectItem & "," & Fi.Substring(6, 4) & "," & Me.Lstctainicial.SelectItem.ToString.Substring(0, 4) & "," & Me.Lstctafinal.SelectItem.ToString.Substring(0, 4)
                    Dim DP As DataSet = Eventos.Obtener_DS(Procedimiento)
                    Try
                        If DP.Tables(0).Rows.Count > 0 Or DP.Tables(1).Rows.Count > 0 Or DP.Tables(2).Rows.Count > 0 Then
                            Dim Saldo As Decimal = 0

                            If DP.Tables(1).Rows.Count > 0 Then
                                If DP.Tables(1).Rows(0)(0) <> 0 Then
                                    Saldo = DP.Tables(1).Rows(0)(0)
                                Else
                                    Saldo = DP.Tables(1).Rows(0)(1)
                                End If
                            Else
                                Saldo = 0
                            End If
                            If DP.Tables(2).Rows.Count > 0 Then
                                Saldo += IIf(IsDBNull(DP.Tables(2).Rows(0)(0)) = True, 0, DP.Tables(2).Rows(0)(0))
                            Else
                                Saldo += 0
                            End If
                            If DP.Tables(0).Rows.Count > 0 Then

                                Select Case a
                                    Case 1
                                        Me.Tablaenero.Item(4, j).Value = Saldo
                                        Me.Tablaenero.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                        Me.Tablaenero.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                    Case 2
                                        Me.TablaFebrero.Item(4, j).Value = Saldo
                                        Me.TablaFebrero.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                        Me.TablaFebrero.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                    Case 3
                                        Me.TablaMarzo.Item(4, j).Value = Saldo
                                        Me.TablaMarzo.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                        Me.TablaMarzo.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                    Case 4
                                        Me.TablaAbril.Item(4, j).Value = Saldo
                                        Me.TablaAbril.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                        Me.TablaAbril.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                    Case 5
                                        Me.TablaMayo.Item(4, j).Value = Saldo
                                        Me.TablaMayo.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                        Me.TablaMayo.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                    Case 6
                                        Me.TablaJunio.Item(4, j).Value = Saldo
                                        Me.TablaJunio.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                        Me.TablaJunio.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                    Case 7
                                        Me.TablaJulio.Item(4, j).Value = Saldo
                                        Me.TablaJulio.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                        Me.TablaJulio.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                    Case 8
                                        Me.TablaAgosto.Item(4, j).Value = Saldo
                                        Me.TablaAgosto.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                        Me.TablaAgosto.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                    Case 9
                                        Me.TablaSeptiembre.Item(4, j).Value = Saldo
                                        Me.TablaSeptiembre.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                        Me.TablaSeptiembre.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                    Case 10
                                        Me.TablaOctubre.Item(4, j).Value = Saldo
                                        Me.TablaOctubre.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                        Me.TablaOctubre.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                    Case 11
                                        Me.TablaNoviembre.Item(4, j).Value = Saldo
                                        Me.TablaNoviembre.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                        Me.TablaNoviembre.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                    Case 12
                                        Me.TablaDiciembre.Item(4, j).Value = Saldo
                                        Me.TablaDiciembre.Item(5, j).Value = DP.Tables(0).Rows(0)(0)
                                        Me.TablaDiciembre.Item(6, j).Value = DP.Tables(0).Rows(0)(1)
                                End Select
                            Else
                                Select Case a
                                    Case 1
                                        Me.Tablaenero.Item(4, j).Value = Saldo
                                    Case 2
                                        Me.TablaFebrero.Item(4, j).Value = Saldo
                                    Case 3
                                        Me.TablaMarzo.Item(4, j).Value = Saldo
                                    Case 4
                                        Me.TablaAbril.Item(4, j).Value = Saldo
                                    Case 5
                                        Me.TablaMayo.Item(4, j).Value = Saldo
                                    Case 6
                                        Me.TablaJunio.Item(4, j).Value = Saldo
                                    Case 7
                                        Me.TablaJulio.Item(4, j).Value = Saldo
                                    Case 8
                                        Me.TablaAgosto.Item(4, j).Value = Saldo
                                    Case 9
                                        Me.TablaSeptiembre.Item(4, j).Value = Saldo
                                    Case 10
                                        Me.TablaOctubre.Item(4, j).Value = Saldo
                                    Case 11
                                        Me.TablaNoviembre.Item(4, j).Value = Saldo
                                    Case 12
                                        Me.TablaDiciembre.Item(4, j).Value = Saldo
                                End Select
                            End If
                            Saldo = 0


                        End If
                    Catch ex As Exception

                    End Try

                Next
#Region "Antes"

#End Region
                'Buscar Anual
                Dim sql3 As String = "SELECT  sum(Detalle_Polizas.Cargo) as Cargos,sum(Detalle_Polizas.Abono ) as Abonos
                                FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                        INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa  
                               WHERE   " & Where & " And polizas.ID_anio = " & Me.Anio2.Text & "  and Polizas.Id_Empresa= " & Me.lstClientesMasivos.SelectItem & "
                                 "

                Dim saldos2 As String = " SELECT  CASE WHEN Naturaleza = 'D' THEN sUM(cargos - abonos) WHEN Naturaleza = 'A' THEN Sum(abonos - cargos) END AS Saldo   FROM (
                              SELECT Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza,sum(cargo ) AS Cargos,sum (abono )AS Abonos FROM Detalle_Polizas 
                              INNER JOIN Polizas ON Polizas.ID_poliza = Detalle_Polizas.ID_poliza
                              INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                              INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Cuenta = Detalle_Polizas.Cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa 
                              WHERE Polizas.Id_Empresa= " & Me.lstClientesMasivos.SelectItem & " AND  " & Where & " AND   Polizas.Concepto = 'Poliza Cierre' AND  Polizas.Id_Anio ='" & Convert.ToInt32(Me.Anio2.Text) - 1 & "'
                              GROUP BY Detalle_Polizas.Cuenta ,Catalogo_de_Cuentas.Naturaleza
                              ) AS Tabla_Saldos GROUP BY NATURALEZA"
                Dim sal2 As Decimal = Calcula_anteriores(saldos2)


                Dim ds3 As DataSet = Eventos.Obtener_DS(sql3)
                If ds3.Tables(0).Rows.Count > 0 Then
                    Me.TablaAnual.Item(4, j).Value = sal2
                    Me.TablaAnual.Item(5, j).Value = IIf(IsDBNull(ds3.Tables(0).Rows(0)(0)) = True, 0, ds3.Tables(0).Rows(0)(0))
                    Me.TablaAnual.Item(6, j).Value = IIf(IsDBNull(ds3.Tables(0).Rows(0)(0)) = True, 0, ds3.Tables(0).Rows(0)(1))
                Else

                    Me.TablaAnual.Item(5, j).Value = 0
                    Me.TablaAnual.Item(6, j).Value = 0
                End If
                frm.Barra.Value = j
                sal2 = 0

            Next
            FinalMeses()
            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        End If

    End Sub
    Private Sub CalculageneralCargos()

        Dim total As Decimal
        Dim cuenta As String
        For i As Integer = 0 To Me.Tabla.RowCount - 1
            If Len(Trim(Me.Tabla.Item(0, i).Value.ToString)) = 4 Then
                total = 0
                cuenta = Trim(Me.Tabla.Item(0, i).Value.ToString)
                For j As Integer = 0 To Me.Tabla.RowCount - 1
                    If Me.Tabla.Item(0, j).Value.ToString.Substring(0, 4) = cuenta Then
                        total = total + Me.Tabla.Item(5, j).Value
                    End If
                Next
                Me.Tabla.Item(5, i).Value = total
            End If
        Next
    End Sub
    Private Sub CalculageneralAbonos()
        Dim total As Decimal
        Dim cuenta As String
        For i As Integer = 0 To Me.Tabla.RowCount - 1
            If Len(Trim(Me.Tabla.Item(0, i).Value.ToString)) = 4 Then
                total = 0
                cuenta = Trim(Me.Tabla.Item(0, i).Value.ToString)
                For j As Integer = 0 To Me.Tabla.RowCount - 1
                    If Me.Tabla.Item(0, j).Value.ToString.Substring(0, 4) = cuenta Then
                        total = total + Me.Tabla.Item(6, j).Value
                    End If
                Next
                Me.Tabla.Item(6, i).Value = total
            End If
        Next
    End Sub
    Private Sub Final()

        For i As Integer = 0 To Me.Tabla.RowCount - 1

            Me.Tabla.Item(7, i).Value = (Me.Tabla.Item(4, i).Value + Me.Tabla.Item(5, i).Value) - Me.Tabla.Item(6, i).Value

        Next
    End Sub
    Private Sub FinalMeses()


        For i As Integer = 0 To Me.Tablaenero.RowCount - 1

            Me.Tablaenero.Item(7, i).Value = (Me.Tablaenero.Item(4, i).Value + Me.Tablaenero.Item(5, i).Value) - Me.Tablaenero.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaFebrero.RowCount - 1
            Me.TablaFebrero.Item(4, i).Value = Me.Tablaenero.Item(7, i).Value

            Me.TablaFebrero.Item(7, i).Value = (Me.TablaFebrero.Item(4, i).Value + Me.TablaFebrero.Item(5, i).Value) - Me.TablaFebrero.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaMarzo.RowCount - 1
            Me.TablaMarzo.Item(4, i).Value = Me.TablaFebrero.Item(7, i).Value

            Me.TablaMarzo.Item(7, i).Value = (Me.TablaMarzo.Item(4, i).Value + Me.TablaMarzo.Item(5, i).Value) - Me.TablaMarzo.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaAbril.RowCount - 1
            Me.TablaAbril.Item(4, i).Value = Me.TablaMarzo.Item(7, i).Value
            Me.TablaAbril.Item(7, i).Value = (Me.TablaAbril.Item(4, i).Value + Me.TablaAbril.Item(5, i).Value) - Me.TablaAbril.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaMayo.RowCount - 1
            Me.TablaMayo.Item(4, i).Value = Me.TablaAbril.Item(7, i).Value

            Me.TablaMayo.Item(7, i).Value = (Me.TablaMayo.Item(4, i).Value + Me.TablaMayo.Item(5, i).Value) - Me.TablaMayo.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaJunio.RowCount - 1
            Me.TablaJunio.Item(4, i).Value = Me.TablaMayo.Item(7, i).Value

            Me.TablaJunio.Item(7, i).Value = (Me.TablaJunio.Item(4, i).Value + Me.TablaJunio.Item(5, i).Value) - Me.TablaJunio.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaJulio.RowCount - 1
            Me.TablaJulio.Item(4, i).Value = Me.TablaJunio.Item(7, i).Value

            Me.TablaJulio.Item(7, i).Value = (Me.TablaJulio.Item(4, i).Value + Me.TablaJulio.Item(5, i).Value) - Me.TablaJulio.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaAgosto.RowCount - 1
            Me.TablaAgosto.Item(4, i).Value = Me.TablaJulio.Item(7, i).Value

            Me.TablaAgosto.Item(7, i).Value = (Me.TablaAgosto.Item(4, i).Value + Me.TablaAgosto.Item(5, i).Value) - Me.TablaAgosto.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaSeptiembre.RowCount - 1
            Me.TablaSeptiembre.Item(4, i).Value = Me.TablaAgosto.Item(7, i).Value
            Me.TablaSeptiembre.Item(7, i).Value = (Me.TablaSeptiembre.Item(4, i).Value + Me.TablaSeptiembre.Item(5, i).Value) - Me.TablaSeptiembre.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaOctubre.RowCount - 1
            Me.TablaOctubre.Item(4, i).Value = Me.TablaSeptiembre.Item(7, i).Value
            Me.TablaOctubre.Item(7, i).Value = (Me.TablaOctubre.Item(4, i).Value + Me.TablaOctubre.Item(5, i).Value) - Me.TablaOctubre.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaNoviembre.RowCount - 1
            Me.TablaNoviembre.Item(4, i).Value = Me.TablaOctubre.Item(7, i).Value
            Me.TablaNoviembre.Item(7, i).Value = (Me.TablaNoviembre.Item(4, i).Value + Me.TablaNoviembre.Item(5, i).Value) - Me.TablaNoviembre.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaDiciembre.RowCount - 1
            Me.TablaDiciembre.Item(4, i).Value = Me.TablaNoviembre.Item(7, i).Value
            Me.TablaDiciembre.Item(7, i).Value = (Me.TablaDiciembre.Item(4, i).Value + Me.TablaDiciembre.Item(5, i).Value) - Me.TablaDiciembre.Item(6, i).Value

        Next
        For i As Integer = 0 To Me.TablaAnual.RowCount - 1
            Me.TablaAnual.Item(7, i).Value = (Me.TablaAnual.Item(4, i).Value + Me.TablaAnual.Item(5, i).Value) - Me.TablaAnual.Item(6, i).Value
        Next
    End Sub
    Private Sub Limpiar()
        Me.lblSaldofinal.Text = ""
        Me.lblsaldoanterior.Text = ""
        Me.lblhaber.Text = ""
        Me.lbldebe.Text = ""
        Me.lblCuentas.Text = ""
        Me.Tabla.Rows.Clear()
        Fila = 0

    End Sub
    Private Sub CTAS(ByVal Fi As String, ByVal FF As String, ByVal Nivel As String)
        Limpiar()
        Dim Where As String = ""
        Dim Cuentas As String = ""
        Select Case Nivel
            Case 1
                Where = " And  Nivel1 > 0 and Nivel2='0000' "
            Case 2
                Where = " And  Nivel1 > 0 And  Nivel2 >='0000' and Nivel3='0000' "
            Case 3
                Where = " And  Nivel1 > 0 And  Nivel2  >='0000' And  Nivel3  >='0000' and Nivel4='0000' "
            Case 4
                Where = " And  Nivel1 > 0 And  Nivel2  >='0000' And  Nivel3  >='0000' and Nivel4 >='0000' "
        End Select
        Dim Ini() As String = Split(Me.Lstctainicial.SelectText, "/")
        Dim Fin() As String = Split(Me.Lstctafinal.SelectText, "/")


        Dim sql As String = "SELECT * FROM (
                            Select cuenta,   CASE WHEN nivel4 >'0000' THEN nivel1 + '-' + nivel2 + '-' + nivel3 + '-' + nivel4 
                            WHEN nivel3 >'0000' THEN nivel1 + '-' + nivel2 + '-' + nivel3 
                            WHEN nivel2 >'0000' THEN nivel1 + '-' + nivel2 
                            WHEN  nivel2 ='0000' then nivel1 END  AS ID_cuenta,  Rtrim(Descripcion) as Descripcion ,Naturaleza,
                            CASE WHEN nivel4 >'0000' THEN 4 
                            WHEN nivel3 >'0000' THEN 3 
                            WHEN nivel2 >'0000' THEN 2 
                            WHEN  nivel2 ='0000' then 1 END  AS Nivel,RFC
                            from catalogo_de_cuentas where Id_Empresa= " & Me.LstCliente.SelectItem & "
                            " & Where & " AND catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo 
                            WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  
                            AND (Cuenta >='" & Ini(1).Replace("-", "") & "' AND Cuenta<'" & Fin(1).Replace("-", "") & "' + '1000000000000000'))
                             ) AS Tabla    order by cuenta"




        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = ds.Tables(0).Rows.Count
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Me.Tabla.Item(0, i).Value = ds.Tables(0).Rows(i)(1) 'Recupera el valor de la cuenta de Mayor
                Me.Tabla.Item(1, i).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla.Item(2, i).Value = ds.Tables(0).Rows(i)(3)
                Me.Tabla.Item(3, i).Value = ds.Tables(0).Rows(i)(2)
            Next
            Fila = Me.Tabla.RowCount - 1
        End If
    End Sub
    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        Me.Tabla.Enabled = False
        Eventos.Cuentas(Me.DtInicio.Value.ToString.Substring(6, 4), Me.LstCliente.SelectItem)
        CTAS(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), Me.LstNivelBalanza.SelectItem)
        SegundoPlano.RunWorkerAsync(Me.Tabla)
        Control.CheckForIllegalCrossThreadCalls = False
        Me.Tabla.Enabled = True

    End Sub


    Private Sub QuitacuentasceroBalanzaNormal()
        Try

            Dim filas As Integer = Me.Tabla.RowCount - 1
            For j As Integer = 0 To filas
                For i As Integer = 0 To Me.Tabla.RowCount - 1
                    If Me.Tabla.Item(5, i).Value = 0 And Me.Tabla.Item(6, i).Value = 0 And Me.Tabla.Item(7, i).Value = 0 Then
                        Try
                            Me.Tabla.Rows.RemoveAt(i)
                        Catch ex As Exception

                        End Try

                        Exit For
                    End If
                Next
            Next
        Catch ex As Exception

        End Try
        Me.lblCuentas.Text = Me.Tabla.RowCount
    End Sub
    Private Sub QuitacuentasceroBalanzas()
        Try


            Dim filas As Integer = Me.TablaMensual.RowCount - 1
            For j As Integer = 0 To filas
                For i As Integer = 0 To Me.TablaMensual.RowCount - 1
                    If Me.TablaMensual.Item(5, i).Value = 0 And Me.TablaMensual.Item(6, i).Value = 0 And Me.TablaMensual.Item(7, i).Value = 0 And
                            Me.TablaMensual.Item(8, i).Value = 0 And Me.TablaMensual.Item(9, i).Value = 0 And Me.TablaMensual.Item(10, i).Value = 0 And
                            Me.TablaMensual.Item(11, i).Value = 0 And Me.TablaMensual.Item(12, i).Value = 0 And Me.TablaMensual.Item(13, i).Value = 0 And
                            Me.TablaMensual.Item(14, i).Value = 0 And Me.TablaMensual.Item(15, i).Value = 0 And Me.TablaMensual.Item(16, i).Value = 0 Then
                        Me.TablaMensual.Rows.RemoveAt(i)
                        Exit For
                    End If

                Next
            Next
            filas = Me.TablaAbonos.RowCount - 1
            For j As Integer = 0 To filas
                For i As Integer = 0 To Me.TablaAbonos.RowCount - 1
                    If Me.TablaAbonos.Item(5, i).Value = 0 And Me.TablaAbonos.Item(6, i).Value = 0 And Me.TablaAbonos.Item(7, i).Value = 0 And
                         Me.TablaAbonos.Item(8, i).Value = 0 And Me.TablaAbonos.Item(9, i).Value = 0 And Me.TablaAbonos.Item(10, i).Value = 0 And
                         Me.TablaAbonos.Item(11, i).Value = 0 And Me.TablaAbonos.Item(12, i).Value = 0 And Me.TablaAbonos.Item(13, i).Value = 0 And
                         Me.TablaAbonos.Item(14, i).Value = 0 And Me.TablaAbonos.Item(15, i).Value = 0 And Me.TablaAbonos.Item(16, i).Value = 0 Then
                        Me.TablaAbonos.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next
            Next
            filas = Me.TablaCargos.RowCount - 1
            For j As Integer = 0 To filas
                For i As Integer = 0 To Me.TablaCargos.RowCount - 1
                    If Me.TablaCargos.Item(5, i).Value = 0 And Me.TablaCargos.Item(6, i).Value = 0 And Me.TablaCargos.Item(7, i).Value = 0 And
                    Me.TablaCargos.Item(8, i).Value = 0 And Me.TablaCargos.Item(9, i).Value = 0 And Me.TablaCargos.Item(10, i).Value = 0 And
                    Me.TablaCargos.Item(11, i).Value = 0 And Me.TablaCargos.Item(12, i).Value = 0 And Me.TablaCargos.Item(13, i).Value = 0 And
                    Me.TablaCargos.Item(14, i).Value = 0 And Me.TablaCargos.Item(15, i).Value = 0 And Me.TablaCargos.Item(16, i).Value = 0 Then
                        Me.TablaCargos.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub QuitacuentasceroBalanzapormes()
        Dim filas As Integer = Me.Tablaenero.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.Tablaenero.RowCount - 1
                Try
                    If Me.Tablaenero.Item(5, i).Value = 0 And Me.Tablaenero.Item(6, i).Value = 0 And Me.Tablaenero.Item(7, i).Value = 0 And Me.Tablaenero.Item(3, i).Value.ToString() <> "Totales:" Then
                        Me.Tablaenero.Rows.RemoveAt(i)
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
        Next
        filas = Me.TablaFebrero.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaFebrero.RowCount - 1
                Try
                    If Me.TablaFebrero.Item(5, i).Value = 0 And Me.TablaFebrero.Item(6, i).Value = 0 And Me.TablaFebrero.Item(7, i).Value = 0 And Me.TablaFebrero.Item(3, i).Value.ToString() <> "Totales:" Then
                        Me.TablaFebrero.Rows.RemoveAt(i)
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
        Next
        filas = Me.TablaMarzo.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaMarzo.RowCount - 1
                Try
                    If Me.TablaMarzo.Item(5, i).Value = 0 And Me.TablaMarzo.Item(6, i).Value = 0 And Me.TablaMarzo.Item(7, i).Value = 0 And Me.TablaMarzo.Item(3, i).Value.ToString() <> "Totales:" Then
                        Me.TablaMarzo.Rows.RemoveAt(i)
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
        Next
        filas = Me.TablaAbril.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaAbril.RowCount - 1
                Try
                    If Me.TablaAbril.Item(5, i).Value = 0 And Me.TablaAbril.Item(6, i).Value = 0 And Me.TablaAbril.Item(7, i).Value = 0 And Me.TablaAbril.Item(3, i).Value.ToString() <> "Totales:" Then
                        Me.TablaAbril.Rows.RemoveAt(i)
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
        Next
        filas = Me.TablaMayo.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaMayo.RowCount - 1
                Try
                    If Me.TablaMayo.Item(5, i).Value = 0 And Me.TablaMayo.Item(6, i).Value = 0 And Me.TablaMayo.Item(7, i).Value = 0 And Me.TablaMayo.Item(3, i).Value.ToString() <> "Totales:" Then
                        Me.TablaMayo.Rows.RemoveAt(i)
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
        Next
        filas = Me.TablaJunio.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaJunio.RowCount - 1
                Try
                    If Me.TablaJunio.Item(5, i).Value = 0 And Me.TablaJunio.Item(6, i).Value = 0 And Me.TablaJunio.Item(7, i).Value = 0 And Me.TablaJunio.Item(3, i).Value.ToString() <> "Totales:" Then
                        Me.TablaJunio.Rows.RemoveAt(i)
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
        Next

        filas = Me.TablaJulio.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaJulio.RowCount - 1
                Try
                    If Me.TablaJulio.Item(5, i).Value = 0 And Me.TablaJulio.Item(6, i).Value = 0 And Me.TablaJulio.Item(7, i).Value = 0 And Me.TablaJulio.Item(3, i).Value.ToString() <> "Totales:" Then
                        Me.TablaJulio.Rows.RemoveAt(i)
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
        Next
        filas = Me.TablaAgosto.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaAgosto.RowCount - 1
                Try
                    If Me.TablaAgosto.Item(5, i).Value = 0 And Me.TablaAgosto.Item(6, i).Value = 0 And Me.TablaAgosto.Item(7, i).Value = 0 And Me.TablaAgosto.Item(3, i).Value.ToString() <> "Totales:" Then
                        Me.TablaAgosto.Rows.RemoveAt(i)
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
        Next
        filas = Me.TablaSeptiembre.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaSeptiembre.RowCount - 1
                Try
                    If Me.TablaSeptiembre.Item(5, i).Value = 0 And Me.TablaSeptiembre.Item(6, i).Value = 0 And Me.TablaSeptiembre.Item(7, i).Value = 0 And Me.TablaSeptiembre.Item(3, i).Value.ToString() <> "Totales:" Then
                        Me.TablaSeptiembre.Rows.RemoveAt(i)
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
        Next
        filas = Me.TablaOctubre.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaOctubre.RowCount - 1
                Try
                    If Me.TablaOctubre.Item(5, i).Value = 0 And Me.TablaOctubre.Item(6, i).Value = 0 And Me.TablaOctubre.Item(7, i).Value = 0 And Me.TablaOctubre.Item(3, i).Value.ToString() <> "Totales:" Then
                        Me.TablaOctubre.Rows.RemoveAt(i)
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
        Next
        filas = Me.TablaNoviembre.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaNoviembre.RowCount - 1
                Try
                    If Me.TablaNoviembre.Item(5, i).Value = 0 And Me.TablaNoviembre.Item(6, i).Value = 0 And Me.TablaNoviembre.Item(7, i).Value = 0 And Me.TablaNoviembre.Item(3, i).Value.ToString() <> "Totales:" Then
                        Me.TablaNoviembre.Rows.RemoveAt(i)
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
        Next
        filas = Me.TablaDiciembre.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaDiciembre.RowCount - 1
                If Me.TablaDiciembre.Item(5, i).Value = 0 And Me.TablaDiciembre.Item(6, i).Value = 0 And Me.TablaDiciembre.Item(7, i).Value = 0 And Me.TablaDiciembre.Item(3, i).Value.ToString() <> "Totales:" Then
                    Me.TablaDiciembre.Rows.RemoveAt(i)
                    Exit For
                End If
            Next
        Next
        filas = Me.TablaAnual.RowCount - 1
        For j As Integer = 0 To filas
            For i As Integer = 0 To Me.TablaAnual.RowCount - 1
                Try
                    If Me.TablaAnual.Item(5, i).Value = 0 And Me.TablaAnual.Item(6, i).Value = 0 And Me.TablaAnual.Item(7, i).Value = 0 And Me.TablaAnual.Item(3, i).Value.ToString() <> "Totales:" Then
                        Me.TablaAnual.Rows.RemoveAt(i)
                        Exit For
                    End If
                Catch ex As Exception

                End Try

            Next
        Next
    End Sub
    Private Sub TotalSaldoanterior()
        Dim total As Decimal = 0
        For i As Integer = 0 To Fila
            If Len(Trim(Me.Tabla.Item(0, i).Value.ToString)) = 4 Then
                total = total + Me.Tabla.Item(4, i).Value
                Me.lblsaldoanterior.Text = total.ToString("N2")
            End If
        Next
    End Sub
    Private Sub Total_haber()
        Dim total As Decimal = 0
        For i As Integer = 0 To Fila
            If Len(Trim(Me.Tabla.Item(0, i).Value.ToString)) = 4 Then
                total = total + Me.Tabla.Item(6, i).Value
                Me.lblhaber.Text = total.ToString("N2")
            End If
        Next
    End Sub
    Private Sub Total_debe()
        Dim total As Decimal = 0
        For i As Integer = 0 To Fila
            If Len(Trim(Me.Tabla.Item(0, i).Value.ToString)) = 4 Then
                total = total + Me.Tabla.Item(5, i).Value
                Me.lbldebe.Text = total.ToString("N2")
            End If
        Next
    End Sub
    Private Sub Total_saldo_final()
        Dim total As Decimal = 0
        For i As Integer = 0 To Fila
            Try


                If Len(Trim(Me.Tabla.Item(0, i).Value.ToString)) = 4 Then
                    total = total + Me.Tabla.Item(7, i).Value
                End If
            Catch ex As Exception

            End Try
        Next

        Me.lblSaldofinal.Text = total.ToString("N2")
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        Limpiar()
    End Sub

    Private Sub LstCliente_cambio_item(value As String, texto As String) Handles LstCliente.Cambio_item
        If activo = False Then
            Me.Lstclientes2.SelectItem = value
            Me.Lstclientes2.SelectText = texto
            Me.lstClientesMasivos.SelectItem = value
            Me.lstClientesMasivos.SelectText = texto
        End If
    End Sub

    Private Sub CmdLimpiar2_Click(sender As Object, e As EventArgs) Handles CmdLimpiar2.Click
        Me.Lstclientes2.SelectText = ""
        Me.ComboAñoB.Text = ""

    End Sub
    Private Sub Limpia2()
        Me.TablaMensual.Rows.Clear()
        Me.TablaAbonos.Rows.Clear()
        Me.TablaCargos.Rows.Clear()
    End Sub

    Private Sub CmdBuscar2_Click(sender As Object, e As EventArgs) Handles CmdBuscar2.Click
        Limpia2()
        Buscar_cuentas(Me.LstNiveles.SelectItem)
    End Sub
    Private Sub Buscar_cuentas(ByVal Nivel As String)
        Me.Cursor = Cursors.AppStarting
        Dim Where As String = ""
        Dim sql As String = ""
        Select Case Nivel
            Case 1
                Where = " And  Nivel1 > 0 and Nivel2='0000' "
            Case 2
                Where = " And  Nivel1 > 0 And  Nivel2 >='0000' and Nivel3='0000' "
            Case 3
                Where = " And  Nivel1 > 0 And  Nivel2  >='0000' And  Nivel3  >='0000' and Nivel4='0000' "
            Case 4
                Where = " And  Nivel1 > 0 And  Nivel2  >='0000' And  Nivel3  >='0000' and Nivel4 >='0000' "
        End Select

        sql = "SELECT * FROM (
                            Select cuenta,   CASE WHEN nivel4 >'0000' THEN nivel1 + '-' + nivel2 + '-' + nivel3 + '-' + nivel4 
                            WHEN nivel3 >'0000' THEN nivel1 + '-' + nivel2 + '-' + nivel3 
                            WHEN nivel2 >'0000' THEN nivel1 + '-' + nivel2 
                            WHEN  nivel2 ='0000' then nivel1 END  AS ID_cuenta,  Rtrim(Descripcion) as Descripcion ,Naturaleza,
                            CASE WHEN nivel4 >'0000' THEN 4 
                            WHEN nivel3 >'0000' THEN 3 
                            WHEN nivel2 >'0000' THEN 2 
                            WHEN  nivel2 ='0000' then 1 END  AS Nivel,RFC
                            from catalogo_de_cuentas where Id_Empresa= " & Me.LstCliente.SelectItem & "
                            " & Where & " AND catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo 
                            WHERE Anio = " & Me.ComboAñoB.Text.Trim() & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  )) AS Tabla    order by cuenta"

        Eventos.Cuentas(Me.ComboAñoB.Text.Trim(), Me.LstCliente.SelectItem)
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaMensual.RowCount = ds.Tables(0).Rows.Count
            Me.TablaCargos.RowCount = ds.Tables(0).Rows.Count
            Me.TablaAbonos.RowCount = ds.Tables(0).Rows.Count
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Me.TablaMensual.Item(0, i).Value = ds.Tables(0).Rows(i)(0)
                Me.TablaMensual.Item(1, i).Value = ds.Tables(0).Rows(i)(1)
                Me.TablaMensual.Item(2, i).Value = ds.Tables(0).Rows(i)(2)
                Me.TablaMensual.Item(3, i).Value = ds.Tables(0).Rows(i)(3)
                'Me.TablaMensual.Item(4, i).Value = Eventos.Calcula_Saldos_InicialesB(Me.TablaMensual.Item(0, i).Value.ToString, Me.ComboAñoB.Text.Trim() - 1, Me.LstCliente.SelectItem, "01/01" & Me.ComboAñoB.Text.Trim())
                ' calcular los saldos iniciales
                ' Dim acumulado As Decimal = Calcula_Saldos_InicialesP(Me.Tabla.Item(1, i).Value.ToString, Fi.ToString.Substring(6, 4) - 1, Me.LstCliente.SelectItem, "  ( Polizas.Fecha_captura <" & Eventos.Sql_hoy(Fi) & " )")
                Me.TablaMensual.Item(4, i).Value = Eventos.Calcula_Saldos_Iniciales(Me.TablaMensual.Item(0, i).Value.ToString, Me.ComboAñoB.Text.Trim() - 1, Me.LstCliente.SelectItem)

                Me.TablaCargos.Item(0, i).Value = ds.Tables(0).Rows(i)(0)
                Me.TablaCargos.Item(1, i).Value = ds.Tables(0).Rows(i)(1)
                Me.TablaCargos.Item(2, i).Value = ds.Tables(0).Rows(i)(2)
                Me.TablaCargos.Item(3, i).Value = ds.Tables(0).Rows(i)(3)

                Me.TablaAbonos.Item(0, i).Value = ds.Tables(0).Rows(i)(0)
                Me.TablaAbonos.Item(1, i).Value = ds.Tables(0).Rows(i)(1)
                Me.TablaAbonos.Item(2, i).Value = ds.Tables(0).Rows(i)(2)
                Me.TablaAbonos.Item(3, i).Value = ds.Tables(0).Rows(i)(3)
            Next

        Else
            Me.TablaMensual.RowCount = 0
        End If

        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.TablaMensual.RowCount - 1
        Me.Cursor = Cursors.AppStarting
        Dim mes As String = ""
        Where = ""
        For j As Integer = 0 To Me.TablaMensual.Rows.Count - 1

            Select Case Len(Trim(Me.TablaMensual.Item(1, j).Value.Replace("-", "")))
                Case 4
                    Where = "   Nivel1 = '" & Trim(Me.TablaMensual.Item(1, j).Value) & "' and Nivel2 >= '0000' "
                Case 8
                    Where = "  Nivel1 = '" & Trim(Me.TablaMensual.Item(1, j).Value.ToString.Substring(0, 4)) & "' and Nivel2   = '" & Trim(Me.TablaMensual.Item(1, j).Value.Replace("-", "").ToString.Substring(4, 4)) & "' and Nivel3 >= '0000' "
                Case 12
                    Where = "  Nivel1 = '" & Trim(Me.TablaMensual.Item(1, j).Value.ToString.Substring(0, 4)) & "' and Nivel2   = '" & Trim(Me.TablaMensual.Item(1, j).Value.Replace("-", "").ToString.Substring(4, 4)) & "' and Nivel3='" & Trim(Me.TablaMensual.Item(1, j).Value.Replace("-", "").ToString.Substring(8, 4)) & "' and Nivel4 >= '0000' "
                Case 16
                    Where = "   Nivel1 = '" & Trim(Me.TablaMensual.Item(1, j).Value.ToString.Substring(0, 4)) & "' and Nivel2   = '" & Trim(Me.TablaMensual.Item(1, j).Value.Replace("-", "").ToString.Substring(4, 4)) & "' and Nivel3='" & Trim(Me.TablaMensual.Item(1, j).Value.Replace("-", "").ToString.Substring(8, 4)) & "' and Nivel4 ='" & Trim(Me.TablaMensual.Item(1, j).Value.Replace("-", "").ToString.Substring(12, 4)) & "'  "
            End Select
            For a As Integer = 1 To 12
                mes = a
                If Len(mes) < 2 Then
                    mes = "0" & mes
                End If

                Dim sql2 As String = "SELECT CASE WHEN Catalogo_de_Cuentas.Naturaleza= 'D' then   sum(Detalle_Polizas.Cargo)   - sum(Detalle_Polizas.Abono ) 
                               When  Catalogo_de_Cuentas.Naturaleza= 'A' THEN   sum(Detalle_Polizas.Abono ) - sum(Detalle_Polizas.Cargo) END AS Saldo  ,
                              sum(Detalle_Polizas.Cargo) as Cargos,sum(Detalle_Polizas.Abono ) as Abonos
                                FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                        INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa  
                               WHERE   " & Where & " And polizas.ID_anio = " & Me.ComboAñoB.Text & " And Polizas.ID_mes = " & mes & " and Polizas.Id_Empresa= " & Me.Lstclientes2.SelectItem & "
                               GROUP BY Polizas.ID_anio,Polizas.ID_mes,Catalogo_de_Cuentas.Naturaleza   "
                Dim ds2 As DataSet = Eventos.Obtener_DS(sql2)
                If ds2.Tables(0).Rows.Count > 0 Then



                    Me.TablaMensual.Item(4 + a, j).Value = Me.TablaMensual.Item(4 + a - 1, j).Value + IIf(IsDBNull(ds2.Tables(0).Rows(0)(0)), 0, ds2.Tables(0).Rows(0)(0))
                    'Saldos
                    If Me.TablaMensual.Item(4 + a, j).Value < 0 Then
                        Me.TablaMensual.Item(4 + a, j).Style.BackColor = Color.Red
                    Else
                        '
                    End If
                    'Cargos
                    If Me.TablaCargos.Item(4 + a, j).Value < 0 Then
                        Me.TablaCargos.Item(4 + a, j).Style.BackColor = Color.Red
                    Else
                        '  Me.TablaCargos.Item(4 + a, j).Style.BackColor = Color.Transparent
                    End If
                    'Abonos
                    If Me.TablaAbonos.Item(4 + a, j).Value < 0 Then
                        Me.TablaAbonos.Item(4 + a, j).Style.BackColor = Color.Red
                    Else
                        'Me.TablaAbonos.Item(4 + a, j).Style.BackColor = Color.Transparent
                    End If
                    Me.TablaCargos.Item(4 + a, j).Value = IIf(IsDBNull(ds2.Tables(0).Rows(0)(1)), 0, ds2.Tables(0).Rows(0)(1))
                    Me.TablaAbonos.Item(4 + a, j).Value = IIf(IsDBNull(ds2.Tables(0).Rows(0)(2)), 0, ds2.Tables(0).Rows(0)(2))
                Else
                    Me.TablaMensual.Item(4 + a, j).Value = 0
                    Me.TablaCargos.Item(4 + a, j).Value = 0
                    Me.TablaAbonos.Item(4 + a, j).Value = 0
                End If


                '    Me.TablaMensual.Item(4 + a, j).Value = Me.TablaMensual.Item(4 + a - 1, j).Value + Consulta_saldos(Me.ComboAñoB.SelectedItem, mes, Where)
                ' Me.TablaMensual.Item(4, j).Value = Consulta_saldos(Me.ComboAñoB.SelectedItem, "01", Where)
            Next
            frm.Barra.Value = j

        Next

        frm.Close()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub TabControl1_TabIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.TabIndexChanged
        Dim Hoja As String = Me.TabControl1.SelectedTab.Name
        If Hoja = "TabPage2" Then ' Si cambia de pestaña
            Me.Lstclientes2.SelectText = ""
        Else

        End If
    End Sub

    Private Function Consulta_saldos(ByVal anio As String, ByVal mes As String, ByVal cuenta As String)
        Dim Saldo As Decimal = 0
        Dim sql As String = "SELECT CASE WHEN Catalogo_de_Cuentas.Naturaleza= 'D' then   sum(Detalle_Polizas.Cargo)   - sum(Detalle_Polizas.Abono ) 
                               When  Catalogo_de_Cuentas.Naturaleza= 'A' THEN   sum(Detalle_Polizas.Abono ) - sum(Detalle_Polizas.Cargo) END AS Saldo  
                               FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                        INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa 
                               WHERE   " & cuenta & " And polizas.ID_anio = " & anio & " And Polizas.ID_mes = " & mes & " and Polizas.Id_Empresa= " & Me.Lstclientes2.SelectItem & "
                               GROUP BY  Catalogo_de_Cuentas.Naturaleza   "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Saldo = ds.Tables(0).Rows(0)(0)
        Else
            Saldo = 0
        End If
        Return Saldo
    End Function

    Private Sub CmdExp_Click(sender As Object, e As EventArgs) Handles CmdExp.Click
        Dim Dato As Object
        If Me.Tabla.RowCount > 0 Then
            If Me.Tabla.Columns.Count > 256 Then
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("El rango de fechas sobrepasa las columnas de una hoja de excel, disminuye el rango...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                Exit Sub
            End If


            Dim excel As Microsoft.Office.Interop.Excel.Application = Eventos.NuevoExcel("Balanza", False)
            For col As Integer = 1 To Me.Tabla.Columns.Count - 1
                Eventos.EscribeExcel(excel, 1, col, Me.Tabla.Columns(col).HeaderText)
            Next
            For i As Integer = 0 To Me.Tabla.RowCount - 1
                For j As Integer = 1 To Me.Tabla.Columns.Count - 1
                    Try
                        If j = 1 Then
                            If Me.Tabla.Item(j, i).Value.ToString <> "" Then
                                Dato = Valor(Me.Tabla.Item(j, i).Value.ToString.Substring(0, 4) & "-" & Me.Tabla.Item(j, i).Value.ToString.Substring(4, 4) & "-" & Me.Tabla.Item(j, i).Value.ToString.Substring(8, 4) & "-" & Me.Tabla.Item(j, i).Value.ToString.Substring(12, 4))
                            End If
                        Else
                            Dato = Valor(Me.Tabla.Item(j, i).Value)

                        End If
                        Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 1, "Balanza")
                    Catch ex As Exception

                    End Try

                    ' Eventos.EscribeExcelHojas(excel, i + 1, j + 1, Valor(Me.Tabla.Item(j, i).Value), 1, "Balanza")
                Next

            Next
            Eventos.EscribeExcelHojas(excel, Me.Tabla.RowCount + 2, 3, "Totales:", 1, "Balanza")
            Eventos.EscribeExcelHojas(excel, Me.Tabla.RowCount + 2, 4, Me.lblsaldoanterior.Text, 1, "Balanza")
            Eventos.EscribeExcelHojas(excel, Me.Tabla.RowCount + 2, 5, Me.lbldebe.Text, 1, "Balanza")
            Eventos.EscribeExcelHojas(excel, Me.Tabla.RowCount + 2, 6, Me.lblhaber.Text, 1, "Balanza")
            Eventos.EscribeExcelHojas(excel, Me.Tabla.RowCount + 2, 7, Me.lblSaldofinal.Text, 1, "Balanza")
            Eventos.Mostrar_Excel(excel)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay datos para exportar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
    End Sub

    Private Sub CmdExportar_Click(sender As Object, e As EventArgs) Handles CmdExportar.Click
        Dim Dato As Object
        If Me.TablaMensual.RowCount > 0 Then
            If Me.TablaMensual.Columns.Count > 256 Then
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("El rango de fechas sobrepasa las columnas de una hoja de excel, disminuye el rango...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                Exit Sub
            End If

            Dim excel As Microsoft.Office.Interop.Excel.Application = Eventos.NuevoExcel("Saldos", False)
            'saldos
            For col As Integer = 1 To Me.TablaMensual.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaMensual.Columns(col).HeaderText, 1)
            Next
            'For i As Integer = 0 To Me.TablaMensual.RowCount - 1
            '    For j As Integer = 1 To Me.TablaMensual.Columns.Count - 1
            '        Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Me.TablaMensual.Item(j, i).Value), 1)
            '    Next

            'Next

            For i As Integer = 0 To Me.TablaMensual.RowCount - 1
                For j As Integer = 1 To Me.TablaMensual.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaMensual.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaMensual.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaMensual.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaMensual.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaMensual.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaMensual.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 1)
                    Dato = ""
                Next

            Next
            'Cargos
            For col As Integer = 1 To Me.TablaCargos.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaCargos.Columns(col).HeaderText, 2)
            Next

            For i As Integer = 0 To Me.TablaCargos.RowCount - 1
                For j As Integer = 1 To Me.TablaCargos.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaCargos.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaCargos.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaCargos.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaCargos.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaCargos.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaCargos.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 2)
                    Dato = ""
                Next

            Next


            'Abonos
            For col As Integer = 1 To Me.TablaAbonos.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaAbonos.Columns(col).HeaderText, 3)
            Next

            For i As Integer = 0 To Me.TablaAbonos.RowCount - 1
                For j As Integer = 1 To Me.TablaAbonos.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaAbonos.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaAbonos.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaAbonos.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaAbonos.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaAbonos.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaAbonos.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 3)
                    Dato = ""
                Next

            Next

            Eventos.Mostrar_Excel(excel)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay datos para exportar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
    End Sub

    Private Sub CmdQuitar_Click(sender As Object, e As EventArgs) Handles CmdQuitar.Click
        QuitacuentasceroBalanzaNormal()
        Total_saldo_final()
    End Sub

    Private Sub CmdCtasCeros_Click(sender As Object, e As EventArgs) Handles CmdCtasCeros.Click
        If Me.TablaCargos.Rows.Count > 0 Then
            QuitacuentasceroBalanzas()
        End If
    End Sub

    Private Sub CmbBuscarMasivo_Click(sender As Object, e As EventArgs) Handles CmbBuscarMasivo.Click
        Limpiar3()
        If Me.lstClientesMasivos.SelectText <> "" Then
            Eventos.Cuentas(Me.Anio2.Text, Me.LstCliente.SelectItem)
            Buscar_Balanzas(Me.Anio2.Text, Me.LstNivelBalanza.SelectItem)
            Inserta_finales()
            Me.CmdQuitarMasivo.PerformClick()
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
    Private Function Calcula_anteriores(ByVal Sql As String)
        Dim saldo As Decimal = 0
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0)(0) <> 0 Then
                saldo = ds.Tables(0).Rows(0)(0)
            Else
                saldo = ds.Tables(0).Rows(0)(1)
            End If
        Else
            saldo = 0
        End If
        Return saldo
    End Function

    Private Sub cmdLimpiarMasivo_Click(sender As Object, e As EventArgs) Handles cmdLimpiarMasivo.Click
        Limpiar3()
        Me.lstClientesMasivos.SelectText = ""
        Me.lstNivel2.SelectText = ""
    End Sub
    Private Sub Limpiar3()
        Me.Tablaenero.Rows.Clear()
        Me.TablaFebrero.Rows.Clear()
        Me.TablaMarzo.Rows.Clear()
        Me.TablaAbril.Rows.Clear()
        Me.TablaMayo.Rows.Clear()
        Me.TablaJunio.Rows.Clear()
        Me.TablaJulio.Rows.Clear()
        Me.TablaAgosto.Rows.Clear()
        Me.TablaSeptiembre.Rows.Clear()
        Me.TablaOctubre.Rows.Clear()
        Me.TablaNoviembre.Rows.Clear()
        Me.TablaDiciembre.Rows.Clear()
        Me.TablaAnual.Rows.Clear()
    End Sub

    Private Sub CmdQuitarMasivo_Click(sender As Object, e As EventArgs) Handles CmdQuitarMasivo.Click
        If Me.Tablaenero.Rows.Count > 0 Then
            QuitacuentasceroBalanzapormes()
        End If
    End Sub

    Private Sub CmdExportaanuales_Click(sender As Object, e As EventArgs) Handles CmdExportaanuales.Click
        Dim Dato As Object
        If Me.Tablaenero.RowCount > 0 Then
            If Me.Tablaenero.Columns.Count > 256 Then
                MessageBox.Show("El rango de fechas sobrepasa las columnas de una hoja de excel, disminuye el rango...", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            Dim excel As Microsoft.Office.Interop.Excel.Application = Eventos.NuevoExcel("Balanzas", False)
            'Enero
            For col As Integer = 1 To Me.Tablaenero.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.Tablaenero.Columns(col).HeaderText, 1)
            Next
            For i As Integer = 0 To Me.Tablaenero.RowCount - 1
                For j As Integer = 1 To Me.Tablaenero.Columns.Count - 1
                    If j = 1 Then
                        If Me.Tablaenero.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.Tablaenero.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.Tablaenero.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.Tablaenero.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.Tablaenero.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.Tablaenero.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 1)
                    Dato = ""
                Next

            Next
            'Febrero
            For col As Integer = 1 To Me.TablaFebrero.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaFebrero.Columns(col).HeaderText, 2)
            Next
            For i As Integer = 0 To Me.TablaFebrero.RowCount - 1
                For j As Integer = 1 To Me.TablaFebrero.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaFebrero.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaFebrero.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaFebrero.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaFebrero.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaFebrero.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaFebrero.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 2)
                    Dato = ""
                Next

            Next
            'Marzo
            For col As Integer = 1 To Me.TablaMarzo.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaMarzo.Columns(col).HeaderText, 3)
            Next
            For i As Integer = 0 To Me.TablaMarzo.RowCount - 1
                For j As Integer = 1 To Me.TablaMarzo.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaMarzo.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaMarzo.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaMarzo.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaMarzo.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaMarzo.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception
                                Dato = Valor(Me.TablaMarzo.Item(j, i).Value)
                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaMarzo.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 3)
                    Dato = ""
                Next

            Next

            'Abril
            For col As Integer = 1 To Me.TablaAbril.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaAbril.Columns(col).HeaderText, 4)
            Next
            For i As Integer = 0 To Me.TablaAbril.RowCount - 1
                For j As Integer = 1 To Me.TablaAbril.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaMarzo.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaAbril.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaAbril.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaAbril.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaAbril.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaAbril.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 4)
                    Dato = ""
                Next

            Next
            'Mayo
            For col As Integer = 1 To Me.TablaMayo.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaMayo.Columns(col).HeaderText, 5)
            Next
            For i As Integer = 0 To Me.TablaMayo.RowCount - 1
                For j As Integer = 1 To Me.TablaMayo.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaMayo.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaMayo.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaMayo.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaMayo.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaMayo.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaMayo.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 5)
                    Dato = ""
                Next

            Next
            'Junio
            For col As Integer = 1 To Me.TablaJunio.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaJunio.Columns(col).HeaderText, 6)
            Next
            For i As Integer = 0 To Me.TablaJunio.RowCount - 1
                For j As Integer = 1 To Me.TablaJunio.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaJunio.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaJunio.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaJunio.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaJunio.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaJunio.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaJunio.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 6)
                    Dato = ""
                Next

            Next
            'Julio
            For col As Integer = 1 To Me.TablaJulio.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaJulio.Columns(col).HeaderText, 7)
            Next
            For i As Integer = 0 To Me.TablaJulio.RowCount - 1
                For j As Integer = 1 To Me.TablaJulio.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaJulio.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaJulio.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaJulio.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaJulio.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaJulio.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaJulio.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 7)
                    Dato = ""
                Next

            Next
            'Agosto
            For col As Integer = 1 To Me.TablaAgosto.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaAgosto.Columns(col).HeaderText, 8)
            Next
            For i As Integer = 0 To Me.TablaAgosto.RowCount - 1
                For j As Integer = 1 To Me.TablaAgosto.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaAgosto.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaAgosto.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaAgosto.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaAgosto.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaAgosto.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaAgosto.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 8)
                    Dato = ""
                Next

            Next
            'Septiembre
            For col As Integer = 1 To Me.TablaSeptiembre.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaSeptiembre.Columns(col).HeaderText, 9)
            Next
            For i As Integer = 0 To Me.TablaSeptiembre.RowCount - 1
                For j As Integer = 1 To Me.TablaSeptiembre.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaSeptiembre.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaSeptiembre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaSeptiembre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaSeptiembre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaSeptiembre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaSeptiembre.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 9)
                    Dato = ""
                Next

            Next
            'Octubre
            For col As Integer = 1 To Me.TablaOctubre.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaOctubre.Columns(col).HeaderText, 10)
            Next
            For i As Integer = 0 To Me.TablaOctubre.RowCount - 1
                For j As Integer = 1 To Me.TablaOctubre.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaOctubre.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaOctubre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaOctubre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaOctubre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaOctubre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))
                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaOctubre.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 10)
                    Dato = ""
                Next

            Next
            'Noviembre
            For col As Integer = 1 To Me.TablaNoviembre.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaNoviembre.Columns(col).HeaderText, 11)
            Next
            For i As Integer = 0 To Me.TablaNoviembre.RowCount - 1
                For j As Integer = 1 To Me.TablaNoviembre.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaNoviembre.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaNoviembre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaNoviembre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaNoviembre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaNoviembre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaNoviembre.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 11)
                    Dato = ""
                Next

            Next
            'Diciembre
            For col As Integer = 1 To Me.TablaDiciembre.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaDiciembre.Columns(col).HeaderText, 12)
            Next
            For i As Integer = 0 To Me.TablaDiciembre.RowCount - 1
                For j As Integer = 1 To Me.TablaDiciembre.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaDiciembre.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaDiciembre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaDiciembre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaDiciembre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaDiciembre.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaDiciembre.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 12)
                    Dato = ""
                Next

            Next
            'Anual
            For col As Integer = 1 To Me.TablaAnual.Columns.Count - 1
                Eventos.EscribeExcelHojas(excel, 1, col, Me.TablaAnual.Columns(col).HeaderText, 13)
            Next
            For i As Integer = 0 To Me.TablaAnual.RowCount - 1
                For j As Integer = 1 To Me.TablaAnual.Columns.Count - 1
                    If j = 1 Then
                        If Me.TablaAnual.Item(j, i).Value.ToString <> "" Then
                            Try
                                Dato = Valor(Me.TablaAnual.Item(j, i).Value.ToString.PadRight(16, "0").Substring(0, 4) & "-" & Me.TablaAnual.Item(j, i).Value.ToString.PadRight(16, "0").Substring(4, 4) & "-" & Me.TablaAnual.Item(j, i).Value.ToString.PadRight(16, "0").Substring(8, 4) & "-" & Me.TablaAnual.Item(j, i).Value.ToString.PadRight(16, "0").Substring(12, 4))

                            Catch ex As Exception

                            End Try
                        End If
                    Else
                        Dato = Valor(Me.TablaAnual.Item(j, i).Value)
                        If j = 5 Or j = 4 Or j = 6 Then
                            If Dato.ToString() = "" Then
                                Dato = 0
                            End If
                        End If
                    End If
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 13)
                    Dato = ""
                Next

            Next
            Eventos.Mostrar_Excel(excel)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay datos para exportar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub
    Private Function Calcula_poliza()
        Dim poliza As String = ""

        poliza = Eventos.Num_poliza(Me.LstCliente.SelectItem, Tip(), Trim(Me.Dtfin.Value.ToString.Substring(6, 4)), "12", Tip())

        Return poliza
    End Function
    Private Function Tip()
        Dim tipo As String = ""
        Dim sql As String = "SELECT id_tipo_pol_sat FROM dbo.Tipos_Poliza_Sat WHERE Nombre = 'Diario' AND Id_Empresa = " & Me.LstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            tipo = ds.Tables(0).Rows(0)(0)
        End If
        Return tipo
    End Function

    Private Sub CmdPoliza_Click(sender As Object, e As EventArgs) Handles CmdPoliza.Click
        If Me.LstNivelBalanza.SelectText = "Nivel4" Then
            Dim poliza As String = Calcula_poliza()
            Dim posicion As Integer = InStr(1, poliza, "-", CompareMethod.Binary)
            Dim cuantos As Integer = Len(poliza) - Len(poliza.Substring(0, posicion))
            Dim consecutivo As Integer = Val(poliza.Substring(posicion, cuantos))
            Dim pol As String = Eventos.ObtenerValorDB("Polizas", "id_poliza", " concepto ='Poliza Cierre' and ID_anio = " & Trim(Me.Dtfin.Value.ToString.Substring(6, 4)) & " and ID_mes= 12 and Id_Empresa =" & Me.LstCliente.SelectItem & " ", True)
            If pol = " " Then


                Dim sql As String = ""
                sql &= " INSERT INTO dbo.Polizas "
                sql &= "("
                sql &= " 	ID_poliza,"
                sql &= "     ID_anio,"
                sql &= "     ID_mes, "
                sql &= "     ID_dia,        "
                sql &= "     Num_Pol,    "
                sql &= "     consecutivo,    "
                sql &= "     Id_Tipo_Pol_Sat,"
                sql &= "     Fecha,          "
                sql &= "     Concepto,      "
                sql &= "     Id_Empresa,     "
                sql &= "     No_Mov,        "
                sql &= "     Fecha_captura,  "
                sql &= "     Movto,         "
                sql &= "     Usuario ,Aplicar_Poliza          "
                sql &= " 	)               "
                sql &= " VALUES              "
                sql &= " 	(               "
                sql &= " 	'" & poliza & "'," '@id_poliza,         
                sql &= " 	" & Trim(Me.Dtfin.Value.ToString.Substring(6, 4)) & "," '@id_anio,           
                sql &= " 	'" & 12 & "'," '@id_mes,      
                sql &= " 	'" & 31 & "'," '@id_dia,  
                sql &= " 	'999999'," '@id_dia,     
                sql &= " 	" & consecutivo & "," '@consecutivo,       
                sql &= " 	" & Trim(Tip()) & "," '@id_tipo_poliza, 
                sql &= " 	" & Eventos.Sql_hoy("31/12/" & Trim(Me.Dtfin.Value.ToString.Substring(6, 4))) & "," '@fecha,             
                sql &= " 	'Poliza Cierre'," '@concepto,          
                sql &= " 	" & Me.LstCliente.SelectItem & "," '@Id_Empresa,        
                sql &= " 	'Cierre'," '@no_mov,            
                sql &= " 	" & Eventos.Sql_hoy() & "," '@fecha_captura,     
                sql &= " 	'A'," '@movto,             
                sql &= "  '" & Eventos.Usuario(Inicio.LblUsuario.Text) & "',1" '@usuario            
                sql &= " 	) "

                If Eventos.Comando_sql(sql) > 0 Then
                    Eventos.Insertar_usuariol("InsrtPolizCierre", sql)
                    Cierre_Detalle(poliza)
                End If
            Else
                If Eventos.Comando_sql("Delete from detalle_polizas where id_poliza ='" & pol.Trim() & "'") > 0 Then

                End If
                Cierre_Detalle(pol.Trim())
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado el nivel correcto para generar cierre este debe ser Nivel 4", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If


    End Sub
    Private Function Cuenta_de_Balanza(ByVal cuenta As String, ByVal nivel As String)
        Dim hacer As Boolean
        Dim ds As DataSet


        If Len(Trim(nivel)) = 19 Then
            hacer = True
        ElseIf Len(Trim(nivel)) = 14 Then
            ds = Eventos.Obtener_DS(" select * from Catalogo_de_Cuentas where Id_Empresa = " & Me.LstCliente.SelectItem & " and Nivel1 = '" & nivel.Substring(0, 4) & "' and Nivel2 = '" & nivel.Substring(5, 4) & "'   and Nivel3 = '" & nivel.Substring(10, 4) & "' and Nivel4 > '0000' ")
            If ds.Tables(0).Rows.Count > 0 Then
                hacer = False
            Else
                hacer = True
            End If

        ElseIf Len(Trim(nivel)) = 9 Then
            ds = Eventos.Obtener_DS(" select * from Catalogo_de_Cuentas where  Id_Empresa = " & Me.LstCliente.SelectItem & " and Nivel1 = '" & nivel.Substring(0, 4) & "' and Nivel2 = '" & nivel.Substring(5, 4) & "'   and Nivel3  > '0000' ")
            If ds.Tables(0).Rows.Count > 0 Then
                hacer = False
            Else
                hacer = True
            End If
        ElseIf Len(Trim(nivel)) = 4 Then
            ds = Eventos.Obtener_DS(" select * from Catalogo_de_Cuentas where  Id_Empresa = " & Me.LstCliente.SelectItem & " and  Nivel1 = '" & nivel.Substring(0, 4) & "' and Nivel2 >= '0000' ")
            If ds.Tables(0).Rows.Count > 0 Then
                hacer = False
            Else
                hacer = True
            End If
        End If

        Return hacer
    End Function
    Private Sub Cierre_Detalle(ByVal Poliza As String)
        Dim C, A As Decimal
        Dim Sql As String = ""
        Dim ITE As Integer = 1
        For i As Integer = 0 To Me.Tabla.Rows.Count - 1


            If AplicaCuenta(Trim(Me.Tabla.Item(Column1.Index, i).Value.ToString)) = True Then


                If Cuenta_de_Balanza(Me.Tabla.Item(1, i).Value.ToString, Me.Tabla.Item(0, i).Value.ToString) = True Then
                    Sql = "         INSERT INTO dbo.Detalle_Polizas "
                    Sql &= "(   "
                    Sql &= " ID_poliza ,      "
                    Sql &= " ID_item,       "
                    Sql &= " Cargo,          "
                    Sql &= " Abono,         "
                    Sql &= " Fecha_captura,  "
                    Sql &= " Movto,"
                    Sql &= " Cuenta, "
                    Sql &= " No_cheque  "
                    Sql &= " ) "
                    Sql &= " VALUES "
                    Sql &= "( "
                    Sql &= " '" & Poliza & "'	," '@id_poliza,     
                    Sql &= "" & ITE & "," '@id_item,   
                    If Trim(Me.Tabla.Item(2, i).Value.ToString) = "D" Then
                        ' IIf(Me.Tabla.Item(7, i).Value < 0, Me.Tabla.Item(7, i).Value * -1, Me.Tabla.Item(7, i).Value)
                        Sql &= "0," '@cargo,          
                        Sql &= "" & IIf(Me.Tabla.Item(7, i).Value < 0, Me.Tabla.Item(7, i).Value * -1, Me.Tabla.Item(7, i).Value) & "," '@abono, 
                        A += IIf(Me.Tabla.Item(7, i).Value < 0, Me.Tabla.Item(7, i).Value * -1, Me.Tabla.Item(7, i).Value)
                    Else
                        Sql &= "" & IIf(Me.Tabla.Item(7, i).Value < 0, Me.Tabla.Item(7, i).Value * -1, Me.Tabla.Item(7, i).Value) & "," '@cargo,          
                        Sql &= "0," '@abono,
                        C += IIf(Me.Tabla.Item(7, i).Value < 0, Me.Tabla.Item(7, i).Value * -1, Me.Tabla.Item(7, i).Value)
                    End If
                    Sql &= "" & Eventos.Sql_hoy() & "," '@fecha_captura, 
                    Sql &= " 'A'	," '@movto,         
                    Sql &= " " & Trim(Me.Tabla.Item(Column1.Index, i).Value.ToString) & "	," '@cuenta,        
                    Sql &= " ' '" '@no_cheque      
                    Sql &= " 	)"
                    If Eventos.Comando_sql(Sql) > 0 Then
                        Eventos.Insertar_usuariol("InsertarPolizDs", Sql)
                    End If
                    ITE += 1
                End If
            End If
        Next
        If IIf(C < 0, C * -1, C) > IIf(A < 0, A * -1, A) Then
            'insertar cuenta de perdida del Cliente
            Sql = "         INSERT INTO dbo.Detalle_Polizas "
            Sql &= "(   "
            Sql &= " ID_poliza,       "
            Sql &= " ID_item,       "
            Sql &= " Cargo,          "
            Sql &= " Abono,         "
            Sql &= " Fecha_captura,  "
            Sql &= " Movto,"
            Sql &= " Cuenta, "
            Sql &= " No_cheque  "
            Sql &= " ) "
            Sql &= " VALUES "
            Sql &= "( "
            Sql &= " '" & Poliza & "'	," '@id_poliza,     
            Sql &= "" & ITE & "," '@id_item,
            Sql &= "0," '@abono,   
            Sql &= "" & C - A & "," '@cargo,
            Sql &= "" & Eventos.Sql_hoy() & "," '@fecha_captura, 
            Sql &= " 'A'	," '@movto,         
            Sql &= " " & Ctaup(1, "Utilidad") & "	," '@cuenta,        
            Sql &= " ' '" '@no_cheque      
            Sql &= " 	)"
            If Eventos.Comando_sql(Sql) > 0 Then
                Eventos.Insertar_usuariol("InsertarPolizDs", Sql)
            End If
        ElseIf IIf(A < 0, A * -1, A) > IIf(C < 0, C * -1, C) Then
            'insertar cuenta de utilidad del Cliente

            Sql = "         INSERT INTO dbo.Detalle_Polizas "
            Sql &= "(   "
            Sql &= " ID_poliza ,      "
            Sql &= " ID_item,       "
            Sql &= " Cargo,          "
            Sql &= " Abono,         "
            Sql &= " Fecha_captura,  "
            Sql &= " Movto,"
            Sql &= " Cuenta, "
            Sql &= " No_cheque  "
            Sql &= " ) "
            Sql &= " VALUES "
            Sql &= "( "
            Sql &= " '" & Poliza & "'	," '@id_poliza,     
            Sql &= "" & ITE & "," '@id_item,   
            Sql &= "" & A - C & "," '@abono, 
            Sql &= "0," '@cargo,          
            Sql &= "" & Eventos.Sql_hoy() & "," '@fecha_captura, 
            Sql &= " 'A'	," '@movto,         
            Sql &= " " & Ctaup(1, "Perdida") & "	," '@cuenta,        
            Sql &= " ' '" '@no_cheque      
            Sql &= " 	)"
            If Eventos.Comando_sql(Sql) > 0 Then
                Eventos.Insertar_usuariol("InsertarPolizDs", Sql)
            End If
        End If
    End Sub
    Private Function Ctaup(ByVal tipo As Integer, ByVal Tipos As String) As String
        Dim sql As String = " select Cuenta FROM dbo.CuentasContablesFiscales where Id_Empresa =" & Me.LstCliente.SelectItem & " and Fiscal =" & tipo & " and Tipo = '" & Tipos & "' "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Ctaup = ds.Tables(0).Rows(0)(0).ToString.Replace("-", "")
        Else
            Ctaup = ""
        End If
        Return Ctaup
    End Function
    Private Function AplicaCuenta(ByVal Cta As String) As Boolean
        Dim Sql As String = "SELECT * FROM Catalogo_de_Cuentas WHERE Cuenta= " & Cta & " AND Clasificacion IN (SELECT ClasificacionCuentas.Clave  FROM ClasificacionCuentas WHERE Estado_de_Resultados =1 AND ID_empresa =1) and Id_Empresa = " & Me.LstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            AplicaCuenta = True
        Else
            AplicaCuenta = False
        End If
        Return AplicaCuenta
    End Function

    Private Sub GeneraBalanzaPDF()

        Dim DT As New DataTable
        With DT
            .Columns.Add("Cat")
            .Columns.Add("Cuenta")
            .Columns.Add("Naturaleza")
            .Columns.Add("Descripcion")
            .Columns.Add("Saldo_Anterior", Type.GetType("System.Decimal"))
            .Columns.Add("Debe", Type.GetType("System.Decimal"))
            .Columns.Add("Haber", Type.GetType("System.Decimal"))
            .Columns.Add("Saldo_final", Type.GetType("System.Decimal"))
        End With
        For Each dr As DataGridViewRow In Me.Tabla.Rows
            DT.Rows.Add(dr.Cells(Column1.Index).Value, dr.Cells(Column4.Index).Value, dr.Cells(Column8.Index).Value, dr.Cells(Column5.Index).Value, dr.Cells(Column6.Index).Value * 1, dr.Cells(Column2.Index).Value * 1, dr.Cells(Column7.Index).Value * 1, dr.Cells(Column3.Index).Value * 1)
        Next

        Dim Cr As New BalanzaGeneral
        Cr.SetDataSource(DT)
        Dim Reporte As New ReporteBalanzaGeneral
        Reporte.CrvBalanza.ReportSource = Cr

        Dim FechaD As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim Rptinv As New CrystalDecisions.Shared.ParameterValues()
        FechaD.Value = Me.DtInicio.Value.ToString.Substring(0, 10)
        Rptinv.Add(FechaD)
        Cr.DataDefinition.ParameterFields("FechaD").ApplyCurrentValues(Rptinv)

        Dim FechaF As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim ff As New CrystalDecisions.Shared.ParameterValues()
        FechaF.Value = Me.Dtfin.Value.ToString.Substring(0, 10)
        ff.Add(FechaF)
        Cr.DataDefinition.ParameterFields("FechaF").ApplyCurrentValues(ff)
        Dim rfc As String = Eventos.ObtenerValorDB("Empresa", "RFC", " Id_Empresa = " & Me.LstCliente.SelectItem & "", True)
        Dim Cliente As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim Cl As New CrystalDecisions.Shared.ParameterValues()
        Cliente.Value = Me.LstCliente.SelectText & vbCr & rfc
        Cl.Add(Cliente)
        Cr.DataDefinition.ParameterFields("Cliente").ApplyCurrentValues(Cl)
        Dim Clt As DataSet = Eventos.Obtener_DS("select * from Empresa where Id_Empresa = " & Me.LstCliente.SelectItem & " ")


        Cliente.Value = Clt.Tables(0).Rows(0)("Direccion")
        Cl.Add(Cliente)
        Cr.DataDefinition.ParameterFields("Direccion").ApplyCurrentValues(Cl)




        ' Reporte.ShowDialog()

        Dim Archivo As String = Application.StartupPath & "\Balanza\PDF\Balanza" & rfc & ".pdf"
        If Directory.Exists(Application.StartupPath & "\Balanza\PDF") = False Then
            ' si no existe la carpeta se crea
            Directory.CreateDirectory(Application.StartupPath & "\Balanza\PDF")
        End If
        If Directory.Exists(Archivo) Then
            My.Computer.FileSystem.DeleteFile(Archivo)
        End If
        Cr.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Archivo)
        Process.Start(Archivo)

    End Sub

    Private Sub CmdPdf_Click(sender As Object, e As EventArgs) Handles CmdPdf.Click
        GeneraBalanzaPDF()
    End Sub



    Private Function Total_haberB(ByVal TA As DataGridView)

        Dim total As Decimal = 0
        For i As Integer = 0 To TA.RowCount - 1
            If Len(Trim(TA.Item(0, i).Value.ToString)) = 4 Then
                total = total + TA.Item(6, i).Value

            End If
        Next
        Return total
    End Function
    Private Function Total_debeB(ByVal TA As DataGridView)
        Dim total As Decimal = 0
        For i As Integer = 0 To TA.RowCount - 1
            If Len(Trim(TA.Item(0, i).Value.ToString)) = 4 Then
                total = total + TA.Item(5, i).Value

            End If
        Next
        Return total
    End Function
    Private Function Total_saldo_final(ByVal TA As DataGridView)
        Dim total As Decimal = 0
        For i As Integer = 0 To TA.RowCount - 1
            If Len(Trim(TA.Item(0, i).Value.ToString)) = 4 Then

                total = total + TA.Item(7, i).Value

            End If
        Next
        Return total
    End Function

    Private Function TotalSaldoanterior(ByVal TA As DataGridView)
        Dim total As Decimal = 0
        Dim A As Decimal = 0
        Dim B As Decimal = 0
        For i As Integer = 0 To TA.RowCount - 1
            If Len(Trim(TA.Item(0, i).Value.ToString)) = 4 Then
                If Trim(TA.Item(2, i).Value.ToString) = "A" Then
                    A = A + TA.Item(4, i).Value
                ElseIf Trim(TA.Item(2, i).Value.ToString) = "D" Then

                    B = B + TA.Item(4, i).Value
                End If
            End If
        Next
        total = A - (B * -1)
        Return total

    End Function
    Private Sub Inserta_finales()
        Me.Tablaenero.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(Tablaenero), Total_debeB(Tablaenero), Total_haberB(Tablaenero), Total_saldo_final(Tablaenero))
        Me.TablaFebrero.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaFebrero), Total_debeB(TablaFebrero), Total_haberB(TablaFebrero), Total_saldo_final(TablaFebrero))
        Me.TablaMarzo.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaMarzo), Total_debeB(TablaMarzo), Total_haberB(TablaMarzo), Total_saldo_final(TablaMarzo))
        Me.TablaAbril.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaAbril), Total_debeB(TablaAbril), Total_haberB(TablaAbril), Total_saldo_final(TablaAbril))
        Me.TablaMayo.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaMayo), Total_debeB(TablaMayo), Total_haberB(TablaMayo), Total_saldo_final(TablaMayo))
        Me.TablaJunio.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaJunio), Total_debeB(TablaJunio), Total_haberB(TablaJunio), Total_saldo_final(TablaJunio))
        Me.TablaJulio.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaJulio), Total_debeB(TablaJulio), Total_haberB(TablaJulio), Total_saldo_final(TablaJulio))
        Me.TablaAgosto.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaAgosto), Total_debeB(TablaAgosto), Total_haberB(TablaAgosto), Total_saldo_final(TablaAgosto))
        Me.TablaSeptiembre.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaSeptiembre), Total_debeB(TablaSeptiembre), Total_haberB(TablaSeptiembre), Total_saldo_final(TablaSeptiembre))
        Me.TablaOctubre.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaOctubre), Total_debeB(TablaOctubre), Total_haberB(TablaOctubre), Total_saldo_final(TablaOctubre))
        Me.TablaNoviembre.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaNoviembre), Total_debeB(TablaNoviembre), Total_haberB(TablaNoviembre), Total_saldo_final(TablaNoviembre))
        Me.TablaDiciembre.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaDiciembre), Total_debeB(TablaDiciembre), Total_haberB(TablaDiciembre), Total_saldo_final(TablaDiciembre))
        Me.TablaAnual.Rows.Add("", "", "", "Totales:", TotalSaldoanterior(TablaAnual), Total_debeB(TablaAnual), Total_haberB(TablaAnual), Total_saldo_final(TablaAnual))

    End Sub


    Private Sub SegundoPlano_DoWork(sender As Object, e As DoWorkEventArgs) Handles SegundoPlano.DoWork
        If Me.LstCliente.SelectText <> "" Then
            My.Forms.Inicio.txtServerDB.Text = serV
            Buscar(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), Me.LstNivelBalanza.SelectItem, Fila)
            TotalSaldoanterior()
            Total_haber()
            Total_debe()
            Total_saldo_final()
            Me.CmdQuitar.PerformClick()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
End Class
