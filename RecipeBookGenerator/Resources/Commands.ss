(define myCommand (lambda (args)
	(display (get-option args "query"))
))

(register-command (
	"todo"
	"Do Something"
	"todo --query "
	myCommand
))