<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tipo_Polizas
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tipo_Polizas))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.Tabla_detallePolizas = New ATMFiscal.tabla_detalle()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Tabla_detallePolizas
        '
        Me.Tabla_detallePolizas.Cmdcerrar_enabled = True
        Me.Tabla_detallePolizas.CmdEditar_Enabled = True
        Me.Tabla_detallePolizas.CmdEliminar_enabled = True
        Me.Tabla_detallePolizas.CmdNuevo_Enabled = True
        Me.Tabla_detallePolizas.CmdRefrescar_enabled = True
        Me.Tabla_detallePolizas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla_detallePolizas.Location = New System.Drawing.Point(0, 0)
        Me.Tabla_detallePolizas.Margin = New System.Windows.Forms.Padding(2)
        Me.Tabla_detallePolizas.Name = "Tabla_detallePolizas"
        Me.Tabla_detallePolizas.Size = New System.Drawing.Size(666, 575)
        Me.Tabla_detallePolizas.SqlSelect = ""
        Me.Tabla_detallePolizas.TabIndex = 0
        Me.Tabla_detallePolizas.Tag = "P_Master"
        '
        'Tipo_Polizas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 575)
        Me.ControlBox = False
        Me.Controls.Add(Me.Tabla_detallePolizas)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Tipo_Polizas"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = " "
        Me.Text = "Tipo de Pólizas"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents Tabla_detallePolizas As tabla_detalle



End Class

