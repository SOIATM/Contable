Imports Telerik.WinControls
Public Class Cuentas_Impuestos
    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub
    Private Sub Buscar_Listas()
        Dim sql As String = "  SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias  
                            FROM Catalogo_de_Cuentas WHERE Clasificacion NOT IN ('ACI','AFI','ADF','PCP','PLP','CCO','IVS') AND Nivel1 > 0 and Nivel4 = '0000' and Id_Empresa = " & Me.lstCliente.SelectItem & " "
        Dim RP As DataSet = Eventos.Obtener_DS(sql)
        If RP.Tables(0).Rows.Count > 0 Then


            If Me.Aliaa1.Items.Count = 0 Then
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.Aliaa1.Items.Add(RP.Tables(0).Rows(i)("Alias"))
                Next
            Else

            End If
        End If
        RP.Clear()
        RP = Eventos.Obtener_DS("SELECT 'Nivel1' AS Nivel UNION SELECT 'Nivel2' AS Nivel UNION SELECT  'Nivel3' AS Nivel UNION SELECT 'Nivel4' AS Nivel")
        If RP.Tables(0).Rows.Count > 0 Then
            If Me.Nivel1.Items.Count = 0 Then
                For i As Integer = 0 To RP.Tables(0).Rows.Count - 1
                    Me.Nivel1.Items.Add(RP.Tables(0).Rows(i)("Nivel"))
                Next
            Else

            End If
        End If


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
        Eventos.DiseñoTabla(Me.Tabla)
    End Sub
    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        If Me.Tabla.Rows.Count > 0 Then
            Me.Tabla.Rows.Clear()
        End If
        Dim consulta As String = " SELECT Id_Cta_Impuesto, Cuenta, Nivel,  Valor FROM dbo.Cuentas_Impuestos where Id_Empresa= " & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Obtener_DS(consulta)
        If ds.Tables(0).Rows.Count > 0 Then
            Me.Tabla.RowCount = ds.Tables(0).Rows.Count
            Buscar_Listas()
            Dim frm As New BarraProcesovb
            frm.Show()
            frm.Barra.Minimum = 0
            frm.Barra.Maximum = ds.Tables(0).Rows.Count - 1
            Me.Cursor = Cursors.AppStarting
            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim Fila As DataGridViewRow = Me.Tabla.Rows(j)
                Me.Tabla.Item(Id1.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Cta_Impuesto")) = True, "", ds.Tables(0).Rows(j)("Id_Cta_Impuesto"))

                Try
                    If Trim(ds.Tables(0).Rows(j)("Cuenta")) <> "" Then
                        Fila.Cells(Aliaa1.Index).Value = Me.Aliaa1.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Cuenta")), Me.Aliaa1))
                    End If
                Catch ex As Exception

                End Try
                Try
                    If Trim(ds.Tables(0).Rows(j)("Nivel")) <> "" Then
                        Fila.Cells(Nivel1.Index).Value = Me.Nivel1.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Nivel")), Me.Nivel1))
                    End If
                Catch ex As Exception

                End Try


                Try
                    Me.Tabla.Item(Valor1.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Valor")) = True, 0.00, ds.Tables(0).Rows(j)("Valor"))
                Catch ex As Exception

                End Try

                frm.Barra.Value = j
            Next
            frm.Close()
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow
        Else
            RadMessageBox.SetThemeName("MaterialBlueGrey")
            RadMessageBox.Show("No hay Cuentas  para Calculo de Impuestos DIOT ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
        End If

    End Sub
    Private Function Obtener_Index(ByVal valor As String, ByVal Col As DataGridViewComboBoxColumn)
        Dim Indice As Integer = -1
        For i As Integer = 0 To Col.Items.Count - 1
            If valor = Trim(Col.Items(i)) Then
                Indice = i
                Exit For
            End If
        Next
        Return Indice
    End Function
    Private Sub Tabla_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles Tabla.EditingControlShowing
        If Me.Tabla.CurrentCellAddress.X = 1 Or Me.Tabla.CurrentCellAddress.X = 2 Then
            Try
                Dim comboBox As DataGridViewComboBoxEditingControl = e.Control

                comboBox.DropDownStyle = ComboBoxStyle.DropDown
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            Catch ex As Exception

            End Try

        End If
    End Sub
    Private Sub Cuentas_Impuestos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_Listas()
        Me.CmdBuscarFact.PerformClick()
    End Sub

    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        Me.Tabla.Rows.Add()
        Buscar_Listas()
    End Sub

    Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
        If Me.Tabla.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In Tabla.Rows
                If Fila.Cells(Aliaa1.Index).Selected = True Then
                    RadMessageBox.SetThemeName("MaterialBlueGrey")
                    If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If Me.Tabla.Item(Id1.Index, Fila.Index).Value <> Nothing Then
                            If Eventos.Comando_sql("Delete from dbo.Cuentas_Impuestos where Id_Cta_Impuesto=" & Me.Tabla.Item(Id1.Index, Fila.Index).Value) > 0 Then
                                RadMessageBox.SetThemeName("MaterialBlueGrey")
                                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                Eventos.Insertar_usuariol("Cuentas_DiotD", "Delete from dbo.Cuentas_Impuestos where Id_Cta_Impuesto=" & Me.Tabla.Item(Id1.Index, Fila.Index).Value & "")
                            Else
                                RadMessageBox.SetThemeName("MaterialBlueGrey")
                                RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                            End If
                        End If

                    End If
                    Me.CmdBuscarFact.PerformClick()
                End If
            Next
        End If
    End Sub
    Private Function Buscar_cuenta(ByVal Cta As String)
        Dim Nombre As String = ""
        Nombre = "SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias FROM Catalogo_de_Cuentas WHERE   Cuenta =" & Cta & " and Id_Empresa = " & Me.lstCliente.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(Nombre)
        If ds.Tables(0).Rows.Count > 0 Then
            Nombre = ds.Tables(0).Rows(0)(0)
        Else
            Nombre = ""
        End If
        Return Nombre
    End Function
    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        For i As Integer = 0 To Me.Tabla.Rows.Count - 1

            Dim Ctas As String() = Me.Tabla.Item(Aliaa1.Index, i).Value.Split(New Char() {"/"c})
            Dim c As String = Trim(Ctas(0).ToString).Replace("-", "")
            If Me.Tabla.Item(Id1.Index, i).Value <> Nothing Then

                Edita_Registro(Me.Tabla.Item(Id1.Index, i).Value, Me.Tabla.Item(Aliaa1.Index, i).Value, Me.Tabla.Item(Nivel1.Index, i).Value, Me.Tabla.Item(Valor1.Index, i).Value)
            Else
                Inserta_Registro(Me.Tabla.Item(Aliaa1.Index, i).Value, Me.Tabla.Item(Nivel1.Index, i).Value, Me.Tabla.Item(Valor1.Index, i).Value)
            End If
        Next
        Me.CmdBuscarFact.PerformClick()
    End Sub
    Private Sub Edita_Registro(ByVal Id_Cta_Impuesto As Integer, ByVal Cuenta As String, ByVal Nivel As String, ByVal Valor As String)
        Dim sql As String = "UPDATE dbo.Cuentas_Impuestos SET   	Cuenta = '" & Cuenta & "',	Nivel = '" & Nivel & "',	 	
                                Valor = '" & Valor & "'   where Id_Cta_Impuesto = " & Id_Cta_Impuesto & "  "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Cuentas_ImpuestosE", sql)
        End If
    End Sub
    Private Sub Inserta_Registro(ByVal Cuenta As String, ByVal Nivel As String, ByVal Valor As String)
        Dim sql As String = "INSERT INTO dbo.Cuentas_Impuestos 	(	Cuenta,	Nivel,Valor,	Id_Empresa	) VALUES 	(	'" & Cuenta & "',	'" & Nivel & "',	 '" & Valor & "',	 " & Me.lstCliente.SelectItem & "	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Cuentas_ImpuestosI", sql)
        End If
    End Sub
End Class
