Imports Telerik.WinControls
Public Class ClavesEgresosGastos
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
                Me.TablaSeries.Rows.Add()
            Else
                Me.TablaSeries.Rows.Insert(0, 1)
                Cargar_Cuentas(1)
            End If
        End If
    End Sub
    Private Sub Ingresos_Series_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.lstCliente.Cargar(" SELECT DISTINCT Empresa.Id_Empresa, Empresa.Razon_Social, Usuarios.Usuario
                            FROM     Empresa INNER JOIN
                            Control_Equipos_Clientes ON Empresa.Id_Empresa = Control_Equipos_Clientes.Id_Empresa INNER JOIN
                            Equipos ON Control_Equipos_Clientes.Id_Equipo = Equipos.Id_Equipo INNER JOIN
                            Usuarios_Equipos ON Equipos.Id_Equipo = Usuarios_Equipos.Id_Equipo INNER JOIN
                            Usuarios ON Usuarios_Equipos.ID_Usuario = Usuarios.ID_Usuario
                            WHERE  (Usuarios.Usuario LIKE '%" & My.Forms.Inicio.LblUsuario.Text & "%')")
        Me.lstCliente.SelectItem = 1
        Me.CmdBuscarFact.PerformClick()
        Eventos.DiseñoTabla(Me.TablaSeries)
    End Sub
    ''' <summary>
    ''' ' Esta region permite automatizar la insersion de informacion'
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="Clave"></param>
    ''' <param name="CtaEgG"></param>
    ''' <param name="CtaEgEx"></param>
    ''' <param name="CtaEgC"></param>
    ''' <param name="IVAAcre"></param>
    ''' <param name="CtaPEgG"></param>
    ''' <param name="CtaPEgEx"></param>
    ''' <param name="CtaPEgC"></param>
    ''' <param name="IVAPAcre"></param>
    ''' <param name="Egresos"></param>
    ''' <param name="Tasa"></param>
#Region "Edicion e Insersion de Claves Egresos"
    Private Sub Edita_ingresos(ByVal Id As Integer, ByVal Clave As String,
                               ByVal CtaEgG As String, ByVal CtaEgEx As String, ByVal CtaEgC As String,
                               ByVal IVAAcre As String, ByVal CtaPEgG As String, ByVal CtaPEgEx As String,
                               ByVal CtaPEgC As String, ByVal IVAPAcre As String, ByVal Egresos As String,
                                ByVal Tasa As String, ByVal Deudor As String, ByVal Efectivo As String,
                               ByVal Transferencia As String, ByVal Cheque As String, ByVal Tercero As String, ByVal Tipo As String)
        Dim sql As String = "UPDATE dbo.ClavesRecibidas
                            SET  
	                        Clave = '" & Clave & "',
	                        CtaEgG = '" & Cuenta(CtaEgG) & "',
	                        CtaEgEx ='" & Cuenta(CtaEgEx) & "',
	                        CtaEgC = '" & Cuenta(CtaEgC) & "',
	                        IVAAcre = '" & Cuenta(IVAAcre) & "',
	                        CtaPEgG = '" & Cuenta(CtaPEgG) & "',
	                        CtaPEgEx = '" & Cuenta(CtaPEgEx) & "',
	                        CtaPEgC = '" & Cuenta(CtaPEgC) & "',
	                        IVAPAcre = '" & Cuenta(IVAPAcre) & "',
	                        Egresos = '" & Egresos & "',
                              Transferencia = '" & Cuenta(Transferencia) & "',
                              Cheque = '" & Cuenta(Cheque) & "',
                                Tercero = '" & Cuenta(Tercero) & "',
                                Efectivo = '" & Cuenta(Efectivo) & "',
	                        Tasa =  " & Tasa & " ,Deudor ='" & Cuenta(Deudor) & "', Tipo =  " & Tipo & " where Id_ClaveR = " & Id & " and Id_Empresa = " & Me.lstCliente.SelectItem & ""
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("ClavesRecibidasD", sql)
        End If
    End Sub
    Private Sub Inserta_ingresos(ByVal Id As Integer, ByVal Clave As String,
                               ByVal CtaEgG As String, ByVal CtaEgEx As String, ByVal CtaEgC As String,
                               ByVal IVAAcre As String, ByVal CtaPEgG As String, ByVal CtaPEgEx As String,
                               ByVal CtaPEgC As String, ByVal IVAPAcre As String, ByVal Egresos As String,
                                ByVal Tasa As String, ByVal Id_Empresa As String, ByVal Deudor As String,
                                 ByVal Efectivo As String, ByVal Transferencia As String, ByVal Cheque As String, ByVal Tercero As String, ByVal Tipo As String)


        Dim sql As String = "INSERT INTO dbo.ClavesRecibidas
	(                          	Clave,
                            	CtaEgG,
                            	CtaEgEx,
                            	CtaEgC,
                            	IVAAcre,
                            	CtaPEgG,
                            	CtaPEgEx,
                            	CtaPEgC,
                            	IVAPAcre,
                            	Egresos,
                            	Tasa,
                                Id_Empresa,   Deudor,Efectivo,	Transferencia,	Cheque,	Tercero,Tipo 
                            	)
                            VALUES 
                            	(
                            	'" & Clave & "',
                            	'" & Cuenta(CtaEgG) & "',
                            	'" & Cuenta(CtaEgEx) & "',
                            	'" & Cuenta(CtaEgC) & "',
                            	'" & Cuenta(IVAAcre) & "',
                            	'" & Cuenta(CtaPEgG) & "',
                            	'" & Cuenta(CtaPEgEx) & "',
                            	'" & Cuenta(CtaPEgC) & "',
                            	'" & Cuenta(IVAPAcre) & "',
                            	'" & Egresos & "',
                            	" & Tasa & ",
                            	" & Id_Empresa & ",
                                '" & Cuenta(Deudor) & "',
                                '" & Cuenta(Efectivo) & "',
                            	'" & Cuenta(Transferencia) & "',
                            	'" & Cuenta(Cheque) & "',
                            	'" & Cuenta(Tercero) & "'," & Tipo & "
                            	)"


        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("ClavesRecibidasD", sql)
        End If
    End Sub
    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        Dim Tipo As String = ""
        For i As Integer = 0 To Me.TablaSeries.Rows.Count - 1
            If Me.TablaSeries.Item(ID.Index, i).Value <> Nothing Then

                'Id_ClaveR,	Clave,	CtaEgG,	,	,	,	,	,	,	,	,	
                If Me.TablaSeries.Item(TipPol.Index, i).Value.ToString <> "" Then
                    Tipo = Me.TablaSeries.Item(TipPol.Index, i).Value.ToString().Substring(0, 2)
                Else
                    Tipo = 12
                End If

                Edita_ingresos(Me.TablaSeries.Item(ID.Index, i).Value, Me.TablaSeries.Item(Clave.Index, i).Value,
                                Me.TablaSeries.Item(CtaEgG.Index, i).Value, Me.TablaSeries.Item(CtaEgEx.Index, i).Value, Me.TablaSeries.Item(CtaEgC.Index, i).Value,
                                      Me.TablaSeries.Item(IVAAcre.Index, i).Value, Me.TablaSeries.Item(CtaPEgG.Index, i).Value, Me.TablaSeries.Item(CtaPEgEx.Index, i).Value,
                               Me.TablaSeries.Item(CtaPEgC.Index, i).Value, Me.TablaSeries.Item(IVAPAcre.Index, i).Value, Me.TablaSeries.Item(Egresos.Index, i).Value, Me.TablaSeries.Item(Tasa.Index, i).Value, Me.TablaSeries.Item(Deudor.Index, i).Value, Me.TablaSeries.Item(Efectivo.Index, i).Value, Me.TablaSeries.Item(Transferencia.Index, i).Value, Me.TablaSeries.Item(Cheque.Index, i).Value, Me.TablaSeries.Item(Terceros.Index, i).Value, Tipo)
            Else
                Inserta_ingresos(Me.TablaSeries.Item(ID.Index, i).Value, Me.TablaSeries.Item(Clave.Index, i).Value,
                                Me.TablaSeries.Item(CtaEgG.Index, i).Value, Me.TablaSeries.Item(CtaEgEx.Index, i).Value, Me.TablaSeries.Item(CtaEgC.Index, i).Value,
                                      Me.TablaSeries.Item(IVAAcre.Index, i).Value, Me.TablaSeries.Item(CtaPEgG.Index, i).Value, Me.TablaSeries.Item(CtaPEgEx.Index, i).Value,
                               Me.TablaSeries.Item(CtaPEgC.Index, i).Value, Me.TablaSeries.Item(IVAPAcre.Index, i).Value, Me.TablaSeries.Item(Egresos.Index, i).Value,
                               Me.TablaSeries.Item(Tasa.Index, i).Value, Me.lstCliente.SelectItem, Me.TablaSeries.Item(Deudor.Index, i).Value, Me.TablaSeries.Item(Efectivo.Index, i).Value, Me.TablaSeries.Item(Transferencia.Index, i).Value, Me.TablaSeries.Item(Cheque.Index, i).Value, Me.TablaSeries.Item(Terceros.Index, i).Value, Tipo)
            End If
        Next
        RadMessageBox.Show("Proceso Terminado.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
        Me.CmdBuscarFact.PerformClick()
    End Sub
#End Region
    Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If RadMessageBox.Show("Realmente deseas eliminar el registro de : " & Me.lstCliente.SelectText & "?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

            For i As Integer = 0 To Me.TablaSeries.Rows.Count - 1
                If Me.TablaSeries.Item(SelFactura.Index, i).Value <> Nothing Then
                    If Eventos.Comando_sql("Delete from dbo.ClavesRecibidas where Id_ClaveR=" & Me.TablaSeries.Item(ID.Index, i).Value) > 0 Then
                        RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                        Eventos.Insertar_usuariol("Id_ClaveR", "Delete from dbo.ClavesRecibidas where Id_ClaveR=" & Me.TablaSeries.Item(ID.Index, i).Value & "")
                    Else
                        RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                    End If
                End If
            Next
        End If
        Me.CmdBuscarFact.PerformClick()
    End Sub

    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        If Me.lstCliente.SelectText <> "" Then
            Buscar_Ingresos(1)
        End If
    End Sub
    Private Function Obtener_Index(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer
        Dim ds As DataSet = Eventos.Obtener_DS(" select rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE  cuenta= " & Trim(valor) & "  and Id_Empresa = " & 1 & " ")
        valor = valor & "-" & ds.Tables(0).Rows(0)(0)
        For i As Integer = 0 To Col.Items.Count - 1
            If valor = Trim(Col.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next

        Return Indice
    End Function
    Private Function Obtener_IndexTipoPol(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer
        Dim ds As DataSet = Eventos.Obtener_DS("  Select NOMBRE as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " AND Id_Tipo_Pol_Sat= " & valor & " ")
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
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        Dim posicion As Integer = 0
        Dim Sql As String = " SELECT 	Id_ClaveR,	Clave,	CtaEgG,	CtaEgEx,	CtaEgC,	IVAAcre,	CtaPEgG,	CtaPEgEx,	CtaPEgC,	IVAPAcre,	Egresos,	Tasa,Deudor,Efectivo,	Transferencia,	Cheque,	Tercero, Tipo FROM dbo.ClavesRecibidas WHERE Id_Empresa = " & Cliente & " "

        Dim ds As DataSet = Eventos.Obtener_DS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.TablaSeries.RowCount = ds.Tables(0).Rows.Count

            Cargar_Cuentas(Cliente)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Fila As DataGridViewRow = Me.TablaSeries.Rows(i)
#Region "Parametros Egresos"
                Me.TablaSeries.Item(ID.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Id_ClaveR"))
                Me.TablaSeries.Item(Clave.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Clave"))
                Me.TablaSeries.Item(Egresos.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Egresos"))
                Me.TablaSeries.Item(Tasa.Index, i).Value = Trim(ds.Tables(0).Rows(i)("Tasa"))
                Try
                    If Trim(ds.Tables(0).Rows(i)("Tipo")) <> "" Then
                        Fila.Cells(TipPol.Index).Value = Me.TipPol.Items(Obtener_IndexTipoPol(Trim(ds.Tables(0).Rows(i)("Tipo")), Me.TipPol))
                    End If
                Catch ex As Exception

                End Try
#End Region
#Region "Cuentas PUE"
                Try
                    If Trim(ds.Tables(0).Rows(i)("CtaEgG")) <> "" Then
                        Fila.Cells(CtaEgG.Index).Value = Me.CtaEgG.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("CtaEgG")), Me.CtaEgG))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("CtaEgEx")) <> "" Then
                        Fila.Cells(CtaEgEx.Index).Value = Me.CtaEgEx.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("CtaEgEx")), Me.CtaEgEx))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("CtaEgC")) <> "" Then
                        Fila.Cells(CtaEgC.Index).Value = Me.CtaEgC.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("CtaEgC")), Me.CtaEgC))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IVAAcre")) <> "" Then
                        Fila.Cells(IVAAcre.Index).Value = Me.IVAAcre.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("IVAAcre")), Me.IVAAcre))
                    End If
                Catch ex As Exception

                End Try
