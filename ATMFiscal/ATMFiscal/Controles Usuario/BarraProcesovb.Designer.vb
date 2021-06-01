<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BarraProcesovb
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.Barra = New System.Windows.Forms.ProgressBar()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Barra
        '
        Me.Barra.ForeColor = System.Drawing.Color.LightGray
        Me.Barra.Location = New System.Drawing.Point(12, 13)
        Me.Barra.Name = "Barra"
        Me.Barra.Size = New System.Drawing.Size(488, 33)
        Me.Barra.TabIndex = 0
        '
        'BarraProcesovb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.ClientSize = New System.Drawing.Size(512, 58)
        Me.ControlBox = False
        Me.Controls.Add(Me.Barra)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "BarraProcesovb"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Procesando por favor espere..."
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents Barra As ProgressBar
End Class

