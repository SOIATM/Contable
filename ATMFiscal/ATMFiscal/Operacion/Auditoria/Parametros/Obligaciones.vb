Imports Telerik.WinControls
Public Class Obligaciones
    Dim Activo As Boolean
    Private Sub Obligaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Activo = True
        Cargar()
        Eventos.DiseñoTabla(Me.TablaImportar)
        Activo = False
    End Sub
    Private Sub Cargar()
        Me.LstEmpresa.Cargar(" SELECT id_Empresa, Razon_social FROM Empresa ")
        Me.LstEmpresa.SelectItem = 1
    End Sub
    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Rows.Clear()
        End If

        If Me.LstEmpresa.SelectText <> "" Then

            Dim consulta As String = "SELECT Id_Obligacion,  Descripcion,  Cargo  ,Abono ,Saldo_Final"

            consulta &= " FROM     Obligaciones   where   Obligaciones.Id_Empresa = " & Me.LstEmpresa.SelectItem & " "
            Dim ds As DataSet = Obtener_DS(consulta)
            If ds.Tables(0).Rows.Count > 0 Then
                Me.TablaImportar.RowCount = ds.Tables(0).Rows.Count

                Dim frm As New BarraProcesovb
                frm.Show()
                frm.Barra.Minimum = 0
                frm.Barra.Maximum = ds.Tables(0).Rows.Count - 1
                Me.Cursor = Cursors.AppStarting
                For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

                    Dim Fila As DataGridViewRow = Me.TablaImportar.Rows(j)
                    Me.TablaImportar.Item(ID.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Obligacion")) = True, "", ds.Tables(0).Rows(j)("Id_Obligacion"))
                    Me.TablaImportar.Item(Descripcion.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Descripcion")) = True, "", ds.Tables(0).Rows(j)("Descripcion"))
                    Me.TablaImportar.Item(Cargo.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Cargo")) = True, False, ds.Tables(0).Rows(j)("Cargo"))
                    Me.TablaImportar.Item(Abono.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Abono")) = True, False, ds.Tables(0).Rows(j)("Abono"))
                    Me.TablaImportar.Item(SF.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Saldo_Final")) = True, False, ds.Tables(0).Rows(j)("Saldo_Final"))

                    frm.Barra.value = j
                Next
                frm.Close()
                RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                Me.Cursor = Cursors.Arrow
            Else

                RadMessageBox.Show("No hay Obligaciones  para " & Me.LstEmpresa.SelectText & " ...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Exclamation)
            End If
        Else
            RadMessageBox.Show("Selecciona una Empresa...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
        End If
    End Sub
    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click

        Me.TablaImportar.Rows.Add()

    End Sub
    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1


            If Me.TablaImportar.Item(ID.Index, i).Value <> Nothing Then

                Edita_Registro(Me.TablaImportar.Item(ID.Index, i).Value, Me.TablaImportar.Item(Descripcion.Index, i).Value, Me.TablaImportar.Item(Cargo.Index, i).Value, Me.TablaImportar.Item(Abono.Index, i).Value, Me.TablaImportar.Item(SF.Index, i).Value)
            Else
                Inserta_Registro(Me.TablaImportar.Item(Cargo.Index, i).Value, Me.TablaImportar.Item(Abono.Index, i).Value, Me.TablaImportar.Item(SF.Index, i).Value, Me.TablaImportar.Item(Descripcion.Index, i).Value)
            End If
        Next
        Me.CmdBuscarFact.PerformClick()
    End Sub
    Private Sub Edita_Registro(ByVal Id_Obligacion As Integer, ByVal Descripcion As String, ByVal Cargo As Integer, ByVal Abono As Integer, ByVal Sf As Integer)
        Dim sql As String = "UPDATE dbo.Obligaciones
                                SET  
    	                        Descripcion = '" & Descripcion & "',
                                 Cargo =  " & Bool2(Cargo) & " ,
                                  Abono =  " & Bool2(Abono) & " ,
                                 Saldo_Final =  " & Bool2(Sf) & " 
    	                        where Id_Obligacion = " & Id_Obligacion & "  "
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("ObligacionesE", sql)
        End If
    End Sub
    Private Sub Inserta_Registro(ByVal Cargo As Integer, ByVal Abono As Integer, ByVal Saldo_Final As Integer, ByVal Descripcion As String)
        Dim sql As String = "INSERT INTO dbo.Obligaciones
    	                            (
    	                            Descripcion,
    	                            Cargo,
    	                            Abono,
    	                            Saldo_Final,
    	                            Id_Empresa
    	                            )
                                VALUES 
                                	(
                                	'" & Trim(Descripcion) & "',
                                    '" & Eventos.Bool2(Cargo) & "',
                                	'" & Eventos.Bool2(Abono) & "',
                                    '" & Eventos.Bool2(Saldo_Final) & "',
                                    " & Me.LstEmpresa.SelectItem & "
                                	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("ObligacionesI", sql)
        End If
    End Sub
    Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In TablaImportar.Rows
                If Fila.Cells(Descripcion.Index).Selected = True Then
                    If RadMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then

                        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
                            If Me.TablaImportar.Item(ID.Index, i).Value <> Nothing Then
                                If Eventos.Comando_sql("Delete from dbo.Obligaciones where Id_Obligacion=" & Me.TablaImportar.Item(ID.Index, i).Value) > 0 Then
                                    RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                    Eventos.Insertar_usuariol("ObligacionesD", "Delete from dbo.Obligaciones where Id_Obligacion=" & Me.TablaImportar.Item(ID.Index, i).Value & "")
                                Else
                                    RadMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Error)
                                End If
                            End If
                        Next
                    End If
                    Me.CmdBuscarFact.PerformClick()
                End If
            Next
        End If

    End Sub
    Private Function Buscar_cuenta(ByVal Cta As String)
        Dim Nombre As String = ""
        Nombre = "SELECT Nivel1 + '-' + Nivel2 + '-' +Nivel3 + '-' + Nivel4 + ' / ' + Catalogo_de_Cuentas.Descripcion  AS Alias FROM Catalogo_de_Cuentas WHERE   Cuenta =" & Cta & " and id_Empresa = " & Me.LstEmpresa.SelectItem & ""
        Dim ds As DataSet = Eventos.Obtener_DS(Nombre)
        If ds.Tables(0).Rows.Count > 0 Then
            Nombre = ds.Tables(0).Rows(0)(0)
        Else
            Nombre = ""
        End If
        Return Nombre
    End Function
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
    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub


End Class