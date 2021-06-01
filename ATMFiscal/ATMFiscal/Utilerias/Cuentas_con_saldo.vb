Imports System.ComponentModel
Imports Telerik.WinControls
Public Class Cuentas_con_saldo
    Public Id_Empresa As Integer
    Public Tipo As String
    Dim Activo As Boolean
    Dim anio = Str(DateTime.Now.Year)
    Public serV As String = My.Forms.Inicio.txtServerDB.Text
    Private Sub Cuentas_con_saldo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Activo = True
        For i = DateTime.Now.Year To DateTime.Now.Year - 10 Step -1
            If i >= 2010 Then
                Me.ComboAñoB.Items.Add(Str(i))
            End If
        Next

        Me.ComboAñoB.Text = Str(DateTime.Now.Year)

        Activo = False
    End Sub
    Private Sub Cuentas(ByVal Anio As Integer)
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim Sql As String = "SELECT DISTINCT  Detalle_Polizas.Cuenta  FROM Polizas INNER JOIN Detalle_Polizas ON Detalle_Polizas.ID_poliza = Polizas.ID_poliza 
                WHERE  Id_Empresa = " & Id_Empresa & " AND ID_anio = " & Anio & " OR 
            POLIZAS.ID_poliza IN (SELECT Polizas.ID_poliza  FROM Polizas 
                WHERE Concepto = 'Poliza Cierre' AND ID_anio = " & Anio - 1 & " AND Id_Empresa = " & Id_Empresa & ")"
        Dim Datos As DataSet = Eventos.Obtener_DS(Sql)
        If Datos.Tables(0).Rows.Count > 0 Then
            Me.Barra.Maximum = Datos.Tables(0).Rows.Count - 1
            Me.Barra.Minimum = 0
            Me.Barra.Value = 0
            For i As Integer = 0 To Datos.Tables(0).Rows.Count - 1
                Inserta_cuenta(Datos.Tables(0).Rows(i)("Cuenta"))
                If Me.Barra.Value = Me.Barra.Maximum Then
                    Me.Barra.Minimum = 0
                    Me.Cursor = Cursors.Arrow
                    Me.Barra.Value = 0
                    RadMessageBox.Show("Proceso terminado", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)

                Else
                    Me.Barra.Increment(1)
                End If
            Next
        End If
    End Sub

    Private Sub Inserta_cuenta(ByVal Cuenta As String)
        Dim Ctas As New List(Of Ctas_S)
        Dim Contador As Integer
        Dim Consulta As String = ""
        Dim DS As DataSet
        If Cuenta.Substring(12, 4) = "0000" And Cuenta.Substring(8, 4) > "0000" Then ' Madre
            Consulta = " select Cuenta from Catalogo_de_Cuentas where Nivel1 = '" & Cuenta.Substring(0, 4) & "' AND Nivel2 = '0000'  And Id_Empresa = " & Inicio.Clt & " "
            DS = Eventos.Obtener_DS(Consulta)
            If DS.Tables(0).Rows.Count > 0 Then
                Ctas.Add(New Ctas_S() With {.Cta = Trim(DS.Tables(0).Rows(0)(0)).ToString, .Int = Contador})
            End If
            Consulta = " select Cuenta from Catalogo_de_Cuentas where Nivel1 = '" & Cuenta.Substring(0, 4) & "' AND Nivel2 = '" & Cuenta.Substring(4, 4) & "'  And Id_Empresa = " & Inicio.Clt & " "
            DS = Eventos.Obtener_DS(Consulta)
            If DS.Tables(0).Rows.Count > 0 Then
                Ctas.Add(New Ctas_S() With {.Cta = Trim(DS.Tables(0).Rows(0)(0)).ToString, .Int = Contador})
            End If
            Ctas.Add(New Ctas_S() With {.Cta = Cuenta, .Int = Contador})
        ElseIf Cuenta.Substring(12, 4) > "0000" And Cuenta.Substring(8, 4) > "0000" Then 'Hija

            Consulta = " select Cuenta from Catalogo_de_Cuentas where Nivel1 = '" & Cuenta.Substring(0, 4) & "' AND Nivel2 = '0000'  And Id_Empresa = " & Inicio.Clt & " "
            DS = Eventos.Obtener_DS(Consulta)
            If DS.Tables(0).Rows.Count > 0 Then
                Ctas.Add(New Ctas_S() With {.Cta = Trim(DS.Tables(0).Rows(0)(0)).ToString, .Int = Contador})
            End If
            Consulta = " select Cuenta from Catalogo_de_Cuentas where Nivel1 = '" & Cuenta.Substring(0, 4) & "' AND Nivel2 = '" & Cuenta.Substring(4, 4) & "'  And Id_Empresa = " & Inicio.Clt & " "
            DS = Eventos.Obtener_DS(Consulta)
            If DS.Tables(0).Rows.Count > 0 Then
                Ctas.Add(New Ctas_S() With {.Cta = Trim(DS.Tables(0).Rows(0)(0)).ToString, .Int = Contador})
            End If
            Consulta = " select Cuenta from Catalogo_de_Cuentas where Nivel1 = '" & Cuenta.Substring(0, 4) & "' AND Nivel2 = '" & Cuenta.Substring(4, 4) & "' AND Nivel3 = '" & Cuenta.Substring(8, 4) & "'  And Id_Empresa = " & Inicio.Clt & " "
            DS = Eventos.Obtener_DS(Consulta)
            If DS.Tables(0).Rows.Count > 0 Then
                Ctas.Add(New Ctas_S() With {.Cta = Trim(DS.Tables(0).Rows(0)(0)).ToString, .Int = Contador})
            End If
            Ctas.Add(New Ctas_S() With {.Cta = Cuenta, .Int = Contador})

        ElseIf Cuenta.Substring(4, 4) > "0000" And Cuenta.Substring(8, 4) = "0000" And Cuenta.Substring(12, 4) = "0000" Then 'Primer Nivel
            Consulta = " select Cuenta from Catalogo_de_Cuentas where Nivel1 = '" & Cuenta.Substring(0, 4) & "' AND Nivel2 = '0000'  And Id_Empresa = " & Inicio.Clt & " "
            DS = Eventos.Obtener_DS(Consulta)
            If DS.Tables(0).Rows.Count > 0 Then
                Ctas.Add(New Ctas_S() With {.Cta = Trim(DS.Tables(0).Rows(0)(0)).ToString, .Int = Contador})
            End If
            Ctas.Add(New Ctas_S() With {.Cta = Cuenta, .Int = Contador})
        Else
            Ctas.Add(New Ctas_S() With {.Cta = Cuenta, .Int = Contador})
        End If


        For Each It In Ctas
            If Eventos.ObtenerValorDB("Cuentas_Con_Saldo", "Cuenta", "  cuenta = " & It.Cta & "  and Id_Empresa =" & Inicio.Clt & "  and anio= " & Me.ComboAñoB.Text.Trim() & " ", True) = " " Then
                Consulta = "INSERT INTO dbo.Cuentas_Con_Saldo	("

                Consulta &= " 	Cuenta,"
                Consulta &= " 	Usar,"
                Consulta &= " 	Anio,"
                Consulta &= " 	Id_Empresa"
                Consulta &= " 	)"
                Consulta &= " VALUES "
                Consulta &= " 	("
                Consulta &= " 	" & It.Cta & ","
                Consulta &= " 	1,"
                Consulta &= " 	" & Me.ComboAñoB.Text.Trim() & ","
                Consulta &= " 	" & Id_Empresa & ""
                Consulta &= " 	)"
                If Eventos.Comando_sql(Consulta) = 0 Then

                End If
            End If
        Next

    End Sub


    Private Sub SegundoPlano_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles SegundoPlano.RunWorkerCompleted
        If e.Cancelled Then
            MessageBox.Show("La ación ha sido cancelada.")
        ElseIf e.Error IsNot Nothing Then
            MessageBox.Show("Se ha producido un error durante la ejecución: " & e.Error.Message)

        End If
    End Sub
    Public Class Ctas_S
        Public Property Int As Integer
        Public Property Cta As String

    End Class



    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub
    Private Sub SegundoPlano_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles SegundoPlano.DoWork
        My.Forms.Inicio.txtServerDB.Text = serV
        If Me.ComboAñoB.Text <> "" Then
            Cuentas(Trim(Me.ComboAñoB.Text.Trim()))
        End If
    End Sub

    Private Sub Cmd_Guardar_Click(sender As Object, e As EventArgs) Handles Cmd_Guardar.Click
        SegundoPlano.RunWorkerAsync()
        Control.CheckForIllegalCrossThreadCalls = False
    End Sub
End Class
