Imports Telerik.WinControls
Public Class VerificadorPolizas
    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        If Me.Tabla.ColumnCount > 0 Then
            Me.Tabla.Columns.Clear()
        End If

    End Sub

    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim sql As String = ""

        sql = "SELECT * FROM (SELECT   Polizas.ID_anio as Año ,Polizas.ID_mes as Mes,Tipos_Poliza_Sat.Clave as Tipo , convert(INT,Polizas.Num_Pol ) as Número , 	convert(INT,Polizas.ID_dia ) AS Día ,Polizas.Concepto as Descripción,
                        sum(detalle_polizas.Cargo ) AS Cargos,sum(detalle_polizas.Abono ) AS Abonos,   sum(detalle_polizas.Cargo ) - sum (detalle_polizas.Abono )  AS Diferecia, convert(VARCHAR,Polizas.Fecha,103) AS Fecha 
                        FROM Polizas
                        INNER JOIN Tipos_Poliza_Sat ON Tipos_Poliza_Sat.Id_Tipo_Pol_Sat = Polizas.Id_Tipo_Pol_Sat 
                        INNER JOIN Detalle_Polizas ON Detalle_Polizas.ID_poliza = Polizas.ID_poliza 
                        WHERE Polizas.Id_Empresa =" & Me.lstCliente.SelectItem & " and Polizas.Fecha >=" & Eventos.Sql_hoy(Me.DtInicio.Value) & "  AND Polizas.Fecha <= " & Eventos.Sql_hoy(Me.Dtfin.Value) & "
                        GROUP BY Polizas.ID_poliza , Polizas.ID_anio ,Polizas.ID_mes,Tipos_Poliza_Sat.Clave ,Polizas.Num_Pol ,Polizas.ID_dia ,Polizas.Concepto,Polizas.Fecha ) AS Tabla
                        ORDER BY Año ,Mes,Tipo ,Número ,día"
        'Dim ds As DataSet = Eventos.Obtener_DS(sql)
        'If ds.Tables(0).Rows.Count > 0 Then
        '    Me.Tabla.RowCount = ds.Tables(0).Rows.Count
        '    Me.Barra.Maximum = Me.Tabla.RowCount - 1
        '    Me.Barra.Minimum = 0
        'Me.Barra.Value1 = 0
        'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
        '        Me.Tabla.Item(0, i).Value = ds.Tables(0).Rows(i)(0) 'Recupera el valor de la cuenta de Mayor
        '        Me.Tabla.Item(1, i).Value = ds.Tables(0).Rows(i)(1)
        '        Me.Tabla.Item(2, i).Value = ds.Tables(0).Rows(i)(2)
        '        Me.Tabla.Item(3, i).Value = ds.Tables(0).Rows(i)(3)
        '        Me.Tabla.Item(4, i).Value = ds.Tables(0).Rows(i)(4)
        '        Me.Tabla.Item(5, i).Value = ds.Tables(0).Rows(i)(5)
        '        Me.Tabla.Item(6, i).Value = ds.Tables(0).Rows(i)(6)
        '        Me.Tabla.Item(7, i).Value = ds.Tables(0).Rows(i)(7)
        '        Me.Tabla.Item(8, i).Value = ds.Tables(0).Rows(i)(8)
        '        Me.Tabla.Item(9, i).Value = ds.Tables(0).Rows(i)(9)
        '    If Me.Barra.Value1 = Me.Barra.Maximum Then
        '        Me.Barra.Minimum = 0
        '        Me.Cursor = Cursors.Arrow
        '        Me.Barra.Value1 = 0
        '        RadMessageBox.Show("Proceso Terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

        '    Else
        '        Me.Barra.Value1 += 1
        '    End If

        'Next
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                Me.Tabla.DataSource = ds.Tables(0).DefaultView
                Me.Tabla.Columns(6).DefaultCellStyle.Format = "C2"
                Me.Tabla.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Me.Tabla.Columns(7).DefaultCellStyle.Format = "C2"
                Me.Tabla.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                Me.Tabla.Columns(8).DefaultCellStyle.Format = "C2"
                Me.Tabla.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Catch ex As Exception

            End Try

            Me.LbLtOTAL.Text = "Total de Polizas: " & Me.Tabla.RowCount
        End If




    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub VerificadorPolizas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_clientes()
        Eventos.DiseñoTabla(Me.Tabla)
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

    Private Sub CmdExp_Click(sender As Object, e As EventArgs) Handles CmdExp.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim Dato As Object
        If Me.Tabla.RowCount > 0 Then
            If Me.Tabla.Columns.Count > 256 Then
                RadMessageBox.Show("El rango de fechas sobrepasa las columnas de una hoja de excel, disminuye el rango...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
                Exit Sub
            End If
            Dim excel As Microsoft.Office.Interop.Excel.Application = Eventos.NuevoExcel("vacio", False)
            For col As Integer = 1 To Me.Tabla.Columns.Count - 1
                Eventos.EscribeExcel(excel, 1, col, Me.Tabla.Columns(col).HeaderText)
            Next
            For i As Integer = 0 To Me.Tabla.RowCount - 1
                For j As Integer = 1 To Me.Tabla.Columns.Count - 1
                    Dato = Valor(Me.Tabla.Item(j, i).Value)
                    Eventos.EscribeExcelHojas(excel, i + 2, j, Valor(Dato), 1, "Hoja1")
                Next
            Next
            Eventos.Mostrar_Excel(excel)
        Else
            RadMessageBox.Show("No hay datos para exportar", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If
    End Sub
End Class
