namespace DemoProject;

public class Calculator
{
	public static int MagicNumber = 9; 
	
	public int Add(int a, int b)
	{
		MagicNumber++;
		return a + b;
	}
}