<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReporteBalanzaGeneral
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ReporteBalanzaGeneral))
		Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
		Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
		Me.CrvBalanza = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
		CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'CrvBalanza
		'
		Me.CrvBalanza.ActiveViewIndex = -1
		Me.CrvBalanza.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.CrvBalanza.Cursor = System.Windows.Forms.Cursors.Default
		Me.CrvBalanza.Dock = System.Windows.Forms.DockStyle.Fill
		Me.CrvBalanza.Location = New System.Drawing.Point(0, 0)
		Me.CrvBalanza.Name = "CrvBalanza"
		Me.CrvBalanza.Size = New System.Drawing.Size(1350, 416)
		Me.CrvBalanza.TabIndex = 0
		'
		'ReporteBalanzaGeneral
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(1350, 416)
		Me.Controls.Add(Me.CrvBalanza)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "ReporteBalanzaGeneral"
		'
		'
		'
		Me.RootElement.ApplyShapeToControl = True
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Reporte"
		Me.ThemeName = "Material"
		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents CrvBalanza As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class

