using System;

class Greet // greeting class 
{
    public void greeting()
    {
        System.Console.WriteLine("----------WELCOME TO Nature View RESTAURANT!----------");
        System.Console.WriteLine("      Please select the options below");
        System.Console.WriteLine();
    }
}
public abstract class MenuP // menu parent class (abstract class and method)
{
   public abstract void print();
}

class MenuItem :  MenuP // menu item class  (inheritance)
{
    public int id;
    public string name;
    public double price;

    public MenuItem (int id, string name, double price) // parameterized constructor
    {
        this.id=id;
        this.name=name;
        this.price=price;
    }

    public MenuItem (string name, double price)  // without id (constructor overloading) [id will be assigned later]
    {
        this.name=name;
        this.price=price;
    }

    public MenuItem (MenuItem old) // copy constructor (constructor overloading) [copying from another object]
    {
        this.id=old.id;
        this.name=old.name;
        this.price=old.price;
    }

    public override void print() // method to print menu item details (polymorphism) [method overriding]
    {
        System.Console.WriteLine(id+ ".     " + name + "      " + "-" +price + "TK");
    }

}  
class OrderItem // order item class (composition) [has a MenuItem object] 
{
    public MenuItem s;
    public int q;

    public OrderItem (MenuItem s, int q)  // parameterized constructor (constructor overloading) [with quantity]
    {
        this.s=new MenuItem(s);
        this.q=q;
    }

    public OrderItem (OrderItem old) // copy constructor (constructor overloading) [copying from another object]
    {
        this.s=new MenuItem(old.s);
        this.q=old.q;
    }

    public static OrderItem operator +(OrderItem a, OrderItem b) // adding quantities of same item (operator overloading) [operator +]
    {
        return new OrderItem (a.s, a.q + b.q);
    }
}
class Cart // cart class (shopping cart) [manages order items]
{
    public int count=0;

    public static double vat = 0.05; // VAT percentage (class variable) [static variable]

    public static double Vatcal(double v) // VAT calculation (static method)
    {
        return v * vat;
    }
    public OrderItem[] cart = new OrderItem[20];

    public void add(MenuItem obj, int n) // add item with specified quantity (method overloading)
    {
        for (int i=0; i<count; i++)
        {
            if (cart[i].s.id == obj.id)
            {
                OrderItem oi = new OrderItem(obj, n);
                cart[i] = cart[i] + oi;
                System.Console.WriteLine(obj.name + "Quantiy Updated");
            }
        }
        cart[count] = new OrderItem(obj, n);
        count++;
        System.Console.WriteLine(obj.name + "added" + "("+"x"+n+")");
        
    }

