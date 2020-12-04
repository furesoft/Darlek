(define myCommand (lambda (args)
	(display (get-option args "query"))
))

(register-command 
	"my-command"
	"Do Something"
	"todo --query "
	myCommand
)