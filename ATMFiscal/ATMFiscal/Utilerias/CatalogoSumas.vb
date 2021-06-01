Imports Telerik.WinControls
Public Class CatalogoSumas
    Dim anio = Str(DateTime.Now.Year)
    Private Sub CatalogoSumas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_Listas()
        Dim i As Integer
        For i = DateTime.Now.Year To DateTime.Now.Year - 5 Step -1
            If i >= 2004 Then
                Me.LstAnio.Items.Add(Str(i))
            End If
        Next
        Me.LstAnio.Text = anio
    End Sub
    Private Sub Cargar_Listas()

        Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
    End Sub

    Private Sub CmdSumas_Click(sender As Object, e As EventArgs) Handles CmdSumas.Click
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelDS2("CtasSumas"), Me.Tabla)
        Try
            Me.Tabla.Rows.RemoveAt(0)

        Catch ex As Exception

        End Try
        Try
            If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "SumasAnual") = True Then
                If Me.Tabla.Rows.Count > 0 Then
                    Me.Barra.Visible = True
                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                    Me.Barra.Minimum = 0
                    Me.Barra.Value1 = 0
                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                        Dim sql As String = "INSERT INTO dbo.SumasAnual"
                        sql &= " 	("
                        sql &= "         Indice,"
                        sql &= "         Descripcion ,"
                        sql &= "         Id_Empresa,"
                        sql &= "         Anio,"
                        sql &= "         Relacion"
                        sql &= " 	)"
                        sql &= "         VALUES "
                        sql &= " 	("
                        sql &= " 	'" & Me.Tabla.Item(0, p).Value & "'," '@Cta
                        sql &= " 	'" & Me.Tabla.Item(1, p).Value & "'," '@Descripcion
                        sql &= " 	" & Me.lstCliente.SelectItem & "," '@Cliente
                        sql &= " 	" & Me.LstAnio.Text.Trim() & "," '@anio
                        sql &= " 	" & Me.Tabla.Item(2, p).Value & " " '@Tipo
                        sql &= " 	)"

                        If Eventos.Comando_sql(sql) > 0 Then
                            Eventos.Insertar_usuariol("SumasAnual", sql)
                        End If


                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                            Me.Barra.Minimum = 0
                            Me.Cursor = Cursors.Arrow
                            MessageBox.Show("Archivo Importado", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Barra.Value1 = 0
                            Me.Barra.Visible = False
                        Else
                            Me.Barra.Value1 += 1
                        End If
                    Next
                Else
                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                If MessageBox.Show("Ya existe una catalogo de sumas para el Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlo?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "SumasAnual") = True Then
                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.SumasAnual"
                                sql &= " 	("
                                sql &= "         Indice,"
                                sql &= "         Descripcion ,"
                                sql &= "         Id_Empresa,"
                                sql &= "         Anio,"
                                sql &= "         Relacion"
                                sql &= " 	)"
                                sql &= "         VALUES "
                                sql &= " 	("
                                sql &= " 	'" & Me.Tabla.Item(0, p).Value & "'," '@Cta
                                sql &= " 	'" & Me.Tabla.Item(1, p).Value & "'," '@Descripcion
                                sql &= " 	" & Me.lstCliente.SelectItem & "," '@Cliente
                                sql &= " 	" & Me.LstAnio.Text.Trim() & "," '@anio
                                sql &= " 	" & Me.Tabla.Item(2, p).Value & " " '@Tipo
                                sql &= " 	)"

                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("SumasAnual", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    MessageBox.Show("Archivo Importado", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next


                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Else
                        MessageBox.Show("No se pudo elimianr la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                Else
                    Exit Sub
                End If
            End If
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Function Buscar_tipo(ByVal Anio As Integer, ByVal Cliente As Integer, ByVal Tabla As String)
        Dim hacer As Boolean
        Dim ds As DataSet = Eventos.Obtener_DS("Select * from " & Tabla & " where   Anio = " & Anio & " and Id_Empresa = " & Cliente & "   ")
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = False
        Else
            hacer = True
        End If
        Return hacer
    End Function
    Private Function Buscar_Tablas(ByVal Anio As Integer, ByVal Cliente As Integer, ByVal Tabla As String, ByVal Mes As String)
        Dim hacer As Boolean
        Dim ds As DataSet = Eventos.Obtener_DS("Select * from " & Tabla & " where   Anio = " & Anio & " and Id_Empresa = " & Cliente & " and  Mes = " & Mes & "  ")
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = False
        Else
            hacer = True
        End If
        Return hacer
    End Function

    Private Function Buscar_tipo2(ByVal Anio As Integer, ByVal Tabla As String)
        Dim hacer As Boolean
        Dim ds As DataSet = Eventos.Obtener_DS("Select * from " & Tabla & " where   Anio = " & Anio & "     ")
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = False
        Else
            hacer = True
        End If
        Return hacer
    End Function
    Private Function Buscar_tipo3(ByVal Anio As Integer, ByVal Tabla As String, ByVal Emp As String)
        Dim hacer As Boolean
        Dim ds As DataSet = Eventos.Obtener_DS("Select * from " & Tabla & " where   Anio = " & Anio & "  AND Id_Estado = " & Emp & "   ")
        If ds.Tables(0).Rows.Count > 0 Then
            hacer = False
        Else
            hacer = True
        End If
        Return hacer
    End Function
    Private Function EliminarPlantilla(ByVal Anio As Integer, ByVal Cliente As Integer, ByVal Tabla As String)
        Dim hacer As Boolean
        Dim ds As DataSet = Eventos.Obtener_DS("delete from " & Tabla & " where   Anio = " & Anio & " and Id_Empresa = " & Cliente & "   ")
        Try
            If ds.Tables(0).Rows.Count > 0 Then
                hacer = False
            Else
                hacer = True
            End If
        Catch ex As Exception
            hacer = True
        End Try

        Return hacer
    End Function
    Private Function EliminarPlantilla2(ByVal Anio As Integer, ByVal Tabla As String)
        Dim hacer As Boolean
        Dim ds As DataSet = Eventos.Obtener_DS("delete from " & Tabla & " where   Anio = " & Anio & " ")
        Try
            If ds.Tables(0).Rows.Count > 0 Then
                hacer = False
            Else
                hacer = True
            End If
        Catch ex As Exception
            hacer = True
        End Try

        Return hacer
    End Function
    Private Function EliminarPlantilla3(ByVal Anio As Integer, ByVal Tabla As String, ByVal Emp As String)
        Dim hacer As Boolean
        Dim ds As DataSet = Eventos.Obtener_DS("delete from " & Tabla & " where   Anio = " & Anio & " AND Id_Estado =" & Emp & " ")
        Try
            If ds.Tables(0).Rows.Count > 0 Then
                hacer = False
            Else
                hacer = True
            End If
        Catch ex As Exception
            hacer = True
        End Try

        Return hacer
    End Function
    Private Function EliminarPlantillaTablas(ByVal Anio As Integer, ByVal Cliente As Integer, ByVal Tabla As String, ByVal Mes As String)
        Dim hacer As Boolean
        Dim ds As DataSet = Eventos.Obtener_DS("delete from " & Tabla & " where   Anio = " & Anio & " and Id_Empresa = " & Cliente & "  and  Mes = '" & Mes & "' ")
        Try
            If ds.Tables(0).Rows.Count > 0 Then
                hacer = False
            Else
                hacer = True
            End If
        Catch ex As Exception
            hacer = True
        End Try

        Return hacer
    End Function

    Private Sub CmdCosto_Click(sender As Object, e As EventArgs) Handles CmdCosto.Click
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelDS2("Costo"), Me.Tabla)
        Try
            Me.Tabla.Rows.RemoveAt(0)

        Catch ex As Exception

        End Try
        Try
            If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "ConciliacionCosto") = True Then
                If Me.Tabla.Rows.Count > 0 Then
                    Me.Barra.Visible = True
                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                    Me.Barra.Minimum = 0
                    Me.Barra.Value1 = 0
                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                        Dim sql As String = "INSERT INTO dbo.ConciliacionCosto"
                        sql &= " 	("
                        sql &= "         Descripcion ,"
                        sql &= "         Id_Empresa,"
                        sql &= "         Anio,"
                        sql &= "         Relacion"
                        sql &= " 	)"
                        sql &= "         VALUES "
                        sql &= " 	("
                        sql &= " 	'" & Me.Tabla.Item(0, p).Value & "'," '@Descripcion
                        sql &= " 	" & Me.lstCliente.SelectItem & "," '@Cliente
                        sql &= " 	" & Me.LstAnio.Text.Trim() & "," '@anio
                        sql &= " 	" & Me.Tabla.Item(1, p).Value & " " '@Tipo
                        sql &= " 	)"

                        If Eventos.Comando_sql(sql) > 0 Then
                            Eventos.Insertar_usuariol("ConciliacionCosto", sql)
                        End If


                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                            Me.Barra.Minimum = 0
                            Me.Cursor = Cursors.Arrow
                            MessageBox.Show("Archivo Importado", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Me.Barra.Value1 = 0
                            Me.Barra.Visible = False
                        Else
                            Me.Barra.Value1 += 1
                        End If
                    Next
                Else
                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                If MessageBox.Show("Ya existe una catalogo de susmas para el Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlo?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "ConciliacionCosto") = True Then
                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.ConciliacionCosto"
                                sql &= " 	("
                                sql &= "         Descripcion ,"
                                sql &= "         Id_Empresa,"
                                sql &= "         Anio,"
                                sql &= "         Relacion"
                                sql &= " 	)"
                                sql &= "         VALUES "
                                sql &= " 	("
                                sql &= " 	'" & Me.Tabla.Item(0, p).Value & "'," '@Descripcion
                                sql &= " 	" & Me.lstCliente.SelectItem & "," '@Cliente
                                sql &= " 	" & Me.LstAnio.Text.Trim() & "," '@anio
                                sql &= " 	" & Me.Tabla.Item(1, p).Value & " " '@Tipo
                                sql &= " 	)"

                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("ConciliacionCosto", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    MessageBox.Show("Archivo Importado", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next


                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Else
                        MessageBox.Show("No se pudo elimianr la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                Else
                    Exit Sub
                End If
            End If
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmdCatalogo_Click(sender As Object, e As EventArgs) Handles CmdCatalogo.Click
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelDS2("Catalogo"), Me.Tabla)
        Try
            Me.Tabla.Rows.RemoveAt(0)

        Catch ex As Exception

        End Try
        QuitaCuentas()
        For i As Integer = 0 To Me.Tabla.RowCount - 1
            ActualizaCuentaCatalogo(Me.Tabla.Item(1, i).Value, Me.lstCliente.SelectItem, Me.Tabla.Item(0, i).Value.ToString.Replace("-", "").PadRight(16, "0"))
        Next
    End Sub
    Private Sub QuitaCuentas()
        Dim Filas As Integer = 0

        Filas = Me.Tabla.RowCount - 1
        Try
            For j As Integer = 0 To Filas
                Try
                    For i As Integer = 0 To Me.Tabla.RowCount - 1
                        If Me.Tabla.Item(0, i).Value Is Nothing Or Me.Tabla.Item(0, i).Value.ToString = DBNull.Value.ToString Then
                            Me.Tabla.Rows.RemoveAt(i)
                            Exit For
                        End If
                    Next
                Catch ex As Exception

                End Try
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub ActualizaCuentaCatalogo(ByVal Enlace As String, ByVal Cliente As Integer, ByVal Cuenta As String)
        Dim sql As String = "UPDATE dbo.Catalogo_de_Cuentas SET CuentaEnlace =  '" & Enlace & "' WHERE Cuenta = " & Cuenta & " AND Id_Empresa =" & Cliente & " "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("CatLigas", sql)
        End If
    End Sub

    Private Sub CmdAnual_Click(sender As Object, e As EventArgs) Handles CmdAnual.Click

        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        With OpenFD
            .Title = "Seleccionar archivo de Excel"
            .Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Archivos de Excel (*.xls)| *.xls"

            .Multiselect = False
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Exit Sub
            End If
        End With
#Region "Tablas insersion masiva"
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Estado"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "EstadoResultadosAnual") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = "INSERT INTO dbo.EstadoResultadosAnual"
                            sql &= " 	("
                            sql &= "         Descripcion ,"
                            sql &= "         Id_Empresa,"
                            sql &= "         Anio,"
                            sql &= "         Tipo,"
                            sql &= "         Cta_PR,"
                            sql &= "         Nivel_PR,"
                            sql &= "         Cta_PNR,"
                            sql &= "         Nivel_PNR,"
                            sql &= "         Partes_Relacionadas,"
                            sql &= "         Partes_no_Relacionadas,"
                            sql &= "         Total"
                            sql &= " 	)"
                            sql &= "         VALUES "
                            sql &= " 	("
                            sql &= " 	'" & Me.Tabla.Item(0, p).Value & "'," '@Descripcion
                            sql &= " 	" & Me.lstCliente.SelectItem & "," '@Cliente
                            sql &= " 	" & Me.LstAnio.Text.Trim() & "," '@anio
                            sql &= " 	" & Me.Tabla.Item(8, p).Value & ", " '@Tipo
                            sql &= " 	'" & Me.Tabla.Item(4, p).Value & "', " '@CTAPR
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(5, p).Value) = True, 0, Me.Tabla.Item(5, p).Value) & ", " '@NIVELPR
                            sql &= " 	'" & Me.Tabla.Item(6, p).Value & "', " '@CTAPNR
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(7, p).Value) = True, 0, Me.Tabla.Item(7, p).Value) & ", " '@NIVELPNR
                            sql &= " 	0, " '@PR
                            sql &= " 	0, " '@PNR
                            sql &= " 	0 " '@total
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("EstadoResultadosAnual", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del Estado de Resultados", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para Estado de resultados Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "EstadoResultadosAnual") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1

                                    Dim sql As String = "INSERT INTO dbo.EstadoResultadosAnual"
                                    sql &= " 	("
                                    sql &= "         Descripcion ,"
                                    sql &= "         Id_Empresa,"
                                    sql &= "         Anio,"
                                    sql &= "         Tipo,"
                                    sql &= "         Cta_PR,"
                                    sql &= "         Nivel_PR,"
                                    sql &= "         Cta_PNR,"
                                    sql &= "         Nivel_PNR,"
                                    sql &= "         Partes_Relacionadas,"
                                    sql &= "         Partes_no_Relacionadas,"
                                    sql &= "         Total"
                                    sql &= " 	)"
                                    sql &= "         VALUES "
                                    sql &= " 	("
                                    sql &= " 	'" & Me.Tabla.Item(0, p).Value & "'," '@Descripcion
                                    sql &= " 	" & Me.lstCliente.SelectItem & "," '@Cliente
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & "," '@anio
                                    sql &= " 	" & Me.Tabla.Item(8, p).Value & ", " '@Tipo
                                    sql &= " 	'" & Me.Tabla.Item(4, p).Value & "', " '@CTAPR
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(5, p).Value) = True, 0, Me.Tabla.Item(5, p).Value) & ", " '@NIVELPR
                                    sql &= " 	'" & Me.Tabla.Item(6, p).Value & "', " '@CTAPNR
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(7, p).Value) = True, 0, Me.Tabla.Item(7, p).Value) & ", " '@NIVELPNR
                                    sql &= " 	0, " '@PR
                                    sql &= " 	0, " '@PNR
                                    sql &= " 	0 " '@total
                                    sql &= " 	)"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("EstadoResultadosAnual", sql)
                                    End If

                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del Estado de Resultados", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el Estado de Resultados", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo elimianr la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Balance"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "BalanceAnual") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = "INSERT INTO dbo.BalanceAnual"
                            sql &= " 	("
                            sql &= "         Descripcion ,"
                            sql &= "         Id_Empresa,"
                            sql &= "         Anio,"
                            sql &= "         Tipo,"
                            sql &= "         Cta_R,"
                            sql &= "         Nivel_R,"
                            sql &= "         Formula,"
                            sql &= "         Total"
                            sql &= " 	)"
                            sql &= "         VALUES "
                            sql &= " 	("
                            sql &= " 	'" & Me.Tabla.Item(0, p).Value & "'," '@Descripcion
                            sql &= " 	" & Me.lstCliente.SelectItem & "," '@Cliente
                            sql &= " 	" & Me.LstAnio.Text.Trim() & "," '@anio
                            sql &= " 	" & Me.Tabla.Item(3, p).Value & ", " '@Tipo
                            sql &= " 	'" & Me.Tabla.Item(4, p).Value & "', " '@CTAPR
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(5, p).Value) = True, 0, Me.Tabla.Item(5, p).Value) & ", " '@NIVELPR
                            sql &= " 	'" & Me.Tabla.Item(2, p).Value & "', " '@Formula
                            sql &= " 	0 " '@total
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("BalanceAnual", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del Balance", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para Balance Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "BalanceAnual") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1

                                    Dim sql As String = "INSERT INTO dbo.BalanceAnual"
                                    sql &= " 	("
                                    sql &= "         Descripcion ,"
                                    sql &= "         Id_Empresa,"
                                    sql &= "         Anio,"
                                    sql &= "         Tipo,"
                                    sql &= "         Cta_R,"
                                    sql &= "         Nivel_R,"
                                    sql &= "         Formula,"
                                    sql &= "         Total"
                                    sql &= " 	)"
                                    sql &= "         VALUES "
                                    sql &= " 	("
                                    sql &= " 	'" & Me.Tabla.Item(0, p).Value & "'," '@Descripcion
                                    sql &= " 	" & Me.lstCliente.SelectItem & "," '@Cliente
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & "," '@anio
                                    sql &= " 	" & Me.Tabla.Item(3, p).Value & ", " '@Tipo
                                    sql &= " 	'" & Me.Tabla.Item(4, p).Value & "', " '@CTAPR
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(5, p).Value) = True, 0, Me.Tabla.Item(5, p).Value) & ", " '@NIVELPR
                                    sql &= " 	'" & Me.Tabla.Item(2, p).Value & "', " '@Formula
                                    sql &= " 	0 " '@total
                                    sql &= " 	)"
                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("EstadoResultadosAnual", sql)
                                    End If

                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del Balance", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el Balance", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "CUCA"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "CUCA") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1



                            Dim sql As String = "INSERT INTO dbo.CUCA"
                            sql &= " ("
                            sql &= " Id_Empresa,"
                            sql &= " Fecha,"
                            sql &= " Capital_Anterior,"
                            sql &= " INPC_Anterior,"
                            sql &= " INPC_Actual,"
                            sql &= " Factor,"
                            sql &= " Capital_Actualizado,"
                            sql &= " Concepto,"
                            sql &= " Cantidad,"
                            sql &= " Capital_de_Aportacion,"
                            sql &= " anio"
                            sql &= " )"
                            sql &= "     VALUES"
                            sql &= "("
                            sql &= " " & Me.lstCliente.SelectItem & ","
                            sql &= " " & Eventos.Sql_hoy(Me.Tabla.Item(0, p).Value) & ","
                            sql &= " " & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                            sql &= " " & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ","
                            sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & ","
                            sql &= " " & IIf(IsDBNull(Me.Tabla.Item(4, p).Value) = True, 0, Me.Tabla.Item(4, p).Value) & ","
                            sql &= " " & IIf(IsDBNull(Me.Tabla.Item(5, p).Value) = True, 0, Me.Tabla.Item(5, p).Value) & ","
                            sql &= " '" & IIf(IsDBNull(Me.Tabla.Item(6, p).Value) = True, "", Me.Tabla.Item(6, p).Value) & "',"
                            sql &= " " & IIf(IsDBNull(Me.Tabla.Item(7, p).Value) = True, 0, Me.Tabla.Item(7, p).Value) & ","
                            sql &= " " & IIf(IsDBNull(Me.Tabla.Item(8, p).Value) = True, 0, Me.Tabla.Item(8, p).Value) & ","
                            sql &= " " & IIf(IsDBNull(Me.Tabla.Item(9, p).Value) = True, 0, Me.Tabla.Item(9, p).Value) & ""
                            sql &= " )"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("CUCA", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del CUCA", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para CUCA Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "CUCA") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1

                                    Dim sql As String = "INSERT INTO dbo.CUCA"
                                    sql &= " ("
                                    sql &= " Id_Empresa,"
                                    sql &= " Fecha,"
                                    sql &= " Capital_Anterior,"
                                    sql &= " INPC_Anterior,"
                                    sql &= " INPC_Actual,"
                                    sql &= " Factor,"
                                    sql &= " Capital_Actualizado,"
                                    sql &= " Concepto,"
                                    sql &= " Cantidad,"
                                    sql &= " Capital_de_Aportacion,"
                                    sql &= " anio"
                                    sql &= " )"
                                    sql &= "     VALUES"
                                    sql &= "("
                                    sql &= " " & Me.lstCliente.SelectItem & ","
                                    sql &= " " & Eventos.Sql_hoy(Me.Tabla.Item(0, p).Value) & ","
                                    sql &= " " & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                                    sql &= " " & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ","
                                    sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & ","
                                    sql &= " " & IIf(IsDBNull(Me.Tabla.Item(4, p).Value) = True, 0, Me.Tabla.Item(4, p).Value) & ","
                                    sql &= " " & IIf(IsDBNull(Me.Tabla.Item(5, p).Value) = True, 0, Me.Tabla.Item(5, p).Value) & ","
                                    sql &= " '" & IIf(IsDBNull(Me.Tabla.Item(6, p).Value) = True, "", Me.Tabla.Item(6, p).Value) & "',"
                                    sql &= " " & IIf(IsDBNull(Me.Tabla.Item(7, p).Value) = True, 0, Me.Tabla.Item(7, p).Value) & ","
                                    sql &= " " & IIf(IsDBNull(Me.Tabla.Item(8, p).Value) = True, 0, Me.Tabla.Item(8, p).Value) & ","
                                    sql &= " " & IIf(IsDBNull(Me.Tabla.Item(9, p).Value) = True, 0, Me.Tabla.Item(9, p).Value) & ""
                                    sql &= " )"
                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("CUCA", sql)
                                    End If

                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del CUCA", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el CUCA", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "PTU"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "PTU") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = " INSERT INTO dbo.PTU"
                            sql &= "("
                            sql &= " Concepto,"
                            sql &= " Importes,"
                            sql &= " Cuenta_Liga,"
                            sql &= " Formula, Anio , Id_Empresa "
                            sql &= " )"
                            sql &= "  VALUES "
                            sql &= "("
                            sql &= " '" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= " " & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                            sql &= " '" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, "", Me.Tabla.Item(2, p).Value) & "',"
                            sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & " , " & Me.LstAnio.Text.Trim() & " , " & Me.lstCliente.SelectItem & ""
                            sql &= " )"



                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("PTU", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del PTU", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para PTU Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "PTU") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1

                                    Dim sql As String = " INSERT INTO dbo.PTU"
                                    sql &= "("
                                    sql &= " Concepto,"
                                    sql &= " Importes,"
                                    sql &= " Cuenta_Liga,"
                                    sql &= " Formula, Anio , Id_Empresa "
                                    sql &= " )"
                                    sql &= "  VALUES "
                                    sql &= "("
                                    sql &= " '" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= " " & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                                    sql &= " '" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, "", Me.Tabla.Item(2, p).Value) & "',"
                                    sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & " , " & Me.LstAnio.Text.Trim() & " , " & Me.lstCliente.SelectItem & ""
                                    sql &= " )"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("PTU", sql)
                                    End If

                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del PTU", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el PTU", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "CUFIN"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo2(Me.LstAnio.Text.Trim(), "CUFIN") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = "  INSERT INTO dbo.CUFIN"
                            sql &= "("
                            sql &= " 	Fecha_Movimiento,"
                            sql &= " 	Saldo_Anterior,"
                            sql &= " 	INPC_Anterior,"
                            sql &= " 	INPC_Actual,"
                            sql &= " 	Factor,"
                            sql &= " 	Saldo_Actualizado,"
                            sql &= " 	Concepto,"
                            sql &= " 	Cantidad,"
                            sql &= " 	Saldo_de_la_CUFIN,"
                            sql &= " 	Reinvertida,"
                            sql &= " 	Anio"
                            sql &= " 	)"
                            sql &= " VALUES "
                            sql &= " 	("
                            sql &= " 	" & Eventos.Sql_hoy(Me.Tabla.Item(0, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(4, p).Value) = True, 0, Me.Tabla.Item(4, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(5, p).Value) = True, 0, Me.Tabla.Item(5, p).Value) & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(6, p).Value) = True, "", Me.Tabla.Item(6, p).Value) & "',"
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(7, p).Value) = True, 0, Me.Tabla.Item(7, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(8, p).Value) = True, 0, Me.Tabla.Item(8, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(9, p).Value) = True, 0, Me.Tabla.Item(9, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(10, p).Value) = True, 0, Me.Tabla.Item(10, p).Value) & ""
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("CUFIN", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del CUFIN", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para CUFIN Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "CUFIN") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = "  INSERT INTO dbo.CUFIN"
                                    sql &= "("
                                    sql &= " 	Fecha_Movimiento,"
                                    sql &= " 	Saldo_Anterior,"
                                    sql &= " 	INPC_Anterior,"
                                    sql &= " 	INPC_Actual,"
                                    sql &= " 	Factor,"
                                    sql &= " 	Saldo_Actualizado,"
                                    sql &= " 	Concepto,"
                                    sql &= " 	Cantidad,"
                                    sql &= " 	Saldo_de_la_CUFIN,"
                                    sql &= " 	Anio,"
                                    sql &= " 	Reinvertida"
                                    sql &= " 	)"
                                    sql &= " VALUES "
                                    sql &= " 	("
                                    sql &= " 	" & Eventos.Sql_hoy(Me.Tabla.Item(0, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(4, p).Value) = True, 0, Me.Tabla.Item(4, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(5, p).Value) = True, 0, Me.Tabla.Item(5, p).Value) & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(6, p).Value) = True, "", Me.Tabla.Item(6, p).Value) & "',"
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(7, p).Value) = True, 0, Me.Tabla.Item(7, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(8, p).Value) = True, 0, Me.Tabla.Item(8, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(9, p).Value) = True, 0, Me.Tabla.Item(9, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(10, p).Value) = True, 0, Me.Tabla.Item(10, p).Value) & ""
                                    sql &= " 	)"
                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("CUFIN", sql)
                                    End If

                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del CUFIN", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el CUFIN", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "COEFI"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "CoefiyPerdidas") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = " INSERT INTO dbo.CoefiyPerdidas"
                            sql &= "("
                            sql &= " 	Id_Empresa,"
                            sql &= " 	Descripcion,"
                            sql &= " 	Anio,"
                            sql &= " 	Formula"
                            sql &= " 	)"
                            sql &= " VALUES "
                            sql &= " 	("
                            sql &= " 	" & Me.lstCliente.SelectItem & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ""
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("CoefiyPerdidas", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del COEFI", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para COEFI Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "COEFI") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = " INSERT INTO dbo.CoefiyPerdidas"
                                    sql &= "("
                                    sql &= " 	Id_Empresa,"
                                    sql &= " 	Descripcion,"
                                    sql &= " 	Anio,"
                                    sql &= " 	Formula"
                                    sql &= " 	)"
                                    sql &= " VALUES "
                                    sql &= " 	("
                                    sql &= " 	" & Me.lstCliente.SelectItem & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & ","
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ""
                                    sql &= " 	)"
                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("COEFI", sql)
                                    End If

                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del COEFI", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el COEFI", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "CCF"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "ResConFis") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = " INSERT INTO dbo.ResConFis"
                            sql &= "("
                            sql &= " 	Id_Empresa,"
                            sql &= " 	Anio,"
                            sql &= " 	Descripcion,"
                            sql &= " 	Importe,"
                            sql &= " 	Formula,"
                            sql &= " 	CtaLiga,"
                            sql &= " 	Tabla,"
                            sql &= " 	Titulo"
                            sql &= " 	)"
                            sql &= " VALUES "
                            sql &= "("
                            sql &= " 	" & Me.lstCliente.SelectItem & ","
                            sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= " 	0,"
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, "", Me.Tabla.Item(2, p).Value) & "',"
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(4, p).Value) = True, 0, Me.Tabla.Item(4, p).Value) & ""
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("ResConFis", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del CCF", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para CCF Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "CCF") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = " INSERT INTO dbo.ResConFis"
                                    sql &= "("
                                    sql &= " 	Id_Empresa,"
                                    sql &= " 	Anio,"
                                    sql &= " 	Descripcion,"
                                    sql &= " 	Importe,"
                                    sql &= " 	Formula,"
                                    sql &= " 	CtaLiga,"
                                    sql &= " 	Tabla,"
                                    sql &= " 	Titulo"
                                    sql &= " 	)"
                                    sql &= " VALUES "
                                    sql &= "("
                                    sql &= " 	" & Me.lstCliente.SelectItem & ","
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= " 	0,"
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, "", Me.Tabla.Item(2, p).Value) & "',"
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(4, p).Value) = True, 0, Me.Tabla.Item(4, p).Value) & ""
                                    sql &= " 	)"
                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("CCF", sql)
                                    End If

                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del CCF", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el CCF", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "CVF"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "CVF") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = " INSERT INTO dbo.CVF"
                            sql &= "("
                            sql &= " 	Id_Empresa,"
                            sql &= " 	Anio,"
                            sql &= " 	Descripcion,"
                            sql &= " 	valor,"

                            sql &= " 	Columna, Titulo,Tabla"
                            sql &= " )"
                            sql &= " VALUES "
                            sql &= " ("
                            sql &= " 	" & Me.lstCliente.SelectItem & ","
                            sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= " 	'',"
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & "," & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & ""
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("CVF", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del CVF", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para CVF Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "CVF") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = " INSERT INTO dbo.CVF"
                                    sql &= "("
                                    sql &= " 	Id_Empresa,"
                                    sql &= " 	Anio,"
                                    sql &= " 	Descripcion,"
                                    sql &= " 	valor,"

                                    sql &= " 	Columna, Titulo,Tabla"
                                    sql &= " )"
                                    sql &= " VALUES "
                                    sql &= " ("
                                    sql &= " 	" & Me.lstCliente.SelectItem & ","
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= " 	'',"
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & "," & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & ""
                                    sql &= " 	)"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("CVF", sql)
                                    End If


                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del CVF", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el CVF", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "INVR"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "INV") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = " INSERT INTO dbo.INV"
                            sql &= "("
                            sql &= " 	Id_Empresa,"
                            sql &= " 	Anio,"
                            sql &= " 	Descripcion,"
                            sql &= " 	DeducEjer,"

                            sql &= " 	DeducIEjer, AdqDurEjer,Formula"
                            sql &= " )"
                            sql &= " VALUES "
                            sql &= " ("
                            sql &= " 	" & Me.lstCliente.SelectItem & ","
                            sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= " 	0,"
                            sql &= " 	0,"
                            sql &= " 	0," & IIf(IsDBNull(Me.Tabla.Item(4, p).Value) = True, 0, Me.Tabla.Item(4, p).Value) & ""
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("INV", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del INV", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para INV Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "INV") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = " INSERT INTO dbo.INV"
                                    sql &= "("
                                    sql &= " 	Id_Empresa,"
                                    sql &= " 	Anio,"
                                    sql &= " 	Descripcion,"
                                    sql &= " 	DeducEjer,"

                                    sql &= " 	DeducIEjer, AdqDurEjer,Formula"
                                    sql &= " )"
                                    sql &= " VALUES "
                                    sql &= " ("
                                    sql &= " 	" & Me.lstCliente.SelectItem & ","
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= " 	0,"
                                    sql &= " 	0,"
                                    sql &= " 	0," & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & ""
                                    sql &= " 	)"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("INV", sql)
                                    End If


                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del INV", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el INV", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "DDA"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "DDA") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = " INSERT INTO dbo.DDA"
                            sql &= "("
                            sql &= " 	Id_Empresa,"
                            sql &= " 	Anio,"
                            sql &= " 	Descripcion,Importe,"
                            sql &= " 	Columna,"
                            sql &= " 	CtaLiga, Formula "
                            sql &= " )"
                            sql &= " VALUES "
                            sql &= " ("
                            sql &= " 	" & Me.lstCliente.SelectItem & ","
                            sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= " 	0,"
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, "", Me.Tabla.Item(2, p).Value) & "'," & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & ""
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("DDA", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del DDA", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para DDA Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "DDA") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = " INSERT INTO dbo.DDA"
                                    sql &= "("
                                    sql &= " 	Id_Empresa,"
                                    sql &= " 	Anio,"
                                    sql &= " 	Descripcion,Importe"
                                    sql &= " 	Columna,"
                                    sql &= " 	CtaLiga, Formula "
                                    sql &= " )"
                                    sql &= " VALUES "
                                    sql &= " ("
                                    sql &= " 	" & Me.lstCliente.SelectItem & ","
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= " 	0,"
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, "", Me.Tabla.Item(2, p).Value) & "'," & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & ""
                                    sql &= " 	)"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("DDA", sql)
                                    End If


                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del DDA", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el DDA", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "CCE"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "CCE") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = " INSERT INTO dbo.CCE"
                            sql &= "("
                            sql &= " 	Id_Empresa,"
                            sql &= " 	Anio,"
                            sql &= " 	Descripcion,Importe,"
                            sql &= " 	Columna,"
                            sql &= " 	 Formula "
                            sql &= " )"
                            sql &= " VALUES "
                            sql &= " ("
                            sql &= " 	" & Me.lstCliente.SelectItem & ","
                            sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= " 	0,"
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ""
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("CCE", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del CCE", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para CCE Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "CCE") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = " INSERT INTO dbo.CCE"
                                    sql &= "("
                                    sql &= " 	Id_Empresa,"
                                    sql &= " 	Anio,"
                                    sql &= " 	Descripcion,Importe,"
                                    sql &= " 	Columna,"
                                    sql &= " 	 Formula "
                                    sql &= " )"
                                    sql &= " VALUES "
                                    sql &= " ("
                                    sql &= " 	" & Me.lstCliente.SelectItem & ","
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= " 	0,"
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ""
                                    sql &= " 	)"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("CCE", sql)
                                    End If


                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del CCE", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el CCE", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "PTUAnual"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "PTUAnual") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = " INSERT INTO dbo.PTUAnual"
                            sql &= "("
                            sql &= " 	Id_Empresa,"
                            sql &= " 	Anio,"
                            sql &= " 	Descripcion,Importe,"
                            sql &= " 	Columna,"
                            sql &= " 	 Formula "
                            sql &= " )"
                            sql &= " VALUES "
                            sql &= " ("
                            sql &= " 	" & Me.lstCliente.SelectItem & ","
                            sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= " 	0,"
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ""
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("PTUAnual", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del PTUAnual", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para PTUAnual Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "PTUAnual") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = " INSERT INTO dbo.PTUAnual"
                                    sql &= "("
                                    sql &= " 	Id_Empresa,"
                                    sql &= " 	Anio,"
                                    sql &= " 	Descripcion,Importe,"
                                    sql &= " 	Columna,"
                                    sql &= " 	 Formula "
                                    sql &= " )"
                                    sql &= " VALUES "
                                    sql &= " ("
                                    sql &= " 	" & Me.lstCliente.SelectItem & ","
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= " 	0,"
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ""
                                    sql &= " 	)"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("PTUAnual", sql)
                                    End If


                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del PTUAnual", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el PTUAnual", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "DUD"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "DUD") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = " INSERT INTO dbo.DUD"
                            sql &= "("
                            sql &= " 	Id_Empresa,"
                            sql &= " 	Anio,"
                            sql &= " 	Descripcion,Importe,"
                            sql &= " 	Columna,"
                            sql &= " 	 Formula "
                            sql &= " )"
                            sql &= " VALUES "
                            sql &= " ("
                            sql &= " 	" & Me.lstCliente.SelectItem & ","
                            sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= " 	0,"
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ""
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("DUD", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del DUD", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para DUD Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "DUD") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = " INSERT INTO dbo.DUD"
                                    sql &= "("
                                    sql &= " 	Id_Empresa,"
                                    sql &= " 	Anio,"
                                    sql &= " 	Descripcion,Importe,"
                                    sql &= " 	Columna,"
                                    sql &= " 	 Formula "
                                    sql &= " )"
                                    sql &= " VALUES "
                                    sql &= " ("
                                    sql &= " 	" & Me.lstCliente.SelectItem & ","
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= " 	0,"
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ""
                                    sql &= " 	)"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("DUD", sql)
                                    End If


                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del DUD", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el DUD", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "DISR"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "DISR") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = " INSERT INTO dbo.DISR"
                            sql &= "("
                            sql &= " 	Id_Empresa,"
                            sql &= " 	Anio,"
                            sql &= " 	Descripcion,Importe,"
                            sql &= " 	Columna,"
                            sql &= " 	 Formula,CtaLiga "
                            sql &= " )"
                            sql &= " VALUES "
                            sql &= " ("
                            sql &= " 	" & Me.lstCliente.SelectItem & ","
                            sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= " 	0,"
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ",'" & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, "", Me.Tabla.Item(3, p).Value) & "'"
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("DISR", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del DISR", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para DISR Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "DISR") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = " INSERT INTO dbo.DISR"
                                    sql &= "("
                                    sql &= " 	Id_Empresa,"
                                    sql &= " 	Anio,"
                                    sql &= " 	Descripcion,Importe,"
                                    sql &= " 	Columna,"
                                    sql &= " 	 Formula ,CtaLiga"
                                    sql &= " )"
                                    sql &= " VALUES "
                                    sql &= " ("
                                    sql &= " 	" & Me.lstCliente.SelectItem & ","
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= " 	0,"
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ",'" & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, "", Me.Tabla.Item(3, p).Value) & "'"
                                    sql &= " 	)"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("DISR", sql)
                                    End If


                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del DISR", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el DISR", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "DIISR"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "DIISR") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = " INSERT INTO dbo.DIISR"
                            sql &= "("
                            sql &= " 	Id_Empresa,"
                            sql &= " 	Anio,"
                            sql &= " 	Descripcion,Importe,"
                            sql &= " 	Columna,"
                            sql &= " 	 Formula "
                            sql &= " )"
                            sql &= " VALUES "
                            sql &= " ("
                            sql &= " 	" & Me.lstCliente.SelectItem & ","
                            sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= " 	0,"
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & " "
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("DIISR", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del DIISR", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para DIISR Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "DIISR") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = " INSERT INTO dbo.DIISR"
                                    sql &= "("
                                    sql &= " 	Id_Empresa,"
                                    sql &= " 	Anio,"
                                    sql &= " 	Descripcion,Importe,"
                                    sql &= " 	Columna,"
                                    sql &= " 	Formula  "
                                    sql &= " )"
                                    sql &= " VALUES "
                                    sql &= " ("
                                    sql &= " 	" & Me.lstCliente.SelectItem & ","
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= " 	0,"
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ""
                                    sql &= " 	)"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("DIISR", sql)
                                    End If


                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del DIISR", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el DIISR", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "DIDEF"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "DIDEF") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = " INSERT INTO dbo.DIDEF"
                            sql &= "("
                            sql &= " 	Id_Empresa,"
                            sql &= " 	Anio,"
                            sql &= " 	Descripcion,Importe,"
                            sql &= " 	Columna,"
                            sql &= " 	 Formula "
                            sql &= " )"
                            sql &= " VALUES "
                            sql &= " ("
                            sql &= " 	" & Me.lstCliente.SelectItem & ","
                            sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                            sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= " 	0,"
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                            sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & " "
                            sql &= " 	)"

                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("DIDEF", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del DIDEF", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para DIISR Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "DIDEF") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = " INSERT INTO dbo.DIDEF"
                                    sql &= "("
                                    sql &= " 	Id_Empresa,"
                                    sql &= " 	Anio,"
                                    sql &= " 	Descripcion,Importe,"
                                    sql &= " 	Columna,"
                                    sql &= " 	Formula  "
                                    sql &= " )"
                                    sql &= " VALUES "
                                    sql &= " ("
                                    sql &= " 	" & Me.lstCliente.SelectItem & ","
                                    sql &= " 	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= " 	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= " 	0,"
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ","
                                    sql &= " 	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, 0, Me.Tabla.Item(2, p).Value) & ""
                                    sql &= " 	)"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("DIDEF", sql)
                                    End If


                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del DIDEF", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el DIDEF", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
#End Region

    End Sub

    Private Sub CmdInv_Click(sender As Object, e As EventArgs) Handles CmdInv.Click
        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        With OpenFD
            .Title = "Seleccionar archivo de Excel"
            .Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Archivos de Excel (*.xls)| *.xls"

            .Multiselect = False
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Exit Sub
            End If
        End With
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "RINV"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "ResumenDepreciaciones") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = "INSERT INTO dbo.ResumenDepreciaciones"
                            sql &= "("
                            sql &= "	Descripcion,"
                            sql &= "	DeduccionenEjercicio,"
                            sql &= "	DeduccionInenEjercicio,"
                            sql &= "	AdquisicionesEjercicio,"
                            sql &= "	DepreciacionContable,"
                            sql &= "	MOI,"
                            sql &= "	Anio,"
                            sql &= "	Id_Empresa,"
                            sql &= "	Formula,Cta_Liga,Titulo"
                            sql &= "	)"
                            sql &= "VALUES "
                            sql &= "("
                            sql &= "	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= "	0,"
                            sql &= "	0,"
                            sql &= "	0,"
                            sql &= "	0,"
                            sql &= "	0,"
                            sql &= "	" & Me.LstAnio.Text.Trim() & ","
                            sql &= "	" & Me.lstCliente.SelectItem & ", NULL,"
                            sql &= "	'" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, "", Me.Tabla.Item(1, p).Value) & "','" & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & "'"

                            sql &= "	)"



                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("RD", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del Resumen Inversiones", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para Resumen Inversiones Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "ResumenDepreciaciones") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = "INSERT INTO dbo.ResumenDepreciaciones"
                                    sql &= "("
                                    sql &= "	Descripcion,"
                                    sql &= "	DeduccionenEjercicio,"
                                    sql &= "	DeduccionInenEjercicio,"
                                    sql &= "	AdquisicionesEjercicio,"
                                    sql &= "	DepreciacionContable,"
                                    sql &= "	MOI,"
                                    sql &= "	Anio,"
                                    sql &= "	Id_Empresa,"
                                    sql &= "	Formula,Cta_Liga,Titulo"
                                    sql &= "	)"
                                    sql &= "VALUES "
                                    sql &= "("
                                    sql &= "	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= "	0,"
                                    sql &= "	0,"
                                    sql &= "	0,"
                                    sql &= "	0,"
                                    sql &= "	0,"
                                    sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= "	" & Me.lstCliente.SelectItem & ", NULL,"
                                    sql &= "	'" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, "", Me.Tabla.Item(1, p).Value) & "','" & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, 0, Me.Tabla.Item(3, p).Value) & "'"

                                    sql &= "	)"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("RD", sql)
                                    End If


                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del Resumen Inversiones", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el Resumen Inversiones", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If

        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "INV"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "ResumenINV") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = "INSERT INTO dbo.ResumenINV"
                            sql &= "("
                            sql &= "	Descripcion,"
                            sql &= "	Total,"
                            sql &= "	Total_Neto,"
                            sql &= "	Anio,"
                            sql &= "	Id_Empresa,Formula"
                            sql &= "	)"
                            sql &= "VALUES "
                            sql &= "("
                            sql &= "	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= "	0,"
                            sql &= "	0,"
                            sql &= "	" & Me.LstAnio.Text.Trim() & ","
                            sql &= "	" & Me.lstCliente.SelectItem & "," & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ""
                            sql &= "	)"


                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("RInD", sql)
                            End If


                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para calculo del Resumen Inversiones", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para Resumen Inversiones Anual del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "ResumenINV") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1
                                    Dim sql As String = "INSERT INTO dbo.ResumenINV"
                                    sql &= "("
                                    sql &= "	Descripcion,"
                                    sql &= "	Total,"
                                    sql &= "	Total_Neto,"
                                    sql &= "	Anio,"
                                    sql &= "	Id_Empresa,Formula"
                                    sql &= "	)"
                                    sql &= "VALUES "
                                    sql &= "("
                                    sql &= "	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= "	0,"
                                    sql &= "	0,"
                                    sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= "	" & Me.lstCliente.SelectItem & "," & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, 0, Me.Tabla.Item(1, p).Value) & ""
                                    sql &= "	)"

                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("RInD", sql)
                                    End If


                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para calculo del Resumen Inversiones", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next


                            Else
                                MessageBox.Show("No se Importaron los datos para el Resumen Inversiones", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If
    End Sub

    Private Sub CmdImpuestos_Click(sender As Object, e As EventArgs) Handles CmdImpuestos.Click
        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        With OpenFD
            .Title = "Seleccionar archivo de Excel"
            .Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Archivos de Excel (*.xls)| *.xls"

            .Multiselect = False
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Exit Sub
            End If
        End With
        Dim PM As Integer = 0
        Dim RFC As String = Eventos.ObtenerValorDB("Empresa", "Reg_fed_causantes", " Id_Empresa  = " & Me.lstCliente.SelectItem & "", True)
        If Len(RFC) = 12 Then
            'Moral
            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "PM"), Me.Tabla)
            PM = 1
        Else
            'Fisica
            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "PF"), Me.Tabla)
            PM = 0
        End If
