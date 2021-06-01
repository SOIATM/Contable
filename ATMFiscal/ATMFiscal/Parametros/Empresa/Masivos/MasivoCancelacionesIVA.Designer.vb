<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MasivoCancelacionesIVA
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MasivoCancelacionesIVA))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.MasivosCancelacionesIVA = New ATMFiscal.Masivos()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MasivosCancelacionesIVA
        '
        Me.MasivosCancelacionesIVA.BackColor = System.Drawing.Color.LightSteelBlue
        Me.MasivosCancelacionesIVA.CmdCerrar_enabled = True
        Me.MasivosCancelacionesIVA.CmdEditar_Enabled = True
        Me.MasivosCancelacionesIVA.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MasivosCancelacionesIVA.Location = New System.Drawing.Point(0, 0)
        Me.MasivosCancelacionesIVA.Name = "MasivosCancelacionesIVA"
        Me.MasivosCancelacionesIVA.Size = New System.Drawing.Size(1075, 493)
        Me.MasivosCancelacionesIVA.SqlSelect = "Select"
        Me.MasivosCancelacionesIVA.TabIndex = 0
        Me.MasivosCancelacionesIVA.Tag = "P_Master"
        '
        'MasivoCancelacionesIVA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1075, 493)
        Me.ControlBox = False
        Me.Controls.Add(Me.MasivosCancelacionesIVA)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MasivoCancelacionesIVA"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Asignacion masiva de cuentas para Cancelaciones de IVA"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents MasivosCancelacionesIVA As Masivos
End Class

