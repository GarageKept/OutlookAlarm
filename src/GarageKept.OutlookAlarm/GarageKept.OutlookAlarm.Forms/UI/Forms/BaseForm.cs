﻿namespace GarageKept.OutlookAlarm.Forms.UI.Forms;

/// <summary>
///     The BaseForm is a custom form that provides functionality for moving the form around on the screen by dragging with
///     the mouse.
///     This form will also remain on top of all other windows.
/// </summary>
public class BaseForm : Form
{
    private Point _dragCursorPoint;
    private Point _dragFormPoint;
    private bool _isDragging;
    private int _screenHeight;
    private int _screenWidth;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseForm" /> class with PinTop set to true.
    /// </summary>
    public BaseForm() : this(true)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseForm" /> class.
    /// </summary>
    /// <param name="pinTop">Whether the form should be pinned to the top of the screen.</param>
    public BaseForm(bool pinTop)
    {
        SubscribeToMouseEvents(this);

        // Make the form stay on top of other windows
        TopMost = true;

        PinTop = pinTop;
    }

    /// <summary>
    ///     Gets the width of the primary screen, caches the value for future use.
    /// </summary>
    public int ScreenWidth => _screenWidth > 0
        ? _screenWidth
        : _screenWidth = Screen.PrimaryScreen?.WorkingArea.Width ?? 0;

    /// <summary>
    ///     Gets the height of the primary screen, caches the value for future use.
    /// </summary>
    public int ScreenHeight => _screenHeight > 0
        ? _screenHeight
        : _screenHeight = Screen.PrimaryScreen?.WorkingArea.Height ?? 0;

    /// <summary>
    ///     Gets or sets a value indicating whether the form should be pinned to the top of the screen.
    /// </summary>
    public bool PinTop { get; set; }

    /// <summary>
    ///     Adds mouse event handlers to a given control and all its children.
    /// </summary>
    /// <param name="control">The control to add event handlers to.</param>
    public void SubscribeToMouseEvents(Control control)
    {
        control.MouseDown += MouseDownHandler;
        control.MouseMove += MouseMoveHandler;
        control.MouseUp += MouseUpHandler;

        foreach (Control child in control.Controls) SubscribeToMouseEvents(child);
    }

    /// <summary>
    ///     Event handler for MouseDown event, begins form dragging.
    /// </summary>
    public void MouseDownHandler(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _isDragging = true;
            _dragCursorPoint = Cursor.Position;
            _dragFormPoint = Location;
        }
    }

    /// <summary>
    ///     Event handler for MouseMove event, updates form location while dragging.
    /// </summary>
    public void MouseMoveHandler(object? sender, MouseEventArgs e)
    {
        if (_isDragging)
        {
            var xDelta = Cursor.Position.X - _dragCursorPoint.X;
            var x = _dragFormPoint.X + xDelta;
            x = Math.Max(0, Math.Min(x, ScreenWidth - Width));

            var y = 0;

            if (!PinTop)
            {
                var yDelta = Cursor.Position.Y - _dragCursorPoint.Y;
                y = _dragFormPoint.Y + yDelta;
                y = Math.Max(0, Math.Min(y, ScreenHeight - Height));
            }

            Location = new Point(x, y);
        }
    }

    /// <summary>
    ///     Event handler for MouseUp event, ends form dragging.
    /// </summary>
    public void MouseUpHandler(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) _isDragging = false;
    }
}