    public void add(MenuItem obj) // add item with default quantity 1 (method overloading) [calls the other add method]
    {
        add(obj, 1);
    }
    public void showcart()
    {
        if (count == 0)
        {
            System.Console.WriteLine("Cart Is Empty!");
            return;
        }
        else
        {
            System.Console.WriteLine("\n======= (YOUR CART) =======");
            for (int i=0; i<count; i++)
            {
                System.Console.WriteLine((i+1)+"."+ cart[i].s.name +"x"+ cart[i].q+"  = "+cart[i].q*cart[i].s.price+"TK");

            }
            System.Console.WriteLine("===========================");
        }
    }
    public void removeitem (int r) // remove item by its position in cart (1-based index)
    {
        if (r>0 && r<=count)
        {
            System.Console.WriteLine(cart[r-1].s.name +" is removed");
            for ( int i=r-1; i<=count-1; i++)
            {
                    cart[i] = cart[i+1];
            }
            cart[count-1] = null;
            count--;
        }
        else
        {
            System.Console.WriteLine("Invalid Item!");
        }
    }
    public double Total() // calculate total price of items in cart (method) [instance method] 
    {
        double t = 0;
        for (int i=0; i<count; i++)
        {
            t = t + cart[i].q * cart[i].s.price;
        }
        return t;
    }
    public void empty() // empty the cart (method) [instance method]
    {
        for (int i=0; i<count; i++)
        {
            cart[i] = null;
        }
        count = 0;
        System.Console.WriteLine("Cart has been refreshed.\n");
    }

}
class Usersystem  // user system class (manages user accounts and login) [authentication]
{
    public string adminname = "admin";
    public string adminpass = "1075";
    public string[] cname = new string[20];
    public string[] cpass = new string[20];
    public Cart[] uocart = new Cart[20];
    public int cuscount=0;
    public string loguser = null;
    public void signup()
    {
        System.Console.Write("Enter new username : ");
        string user = Console.ReadLine();
        System.Console.Write("Enter new password : ");
        string pass = Console.ReadLine();
            cname[cuscount] = user;
            cpass[cuscount] = pass;
            uocart[cuscount] = new Cart();
            loguser = user;
            cuscount++;
        System.Console.WriteLine("Signup successfull!");
    }
    public void login() // login method (instance method) [authentication]
    {
        System.Console.Write("Enter username : ");
        string u = Console.ReadLine();
        System.Console.Write("Enter password : ");
        string p = Console.ReadLine();
        for (int i=0; i<cuscount; i++)
        {
            if (cname[i]==u && cpass[i]==p)
            {
                loguser = u;
                System.Console.WriteLine("customer login successfull"+"\nlogged in as "+loguser);
                break;
            }
            else {System.Console.WriteLine("wrong username or password!");}
        }
    }
    public void logout()  // logout method (instance method) [authentication]
    {
        if (loguser != null)
        {
            System.Console.WriteLine(loguser+ " has been logged out.");
            loguser = null;
        }
        else { System.Console.WriteLine("No user is logged in!");}
    }
}
class AdminP // admin panel class (admin functionalities) [admin authentication and management]
{
    private Usersystem user;
    private Cart cart;
    public MenuItem[] menu;
    public AdminP (Usersystem u, Cart c, MenuItem[] m)  // parameterized constructor (constructor) [initializes with user system, cart, and menu]
    {
        user=u;
        cart=c;
        menu=m;
    }
    public void panel()  // admin panel method (instance method) [admin functionalities]
    {
    System.Console.Write("Enter admin username : ");
    string ad = Console.ReadLine();
    System.Console.Write("Enter admin password : ");
    string pas = Console.ReadLine();

            if (ad == user.adminname && pas == user.adminpass)
        {
            Console.WriteLine("Admin Login Successful!");
            while (true)
            {
                Console.WriteLine("\n====== ADMIN PANEL ======");
                Console.WriteLine("1. View All Customers");
                Console.WriteLine("2. View Cart (Orders)");
                Console.WriteLine("3. Add Menu Item");
                Console.WriteLine("4. Remove Menu Item");
                Console.WriteLine("0. Go Back");
                Console.Write("Select Option : ");
                int adi = Convert.ToInt32(Console.ReadLine());

                if (adi==0) {break;}
                else if (adi==1)
                {
                    System.Console.WriteLine("_______Customer List_______");
                    if (user.cuscount == 0 )
                    {
                        System.Console.WriteLine("No Customer Yet");
                    }
                    else
                    {
                        for (int i=0; i<user.cuscount; i++)
                        {
                            System.Console.WriteLine((i+1)+"."+user.cname[i]);
                        }
                    }
                }
                else if (adi==2)
                {
                    System.Console.WriteLine("_______Current Orders_______\n");
                    if (user.cuscount == 0) 
                    {
                        System.Console.WriteLine("NO OREDERS YET");
                        continue;
                    }
                    for (int i=0; i<user.cuscount; i++)
                    {
                        System.Console.WriteLine("\nCustomer : "+user.cname[i]);
                        if (user.uocart[i] != null && user.uocart[i].count > 0)
                        {
                            for (int j=0; j<user.uocart[i].count; j++)
                            {
                                OrderItem o = user.uocart[i].cart[j];
                                System.Console.WriteLine(o.s.name + " x" + o.q);
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("No item selected");
                        }
                    }
                }
                else if (adi==3)
                {
                    System.Console.Write("Enter New Item Name : ");
                    string Name = Console.ReadLine();
                    System.Console.Write("Enter New Item Price: ");
                    double Price = Convert.ToInt32(Console.ReadLine());

                    MenuItem[] newmenu = new MenuItem[menu.Length + 1];
                    for (int i=0; i<menu.Length; i++)
                    {
                        newmenu[i] = menu[i];
                    }
                    newmenu[menu.Length] = new MenuItem(menu.Length+1, Name, Price);
                    menu = newmenu;
                    System.Console.WriteLine("item added successfully");
                }
                else if (adi==4)
                {
                    System.Console.WriteLine("_______Current Menu_______");
                    for (int i=0; i<menu.Length; i++)
                    {
                        System.Console.WriteLine(menu[i].id+". "+menu[i].name+"   - "+menu[i].price);
                    }
                    System.Console.WriteLine("__________________________");
                    System.Console.Write("\nEnter Item Id To Remove (O to go back) : ");
                    int item = Convert.ToInt32(Console.ReadLine());

                    if (item>0 && item<=menu.Length)
                    {
                        
                        for (int i = item - 1; i < menu.Length - 1; i++)  // item-1 for 0 based index (Shift items left to remove the selected item) []
                        {
                            menu[i] = menu[i + 1];
                            menu[i].id = i + 1;   // update id to maintain consistency
                        }
                        menu[menu.Length-1] = null;
                        
                        int newlen = menu.Length-1;
                        MenuItem[] newmenu = new MenuItem[newlen];
                        for (int i = 0; i < newlen; i++)
                        {
                            newmenu[i] = menu[i];
                        }
                        menu = newmenu;

                        System.Console.WriteLine("Item removed successfully");
                    }
                    else if (item==0) { continue; }
                    else 
                    {
                        System.Console.WriteLine("Invali id!");
                    }
                }
            }
        }
    }        
}


class Program   // main program class (application entry point) [contains Main method]
{
    public static void Main()  // main method (static method) [application entry point]
    {
        System.Console.WriteLine();
        Greet o=new Greet();
        o.greeting();
        MenuItem[] menu = new MenuItem[6];
        menu[0] = new MenuItem(1, "Burger      ", 150);
        menu[1] = new MenuItem(2, "Pizza       ", 350);
        menu[2] = new MenuItem(3, "Pasta       ", 180);
        menu[3] = new MenuItem(4, "Rice Bowl   ", 170);
        menu[4] = new MenuItem(5, "Cold Coffee ", 120);
        menu[5] = new MenuItem(6, "Soft Drinks ", 30);
        
        Cart cart = new Cart();
        Usersystem user = new Usersystem();

        while (true)
        {
            System.Console.WriteLine("=====OPTONS=====");
            System.Console.WriteLine(" 1. Show Menu");
            System.Console.WriteLine(" 2. View Cart");
            System.Console.WriteLine(" 3. Remove Item");
            System.Console.WriteLine(" 4. Check Out");
            System.Console.WriteLine(" 5. Exit");
            System.Console.WriteLine(" 6. Login / Signup");
            System.Console.Write("Select Option : ");
            int input = Convert.ToInt32(Console.ReadLine());
        
        if (input==1)
        {
            while (true)
            {
                System.Console.WriteLine("\n"+"==============MENU==============");
                for (int i=0; i<menu.Length; i++)
                {
                    menu[i].print();
                }
                System.Console.WriteLine("================================");
                System.Console.Write("\n"+"Enter Item ID To Add(0 to go back) : ");
                int id = Convert.ToInt32(Console.ReadLine());

                MenuItem s=null;

                if (id == 0) {break;}
                else if (id>menu.Length && s==null)
                    {
                        System.Console.WriteLine("Invalid Choice!");
                        continue;
                    }
                else
                {
                for (int i=0; i<menu.Length; i++)
                    {
                        if (menu[i].id == id)
                        {
                            s = menu[i];
                        }
                    }
                    System.Console.Write("Enter Quantity : ");

                    string qty = Console.ReadLine();
                    if (qty == "" || qty == null)
                        {
                            cart.add(s);
                            continue;
                        }
                    int q = Convert.ToInt32(qty);
                    cart.add(s, q);
                    cart.showcart();  
                }
            }
        }
        else if (input == 2)
            {
                cart.showcart();
            }
        else if (input == 3)
            {
                cart.showcart();
                System.Console.Write("Enter item number to Remove : ");
                int rem = Convert.ToInt32(Console.ReadLine());
                cart.removeitem(rem);
                
            }
        else if (input == 4)
            {
                if (user.loguser == null)
                {
                    System.Console.WriteLine("You must login/signup before placing order!");
                    continue;
                }

                cart.showcart();
                double subt = cart.Total() + Cart.Vatcal(cart.Total()) ;
                System.Console.WriteLine("-----* FINAL BILL *-----");
                System.Console.WriteLine("   Total     : "+cart.Total());
                System.Console.WriteLine("   VAT-(5%)  : "+Cart.Vatcal(cart.Total()));
                System.Console.WriteLine("  -------------------  ");
                System.Console.WriteLine("   Sub-Total : "+subt);
                System.Console.WriteLine("------------------------");
                System.Console.WriteLine("Thank you "+user.loguser+" for visiting us!!!");

                for (int i=0; i<user.cuscount; i++)
                {
                    if (user.cname[i] == user.loguser)
                    {
                        user.uocart[i] = new Cart();
                        for (int j=0; j<cart.count; j++)
                        {
                            user.uocart[i].cart[j] = new OrderItem(cart.cart[j]);
                        }
                        user.uocart[i].count = cart.count;
                        break;
                    }
                }

                cart.empty();
                continue;
            }
        else if (input == 5)
            {
                System.Console.WriteLine("Exiting.....");
                break;
            }
        else if (input == 6)
            {                
                while (true)
                {
                System.Console.WriteLine("--------------");
                System.Console.WriteLine("1| Log-in "+"\n2| Sign-up "+"\n3| Admin-Panel "+"\n4| Log-out ");
                System.Console.Write("Select an option to proceed(0 to go back) : ");
                int log = Convert.ToInt32(Console.ReadLine());

                    if (log==0) {break;}
                    
                    if (log == 1)
                    {
                        user.login();
                        continue;
                    }
                    else if (log == 2)
                    {
                        user.signup();
                        continue;
                    }
/*admin panel*/     else if (log ==3) 
                    {
                        AdminP adp = new AdminP(user, cart, menu);
                        adp.panel();
                        menu = adp.menu;
                    }
                    else if (log ==4)
                    {
                        user.logout();
                    }
                    else
                    { 
                        System.Console.WriteLine("invalid Selection!");
                    
                    }
                    break;
                }
            }

            else {System.Console.WriteLine("Invalid Option Selection!");};

        }
    }
}
