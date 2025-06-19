
# Darlek

A simple and useful recipe crawler and ebook generator

## Run

1. Install .Net 8
2. Run `dotnet run` in the folder where the sln file is 
3. Enjoy :D

# Configuration
`darlek config key value`

# Syncrhonize Recipes with Grocy
1. Configure URL and ApiKey initialially. (The url has to point to the api endpoint)
   ```
  darlek config GROCY_URL https://mygrocyinstance.de/public/index.php/api/
  darlek config GROCY_APIKEY 123456789
   ```
2. You can synchronize already saved recipes by selecting the menu item in `Managing Recipes` or using this command:
`darlek grocy-sync --url https://www.chefkoch.de/rezepte/599651159698445/Quiche-mit-Lauch-und-Schinken.html`

## Screenshots

![Screenshot 2024-05-18 09 46 02](https://github.com/furesoft/Darlek/assets/4117602/fb15c255-9153-4311-9a61-292cc2481699)

![Screenshot 2024-05-18 09 46 42](https://github.com/furesoft/Darlek/assets/4117602/57752070-5842-4271-a2dc-809d403b1d53)

![Screenshot 2024-05-18 09 47 18](https://github.com/furesoft/Darlek/assets/4117602/8fb2ece2-cf5b-4d70-bbc0-c520373992f5)
