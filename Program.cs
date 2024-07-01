/**
 * Zasobnik pro uchovani retezcu
 */
interface IStringStack
{
    /**
     * Prida retezec do zasobniku
     * @param s retezec, ktery ma byt pridan do zasobniku
     */
    void Add(String s);

    /**
     * Vrati retezec z vrcholu zasobniku
     */
    String Get();

    /**
     * Odstrani prvek z vrcholu zasobniku
     */
    void RemoveLast();

    bool IsEmpty();
}

/**
 * Asistent omezujici prokrastinaci
 */
class ProcrastinationAssistant
{
    public static void Main(String[] args)
    {
        IStringStack stack = new StackArray();
        IStringStack stack2 = new StackArraySecond();
        /*    
        stack.Add("Naucit se hrat na ukulele");
        //randomString -> RandomString()
        stack.Add(RandomString());
        */
        /*
        Console.WriteLine(TestStack(stack));
        Console.WriteLine(TestStack(stack2));
        */

        Console.WriteLine("Co je třeba udělat?");
        string mainTask;
        while ((mainTask = Console.ReadLine()).Trim() == "")
        {
            Console.WriteLine("Je nutné zadat úkol.");
        }
        stack.Add(mainTask);
        while (!stack.IsEmpty())
        {
            Console.WriteLine($"Aktuální úkol: {stack.Get()}.");
            Console.WriteLine("Co s úkolem? (H = Hotovo, R = Rozdělit)");

            string decision;
            while (((decision = Console.ReadLine()).Trim() == ""))
            {
                Console.WriteLine("Je nutné zvolit možnost.");
            }

            if (decision.Equals("H"))
            {
                stack.RemoveLast();
            }
            if(decision.Equals("R"))
            {
                string input;
                IStringStack temp = new StackArray();
                Console.WriteLine("Prosím zadej podúkoly, ukončené prázdným řetězcem.");
                while ((input = Console.ReadLine()).Trim() != "")
                {
                    temp.Add(input);
                }
                if (!temp.IsEmpty())
                {
                    stack.RemoveLast();
                    do
                    {
                        stack.Add(temp.Get());
                        temp.RemoveLast();
                    } while (!temp.IsEmpty());
                }
            }
        }
        Console.WriteLine($"Hlavní úkol {mainTask} byl splněn.");
    }

    /**
     * Vygeneruje a vrati nahodny retezec 
     */
    static string RandomString()
    {
        Random r = new Random();
        String result = "";
        for (int i = 0; i < (5 + r.Next(20)); i++)
        {
            result += (char)(r.Next(24) + 65);
        }
        return result;
    }

    /// <summary>
    /// Metoda, která otestuje prázdný zásobník <paramref name="stack"/> vložením a odebráním 100 000 náhodně generovaných prvků a porovná je s prvky pole.
    /// </summary>
    /// <param name="stack">Prázdný zásobník</param>
    /// <returns>Vrací <b>true</b>, pokud jsou prvky v zásobníku a poli stejné</returns>
    static bool TestStack(IStringStack stack)
    {
        string[] randomArray = new string[100_000];
        
        for (int i = 0; i < randomArray.Length; i++)
        {
            string r = RandomString();
            randomArray[i] = r;
        }

        DateTime start = DateTime.Now;
        for (int i = 0; i < randomArray.Length; i++)
        {
            
            stack.Add(randomArray[i]);
        }

        for (int i = 0; i < randomArray.Length; i++)
        {
            if (!randomArray[randomArray.Length - 1 - i].Equals(stack.Get()))
            {
                return false;
            }
            stack.RemoveLast();
        }

        double seconds = (DateTime.Now - start).TotalSeconds;
        Console.WriteLine(seconds + " sekund");
        return true;
    }
}

class StackArraySecond : StackArray
{
    /// <summary>
    /// Metoda zvětší zásobník o 10.
    /// </summary>
    public override void ExpandArray()
    {
        string[] biggerArray = new string[freeIndex + 10];

        for (int i = 0; i < freeIndex; i++)
        {
            biggerArray[i] = data[i];
        }

        data = biggerArray;
    }
}

/**
    * Implementace zasobniku retezcu pomoci pole
*/
class StackArray : IStringStack
{
    /** Data v zasobniku */
    protected String[] data;
    /** Index pozice, na kterou se vlozi novy prvek */
    protected int freeIndex;

    /**
        * Vytvori novy prazdny zasobnik
        */
    public StackArray()
    {
        data = new string[5];
        freeIndex = 0;
    }

    /// <summary>
    /// Metoda přidá do zásobníku prvek <paramref name="s"/>. Pokud se do zásobníku nevejde, zvětší se pomocí <seealso cref="ExpandArray"/>
    /// </summary>
    /// <param name="s">Přidávaný prvek</param>
    public void Add(String s)
    {
        if (freeIndex == data.Length)
        {
            ExpandArray();
        }

        data[freeIndex] = s;
        freeIndex++;
    }

    /// <summary>
    /// Metoda ze zásobníku vrátí poslední přidaný prvek.
    /// </summary>
    /// <returns>Vrací poslední přidaný prvek</returns>
    public String Get()
    {
        if(freeIndex - 1 >= 0) 
        {
            return data[freeIndex - 1];
        }
        return data[0];
    }

    /// <summary>
    /// Metoda ze zásobníku odstraní poslední přidaný prvek.
    /// </summary>
    public void RemoveLast()
    {
        if (!IsEmpty()) 
        {
            freeIndex--;
            data[freeIndex] = null;
        }
    }

    /// <summary>
    /// Metoda zkontroluje, zda je zásobník prázdný.
    /// </summary>
    /// <returns>Vrací <b>true</b>, pokud je prázdný</returns>
    public bool IsEmpty()
    {
        return freeIndex == 0 ? true : false;
    }

    /// <summary>
    /// Metoda zvětší zásobník na dvojnásobek své velikosti.
    /// </summary>
    public virtual void ExpandArray()
    {
        string[] biggerArray = new string[data.Length * 2];
            
        for (int i = 0; i < data.Length; i++)
        {
            biggerArray[i] = data[i];
        }

        data = biggerArray;
    }
}
