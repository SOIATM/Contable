Imports System.IO
Imports Telerik.WinControls
Public Class Auxiliares

    Dim Negrita_verde As New DataGridViewCellStyle
    Dim Negrita_morado As New DataGridViewCellStyle
    Dim Axul As New DataGridViewCellStyle
    Dim ds_cargos_abonos As DataSet
    Dim ds_polizas_subctas, ds_subcta, ds_sub_subsubctas, ds_sub_subctas As DataSet
    Dim subcta As String = ""
    Dim cta As String = ""
    Dim reg As Integer
    Dim t_cargos, t_abonos As Decimal
    Private Sub PintaTabla(ByVal Fila As Integer, ByVal Tabla As DataGridView, ByVal T As Integer)
        Negrita_verde.Font = New Font(Tabla.Font, FontStyle.Bold)
        Negrita_verde.BackColor = Color.LawnGreen
        Negrita_verde.Alignment = DataGridViewContentAlignment.MiddleCenter
        Axul.Font = New Font(Tabla.Font, FontStyle.Bold)
        Axul.BackColor = Color.LawnGreen
        Axul.Alignment = DataGridViewContentAlignment.MiddleCenter
        Negrita_morado.Font = New Font(Tabla.Font, FontStyle.Bold)
        Negrita_morado.BackColor = Color.Plum
        Dim F As DataGridViewRow
        F = Tabla.Rows(Fila)
        If T = 1 Then
            F.DefaultCellStyle = Negrita_verde
        ElseIf T = 2 Then
            F.DefaultCellStyle = Negrita_morado
        Else
            F.DefaultCellStyle = Axul
        End If

    End Sub
    Private Sub Auxiliares_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.DiseñoTabla(Me.auxiliar)
        Me.lstCliente.Cargar(" SELECT Id_Empresa, Razon_social FROM Empresa ")
        Me.lstCliente.SelectItem = 1
        Me.Lstctainicial.Cargar("SELECT cuenta, Rtrim(Descripcion) + '/' +  Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4  from   Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " order by nivel1,nivel2,nivel3,nivel4")
        Me.Lstctainicial.SelectText = ""
        Me.Lstctafinal.Cargar("SELECT cuenta, Rtrim(Descripcion) + '/' + Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4   from Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " order by nivel1,nivel2,nivel3,nivel4")
        Me.Lstctafinal.SelectText = ""
    End Sub

    Private Sub CalcularAuxiliar()
        If Me.auxiliar.Rows.Count > 0 Then
            Try
                Me.auxiliar.Rows.Clear()
            Catch ex As Exception

            End Try
        End If
        Dim Cnt As Integer = 0
        Dim C As Decimal = 0
        Dim A As Decimal = 0
        Dim Total As Decimal = 0
        Dim sql As String = "SELECT Cuenta, Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 +'-'+  Rtrim(Descripcion) as DCuenta ,  Nivel1,Naturaleza , Rtrim(Descripcion) as Nombre FROM Catalogo_de_Cuentas    "
        sql &= " where Id_Empresa = " & Me.lstCliente.SelectItem & " and  cuenta >= " & Me.Lstctainicial.SelectItem & " and cuenta<=" & Me.Lstctafinal.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.lstCliente.SelectItem & "  ) ORDER BY cuenta"
        Dim dscuentas As DataSet = Eventos.Obtener_DS(sql)
        If dscuentas.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To dscuentas.Tables(0).Rows.Count - 1
                sql = " SELECT Detalle_Polizas.cuenta,  Polizas.ID_poliza as numero, Polizas.Fecha, Polizas.Concepto as Movimiento, "
                sql &= " Detalle_Polizas.Cargo, Detalle_Polizas.Abono,  Polizas.ID_anio, Polizas.ID_mes,Tipos_Poliza_Sat.Clave +' '+ Tipos_Poliza_Sat.Nombre as Clave , Polizas.Num_pol as N FROM Polizas INNER JOIN empresa ON Polizas.Id_Empresa = empresa.Id_Empresa "
                sql &= "  INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat "
                sql &= " INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza "
                sql &= " INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa "
                sql &= " WHERE Catalogo_de_Cuentas.Id_Empresa = " & Me.lstCliente.SelectItem & " and (Polizas.fecha >= " & Eventos.Sql_hoy(Me.DtInicio.Value.ToString.Substring(0, 10)) & " AND Polizas.fecha <= " & Eventos.Sql_hoy(Me.Dtfin.Value.ToString.Substring(0, 10)) & ") "
                sql &= " AND (Detalle_Polizas.cuenta = '" & dscuentas.Tables(0).Rows(i)("cuenta") & "') order by Polizas.Fecha "
                Dim ds = Eventos.Obtener_DS(sql)
                If ds.Tables(0).Rows.Count > 0 Then
                    For j As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Me.auxiliar.Rows.Add()
                        reg = Me.auxiliar.Rows.Count - 1
                        If j = 0 Then
                            PintaTabla(Me.auxiliar.Rows.Count - 1, Me.auxiliar, 1)
                        End If
                        With Me.auxiliar
                            .Item(Ctas.Index, reg).Value = Trim(dscuentas.Tables(0).Rows(i)("Dcuenta"))
                            .Item(Tipo.Index, reg).Value = ds.Tables(0).Rows(j)("Clave")

                            .Item(Suma.Index, reg).Value = ds.Tables(0).Rows(j)("N")
                            .Item(Nop.Index, reg).Value = ds.Tables(0).Rows(j)("Numero")
                            .Item(Fecha.Index, reg).Value = ds.Tables(0).Rows(j)("Fecha").ToString.Substring(0, 10)
                            .Item(Concepto.Index, reg).Value = ds.Tables(0).Rows(j)("Movimiento")
                            .Item(Car.Index, reg).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cargo")), 0, ds.Tables(0).Rows(j)("Cargo"))
                            .Item(AB.Index, reg).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Abono")), 0, ds.Tables(0).Rows(j)("Abono"))
                            If j = 0 Then
                                .Item(Nom.Index, reg).Value = Trim(dscuentas.Tables(0).Rows(i)("Nombre"))
                            Else
                                .Item(Nom.Index, reg).Value = ""
                            End If

                            C += IIf(IsDBNull(.Item(Car.Index, reg).Value) = True, 0, .Item(Car.Index, reg).Value)
                            A += IIf(IsDBNull(.Item(AB.Index, reg).Value) = True, 0, .Item(AB.Index, reg).Value)
                            If Trim(dscuentas.Tables(0).Rows(i)("Naturaleza")) = "D" Then
                                .Item(Saldo.Index, reg).Value = C - A
                            Else
                                .Item(Saldo.Index, reg).Value = A - C
                            End If

                        End With
                    Next
                    Me.auxiliar.Rows.Add()
                    PintaTabla(Me.auxiliar.Rows.Count - 1, Me.auxiliar, 2)
                    Me.auxiliar.Item(Concepto.Index, Me.auxiliar.Rows.Count - 1).Value = "Saldo de la Cuenta: " & dscuentas.Tables(0).Rows(i)("cuenta")
                    Me.auxiliar.Item(Car.Index, Me.auxiliar.Rows.Count - 1).Value = C
                    Me.auxiliar.Item(AB.Index, Me.auxiliar.Rows.Count - 1).Value = A
                    If Trim(dscuentas.Tables(0).Rows(i)("Naturaleza")) = "D" Then
                        Me.auxiliar.Item(Saldo.Index, Me.auxiliar.Rows.Count - 1).Value = C - A
                    Else
                        Me.auxiliar.Item(Saldo.Index, Me.auxiliar.Rows.Count - 1).Value = A - C
                    End If
                    Total = Me.auxiliar.Item(Saldo.Index, Me.auxiliar.Rows.Count - 1).Value
                    Me.auxiliar.Rows.Add()
                End If
                C = 0
                A = 0

            Next
        End If
    End Sub

    Private Sub Todas_cuentas()
        Eventos.Cuentas(Me.DtInicio.Value.ToString.Substring(6, 4), Me.lstCliente.SelectItem)

        Dim reg As Integer = 0
        Dim sql As String = "SELECT Cuenta, Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 +'-'+  Rtrim(Descripcion) as DCuenta ,  Nivel1,Naturaleza  FROM Catalogo_de_Cuentas    "
        sql &= " where Id_Empresa = " & Me.lstCliente.SelectItem & " and  cuenta >= " & Me.Lstctainicial.SelectItem & " and cuenta<=" & Me.Lstctafinal.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.lstCliente.SelectItem & "  ) ORDER BY cuenta"

        Dim dscuentas As DataSet = Eventos.Obtener_DS(sql)
        Dim cta As String = ""
        If dscuentas.Tables(0).Rows.Count > 0 Then
            Me.auxiliar.RowCount = 1
            reg = Me.auxiliar.Rows.Count - 1
            For i As Integer = 0 To dscuentas.Tables(0).Rows.Count - 1 '//Cuentas de mayor
                Me.auxiliar.RowCount = dscuentas.Tables(0).Rows.Count

                'cta = dscuentas.Tables(0).Rows(i)("num_cta")
                Me.auxiliar.CurrentCell = Me.auxiliar.Rows(Me.auxiliar.Rows.Count - 1).Cells(1)
                Me.auxiliar.Rows.Add()
                reg = Me.auxiliar.Rows.Count - 1 'reg + 1
                Me.auxiliar.Item(0, reg).Value = Trim(dscuentas.Tables(0).Rows(i)("Dcuenta"))
                Dim acumulado As Decimal = Calcula_Saldos_InicialesP(dscuentas.Tables(0).Rows(i)("cuenta"), Me.DtInicio.Value.ToString.Substring(6, 4), Me.lstCliente.SelectItem, "  ( Polizas.Fecha_captura <" & Eventos.Sql_hoy(Me.DtInicio.Value.ToString.Substring(0, 10)) & " )")
                Me.auxiliar.Item(5, reg).Value = Eventos.Calcula_Saldos_Iniciales(dscuentas.Tables(0).Rows(i)("cuenta"), Me.DtInicio.Value.ToString.Substring(0, 10), Me.lstCliente.SelectItem) + acumulado
                Dim saldo As Decimal = IIf(IsDBNull(Me.auxiliar.Item(5, reg).Value) = True, 0, Me.auxiliar.Item(5, reg).Value)
                '   Me.auxiliar.Item(9, reg).Value = Trim(dscuentas.Tables(0).Rows(i)("Tipo_suma"))
                Polizas_subctas(dscuentas.Tables(0).Rows(i)("cuenta"))
                If ds_polizas_subctas.Tables(0).Rows.Count > 0 Then
                    reg = reg + 1
                    t_cargos = 0
                    t_abonos = 0
                    For j As Integer = 0 To ds_polizas_subctas.Tables(0).Rows.Count - 1
                        With Me.auxiliar
                            Me.auxiliar.CurrentCell = Me.auxiliar.Rows(Me.auxiliar.Rows.Count - 1).Cells(1)
                            Me.auxiliar.Rows.Add()
                            If j = 0 Then
                                If Trim(dscuentas.Tables(0).Rows(i)("Naturaleza")) = "D" Then
                                    .Item(8, reg).Value = (saldo + IIf(IsDBNull(ds_polizas_subctas.Tables(0).Rows(j)("Cargo")) = True, 0, ds_polizas_subctas.Tables(0).Rows(j)("Cargo"))) - IIf(IsDBNull(ds_polizas_subctas.Tables(0).Rows(j)("Abono")) = True, 0, ds_polizas_subctas.Tables(0).Rows(j)("Abono"))
                                Else
                                    .Item(8, reg).Value = (saldo - IIf(IsDBNull(ds_polizas_subctas.Tables(0).Rows(j)("Cargo")) = True, 0, ds_polizas_subctas.Tables(0).Rows(j)("Cargo"))) + IIf(IsDBNull(ds_polizas_subctas.Tables(0).Rows(j)("Abono")) = True, 0, ds_polizas_subctas.Tables(0).Rows(j)("Abono"))
                                End If
                                .Item(5, reg).Value = .Item(5, reg - 1).Value
                            Else
                                If Trim(dscuentas.Tables(0).Rows(i)("Naturaleza")) = "D" Then
                                    .Item(8, reg).Value = (.Item(8, reg - 1).Value + IIf(IsDBNull(ds_polizas_subctas.Tables(0).Rows(j)("Cargo")) = True, 0, ds_polizas_subctas.Tables(0).Rows(j)("Cargo"))) - IIf(IsDBNull(ds_polizas_subctas.Tables(0).Rows(j)("Abono")) = True, 0, ds_polizas_subctas.Tables(0).Rows(j)("Abono"))
                                Else
                                    .Item(8, reg).Value = (.Item(8, reg - 1).Value - IIf(IsDBNull(ds_polizas_subctas.Tables(0).Rows(j)("Cargo")) = True, 0, ds_polizas_subctas.Tables(0).Rows(j)("Cargo"))) + IIf(IsDBNull(ds_polizas_subctas.Tables(0).Rows(j)("Abono")) = True, 0, ds_polizas_subctas.Tables(0).Rows(j)("Abono"))
                                End If
                                .Item(5, reg).Value = .Item(8, reg - 1).Value
                            End If
                            .Item(0, reg).Value = Trim(dscuentas.Tables(0).Rows(i)("Dcuenta"))
                            .Item(1, reg).Value = ds_polizas_subctas.Tables(0).Rows(j)("Clave")
                            .Item(2, reg).Value = ds_polizas_subctas.Tables(0).Rows(j)("Numero")
                            .Item(3, reg).Value = ds_polizas_subctas.Tables(0).Rows(j)("Fecha").ToString.Substring(0, 10)
                            .Item(4, reg).Value = ds_polizas_subctas.Tables(0).Rows(j)("Movimiento")
                            .Item(6, reg).Value = ds_polizas_subctas.Tables(0).Rows(j)("Cargo")
                            .Item(7, reg).Value = ds_polizas_subctas.Tables(0).Rows(j)("Abono")

                            t_cargos = t_cargos + IIf(IsDBNull(Me.auxiliar.Item(6, reg).Value) = True, 0, Me.auxiliar.Item(6, reg).Value)
                            t_abonos = t_abonos + IIf(IsDBNull(Me.auxiliar.Item(7, reg).Value) = True, 0, Me.auxiliar.Item(7, reg).Value)
                            reg = reg + 1
                        End With
                    Next
                    Me.auxiliar.CurrentCell = Me.auxiliar.Rows(Me.auxiliar.Rows.Count - 1).Cells(1)
                    Me.auxiliar.Rows.Add()

                    Me.auxiliar.Item(4, reg).Value = "Saldo: "
                    Me.auxiliar.Item(8, reg).Value = Me.auxiliar.Item(8, reg - 1).Value
                Else
                    Me.auxiliar.CurrentCell = Me.auxiliar.Rows(Me.auxiliar.Rows.Count - 1).Cells(1)
                    Me.auxiliar.Rows.Add()

                    Me.auxiliar.Item(4, reg + 1).Value = "Saldo:"
                    Me.auxiliar.Item(6, reg + 1).Value = 0
                    Me.auxiliar.Item(7, reg + 1).Value = 0
                End If
            Next
        End If
    End Sub

    Private Sub Saldo_mayores(ByVal nivel1 As String)
        Dim sql As String = " SELECT Catalogo_de_Cuentas.Nivel1 AS Cuenta, SUM(Polizas.Cargo) AS Cargos, SUM(Polizas.Abono) AS Abonos"
        sql &= " FROM  Catalogo_de_Cuentas INNER JOIN Polizas ON Catalogo_de_Cuentas.cuenta = Polizas.cuenta"
        sql &= " WHERE (Polizas.fecha >= '" & Me.DtInicio.Value.ToString.Substring(0, 10) & "' AND (Polizas.fecha <= '" & Me.Dtfin.Value.ToString.Substring(0, 10) & "'"
        sql &= " AND Polizas.Id_cta IN (SELECT cuenta FROM Catalogo_de_Cuentas "
        sql &= " WHERE (Nivel1>='" & Me.Lstctainicial.SelectText.Substring(0, 4) & "' AND Nivel2>='" & Me.Lstctainicial.SelectText.Substring(5, 3) & "' AND Nivel3>='" & Me.Lstctainicial.SelectText.Substring(9, 3) & "' AND Nivel4>='" & Me.Lstctainicial.SelectText.Substring(13, 3) & "')"
        sql &= " AND (Nivel1<='" & Me.Lstctafinal.SelectText.Substring(0, 4) & "' AND Nivel2<='" & Me.Lstctafinal.SelectText.Substring(5, 3) & "' AND Nivel3<='" & Me.Lstctafinal.SelectText.Substring(9, 3) & "' AND Nivel4<='" & Me.Lstctafinal.SelectText.Substring(13, 3) & "'))"
        sql &= " GROUP BY Nivel1 "
        ds_cargos_abonos = Eventos.Obtener_DS(sql)
    End Sub


    Private Sub Dame_subcta(ByVal cuenta As String)
        Dim sql As String = "SELECT cuenta, Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4+'  '+ Descripcion as Cuenta,  Nivel1  FROM Catalogo_de_Cuentas where  nivel1 = '" & cuenta & "' and nivel2>0 and Nivel3='0000' and Id_empresa  =" & Me.lstCliente.SelectItem & "order by nivel1,nivel2,nivel3,nivel4 "
        ds_subcta = Eventos.Obtener_DS(sql)
    End Sub


    Private Sub Genera_aux()
        Dim cuentas As String = ""
        Dim excel As Microsoft.Office.Interop.Excel.Application
        excel = Eventos.NuevoExcel("Auxiliar", False)
        Eventos.EscribeExcel(excel, 4, 5, "Del " & Me.DtInicio.Value & "   Al    " & Me.Dtfin.Value & "")
        Dim fila As Integer = 7
        For i As Integer = 1 To Me.auxiliar.Rows.Count - 1
            If Me.auxiliar.Item(0, i).Value <> "" And Me.auxiliar.Item(1, i).Value = "" Then
                'If Me.auxiliar.Item(9, i).Value = 1 Then
                Dim letras As Integer = Len(Me.auxiliar.Item(0, i).Value)
                Dim cuent = Trim(Me.auxiliar.Item(0, i).Value).Substring(16, letras - 16)
                cuentas = Me.auxiliar.Item(0, i).Value
                Eventos.EscribeExcel(excel, fila, 1, cuentas.ToString.Substring(0, 4)) 'nivel1
                Eventos.EscribeExcel(excel, fila, 2, "'" & cuentas.ToString.Substring(5, 3)) 'nivel2
                Eventos.EscribeExcel(excel, fila, 3, "'" & cuentas.ToString.Substring(9, 3)) 'nivel3
                Eventos.EscribeExcel(excel, fila, 4, "'" & cuentas.ToString.Substring(13, 3)) 'nivel4
                excel.Range("A" & fila & ":F" & fila & "").Select()
                excel.Selection.Interior.ColorIndex = 19
                Eventos.EscribeExcel(excel, fila, 5, Trim(cuent))
                excel.Range("E" & fila & ":H" & fila & "").Select()
                excel.Selection.Interior.ColorIndex = 19
                excel.Selection.Interior.Pattern = 1
                excel.Selection.Font.ColorIndex = 1
                excel.Selection.Font.bold = True
                '  excel.Selection.Font.color = 2
                excel.Selection.MergeCells = True

                excel.Range("G" & fila & ":I" & fila & "").Select()
                excel.Selection.Interior.ColorIndex = 19
            Else
                Eventos.EscribeExcel(excel, fila, 1, cuentas.ToString.Substring(0, 4)) 'nivel1
                Eventos.EscribeExcel(excel, fila, 2, "'" & cuentas.ToString.Substring(5, 3)) 'nivel2
                Eventos.EscribeExcel(excel, fila, 3, "'" & cuentas.ToString.Substring(9, 3)) 'nivel3
                Eventos.EscribeExcel(excel, fila, 4, "'" & cuentas.ToString.Substring(13, 3)) 'nivel4
                Eventos.EscribeExcel(excel, fila, 5, Me.auxiliar.Item(1, i).Value) 'tipo
                Eventos.EscribeExcel(excel, fila, 6, Me.auxiliar.Item(2, i).Value) 'numero
                Eventos.EscribeExcel(excel, fila, 7, Me.auxiliar.Item(3, i).Value) 'fecha
                Eventos.EscribeExcel(excel, fila, 8, Me.auxiliar.Item(4, i).Value) 'concepto
                Dim carg As Decimal = IIf(IsDBNull(Me.auxiliar.Item(6, i).Value) = True, "0", Me.auxiliar.Item(6, i).Value)
                Dim abon As Decimal = IIf(IsDBNull(Me.auxiliar.Item(7, i).Value) = True, "0", Me.auxiliar.Item(7, i).Value)
                Eventos.EscribeExcel(excel, fila, 10, carg)
                Eventos.EscribeExcel(excel, fila, 11, abon)
                Eventos.EscribeExcel(excel, fila, 9, IIf(IsDBNull(Me.auxiliar.Item(8, i).Value) = True, "0", Me.auxiliar.Item(8, i).Value))
            End If
            fila += 1
        Next

        Eventos.Mostrar_Excel(excel)
        ' End With

    End Sub

    Private Sub CmdExportar_Click(sender As Object, e As EventArgs) Handles CmdExportar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.auxiliar.RowCount > 0 Then
            If Me.auxiliar.Columns.Count > 256 Then
                RadMessageBox.Show("El rango de fechas sobrepasa las columnas de una hoja de excel, disminuye el rango...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                Exit Sub
            End If

            Dim excel As Microsoft.Office.Interop.Excel.Application = Eventos.NuevoExcel("vacio", False)
            For col As Integer = 0 To Me.auxiliar.Columns.Count - 1
                Eventos.EscribeExcel(excel, 1, col + 1, Me.auxiliar.Columns(col).HeaderText)
            Next
            For i As Integer = 0 To Me.auxiliar.RowCount - 1
                For j As Integer = 0 To Me.auxiliar.Columns.Count - 1
                    Eventos.EscribeExcel(excel, i + 2, j + 1, Valor(Me.auxiliar.Item(j, i).Value))
                Next
            Next
            Eventos.Mostrar_Excel(excel)
        Else
            RadMessageBox.Show("No hay datos para exportar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
    End Sub

    Private Sub CmdGenerar_Click(sender As Object, e As EventArgs) Handles CmdGenerar.Click
        'Todas_cuentas()
        CalcularAuxiliar()
    End Sub

    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub

    Private Sub CmdPdf_Click(sender As Object, e As EventArgs) Handles CmdPdf.Click
        If Me.auxiliar.Rows.Count > 0 Then
            Crear_Reporte()
        End If
    End Sub


    Private Sub Crear_Reporte()
        Dim DT As New DataTable


        With DT
            .Columns.Add("Cuenta")
            .Columns.Add("Mes")
            .Columns.Add("Dia")
            .Columns.Add("Tipo")
            .Columns.Add("Pol")
            .Columns.Add("Descripcion")
            .Columns.Add("Nombre")
            .Columns.Add("Cargos", Type.GetType("System.Decimal"))
            .Columns.Add("Abonos", Type.GetType("System.Decimal"))
            .Columns.Add("Saldo", Type.GetType("System.Decimal"))
        End With
        For Each dr As DataGridViewRow In Me.auxiliar.Rows
            If dr.DefaultCellStyle.BackColor <> Color.Plum Then
                If dr.Cells(Nom.Index).Value IsNot Nothing Then

                    Try
                        DT.Rows.Add(dr.Cells(Ctas.Index).Value.ToString.Substring(0, 16), dr.Cells(Fecha.Index).Value.ToString.Substring(3, 2), dr.Cells(Fecha.Index).Value.ToString.Substring(0, 2), dr.Cells(Tipo.Index).Value.ToString.Substring(0, 3), dr.Cells(Suma.Index).Value.trim().ToString.PadLeft(6, "0"), dr.Cells(Concepto.Index).Value, dr.Cells(Nom.Index).Value, dr.Cells(Car.Index).Value, dr.Cells(AB.Index).Value, dr.Cells(Saldo.Index).Value)

                    Catch ex As Exception
                    End Try
                End If

            End If
        Next

        Dim Cr As New Auxiliar
        Cr.Database.Tables("Auxiliar").SetDataSource(DT)

        Dim Reporte As New ReporteBalanzaGeneral
        Reporte.CrvBalanza.ReportSource = Cr

        Dim FechaD As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim Rptinv As New CrystalDecisions.Shared.ParameterValues()
        FechaD.Value = Me.DtInicio.Value.ToString.Substring(0, 10)
        Rptinv.Add(FechaD)
        Cr.DataDefinition.ParameterFields("Fecha Desde").ApplyCurrentValues(Rptinv)

        Dim FechaF As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim ff As New CrystalDecisions.Shared.ParameterValues()
        FechaF.Value = Me.Dtfin.Value.ToString.Substring(0, 10)
        ff.Add(FechaF)
        Cr.DataDefinition.ParameterFields("Fecha Hasta").ApplyCurrentValues(ff)

        Dim Cliente As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim Cl As New CrystalDecisions.Shared.ParameterValues()
        Cliente.Value = Me.lstCliente.SelectText
        Cl.Add(Cliente)
        Cr.DataDefinition.ParameterFields("Cliente").ApplyCurrentValues(Cl)


        Dim RFC As String = Eventos.ObtenerValorDB("Empresa", "Reg_fed_causantes", " Id_Empresa = " & Me.lstCliente.SelectItem & "", True)

        Dim RFCs As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim RF As New CrystalDecisions.Shared.ParameterValues()
        RFCs.Value = RFC
        RF.Add(RFCs)
        Cr.DataDefinition.ParameterFields("RFC").ApplyCurrentValues(RF)
        Dim Archivo As String = "C:\Program Files\Contable\SetupProyectoContable\Auxiliar\PDF\Auxiliar" & RFC & ".pdf"
        If Directory.Exists("C:\Program Files\Contable\SetupProyectoContable\Auxiliar\PDF") = False Then
            ' si no existe la carpeta se crea
            Directory.CreateDirectory("C:\Program Files\Contable\SetupProyectoContable\Auxiliar\PDF")
        End If
        If Directory.Exists(Archivo) Then
            My.Computer.FileSystem.DeleteFile(Archivo)
        End If
        Cr.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Archivo)
        Process.Start(Archivo)
    End Sub



    Private Sub Polizas_subctas(ByVal n_cuenta As String)
        Dim sql As String = ""

        sql &= " SELECT Detalle_Polizas.cuenta,  Polizas.ID_poliza as numero, Polizas.Fecha, Polizas.Concepto as Movimiento, 
                     Detalle_Polizas.Cargo, Detalle_Polizas.Abono,  Polizas.ID_anio, Polizas.ID_mes,Tipos_Poliza_Sat.Clave +' '+ Tipos_Poliza_Sat.Nombre as Clave  FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
              INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Clientes.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa "
        sql &= " WHERE Catalogo_de_Cuentas.Id_Empresa = " & Me.lstCliente.SelectItem & " and (Polizas.fecha >= '" & Me.DtInicio.Value.ToString.Substring(0, 10) & "' AND Polizas.ID_mes <= '" & Me.Dtfin.Value.ToString.Substring(0, 10) & "') "
        sql &= "   AND (Detalle_Polizas.cuenta = '" & n_cuenta & "') order by Polizas.Fecha,polizas.consecutivo"

        ds_polizas_subctas = Eventos.Obtener_DS(sql)
    End Sub

    Private Sub Dame_subsubsubctas(ByVal cuenta As String, ByVal subsubcuenta As String, ByVal subsubsubcuenta As String)
        Dim sql As String = "SELECT cuenta , Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 +'  '+ Descripcion as DCuenta, nivel1  FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and  nivel1 = '" & cuenta & "' and nivel2='" & subsubcuenta & "' and nivel3='" & subsubsubcuenta & "'  and nivel4 > 0 order by nivel1, nivel2, nivel3, nivel4"
        ds_sub_subsubctas = Eventos.Obtener_DS(sql)
    End Sub

    Private Sub Dame_subsubctas(ByVal cuenta As String, ByVal subcuenta As String)
        Dim sql As String = "SELECT cuenta, Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4+'  '+ Descripcion as Dcuenta,  nivel1  FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " and nivel1 = '" & cuenta & "' and nivel2='" & subcuenta & "' and nivel3>0 and nivel4='0000'"
        ds_sub_subctas = Eventos.Obtener_DS(sql)
    End Sub

    Private Sub Lstctainicial_cambio_item(value As String, texto As String) Handles Lstctainicial.Cambio_item
        Me.Lstctafinal.SelectItem = Me.Lstctainicial.SelectItem
    End Sub
End Class