#Region "Asignar Estados"
        Dim Ds As DataSet = Eventos.Obtener_DS("SELECT convert(VARCHAR,Id_Estado) + '-' + Nombre_Estado  AS Estado FROM Estados ORDER BY Nombre_Estado")
        Dim cuenta As String = ""
        Dim Lst(,) As String
        ReDim Lst(2, Ds.Tables(0).Rows.Count + 1)
        For s As Integer = 0 To Ds.Tables(0).Rows.Count - 1
            Lst(0, s) = Ds.Tables(0).Rows(s)(0)
            Debug.Print(Ds.Tables(0).Rows(s)(0))
            Lst(1, s) = "0"
        Next
        With My.Forms.DialogUnaSeleccion
            .limpiar()
            .Titulo = Eventos.titulo_app
            .Texto = "Selecciona Estado para Ingresar los datos:"
            .MinSeleccion = 1
            .MaxSeleccion = 1
            .elementos = Lst
            .ShowDialog()
            Lst = .elementos
            If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End With

        Dim ValorCampo As String = ""
        For s As Integer = 0 To Lst.GetLength(1) ' por cada elemento 
            If Lst(1, s) = "1" Then ' Si se encuentra seleccionada la opcion la paso a la variable
                ValorCampo = Lst(0, s) 'Se asigna el valor
                Exit For
            End If
        Next
        ValorCampo = Trim(ValorCampo)
        Dim posi As Integer = InStr(1, ValorCampo, "-", CompareMethod.Binary)
        ValorCampo = ValorCampo.Substring(0, posi - 1)