#End Region
#Region "Cuentas PPD"
                Try
                    If Trim(ds.Tables(0).Rows(i)("CtaPEgG")) <> "" Then
                        Fila.Cells(CtaPEgG.Index).Value = Me.CtaPEgG.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("CtaPEgG")), Me.CtaPEgG))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("CtaPEgEx")) <> "" Then
                        Fila.Cells(CtaPEgEx.Index).Value = Me.CtaPEgEx.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("CtaPEgEx")), Me.CtaPEgEx))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("CtaPEgC")) <> "" Then
                        Fila.Cells(CtaPEgC.Index).Value = Me.CtaPEgC.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("CtaPEgC")), Me.CtaPEgC))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("IVAPAcre")) <> "" Then
                        Fila.Cells(IVAPAcre.Index).Value = Me.IVAPAcre.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("IVAPAcre")), Me.IVAPAcre))
                    End If
                Catch ex As Exception

                End Try
#End Region
                Try
                    If Trim(ds.Tables(0).Rows(i)("Deudor")) <> "" Then
                        Fila.Cells(Deudor.Index).Value = Me.Deudor.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("Deudor")), Me.Deudor))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("Efectivo")) <> "" Then
                        Fila.Cells(Efectivo.Index).Value = Me.Efectivo.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("Efectivo")), Me.Efectivo))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("Transferencia")) <> "" Then
                        Fila.Cells(Transferencia.Index).Value = Me.Transferencia.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("Transferencia")), Me.Transferencia))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("Cheque")) <> "" Then
                        Fila.Cells(Cheque.Index).Value = Me.Cheque.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("Cheque")), Me.Cheque))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(i)("Terceros")) <> "" Then
                        Fila.Cells(Terceros.Index).Value = Me.Terceros.Items(Obtener_Index(Trim(ds.Tables(0).Rows(i)("Terceros")), Me.Terceros))
                    End If
                Catch ex As Exception

                End Try
            Next
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow

        End If
    End Sub
    Private Sub Cargar_Cuentas(ByVal Cliente As Integer)
        Dim sql As String = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 6010 AND Nivel2 > 0 AND Nivel3 > 0 AND Nivel4 = '0000' and Id_Empresa = " & 1 & " order by Alias "
        Dim ds As DataSet = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then
