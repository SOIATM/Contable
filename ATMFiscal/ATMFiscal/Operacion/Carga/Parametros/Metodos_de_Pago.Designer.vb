<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Metodos_de_Pago
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Metodos_de_Pago))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.Tabla_detalleMetodos_Pago = New ATMFiscal.tabla_detalle()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Tabla_detalleMetodos_Pago
        '
        Me.Tabla_detalleMetodos_Pago.Cmdcerrar_enabled = True
        Me.Tabla_detalleMetodos_Pago.CmdEditar_Enabled = True
        Me.Tabla_detalleMetodos_Pago.CmdEliminar_enabled = True
        Me.Tabla_detalleMetodos_Pago.CmdNuevo_Enabled = True
        Me.Tabla_detalleMetodos_Pago.CmdRefrescar_enabled = True
        Me.Tabla_detalleMetodos_Pago.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla_detalleMetodos_Pago.Location = New System.Drawing.Point(0, 0)
        Me.Tabla_detalleMetodos_Pago.Margin = New System.Windows.Forms.Padding(2)
        Me.Tabla_detalleMetodos_Pago.Name = "Tabla_detalleMetodos_Pago"
        Me.Tabla_detalleMetodos_Pago.Size = New System.Drawing.Size(609, 322)
        Me.Tabla_detalleMetodos_Pago.SqlSelect = ""
        Me.Tabla_detalleMetodos_Pago.TabIndex = 0
        Me.Tabla_detalleMetodos_Pago.Tag = "P_Master"
        '
        'Metodos_de_Pago
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(609, 322)
        Me.ControlBox = False
        Me.Controls.Add(Me.Tabla_detalleMetodos_Pago)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Metodos_de_Pago"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Metodos de Pago"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents Tabla_detalleMetodos_Pago As tabla_detalle
End Class

