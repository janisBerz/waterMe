# waterMe

Monitor soil moisture levels using RPi and get notified!

## RPi requirments

### System dependencies

Using Rasbian install:

- Install .Net Core 3.1

    ```bash
    sudo mkdir -p $HOME/dotnet
    sudo tar zxf dotnet-sdk-3.1.100-linux-arm.tar.gz -C $HOME/dotnet
    export DOTNET_ROOT=$HOME/dotnet
    export PATH=$PATH:$HOME/dotnet
    ```

- Install the VS remote debugger on your Pi by running this command:

    ```bash
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg
    ```

- To debug you will need to run the program as root, so we'll need to be able to remote launch the program as root as well. For this, we need to first set a password for the root user in your pi, which you can do by running:

    ```bash
    sudo passwd root
    ```

- Then we need to enable ssh connections using root, by running :

```bash
sudo nano /etc/ssh/sshd_config
```

and adding a line that reads:
`PermitRootLogin yes`
reboot the pi: `sudo reboot`

Development machine:


#### For remote debugging

 Start here: https://github.com/OmniSharp/omnisharp-vscode/wiki/Attaching-to-remote-processes


```sql
SELECT * FROM c WHERE c.HostName="raspberrypi" ORDER BY c._ts DESC
```