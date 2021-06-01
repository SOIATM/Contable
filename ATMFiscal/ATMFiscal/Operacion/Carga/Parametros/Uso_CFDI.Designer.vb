<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Uso_CFDI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Uso_CFDI))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TablaUsoCFDI = New ATMFiscal.tabla_detalle()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TablaUsoCFDI
        '
        Me.TablaUsoCFDI.Cmdcerrar_enabled = True
        Me.TablaUsoCFDI.CmdEditar_Enabled = True
        Me.TablaUsoCFDI.CmdEliminar_enabled = True
        Me.TablaUsoCFDI.CmdNuevo_Enabled = True
        Me.TablaUsoCFDI.CmdRefrescar_enabled = True
        Me.TablaUsoCFDI.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaUsoCFDI.Location = New System.Drawing.Point(0, 0)
        Me.TablaUsoCFDI.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaUsoCFDI.Name = "TablaUsoCFDI"
        Me.TablaUsoCFDI.Size = New System.Drawing.Size(587, 529)
        Me.TablaUsoCFDI.SqlSelect = ""
        Me.TablaUsoCFDI.TabIndex = 0
        Me.TablaUsoCFDI.Tag = "P_Master"
        '
        'Uso_CFDI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(587, 529)
        Me.ControlBox = False
        Me.Controls.Add(Me.TablaUsoCFDI)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Uso_CFDI"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Uso Comprobante Fiscal Digital"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TablaUsoCFDI As tabla_detalle
End Class

