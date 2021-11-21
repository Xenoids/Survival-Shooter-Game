using UnityEngine;
using System.Collections.Generic;
public class InputHandler : MonoBehaviour
{
    public PlayerMovement pMovement;
    public PlayerShooting PShooting;

    // queue list Command
    Queue<Command> commands = new Queue<Command>();

    void FixedUpdate()
    {
        // handle input Movement
        Command moveCommand = InputMovementHandling();

        if(moveCommand != null)
        {
            commands.Enqueue(moveCommand);

            moveCommand.Execute();
        }

    }

    void Update()
    {
        // handle shoot
        Command shootCommand = InputShootHandling();

        if(shootCommand != null) shootCommand.Execute();
    }

    Command InputMovementHandling()
    {
        // cek sesuai apa nda
        if (Input.GetKey(KeyCode.D))
        {
            return new MoveCommand(pMovement, 1, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            return new MoveCommand(pMovement, -1, 0);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            return new MoveCommand(pMovement, 0, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return new MoveCommand(pMovement, 0, -1);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            //Undo movement
            return Undo();
        }
        else
        {
            return new MoveCommand(pMovement, 0 , 0);
        }
           
    }

    Command Undo()
    {
        // jika queue tdk kosong, undo
        if(commands.Count > 0)
        {
            Command undoCommand = commands.Dequeue();
            undoCommand.UnExecute();
        }
        return null;
    }

    Command InputShootHandling()
    {
        // nembak trigger shoot commands
        if(Input.GetButtonDown("Fire1"))
        {
            return new ShootCommand(PShooting);
        }
        else return null;
    }

}