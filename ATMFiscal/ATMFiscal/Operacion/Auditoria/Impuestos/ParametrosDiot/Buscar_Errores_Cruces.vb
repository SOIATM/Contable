Imports Telerik.WinControls

Public Class Buscar_Errores_Cruces
    Dim m = Now.Date.Month.ToString
    Dim a = Str(DateTime.Now.Year)
    Public Arreglo() As String
    Dim Negrita_verde As New DataGridViewCellStyle
    Dim Error_Color As New DataGridViewCellStyle
    Public Cuentas As New List(Of Tipos_Cruces)
    Public Polizas As New List(Of Polizas_Cruces)

    Public Class Tipos_Cruces
        Public Property ID As Integer
        Public Property Tipo_Suma As String
        Public Property Descripcion As String
    End Class
    Public Class Polizas_Cruces
        Public Property Int As Integer
        Public Property Poliza As String
        Public Property Tipo As String
    End Class
    Public Class Poliza_Detalle
        Public Property Int As Integer
        Public Property Cuenta As String
        Public Property Cargo As Decimal
        Public Property Abono As Decimal


    End Class
    Public Class Poliza_DetalleN
        Public Property Pol As String
        Public Property Int As Integer
        Public Property Cuenta As String
        Public Property Cargo As Decimal
        Public Property Abono As Decimal
    End Class
    Private Sub Buscar_Errores_Cruces_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_clientes()
        Eventos.DiseñoTablaEnca(Me.TablaImportar)
    End Sub
    Private Sub Cargar_clientes()
        Me.lstCliente.Cargar("SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & Inicio.LblUsuario.Text & "%')")

        Me.lstCliente.SelectItem = Inicio.Clt

    End Sub

    Private Sub cmdCerrar_Click(sender As Object, e As EventArgs) Handles cmdCerrar.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiar_Click(sender As Object, e As EventArgs) Handles CmdLimpiar.Click
        Limpiar()
    End Sub
    Private Sub Limpiar()
        Me.TablaImportar.Rows.Clear()

    End Sub
    Private Function Ver_tamaño_cuenta(ByVal Cuenta As String)
        Dim Hacer As Integer = 0
        If Cuenta.Substring(4, 4) = "0000" Then ' Nivel 1
            Hacer = 4
        ElseIf Cuenta.Substring(8, 4) = "0000" And Cuenta.Substring(4, 4) <> "0000" Then ' Nivel 2
            Hacer = 8
        ElseIf Cuenta.Substring(8, 4) <> "0000" And Cuenta.Substring(4, 4) <> "0000" And Cuenta.Substring(12, 4) = "0000" Then ' Nivel 3
            Hacer = 12
        ElseIf Cuenta.Substring(8, 4) <> "0000" And Cuenta.Substring(4, 4) <> "0000" And Cuenta.Substring(12, 4) <> "0000" Then ' Nivel 4
            Hacer = 16
        End If


        Select Case Cuenta.Substring(0, 4)
            Case "2080", "2090", "1190", "1180"
                If Cuenta.Substring(12, 4) = "0000" Then
                    Hacer = 12
                Else
                    Hacer = 16
                End If
            Case Else
                Hacer = Hacer
        End Select

        Return Hacer
    End Function
    Private Function Cruce_Iva_Trasladado(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal

        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='1'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If Cu.Cuenta.Replace("-", "") Like ds.Tables(0).Rows(i)(0).Replace("-0000", "").Replace("-", "").ToString & "*" Then
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                c = a + b
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='2'")
        a = 0
        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try


                For Each Cu In Det
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If Cu.Cuenta.Replace("-", "") Like ds.Tables(0).Rows(i)(0).Replace("-0000", "").Replace("-", "").ToString & "*" Then
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                c = c + a + b
            Catch ex As Exception
                c = c
            End Try
        End If


        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='3'")
        a = 0
        b = 0
        If ds.Tables(0).Rows.Count > 0 Then

            For Each Cu In Det
                Try
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If Cu.Cuenta.Replace("-", "").Substring(0, 12) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, 12) Then
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next
                Catch ex As Exception

                End Try
            Next
            a = a + b

        End If
        c = Math.Round((c * 0.16) - a, 2)
        Return c
    End Function
    Private Function Cruce_Cargos_Clientes(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='3.2'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = a + b
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='3.4'")
        a = 0
        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                a = a + b
            Catch ex As Exception
                a = a
            End Try
        End If


        c = Math.Round(c - a, 2)
        Return c
    End Function
    Private Function Cruce_Abonos_Clientes(ByVal Det As List(Of Poliza_Detalle))

        Dim a = 0, b = 0, c As Decimal = 0
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='4'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then

                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = a + b
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='5'")
        a = 0
        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                a = a + b
            Catch ex As Exception
                a = a
            End Try
        End If


        c = Math.Round(c - a, 2)
        Return c
    End Function
    Private Function Cruce_Cargos_ClientesPR(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='9'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = a + b
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='8'")
        a = 0
        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                a = a + b
            Catch ex As Exception
                a = a
            End Try
        End If


        c = Math.Round(c - a, 2)
        Return c
    End Function
    Private Function Cruce_Abonos_ClientesPR(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='10'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = a + b
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='11'")
        a = 0
        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                a = a + b
            Catch ex As Exception
                a = a
            End Try
        End If


        c = Math.Round(c - a, 2)
        Return c
    End Function
    Private Function Cruce_Cargos_ClientesAC(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='14'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = a + b
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='15'")
        a = 0
        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                a = a + b
            Catch ex As Exception
                a = a
            End Try
        End If


        c = Math.Round(c - a, 2)
        Return c
    End Function
    Private Function Cruce_Abonos_ClientesAC(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='16'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = a + b
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='17'")
        a = 0
        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If
                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                a = a + b
            Catch ex As Exception
                a = a
            End Try
        End If


        c = Math.Round(c - a, 2)
        Return c
    End Function

    Private Function Cruce_Abonos_ClientesAN(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='20'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try

                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar

                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a + b) * 0.16
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='20.0'")
        a = 0
        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If
                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                a = a + b
            Catch ex As Exception
                a = a
            End Try
        End If


        c = Math.Round(c - a, 2)
        Return c
    End Function
    Private Function Cruce_Cargos_ClientesAN(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='21'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If
                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a + b) * 0.16
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='21.0'")
        a = 0
        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If
                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                a = a + b
            Catch ex As Exception
                a = a
            End Try
        End If


        c = Math.Round(c - a, 2)
        Return c
    End Function
    Private Function Cruce_DescuentosSV(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='22'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try


                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If
                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a + b) * 0.16
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='22.0'")
        a = 0
        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                a = a + b
            Catch ex As Exception
                a = a
            End Try
        End If


        c = Math.Round(c - a, 2)
        Return c
    End Function

    Private Function Cruce_Ingresos_Vs_IVA_Trasl_Acumulado(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='23'   ")
        If ds.Tables(0).Rows.Count > 0 Then
            Try


                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If
                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (b - a) * 0.16
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()
        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='24'")
        a = 0

        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a - Cu.Cargo
                            Else
                                a = a + Cu.Abono
                            End If
                        End If
                    Next

                Next
                a = a * 0.16
            Catch ex As Exception
                a = a
            End Try
        End If
        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='25'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b - Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round((a + c) - b, 2)
        Return c
    End Function

    Private Function Cruce_de_Abonos_a_Proveedores_Nacionales(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal

        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='27'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If Cu.Cuenta.Replace("-", "") Like ds.Tables(0).Rows(i)(0).Replace("-0000", "").Replace("-", "").ToString & "*" Then
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next
                Next
                c = b - a
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    Private Function Cruce_de_Cargos_a_Proveedores_Nacionales(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal

        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='28'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If Cu.Cuenta.Replace("-", "") Like ds.Tables(0).Rows(i)(0).Replace("-0000", "").Replace("-", "").ToString & "*" Then
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next
                Next
                c = b - a
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    Private Function Cruce_de_Abonos_a_Proveedores_NacionalesPR(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal

        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='30'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If Cu.Cuenta.Replace("-", "") Like ds.Tables(0).Rows(i)(0).Replace("-0000", "").Replace("-", "").ToString & "*" Then
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next
                Next
                c = b - a
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    Private Function Cruce_de_Cargos_a_Proveedores_NacionalesPR(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal

        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='31'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If Cu.Cuenta.Replace("-", "") Like ds.Tables(0).Rows(i)(0).Replace("-0000", "").Replace("-", "").ToString & "*" Then
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next
                Next
                c = b - a
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function

    Private Function Cruce_de_Abonos_a_Proveedores_NacionalesE(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal

        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='33'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If Cu.Cuenta.Replace("-", "") Like ds.Tables(0).Rows(i)(0).Replace("-0000", "").Replace("-", "").ToString & "*" Then
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next
                Next
                c = b - a
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    Private Function Cruce_de_Cargos_a_Proveedores_NacionalesE(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal

        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='34'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If Cu.Cuenta.Replace("-", "") Like ds.Tables(0).Rows(i)(0).Replace("-0000", "").Replace("-", "").ToString & "*" Then
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next
                Next
                c = b - a
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    Private Function Cruce_Compras_VS_IVA_Acreditable(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='40'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).TRIM() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a + b) * 0.16
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='41'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).TRIM() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    Private Function Cruce_de_Abonos_a_Acreedores(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal

        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='37'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        'If Cu.Cuenta.Replace("-", "").Substring(0, 8) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, 8) Then
                        If Cu.Cuenta.Replace("-", "") Like ds.Tables(0).Rows(i)(0).Replace("-0000", "").Replace("-", "").ToString & "*" Then
                            If ds.Tables(0).Rows(i)(1).TRIM() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next
                Next
                c = b - a
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    Private Function Cruce_de_Cargos_a_Acreedores(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal

        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='38'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        If Cu.Cuenta.Replace("-", "") Like ds.Tables(0).Rows(i)(0).Replace("-0000", "").Replace("-", "").ToString & "*" Then
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next
                Next
                c = b - a
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    Private Function Cruce_Descto_Compras_Vs_IVA_Acred_de_Descto(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='43'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a + b) * 0.16
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='43.0'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    'Cruce_de_IVA_Acred_Por_Gastos
    Private Function Cruce_de_IVA_Acred_Por_Gastos(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='44'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a + b) * 0.16
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='45'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If
                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    'Cruce_de_IVA_Acred_Por_Gastos_Financieros
    Private Function Cruce_de_IVA_Acred_Por_Gastos_Financieros(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='46'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a + b) * 0.16
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='47'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    'Cruce_de_Acred_Por_Activos_Fijos
    Private Function Cruce_de_Acred_Por_Activos_Fijos(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='48'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If
                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a + b) * 0.16
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='49'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    'Cruce_de_Activos_Diferidos
    Private Function Cruce_de_Activos_Diferidos(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='50'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a + b) * 0.16
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='50.0'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    'Cruce_de_Egresos_Vs_IVA_Acreditable
    Private Function Cruce_de_Egresos_Vs_IVA_Acreditable(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                            WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='51' order by Cuenta")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        If Len(ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")) = 4 Then

                            Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                            If NUMERO >= 9000 Then
                                texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                            Else
                                texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                            End If

                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos

                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then   'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a - b) * 0.16
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='53' order by cuenta")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        If Len(ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")) = 4 Then
                            Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                            If NUMERO >= 9000 Then
                                texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                            Else
                                texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                            End If
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b - Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    'Cruce_de_IVA_Acred_en_Cruces_Vs_Declaracion
    Private Function Cruce_de_IVA_Acred_en_Cruces_Vs_Declaracion(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='55.0'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        If Len(ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")) = 4 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a + b) * 0.16
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='55.1'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    'Cruce_de_Ret_de_ISR_Por_Honorarios
    Private Function Cruce_de_Ret_de_ISR_Por_Honorarios(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='56'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a + b) * 0.1
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='56.0'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function

    Private Function Cruce_de_Ret_de_ISR_Por_Arrendamiento(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='57'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a + b) * 0.1
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='57.0'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    'Cruce_de_Ret_de_IVA_Por_Arrendamiento
    Private Function Cruce_de_Ret_de_IVA_Por_Arrendamiento(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='58'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = ((a + b) * 0.16) / (3 * 2)
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='58.0'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    ' Cruce_de_Ret_de_IVA_Por_Honorarios
    Private Function Cruce_de_Ret_de_IVA_Por_Honorarios(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='59'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (((a + b) * 0.16) / 3) * 2
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='59.0'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    'Cruce_de_Ret_de_IVA_Por_Fletes
    Private Function Cruce_de_Ret_de_IVA_Por_Fletes(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='59.1'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = ((a + b) * 0.04)
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='60'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    'Prueba_de_Ing_Por_Honor_Vs_IVA_Retenido
    Private Function Prueba_de_Ing_Por_Honor_Vs_IVA_Retenido(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='62'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (((a + b) * 0.16) / 3) * 2
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='62.0'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    'Prueba_de_Ing_Por_Honor_Vs_ISR_Retenido
    Private Function Prueba_de_Ing_Por_Honor_Vs_ISR_Retenido(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='63'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = ((a + b) * 0.1)
            Catch ex As Exception
                c = 0
            End Try
        End If

        ds.Clear()

        ds = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='63.0'")

        b = 0
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If
                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                b = b + Cu.Cargo
                            Else
                                b = b + Cu.Abono
                            End If
                        End If
                    Next

                Next
                b = b
            Catch ex As Exception
                b = b
            End Try
        End If

        c = Math.Round(c - b, 2)
        Return c
    End Function
    'Cruce_de_Ingresos_P_Cobrar_Cargos_CO
    Private Function Cruce_de_Ingresos_P_Cobrar_Cargos_CO(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='64'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a - b)
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    'Cruce_de_Ingresos_P_Cobrar_Abonos_CO
    Private Function Cruce_de_Ingresos_P_Cobrar_Abonos_CO(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='65'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a - b)
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    'Cruce_de_Compras_P_Cobrar_Abonos_CO
    Private Function Cruce_de_Compras_P_Cobrar_Cargos_CO(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='67'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a - b)
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    'Cruce_de_Compras_P_Cobrar_Abonos_CO
    Private Function Cruce_de_Compras_P_Cobrar_Abonos_CO(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='68'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos

                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a - b)
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    'Cruce_de_Gastos_P_Cobrar_Cargos_CO
    Private Function Cruce_de_Gastos_P_Cobrar_Cargos_CO(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='70'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a - b)
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    Private Function Cruce_de_Gastos_P_Cobrar_Abonos_CO(ByVal Det As List(Of Poliza_Detalle))

        Dim a, b, c As Decimal
        Dim tamaño As Integer = 0
        Dim ds As DataSet = Eventos.Obtener_DS("SELECT Calculo_Impuestos.Cuenta,Naturaleza FROM Calculo_Impuestos
                        WHERE Calculo_Impuestos.Id_Empresa = " & Me.lstCliente.SelectItem & " and Anio = " & Me.DtInicio.Value.Year & " AND Cuenta <> '' AND Calculo_Impuestos.Operacion ='71'")
        If ds.Tables(0).Rows.Count > 0 Then
            Try
                For Each Cu In Det ' cada cuenta del detalle de poliza se analiza
                    tamaño = Ver_tamaño_cuenta(Cu.Cuenta.Replace("-", "")) ' se define el tamaño de cadena a analizar
                    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1 ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        Dim texto = ""
                        Dim NUMERO = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        If NUMERO >= 9000 Then
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-", "")
                        Else
                            texto = ds.Tables(0).Rows(i)(0).ToString.Replace("-0000", "").Replace("-", "")
                        End If

                        ' por cada cuenta del cruce se comparan las cuentas para sumar importes en cargos o abonos
                        If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) Like "*" & texto & "*" Then
                            'If Cu.Cuenta.Replace("-", "").Substring(0, tamaño) = ds.Tables(0).Rows(i)(0).Replace("-", "").ToString.Substring(0, tamaño) Then ' comparar cuentas
                            If ds.Tables(0).Rows(i)(1).Trim() = "C" Then
                                a = a + Cu.Cargo ' se suma el cargo 
                            Else
                                b = b + Cu.Abono ' se suma el abono
                            End If
                        End If
                    Next

                Next
                c = (a - b)
            Catch ex As Exception
                c = 0
            End Try
        End If
        c = Math.Round(c, 2)
        Return c
    End Function
    Private Sub Cmd_Procesar_Click(sender As Object, e As EventArgs) Handles Cmd_Procesar.Click
        Buscar()

    End Sub
    Private Sub Buscar()
        Limpiar()
        Dim fi = "", ff As String = ""
        fi = Me.DtInicio.Value.ToString.Substring(0, 10)
        ff = Me.Dtfin.Value.ToString.Substring(0, 10)
        Dim Detalle As New List(Of Poliza_DetalleN)
        Dim DetalleA As New List(Of Poliza_Detalle)
        Negrita_verde.Font = New Font(Me.TablaImportar.Font, FontStyle.Bold)
        Negrita_verde.BackColor = Color.LawnGreen
        Error_Color.Font = New Font(Me.TablaImportar.Font, FontStyle.Bold)
        Error_Color.BackColor = Color.LightCoral
        Dim posicion As Integer = 0
        Dim Cuantos As Integer = 0
        Dim Sql As String = ""
        Dim Ctas As New List(Of String)
        Dim Ds As New DataSet
        Dim Registros As String = ""
        Dim frm As New BarraProcesovb

        frm.Show()
        frm.Barra.Minimum = 0
        frm.Barra.Maximum = Arreglo.Count
        Me.Cursor = Cursors.AppStarting

        Dim IvaT() As String = New String() {}
        Dim CarGosClt() As String = New String() {}
        Dim AbonosClt() As String = New String() {}
        Dim CarGosCltPR() As String = New String() {}
        Dim AbonosCltPR() As String = New String() {}
        Dim CCA() As String = New String() {}
        Dim ACA() As String = New String() {}
        Dim AAC() As String = New String() {}
        Dim CAC() As String = New String() {}
        Dim CDRDV() As String = New String() {}
        Dim CIITA() As String = New String() {}
        Dim CAPN() As String = New String() {}
        Dim CCPN() As String = New String() {}
        Dim CAPNPR() As String = New String() {}
        Dim CCPNPR() As String = New String() {}
        Dim CAPE() As String = New String() {}
        Dim CCPE() As String = New String() {}
        Dim CAA() As String = New String() {}
        Dim CCA2() As String = New String() {}
        Dim CGPPA() As String = New String() {}
        Dim CCIA() As String = New String() {}
        Dim CDSCIAD() As String = New String() {}
        Dim CIAPG() As String = New String() {}
        Dim CIAPGF() As String = New String() {}
        Dim CAPAF() As String = New String() {}
        Dim CAD() As String = New String() {}
        Dim CEIA() As String = New String() {}
        Dim CIACD() As String = New String() {}
        Dim CRIH() As String = New String() {}
        Dim CRIPA() As String = New String() {}
        Dim CRIVPA() As String = New String() {}
        Dim CRIPH() As String = New String() {}
        Dim CRIPF() As String = New String() {}
        Dim PIRHIR() As String = New String() {}
        Dim PIRHISR() As String = New String() {}
        Dim CIPC() As String = New String() {}
        Dim CIPCA() As String = New String() {}
        Dim CCPPC() As String = New String() {}
        Dim CCPPA() As String = New String() {}
        Dim CGPPC() As String = New String() {}

        For Each Cuenta In Polizas
            Select Case Cuenta.Tipo
                Case 1, 2, 3
                    If IvaT Is Nothing Then
                        ReDim Preserve IvaT(0)
                        IvaT(UBound(IvaT)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve IvaT(UBound(IvaT) + 1)
                        IvaT(UBound(IvaT)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 3.2, 3.4
                    If CarGosClt Is Nothing Then
                        ReDim Preserve CarGosClt(0)
                        CarGosClt(UBound(CarGosClt)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CarGosClt(UBound(CarGosClt) + 1)
                        CarGosClt(UBound(CarGosClt)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 4, 5
                    If AbonosClt Is Nothing Then
                        ReDim Preserve AbonosClt(0)
                        AbonosClt(UBound(AbonosClt)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve AbonosClt(UBound(AbonosClt) + 1)
                        AbonosClt(UBound(AbonosClt)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 9, 10
                    If CarGosCltPR Is Nothing Then
                        ReDim Preserve CarGosCltPR(0)
                        CarGosCltPR(UBound(CarGosCltPR)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CarGosCltPR(UBound(CarGosCltPR) + 1)
                        CarGosCltPR(UBound(CarGosCltPR)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 11, 12
                    If AbonosCltPR Is Nothing Then
                        ReDim Preserve AbonosCltPR(0)
                        AbonosCltPR(UBound(AbonosCltPR)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve AbonosCltPR(UBound(AbonosCltPR) + 1)
                        AbonosCltPR(UBound(AbonosCltPR)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 14, 15
                    If CCA Is Nothing Then
                        ReDim Preserve CCA(0)
                        CCA(UBound(CCA)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CCA(UBound(CCA) + 1)
                        CCA(UBound(CCA)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 16, 17
                    If ACA Is Nothing Then
                        ReDim Preserve ACA(0)
                        ACA(UBound(ACA)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve ACA(UBound(ACA) + 1)
                        ACA(UBound(ACA)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 20, 20.0
                    If AAC Is Nothing Then
                        ReDim Preserve AAC(0)
                        AAC(UBound(AAC)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve AAC(UBound(AAC) + 1)
                        AAC(UBound(AAC)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 21, 21.0
                    If CAC Is Nothing Then
                        ReDim Preserve CAC(0)
                        CAC(UBound(CAC)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CAC(UBound(CAC) + 1)
                        CAC(UBound(CAC)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 22, 22.0
                    If CDRDV Is Nothing Then
                        ReDim Preserve CDRDV(0)
                        CDRDV(UBound(CDRDV)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CDRDV(UBound(CDRDV) + 1)
                        CDRDV(UBound(CDRDV)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 25, 24, 23
                    If CIITA Is Nothing Then
                        ReDim Preserve CIITA(0)
                        CIITA(UBound(CIITA)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CIITA(UBound(CIITA) + 1)
                        CIITA(UBound(CIITA)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 27
                    If CAPN Is Nothing Then
                        ReDim Preserve CAPN(0)
                        CAPN(UBound(CAPN)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CAPN(UBound(CAPN) + 1)
                        CAPN(UBound(CAPN)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 28
                    If CCPN Is Nothing Then
                        ReDim Preserve CCPN(0)
                        CCPN(UBound(CCPN)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CCPN(UBound(CCPN) + 1)
                        CCPN(UBound(CCPN)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 30
                    If CAPNPR Is Nothing Then
                        ReDim Preserve CAPNPR(0)
                        CAPNPR(UBound(CAPNPR)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CAPNPR(UBound(CAPNPR) + 1)
                        CAPNPR(UBound(CAPNPR)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 31
                    If CCPNPR Is Nothing Then
                        ReDim Preserve CCPNPR(0)
                        CCPNPR(UBound(CCPNPR)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CCPNPR(UBound(CCPNPR) + 1)
                        CCPNPR(UBound(CCPNPR)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 33
                    If CAPE Is Nothing Then
                        ReDim Preserve CAPE(0)
                        CAPE(UBound(CAPE)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CAPE(UBound(CAPE) + 1)
                        CAPE(UBound(CAPE)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 34
                    If CCPE Is Nothing Then
                        ReDim Preserve CCPE(0)
                        CCPE(UBound(CCPE)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CCPE(UBound(CCPE) + 1)
                        CCPE(UBound(CCPE)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 37
                    If CAA Is Nothing Then
                        ReDim Preserve CAA(0)
                        CAA(UBound(CAA)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CAA(UBound(CAA) + 1)
                        CAA(UBound(CAA)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 38
                    If CCA2 Is Nothing Then
                        ReDim Preserve CCA2(0)
                        CCA2(UBound(CCA2)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CCA2(UBound(CCA2) + 1)
                        CCA2(UBound(CCA2)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 41, 40

                    If CCIA Is Nothing Then
                        ReDim Preserve CCIA(0)
                        CCIA(UBound(CCIA)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CCIA(UBound(CCIA) + 1)
                        CCIA(UBound(CCIA)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 43, 43.0
                    If CDSCIAD Is Nothing Then
                        ReDim Preserve CDSCIAD(0)
                        CDSCIAD(UBound(CDSCIAD)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CDSCIAD(UBound(CDSCIAD) + 1)
                        CDSCIAD(UBound(CDSCIAD)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 44, 45
                    If CIAPG Is Nothing Then
                        ReDim Preserve CIAPG(0)
                        CIAPG(UBound(CIAPG)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CIAPG(UBound(CIAPG) + 1)
                        CIAPG(UBound(CIAPG)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 46, 47
                    If CIAPGF Is Nothing Then
                        ReDim Preserve CIAPGF(0)
                        CIAPGF(UBound(CIAPGF)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CIAPGF(UBound(CIAPGF) + 1)
                        CIAPGF(UBound(CIAPGF)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 48, 49
                    If CAPAF Is Nothing Then
                        ReDim Preserve CAPAF(0)
                        CAPAF(UBound(CAPAF)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CAPAF(UBound(CAPAF) + 1)
                        CAPAF(UBound(CAPAF)) = "'" & Cuenta.Poliza & "'"
                    End If

                Case 50, 50.0
                    If CAD Is Nothing Then
                        ReDim Preserve CAD(0)
                        CAD(UBound(CAD)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CAD(UBound(CAD) + 1)
                        CAD(UBound(CAD)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 53, 51
                    If CEIA Is Nothing Then
                        ReDim Preserve CEIA(0)
                        CEIA(UBound(CEIA)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CEIA(UBound(CEIA) + 1)
                        CEIA(UBound(CEIA)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 55.0, 55.1
                    If CIACD Is Nothing Then
                        ReDim Preserve CIACD(0)
                        CIACD(UBound(CIACD)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CIACD(UBound(CIACD) + 1)
                        CIACD(UBound(CIACD)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 56, 56.0
                    If CRIH Is Nothing Then
                        ReDim Preserve CRIH(0)
                        CRIH(UBound(CRIH)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CRIH(UBound(CRIH) + 1)
                        CRIH(UBound(CRIH)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 57, 57.0
                    If CRIPA Is Nothing Then
                        ReDim Preserve CRIPA(0)
                        CRIPA(UBound(CRIPA)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CRIPA(UBound(CRIPA) + 1)
                        CRIPA(UBound(CRIPA)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 58, 58.0
                    If CRIVPA Is Nothing Then
                        ReDim Preserve CRIVPA(0)
                        CRIVPA(UBound(CRIVPA)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CRIVPA(UBound(CRIVPA) + 1)
                        CRIVPA(UBound(CRIVPA)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 59, 59.0
                    If CRIPH Is Nothing Then
                        ReDim Preserve CRIPH(0)
                        CRIPH(UBound(CRIPH)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CRIPH(UBound(CRIPH) + 1)
                        CRIPH(UBound(CRIPH)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 59.1, 60
                    If CRIPF Is Nothing Then
                        ReDim Preserve CRIPF(0)
                        CRIPF(UBound(CRIPF)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CRIPF(UBound(CRIPF) + 1)
                        CRIPF(UBound(CRIPF)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 62, 62.0
                    If PIRHIR Is Nothing Then
                        ReDim Preserve PIRHIR(0)
                        PIRHIR(UBound(PIRHIR)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve PIRHIR(UBound(PIRHIR) + 1)
                        PIRHIR(UBound(PIRHIR)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 63, 63.0
                    If PIRHISR Is Nothing Then
                        ReDim Preserve PIRHISR(0)
                        PIRHISR(UBound(PIRHISR)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve PIRHISR(UBound(PIRHISR) + 1)
                        PIRHISR(UBound(PIRHISR)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 64
                    If CIPC Is Nothing Then
                        ReDim Preserve CIPC(0)
                        CIPC(UBound(CIPC)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CIPC(UBound(CIPC) + 1)
                        CIPC(UBound(CIPC)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 65
                    If CIPCA Is Nothing Then
                        ReDim Preserve CIPCA(0)
                        CIPCA(UBound(CIPCA)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CIPCA(UBound(CIPCA) + 1)
                        CIPCA(UBound(CIPCA)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 67
                    If CCPPC Is Nothing Then
                        ReDim Preserve CCPPC(0)
                        CCPPC(UBound(CCPPC)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CCPPC(UBound(CCPPC) + 1)
                        CCPPC(UBound(CCPPC)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 68
                    If CGPPC Is Nothing Then
                        ReDim Preserve CGPPC(0)
                        CGPPC(UBound(CGPPC)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CGPPC(UBound(CGPPC) + 1)
                        CGPPC(UBound(CGPPC)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 70
                    If CGPPC Is Nothing Then
                        ReDim Preserve CCPPA(0)
                        CCPPA(UBound(CCPPA)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CCPPA(UBound(CCPPA) + 1)
                        CCPPA(UBound(CCPPA)) = "'" & Cuenta.Poliza & "'"
                    End If
                Case 71
                    If CGPPA Is Nothing Then
                        ReDim Preserve CGPPA(0)
                        CGPPA(UBound(CGPPA)) = "'" & Cuenta.Poliza & "'"
                    Else
                        ReDim Preserve CGPPA(UBound(CGPPA) + 1)
                        CGPPA(UBound(CGPPA)) = "'" & Cuenta.Poliza & "'"
                    End If
            End Select
        Next


        For Each Opcion In Arreglo

            Select Case Opcion

                Case "Cruce de IVA Trasladado"
                    Registros = Join(IvaT, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de IVA Trasladado", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = 1
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_Iva_Trasladado(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen

                                        End If
                                    End If
                                Next



                            End If

                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce Cargos de Empresa"
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce Cargos de Empresa", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Registros = Join(CarGosClt, ",")
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_Cargos_Clientes(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen

                                        End If
                                    End If
                                Next



                            End If



                        Catch ex As Exception

                        End Try
                    End If

                Case "Cruce de Abonos de Empresa"
                    Registros = Join(AbonosClt, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Abonos de Empresa", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion

                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_Abonos_Clientes(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen

                                        End If
                                    End If
                                Next



                            End If



                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce Cargos de Empresa PR"
                    Registros = Join(CarGosCltPR, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce Cargos de Empresa PR", "", "", "", "", "", "", "")
                        'Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Abonos de Empresa", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion

                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_Cargos_ClientesPR(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen

                                        End If
                                    End If
                                Next



                            End If



                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Abonos de Empresa PR"
                    Registros = Join(AbonosCltPR, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce Abonos de Empresa PR", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_Abonos_ClientesPR(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen

                                        End If
                                    End If
                                Next



                            End If



                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce Cargos de Empresa Acumul"
                    Registros = Join(CCA, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce Cargos de Empresa Acumul", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_Cargos_ClientesAC(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen

                                        End If
                                    End If
                                Next



                            End If



                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce Abonos de Empresa Acumul"
                    Registros = Join(ACA, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce Abonos de Empresa Acumul", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_Abonos_ClientesAC(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce Abonos de Ant. de Empresa"
                    Registros = Join(AAC, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce Abonos de Ant. de Empresa", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_Abonos_ClientesAN(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce Cargos de Ant. de Empresa"
                    Registros = Join(CAC, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce Cargos de Ant. de Empresa", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_Cargos_ClientesAN(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Descto., Rebaj. y Dev. S/Venta"
                    Registros = Join(CDRDV, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Descto., Rebaj. y Dev. S/Venta", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_DescuentosSV(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Ingresos Vs IVA Trasl Acumulado"
                    Registros = Join(CIITA, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Ingresos Vs IVA Trasl Acumulado", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_Ingresos_Vs_IVA_Trasl_Acumulado(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Abonos a Proveedores Nacionales"
                    Registros = Join(CAPN, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Abonos a Proveedores Nacionales", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Abonos_a_Proveedores_Nacionales(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Cargos de Proveedores Nacionales"
                    Registros = Join(CCPN, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Cargos de Proveedores Nacionales", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Cargos_a_Proveedores_Nacionales(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Abonos a Proveed. Nac. PR"
                    Registros = Join(CAPNPR, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Abonos a Proveed. Nac. PR", "", "", "", "", "", "", "")

                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Cargos_a_Proveedores_NacionalesPR(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Cargos de Proveed. Nac. PR"
                    Registros = Join(CCPNPR, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Cargos de Proveed. Nac. PR", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Cargos_a_Proveedores_NacionalesPR(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Abonos de Proveedores Extranjeros"
                    Registros = Join(CAPE, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Abonos de Proveedores Extranjeros", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Abonos_a_Proveedores_NacionalesE(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                Case "Cruce de Cargos de Proveedores Extranjeros"
                    Registros = Join(CCPE, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Cargos de Proveedores Extranjeros", "", "", "", "", "", "", "")


                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Cargos_a_Proveedores_NacionalesE(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Abonos a Acreedores"
                    Registros = Join(CAA, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Abonos a Acreedores", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Abonos_a_Acreedores(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Cargos de Acreedores"
                    Registros = Join(CCA2, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Cargos de Acreedores", "", "", "", "", "", "", "")

                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Cargos_a_Acreedores(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce Compras Vs IVA Acreditable"
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce Compras Vs IVA Acreditable", "", "", "", "", "", "", "")

                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Registros = Join(CCIA, ",")
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_Compras_VS_IVA_Acreditable(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce Descto. S/Compras Vs IVA Acred de Descto"
                    Registros = Join(CDSCIAD, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce Descto. S/Compras Vs IVA Acred de Descto", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_Descto_Compras_Vs_IVA_Acred_de_Descto(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de IVA Acred. Por Gastos"
                    Registros = Join(CIAPG, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de IVA Acred. Por Gastos", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_IVA_Acred_Por_Gastos(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de IVA Acred. Por Gastos Financieros"
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de IVA Acred. Por Gastos Financieros", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Registros = Join(CIAPGF, ",")
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_IVA_Acred_Por_Gastos_Financieros(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Acred Por Activos Fijos"
                    Registros = Join(CAPAF, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Acred Por Activos Fijos", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Acred_Por_Activos_Fijos(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Activos Diferidos"
                    Registros = Join(CAD, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Activos Diferidos", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Activos_Diferidos(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If


                Case "Cruce de Egresos Vs IVA Acreditable"
                    Registros = Join(CEIA, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Egresos Vs IVA Acreditable", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Egresos_Vs_IVA_Acreditable(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                Case "Cruce de IVA Acred en Cruces Vs Declaracion"

                    Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de IVA Acred en Cruces Vs Declaracion", "", "", "", "", "", "", "")
                    Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                    Fila.DefaultCellStyle = Negrita_verde
                    posicion += 1
                    Cuantos = posicion
                    Registros = Join(CIACD, ",")
                    If Not (Registros Is Nothing) Then
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1
                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_IVA_Acred_en_Cruces_Vs_Declaracion(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try


                    End If

                Case "Cruce de Ret de ISR Por Honorarios"
                    Registros = Join(CRIH, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Ret de ISR Por Honorarios", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Ret_de_ISR_Por_Honorarios(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                Case "Cruce de Ret de ISR Por Arrendamiento"
                    Registros = Join(CRIPA, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Ret de ISR Por Arrendamiento", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1
                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Ret_de_ISR_Por_Arrendamiento(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Ret de IVA Por Arrendamiento"
                    Registros = Join(CRIVPA, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Ret de IVA Por Arrendamiento", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1
                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Ret_de_IVA_Por_Arrendamiento(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Ret de IVA Por Honorarios"
                    Registros = Join(CRIPH, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Ret de IVA Por Honorarios", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Ret_de_IVA_Por_Honorarios(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                Case "Cruce de Ret de IVA Por Fletes"
                    Registros = Join(CRIPF, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Ret de IVA Por Fletes", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1
                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Ret_de_IVA_Por_Fletes(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try

                    End If

                Case "Prueba de Ing. Por Honor. Vs. IVA Retenido"
                    Registros = Join(PIRHIR, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Prueba de Ing. Por Honor. Vs. IVA Retenido", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Registros = Join(PIRHIR, ",")
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Prueba_de_Ing_Por_Honor_Vs_IVA_Retenido(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Prueba de Ing. Por Honor. Vs. ISR Retenido"
                    Registros = Join(PIRHISR, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Prueba de Ing. Por Honor. Vs. ISR Retenido", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Registros = Join(PIRHISR, ",")
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0

                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})
                                    posicion += 1
                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Prueba_de_Ing_Por_Honor_Vs_ISR_Retenido(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                Case "Cruce de Ingresos P/Cobrar Cargos (CO)"
                    Registros = Join(CIPC, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Ingresos P/Cobrar Cargos (CO)", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Registros = Join(CIPC, ",")
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1
                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                        posicion += 1
                                    End If
                                Next

                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Ingresos_P_Cobrar_Cargos_CO(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Ingresos P/Cobrar Abonos (CO)"
                    Registros = Join(CIPCA, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Ingresos P/Cobrar Abonos (CO)", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Registros = Join(CIPCA, ",")
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1
                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Ingresos_P_Cobrar_Abonos_CO(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Compras P/Pagar Cargos (CO)"
                    Registros = Join(CCPPC, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Compras P/Pagar Cargos (CO)", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1

                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Compras_P_Cobrar_Cargos_CO(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                Case "Cruce de Compras P/Pagar Abonos (CO)"
                    Registros = Join(CCPPA, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Compras P/Pagar Abonos (CO)", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0
                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})
                                    posicion += 1
                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Compras_P_Cobrar_Abonos_CO(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If
                Case "Cruce de Gastos P/Pagar Cargos (CO)"
                    Registros = Join(CGPPC, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Gastos P/Pagar Cargos (CO)", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1
                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Gastos_P_Cobrar_Cargos_CO(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If

                Case "Cruce de Gastos P/Pagar Abonos (CO)"
                    Registros = Join(CGPPA, ",")
                    If Not (Registros Is Nothing) Then
                        Me.TablaImportar.Rows.Insert(posicion, "", "", "", "Cruce de Gastos P/Pagar Abonos (CO)", "", "", "", "", "", "", "")
                        Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(posicion)
                        Fila.DefaultCellStyle = Negrita_verde
                        posicion += 1
                        Cuantos = posicion
                        Try
                            Sql = "SELECT  Polizas.Id_Poliza as Poliza,
                                            Polizas.ID_anio,Polizas.ID_mes, Tipos_Poliza_Sat.Clave + ' - ' + Tipos_Poliza_Sat.Nombre as Tipo,Polizas.Num_Pol,Polizas.ID_dia,Nivel1 + '-' + Nivel2 + '-' + Nivel3 + '-' + Nivel4 as Cuenta,
                                            Catalogo_de_Cuentas.Descripcion, Detalle_Polizas.Cargo , Detalle_Polizas.Abono  
                                            FROM     Polizas INNER JOIN Empresa ON Polizas.Id_Empresa = Empresa.Id_Empresa 
                                            INNER JOIN Tipos_Poliza_Sat ON Polizas.Id_Tipo_Pol_Sat = Tipos_Poliza_Sat.Id_Tipo_Pol_Sat
                                            INNER JOIN Detalle_Polizas ON Polizas.Id_poliza = Detalle_Polizas.Id_poliza
                                            INNER JOIN Catalogo_de_Cuentas ON Catalogo_de_Cuentas.cuenta = Detalle_Polizas.cuenta AND Empresa.Id_Empresa = Catalogo_de_Cuentas.Id_Empresa
                                            WHERE (Polizas.Fecha_captura >=" & Eventos.Sql_hoy(fi) & " and Polizas.Fecha_captura <= " & Eventos.Sql_hoy(ff) & ") AND (   Polizas.Id_Empresa = " & Me.lstCliente.SelectItem & " 
                                            AND Polizas.ID_poliza in  (" & Registros & ")) ORDER BY Polizas.ID_poliza , Detalle_Polizas.Id_item  "
                            Ds = Eventos.Obtener_DS(Sql)
                            If Ds.Tables(0).Rows.Count > 0 Then
                                Dim Actual As String = ""
                                Dim salto As Boolean = False
                                Me.TablaImportar.RowCount = Me.TablaImportar.RowCount + Ds.Tables(0).Rows.Count
                                For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                                    If Actual = "" Then
                                        Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                        salto = False
                                    Else
                                        salto = If(Not (Actual = Trim(Ds.Tables(0).Rows(i)(0))), True, False)
                                        If salto = True Then
                                            'Me.TablaImportar.RowCount += 1
                                            Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)
                                            Actual = Trim(Ds.Tables(0).Rows(i)(0))
                                            posicion += 1
                                        End If
                                    End If
                                    Me.TablaImportar.Item(Poliza.Index, posicion).Value = Actual
                                    Me.TablaImportar.Item(Anio.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(1))
                                    Me.TablaImportar.Item(Mes.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(2))
                                    Me.TablaImportar.Item(Tipo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(3))
                                    Me.TablaImportar.Item(Numpol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(4))
                                    Me.TablaImportar.Item(DiaPol.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(5))
                                    Me.TablaImportar.Item(Cta.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(6))
                                    Me.TablaImportar.Item(Des.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(7))
                                    Me.TablaImportar.Item(Cargo.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(8)) * 1
                                    Me.TablaImportar.Item(Abono.Index, posicion).Value = Trim(Ds.Tables(0).Rows(i)(9)) * 1
                                    Me.TablaImportar.Item(Saldo.Index, posicion).Value = 0


                                    Detalle.Add(New Poliza_DetalleN() With {.Pol = Trim(Ds.Tables(0).Rows(i)(0)), .Cuenta = Trim(Ds.Tables(0).Rows(i)(6)), .Cargo = Trim(Ds.Tables(0).Rows(i)(8)), .Int = i, .Abono = Trim(Ds.Tables(0).Rows(i)(9))})

                                    posicion += 1
                                    If i = Ds.Tables(0).Rows.Count - 1 Then
                                        'Me.TablaImportar.RowCount += 1
                                        Me.TablaImportar.Rows.Insert(posicion, Actual, "", "", "", "", "", "", "Saldo Error", 0, 0, 0)

                                        posicion += 1
                                    End If
                                Next


                                For i As Integer = Cuantos To posicion - 1
                                    Fila = Me.TablaImportar.Rows(i)
                                    If Me.TablaImportar.Item(Des.Index, i).Value = "Saldo Error" Then
                                        For Each Item In Detalle
                                            If Item.Pol = Me.TablaImportar.Item(Poliza.Index, i).Value Then
                                                DetalleA.Add(New Poliza_Detalle() With {.Cuenta = Item.Cuenta, .Cargo = Item.Cargo, .Int = i, .Abono = Item.Abono})
                                            End If
                                        Next
                                        Me.TablaImportar.Item(Saldo.Index, i).Value = Cruce_de_Gastos_P_Cobrar_Abonos_CO(DetalleA)
                                        Me.TablaImportar.Item(Poliza.Index, i).Value = ""
                                        DetalleA.Clear()
                                        If Me.TablaImportar.Item(Saldo.Index, i).Value <> 0 Then
                                            Fila.DefaultCellStyle = Error_Color
                                        Else
                                            Me.TablaImportar.Item(Saldo.Index, i).Style.BackColor = Color.YellowGreen
                                        End If
                                    End If
                                Next
                            End If
                        Catch ex As Exception

                        End Try
                    End If

            End Select
            Cuantos += 1
            frm.Barra.Value += 1
        Next

        frm.Close()
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Me.Cursor = Cursors.Arrow
    End Sub
    Private Sub CmdExportaanuales_Click(sender As Object, e As EventArgs) Handles CmdExportaanuales.Click
        If TablaImportar.RowCount > 0 Then
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Este Proceso puede tardar dependiendo de la información a exportar, presione Aceptar y espere a que el proceso termine...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = Me.TablaImportar.RowCount - 1
            Dim m_Excel As Microsoft.Office.Interop.Excel.Application
            '' Creamos un objeto WorkBook 
            Dim objLibroExcel As Microsoft.Office.Interop.Excel.Workbook
            '' Creamos un objeto WorkSheet
            Dim objHojaExcel As Microsoft.Office.Interop.Excel.Worksheet
            '' Iniciamos una instancia a Excel
            m_Excel = New Microsoft.Office.Interop.Excel.Application
            m_Excel.Visible = False
            '' Creamos una instancia del Workbooks de Excel
            '' Creamos una instancia de la primera hoja de trabajo de Excel
            objLibroExcel = m_Excel.Workbooks.Add()
            objHojaExcel = objLibroExcel.Worksheets(1)
            objHojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible
            '' Hacemos esta hoja la visible en pantalla 
            '' (como seleccionamos la primera esto no es necesario
            '' si seleccionamos una diferente a la primera si lo
            '' necesitaríamos, esto lo hacemos como forma de mostrar como cambiar de entre hojas en un documento Excel).
            objHojaExcel.Activate()
            Dim i As Integer, j As Integer
            For i = 0 To TablaImportar.Columns.Count - 1
                objHojaExcel.Cells(1, i + 1) = Me.TablaImportar.Columns.Item(i).HeaderCell.Value
            Next
            For i = 0 To TablaImportar.RowCount - 1
                frm.Barra.Value = i
                For j = 0 To TablaImportar.Columns.Count - 1
                    If j = 6 Then
                        Try
                            objHojaExcel.Cells(i + 2, j + 1) = Me.TablaImportar.Item(j, i).Value.ToString("####-####-####-####")
                        Catch ex As Exception
                            objHojaExcel.Cells(i + 2, j + 1) = Me.TablaImportar.Item(j, i).Value
                        End Try

                    ElseIf j = 8 Or j = 9 Or j = 10 Then
                        If IsNumeric(Me.TablaImportar.Item(j, i).Value) Then
                            m_Excel.Workbooks(1).Worksheets("Hoja1").Cells(i + 2, j + 1) = Convert.ToDecimal(Me.TablaImportar.Item(j, i).Value)
                            m_Excel.Workbooks(1).Worksheets("Hoja1").Columns(j + 1).NumberFormat = "$#,##0.00_);[Red]($#,##0.00)"
                        Else
                            m_Excel.Workbooks(1).Worksheets("Hoja1").Cells(i + 2, j + 1) = Me.TablaImportar.Item(j, i).Value
                        End If
                    Else
                        objHojaExcel.Cells(i + 2, j + 1) = Me.TablaImportar.Item(j, i).Value
                    End If

                Next
            Next
            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            m_Excel.Visible = True
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay registros a exportar....", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        End If
    End Sub
End Class