#Region "Tipos de Polizas"
            Dim Tipo As DataSet = Eventos.Obtener_DS(" Select convert(NVARCHAR,Id_Tipo_Pol_Sat,103)  +'-' + Nombre as Clave  from Tipos_Poliza_Sat where Id_Empresa= " & Me.lstCliente.SelectItem & " ")
            If Tipo.Tables(0).Rows.Count > 0 Then
                If Me.TipPol.Items.Count = 0 Then
                    For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                        Me.TipPol.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                    Next
                Else
                    Me.TipPol.Items.Clear()
                    For i As Integer = 0 To Tipo.Tables(0).Rows.Count - 1
                        Me.TipPol.Items.Add(Trim(Tipo.Tables(0).Rows(i)("Clave")))
                    Next
                End If
            End If
#End Region


#Region "Egresos cuentas PUE"
            If Me.CtaEgG.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.CtaEgG.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next

            End If

            If Me.CtaEgEx.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.CtaEgEx.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next

            End If

            If Me.CtaEgC.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.CtaEgC.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next

            End If



        End If

        'Provisionados
        sql = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 1180 AND Nivel2 = 0001  AND (Nivel3 = 0002 or Nivel3 =0012) and Id_Empresa = " & 1 & " order by Alias "
        ds.Clear()
        ds = Eventos.Obtener_DS(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            If Me.IVAAcre.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.IVAAcre.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next

            End If





        End If
#End Region
#Region "Egresos cuentas PPD"
        Dim impuestos As String = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE    Nivel1 = 6015 AND Nivel2 > 0 AND Nivel3 > 0 AND Nivel4 = '0000'  and Id_Empresa = " & 1 & " order by Alias "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.CtaPEgG.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.CtaPEgG.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next

            End If

            If Me.CtaPEgEx.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.CtaPEgEx.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next

            End If
            If Me.CtaPEgC.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.CtaPEgC.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next

            End If


        End If

        impuestos = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 1190 AND Nivel2 = 0001  AND (Nivel3 = 0002 or  Nivel3 = 0012 ) and Id_Empresa = " & 1 & " order by Alias "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.IVAPAcre.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.IVAPAcre.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next

            End If
        End If

#Region "Pagos"
        impuestos = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 1010 AND Nivel2 >= 0001  AND Nivel4 = 0000 and Id_Empresa = " & 1 & " order by Alias "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.Efectivo.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.Efectivo.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next

            End If
        End If
        impuestos = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 1020  AND Nivel2 = 0001  AND Nivel3 > 0000 and Id_Empresa = " & 1 & " order by Alias "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.Transferencia.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.Transferencia.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next

            End If
            If Me.Cheque.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.Cheque.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next
            End If
        End If

        impuestos = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 2050	 AND Nivel2 = 0002  AND Nivel3 = 0003 AND Nivel4 = 0000 and Id_Empresa = " & 1 & " order by Alias "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.Terceros.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.Terceros.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next

            End If
        End If
#End Region





#End Region
        impuestos = " SELECT  rtrim(Catalogo_de_Cuentas.Cuenta)  + '-'+ rtrim(Catalogo_de_Cuentas.Descripcion) AS Alias FROM Catalogo_de_Cuentas WHERE   Nivel1 = 1070 AND Nivel2 = 0001  AND Nivel3 = 0000 and Id_Empresa = " & 1 & " order by Alias "
        ds.Clear()
        ds = Eventos.Obtener_DS(impuestos)
        If ds.Tables(0).Rows.Count > 0 Then
            If Me.Deudor.Items.Count = 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Me.Deudor.Items.Add(ds.Tables(0).Rows(i)("Alias"))
                Next

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

    Private Sub TablaSeries_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles TablaSeries.DataError

    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        If Me.TablaSeries.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In TablaSeries.Rows
                For j As Integer = 0 To Me.TablaSeries.Columns.Count - 1
                    If Me.TablaSeries.Item(j, Fila.Index).Selected = True Then
                        Me.TablaSeries.Item(j, Fila.Index).Value = ""
                    End If
                Next
            Next
        End If
    End Sub
End Class
