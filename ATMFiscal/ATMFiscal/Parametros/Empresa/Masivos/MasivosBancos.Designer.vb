<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MasivosBancos
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MasivosBancos))
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LblFiltro = New System.Windows.Forms.Label()
        Me.Masivos1 = New ATMFiscal.Masivos()
        Me.TxtFiltro = New Telerik.WinControls.UI.RadMaskedEditBox()
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(605, 5)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 20)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "Filtrar:"
        '
        'LblFiltro
        '
        Me.LblFiltro.AutoSize = True
        Me.LblFiltro.BackColor = System.Drawing.Color.LightSteelBlue
        Me.LblFiltro.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFiltro.Location = New System.Drawing.Point(687, 5)
        Me.LblFiltro.Name = "LblFiltro"
        Me.LblFiltro.Size = New System.Drawing.Size(0, 20)
        Me.LblFiltro.TabIndex = 50
        '
        'Masivos1
        '
        Me.Masivos1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Masivos1.CmdCerrar_enabled = True
        Me.Masivos1.CmdEditar_Enabled = True
        Me.Masivos1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Masivos1.Location = New System.Drawing.Point(0, 0)
        Me.Masivos1.Name = "Masivos1"
        Me.Masivos1.Size = New System.Drawing.Size(1085, 508)
        Me.Masivos1.SqlSelect = "Select"
        Me.Masivos1.TabIndex = 0
        Me.Masivos1.Tag = "P_Parametros"
        '
        'TxtFiltro
        '
        Me.TxtFiltro.AutoSize = False
        Me.TxtFiltro.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFiltro.Location = New System.Drawing.Point(609, 28)
        Me.TxtFiltro.Mask = "00/00/0000"
        Me.TxtFiltro.Name = "TxtFiltro"
        Me.TxtFiltro.Size = New System.Drawing.Size(314, 36)
        Me.TxtFiltro.TabIndex = 675
        Me.TxtFiltro.TabStop = False
        '
        'MasivosBancos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1085, 508)
        Me.Controls.Add(Me.TxtFiltro)
        Me.Controls.Add(Me.LblFiltro)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Masivos1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MasivosBancos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Control de Bancos Masivos"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Masivos1 As Masivos
    Friend WithEvents Label6 As Label
    Friend WithEvents LblFiltro As Label
    Friend WithEvents TxtFiltro As Telerik.WinControls.UI.RadMaskedEditBox
End Class

