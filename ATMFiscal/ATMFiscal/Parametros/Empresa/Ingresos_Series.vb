Imports Telerik.WinControls
Public Class Ingresos_Series
    Dim columna As DataGridViewTextBoxColumn
    ' Dim columna As DataGridViewTextBoxCell
    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub

    Private Sub CmdLimpiarF_Click(sender As Object, e As EventArgs) Handles CmdLimpiarF.Click
        If Me.TablaSeries.Rows.Count > 0 Then
            Me.TablaSeries.Rows.Clear()
        End If
    End Sub

    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        If Me.lstCliente.SelectText <> "" Then
            If Me.TablaSeries.Rows.Count > 0 Then
                'If Me.TablaSeries.CurrentRow.Index = 0 Then
                '    Me.TablaSeries.Rows.Insert(Me.TablaSeries.CurrentRow.Index, 1)
                'Else
                Me.TablaSeries.Rows.Insert(Me.TablaSeries.CurrentRow.Index + 1, 1)
                'End If
            Else
                Me.TablaSeries.Rows.Insert(0, 1)
                Cargar_Cuentas(Me.lstCliente.SelectItem)
            End If
        End If
    End Sub
    Private Sub Ingresos_Series_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Permiso(Me.Tag.ToString) Then

            Me.lstCliente.Cargar(" SELECT DISTINCT  Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
            Me.lstCliente.SelectItem = My.Forms.Inicio.Clt
            Eventos.DiseñoTabla(Me.TablaSeries)
        Else
            MessageBox.Show("No tienes permiso para modificar la información...", "Proyecto Contable", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Me.CmdBuscarFact.Enabled = False
            Me.CmdEliminarF.Enabled = False
            Me.CmdGuardarF.Enabled = False
            Me.CmdNuevoF.Enabled = False
            Me.lstCliente.Enabled = False
        End If
    End Sub
    Private Sub Edita_ingresos(ByVal Id As Integer, ByVal Abono As Integer, ByVal Serie As String,
                               ByVal CtaIngG As String, ByVal CtaIngEx As String, ByVal CtaIngC As String,
                               ByVal IVATras As String, ByVal ISRRet As String, ByVal IVARet As String,
                               ByVal CtaIngPCG As String, ByVal CtaIngPCE As String, ByVal CtaIngPCC As String,
                                ByVal IVAPTras As String, ByVal ISRRPA As String, ByVal IVARetPA As String,
                                 ByVal Clientes As String, ByVal AnticipoClientes As String, ByVal IvaClientes As String, ByVal IvaCancelClientes As String,
                               ByVal DevSVentasG As String, ByVal DevSVentasC As String, ByVal DevSVentasEx As String, ByVal IvaSDev As String)
        Dim sql As String = "UPDATE dbo.Series
                            SET  
	                        Serie = '" & Serie & "',
	                        Abono = " & Abono & ",
	                        CtaIngG ='" & Cuenta(CtaIngG) & "',
	                        CtaIngEx = '" & Cuenta(CtaIngEx) & "',
	                        CtaIngC = '" & Cuenta(CtaIngC) & "',
	                        IVATras = '" & Cuenta(IVATras) & "',
	                        ISRRet = '" & Cuenta(ISRRet) & "',
	                        IVARet = '" & Cuenta(IVARet) & "',
	                        CtaIngPCG = '" & Cuenta(CtaIngPCG) & "',
	                        CtaIngPCE = '" & Cuenta(CtaIngPCE) & "',
	                        CtaIngPCC = '" & Cuenta(CtaIngPCC) & "',
	                        IVAPTras = '" & Cuenta(IVAPTras) & "',
	                        ISRRPA = '" & Cuenta(ISRRPA) & "',
                              Clientes = '" & Cuenta(Clientes) & "',
	                        AnticipoClientes = '" & Cuenta(AnticipoClientes) & "',
	                        IvaClientes = '" & Cuenta(IvaClientes) & "',
	                        IvaCancelClientes = '" & Cuenta(IvaCancelClientes) & "',

                            DevSVentasG = '" & Cuenta(DevSVentasG) & "',
                            DevSVentasC = '" & Cuenta(DevSVentasC) & "',
                            DevSVentasEx = '" & Cuenta(DevSVentasEx) & "',
                            IvaSDev = '" & Cuenta(IvaSDev) & "',

	                        IVARetPA = '" & Cuenta(IVARetPA) & "'
	 where Id_Ingreso = " & Id & " and Id_Empresa = " & Me.lstCliente.SelectItem & ""
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarPolizD", sql)
        End If
    End Sub
    Private Sub Inserta_ingresos(ByVal Id As Integer, ByVal Abono As Integer, ByVal Serie As String,
                               ByVal CtaIngG As String, ByVal CtaIngEx As String, ByVal CtaIngC As String,
                               ByVal IVATras As String, ByVal ISRRet As String, ByVal IVARet As String,
                               ByVal CtaIngPCG As String, ByVal CtaIngPCE As String, ByVal CtaIngPCC As String,
                                ByVal IVAPTras As String, ByVal ISRRPA As String, ByVal IVARetPA As String,
                                 ByVal Clientes As String, ByVal AnticipoClientes As String, ByVal IvaClientes As String, ByVal IvaCancelClientes As String,
                                 ByVal DevSVentasG As String, ByVal DevSVentasC As String, ByVal DevSVentasEx As String, ByVal IvaSDev As String)
        Dim sql As String = "INSERT INTO dbo.Series
	(
	 
                            	Serie,
                            	Abono,
                            	CtaIngG,
                            	CtaIngEx,
                            	CtaIngC,
                            	IVATras,
                            	ISRRet,
                            	IVARet,
                            	CtaIngPCG,
                            	CtaIngPCE,
                            	CtaIngPCC,
                            	IVAPTras,
                            	ISRRPA,
                            	IVARetPA,
	                            Clientes,
	                            AnticipoClientes,
	                            IvaClientes,
	                            IvaCancelClientes,

                                DevSVentasG,
	                            DevSVentasC,
	                            DevSVentasEx,
	                            IvaSDev,

                                Id_Empresa
                            	)
                            VALUES 
                            	(
                            	 
                            	'" & Serie & "',
                            	" & Abono & ",
                            	'" & Cuenta(CtaIngG) & "',
                            	'" & Cuenta(CtaIngEx) & "',
                            	'" & Cuenta(CtaIngC) & "',
                            	'" & Cuenta(IVATras) & "',
                            	'" & Cuenta(ISRRet) & "',
                            	'" & Cuenta(IVARet) & "',
                            	'" & Cuenta(CtaIngPCG) & "',
                            	'" & Cuenta(CtaIngPCE) & "',
                            	'" & Cuenta(CtaIngPCC) & "',
                            	'" & Cuenta(IVAPTras) & "',
                            	'" & Cuenta(ISRRPA) & "',
                            	'" & Cuenta(IVARetPA) & "',
                                '" & Cuenta(Clientes) & "',
                            	'" & Cuenta(AnticipoClientes) & "',
                            	'" & Cuenta(IvaClientes) & "',
                            	'" & Cuenta(IvaCancelClientes) & "',

                                '" & Cuenta(DevSVentasG) & "',
                            	'" & Cuenta(DevSVentasC) & "',
                            	'" & Cuenta(DevSVentasEx) & "',
                            	'" & Cuenta(IvaSDev) & "',

                                '" & Me.lstCliente.SelectItem & "'
                            	)"


        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("InsertarPolizD", sql)
        End If
    End Sub
    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        For i As Integer = 0 To Me.TablaSeries.Rows.Count - 1
            If Me.TablaSeries.Item(ID.Index, i).Value <> Nothing Then

                Edita_ingresos(Me.TablaSeries.Item(ID.Index, i).Value, Eventos.Bool2(Me.TablaSeries.Item(Abono.Index, i).Value), Me.TablaSeries.Item(Serie.Index, i).Value,
                                Me.TablaSeries.Item(CtaIngG.Index, i).Value, Me.TablaSeries.Item(CtaIngEx.Index, i).Value, Me.TablaSeries.Item(CtaIngC.Index, i).Value,
                                    Me.TablaSeries.Item(IVATras.Index, i).Value, Me.TablaSeries.Item(ISRRet.Index, i).Value, Me.TablaSeries.Item(IVARet.Index, i).Value,
                                    Me.TablaSeries.Item(CtaIngPCG.Index, i).Value, Me.TablaSeries.Item(CtaIngPCE.Index, i).Value, Me.TablaSeries.Item(CtaIngPCC.Index, i).Value,
                                    Me.TablaSeries.Item(IVAPTras.Index, i).Value, Me.TablaSeries.Item(ISRRPA.Index, i).Value, Me.TablaSeries.Item(IVARetPA.Index, i).Value,
                                    Me.TablaSeries.Item(Clientes.Index, i).Value, Me.TablaSeries.Item(AnticipoClientes.Index, i).Value, Me.TablaSeries.Item(IvaClientes.Index, i).Value, Me.TablaSeries.Item(IvaCancelClientes.Index, i).Value,
                                    Me.TablaSeries.Item(DevSVentasG.Index, i).Value, Me.TablaSeries.Item(DevSVentasC.Index, i).Value, Me.TablaSeries.Item(DevSVentasEx.Index, i).Value, Me.TablaSeries.Item(IvaSDev.Index, i).Value)
            Else
                Inserta_ingresos(Me.TablaSeries.Item(ID.Index, i).Value, Me.TablaSeries.Item(Abono.Index, i).Value, Me.TablaSeries.Item(Serie.Index, i).Value,
                                    Me.TablaSeries.Item(CtaIngG.Index, i).Value, Me.TablaSeries.Item(CtaIngEx.Index, i).Value, Me.TablaSeries.Item(CtaIngC.Index, i).Value,
                                    Me.TablaSeries.Item(IVATras.Index, i).Value, Me.TablaSeries.Item(ISRRet.Index, i).Value, Me.TablaSeries.Item(IVARet.Index, i).Value,
                                    Me.TablaSeries.Item(CtaIngPCG.Index, i).Value, Me.TablaSeries.Item(CtaIngPCE.Index, i).Value, Me.TablaSeries.Item(CtaIngPCC.Index, i).Value,
                                    Me.TablaSeries.Item(IVAPTras.Index, i).Value, Me.TablaSeries.Item(ISRRPA.Index, i).Value, Me.TablaSeries.Item(IVARetPA.Index, i).Value,
                                    Me.TablaSeries.Item(Clientes.Index, i).Value, Me.TablaSeries.Item(AnticipoClientes.Index, i).Value, Me.TablaSeries.Item(IvaClientes.Index, i).Value, Me.TablaSeries.Item(IvaCancelClientes.Index, i).Value,
                                    Me.TablaSeries.Item(DevSVentasG.Index, i).Value, Me.TablaSeries.Item(DevSVentasC.Index, i).Value, Me.TablaSeries.Item(DevSVentasEx.Index, i).Value, Me.TablaSeries.Item(IvaSDev.Index, i).Value)
            End If
        Next
        Me.CmdBuscarFact.PerformClick()
    End Sub
    Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
        If MessageBox.Show("Realmente deseas eliminar el registro de : " & Me.lstCliente.SelectText & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            For i As Integer = 0 To Me.TablaSeries.Rows.Count - 1
                If Me.TablaSeries.Item(SelFactura.Index, i).Value <> Nothing Then
                    If Eventos.Comando_sql("Delete from dbo.Series where Id_Ingreso=" & Me.TablaSeries.Item(ID.Index, i).Value) > 0 Then

                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        Dim Ms As DialogResult = RadMessageBox.Show(Me, "Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                        Me.Text = Ms.ToString()

                        Eventos.Insertar_usuariol("Id_Ingreso", "Delete from dbo.Series where Id_Ingreso=" & Me.TablaSeries.Item(ID.Index, i).Value & "")
                    Else
                        RadMessageBox.SetThemeName("MaterialBlueGrey")
                        Dim Ms As DialogResult = RadMessageBox.Show(Me, "No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                        Me.Text = Ms.ToString()

                    End If
                End If
            Next
        End If
        Me.CmdBuscarFact.PerformClick()
    End Sub

    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        If Me.lstCliente.SelectText <> "" Then
            Buscar_Ingresos(Me.lstCliente.SelectItem)
        End If
    End Sub
    Private Function Obtener_Index(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer = -1
        Dim ds As DataSet = Eventos.Obtener_DS(" select rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE  cuenta= " & Trim(valor) & "  and Id_Empresa = " & Me.lstCliente.SelectItem & " ")
        valor = valor & "-" & ds.Tables(0).Rows(0)(0)
        For i As Integer = 0 To Col.Items.Count - 1
            If valor = Trim(Col.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next

        Return Indice
    End Function
    Private Sub Buscar_Ingresos(ByVal Cliente As Integer)
        Dim posicion As Integer = 0
        Dim Sql As String = " SELECT 	Id_Ingreso,	Serie,	Abono,	CtaIngG,	CtaIngEx,	CtaIngC,	IVATras,	ISRRet,	IVARet,	CtaIngPCG,
	                            CtaIngPCE,	CtaIngPCC,	IVAPTras,	ISRRPA,	IVARetPA,Clientes,AnticipoClientes,IvaClientes,IvaCancelClientes,DevSVentasG, DevSVentasC, DevSVentasEx, IvaSDev,	Id_Empresa FROM dbo.Series WHERE Id_Empresa = " & Cliente & " "

        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaSeries.RowCount = ds.Tables(0).Rows.Count

            Cargar_Cuentas(Cliente)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Fila As DataGridViewRow = Me.TablaSeries.Rows(i)
                Me.TablaSeries.Item(ID.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Id_Ingreso"))
                Me.TablaSeries.Item(Serie.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Serie"))
                Me.TablaSeries.Item(Abono.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Abono"))
                Try
                    If Trim(ds.Tables(0).Rows(i)("CtaIngG")) <> "" Then
                        Fila.Cells(CtaIngG.Index).Value = Me.CtaIngG.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("CtaIngG")), Me.CtaIngG))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("CtaIngEx")) <> "" Then
                        Fila.Cells(CtaIngEx.Index).Value = Me.CtaIngEx.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("CtaIngEx")), Me.CtaIngEx))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("CtaIngC")) <> "" Then
                        Fila.Cells(CtaIngC.Index).Value = Me.CtaIngC.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("CtaIngC")), Me.CtaIngC))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IVATras")) <> "" Then
                        Fila.Cells(IVATras.Index).Value = Me.IVATras.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("IVATras")), Me.IVATras))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("ISRRet")) <> "" Then
                        Fila.Cells(ISRRet.Index).Value = Me.ISRRet.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("ISRRet")), Me.ISRRet))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IVARet")) <> "" Then
                        Fila.Cells(IVARet.Index).Value = Me.IVARet.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("IVARet")), Me.IVARet))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("CtaIngPCG")) <> "" Then
                        Fila.Cells(CtaIngPCG.Index).Value = Me.CtaIngPCG.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("CtaIngPCG")), Me.CtaIngPCG))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("CtaIngPCE")) <> "" Then
                        Fila.Cells(CtaIngPCE.Index).Value = Me.CtaIngPCE.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("CtaIngPCE")), Me.CtaIngPCE))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("CtaIngPCC")) <> "" Then
                        Fila.Cells(CtaIngPCC.Index).Value = Me.CtaIngPCC.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("CtaIngPCC")), Me.CtaIngPCC))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IVAPTras")) <> "" Then
                        Fila.Cells(IVAPTras.Index).Value = Me.IVAPTras.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("IVAPTras")), Me.IVAPTras))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("ISRRPA")) <> "" Then
                        Fila.Cells(ISRRPA.Index).Value = Me.ISRRPA.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("ISRRPA")), Me.ISRRPA))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IVARetPA")) <> "" Then
                        Fila.Cells(IVARetPA.Index).Value = Me.IVARetPA.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("IVARetPA")), Me.IVARetPA))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("Clientes")) <> "" Then
                        Fila.Cells(Clientes.Index).Value = Me.Clientes.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("Clientes")), Me.Clientes))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("AnticipoClientes")) <> "" Then
                        Fila.Cells(AnticipoClientes.Index).Value = Me.AnticipoClientes.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("AnticipoClientes")), Me.AnticipoClientes))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IvaClientes")) <> "" Then
                        Fila.Cells(IvaClientes.Index).Value = Me.IvaClientes.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("IvaClientes")), Me.IvaClientes))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IvaCancelClientes")) <> "" Then
                        Fila.Cells(IvaCancelClientes.Index).Value = Me.IvaCancelClientes.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("IvaCancelClientes")), Me.IvaCancelClientes))
                    End If
                Catch ex As Exception

                End Try


                Try
                    If Trim(ds.Tables(0).Rows(i)("DevSVentasG")) <> "" Then
                        Fila.Cells(DevSVentasG.Index).Value = Me.DevSVentasG.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("DevSVentasG")), Me.DevSVentasG))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("DevSVentasC")) <> "" Then
                        Fila.Cells(DevSVentasC.Index).Value = Me.DevSVentasC.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("DevSVentasC")), Me.DevSVentasC))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("DevSVentasEx")) <> "" Then
                        Fila.Cells(DevSVentasEx.Index).Value = Me.DevSVentasEx.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("DevSVentasEx")), Me.DevSVentasEx))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IvaSDev")) <> "" Then
                        Fila.Cells(IvaSDev.Index).Value = Me.IvaSDev.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("IvaSDev")), Me.IvaSDev))
                    End If
                Catch ex As Exception

                End Try



            Next
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            Dim Ms As DialogResult = RadMessageBox.Show(Me, "Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Text = Ms.ToString()

            Me.Cursor = Cursors.Arrow

        End If
    End Sub
    Private Sub Cargar_Cuentas(ByVal Cliente As Integer)
        Dim sql As String = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 4010 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            If Me.CtaIngG.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.CtaIngG.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else
                'Try
                '    Me.CtaIngG.Items.Clear()
                'Catch ex As Exception

                'End Try

                'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                '    Me.CtaIngG.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                'Next
            End If

            If Me.CtaIngEx.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.CtaIngEx.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else
                'Try
                '    Me.CtaIngEx.Items.Clear()
                'Catch ex As Exception

                'End Try

                'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                '    Me.CtaIngEx.Items.Add(Trim(ds.Tables(0).Rows(i)("Alias")))
                'Next
            End If

            If Me.CtaIngC.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.CtaIngC.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else
                'Try
                '    Me.CtaIngC.Items.Clear()
                'Catch ex As Exception

                'End Try

                'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                '    Me.CtaIngC.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                'Next
            End If



        End If

        'Provisionados
        sql = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 4015 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            If Me.CtaIngPCG.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.CtaIngPCG.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If

            If Me.CtaIngPCE.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.CtaIngPCE.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If

            If Me.CtaIngPCC.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.CtaIngPCC.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If



        End If


        'Devoluciones


        sql = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 4020 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            If Me.DevSVentasG.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.DevSVentasG.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If

            If Me.DevSVentasEx.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.DevSVentasEx.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If

            If Me.DevSVentasC.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.DevSVentasC.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If



        End If






        'Impuestos
        Dim impuestos As String = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 2080 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.IVATras.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.IVATras.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If


        End If

        impuestos = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 1130 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.ISRRet.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.ISRRet.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If

        impuestos = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 1130 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.IVARet.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.IVARet.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else
                'Try
                '    Me.IVARet.Items.Clear()
                'Catch ex As Exception

                'End Try

                'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                '    Me.IVARet.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                'Next
            End If


        End If


        impuestos = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 2090 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.IVAPTras.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.IVAPTras.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else
                'Try
                '    Me.IVAPTras.Items.Clear()
                'Catch ex As Exception

                'End Try

                'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                '    Me.IVAPTras.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                'Next
            End If
        End If

        impuestos = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 1135 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.ISRRPA.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.ISRRPA.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If

        impuestos = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 1135 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.IVARetPA.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.IVARetPA.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If


        impuestos = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 2080 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.IvaSDev.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.IvaSDev.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If








        Dim clientes As String = ""
        clientes = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 1050 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(clientes)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.Clientes.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.Clientes.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If

        clientes = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 2060 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(clientes)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.AnticipoClientes.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.AnticipoClientes.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If

        clientes = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 2080 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(clientes)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.IvaClientes.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.IvaClientes.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If
        clientes = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 2080 AND Nivel2 > 0  and Id_Empresa = " & Me.lstCliente.SelectItem & "  "
        ds.Clear()
        ds = Eventos.Obtener_DS(clientes)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.IvaCancelClientes.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.IvaCancelClientes.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If
    End Sub
    Private Function Cuenta(ByVal Leyenda As String)
        Dim cta As String = ""
        If Leyenda = Nothing Then
            cta = ""
        Else
            Dim posi As Integer = InStr(1, Trim(Leyenda), "-", CompareMethod.Binary)
            Dim cuantos As Integer = Len(Trim(Leyenda)) - Len(Trim(Leyenda).Substring(0, posi))
            cta = Trim(Leyenda).Substring(0, 16)
        End If
        Return cta
    End Function


End Class