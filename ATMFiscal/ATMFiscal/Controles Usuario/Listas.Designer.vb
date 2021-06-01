<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Listas
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Combo = New Telerik.WinControls.UI.RadDropDownList()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        CType(Me.Combo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Combo
        '
        Me.Combo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Combo.AutoSize = False
        Me.Combo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Combo.DropDownHeight = 119
        Me.Combo.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Combo.Location = New System.Drawing.Point(0, 0)
        Me.Combo.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Combo.Name = "Combo"
        Me.Combo.ShowImageInEditorArea = False
        Me.Combo.Size = New System.Drawing.Size(273, 36)
        Me.Combo.TabIndex = 24
        Me.Combo.ThemeName = "Material"
        '
        'Listas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Combo)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Listas"
        Me.Size = New System.Drawing.Size(273, 36)
        CType(Me.Combo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Combo As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
End Class
