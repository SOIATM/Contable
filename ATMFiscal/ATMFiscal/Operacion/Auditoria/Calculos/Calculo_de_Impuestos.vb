Imports System.ComponentModel
Imports Telerik.WinControls

Public Class Calculo_de_Impuestos
    Dim anio = Str(DateTime.Now.Year)
    Dim m = Now.Date.Month.ToString
    Dim Negrita_verde As New DataGridViewCellStyle
    Dim Negrita_morado As New DataGridViewCellStyle
    Dim Tasa As Decimal = 0.16
    Dim ISR As Decimal = 0.1
    Dim Polizas As New List(Of Buscar_Errores_Cruces.Polizas_Cruces)
    Dim contador As Integer = 1
    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Private Sub Calculo_de_Impuestos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_Listas()
        Eventos.DiseñoTablaEnca(Me.TablaErrores)
        Eventos.DiseñoTablaEnca(Me.TablaImportar)
        Dim i As Integer
        For i = DateTime.Now.Year To DateTime.Now.Year - 5 Step -1
            If i >= 2004 Then
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

    End Sub
    Private Sub Cargar_Listas()
        Me.lstCliente.Cargar(" SELECT Id_Empresa, Razon_social FROM Empresa ")
        Me.lstCliente.SelectItem = 1

    End Sub
    ''' <summary>
    ''' Este evento carga los registros contenidos en la base de datos y en caso de que no existan crea una plantilla a partir de la info cargada previamente el la DB
    ''' </summary>
    ''' <param name="Anio"> Paramatero Obligatorio en formato entero </param>
    ''' <param name="Cliente"> Parametro Oblicagorio representa la empresa </param>
    Private Sub Crear_Filas(ByVal Anio As Integer, ByVal Cliente As Integer)
        Negrita_verde.Font = New Font(Me.TablaImportar.Font, FontStyle.Bold)
        Negrita_verde.BackColor = Color.LawnGreen
        Negrita_morado.Font = New Font(Me.TablaImportar.Font, FontStyle.Bold)
        Negrita_morado.BackColor = Color.Plum
        Dim sql As String = " SELECT Descripcion, Cuenta, Naturaleza, Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre,Diciembre, Anio, Operacion
                               FROM     Calculo_Impuestos 
                               WHERE  Id_Empresa = " & Cliente & " and Anio  =" & Anio & "  ORDER BY Id_Calculo  "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then



            Dim Cuentas As DataSet = Eventos.Obtener_DS(" SELECT  rtrim(Nivel1)  + '-'+  rtrim(Nivel2) + '-'+  rtrim(Nivel3) + '-'+  rtrim(Nivel4) AS Alias FROM Catalogo_de_Cuentas WHERE   Id_Empresa = " & Me.lstCliente.SelectItem & " order by Alias ")
            If Cuentas.Tables(0).Rows.Count > 0 Then
                If Me.Cta.Items.Count = 0 Then
                    For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
                        Me.Cta.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Alias")))
                    Next
                End If
            End If

            Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.TablaImportar.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(i)
                Me.TablaImportar.Item(Descrip.Index, i).Value = ds.Tables(0).Rows(i)("Descripcion")
                Try
                    If ds.Tables(0).Rows(i)("Cuenta") <> "" Then
                        Fila.Cells(Cta.Index).Value = Me.Cta.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("Cuenta")), Me.Cta))
                    End If
                Catch ex As Exception

                End Try
                If Trim(ds.Tables(0).Rows(i)("Naturaleza")) = "Diferencia" Then
                    'Fila.DefaultCellStyle.BackColor = Color.LawnGreen
                    Fila.DefaultCellStyle = Negrita_verde
                End If

                If Trim(ds.Tables(0).Rows(i)("Naturaleza")) = "" And Trim(ds.Tables(0).Rows(i)("Cuenta")) = "" And Trim(ds.Tables(0).Rows(i)("Descripcion")) <> "" Then
                    ' Fila.DefaultCellStyle.BackColor = Color.Plum

                    Fila.DefaultCellStyle = Negrita_morado
                End If
                Me.TablaImportar.Item(Nat.Index, i).Value = ds.Tables(0).Rows(i)("Naturaleza")
                Me.TablaImportar.Item(Enero.Index, i).Value = ds.Tables(0).Rows(i)("Enero")
                Me.TablaImportar.Item(Febrero.Index, i).Value = ds.Tables(0).Rows(i)("Febrero")
                Me.TablaImportar.Item(Marzo.Index, i).Value = ds.Tables(0).Rows(i)("Marzo")
                Me.TablaImportar.Item(Abril.Index, i).Value = ds.Tables(0).Rows(i)("Abril")
                Me.TablaImportar.Item(Mayo.Index, i).Value = ds.Tables(0).Rows(i)("Mayo")
                Me.TablaImportar.Item(Junio.Index, i).Value = ds.Tables(0).Rows(i)("Junio")
                Me.TablaImportar.Item(Julio.Index, i).Value = ds.Tables(0).Rows(i)("Julio")
                Me.TablaImportar.Item(Agosto.Index, i).Value = ds.Tables(0).Rows(i)("Agosto")
                Me.TablaImportar.Item(Septiembre.Index, i).Value = ds.Tables(0).Rows(i)("Septiembre")
                Me.TablaImportar.Item(Octubre.Index, i).Value = ds.Tables(0).Rows(i)("Octubre")
                Me.TablaImportar.Item(Noviembre.Index, i).Value = ds.Tables(0).Rows(i)("Noviembre")
                Me.TablaImportar.Item(Noviembre.Index, i).Value = ds.Tables(0).Rows(i)("Noviembre")
                Me.TablaImportar.Item(Diciembre.Index, i).Value = ds.Tables(0).Rows(i)("Diciembre")
                Me.TablaImportar.Item(Ti.Index, i).Value = ds.Tables(0).Rows(i)("Operacion")
                frm.Barra.Value = i
            Next
            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow




        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            If RadMessageBox.Show("No existe plantilla del Cliente " & Me.lstCliente.SelectText & " para el año " & Anio & " deseas crearla?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then


                sql = " SELECT Descripcion, Cuenta, Naturaleza, Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre,Diciembre, Anio, Operacion
                               FROM     Calculo_Impuestos 
                               WHERE  Id_Empresa =  0 and Anio  = 2019  ORDER BY Id_Calculo  "
                ds = Eventos.Obtener_DS(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim Cuentas As DataSet = Eventos.Obtener_DS(" SELECT  rtrim(Nivel1)  + '-'+  rtrim(Nivel2) + '-'+  rtrim(Nivel3) + '-'+  rtrim(Nivel4) AS Alias FROM Catalogo_de_Cuentas WHERE   Id_Empresa = " & Me.lstCliente.SelectItem & " order by Alias ")
                    If Cuentas.Tables(0).Rows.Count > 0 Then
                        If Me.Cta.Items.Count = 0 Then
                            For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
                                Me.Cta.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Alias")))
                            Next
                        End If
                    End If

                    Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(i)
                        Me.TablaImportar.Item(Descrip.Index, i).Value = ds.Tables(0).Rows(i)("Descripcion")
                        Try
                            If ds.Tables(0).Rows(i)("Cuenta") <> "" Then
                                Fila.Cells(Cta.Index).Value = Me.Cta.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("Cuenta")), Me.Cta))
                            End If
                        Catch ex As Exception

                        End Try
                        If Trim(ds.Tables(0).Rows(i)("Naturaleza")) = "Diferencia" Then

                            Fila.DefaultCellStyle = Negrita_verde
                        End If

                        If Trim(ds.Tables(0).Rows(i)("Naturaleza")) = "" And Trim(ds.Tables(0).Rows(i)("Cuenta")) = "" And Trim(ds.Tables(0).Rows(i)("Descripcion")) <> "" Then

                            Fila.DefaultCellStyle = Negrita_morado
                        End If
                        Me.TablaImportar.Item(Nat.Index, i).Value = ds.Tables(0).Rows(i)("Naturaleza")
                        Me.TablaImportar.Item(Enero.Index, i).Value = ds.Tables(0).Rows(i)("Enero")
                        Me.TablaImportar.Item(Febrero.Index, i).Value = ds.Tables(0).Rows(i)("Febrero")
                        Me.TablaImportar.Item(Marzo.Index, i).Value = ds.Tables(0).Rows(i)("Marzo")
                        Me.TablaImportar.Item(Abril.Index, i).Value = ds.Tables(0).Rows(i)("Abril")
                        Me.TablaImportar.Item(Mayo.Index, i).Value = ds.Tables(0).Rows(i)("Mayo")
                        Me.TablaImportar.Item(Junio.Index, i).Value = ds.Tables(0).Rows(i)("Junio")
                        Me.TablaImportar.Item(Julio.Index, i).Value = ds.Tables(0).Rows(i)("Julio")
                        Me.TablaImportar.Item(Agosto.Index, i).Value = ds.Tables(0).Rows(i)("Agosto")
                        Me.TablaImportar.Item(Septiembre.Index, i).Value = ds.Tables(0).Rows(i)("Septiembre")
                        Me.TablaImportar.Item(Octubre.Index, i).Value = ds.Tables(0).Rows(i)("Octubre")
                        Me.TablaImportar.Item(Noviembre.Index, i).Value = ds.Tables(0).Rows(i)("Noviembre")
                        Me.TablaImportar.Item(Noviembre.Index, i).Value = ds.Tables(0).Rows(i)("Noviembre")
                        Me.TablaImportar.Item(Diciembre.Index, i).Value = ds.Tables(0).Rows(i)("Diciembre")
                        Me.TablaImportar.Item(Ti.Index, i).Value = ds.Tables(0).Rows(i)("Operacion")
                    Next
                    Guarda_Ctas()
                End If
            Else
                Exit Sub
            End If
        End If


    End Sub
    ''' <summary>
    ''' Obtiene el indice del registro que deseamos buscar para que el sistema lo muestre en la columna correspondiente
    ''' </summary>
    ''' <param name="valor"> parametro a uscar en el DataSource </param>
    ''' <param name="Col"> Representa el Datasource</param>
    ''' <returns></returns>
    Private Function Obtener_Index(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
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
    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        If Me.TablaImportar.RowCount > 0 Then
            Me.TablaImportar.Rows.Insert(Me.TablaImportar.CurrentRow.Index + 1, "", "", "", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", Me.TablaImportar.Item(Ti.Index, Me.TablaImportar.CurrentRow.Index).Value.ToString)
        End If
    End Sub
    ''' <summary>
    ''' Esta clase permite calcular la informacion de cada cuenta de acuerdo a sus cargos y abonos.
    ''' </summary>
    ''' <param name="Periodo"></param>
    ''' <param name="Id_Empresa"></param>
    ''' <param name="Cta"> Es la cuenta que se esta auditando </param>
    ''' <param name="Fila">  </param>
    ''' <param name="Col"> Define el Mes de consulta </param>
    ''' <param name="Nat"> La Nat es C para Cargos, A para Abonos y SF para Saldo Final </param>
    Private Sub Calcular(ByVal Periodo As String, ByVal Id_Empresa As Integer, ByVal Cta As String, ByVal Fila As Integer, ByVal Col As Integer, ByVal Nat As String)
        Dim Saldo_Inicial As Decimal = 0
        Dim where As String = ""
        Cta = Trim(Cta.Replace("-", ""))
        Cta = Cta.TrimEnd("0")
        Col += Convert.ToInt32(Me.ComboMes.Text) - 1
        Select Case Len(Cta)
            Case 3, 2, 1
                Cta = Cta.PadRight(4, "0")
                where = "   Nivel1 = '" & Trim(Cta) & "' and Nivel2 >= '0000' "
            Case 4

                where = "   Nivel1 = '" & Trim(Cta) & "' and Nivel2 >= '0000' "
            Case 7, 6, 5
                Cta = Cta.PadRight(8, "0")
                where = "  Nivel1 = '" & Cta.Substring(0, 4) & "' and Nivel2   = '" & Cta.Substring(4, 4) & "' and Nivel3 >= '0000' "
            Case 8
                where = "  Nivel1 = '" & Cta.Substring(0, 4) & "' and Nivel2   = '" & Cta.Substring(4, 4) & "' and Nivel3 >= '0000' "
            Case 11, 10, 9
                Cta = Cta.PadRight(12, "0")
                where = "  Nivel1 = '" & Cta.Substring(0, 4) & "' and Nivel2   = '" & Cta.Substring(4, 4) & "' and Nivel3='" & Cta.Substring(8, 4) & "' and Nivel4 >= '0000' "
            Case 12
                where = "  Nivel1 = '" & Cta.Substring(0, 4) & "' and Nivel2   = '" & Cta.Substring(4, 4) & "' and Nivel3='" & Cta.Substring(8, 4) & "' and Nivel4 >= '0000' "
            Case 15, 14, 13
                Cta = Cta.PadRight(16, "0")
                where = "   Nivel1 = '" & Cta.Substring(0, 4) & "' and Nivel2   = '" & Cta.Substring(4, 4) & "' and Nivel3='" & Cta.Substring(8, 4) & "' and Nivel4 ='" & Cta.Substring(12, 4) & "'  "
            Case 16
                where = "   Nivel1 = '" & Cta.Substring(0, 4) & "' and Nivel2   = '" & Cta.Substring(4, 4) & "' and Nivel3='" & Cta.Substring(8, 4) & "' and Nivel4 ='" & Cta.Substring(12, 4) & "'  "
        End Select


        Nat = Trim(Nat)
        Dim Tipo As String = ""
        Dim Campo As String = ""
        If Nat = "C" Then
            Tipo = "Detalle_Polizas.Cargo <> 0"
            Campo = "Detalle_Polizas.Cargo"
            Saldo_Inicial = 0
        ElseIf Nat = "A" Then
            Tipo = "Detalle_Polizas.Abono <> 0"
            Campo = "Detalle_Polizas.Abono"
            Saldo_Inicial = 0
        ElseIf Nat = "SF" Then
            Tipo = "Detalle_Polizas.Cargo <> 0 or Detalle_Polizas.Abono <> 0"
            Campo = "  Detalle_Polizas.Cargo - Detalle_Polizas.Abono   "
            'Campo = " CASE WHEN Catalogo_de_Cuentas.Naturaleza = 'D' THEN  Detalle_Polizas.Cargo - Detalle_Polizas.Abono WHEN Catalogo_de_Cuentas.Naturaleza ='A' THEN Detalle_Polizas.Abono  - Detalle_Polizas.Cargo END  "
            Cta = Cta.PadRight(16, "0")
            Cta = Replace(Cta, "-", "")

            Saldo_Inicial = Eventos.Calcula_Saldos_Iniciales(Cta.Substring(0, 16), Me.LstAnio.Text.Trim() - 1, Me.lstCliente.SelectItem)
            ' Saldo_Inicial = Eventos.Calcula_Saldos_InicialesB(Cta.Substring(0,4)&"000000000000", Me.LstAnio.Text.Trim() - 1, Me.lstCliente.SelectItem, "01/01" & Me.LstAnio.Text.Trim())
        Else
            Exit Sub
        End If
        Dim Simbolo As String = ""

        Dim Mes() As String

        For i As Integer = Convert.ToInt32(Me.ComboMes.Text) To Convert.ToInt32(Me.ComboMes2.Text)
            If Mes Is Nothing Then
                ReDim Preserve Mes(0)
                Mes(0) = IIf(Len(i.ToString()) = 1, "0" & i, i)
            Else
                ReDim Preserve Mes(UBound(Mes) + 1)
                Mes(UBound(Mes)) = IIf(Len(i.ToString()) = 1, "0" & i, i)
            End If

        Next

        Try


            For Each MS As String In Mes


                Dim Sql As String = "SELECT sum (total) AS total FROM (SELECT        SUM(" & Campo & ") AS Total, Detalle_Polizas.cuenta, Catalogo_de_Cuentas.Descripcion
                                     FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                     INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                     INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                     INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                     WHERE (" & Tipo & ") 
                                     GROUP BY Polizas.Id_Empresa, Catalogo_de_Cuentas.Id_Empresa,Detalle_Polizas.cuenta, Catalogo_de_Cuentas.Descripcion, Polizas.Id_Anio, Polizas.Id_Mes,Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Nivel2,Catalogo_de_Cuentas.Nivel3,Catalogo_de_Cuentas.Nivel4   
                                     HAVING " & Periodo & "  AND ( Polizas.Id_Mes =" & MS & ")  AND " & where & " and Polizas.Id_Empresa =" & Id_Empresa & "  )As tabla  "

                Sql &= " SELECT DISTINCT  Poliza FROM (SELECT        Polizas.ID_poliza AS Poliza, Detalle_Polizas.cuenta, Catalogo_de_Cuentas.Descripcion
                        FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                        INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                        WHERE (" & Tipo & ") 
                        GROUP BY Polizas.Id_Empresa, polizas.ID_poliza, Catalogo_de_Cuentas .Id_Empresa,Detalle_Polizas.cuenta, Catalogo_de_Cuentas.Descripcion, 
                        Polizas.Id_Anio, Polizas.Id_Mes,Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Nivel2,Catalogo_de_Cuentas.Nivel3,Catalogo_de_Cuentas.Nivel4   
                        HAVING " & Periodo & "  AND ( Polizas.Id_Mes =" & MS & ")  AND " & where & " and Polizas.Id_Empresa =" & Id_Empresa & " )As tabla  "

                'Dim Sql As String = "Cruces '" & Campo & "'," & Id_Empresa & ",'" & Tipo & "','" & Periodo & "','" & MS & "','" & where.Replace("'", "") & "'"

                Dim ds As DataSet = Eventos.Obtener_DS(Sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Nat = "SF" Then
                        Saldo_Inicial += IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
                        Me.TablaImportar.Item(Col, Fila).Value = Saldo_Inicial
                    Else
                        Me.TablaImportar.Item(Col, Fila).Value = Saldo_Inicial + IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
                    End If

                    If ds.Tables(1).Rows.Count > 0 Then
                        Try
                            For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                                If Not Polizas.Exists(Function(x) x.Poliza = Trim(ds.Tables(1).Rows(i)(0)).ToString And x.Tipo = Trim(Me.TablaImportar.Item(Ti.Index, Fila).Value)) Then
                                    Polizas.Add(New Buscar_Errores_Cruces.Polizas_Cruces() With {.Poliza = Trim(ds.Tables(1).Rows(i)(0)).ToString, .Tipo = Trim(Me.TablaImportar.Item(Ti.Index, Fila).Value), .Int = contador})
                                    contador += 1
                                End If

                            Next

                        Catch ex As Exception

                        End Try
                    End If
                Else
                    Me.TablaImportar.Item(Col, Fila).Value = Saldo_Inicial + 0
                End If

                Col += 1
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmdCalcular_Click(sender As Object, e As EventArgs) Handles CmdCalcular.Click
        Call Hacer(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem)

    End Sub
    Private Sub CalculaImpuesto(ByVal Mes As Integer, ByVal M As String)

        Dim Valores(63) As Decimal

        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            Try


                Select Case Trim(Me.TablaImportar.Item(Ti.Index, i).Value.ToString)
                    Case "1.1"
                        Valores(0) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "2.1"
                        Valores(1) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)

                    Case "3.0"
                        Valores(2) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "3.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(((Valores(0) * Tasa) + (Valores(1) * Tasa)) - Valores(2), 2) ' Formula

                    Case "3.3"
                        Valores(3) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "3.4"
                        Valores(4) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "3.5"
                        Me.TablaImportar.Item(Mes, i).Value = Valores(3) - Valores(4) ' Formula CARGOS Empresa

                    Case "4.1"
                        Valores(5) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "5"
                        Valores(6) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "5.1"
                        Me.TablaImportar.Item(Mes, i).Value = Valores(5) - Valores(6) ' Formula ABONOS Empresa

                    Case "6.1"
                        Valores(7) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "7"
                        Valores(8) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "7.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round((IIf(Valores(7) < 0, Valores(7) * -1, Valores(7))) - Valores(8), 2) ' Formula Saldos finales Empresa 

                    Case "8.1"
                        Valores(9) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "9"
                        Valores(10) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)

                    Case "9.1"
                        Me.TablaImportar.Item(Mes, i).Value = Valores(9) - Valores(10) ' Formula

                    Case "10.1"
                        Valores(11) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "11"
                        Valores(12) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "11.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(Valores(11) - Valores(12), 2) ' Formula

                    Case "12.1"
                        Valores(13) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "13"
                        Valores(14) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "13.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(Valores(13) - Valores(14), 2) ' Formula


                    Case "14.1"
                        Valores(15) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "15"
                        Valores(16) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "15.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(Valores(15) - Valores(16), 2) ' Formula

                    Case "16.1"
                        Valores(17) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "17"
                        Valores(18) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "17.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(Valores(17) - Valores(18), 2) ' Formula


                    Case "18.1"
                        Valores(19) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "19"
                        Valores(20) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "19.1"
                        Me.TablaImportar.Item(Mes, i).Value = (IIf(Valores(19) < 0, Valores(7) * -1, Valores(19))) - Valores(20) ' Formula Saldos finales Empresa 
                    Case "20"
                        Valores(21) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "20.0"
                        Valores(22) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "20.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round((Valores(21) * Tasa) - Valores(22), 2)' Formula

                    Case "21"
                        Valores(23) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "21.0"
                        Valores(24) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "21.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round((Valores(23) * Tasa) - Valores(24), 2) ' Formula

                    Case "22"
                        Valores(25) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "22.0"
                        Valores(26) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "22.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round((Valores(25) * Tasa) - Valores(26), 2) ' Formula

                    Case "23.1"
                        Valores(27) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "24.1"
                        Valores(28) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "25.1"
                        Valores(29) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "26"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(((Valores(27) * Tasa) + (Valores(28) * Tasa)) - Valores(29), 2) ' Formula




                    Case "27.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("27", Mes)
                    Case "28.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("28", Mes)
                    Case "29.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("29", Mes)
                    Case "30.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("30", Mes)
                    Case "31.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("31", Mes)
                    Case "32.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("32", Mes)
                    Case "33.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("33", Mes)
                    Case "34.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("34", Mes)
                    Case "35.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("35", Mes)
                    Case "36.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("36", Mes)
                    Case "37.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("37", Mes)
                    Case "38.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("38", Mes)
                    Case "39.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("39", Mes)



                    Case "40.1"
                        Valores(30) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "41.1"
                        Valores(31) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "42"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(((Valores(30) * Tasa)) - Valores(31), 2) ' Formula


                    Case "43"
                        Valores(32) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "43.0"
                        Valores(33) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "43.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round((Valores(32) * Tasa) - Valores(33), 2) ' Formula


                    Case "44.1"
                        Valores(34) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "45.0"
                        Valores(35) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "45.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(((Valores(34) * Tasa)) - Valores(35), 2) ' Formula

                    Case "46.1"
                        Valores(36) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "47"
                        Valores(37) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "47.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(((Valores(36) * Tasa)) - Valores(37), 2) ' Formula

                    Case "48.1"
                        Valores(38) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "49"
                        Valores(39) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "49.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(((Valores(38) * Tasa)) - Valores(39), 2) ' Formula


                    Case "48.1"
                        Valores(40) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "49"
                        Valores(41) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "49.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(((Valores(40) * Tasa)) - Valores(41), 2) ' Formula


                    Case "50"
                        Valores(42) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "50.0"
                        Valores(43) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "50.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(((Valores(42) * Tasa)) - Valores(43), 2) ' Formula

                    Case "51.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("51", Mes)
                        Valores(44) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "53.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("53", Mes)
                        Valores(45) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "54"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(((Valores(44) * Tasa)) - Valores(45), 2) ' Formula


                    Case "55"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(Valores(45) - IVA_Acreditable_Real(M), 2) '-  Fromula para calculo de ( C 1190-0001-0007  C 1190-0001-0006)

                        Valores(46) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "55.0"
                        Valores(47) = IvaAcreditableCruces(Me.lstCliente.SelectItem, Me.LstAnio.Text.Trim(), Mes - 3) 'IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "55.01"
                        Me.TablaImportar.Item(Mes, i).Value = Valores(47) '  IVA Acreditable en Declaracion

                        Valores(48) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "55.1"
                        Me.TablaImportar.Item(Mes, i).Value = Valores(46) - Valores(47)   ' Formula

                        'Me.TablaImportar.Item(Mes, i).Value = Valores(46) + Valores(47) - Valores(48) ' Formula
                    Case "56"
                        Valores(49) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "56.0"
                        Valores(50) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "56.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(((Valores(49) * ISR)) - Valores(50), 2) ' Formula

                    Case "57"
                        Valores(51) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "57.0"
                        Valores(52) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "57.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round(((Valores(51) * ISR)) - Valores(52), 2) ' Formula

                    Case "58"
                        Valores(53) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "58.0"
                        Valores(54) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "58.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round((((Valores(53) * Tasa) / 3) * 2) - Valores(54), 2)' Formula

                    Case "59"
                        Valores(55) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "59.0"
                        Valores(56) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "59.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round((((Valores(55) * Tasa) / 3) * 2) - Valores(56), 2) ' Formula

                    Case "60"
                        Valores(57) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "60.0"
                        Valores(58) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "60.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round((Valores(57) * 0.04) - Valores(58), 2) ' Formula

                    Case "61.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("61", Mes)

                    Case "62"
                        Valores(59) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "62.0"
                        Valores(60) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "62.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round((((Valores(59) * Tasa) / 3) * 2) - Valores(60), 2) ' Formula

                    Case "63"
                        Valores(61) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "63.0"
                        Valores(62) = IIf(IsDBNull(Me.TablaImportar.Item(Mes, i).Value) = True, 0, Me.TablaImportar.Item(Mes, i).Value)
                    Case "63.1"
                        Me.TablaImportar.Item(Mes, i).Value = Math.Round((Valores(61) * ISR) - Valores(62), 2) ' Formula

                    Case "64.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("64", Mes)
                    Case "65.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("65", Mes)
                    Case "66.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("66", Mes)
                    Case "67.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("67", Mes)
                    Case "68.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("68", Mes)
                    Case "69.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("69", Mes)
                    Case "70.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("70", Mes)
                    Case "71.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("71", Mes)
                    Case "72.1"
                        Me.TablaImportar.Item(Mes, i).Value = Importe_Naturaleza("72", Mes)




                End Select
            Catch ex As Exception

            End Try
        Next


    End Sub
    Private Function IVA_Acreditable_Real(ByVal Mes As String)
        Dim Imp As Decimal = 0
        Dim Sql As String = ""
        Return Imp
    End Function
    Private Function IVA_Acreditable_Delcarado(ByVal Mes As String)
        Dim Imp As Decimal = 0
        Dim Sql As String = ""
        Return Imp
    End Function
    Private Function Importe_Naturaleza(ByVal Tip As String, ByVal Mes As Integer)
        Dim Imp As Decimal = 0
        For j As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            If Trim(Me.TablaImportar.Item(Ti.Index, j).Value.ToString) = Tip Then
                If Trim(Me.TablaImportar.Item(Nat.Index, j).Value.ToString) = "A" Then
                    Imp = Imp - IIf(IsDBNull(Me.TablaImportar.Item(Mes, j).Value) = True, 0, Me.TablaImportar.Item(Mes, j).Value)
                ElseIf Trim(Me.TablaImportar.Item(Nat.Index, j).Value.ToString) = "C" Then
                    Imp = Imp + IIf(IsDBNull(Me.TablaImportar.Item(Mes, j).Value) = True, 0, Me.TablaImportar.Item(Mes, j).Value)
                ElseIf Trim(Me.TablaImportar.Item(Nat.Index, j).Value.ToString) = "SF" Then
                    Imp = Imp + IIf(IsDBNull(Me.TablaImportar.Item(Mes, j).Value) = True, 0, Me.TablaImportar.Item(Mes, j).Value)
                End If
            End If
        Next
        Return Imp
    End Function
    Private Sub CmdListar_Click(sender As Object, e As EventArgs) Handles CmdListar.Click
        If Me.lstCliente.SelectText <> "" Then

            If Trim(Me.LstAnio.Text) <> "" Then
                If Me.TablaImportar.Rows.Count > 0 Then
                    Me.TablaImportar.Rows.Clear()
                    Crear_Filas(Trim(Me.LstAnio.Text), Me.lstCliente.SelectItem)
                Else
                    Crear_Filas(Trim(Me.LstAnio.Text), Me.lstCliente.SelectItem)
                End If

            Else
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("Debes seleccionar un Año para consultar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Debes seleccionar una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
            Exit Sub
        End If

    End Sub

    Private Sub TablaImportar_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles TablaImportar.EditingControlShowing
        Dim ctrl As Control = DirectCast(e.Control, Control)

        If (TypeOf ctrl Is DataGridViewComboBoxEditingControl) Then

            Dim cb As ComboBox = DirectCast(ctrl, ComboBox)

            cb.DropDownStyle = ComboBoxStyle.DropDown

        End If
    End Sub

    Private Sub Calcula(ByVal Tip As String, ByVal Fila As Integer)
        Dim Total As Decimal = 0
        Dim col As Integer = Enero.Index
        For j As Integer = 0 To 11

            Try

                For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                    If Me.TablaImportar.Item(Ti.Index, i).Value = Tip Then
                        Total = Total + IIf(IsDBNull(Me.TablaImportar.Item(col, i).Value) = True, 0, Me.TablaImportar.Item(col, i).Value)
                    End If

                Next

                Me.TablaImportar.Item(col, Fila).Value = Total
                col += 1
                Total = 0
            Catch ex As Exception

            End Try
        Next

    End Sub

    Private Sub TablaImportar_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellDoubleClick
        If Me.TablaImportar.CurrentCell.ColumnIndex = Nat.Index Then
            If Me.TablaImportar.Item(Nat.Index, Me.TablaImportar.CurrentRow.Index).Value <> "Diferencia" Then
                ' creamos el control comboBox
                Dim Combo As New DataGridViewComboBoxCell

                ' rellenamos los items del combobox
                Combo.Items.Add("C")
                Combo.Items.Add("A")
                Combo.Items.Add("SF")
                Combo.FlatStyle = FlatStyle.Flat
                ' le asignamos el control combobox a la celda
                Me.TablaImportar.Item(Nat.Index, Me.TablaImportar.CurrentRow.Index) = Combo.Clone

            End If
        End If
    End Sub

    Private Sub TablaImportar_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles TablaImportar.CellEndEdit
        If Me.TablaImportar.Item(Cta.Index, Me.TablaImportar.CurrentRow.Index).Value <> Nothing Then
            Me.TablaImportar.Item(Descrip.Index, Me.TablaImportar.CurrentRow.Index).Value = Buscar_cuenta(Replace(Me.TablaImportar.Item(Cta.Index, Me.TablaImportar.CurrentRow.Index).Value, "-", ""))
        Else
            Me.TablaImportar.Item(Descrip.Index, Me.TablaImportar.CurrentRow.Index).Value = ""

        End If
    End Sub
    Private Function Buscar_cuenta(ByVal Cta As String)
        Dim Nombre As String = ""
        Nombre = "SELECT Descripcion FROM Catalogo_de_Cuentas WHERE Id_Empresa = " & Me.lstCliente.SelectItem & " AND Cuenta =" & Cta & ""
        Dim ds As DataSet = Eventos.Obtener_DS(Nombre)
        If ds.Tables(0).Rows.Count > 0 Then
            Nombre = ds.Tables(0).Rows(0)(0)
        Else
            Nombre = ""
        End If
        Return Nombre
    End Function

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click

        If Me.lstCliente.SelectText <> "" Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            If RadMessageBox.Show("Se actualizara la informacion de la Empresa  " & Me.lstCliente.SelectText & " para el año " & Me.LstAnio.Text.Trim() & " esto es correcto?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                Dim Sql As String = "Delete From Calculo_Impuestos where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Trim(Me.LstAnio.Text) & " "
                If Eventos.Comando_sql(Sql) > 0 Then
                    Guarda_Ctas()
                End If
            Else
                Exit Sub
            End If
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Debes seleccionar una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub
    Private Sub Guarda_Ctas()
        Dim sql As String = ""
        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.TablaImportar.RowCount - 1
        Me.Cursor = Cursors.AppStarting
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            sql = "INSERT INTO dbo.Calculo_Impuestos 	
                            ( Descripcion, Cuenta,Naturaleza,Enero,Febrero,Marzo,Abril,Mayo,Junio,Julio,Agosto,Septiembre,Octubre,Noviembre,Diciembre,Anio,Id_Empresa,Operacion)
                        	 VALUES 	( '" & Trim(Me.TablaImportar.Item(Descrip.Index, i).Value) & "','" & Trim(Me.TablaImportar.Item(Cta.Index, i).Value) & "', '" & Trim(Me.TablaImportar.Item(Nat.Index, i).Value) & "',	" & IIf(IsDBNull(Me.TablaImportar.Item(Enero.Index, i).Value) = True, 0, Me.TablaImportar.Item(Enero.Index, i).Value) & ",	
                        " & IIf(IsDBNull(Me.TablaImportar.Item(Febrero.Index, i).Value) = True, 0, Me.TablaImportar.Item(Febrero.Index, i).Value) & ",	" & IIf(IsDBNull(Me.TablaImportar.Item(Marzo.Index, i).Value) = True, 0, Me.TablaImportar.Item(Marzo.Index, i).Value) & "," & IIf(IsDBNull(Me.TablaImportar.Item(Abril.Index, i).Value) = True, 0, Me.TablaImportar.Item(Abril.Index, i).Value) & ",	
                        " & IIf(IsDBNull(Me.TablaImportar.Item(Mayo.Index, i).Value) = True, 0, Me.TablaImportar.Item(Mayo.Index, i).Value) & ",	" & IIf(IsDBNull(Me.TablaImportar.Item(Junio.Index, i).Value) = True, 0, Me.TablaImportar.Item(Junio.Index, i).Value) & ",	" & IIf(IsDBNull(Me.TablaImportar.Item(Julio.Index, i).Value) = True, 0, Me.TablaImportar.Item(Julio.Index, i).Value) & ",
                        " & IIf(IsDBNull(Me.TablaImportar.Item(Agosto.Index, i).Value) = True, 0, Me.TablaImportar.Item(Agosto.Index, i).Value) & ",	" & IIf(IsDBNull(Me.TablaImportar.Item(Septiembre.Index, i).Value) = True, 0, Me.TablaImportar.Item(Septiembre.Index, i).Value) & ",	" & IIf(IsDBNull(Me.TablaImportar.Item(Octubre.Index, i).Value) = True, 0, Me.TablaImportar.Item(Octubre.Index, i).Value) & ",
                        " & IIf(IsDBNull(Me.TablaImportar.Item(Noviembre.Index, i).Value) = True, 0, Me.TablaImportar.Item(Noviembre.Index, i).Value) & ",	" & IIf(IsDBNull(Me.TablaImportar.Item(Diciembre.Index, i).Value) = True, 0, Me.TablaImportar.Item(Diciembre.Index, i).Value) & ",	" & Trim(LstAnio.Text) & ",	" & Me.lstCliente.SelectItem & ", " & Trim(Me.TablaImportar.Item(Ti.Index, i).Value) & ")
"
            If Eventos.Comando_sql(sql) = 0 Then
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("No se guardo la informacion error en " & Trim(Me.TablaImportar.Item(Descrip.Index, i).Value) & " verifique la Informacion ", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                sql = "Delete From Calculo_Impuestos where Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio  =" & Trim(Me.LstAnio.Text) & " "
                If Eventos.Comando_sql(sql) = 0 Then

                End If
            End If
            frm.Barra.Value = i
        Next
        frm.Close()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click

        If Polizas.Count > 0 Then

            Dim Listas() As String 'Array sin dimensiones de String
            Dim Cuentas As New List(Of Buscar_Errores_Cruces.Tipos_Cruces)
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.TablaImportar.RowCount - 1
            Me.Cursor = Cursors.AppStarting

            For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                Try
                    If Trim(Me.TablaImportar.Item(Nat.Index, i).Value) = "Diferencia" Then
                        For j As Integer = 0 To 11

                            If Me.TablaImportar.Item(Enero.Index + j, i).Value >= 1 Or Me.TablaImportar.Item(Enero.Index + j, i).Value <= -1 Then
                                Cuentas.Add(New Buscar_Errores_Cruces.Tipos_Cruces() With {.Descripcion = Trim(Me.TablaImportar.Item(Descrip.Index, i).Value).ToString, .Tipo_Suma = Trim(Me.TablaImportar.Item(Ti.Index, i).Value), .ID = i})
                                If Listas Is Nothing Then
                                    ReDim Preserve Listas(0)
                                    Listas(UBound(Listas)) = Me.TablaImportar.Item(Descrip.Index, i).Value
                                Else
                                    ReDim Preserve Listas(UBound(Listas) + 1)
                                    Listas(UBound(Listas)) = Me.TablaImportar.Item(Descrip.Index, i).Value
                                End If
                            End If


                        Next
                    End If
                Catch ex As Exception

                End Try
                frm.Barra.Value = i
            Next
            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso busqueda de errores Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
            If Listas Is Nothing Then
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("No hay errores...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Else
                Buscar_Errores_Cruces.Cuentas = Cuentas
                Buscar_Errores_Cruces.Polizas = Polizas
                Buscar_Errores_Cruces.Arreglo = Listas
                Eventos.Abrir_form(Buscar_Errores_Cruces)
            End If
        End If
    End Sub
    Private Sub Cuentaerrores_mes()
        If Me.TablaErrores.RowCount = 2 Then
            Me.TablaErrores.Rows.Clear()
        End If
        Me.TablaErrores.RowCount = 2


        If Me.TablaImportar.Rows.Count > 0 Then
            For j As Integer = 0 To 11
                For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1

                    If (Me.TablaImportar.Item(Enero.Index + j, i).Value >= 1 Or Me.TablaImportar.Item(Enero.Index + j, i).Value <= -1) And Trim(Me.TablaImportar.Item(Nat.Index, i).Value) = "Diferencia" Then
                        Me.TablaErrores.Item(j, 0).Value += 1

                        Me.TablaErrores.Item(j, 1).Value += IIf(Me.TablaImportar.Item(Enero.Index + j, i).Value < -1, Me.TablaImportar.Item(Enero.Index + j, i).Value * -1, Me.TablaImportar.Item(Enero.Index + j, i).Value)
                    End If
                Next
                contador = 0
            Next

        End If

    End Sub

    Private Sub SegundoPlano_DoWork(sender As Object, e As DoWorkEventArgs) Handles SegundoPlano.DoWork

    End Sub

    Private Sub SegundoPlano_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles SegundoPlano.RunWorkerCompleted
        If e.Cancelled Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("La ación ha sido cancelada.")
        ElseIf e.Error IsNot Nothing Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Se ha producido un error durante la ejecución: " & e.Error.Message)

        End If
    End Sub
    Private Sub Hacer(ByVal An As Integer, ByVal Cliente As Integer)
        Try
            My.Forms.Inicio.txtServerDB.Text = serV
            Me.Barra.Maximum = Me.TablaImportar.RowCount - 1
            Me.Barra.Minimum = 0
            Me.Barra.Value1 = 0
            For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1

                If Me.TablaImportar.Item(Cta.Index, i).Value <> Nothing Then

                    If Me.TablaImportar.Item(Nat.Index, i).Value <> Nothing Then
                        Dim Tipo As String = IIf(IsNothing(Me.TablaImportar.Item(Nat.Index, i).Value.ToString) = True, "", Me.TablaImportar.Item(Nat.Index, i).Value.ToString)
                        'Calcular("Polizas.Id_Anio  = " & Me.LstAnio.Text & "", Me.lstCliente.SelectItem, Me.TablaImportar.Item(Cta.Index, i).Value.ToString, i, Enero.Index, Trim(Tipo))
                        Calcular("Polizas.Id_Anio  = " & An & "", Cliente, Me.TablaImportar.Item(Cta.Index, i).Value.ToString, i, Enero.Index, Trim(Tipo))
                    End If

                End If
                If Me.Barra.Value1 = Me.Barra.Maximum Then
                    Me.Barra.Minimum = 0
                    Me.Cursor = Cursors.Arrow
                    Me.Barra.Value1 = 0
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    RadMessageBox.Show("Se han calculado los saldos Ahora se realizaran los Cruces", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

                Else
                    Me.Barra.Value1 += 1
                End If

            Next

        Catch ex As Exception

        End Try
        Try
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Text = "Calculando cruces, por favor espere..."
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.TablaImportar.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                Select Case Trim(Me.TablaImportar.Item(Ti.Index, i).Value.ToString)
                    Case "1.1"
                        Calcula(1, i)
                    Case "2.1"
                        Calcula(2, i)
                    Case "3.0"
                        Calcula(3, i)
                    Case "3.3"
                        Calcula("3.2", i)

                    Case "4.1"
                        Calcula(4, i)
                    Case "6.1"
                        Calcula(6, i)
                    Case "8.1"
                        Calcula(8, i)
                    Case "10.1"
                        Calcula(10, i)
                    Case "12.1"
                        Calcula(12, i)
                    Case "14.1"
                        Calcula(14, i)
                    Case "16.1"
                        Calcula(16, i)
                    Case "18.1"
                        Calcula(18, i)
                    Case "23.1"
                        Calcula(23, i)
                    Case "24.1"
                        Calcula(24, i)
                    Case "25.1"
                        Calcula(25, i)
                    Case "40.1"
                        Calcula(40, i)
                    Case "41.1"
                        Calcula(41, i)
                    Case "44.1"
                        Calcula(44, i)
                    Case "46.1"
                        Calcula(46, i)
                    Case "48.1"
                        Calcula(48, i)
                    Case "51.1"
                        Calcula(51, i)
                    Case "52.1"
                        Calcula(52, i)
                    Case "53.1"
                        Calcula(53, i)
                End Select
                frm.Barra.Value = i
            Next
            'Calculara Impuestos
            Dim col As Integer = Enero.Index
            For j As Integer = 0 To 11
                CalculaImpuesto(col, IIf(Len((j + 1).ToString) = 1, "0" & (j + 1).ToString, j.ToString))
                col += 1

            Next
            Cuentaerrores_mes()
            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        Catch ex As Exception

        End Try
    End Sub



    Private Sub TablaImportar_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaImportar.DataError

    End Sub




    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        Try
            For j As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                If Me.TablaImportar.Item(1, j).Selected = True Then
                    Me.TablaImportar.Item(1, j).Value = ""
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub
End Class
