Imports System.IO
Imports Telerik.WinControls
Public Class Diario
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        Limpiar()
    End Sub
    Private Sub Limpiar()
        Me.Tabla.Rows.Clear()
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
            objHojaExcel.Name = "Diario"
            objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            objHojaExcel.Activate()
            Dim i As Integer, j As Integer
            For i = 0 To Tabla.Columns.Count - 1
                objHojaExcel.Cells(1, i + 1) = Me.Tabla.Columns.Item(i).HeaderCell.Value
            Next
            Dim Valor As String
            For i = 0 To Tabla.RowCount - 1
                frm.Barra.Value = i
                For j = 0 To Tabla.Columns.Count - 1

                    Valor = IIf(IsDBNull(Me.Tabla.Item(j, i).Value), "", Me.Tabla.Item(j, i).Value)

                    If IsNumeric(Valor) Then
                        If j = 8 Then
                            objHojaExcel.Cells(i + 2, j + 1) = Valor.Substring(0, 4) & "-" & Valor.Substring(4, 4) & "-" & Valor.Substring(8, 4) & "-" & Valor.Substring(12, 4)
                        ElseIf j = 10 Or j = 11 Then
                            m_Excel.Workbooks(1).Worksheets(1).Columns(j + 1).NumberFormat = "$#,##0.00_);[Red]($#,##0.00)"
                            objHojaExcel.Cells(i + 2, j + 1) = Valor

                        End If
                    Else
                        objHojaExcel.Cells(i + 2, j + 1) = Valor
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

    Private Sub Diario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar()
    End Sub
    Private Sub Cargar()
        Me.LstCliente.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social FROM     Empresa  ")
        Me.LstCliente.SelectItem = 1
        Eventos.DiseñoTabla(Me.Tabla)
    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        Limpiar()
        Eventos.Cuentas(Me.DtInicio.Value.ToString.Substring(6, 4), Me.LstCliente.SelectItem)
        Polizas(Me.DtInicio.Value.ToString.Substring(0, 10), Me.Dtfin.Value.ToString.Substring(0, 10))
    End Sub

    Private Sub Polizas(ByVal Fi As String, ByVal Ff As String)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim sql As String = "SELECT Polizas.ID_poliza, Polizas.ID_anio, Polizas.ID_mes, Polizas.ID_dia, Polizas.Num_Pol, Polizas.Concepto, Polizas.Fecha_captura AS Fecha, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre AS clave
	                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa INNER JOIN
	                            Tipos_Poliza_Sat ON Empresa.Id_Empresa = Tipos_Poliza_Sat.Id_Empresa AND Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat 
	                            WHERE Polizas.Fecha_captura >= " & Eventos.Sql_hoy(Fi) & " AND Polizas.Fecha_captura <=" & Eventos.Sql_hoy(Ff) & " and polizas.Id_Empresa = " & Me.LstCliente.SelectItem & ""



        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then ' buscar todas las polizas del periodo
            Me.Tabla.RowCount = ds.Tables(0).Rows.Count
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.Tabla.RowCount - 1
            Me.Cursor = Cursors.AppStarting
            Dim posicion As Integer = 0
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Me.Tabla.Item(Pol.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                Me.Tabla.Item(anio.Index, posicion).Value = ds.Tables(0).Rows(i)(1)
                Me.Tabla.Item(Mess.Index, posicion).Value = ds.Tables(0).Rows(i)(2)
                Me.Tabla.Item(Day.Index, posicion).Value = ds.Tables(0).Rows(i)(3)
                Me.Tabla.Item(P.Index, posicion).Value = ds.Tables(0).Rows(i)(4)
                Me.Tabla.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(5)
                Me.Tabla.Item(Tip.Index, posicion).Value = ds.Tables(0).Rows(i)(7)
                Me.Tabla.Item(Fech.Index, posicion).Value = ds.Tables(0).Rows(i)(6)
                Dim suma1, suma2 As Decimal
                ' Buscar Cargos

                Dim Cargos As String = "SELECT 
	                                            Detalle_Polizas.Cuenta,Catalogo_de_Cuentas.Descripcion,Detalle_Polizas.Cargo,Detalle_Polizas.Descripcion 
	                                             FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
	                                        INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
	                                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
	                                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa 
	                                            WHERE Polizas.id_poliza = '" & Trim(ds.Tables(0).Rows(i)(0)) & "' AND detalle_polizas.Cargo >0"

                Dim Car As DataSet = Eventos.Obtener_DS(Cargos)
                If Car.Tables(0).Rows.Count > 0 Then
                    Me.Tabla.RowCount = Me.Tabla.RowCount + Car.Tables(0).Rows.Count
                    For C As Integer = 0 To Car.Tables(0).Rows.Count - 1
                        posicion = posicion + 1
                        Me.Tabla.Item(Pol.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                        'Me.Tabla.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(5)
                        Me.Tabla.Item(Desc.Index, posicion).Value = Car.Tables(0).Rows(C)(3)
                        Me.Tabla.Item(Cta.Index, posicion).Value = Car.Tables(0).Rows(C)(0).ToString().Substring(0, 4) & "-" & Car.Tables(0).Rows(C)(0).ToString().Substring(4, 4) & "-" & Car.Tables(0).Rows(C)(0).ToString().Substring(8, 4) & "-" & Car.Tables(0).Rows(C)(0).ToString().Substring(12, 4)

                        Buscar_Madre(Car.Tables(0).Rows(C)(0), posicion)

                        Me.Tabla.Item(NCta.Index, posicion).Value = Car.Tables(0).Rows(C)(1)
                        Me.Tabla.Item(carg.Index, posicion).Value = Car.Tables(0).Rows(C)(2)
                        Me.Tabla.Item(Abon.Index, posicion).Value = 0

                        suma1 = suma1 + Car.Tables(0).Rows(C)(2)
                    Next
                Else
                    suma1 = 0
                End If
                'Buscar abonos
                Dim Abonos As String = "SELECT 
	                                            Detalle_Polizas.Cuenta,Catalogo_de_Cuentas.Descripcion,Detalle_Polizas.Abono ,Detalle_Polizas.Descripcion 
	                                             FROM Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
	                                        INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
	                                        INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
	                                        INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa 
	                                            WHERE Polizas.id_poliza = '" & Trim(ds.Tables(0).Rows(i)(0)) & "' AND detalle_polizas.Abono >0"
                Dim ab As DataSet = Eventos.Obtener_DS(Abonos)
                If ab.Tables(0).Rows.Count > 0 Then
                    Me.Tabla.RowCount = Me.Tabla.RowCount + ab.Tables(0).Rows.Count
                    For A As Integer = 0 To ab.Tables(0).Rows.Count - 1
                        posicion = posicion + 1
                        Me.Tabla.Item(Pol.Index, posicion).Value = ds.Tables(0).Rows(i)(0)
                        'Me.Tabla.Item(Desc.Index, posicion).Value = ds.Tables(0).Rows(i)(5)
                        Me.Tabla.Item(Desc.Index, posicion).Value = ab.Tables(0).Rows(A)(3)
                        Me.Tabla.Item(Cta.Index, posicion).Value = ab.Tables(0).Rows(A)(0).ToString().Substring(0, 4) & "-" & ab.Tables(0).Rows(A)(0).ToString().Substring(4, 4) & "-" & ab.Tables(0).Rows(A)(0).ToString().Substring(8, 4) & "-" & ab.Tables(0).Rows(A)(0).ToString().Substring(12, 4)

                        Buscar_Madre(ab.Tables(0).Rows(A)(0), posicion)
                        Me.Tabla.Item(NCta.Index, posicion).Value = ab.Tables(0).Rows(A)(1)
                        Me.Tabla.Item(carg.Index, posicion).Value = 0
                        Me.Tabla.Item(Abon.Index, posicion).Value = ab.Tables(0).Rows(A)(2)

                        suma2 = suma2 + ab.Tables(0).Rows(A)(2)
                    Next
                Else
                    suma2 = 0
                End If

                posicion = posicion + 1
                Me.Tabla.RowCount = Me.Tabla.RowCount + 1 ' AGREGAR REGISTRO DE TOTALES
                Me.Tabla.Item(carg.Index - 1, posicion).Value = "Totales Poliza"
                Me.Tabla.Item(carg.Index, posicion).Value = suma1
                Me.Tabla.Item(Abon.Index, posicion).Value = suma2
                suma1 = 0
                suma2 = 0
                posicion = posicion + 1
                frm.Barra.Value = i
            Next
            Colorear()
            frm.Close()
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        Else
            'No hay Registros

        End If

    End Sub
    Private Sub Colorear()
        Dim contador As Integer = 0
        For Each Fila As DataGridViewRow In Tabla.Rows
            If Fila.Cells(6).Value <> Nothing Then
                Fila.DefaultCellStyle.BackColor = Color.PaleGreen
                contador = contador + 1
            End If
        Next

    End Sub
    Private Sub Buscar_Madre(ByVal Cta As String, ByVal i As Integer)
        Dim Madre As String = ""
        Cta = Cta.ToString.Replace("-", "")
        If Cta.Substring(12, 4) = "0000" And Cta.Substring(8, 4) > "0000" Then ' Segundo nivel
            Dim Cuenta As String = " select Descripcion from Catalogo_de_Cuentas where Nivel1 = '" & Cta.Substring(0, 4) & "' AND Nivel2 = '" & Cta.Substring(4, 4) & "' AND Nivel3 = '0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " "
            Dim ds As DataSet = Eventos.Obtener_DS(Cuenta)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.Tabla.Item(Day.Index, i).Value = Trim(ds.Tables(0).Rows(0)(0))
                Madre = Trim(ds.Tables(0).Rows(0)(0))
                Cuenta = "select Descripcion from Catalogo_de_Cuentas where Nivel1 = '" & Cta.Substring(0, 4) & "' AND Nivel2 =  '0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " "
                ds.Clear()
                ds = Eventos.Obtener_DS(Cuenta)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.Tabla.Item(anio.Index, i).Value = Trim(ds.Tables(0).Rows(0)(0))
                    Madre = Trim(ds.Tables(0).Rows(0)(0))
                End If
            End If
        ElseIf Cta.Substring(12, 4) > "0000" And Cta.Substring(8, 4) > "0000" Then 'Tercer Nivel
            Dim Cuenta As String = " select Descripcion from Catalogo_de_Cuentas where Nivel1 = '" & Cta.Substring(0, 4) & "' AND Nivel2 = '" & Cta.Substring(4, 4) & "' AND Nivel3 = '" & Cta.Substring(8, 4) & "' AND Nivel4 = '0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " "
            Dim ds As DataSet = Eventos.Obtener_DS(Cuenta)
            If ds.Tables(0).Rows.Count > 0 Then
                Madre = Trim(ds.Tables(0).Rows(0)(0))
                Me.Tabla.Item(Tip.Index, i).Value = Trim(ds.Tables(0).Rows(0)(0))

                Cuenta = " select Descripcion from Catalogo_de_Cuentas where Nivel1 = '" & Cta.Substring(0, 4) & "' AND Nivel2 = '" & Cta.Substring(4, 4) & "' AND Nivel3 = '0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " "
                ds.Clear()
                ds = Eventos.Obtener_DS(Cuenta)
                If ds.Tables(0).Rows.Count > 0 Then
                    Me.Tabla.Item(Day.Index, i).Value = Trim(ds.Tables(0).Rows(0)(0))
                    Madre = Trim(ds.Tables(0).Rows(0)(0))
                    Cuenta = "select Descripcion from Catalogo_de_Cuentas where Nivel1 = '" & Cta.Substring(0, 4) & "' AND Nivel2 =  '0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " "
                    ds.Clear()
                    ds = Eventos.Obtener_DS(Cuenta)
                    If ds.Tables(0).Rows.Count > 0 Then
                        Me.Tabla.Item(anio.Index, i).Value = Trim(ds.Tables(0).Rows(0)(0))
                        Madre = Trim(ds.Tables(0).Rows(0)(0))
                    End If
                End If
            End If
        ElseIf Cta.Substring(4, 4) > "0000" And Cta.Substring(8, 4) = "0000" And Cta.Substring(12, 4) = "0000" Then 'Primer Nivel
            Dim Cuenta As String = " select Descripcion from Catalogo_de_Cuentas where Nivel1 = '" & Cta.Substring(0, 4) & "' AND Nivel2 > '0000' AND Nivel3='0000' AND Nivel4 = '0000' and Id_Empresa = " & Me.LstCliente.SelectItem & " "
            Dim ds As DataSet = Eventos.Obtener_DS(Cuenta)
            If ds.Tables(0).Rows.Count > 0 Then
                Madre = Trim(ds.Tables(0).Rows(0)(0))
                Me.Tabla.Item(anio.Index, i).Value = Trim(ds.Tables(0).Rows(0)(0))
            End If
        Else

        End If
        ' Return Madre
    End Sub



    Private Sub CmdPdf_Click(sender As Object, e As EventArgs) Handles CmdPdf.Click
        DiarioPDF()
    End Sub
    Private Sub Diario()
        Dim DT As New DataTable
        With DT
            .Columns.Add("Año")
            .Columns.Add("Mes")
            .Columns.Add("Dia")
            .Columns.Add("Poliza")
            .Columns.Add("Tipo_Poliza")
            .Columns.Add("Descripcion")
            .Columns.Add("Cuenta")
            .Columns.Add("Nombre_Cuenta")
            .Columns.Add("Cargo", Type.GetType("System.Decimal"))
            .Columns.Add("Abono", Type.GetType("System.Decimal"))
            .Columns.Add("Haber", Type.GetType("System.Decimal"))
            .Columns.Add("Saldo_final", Type.GetType("System.Decimal"))
        End With
        For Each dr As DataGridViewRow In Me.Tabla.Rows

            DT.Rows.Add(dr.Cells(1).Value, dr.Cells(2).Value, dr.Cells(3).Value, dr.Cells(4).Value, dr.Cells(5).Value, dr.Cells(7).Value, dr.Cells(8).Value, dr.Cells(9).Value, dr.Cells(10).Value * 1, dr.Cells(11).Value * 1)
        Next

        Dim Cr As New ReporteDiarioPDF
        Cr.SetDataSource(DT)
        Dim Reporte As New ReporteBalanzaGeneral
        Reporte.CrvBalanza.ReportSource = Cr

        Dim FechaD As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim Rptinv As New CrystalDecisions.Shared.ParameterValues()
        FechaD.Value = Me.DtInicio.Value.ToString.Substring(0, 10)
        Rptinv.Add(FechaD)
        Cr.DataDefinition.ParameterFields("FechaI").ApplyCurrentValues(Rptinv)

        Dim FechaF As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim ff As New CrystalDecisions.Shared.ParameterValues()
        FechaF.Value = Me.Dtfin.Value.ToString.Substring(0, 10)
        ff.Add(FechaF)
        Cr.DataDefinition.ParameterFields("FechaF").ApplyCurrentValues(ff)

        Dim Cliente As New CrystalDecisions.Shared.ParameterDiscreteValue()
        Dim Cl As New CrystalDecisions.Shared.ParameterValues()
        Cliente.Value = Me.LstCliente.SelectText
        Cl.Add(Cliente)
        Cr.DataDefinition.ParameterFields("Cliente").ApplyCurrentValues(Cl)
        Dim Clt As DataSet = Eventos.Obtener_DS("select * from Empresa where Id_Empresa = " & Me.LstCliente.SelectItem & " ")

        Cliente.Value = Clt.Tables(0).Rows(0)("Direccion")
        Cl.Add(Cliente)
        Cr.DataDefinition.ParameterFields("Direccion").ApplyCurrentValues(Cl)


        Reporte.ShowDialog()
    End Sub
    Private Sub DiarioPDF()
        Dim DT As New DataTable
        With DT
            .Columns.Add("Id_Poliza")
            .Columns.Add("Anio")
            .Columns.Add("Mes")
            .Columns.Add("Dia")
            .Columns.Add("Numero")
            .Columns.Add("Tipo")
            .Columns.Add("Descricpcion")
            .Columns.Add("CuentaDe")
            .Columns.Add("Cuenta")
            .Columns.Add("Nombre")
            .Columns.Add("Cargo", Type.GetType("System.Decimal"))
            .Columns.Add("Abono", Type.GetType("System.Decimal"))
        End With
        Dim An, Ms, Da, Pl, Tp As String
        For Each dr As DataGridViewRow In Me.Tabla.Rows
            If IsNumeric(dr.Cells(anio.Index).Value) And dr.Cells(anio.Index).Value IsNot Nothing Then
                DT.Rows.Add(dr.Cells(Pol.Index).Value, dr.Cells(anio.Index).Value, dr.Cells(Mess.Index).Value, dr.Cells(Day.Index).Value, dr.Cells(P.Index).Value, dr.Cells(Tip.Index).Value, dr.Cells(Desc.Index).Value, Nothing, Nothing, Nothing, Nothing, Nothing)
                An = dr.Cells(anio.Index).Value.ToString()
                Ms = dr.Cells(Mess.Index).Value.ToString()
                Da = dr.Cells(Day.Index).Value.ToString()
                Pl = dr.Cells(P.Index).Value.ToString()
                Tp = dr.Cells(Tip.Index).Value.ToString()
            ElseIf IsNumeric(dr.Cells(anio.Index).Value) = False And dr.Cells(anio.Index).Value IsNot Nothing Then
                DT.Rows.Add(dr.Cells(Pol.Index).Value, An, Ms, Da, Pl, dr.Cells(Desc.Index).Value, dr.Cells(Desc.Index).Value, dr.Cells(anio.Index).Value, dr.Cells(Cta.Index).Value, dr.Cells(NCta.Index).Value, dr.Cells(carg.Index).Value * 1, dr.Cells(Abon.Index).Value * 1)
            End If
        Next

        Try


            Dim Cr As New ReporteDiarioPDF
            Cr.Database.Tables("PolizasHeaders").SetDataSource(DT)

            Dim Reporte As New ReporteBalanzaGeneral
            Reporte.CrvBalanza.ReportSource = Cr

            Dim FechaD As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim Rptinv As New CrystalDecisions.Shared.ParameterValues()
            FechaD.Value = Me.DtInicio.Value.ToString.Substring(0, 10)
            Rptinv.Add(FechaD)
            Cr.DataDefinition.ParameterFields("FechaI").ApplyCurrentValues(Rptinv)

            Dim FechaF As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim ff As New CrystalDecisions.Shared.ParameterValues()
            FechaF.Value = Me.Dtfin.Value.ToString.Substring(0, 10)
            ff.Add(FechaF)
            Cr.DataDefinition.ParameterFields("FechaF").ApplyCurrentValues(ff)

            Dim RFC As String = Eventos.ObtenerValorDB("Empresa", "Reg_fed_causantes", " Id_Empresa = " & Me.LstCliente.SelectItem & "", True)
            Dim Cliente As New CrystalDecisions.Shared.ParameterDiscreteValue()
            Dim Cl As New CrystalDecisions.Shared.ParameterValues()
            Cliente.Value = Me.LstCliente.SelectText & vbCr & RFC
            Cl.Add(Cliente)
            Cr.DataDefinition.ParameterFields("Cliente").ApplyCurrentValues(Cl)
            Dim Clt As DataSet = Eventos.Obtener_DS("select * from Empresa where Id_Empresa = " & Me.LstCliente.SelectItem & " ")

            Cliente.Value = Clt.Tables(0).Rows(0)("Direccion")
            Cl.Add(Cliente)
            Cr.DataDefinition.ParameterFields("Direccion").ApplyCurrentValues(Cl)



            Dim Archivo As String = "C:\Program Files\Contable\SetupProyectoContable\Diario\PDF\Diario" & RFC & ".pdf"
            If Directory.Exists("C:\Program Files\Contable\SetupProyectoContable\Diario\PDF") = False Then
                ' si no existe la carpeta se crea
                Directory.CreateDirectory("C:\Program Files\Contable\SetupProyectoContable\Diario\PDF")
            End If
            If Directory.Exists(Archivo) Then
                My.Computer.FileSystem.DeleteFile(Archivo)
            End If
            Cr.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Archivo)
            Process.Start(Archivo)
        Catch ex As Exception

        End Try
    End Sub
End Class
