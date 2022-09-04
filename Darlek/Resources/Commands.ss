(open 'ObjectModel 'conversion)

(define myCommand (lambda (menu)
	(display "Hello From Scheme :)")
	(wait-menu menu)
))

(register-command 
	"my-command"
	myCommand
	manage-menu
)

(define myImport 
	(lambda args
		(define obj (make-object))
		(set-property obj 'name "scheme test")
		(set-property obj 'content (binary->string (car args)))

		obj
	)
)

(register-importer '.t myImport)