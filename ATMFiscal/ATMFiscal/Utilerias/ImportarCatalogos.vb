Imports System.Reflection
Imports Microsoft.Office.Interop
Imports System.ComponentModel
Imports Telerik.WinControls

Public Class ImportarCatalogos
    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Dim LCuentas As New List(Of Cuentas)
    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Public Class Cuentas
        Public Property Cuenta As String

    End Class
    Private Sub CmdImportar_Click(sender As Object, e As EventArgs) Handles CmdImportar.Click
        Buscar()
    End Sub
    Private Sub Buscar()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.Tabla.Columns.Count > 0 Then
            Me.Tabla.Columns.Clear()
        End If
        Dim Sql As String = " SELECT 	Nivel1,	Nivel2,	Nivel3,	Nivel4,  Nivel1 +'-'+ Nivel2  +'-'+	Nivel3 +'-'+ Nivel4 as [Cuenta Completa]	   ,
    Descripcion,	Naturaleza,	Clasificacion,	Codigo_Agrupador, RFC,convert(INT,Balanza) AS Balanza,convert(INT,Cta_ceros) AS Cta_ceros,
    convert(INT,Cta_Cargo_Cero) AS Cta_Cargo_Cero,		convert(INT,Cta_Abono_Cero) AS Cta_Abono_Cero,
    convert(INT,Balance) AS Balance,		convert(INT,Estado_de_Resultados) AS Estado_de_Resultados,
    CuentaEnlace  FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & "   ORDER BY  Nivel1,	Nivel2,	Nivel3,	Nivel4 "
        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.DataSource = ds.Tables(0).DefaultView
        End If
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
    End Sub
    Private Sub ExportarCatalogo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
    End Sub
    Private Sub CmdExportar_Click(sender As Object, e As EventArgs) Handles CmdExportar.Click

        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim frm As New DialogExpExcel
        frm.Show()
        frm.Text = "Exportando Catalogo de Cuentas de " & Me.lstCliente.SelectText & " por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = 1
        Dim Excel As Excel.Application = Eventos.NuevoExcel("CatE", False)
        Try
            Me.Tabla.SelectAll()
            Dim dataObj As DataObject = Me.Tabla.GetClipboardContent
            Eventos.ExMasivo(Excel, dataObj, 1, "A2", "Catalogo")

        Catch ex As Exception

        End Try

        frm.Barra.Value1 += 1
        frm.Close()
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Excel.Visible = True
        Excel = Nothing

    End Sub
    Private Sub CmdImport_Click(sender As Object, e As EventArgs) Handles CmdImport.Click
        Eventos.LlenarDataGrid_DSCatalogo(Eventos.CargarExcelCatN("Catalogo"), Me.Tabla)
        Me.Tabla.Rows.RemoveAt(0)
        Codificar()
    End Sub
    Private Sub Codificar()

        Dim P As DataSet = Eventos.Obtener_DS("SELECT  Nivel1 +'-'+ Nivel2  +'-'+	Nivel3 +'-'+ Nivel4 as cuenta FROM Catalogo_de_Cuentas where Id_Empresa = " & Me.lstCliente.SelectItem & " ")
        If P.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To P.Tables(0).Rows.Count - 1
                LCuentas.Add(New Cuentas() With {.Cuenta = P.Tables(0).Rows(i)("cuenta")})
            Next
            If Me.Tabla.Rows.Count > 0 Then
                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Text = "Cruzando catalogos por favor espere..."
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = Me.Tabla.Rows.Count

                For i As Integer = 0 To Me.Tabla.Rows.Count - 1
                    Dim valor As Cuentas
                    valor = LCuentas.Find(Function(S) S.Cuenta = Me.Tabla.Item(4, i).Value.ToString.Trim())
                    Try
                        If valor.Cuenta = Me.Tabla.Item(4, i).Value.ToString.Trim() Then
                            Me.Tabla.Item(4, i).Style.ForeColor = Color.Red
                        Else
                            Me.Tabla.Item(4, i).Style.ForeColor = Color.DarkBlue
                        End If
                    Catch ex As Exception
                        LCuentas.Add(New Cuentas() With {.Cuenta = Me.Tabla.Item(4, i).Value.ToString.Trim()})

                        Me.Tabla.Item(4, i).Style.ForeColor = Color.Purple
                    End Try
                    frm.Barra.Value += 1
                Next
                frm.Close()
                RadMessageBox.SetThemeName("MaterialBlueGrey")
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            End If
        Else
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Text = "Cruzando catalogos por favor espere..."
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.Tabla.Rows.Count
            For i As Integer = 0 To Me.Tabla.Rows.Count - 1
                Dim valor As Cuentas
                valor = LCuentas.Find(Function(S) S.Cuenta = Me.Tabla.Item(4, i).Value.ToString.Trim())
                Try
                    If valor.Cuenta = Me.Tabla.Item(4, i).Value.ToString.Trim() Then
                        Me.Tabla.Item(4, i).Style.ForeColor = Color.Red
                    Else
                        Me.Tabla.Item(4, i).Style.ForeColor = Color.DarkBlue
                    End If
                Catch ex As Exception
                    LCuentas.Add(New Cuentas() With {.Cuenta = Me.Tabla.Item(4, i).Value.ToString.Trim()})

                    Me.Tabla.Item(4, i).Style.ForeColor = Color.Purple
                End Try
                frm.Barra.Value += 1
            Next
            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        SegundoPlano.RunWorkerAsync(Me.Tabla)
        Control.CheckForIllegalCrossThreadCalls = False

        Me.Tabla.Enabled = True
    End Sub
    Private Sub Guardar(ByVal Fila As String)

        If Eventos.Comando_sql(Fila) > 0 Then

        End If
    End Sub

    Private Sub SegundoPlano_DoWork(sender As Object, e As DoWorkEventArgs) Handles SegundoPlano.DoWork
        My.Forms.Inicio.txtServerDB.Text = serV
        Try
            Calcular1()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Calcular1()
        Dim dt As DataTable
        Dim Contador As Integer = 0

        Dim frm As New BarraProcesovb
        frm.Show()
        frm.Text = "Guardando catalogo por favor espere..."
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Me.Tabla.Rows.Count
        Dim Sql As String = ""
        For i As Integer = 0 To Me.Tabla.Rows.Count - 1

            If Me.Tabla.Item(4, i).Style.ForeColor = Color.DarkBlue Or Me.Tabla.Item(4, i).Style.ForeColor = Color.Purple Then
                If Contador = 500 Then
                    Sql = "INSERT INTO dbo.Catalogo_de_Cuentas"
                    Sql &= " 	("
                    Sql &= " 	Nivel1,"
                    Sql &= " 	Nivel2,"
                    Sql &= " 	Nivel3,"
                    Sql &= " 	Nivel4,"
                    Sql &= " 	Cuenta,"
                    Sql &= " 	Descripcion,"
                    Sql &= " 	Naturaleza,"
                    Sql &= " 	Clasificacion,"
                    Sql &= " 	Codigo_Agrupador,"
                    Sql &= " 	Id_Empresa,"
                    Sql &= " 	RFC,"
                    Sql &= " 	Clave,"
                    Sql &= " 	Balanza,"
                    Sql &= " 	Cta_ceros,"
                    Sql &= " 	Cta_Cargo_Cero,"
                    Sql &= " 	Cta_Abono_Cero,"
                    Sql &= " 	Balance,"
                    Sql &= " 	Estado_de_Resultados,"
                    Sql &= " 	CuentaEnlace"
                    Sql &= " 	)"
                    Sql &= " VALUES "
                    Sql &= " 	("
                    Sql &= " 	'" & Me.Tabla.Item(0, i).Value.ToString().PadLeft(4, "0") & "',"
                    Sql &= " 	'" & Me.Tabla.Item(1, i).Value.ToString().PadLeft(4, "0") & "',"
                    Sql &= " 	'" & Me.Tabla.Item(2, i).Value.ToString().PadLeft(4, "0") & "',"
                    Sql &= " 	'" & Me.Tabla.Item(3, i).Value.ToString().PadLeft(4, "0") & "',"
                    Sql &= " 	" & Me.Tabla.Item(4, i).Value.ToString().Replace("-", "") & ","
                    Sql &= " 	'" & Me.Tabla.Item(5, i).Value.ToString.Trim().Replace("'", " ").Replace("Â", "A").Replace("´", "") & "',"
                    Sql &= " 	'" & Me.Tabla.Item(6, i).Value.ToString.Trim() & "',"
                    Sql &= " 	'" & Me.Tabla.Item(7, i).Value.ToString.Trim() & "',"
                    Sql &= " 	'" & Me.Tabla.Item(8, i).Value.ToString.Trim() & "',"
                    Sql &= " 	 " & Me.lstCliente.SelectItem & " ,"
                    Sql &= " 	'" & IIf(Me.Tabla.Item(9, i).Value Is Nothing, "", Me.Tabla.Item(9, i).Value) & "',"
                    Sql &= " 	'',"
                    Sql &= " 	 " & IIf(Me.Tabla.Item(10, i).Value Is Nothing, 0, IIf(UCase(Me.Tabla.Item(10, i).Value.ToString()) = "NO", 0, 1)) & " ,"
                    Sql &= " 	 " & IIf(Me.Tabla.Item(11, i).Value Is Nothing, 0, IIf(UCase(Me.Tabla.Item(11, i).Value.ToString()) = "NO", 0, 1)) & " ,"
                    Sql &= " 	 " & IIf(Me.Tabla.Item(12, i).Value Is Nothing, 0, IIf(UCase(Me.Tabla.Item(12, i).Value.ToString()) = "NO", 0, 1)) & " ,"
                    Sql &= " 	 " & IIf(Me.Tabla.Item(13, i).Value Is Nothing, 0, IIf(UCase(Me.Tabla.Item(13, i).Value.ToString()) = "NO", 0, 1)) & " ,"
                    Sql &= " 	 " & IIf(Me.Tabla.Item(14, i).Value Is Nothing, 0, IIf(UCase(Me.Tabla.Item(14, i).Value.ToString()) = "NO", 0, 1)) & " ,"
                    Sql &= " 	 " & IIf(Me.Tabla.Item(15, i).Value Is Nothing, 0, IIf(UCase(Me.Tabla.Item(15, i).Value.ToString()) = "NO", 0, 1)) & " ,"
                    Sql &= " 	'" & IIf(Me.Tabla.Item(16, i).Value Is Nothing, "", Me.Tabla.Item(16, i).Value.ToString()) & "'"
                    Sql &= " 	)" & vbCrLf

                    Contador = 1
                Else

                    Sql &= "INSERT INTO dbo.Catalogo_de_Cuentas"
                    Sql &= " 	("
                    Sql &= " 	Nivel1,"
                    Sql &= " 	Nivel2,"
                    Sql &= " 	Nivel3,"
                    Sql &= " 	Nivel4,"
                    Sql &= " 	Cuenta,"
                    Sql &= " 	Descripcion,"
                    Sql &= " 	Naturaleza,"
                    Sql &= " 	Clasificacion,"
                    Sql &= " 	Codigo_Agrupador,"
                    Sql &= " 	Id_Empresa,"
                    Sql &= " 	RFC,"
                    Sql &= " 	Clave,"
                    Sql &= " 	Balanza,"
                    Sql &= " 	Cta_ceros,"
                    Sql &= " 	Cta_Cargo_Cero,"
                    Sql &= " 	Cta_Abono_Cero,"
                    Sql &= " 	Balance,"
                    Sql &= " 	Estado_de_Resultados,"
                    Sql &= " 	CuentaEnlace"
                    Sql &= " 	)"
                    Sql &= " VALUES "
                    Sql &= " 	("
                    Sql &= " 	'" & Me.Tabla.Item(0, i).Value.ToString().PadLeft(4, "0") & "',"
                    Sql &= " 	'" & Me.Tabla.Item(1, i).Value.ToString().PadLeft(4, "0") & "',"
                    Sql &= " 	'" & Me.Tabla.Item(2, i).Value.ToString().PadLeft(4, "0") & "',"
                    Sql &= " 	'" & Me.Tabla.Item(3, i).Value.ToString().PadLeft(4, "0") & "',"
                    Sql &= " 	" & Me.Tabla.Item(4, i).Value.ToString().Replace("-", "") & ","
                    Sql &= " 	'" & Me.Tabla.Item(5, i).Value.ToString.Trim().Replace("'", " ").Replace("Â", "A").Replace("´", "") & "',"
                    Sql &= " 	'" & Me.Tabla.Item(6, i).Value.ToString.Trim() & "',"
                    Sql &= " 	'" & Me.Tabla.Item(7, i).Value.ToString.Trim() & "',"
                    Sql &= " 	'" & Me.Tabla.Item(8, i).Value.ToString.Trim() & "',"
                    Sql &= " 	 " & Me.lstCliente.SelectItem & " ,"
                    Sql &= " 	'" & IIf(Me.Tabla.Item(9, i).Value Is Nothing, "", Me.Tabla.Item(9, i).Value) & "',"
                    Sql &= " 	'',"
                    Sql &= " 	 " & IIf(Me.Tabla.Item(10, i).Value Is Nothing, 0, IIf(UCase(Me.Tabla.Item(10, i).Value.ToString()) = "NO", 0, 1)) & " ,"
                    Sql &= " 	 " & IIf(Me.Tabla.Item(11, i).Value Is Nothing, 0, IIf(UCase(Me.Tabla.Item(11, i).Value.ToString()) = "NO", 0, 1)) & " ,"
                    Sql &= " 	 " & IIf(Me.Tabla.Item(12, i).Value Is Nothing, 0, IIf(UCase(Me.Tabla.Item(12, i).Value.ToString()) = "NO", 0, 1)) & " ,"
                    Sql &= " 	 " & IIf(Me.Tabla.Item(13, i).Value Is Nothing, 0, IIf(UCase(Me.Tabla.Item(13, i).Value.ToString()) = "NO", 0, 1)) & " ,"
                    Sql &= " 	 " & IIf(Me.Tabla.Item(14, i).Value Is Nothing, 0, IIf(UCase(Me.Tabla.Item(14, i).Value.ToString()) = "NO", 0, 1)) & " ,"
                    Sql &= " 	 " & IIf(Me.Tabla.Item(15, i).Value Is Nothing, 0, IIf(UCase(Me.Tabla.Item(15, i).Value.ToString()) = "NO", 0, 1)) & " ,"
                    Sql &= " 	'" & IIf(Me.Tabla.Item(16, i).Value Is Nothing, "", Me.Tabla.Item(16, i).Value.ToString()) & "'"
                    Sql &= " 	)" & vbCrLf
                    Contador += 1
                End If
                If Contador >= 500 Then ' Lista normal
                    If Eventos.Comando_sql(Sql) > 0 Then

                    Else

                        Exit Sub
                    End If
                ElseIf Contador = Me.Tabla.Rows.Count - 1 Then ' SI el total es menos a 500
                    If Eventos.Comando_sql(Sql) > 0 Then

                    Else

                        Exit Sub
                    End If
                ElseIf i = Me.Tabla.Rows.Count - 1 And Contador > 0 Then ' los sobrantes de 500
                    If Eventos.Comando_sql(Sql) > 0 Then

                    Else

                        Exit Sub
                    End If
                End If
            End If

            frm.Barra.Value += 1
        Next
        frm.Close()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

    End Sub
End Class
