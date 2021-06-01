<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Claves_Sat
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Claves_Sat))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TablaUsoSAT = New ATMFiscal.tabla_detalle()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TablaUsoSAT
        '
        Me.TablaUsoSAT.Cmdcerrar_enabled = True
        Me.TablaUsoSAT.CmdEditar_Enabled = True
        Me.TablaUsoSAT.CmdEliminar_enabled = True
        Me.TablaUsoSAT.CmdNuevo_Enabled = True
        Me.TablaUsoSAT.CmdRefrescar_enabled = True
        Me.TablaUsoSAT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaUsoSAT.Location = New System.Drawing.Point(0, 0)
        Me.TablaUsoSAT.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaUsoSAT.Name = "TablaUsoSAT"
        Me.TablaUsoSAT.Size = New System.Drawing.Size(546, 524)
        Me.TablaUsoSAT.SqlSelect = ""
        Me.TablaUsoSAT.TabIndex = 0
        Me.TablaUsoSAT.Tag = "P_Master"
        '
        'Claves_Sat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(546, 524)
        Me.ControlBox = False
        Me.Controls.Add(Me.TablaUsoSAT)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Claves_Sat"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Claves Sat"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TablaUsoSAT As tabla_detalle
End Class

