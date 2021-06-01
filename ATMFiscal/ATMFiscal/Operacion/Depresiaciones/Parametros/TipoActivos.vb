Imports Telerik.WinControls
Public Class TipoActivos
    Private Sub CmdSalirF_Click(sender As Object, e As EventArgs) Handles CmdSalirF.Click
        Me.Close()
    End Sub

    Private Sub CmdBuscarFact_Click(sender As Object, e As EventArgs) Handles CmdBuscarFact.Click
        If Me.TablaImportar.Rows.Count > 0 Then
            Me.TablaImportar.Rows.Clear()
        End If

        Dim Cuentas As DataSet = Eventos.Obtener_DS(" SELECT  convert(VARCHAR,Clave_Activos.Id_Clave,103)  + '-'+  Clave_Activos.Descripcion   AS Alias FROM Clave_Activos   order by Alias ")
        If Cuentas.Tables(0).Rows.Count > 0 Then
            If Me.Clave.Items.Count = 0 Then
                For i As Integer = 0 To Cuentas.Tables(0).Rows.Count - 1
                    Me.Clave.Items.Add(Trim(Cuentas.Tables(0).Rows(i)("Alias")))
                Next
            End If
        End If
        Dim consulta As String = "SELECT Tipo_Activos.Id_Tipo,Tipo_Activos.Descripcion, Tipo_Activos.Tasa,	Clave_Activos.Id_Clave,Clave_Activos.Descripcion as Nombre,Tipo_Activos.CtaMadre FROM dbo.Tipo_Activos INNER JOIN Clave_Activos ON Clave_Activos.Id_Clave = Tipo_Activos.Id_Clave "
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
                Me.TablaImportar.Item(ID.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Id_Tipo")) = True, "", ds.Tables(0).Rows(j)("Id_Tipo"))
                Me.TablaImportar.Item(Alia.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Descripcion")) = True, "", ds.Tables(0).Rows(j)("Descripcion"))
                Me.TablaImportar.Item(Tasa.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("Tasa")) = True, "", ds.Tables(0).Rows(j)("Tasa"))
                Try
                    If Trim(ds.Tables(0).Rows(j)("Id_Clave")) <> "" Then
                        Fila.Cells(Clave.Index).Value = Me.Clave.Items(Obtener_Index(Trim(ds.Tables(0).Rows(j)("Id_Clave") & "-" & ds.Tables(0).Rows(j)("Nombre")), Me.Clave))
                    End If
                Catch ex As Exception

                End Try
                Me.TablaImportar.Item(CtaActivo.Index, j).Value = IIf(IsDBNull(ds.Tables(0).Rows(j)("CtaMadre")) = True, "", ds.Tables(0).Rows(j)("CtaMadre"))
                frm.Barra.Value = j
            Next
            frm.Close()
            RadMessageBox.Show("Proceso Terminado...", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
            Me.Cursor = Cursors.Arrow

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

    Private Sub CmdNuevoF_Click(sender As Object, e As EventArgs) Handles CmdNuevoF.Click
        Me.TablaImportar.Rows.Add()
    End Sub

    Private Sub CmdGuardarF_Click(sender As Object, e As EventArgs) Handles CmdGuardarF.Click
        For i As Integer = 0 To Me.TablaImportar.Rows.Count - 1
            Dim cadena As String = Me.TablaImportar.Item(Clave.Index, i).Value
            Dim posil As Integer = InStr(1, cadena, "-", CompareMethod.Binary)
            Dim Cla As String = cadena.Substring(0, posil - 1)
            If Me.TablaImportar.Item(ID.Index, i).Value <> Nothing Then

                Edita_Registro(Me.TablaImportar.Item(ID.Index, i).Value, Me.TablaImportar.Item(Alia.Index, i).Value, Me.TablaImportar.Item(Tasa.Index, i).Value, Cla, Me.TablaImportar.Item(CtaActivo.Index, i).Value)
            Else
                Inserta_Registro(Me.TablaImportar.Item(Alia.Index, i).Value, Me.TablaImportar.Item(Tasa.Index, i).Value, Cla, Me.TablaImportar.Item(CtaActivo.Index, i).Value)
            End If
        Next
        Me.CmdBuscarFact.PerformClick()
    End Sub
    Private Sub Edita_Registro(ByVal Id As Integer, ByVal Ali As String, ByVal Tasa As Decimal, ByVal Id_Clave As Integer, ByVal CtaMadre As String)
        Dim sql As String = "UPDATE dbo.Tipo_Activos
	                            SET  
		                        Descripcion = '" & Ali & "',Tasa = " & Tasa & ",Id_Clave = " & Id_Clave & " ,CtaMadre = '" & CtaMadre & "'

		                        where Id_Tipo = " & Id & "  "
        If Eventos.Comando_sql(sql) > 0 Then

        End If
    End Sub
    Private Sub Inserta_Registro(ByVal Ali As String, ByVal Tasa As Decimal, ByVal Id_Clave As Integer, ByVal CtaMadre As String)
        Dim sql As String = "INSERT INTO dbo.Tipo_Activos
		                        (
		                        Descripcion, Tasa,Id_Clave,CtaMadre
		                        )
	                            VALUES 
	                            	(

	                            	'" & Trim(Ali) & "', 
	                                " & Tasa & " ,
	                                " & Id_Clave & ",'" & CtaMadre & "'
	                            	)"
        If Eventos.Comando_sql(sql) > 0 Then
            Eventos.Insertar_usuariol("Tipo_ActivosI", sql)
        End If
    End Sub
    Private Sub CmdEliminarF_Click(sender As Object, e As EventArgs) Handles CmdEliminarF.Click
        RadMessageBox.SetThemeName("MaterialBlueGrey")
        If Me.TablaImportar.Rows.Count > 0 Then
            For Each Fila As DataGridViewRow In TablaImportar.Rows
                If Fila.Cells(Alia.Index).Selected = True Then
                    If radMessageBox.Show("Realmente deseas eliminar los registros seleccionados?", Eventos.titulo_app, MessageBoxButtons.YesNo, radMessageIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        If Me.TablaImportar.Item(ID.Index, Fila.Index).Value <> Nothing Then
                            If Eventos.Comando_sql("Delete from dbo.Tipo_Activos where Id_Tipo=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value) > 0 Then
                                RadMessageBox.Show("Registro eliminado correctamente.", Eventos.titulo_app, MessageBoxButtons.OK, RadMessageIcon.Info)
                                Eventos.Insertar_usuariol("Clave_ActivosD", "Delete from dbo.Tipo_Activos where Id_Tipo=" & Me.TablaImportar.Item(ID.Index, Fila.Index).Value & "")
                            Else
                                radMessageBox.Show("No se pudo eliminar el registro, posiblemente exista información relacionada a este...", Eventos.titulo_app, MessageBoxButtons.OK, radMessageIcon.Error)
                            End If
                        End If
                    End If
                    Me.CmdBuscarFact.PerformClick()
                End If
            Next
        End If

    End Sub

    Private Sub TipoActivos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Eventos.DiseñoTabla(Me.TablaImportar)
        Me.CmdBuscarFact.PerformClick()
    End Sub

End Class