namespace APP.Views;

public partial class NumberPad : ContentView
{
	public NumberPad()
	{
		InitializeComponent();
	}

    decimal amount = 0;
    string textamount = "";

    public string symbol = "";
    public string altSymbol = "";

    private void Button_Pressed(object sender, EventArgs e)
    {
        if(amount > 0)
        {
            UpdateNumber("0");
        }
    }

    private void Button_Pressed_1(object sender, EventArgs e)
    {
        UpdateNumber("1");
    }

    private void Button_Pressed_2(object sender, EventArgs e)
    {
        UpdateNumber("2");
    }

    private void Button_Pressed_3(object sender, EventArgs e)
    {
        UpdateNumber("3");
    }

    private void Button_Pressed_4(object sender, EventArgs e)
    {
        UpdateNumber("4");
    }

    private void Button_Pressed_5(object sender, EventArgs e)
    {
        UpdateNumber("5");
    }

    private void Button_Pressed_6(object sender, EventArgs e)
    {
        UpdateNumber("6");
    }

    private void Button_Pressed_7(object sender, EventArgs e)
    {
        UpdateNumber("7");
    }

    private void Button_Pressed_8(object sender, EventArgs e)
    {
        UpdateNumber("8");
    }

    private void Button_Pressed_9(object sender, EventArgs e)
    {
        UpdateNumber("9");
    }

    private void Button_Pressed_10(object sender, EventArgs e)
    {
        UpdateNumber(".");
    }

    private void Button_Pressed_11(object sender, EventArgs e)
    {
        UpdateNumber("backspace");
    }

    private void UpdateNumber(string v)
    {
        if (v.Length == 1)
        {
            textamount += v;
        }
        else
        {
            if (textamount.Length > 0)
            {
                textamount = textamount.Substring(0, textamount.Length - 1);
            }
        }

        ConvertText();

        bool doesEnd = GetEnding();

        var Trade = (TradeAmountPage)Parent.Parent.Parent;
        Trade.UpdateAmount((double)amount, symbol, false, doesEnd);

        textamount = Trade.detail.Amount.ToString();

        if(doesEnd) { textamount += "."; }
    }

    private void ConvertText()
    {
        bool ends = false;

        if (textamount.Length > 0)
        {
            ends = GetEnding();
        }
        else
        {
            amount = 0;
        }

        try
        {
            amount = decimal.Parse(textamount);
        }
        catch { }

        textamount = amount.ToString();

        if (ends) 
            textamount += ".";
    }

    private bool GetEnding()
    {
        bool ends = false;

        if (textamount.Length > 0)
        {
            ends = textamount[textamount.Length - 1] == '.';
        }

        return ends;
    }
}