#End Region

        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "ImpuestosPMPF") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = "INSERT INTO dbo.ImpuestosPMPF"
                            sql &= "("
                            sql &= "  Descripcion,"
                            sql &= "  Cuenta,"
                            sql &= "  Naturaleza,"
                            sql &= "  anio,"
                            sql &= "  Id_Empresa,"
                            sql &= "  Suma,"
                            sql &= "  Operacion,"
                            sql &= "  Tipo,"
                            sql &= "  Pm ,Id_Estado"
                            sql &= "	)"
                            sql &= "VALUES "
                            sql &= "("
                            sql &= "	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                            sql &= "	'" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, "", Me.Tabla.Item(1, p).Value) & "',"
                            sql &= "	'" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, "", Me.Tabla.Item(2, p).Value) & "',"
                            sql &= "	" & Me.LstAnio.Text.Trim() & ","
                            sql &= "	" & Me.lstCliente.SelectItem & ", "
                            sql &= " '" & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, "", Me.Tabla.Item(3, p).Value) & "',"
                            sql &= " '" & IIf(IsDBNull(Me.Tabla.Item(4, p).Value) = True, "", Me.Tabla.Item(4, p).Value) & "',"
                            sql &= " '" & IIf(IsDBNull(Me.Tabla.Item(5, p).Value) = True, 0, Me.Tabla.Item(5, p).Value) & "',"
                            sql &= " " & PM & "," & ValorCampo & ""
                            sql &= "	)"
                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("IPM", sql)
                            End If
                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                MessageBox.Show("Datos cargados para el Calculo de Impuestos de " & IIf(PM = 1, "Persona Moral", "Persona Fisica") & "", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    If MessageBox.Show("Ya existen datos para el Calculo de Impuestos de " & IIf(PM = 1, "Persona Moral", "Persona Fisica") & " del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla(Me.LstAnio.Text.Trim(), Me.lstCliente.SelectItem, "ImpuestosPMPF") = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1

                                    Dim sql As String = "INSERT INTO dbo.ImpuestosPMPF"
                                    sql &= "("
                                    sql &= "  Descripcion,"
                                    sql &= "  Cuenta,"
                                    sql &= "  Naturaleza,"
                                    sql &= "  anio,"
                                    sql &= "  Id_Empresa,"
                                    sql &= "  Suma,"
                                    sql &= "  Operacion,"
                                    sql &= "  Tipo,"
                                    sql &= "  Pm ,Id_Estado"
                                    sql &= "	)"
                                    sql &= "VALUES "
                                    sql &= "("
                                    sql &= "	'" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value) = True, "", Me.Tabla.Item(0, p).Value) & "',"
                                    sql &= "	'" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value) = True, "", Me.Tabla.Item(1, p).Value) & "',"
                                    sql &= "	'" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value) = True, "", Me.Tabla.Item(2, p).Value) & "',"
                                    sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= "	" & Me.lstCliente.SelectItem & ", "
                                    sql &= " '" & IIf(IsDBNull(Me.Tabla.Item(3, p).Value) = True, "", Me.Tabla.Item(3, p).Value) & "',"
                                    sql &= " '" & IIf(IsDBNull(Me.Tabla.Item(4, p).Value) = True, "", Me.Tabla.Item(4, p).Value) & "',"
                                    sql &= " '" & IIf(IsDBNull(Me.Tabla.Item(5, p).Value) = True, 0, Me.Tabla.Item(5, p).Value) & "',"
                                    sql &= " " & PM & "," & ValorCampo & ""
                                    sql &= "	)"


                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("IPM", sql)
                                    End If


                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        MessageBox.Show("Datos cargados para el Calculo de Impuestos de " & IIf(PM = 1, "Persona Moral", "Persona Fisica") & "", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next
                            Else
                                MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try
            Me.Tabla.Columns.Clear()

        End If


    End Sub

    Private Sub CmdTablas_Click(sender As Object, e As EventArgs) Handles CmdTablas.Click

        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        With OpenFD
            .Title = "Seleccionar archivo de Excel"
            .Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Archivos de Excel (*.xls)| *.xls"

            .Multiselect = False
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Exit Sub
            End If
        End With
        Dim PM As Integer = 0




        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Enero"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then

            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo2(Me.LstAnio.Text.Trim(), "TarifasImpuestosPFPM") = True Then
                    If Me.Tabla.Rows.Count > 0 Then
                        Me.Barra.Visible = True
                        Me.Barra.Maximum = Me.Tabla.RowCount - 1
                        Me.Barra.Minimum = 0
                        Me.Barra.Value1 = 0
                        For p As Integer = 0 To Me.Tabla.RowCount - 1

                            Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                            sql &= "("
                            sql &= "  Limite_Inferior,"
                            sql &= "  Limite_Superior,"
                            sql &= "  Cuota_Fija,"
                            sql &= "  anio,"
                            sql &= "  PrcAplicacion,"
                            sql &= "  Mes,"
                            sql &= " PF  "
                            sql &= "	)"
                            sql &= "VALUES "
                            sql &= "("
                            sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                            sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                            sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                            sql &= "	" & Me.LstAnio.Text.Trim() & ","
                            sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                            sql &= " '01',"
                            sql &= " " & PM & " "
                            sql &= "	)"
                            If Eventos.Comando_sql(sql) > 0 Then
                                Eventos.Insertar_usuariol("TarfIPM", sql)
                            End If
                            If Me.Barra.Value1 = Me.Barra.Maximum Then
                                Me.Barra.Minimum = 0
                                Me.Cursor = Cursors.Arrow
                                Me.Barra.Value1 = 0
                                Me.Barra.Visible = False
                            Else
                                Me.Barra.Value1 += 1
                            End If
                        Next
                        Me.Tabla.Columns.Clear()
                    Else
                        MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Febrero"), Me.Tabla)
                    Try

                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Tabla.Rows.RemoveAt(0)
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                sql &= "("
                                sql &= "  Limite_Inferior,"
                                sql &= "  Limite_Superior,"
                                sql &= "  Cuota_Fija,"
                                sql &= "  anio,"
                                sql &= "  PrcAplicacion,"
                                sql &= "  Mes,"
                                sql &= " PF  "
                                sql &= "	)"
                                sql &= "VALUES "
                                sql &= "("
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                sql &= " '02',"
                                sql &= " " & PM & " "
                                sql &= "	)"


                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("IPM", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next
                            Me.Tabla.Columns.Clear()
                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    Catch ex As Exception

                    End Try
                    Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Marzo"), Me.Tabla)
                    Try

                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Tabla.Rows.RemoveAt(0)
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                sql &= "("
                                sql &= "  Limite_Inferior,"
                                sql &= "  Limite_Superior,"
                                sql &= "  Cuota_Fija,"
                                sql &= "  anio,"
                                sql &= "  PrcAplicacion,"
                                sql &= "  Mes,"
                                sql &= "  PF "
                                sql &= "	)"
                                sql &= "VALUES "
                                sql &= "("
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                sql &= " '03',"
                                sql &= " " & PM & " "
                                sql &= "	)"


                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("IPM", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next
                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Me.Tabla.Columns.Clear()
                    Catch ex As Exception

                    End Try
                    Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Abril"), Me.Tabla)
                    Try

                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Tabla.Rows.RemoveAt(0)
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                sql &= "("
                                sql &= "  Limite_Inferior,"
                                sql &= "  Limite_Superior,"
                                sql &= "  Cuota_Fija,"
                                sql &= "  anio,"
                                sql &= "  PrcAplicacion,"
                                sql &= "  Mes,"
                                sql &= "  PF "
                                sql &= "	)"
                                sql &= "VALUES "
                                sql &= "("
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                sql &= " '04',"
                                sql &= " " & PM & " "
                                sql &= "	)"


                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("IPM", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next
                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Me.Tabla.Columns.Clear()
                    Catch ex As Exception

                    End Try
                    Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Mayo"), Me.Tabla)
                    Try

                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Tabla.Rows.RemoveAt(0)
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                sql &= "("
                                sql &= "  Limite_Inferior,"
                                sql &= "  Limite_Superior,"
                                sql &= "  Cuota_Fija,"
                                sql &= "  anio,"
                                sql &= "  PrcAplicacion,"
                                sql &= "  Mes,"
                                sql &= " PF  "
                                sql &= "	)"
                                sql &= "VALUES "
                                sql &= "("
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                sql &= " '05',"
                                sql &= " " & PM & " "
                                sql &= "	)"


                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("IPM", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next
                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Me.Tabla.Columns.Clear()
                    Catch ex As Exception

                    End Try
                    Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Junio"), Me.Tabla)
                    Try

                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Tabla.Rows.RemoveAt(0)
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                sql &= "("
                                sql &= "  Limite_Inferior,"
                                sql &= "  Limite_Superior,"
                                sql &= "  Cuota_Fija,"
                                sql &= "  anio,"
                                sql &= "  PrcAplicacion,"
                                sql &= "  Mes,"
                                sql &= "  PF "
                                sql &= "	)"
                                sql &= "VALUES "
                                sql &= "("
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                sql &= " '06',"
                                sql &= " " & PM & " "
                                sql &= "	)"


                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("IPM", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next
                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Me.Tabla.Columns.Clear()
                    Catch ex As Exception

                    End Try
                    Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Julio"), Me.Tabla)
                    Try

                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Tabla.Rows.RemoveAt(0)
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                sql &= "("
                                sql &= "  Limite_Inferior,"
                                sql &= "  Limite_Superior,"
                                sql &= "  Cuota_Fija,"
                                sql &= "  anio,"
                                sql &= "  PrcAplicacion,"
                                sql &= "  Mes,"
                                sql &= " PF  "
                                sql &= "	)"
                                sql &= "VALUES "
                                sql &= "("
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                sql &= " '07',"
                                sql &= " " & PM & " "
                                sql &= "	)"


                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("IPM", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next
                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Me.Tabla.Columns.Clear()
                    Catch ex As Exception

                    End Try
                    Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Agosto"), Me.Tabla)
                    Try

                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Tabla.Rows.RemoveAt(0)
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                sql &= "("
                                sql &= "  Limite_Inferior,"
                                sql &= "  Limite_Superior,"
                                sql &= "  Cuota_Fija,"
                                sql &= "  anio,"
                                sql &= "  PrcAplicacion,"
                                sql &= "  Mes,"
                                sql &= "  PF "
                                sql &= "	)"
                                sql &= "VALUES "
                                sql &= "("
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                sql &= " '08',"
                                sql &= " " & PM & " "
                                sql &= "	)"


                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("IPM", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next
                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Me.Tabla.Columns.Clear()
                    Catch ex As Exception

                    End Try
                    Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Septiembre"), Me.Tabla)
                    Try

                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Tabla.Rows.RemoveAt(0)
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                sql &= "("
                                sql &= "  Limite_Inferior,"
                                sql &= "  Limite_Superior,"
                                sql &= "  Cuota_Fija,"
                                sql &= "  anio,"
                                sql &= "  PrcAplicacion,"
                                sql &= "  Mes,"
                                sql &= " PF  "
                                sql &= "	)"
                                sql &= "VALUES "
                                sql &= "("
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                sql &= " '09',"
                                sql &= " " & PM & " "
                                sql &= "	)"


                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("IPM", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next
                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Me.Tabla.Columns.Clear()
                    Catch ex As Exception

                    End Try
                    Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Octubre"), Me.Tabla)
                    Try

                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Tabla.Rows.RemoveAt(0)
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                sql &= "("
                                sql &= "  Limite_Inferior,"
                                sql &= "  Limite_Superior,"
                                sql &= "  Cuota_Fija,"
                                sql &= "  anio,"
                                sql &= "  PrcAplicacion,"
                                sql &= "  Mes,"
                                sql &= "  PF "
                                sql &= "	)"
                                sql &= "VALUES "
                                sql &= "("
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                sql &= " '10',"
                                sql &= " " & PM & " "
                                sql &= "	)"


                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("IPM", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next
                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Me.Tabla.Columns.Clear()
                    Catch ex As Exception

                    End Try
                    Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Noviembre"), Me.Tabla)
                    Try

                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Tabla.Rows.RemoveAt(0)
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                sql &= "("
                                sql &= "  Limite_Inferior,"
                                sql &= "  Limite_Superior,"
                                sql &= "  Cuota_Fija,"
                                sql &= "  anio,"
                                sql &= "  PrcAplicacion,"
                                sql &= "  Mes,"
                                sql &= "  PF "
                                sql &= "	)"
                                sql &= "VALUES "
                                sql &= "("
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                sql &= " '11',"
                                sql &= " " & PM & " "
                                sql &= "	)"


                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("IPM", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next
                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Me.Tabla.Columns.Clear()
                    Catch ex As Exception

                    End Try
                    Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Diciembre"), Me.Tabla)
                    Try

                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Tabla.Rows.RemoveAt(0)
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1

                                Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                sql &= "("
                                sql &= "  Limite_Inferior,"
                                sql &= "  Limite_Superior,"
                                sql &= "  Cuota_Fija,"
                                sql &= "  anio,"
                                sql &= "  PrcAplicacion,"
                                sql &= "  Mes,"
                                sql &= "  PF "
                                sql &= "	)"
                                sql &= "VALUES "
                                sql &= "("
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                sql &= " '12',"
                                sql &= " " & PM & " "
                                sql &= "	)"


                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("IPM", sql)
                                End If


                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next
                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Me.Tabla.Columns.Clear()
                    Catch ex As Exception

                    End Try
                Else
                    If MessageBox.Show("Ya existen datos para el Calculo de Impuestos de " & IIf(PM = 1, "Persona Moral", "Persona Fisica") & " del Cliente: " & Me.lstCliente.SelectText & " deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If EliminarPlantilla2(Me.LstAnio.Text.Trim(), "TarifasImpuestosPFPM") = True Then
                            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Enero"), Me.Tabla)
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Tabla.Rows.RemoveAt(0)
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1

                                    Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                    sql &= "("
                                    sql &= "  Limite_Inferior,"
                                    sql &= "  Limite_Superior,"
                                    sql &= "  Cuota_Fija,"
                                    sql &= "  anio,"
                                    sql &= "  PrcAplicacion,"
                                    sql &= "  Mes,"
                                    sql &= "  PF "
                                    sql &= "	)"
                                    sql &= "VALUES "
                                    sql &= "("
                                    sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                    sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                    sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                    sql &= "	" & Me.LstAnio.Text.Trim() & ","

                                    sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                    sql &= " '01',"
                                    sql &= " " & PM & " "
                                    sql &= "	)"
                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("TarfIPM", sql)
                                    End If
                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next
                                Me.Tabla.Columns.Clear()
                            Else
                                MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Febrero"), Me.Tabla)
                            Try

                                If Me.Tabla.Rows.Count > 0 Then
                                    Me.Tabla.Rows.RemoveAt(0)
                                    Me.Barra.Visible = True
                                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                    Me.Barra.Minimum = 0
                                    Me.Barra.Value1 = 0
                                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                                        Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                        sql &= "("
                                        sql &= "  Limite_Inferior,"
                                        sql &= "  Limite_Superior,"
                                        sql &= "  Cuota_Fija,"
                                        sql &= "  anio,"
                                        sql &= "  PrcAplicacion,"
                                        sql &= "  Mes,"
                                        sql &= "  PF "
                                        sql &= "	)"
                                        sql &= "VALUES "
                                        sql &= "("
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                        sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= " '02',"
                                        sql &= " " & PM & " "
                                        sql &= "	)"


                                        If Eventos.Comando_sql(sql) > 0 Then
                                            Eventos.Insertar_usuariol("IPM", sql)
                                        End If


                                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                                            Me.Barra.Minimum = 0
                                            Me.Cursor = Cursors.Arrow
                                            Me.Barra.Value1 = 0
                                            Me.Barra.Visible = False
                                        Else
                                            Me.Barra.Value1 += 1
                                        End If
                                    Next
                                    Me.Tabla.Columns.Clear()
                                Else
                                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Catch ex As Exception

                            End Try
                            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Marzo"), Me.Tabla)
                            Try

                                If Me.Tabla.Rows.Count > 0 Then
                                    Me.Tabla.Rows.RemoveAt(0)
                                    Me.Barra.Visible = True
                                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                    Me.Barra.Minimum = 0
                                    Me.Barra.Value1 = 0
                                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                                        Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                        sql &= "("
                                        sql &= "  Limite_Inferior,"
                                        sql &= "  Limite_Superior,"
                                        sql &= "  Cuota_Fija,"
                                        sql &= "  anio,"

                                        sql &= "  PrcAplicacion,"
                                        sql &= "  Mes,"
                                        sql &= " PF "
                                        sql &= "	)"
                                        sql &= "VALUES "
                                        sql &= "("
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                        sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= " '03',"
                                        sql &= " " & PM & " "
                                        sql &= "	)"


                                        If Eventos.Comando_sql(sql) > 0 Then
                                            Eventos.Insertar_usuariol("IPM", sql)
                                        End If


                                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                                            Me.Barra.Minimum = 0
                                            Me.Cursor = Cursors.Arrow
                                            Me.Barra.Value1 = 0
                                            Me.Barra.Visible = False
                                        Else
                                            Me.Barra.Value1 += 1
                                        End If
                                    Next
                                    Me.Tabla.Columns.Clear()
                                Else
                                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            Catch ex As Exception

                            End Try
                            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Abril"), Me.Tabla)
                            Try

                                If Me.Tabla.Rows.Count > 0 Then
                                    Me.Tabla.Rows.RemoveAt(0)
                                    Me.Barra.Visible = True
                                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                    Me.Barra.Minimum = 0
                                    Me.Barra.Value1 = 0
                                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                                        Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                        sql &= "("
                                        sql &= "  Limite_Inferior,"
                                        sql &= "  Limite_Superior,"
                                        sql &= "  Cuota_Fija,"
                                        sql &= "  anio,"
                                        sql &= "  PrcAplicacion,"
                                        sql &= "  Mes,"
                                        sql &= " PF "
                                        sql &= "	)"
                                        sql &= "VALUES "
                                        sql &= "("
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                        sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= " '04',"
                                        sql &= " " & PM & " "
                                        sql &= "	)"


                                        If Eventos.Comando_sql(sql) > 0 Then
                                            Eventos.Insertar_usuariol("IPM", sql)
                                        End If


                                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                                            Me.Barra.Minimum = 0
                                            Me.Cursor = Cursors.Arrow
                                            Me.Barra.Value1 = 0
                                            Me.Barra.Visible = False
                                        Else
                                            Me.Barra.Value1 += 1
                                        End If
                                    Next

                                Else
                                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                Me.Tabla.Columns.Clear()
                            Catch ex As Exception

                            End Try
                            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Mayo"), Me.Tabla)
                            Try

                                If Me.Tabla.Rows.Count > 0 Then
                                    Me.Tabla.Rows.RemoveAt(0)
                                    Me.Barra.Visible = True
                                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                    Me.Barra.Minimum = 0
                                    Me.Barra.Value1 = 0
                                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                                        Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                        sql &= "("
                                        sql &= "  Limite_Inferior,"
                                        sql &= "  Limite_Superior,"
                                        sql &= "  Cuota_Fija,"
                                        sql &= "  anio,"
                                        sql &= "  PrcAplicacion,"
                                        sql &= "  Mes,"
                                        sql &= "  PF "
                                        sql &= "	)"
                                        sql &= "VALUES "
                                        sql &= "("
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                        sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= " '05',"
                                        sql &= " " & PM & " "
                                        sql &= "	)"


                                        If Eventos.Comando_sql(sql) > 0 Then
                                            Eventos.Insertar_usuariol("IPM", sql)
                                        End If


                                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                                            Me.Barra.Minimum = 0
                                            Me.Cursor = Cursors.Arrow
                                            Me.Barra.Value1 = 0
                                            Me.Barra.Visible = False
                                        Else
                                            Me.Barra.Value1 += 1
                                        End If
                                    Next

                                Else
                                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                Me.Tabla.Columns.Clear()
                            Catch ex As Exception

                            End Try
                            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Junio"), Me.Tabla)
                            Try

                                If Me.Tabla.Rows.Count > 0 Then
                                    Me.Tabla.Rows.RemoveAt(0)
                                    Me.Barra.Visible = True
                                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                    Me.Barra.Minimum = 0
                                    Me.Barra.Value1 = 0
                                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                                        Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                        sql &= "("
                                        sql &= "  Limite_Inferior,"
                                        sql &= "  Limite_Superior,"
                                        sql &= "  Cuota_Fija,"
                                        sql &= "  anio,"
                                        sql &= "  PrcAplicacion,"
                                        sql &= "  Mes,"
                                        sql &= "  PF "
                                        sql &= "	)"
                                        sql &= "VALUES "
                                        sql &= "("
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                        sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= " '06',"
                                        sql &= " " & PM & " "
                                        sql &= "	)"


                                        If Eventos.Comando_sql(sql) > 0 Then
                                            Eventos.Insertar_usuariol("IPM", sql)
                                        End If


                                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                                            Me.Barra.Minimum = 0
                                            Me.Cursor = Cursors.Arrow
                                            Me.Barra.Value1 = 0
                                            Me.Barra.Visible = False
                                        Else
                                            Me.Barra.Value1 += 1
                                        End If
                                    Next

                                Else
                                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                Me.Tabla.Columns.Clear()
                            Catch ex As Exception

                            End Try
                            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Julio"), Me.Tabla)
                            Try

                                If Me.Tabla.Rows.Count > 0 Then
                                    Me.Tabla.Rows.RemoveAt(0)
                                    Me.Barra.Visible = True
                                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                    Me.Barra.Minimum = 0
                                    Me.Barra.Value1 = 0
                                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                                        Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                        sql &= "("
                                        sql &= "  Limite_Inferior,"
                                        sql &= "  Limite_Superior,"
                                        sql &= "  Cuota_Fija,"
                                        sql &= "  anio,"
                                        sql &= "  PrcAplicacion,"
                                        sql &= "  Mes,"
                                        sql &= " PF "
                                        sql &= "	)"
                                        sql &= "VALUES "
                                        sql &= "("
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                        sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= " '07',"
                                        sql &= " " & PM & " "
                                        sql &= "	)"


                                        If Eventos.Comando_sql(sql) > 0 Then
                                            Eventos.Insertar_usuariol("IPM", sql)
                                        End If


                                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                                            Me.Barra.Minimum = 0
                                            Me.Cursor = Cursors.Arrow
                                            Me.Barra.Value1 = 0
                                            Me.Barra.Visible = False
                                        Else
                                            Me.Barra.Value1 += 1
                                        End If
                                    Next

                                Else
                                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                Me.Tabla.Columns.Clear()
                            Catch ex As Exception

                            End Try
                            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Agosto"), Me.Tabla)
                            Try

                                If Me.Tabla.Rows.Count > 0 Then
                                    Me.Tabla.Rows.RemoveAt(0)
                                    Me.Barra.Visible = True
                                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                    Me.Barra.Minimum = 0
                                    Me.Barra.Value1 = 0
                                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                                        Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                        sql &= "("
                                        sql &= "  Limite_Inferior,"
                                        sql &= "  Limite_Superior,"
                                        sql &= "  Cuota_Fija,"
                                        sql &= "  anio,"
                                        sql &= "  PrcAplicacion,"
                                        sql &= "  Mes,"
                                        sql &= "  PF "
                                        sql &= "	)"
                                        sql &= "VALUES "
                                        sql &= "("
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                        sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= " '08',"
                                        sql &= " " & PM & " "
                                        sql &= "	)"

                                        If Eventos.Comando_sql(sql) > 0 Then
                                            Eventos.Insertar_usuariol("IPM", sql)
                                        End If


                                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                                            Me.Barra.Minimum = 0
                                            Me.Cursor = Cursors.Arrow
                                            Me.Barra.Value1 = 0
                                            Me.Barra.Visible = False
                                        Else
                                            Me.Barra.Value1 += 1
                                        End If
                                    Next

                                Else
                                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                Me.Tabla.Columns.Clear()
                            Catch ex As Exception

                            End Try
                            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Septiembre"), Me.Tabla)
                            Try

                                If Me.Tabla.Rows.Count > 0 Then
                                    Me.Tabla.Rows.RemoveAt(0)
                                    Me.Barra.Visible = True
                                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                    Me.Barra.Minimum = 0
                                    Me.Barra.Value1 = 0
                                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                                        Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                        sql &= "("
                                        sql &= "  Limite_Inferior,"
                                        sql &= "  Limite_Superior,"
                                        sql &= "  Cuota_Fija,"
                                        sql &= "  anio,"
                                        sql &= "  PrcAplicacion,"
                                        sql &= "  Mes,"
                                        sql &= "  PF "
                                        sql &= "	)"
                                        sql &= "VALUES "
                                        sql &= "("
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                        sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= " '09',"
                                        sql &= " " & PM & " "
                                        sql &= "	)"


                                        If Eventos.Comando_sql(sql) > 0 Then
                                            Eventos.Insertar_usuariol("IPM", sql)
                                        End If


                                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                                            Me.Barra.Minimum = 0
                                            Me.Cursor = Cursors.Arrow
                                            Me.Barra.Value1 = 0
                                            Me.Barra.Visible = False
                                        Else
                                            Me.Barra.Value1 += 1
                                        End If
                                    Next
                                Else
                                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                Me.Tabla.Columns.Clear()
                            Catch ex As Exception

                            End Try
                            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Octubre"), Me.Tabla)
                            Try

                                If Me.Tabla.Rows.Count > 0 Then
                                    Me.Tabla.Rows.RemoveAt(0)
                                    Me.Barra.Visible = True
                                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                    Me.Barra.Minimum = 0
                                    Me.Barra.Value1 = 0
                                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                                        Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                        sql &= "("
                                        sql &= "  Limite_Inferior,"
                                        sql &= "  Limite_Superior,"
                                        sql &= "  Cuota_Fija,"
                                        sql &= "  anio,"
                                        sql &= "  PrcAplicacion,"
                                        sql &= "  Mes,"
                                        sql &= "  PF "
                                        sql &= "	)"
                                        sql &= "VALUES "
                                        sql &= "("
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                        sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= " '10',"
                                        sql &= " " & PM & " "
                                        sql &= "	)"


                                        If Eventos.Comando_sql(sql) > 0 Then
                                            Eventos.Insertar_usuariol("IPM", sql)
                                        End If


                                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                                            Me.Barra.Minimum = 0
                                            Me.Cursor = Cursors.Arrow
                                            Me.Barra.Value1 = 0
                                            Me.Barra.Visible = False
                                        Else
                                            Me.Barra.Value1 += 1
                                        End If
                                    Next

                                Else
                                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                Me.Tabla.Columns.Clear()
                            Catch ex As Exception

                            End Try
                            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Noviembre"), Me.Tabla)
                            Try

                                If Me.Tabla.Rows.Count > 0 Then
                                    Me.Tabla.Rows.RemoveAt(0)
                                    Me.Barra.Visible = True
                                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                    Me.Barra.Minimum = 0
                                    Me.Barra.Value1 = 0
                                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                                        Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                        sql &= "("
                                        sql &= "  Limite_Inferior,"
                                        sql &= "  Limite_Superior,"
                                        sql &= "  Cuota_Fija,"
                                        sql &= "  anio,"
                                        sql &= "  PrcAplicacion,"
                                        sql &= "  Mes,"
                                        sql &= "  PF "
                                        sql &= "	)"
                                        sql &= "VALUES "
                                        sql &= "("
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                        sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= " '11',"
                                        sql &= " " & PM & " "
                                        sql &= "	)"


                                        If Eventos.Comando_sql(sql) > 0 Then
                                            Eventos.Insertar_usuariol("IPM", sql)
                                        End If


                                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                                            Me.Barra.Minimum = 0
                                            Me.Cursor = Cursors.Arrow
                                            Me.Barra.Value1 = 0
                                            Me.Barra.Visible = False
                                        Else
                                            Me.Barra.Value1 += 1
                                        End If
                                    Next

                                Else
                                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                Me.Tabla.Columns.Clear()
                            Catch ex As Exception

                            End Try
                            Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Diciembre"), Me.Tabla)
                            Try

                                If Me.Tabla.Rows.Count > 0 Then
                                    Me.Tabla.Rows.RemoveAt(0)
                                    Me.Barra.Visible = True
                                    Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                    Me.Barra.Minimum = 0
                                    Me.Barra.Value1 = 0
                                    For p As Integer = 0 To Me.Tabla.RowCount - 1

                                        Dim sql As String = "INSERT INTO dbo.TarifasImpuestosPFPM"
                                        sql &= "("
                                        sql &= "  Limite_Inferior,"
                                        sql &= "  Limite_Superior,"
                                        sql &= "  Cuota_Fija,"
                                        sql &= "  anio,"
                                        sql &= "  PrcAplicacion,"
                                        sql &= "  Mes,"
                                        sql &= "  PF "
                                        sql &= "	)"
                                        sql &= "VALUES "
                                        sql &= "("
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                        sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                        sql &= " '12',"
                                        sql &= " " & PM & " "
                                        sql &= "	)"


                                        If Eventos.Comando_sql(sql) > 0 Then
                                            Eventos.Insertar_usuariol("IPM", sql)
                                        End If


                                        If Me.Barra.Value1 = Me.Barra.Maximum Then
                                            Me.Barra.Minimum = 0
                                            Me.Cursor = Cursors.Arrow
                                            Me.Barra.Value1 = 0
                                            Me.Barra.Visible = False
                                        Else
                                            Me.Barra.Value1 += 1
                                        End If
                                    Next
                                Else
                                    MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                                Me.Tabla.Columns.Clear()
                            Catch ex As Exception

                            End Try


                        Else
                            MessageBox.Show("No se pudo eliminar la Pantilla", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If

                    Else
                        Exit Sub
                    End If
                End If

            Catch ex As Exception

            End Try

        End If





    End Sub

    Private Sub CmdImpuesN_Click(sender As Object, e As EventArgs) Handles CmdImpuesN.Click
        Dim OpenFD As New System.Windows.Forms.OpenFileDialog
        Dim archivo As String
        With OpenFD
            .Title = "Seleccionar archivo de Excel"
            .Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Archivos de Excel (*.xls)| *.xls"

            .Multiselect = False
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                archivo = .FileName
            Else
                Exit Sub
            End If
        End With
#Region "Asignar Estados"
        Dim Ds As DataSet = Eventos.Obtener_DS("SELECT convert(VARCHAR,Id_Estado) + '-' + Nombre_Estado  AS Estado FROM Estados ORDER BY Nombre_Estado")
        Dim cuenta As String = ""
        Dim Lst(,) As String
        ReDim Lst(2, Ds.Tables(0).Rows.Count + 1)
        For s As Integer = 0 To Ds.Tables(0).Rows.Count - 1
            Lst(0, s) = Ds.Tables(0).Rows(s)(0)
            Debug.Print(Ds.Tables(0).Rows(s)(0))
            Lst(1, s) = "0"
        Next
        With My.Forms.DialogUnaSeleccion
            .limpiar()
            .Titulo = Eventos.titulo_app
            .Texto = "Selecciona Estado para Ingresar los datos:"
            .MinSeleccion = 1
            .MaxSeleccion = 1
            .elementos = Lst
            .ShowDialog()
            Lst = .elementos
            If .DialogResult = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End With

        Dim ValorCampo As String = ""
        For s As Integer = 0 To Lst.GetLength(1) ' por cada elemento 
            If Lst(1, s) = "1" Then ' Si se encuentra seleccionada la opcion la paso a la variable
                ValorCampo = Lst(0, s) 'Se asigna el valor
                Exit For
            End If
        Next
        ValorCampo = Trim(ValorCampo)
        Dim posi As Integer = InStr(1, ValorCampo, "-", CompareMethod.Binary)
        ValorCampo = ValorCampo.Substring(0, posi - 1)

#End Region
        Eventos.LlenarDataGrid_DS(Eventos.CargarExcelStandar(archivo, "Tabla"), Me.Tabla)
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.RemoveAt(0)
            Try
                If Buscar_tipo3(Me.LstAnio.Text.Trim(), "TablaImpuestosNominas", ValorCampo) = True Then
                    Try
                        If Me.Tabla.Rows.Count > 0 Then
                            Me.Tabla.Rows.RemoveAt(0)
                            Me.Barra.Visible = True
                            Me.Barra.Maximum = Me.Tabla.RowCount - 1
                            Me.Barra.Minimum = 0
                            Me.Barra.Value1 = 0
                            For p As Integer = 0 To Me.Tabla.RowCount - 1
                                Dim sql As String = "INSERT INTO dbo.TablaImpuestosNominas"
                                sql &= "("
                                sql &= "  Limite_Inferior,"
                                sql &= "  Limite_Superior,"
                                sql &= "  Cuota_Fija,"
                                sql &= "  anio,"
                                sql &= "  PrcAplicacion,"
                                sql &= "  Id_Estado"
                                sql &= "	)"
                                sql &= "VALUES "
                                sql &= "("
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                sql &= " " & ValorCampo & ""

                                sql &= "	)"
                                If Eventos.Comando_sql(sql) > 0 Then
                                    Eventos.Insertar_usuariol("IPM", sql)
                                End If
                                If Me.Barra.Value1 = Me.Barra.Maximum Then
                                    Me.Barra.Minimum = 0
                                    Me.Cursor = Cursors.Arrow
                                    Me.Barra.Value1 = 0
                                    Me.Barra.Visible = False
                                Else
                                    Me.Barra.Value1 += 1
                                End If
                            Next
                        Else
                            MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Me.Tabla.Columns.Clear()
                    Catch ex As Exception

                    End Try
                Else
                    If MessageBox.Show("Ya existen datos para el calculo de Nominas  deseas reemplazarlos?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        If EliminarPlantilla3(Me.LstAnio.Text.Trim(), "TablaImpuestosNominas", ValorCampo) = True Then
                            If Me.Tabla.Rows.Count > 0 Then
                                Me.Barra.Visible = True
                                Me.Barra.Maximum = Me.Tabla.RowCount - 1
                                Me.Barra.Minimum = 0
                                Me.Barra.Value1 = 0
                                For p As Integer = 0 To Me.Tabla.RowCount - 1

                                    Dim sql As String = "INSERT INTO dbo.TablaImpuestosNominas"
                                    sql &= "("
                                    sql &= "  Limite_Inferior,"
                                    sql &= "  Limite_Superior,"
                                    sql &= "  Cuota_Fija,"
                                    sql &= "  anio,"
                                    sql &= "  PrcAplicacion,"
                                    sql &= "  Id_Estado"
                                    sql &= "	)"
                                    sql &= "VALUES "
                                    sql &= "("
                                    sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(0, p).Value.ToString.Replace(",", "")) & ","
                                    sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(1, p).Value.ToString.Replace(",", "")) & ","
                                    sql &= "	" & IIf(IsDBNull(Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(2, p).Value.ToString.Replace(",", "")) & ","
                                    sql &= "	" & Me.LstAnio.Text.Trim() & ","
                                    sql &= " " & IIf(IsDBNull(Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) = True, 0, Me.Tabla.Item(3, p).Value.ToString.Replace(",", "")) & ","
                                    sql &= " " & ValorCampo & ""
                                    sql &= "	)"
                                    If Eventos.Comando_sql(sql) > 0 Then
                                        Eventos.Insertar_usuariol("IPM", sql)
                                    End If
                                    If Me.Barra.Value1 = Me.Barra.Maximum Then
                                        Me.Barra.Minimum = 0
                                        Me.Cursor = Cursors.Arrow
                                        Me.Barra.Value1 = 0
                                        Me.Barra.Visible = False
                                    Else
                                        Me.Barra.Value1 += 1
                                    End If
                                Next
                            Else
                                MessageBox.Show("No se ha Importado ningun archivo", Eventos.titulo_app, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                            Me.Tabla.Columns.Clear()
                        End If

                    End If


                End If
            Catch ex As Exception

            End Try

        End If


    End Sub
End Class
