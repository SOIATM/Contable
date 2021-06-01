Imports System.IO
Imports Telerik.WinControls
Public Class EstadodeResultados
    Private Sub EstadodeResultados_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar()
    End Sub
    Private Sub Cargar()
        Me.LstCliente.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social FROM     Empresa  ")
        Me.LstCliente.SelectItem = 1

    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        Limpiar()
    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.LstCliente.SelectText <> "" Then
            Limpiar()
            Buscar_estado()
        Else
            RadMessageBox.Show("No se ha seleccionado una Empresa", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If

    End Sub
    Private Sub Buscar_estado()
        'Ingresos
        Dim Negritas As Font
        Negritas = New Font(Tabla.Font, FontStyle.Bold)

        Dim posicion As Integer = 0
        Dim Suma As Decimal = 0
        Dim sql As String = "SELECT Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Descripcion,Catalogo_de_Cuentas.Naturaleza FROM Catalogo_de_Cuentas WHERE Clasificacion ='IVS'AND Nivel1 >0 AND Nivel2 ='0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  ) ORDER BY Cuenta"
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = ds.Tables(0).Rows.Count + 1
            Me.Tabla.Item(Desc.Index, posicion).Value = "Ingreso"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                posicion = posicion + 1
                Me.Tabla.Item(CtaAc.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(1)
                Me.Tabla.Item(Saldof.Index, posicion).Value = Calcula_saldo(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), ds.Tables(0).Rows(i)(0), True)
                Me.Tabla.Item(Signo.Index, posicion).Value = ds.Tables(0).Rows(i)(2)
                Me.Tabla.Item(Tip.Index, posicion).Value = "U"
                If Me.Tabla.Item(Signo.Index, posicion).Value.ToString.Trim() = "D" Then
                    Suma -= Me.Tabla.Item(Saldof.Index, posicion).Value
                Else
                    Suma += Me.Tabla.Item(Saldof.Index, posicion).Value
                End If

            Next
            Me.Tabla.RowCount = Me.Tabla.RowCount + 1
            posicion = posicion + 1
            Me.Tabla.Item(Desc.Index, posicion).Value = "INGRESOS NETOS"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            Me.Tabla.Item(Saldof.Index, posicion).Value = Suma
            Suma = 0
            Me.Tabla.Rows.Add()
            Me.Tabla.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1
        End If

        ' Compras
        sql = "SELECT Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Descripcion,Catalogo_de_Cuentas.Naturaleza FROM Catalogo_de_Cuentas WHERE Clasificacion ='CVE' AND Nivel1 >0 AND Nivel2 ='0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  ) ORDER BY Cuenta"
        ds.Clear()
        ds = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = Me.Tabla.RowCount + ds.Tables(0).Rows.Count
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                posicion = posicion + 1
                Me.Tabla.Item(CtaAc.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(1)
                Me.Tabla.Item(Signo.Index, posicion).Value = ds.Tables(0).Rows(i)(2)
                Me.Tabla.Item(Saldof.Index, posicion).Value = Calcula_saldo(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), ds.Tables(0).Rows(i)(0), False)
                Me.Tabla.Item(Tip.Index, posicion).Value = "U"
                If Me.Tabla.Item(Signo.Index, posicion).Value.ToString.Trim() = "D" Then
                    Suma += Me.Tabla.Item(Saldof.Index, posicion).Value
                Else
                    Suma -= Me.Tabla.Item(Saldof.Index, posicion).Value
                End If
            Next
            Me.Tabla.RowCount = Me.Tabla.RowCount + 1
            posicion = posicion + 1
            Me.Tabla.Item(Desc.Index, posicion).Value = "COSTO DE VENTAS NETO"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            Me.Tabla.Item(Saldof.Index, posicion).Value = Suma

            Me.Tabla.Rows.Add()
            Me.Tabla.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1


        End If
        'Utilidad Bruta
        Me.Tabla.RowCount = Me.Tabla.RowCount + 1
        posicion = posicion + 1
        Me.Tabla.Item(Desc.Index, posicion).Value = "Utilidad Bruta"
        Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
        Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas

        Me.Tabla.Item(Saldof.Index, posicion).Value = Calcula_Utilidad("U")
        Me.Tabla.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas

        Me.Tabla.Rows.Add()
        Me.Tabla.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
        Me.Tabla.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
        posicion = posicion + 1

        'Gastos de Operacion
        Me.Tabla.RowCount = Me.Tabla.RowCount + 1
        posicion = posicion + 1
        Me.Tabla.Item(Desc.Index, posicion).Value = "Gastos de Operacion"
        Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
        Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
        Suma = 0
        sql = "SELECT Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Descripcion,Catalogo_de_Cuentas.Naturaleza FROM Catalogo_de_Cuentas WHERE Clasificacion in ('GGE','GVE') AND Nivel1 >0 AND Nivel2 ='0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  ) ORDER BY Cuenta"
        ds.Clear()
        ds = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = Me.Tabla.RowCount + ds.Tables(0).Rows.Count
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                posicion = posicion + 1
                Me.Tabla.Item(CtaAc.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(1)
                Me.Tabla.Item(Signo.Index, posicion).Value = ds.Tables(0).Rows(i)(2)
                Me.Tabla.Item(Saldof.Index, posicion).Value = Calcula_saldo(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), ds.Tables(0).Rows(i)(0), False)
                Me.Tabla.Item(Tip.Index, posicion).Value = "GO"
                If Me.Tabla.Item(Signo.Index, posicion).Value.ToString.Trim() = "D" Then
                    Suma += Me.Tabla.Item(Saldof.Index, posicion).Value
                Else
                    Suma -= Me.Tabla.Item(Saldof.Index, posicion).Value
                End If
            Next
            Me.Tabla.RowCount = Me.Tabla.RowCount + 1
            posicion = posicion + 1
            Me.Tabla.Item(Desc.Index, posicion).Value = "GASTOS DE OPERACION NETOS"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            Me.Tabla.Item(Saldof.Index, posicion).Value = Suma

            Me.Tabla.Rows.Add()
            Me.Tabla.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1
            'Utilidad de Operacion
            Me.Tabla.RowCount = Me.Tabla.RowCount + 1
            posicion = posicion + 1
            Me.Tabla.Item(Desc.Index, posicion).Value = "Utilidad de Operacion"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas

            Me.Tabla.Item(Saldof.Index, posicion).Value = Calcula_Utilidad("GO")
            Me.Tabla.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas

            Me.Tabla.Rows.Add()
            Me.Tabla.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1

            'Costo Integral de Financiamiento
            Me.Tabla.RowCount = Me.Tabla.RowCount + 1
            posicion = posicion + 1
            Me.Tabla.Item(Desc.Index, posicion).Value = "Costo Integral de Financiamiento"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas

            Me.Tabla.Rows.Add()
            Me.Tabla.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1
        End If


        'Costo Integral
        Suma = 0
        sql = "SELECT Catalogo_de_Cuentas.Nivel1, Catalogo_de_Cuentas.Descripcion,Catalogo_de_Cuentas.Naturaleza FROM Catalogo_de_Cuentas WHERE Clasificacion in ('OGS','GFI','PFI') AND Nivel1 >0 AND Nivel2 ='0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " and catalogo_de_cuentas.Cuenta IN (SELECT Cuenta FROM Cuentas_Con_Saldo WHERE Anio = " & Me.DtInicio.Value.ToString.Substring(6, 4) & " AND Id_Empresa = " & Me.LstCliente.SelectItem & "  ) ORDER BY Cuenta"
        ds.Clear()
        ds = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = Me.Tabla.RowCount + ds.Tables(0).Rows.Count
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                posicion = posicion + 1
                Me.Tabla.Item(CtaAc.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(1)
                Me.Tabla.Item(Signo.Index, posicion).Value = ds.Tables(0).Rows(i)(2)
                Me.Tabla.Item(Saldof.Index, posicion).Value = Calcula_saldo(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10), ds.Tables(0).Rows(i)(0), False)
                Me.Tabla.Item(Tip.Index, posicion).Value = "CI"
                If Me.Tabla.Item(Signo.Index, posicion).Value.ToString.Trim() = "D" Then
                    Suma += Me.Tabla.Item(Saldof.Index, posicion).Value
                Else
                    Suma -= Me.Tabla.Item(Saldof.Index, posicion).Value
                End If
            Next
            Me.Tabla.RowCount = Me.Tabla.RowCount + 1
            posicion = posicion + 1
            Me.Tabla.Item(Desc.Index, posicion).Value = "COSTO FINANCIAMIENTO NETO"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.PaleGreen
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            Me.Tabla.Item(Saldof.Index, posicion).Value = Suma

            Me.Tabla.Rows.Add()
            Me.Tabla.Rows(posicion + 1).DefaultCellStyle.BackColor = Color.FloralWhite
            Me.Tabla.Rows(posicion + 1).Cells(Desc.Index).Style.Font = Negritas
            posicion = posicion + 1

            Me.Tabla.RowCount = Me.Tabla.RowCount + 1

        End If
        posicion = posicion + 1
        Dim saldo As Decimal = 0
        For i As Integer = 0 To Me.Tabla.Rows.Count - 1
            Select Case Me.Tabla.Item(Desc.Index, i).Value
                Case "COSTO FINANCIAMIENTO NETO"
                    saldo -= Me.Tabla.Item(Saldof.Index, i).Value
                Case "Utilidad de Operacion"
                    saldo += Me.Tabla.Item(Saldof.Index, i).Value
            End Select
        Next
        Try
            Me.Tabla.Item(Desc.Index, posicion).Value = "Utilidad o (Perdida) del Ejercicio"
            Me.Tabla.Rows(posicion).DefaultCellStyle.BackColor = Color.RoyalBlue
            Me.Tabla.Rows(posicion).Cells(Desc.Index).Style.Font = Negritas
            Me.Tabla.Item(Saldof.Index, posicion).Value = saldo
            Me.Tabla.Rows(posicion).Cells(Saldof.Index).Style.Font = Negritas
        Catch ex As Exception

        End Try

    End Sub

    Private Function Calcula_saldo(ByVal Fi As String, ByVal FF As String, ByVal cuenta As String, ByVal Evaluar As Boolean)
        Dim saldo As Decimal
        Dim Sql As String = "SELECT sum(Saldo) AS S FROM ("
        Sql &= " SELECT   Case WHEN Naturaleza = 'D' THEN sum(cargo)-sum(abono) WHEN Naturaleza = 'A' THEN sum(abono)-sum(cargo) END  AS Saldo FROM Detalle_Polizas"
        Sql &= " INNER JOIN  Polizas ON Polizas.ID_poliza = Detalle_Polizas.ID_poliza"
        Sql &= " INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.Cuenta = Detalle_Polizas.Cuenta"
        Sql &= " WHERE Catalogo_de_Cuentas.Id_Empresa = " & Me.LstCliente.SelectItem & "   and Detalle_Polizas.Cuenta  IN ( SELECT Cuenta  FROM Catalogo_de_Cuentas WHERE  Nivel1 ='" & cuenta & "' AND Id_Empresa = " & Me.LstCliente.SelectItem & "  ) AND "
        Sql &= " Polizas.Id_Empresa = " & Me.LstCliente.SelectItem & "   and  (Polizas.Fecha_captura >= " & Eventos.Sql_hoy(Fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(FF) & " )"
        Sql &= " GROUP BY  Catalogo_de_Cuentas.Naturaleza  ) AS Tabla"
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            saldo = IIf(IsDBNull(ds.Tables(0).Rows(0)(0)) = True, 0, ds.Tables(0).Rows(0)(0))
        Else
            saldo = 0
        End If
        Dim Sal As Decimal = 0
        Dim Acumulado As Decimal = Calcula_Saldos_InicialesP(cuenta.ToString().PadRight(16, "0"), Fi.ToString.Substring(6, 4), Me.LstCliente.SelectItem, "  ( Polizas.Fecha_captura <" & Eventos.Sql_hoy(Fi) & " )")
        Sal = Eventos.Calcula_Saldos_Iniciales(cuenta.ToString().PadRight(16, "0"), Fi.ToString.Substring(6, 4) - 1, Me.LstCliente.SelectItem) + Acumulado
        If Evaluar = True Then


            If (Eventos.ObtenerValorDB("Catalogo_de_Cuentas", "Naturaleza", " Nivel1 = " & cuenta.ToString().Substring(0, 4) & " AND Nivel2=0000 And Id_Empresa= " & Me.LstCliente.SelectItem & "", True)).ToString().Trim() = "A" Then
                Sal = Sal * -1
            Else
                Sal = Sal
            End If
        End If

        Return Sal + saldo
    End Function
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
    Private Function Calcula_Utilidad(ByVal Tipo As String)
        Dim dinero As Decimal = 0
        Dim a As Decimal = 0
        Dim d As Decimal = 0
        For i As Integer = 0 To Me.Tabla.Rows.Count - 1
            If Me.Tabla.Item(Desc.Index, i).Value IsNot Nothing Then
                If Tipo = "U" Then
                    If Me.Tabla.Item(Desc.Index, i).Value.ToString.Trim() = "INGRESOS NETOS" Then
                        dinero += Me.Tabla.Item(Saldof.Index, i).Value
                    ElseIf Me.Tabla.Item(Desc.Index, i).Value.ToString.Trim() = "COSTO DE VENTAS NETO" Then
                        dinero -= Me.Tabla.Item(Saldof.Index, i).Value
                    End If
                ElseIf Tipo = "GO" Then

                    If Me.Tabla.Item(Desc.Index, i).Value.ToString.Trim() = "Utilidad Bruta" Then
                        dinero += Me.Tabla.Item(Saldof.Index, i).Value
                    ElseIf Me.Tabla.Item(Desc.Index, i).Value.ToString.Trim() = "GASTOS DE OPERACION NETOS" Then
                        dinero -= Me.Tabla.Item(Saldof.Index, i).Value
                    End If
                End If

            End If

        Next
        Return dinero
    End Function
    Private Sub Limpiar()
        Me.Tabla.Rows.Clear()

    End Sub


    Private Sub CmdPdf_Click(sender As Object, e As EventArgs) Handles CmdPdf.Click
        CrearEstado()
    End Sub
    Private Sub CrearEstado()
        Dim DT As New DataTable
        With DT
            .Columns.Add("Descripcion")
            .Columns.Add("Naturaleza")
            .Columns.Add("Saldo", Type.GetType("System.Decimal"))
        End With
        For Each dr As DataGridViewRow In Me.Tabla.Rows
            DT.Rows.Add(UCase(dr.Cells(Desc.Index).Value), dr.Cells(Signo.Index).Value, dr.Cells(Saldof.Index).Value * 1)
        Next

        Dim Cr As New EstadodeResultadosPDF
        Cr.SetDataSource(DT)
        Dim Reporte As New ReporteBalanzaGeneral
        Reporte.CrvBalanza.ReportSource = Cr


        Dim FechaF As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim ff As New CrystalDecisions.Shared.ParameterValues()
        FechaF.Value = Me.Dtfin.Value.ToString.Substring(0, 10)
        ff.Add(FechaF)
        Cr.DataDefinition.ParameterFields("Fecha").ApplyCurrentValues(ff)
        Dim RFC As String = Eventos.ObtenerValorDB("Empresa", "RFC", " Id_Empresa = " & Me.LstCliente.SelectItem & "", True)
        Dim Cliente As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim Cl As New CrystalDecisions.Shared.ParameterValues()
        Cliente.Value = Me.LstCliente.SelectText & vbCr & RFC
        Cl.Add(Cliente)
        Cr.DataDefinition.ParameterFields("Cliente").ApplyCurrentValues(Cl)
        Dim Clt As DataSet = Eventos.Obtener_DS("select * from Empresa where Id_Empresa = " & Me.LstCliente.SelectItem & " ")

        Cliente.Value = Clt.Tables(0).Rows(0)("Direccion")
        Cl.Add(Cliente)
        Cr.DataDefinition.ParameterFields("Direccion").ApplyCurrentValues(Cl)



        Dim Archivo As String = "C:\Program Files\Contable\SetupProyectoContable\Estado\PDF\Estado" & RFC & ".pdf"
        If Directory.Exists("C:\Program Files\Contable\SetupProyectoContable\Estado\PDF") = False Then
            ' si no existe la carpeta se crea
            Directory.CreateDirectory("C:\Program Files\Contable\SetupProyectoContable\Estado\PDF")
        End If
        If Directory.Exists(Archivo) Then
            My.Computer.FileSystem.DeleteFile(Archivo)
        End If
        Cr.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Archivo)
        Process.Start(Archivo)
    End Sub

    Private Sub CmdExp_Click(sender As Object, e As EventArgs) Handles CmdExp.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Tabla.RowCount > 0 Then
            RadMessageBox.Show("Este Proceso puede tardar dependiendo de la información a exportar, presione Aceptar y espere a que el proceso termine...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.Tabla.RowCount - 1
            Dim m_Excel As Microsoft.Office.Interop.Excel.Application
            Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook
            Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet

            m_Excel = New Microsoft.Office.Interop.Excel.Application


            objLibroExcel = m_Excel.Workbooks.Add()
            objHojaExcel = objLibroExcel.Worksheets(1)
            objHojaExcel.Name = "Estado de Resultados"
            objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            objHojaExcel.Activate()
            Dim i As Integer, j As Integer
            Dim Item As Integer = 1
            For i = 0 To Tabla.Columns.Count - 1
                If i = 1 Or i = 3 Then
                    objHojaExcel.Cells(1, Item) = Me.Tabla.Columns.Item(i).HeaderCell.Value
                    Item += 1
                End If
            Next
            Dim Valor As String
            For i = 0 To Tabla.RowCount - 1
                frm.Barra.Value = i
                For j = 0 To Tabla.Columns.Count - 1
                    Valor = IIf(IsDBNull(Me.Tabla.Item(j, i).Value), "", Me.Tabla.Item(j, i).Value)
                    If j = 1 Then
                        objHojaExcel.Cells(i + 2, 1) = Valor
                    ElseIf j = 3 Then
                        m_Excel.Workbooks(1).Worksheets(1).Columns(2).NumberFormat = "$#,##0.00_);[Red]($#,##0.00)"
                        objHojaExcel.Cells(i + 2, 2) = Valor
                    End If
                Next
            Next
            frm.Close()
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            m_Excel.Visible = True
        Else
            RadMessageBox.Show("No hay registros a exportar....", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
End Class