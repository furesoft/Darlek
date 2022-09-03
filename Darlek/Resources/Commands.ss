(open 'cli-vector 'ObjectModel 'conversion)

(define myCommand (lambda (args)
	(display "Hello From Scheme :)")
))

(register-command 
	"my-command"
	myCommand
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