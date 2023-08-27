## REZ Code Style Guide
Since this is a student project, we're still very flexible about Code Style improvements and suggestions. Here are the basic rules we're following to style our code and make it readable.


### For C# / Code-behind:
- Class members and methods starting with CapitalLetters and CamelCase.
- "Disposable" variables such `i, j` inside `for loops` always in lowercase.
- Indentation (Tab) with 4 spaces.
- Bindings starting with CapitalLetters.
- Allman indentation style convention.
- One-line `if/for/foreach` statements can be left without brackets following the example:
```cs
    if (String.IsNullOrEmpty(Name.Text))
        ErrorMessage_Name.Visibility = Visibility.Visible;
```
- Class Methods must have a dashed-line separator before them, followed by a simple description if possible:
```cs
    //----------------------------------------------------------------------------
    // This method will take the TextChange event inside a TextBox 
    // from View and apply input validation.
    private void InputName_TextChange(object sender, TextChangedEventArgs e)
    {
        // Method
    }
```

### For XAML / View files:
- Bindings inside tags come first and must start with CapitalLetters/CamelCase.
- If there are too many properties for one line, you can indent the following properties aligned with the first one. 
- If there are multiple tags with many properties in multiple lines, you can preferably align the properties.
- Keep in mind that the first property should remain in the same line as the tag.
- If possible, you can separate major groups of elements from each other by adding a newline between them.

Full example:

```XAML
<TextBlock  Text="{Binding Key}" 
            Style="{ThemeResource TitleTextBlockStyle}" 
            AutomationProperties.AccessibilityView="Raw"/>

<TextBox    x:Name="Greetings"
            Text=""
            TextWrapping="Wrap"
            Margin="12, 12, 12, 0"/>

<TextBlock  Text="Revise seu pedido" 
            TextWrapping="Wrap"
            FontSize="20"
            FontWeight="Bold"
            Margin="12, 0, 12, 12"/>
            
<Line       Stroke="#C9C9C9"
            X1="0" Y1="0"
            X2="1000" Y2="0"
            StrokeThickness="1"
            Margin="10"/>

<StackPanel Width="468" VerticalAlignment="Center" HorizontalAlignment="Center">
    <TextBlock Text="Selecione as contas que vão compartilhar esse pedido com você:" TextAlignment="Left" Foreground="Gray" />
    <ComboBox x:Name="AccountComboBox" Width="400">
    <ComboBox.ItemTemplate>
        <DataTemplate>
                <CheckBox Content="{Binding Name}" IsChecked="{Binding IsSelected}" IsEnabled="{Binding IsEnabled}" />
        </DataTemplate>
    </ComboBox.ItemTemplate>
    </ComboBox>
</StackPanel>
```
